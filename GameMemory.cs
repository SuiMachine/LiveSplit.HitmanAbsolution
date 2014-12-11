using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.HMA
{
    class GameMemory
    {
        public event EventHandler<LoadingChangedEventArgs> OnLoadingChanged;

        private Task _thread;
        private SynchronizationContext _uiThread;
        private CancellationTokenSource _cancelSource;

        private DeepPointer _isTerminusElevatorLoading;

        public void StartReading()
        {
            if (_thread != null && _thread.Status == TaskStatus.Running)
                throw new InvalidOperationException();
            if (!(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
                throw new InvalidOperationException("SynchronizationContext.Current is not a UI thread.");

            _isTerminusElevatorLoading = new DeepPointer(0x00E39520, 0x38); // == 1 if a loadscreen is happening

            _cancelSource = new CancellationTokenSource();
            _uiThread = SynchronizationContext.Current;
            _thread = Task.Factory.StartNew(() => MemoryReadThread(_cancelSource));
        }

        public void Stop()
        {
            if (_cancelSource == null || _thread == null || _thread.Status != TaskStatus.Running)
                return;

            _cancelSource.Cancel();
            _thread.Wait();
        }

        void MemoryReadThread(CancellationTokenSource cts)
        {
            while (true)
            {
                try
                {
                    Process game;
                    while (!this.TryGetGameProcess(out game))
                    {
                        Thread.Sleep(500);

                        if (cts.IsCancellationRequested)
                            goto ret;
                    }

                    this.HandleProcess(game, cts);

                    if (cts.IsCancellationRequested)
                        goto ret;
                }
                catch (Exception ex) // probably a Win32Exception on access denied to a process
                {
                    Trace.WriteLine(ex.ToString());
                    Thread.Sleep(1000);
                }
            }

        ret: ;
        }

        bool TryGetGameProcess(out Process p)
        {
            p = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToLower() == "hma");
            if (p == null || p.HasExited)
                return false;

            return true;
        }

        void HandleProcess(Process game, CancellationTokenSource cts)
        {
            bool prevIsInLoadingScreen = false;
            bool prevIsInMenu = false;
            bool prevIsOutOfFocus = false;
            bool prevIsTerminusElevatorLoading = false;
            bool prevIsRosewoodLoading = false;

            while (!game.HasExited && !cts.IsCancellationRequested)
            {
                bool isActuallyLoading, IsInLoadingScreen, IsInMenu, IsOutOfFocus, IsTerminusElevatorLoading, IsRosewoodLoading;
                game.ReadBool(game.MainModule.BaseAddress + 0xE53E20, out IsInLoadingScreen); //not actual, but close enough
                game.ReadBool(game.MainModule.BaseAddress + 0xE321FC, out IsRosewoodLoading); //or HMA.exe+E38CB4
                game.ReadBool(game.MainModule.BaseAddress + 0xD61C7B, out IsInMenu);
                game.ReadBool(game.MainModule.BaseAddress + 0xE31F4D, out IsOutOfFocus);
                _isTerminusElevatorLoading.Deref(game, out IsTerminusElevatorLoading);

                if (IsInLoadingScreen != prevIsInLoadingScreen || IsInMenu != prevIsInMenu || IsOutOfFocus != prevIsOutOfFocus || IsTerminusElevatorLoading != prevIsTerminusElevatorLoading || IsRosewoodLoading != prevIsRosewoodLoading) 
                {
                    if (IsInLoadingScreen == true)
                        isActuallyLoading = true;
                    else if (IsRosewoodLoading == true)
                        isActuallyLoading = true;
                    else
                    {
                        if (IsOutOfFocus == false)
                        {
                            if (IsInMenu == true)
                                isActuallyLoading = false;
                            else
                                isActuallyLoading = true;
                        }
                        else if (IsTerminusElevatorLoading == true)
                            isActuallyLoading = true;
                        else
                            isActuallyLoading = false;
                    }
                    _uiThread.Post(d =>
                    {
                        if (this.OnLoadingChanged != null)
                            this.OnLoadingChanged(this, new LoadingChangedEventArgs(isActuallyLoading));
                    }, null);
                }

                prevIsInLoadingScreen = IsInLoadingScreen;
                prevIsInMenu = IsInMenu;
                prevIsOutOfFocus = IsOutOfFocus;
                prevIsTerminusElevatorLoading = IsTerminusElevatorLoading;

                Thread.Sleep(15);
            }
        }

        private void DeepPointer()
        {
            throw new NotImplementedException();
        }
    }

    class LoadingChangedEventArgs : EventArgs
    {
        public bool IsLoading { get; private set; }

        public LoadingChangedEventArgs(bool isLoading)
        {
            this.IsLoading = isLoading;
        }
    }
}
