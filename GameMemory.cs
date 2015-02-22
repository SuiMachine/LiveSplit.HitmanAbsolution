using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.HMA
{
    class GameMemory
    {
        public enum SplitArea : int
        {
            None,
            C0_Garden,
            C0_Greenhouse,
            C0_Cliffside,
            C0_Mansion,
            C0_Kottidor,
            C1_Chinatown,
            C2_TerminusBottom,
            C2_TerminusUpper,
            C3_BurningHotel,
            C3_Libary,
            C3_PigeonCoop,
            C3_ShangriLa,
            C3_Trainstation,
            C4_Countryard,
            C4_VixenClub,
            C4_DressingRooms,
            C4_DerelictBuilding,
            C4_Conveniencestore,
            C4_LoadingArea,
            C4_ChineseNewYear,
            C6_Victoria,        //No Chapter 5
            C6_Orphanage,
            C6_CentralHeating,
            C7_BallsOfFire,
            C8_GunShop,
            C9_StreetsOfHope,
            C9_Barbershop,
            C10_TheDesert,
            C11_DeadEnd,
            C11_OldMill,
            C11_Descent,
            C11_FactoryCompound,
            C12_TestFacility,
            C12_Decontamination,
            C12_RnD,
            C13_PatriotsHangar,
            C13_Arena,
            C14_Parking,
            C14_Reception,
            C14_Cornfield,      //No Chapters 15, 16
            C17_Courthouse,
            C17_HoldingCells,
            C17_Prision,
            C18_CountyJail,
            C18_Outgunned,
            C18_Burn,
            C18_HopeFiar,       //No Chapters 19, 20
            C21_TailorShop,
            C22_BlackwaterPark,
            C22_Penthouse,      //No Chapters 23
            C24_BlackwaterRoof,
            C25_CementaryEntrence,
            C25_BurnwoodFamilyTomb,
            C25_Crematorium,
        }

        public event EventHandler OnFirstLevelLoading;
        public event EventHandler OnPlayerGainedControl;
        public event EventHandler OnLoadStarted;
        public event EventHandler OnLoadFinished;
        public delegate void SplitCompletedEventHandler(object sender, SplitArea type, uint frame);
        public event SplitCompletedEventHandler OnSplitCompleted;

        private Task _thread;
        private CancellationTokenSource _cancelSource;
        private SynchronizationContext _uiThread;
        private List<int> _ignorePIDs;
        private HMASettings _settings;

        private DeepPointer _IsInLoadingScreen;
        private DeepPointer _IsRosewoodCutscene;
        private DeepPointer _IsInMenu;
        private DeepPointer _IsOutOfFocus;
        private DeepPointer _IsTerminusElevatorLoading;
        private DeepPointer _IsFinaleExplosion;

        private DeepPointer _Level;
        private DeepPointer _Section;
        private DeepPointer _IsResultScreen;

        private enum ExpectedDllSizes
        {
            HMASteam = 36777984,
        }

        public bool[] splitStates { get; set; }

        public void resetSplitStates()
        {
            for (int i = 0; i <= (int)SplitArea.C25_Crematorium; i++)
            {
                splitStates[i] = false;
            }
        }

        public GameMemory(HMASettings componentSettings)
        {
            _settings = componentSettings;
            splitStates = new bool[(int)SplitArea.C25_Crematorium + 1];
            _IsInLoadingScreen = new DeepPointer(0xE53E20);         //not actual, but close enough
            _IsRosewoodCutscene = new DeepPointer(0xE321FC);
            _IsInMenu = new DeepPointer(0xD61C7B);
            _IsOutOfFocus = new DeepPointer(0xE31F4D);
            _IsTerminusElevatorLoading = new DeepPointer(0x00E39520, 0x38); // == 1 if a loadscreen is happening
            _IsFinaleExplosion = new DeepPointer(0x00D610B4, 0x8, 0x5FC, 0x10, 0x74, 0x508); // == 1 if EXPLOSION!!!!!!1

            _Level = new DeepPointer(0xE20F48);         //0 for first level, some never used
            _Section = new DeepPointer(0xD60F94);
            _IsResultScreen = new DeepPointer(0xE213E8);

            resetSplitStates();

            _ignorePIDs = new List<int>();
        }

        public void StartMonitoring()
        {
            if (_thread != null && _thread.Status == TaskStatus.Running)
            {
                throw new InvalidOperationException();
            }
            if (!(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
            {
                throw new InvalidOperationException("SynchronizationContext.Current is not a UI thread.");
            }

            _uiThread = SynchronizationContext.Current;
            _cancelSource = new CancellationTokenSource();
            _thread = Task.Factory.StartNew(MemoryReadThread);
        }

        public void Stop()
        {
            if (_cancelSource == null || _thread == null || _thread.Status != TaskStatus.Running)
            {
                return;
            }

            _cancelSource.Cancel();
            _thread.Wait();
        }

        void MemoryReadThread()
        {
            Debug.WriteLine("[NoLoads] MemoryReadThread");

            while (!_cancelSource.IsCancellationRequested)
            {
                try
                {
                    Debug.WriteLine("[NoLoads] Waiting for HMA.exe...");
                    bool isActuallyLoading;
                    bool prevIsInLoadingScreen = false;
                    bool loadingStarted = false;
                    uint frameCounter = 0;
                    
                    Process game;
                    while ((game = GetGameProcess()) == null)
                    {
                        isActuallyLoading = true;
                        if (isActuallyLoading)
                        {
                            Debug.WriteLine(String.Format("[NoLoads] Load Start - {0}", frameCounter));

                            loadingStarted = true;

                            // pause game timer
                            _uiThread.Post(d =>
                            {
                                if (this.OnLoadStarted != null)
                                {
                                    this.OnLoadStarted(this, EventArgs.Empty);
                                }
                            }, null);
                        }

                        Thread.Sleep(250);
                        if (_cancelSource.IsCancellationRequested)
                        {
                            return;
                        }
                    }

                    Debug.WriteLine("[NoLoads] Got games process!");

                    bool prevIsInMenu = false;
                    bool prevIsOutOfFocus = false;
                    bool prevIsRosewoodCutscene = false;
                    bool prevIsFinaleExplosion = false;

                    int prevIsTerminusElevatorLoading = 0;
                    int prevLevel=25;
                    int prevSection=0;

                    bool prevIsResultScreen = false;

                    bool IsInLoadingScreen, IsInMenu, IsOutOfFocus, IsRosewoodCutscene, IsResultScreen, IsFinaleExplosion;
                    int CurrentLevel, CurrentSection, IsTerminusElevatorLoading;

                    while (!game.HasExited)
                    {
                        _IsInLoadingScreen.Deref(game, out IsInLoadingScreen);
                        _IsInMenu.Deref(game, out IsInMenu);
                        _IsOutOfFocus.Deref(game, out IsOutOfFocus);
                        _IsRosewoodCutscene.Deref(game, out IsRosewoodCutscene);
                        _IsTerminusElevatorLoading.Deref(game, out IsTerminusElevatorLoading);
                        _Level.Deref(game, out CurrentLevel);
                        _Section.Deref(game, out CurrentSection);
                        _IsResultScreen.Deref(game, out IsResultScreen);
                        _IsFinaleExplosion.Deref(game, out IsFinaleExplosion);

                        if(CurrentLevel != prevLevel || CurrentSection != prevSection || IsResultScreen != prevIsResultScreen || IsTerminusElevatorLoading != prevIsTerminusElevatorLoading || IsFinaleExplosion != prevIsFinaleExplosion)              //All of the level splits
                        {
                            if(CurrentLevel == 0 && CurrentSection == 1)
                            {
                                Split(SplitArea.C0_Garden, frameCounter);
                            }
                            else if(CurrentLevel == 0 && CurrentSection == 2)
                            {
                                Split(SplitArea.C0_Greenhouse, frameCounter);
                            }
                            else if (CurrentLevel == 0 && CurrentSection == 3)
                            {
                                Split(SplitArea.C0_Cliffside, frameCounter);
                            }
                            else if(CurrentLevel == 0 && CurrentSection == 4 && !IsResultScreen)
                            {
                                Split(SplitArea.C0_Mansion, frameCounter);
                            }
                            else if(CurrentLevel == 0 && CurrentSection == 4 && IsResultScreen)
                            {
                                Split(SplitArea.C0_Kottidor, frameCounter);
                            }
                            else if(CurrentLevel == 1 && CurrentSection == 0 && IsResultScreen)
                            {
                                Split(SplitArea.C1_Chinatown, frameCounter);
                            }
                            else if(CurrentLevel == 2 && CurrentSection == 0 && IsTerminusElevatorLoading==1)
                            {
                                Split(SplitArea.C2_TerminusBottom, frameCounter);
                            }
                            else if(CurrentLevel == 2 && CurrentSection == 3 && IsResultScreen)
                            {
                                Split(SplitArea.C2_TerminusUpper, frameCounter);
                            }                            
                            else if(CurrentLevel == 3 && CurrentSection == 1)           //Chapter 3
                            {
                                Split(SplitArea.C3_BurningHotel, frameCounter);
                            }
                            else if(CurrentLevel == 3 && CurrentSection == 2)
                            {
                                Split(SplitArea.C3_Libary, frameCounter);
                            }                            
                            else if(CurrentLevel == 3 && CurrentSection == 4)
                            {
                                Split(SplitArea.C3_PigeonCoop, frameCounter);
                            }
                            else if(CurrentLevel == 3 && CurrentSection == 5)
                            {
                                Split(SplitArea.C3_ShangriLa, frameCounter);
                            }
                            else if(CurrentLevel == 3 && CurrentSection == 6 && IsResultScreen)
                            {
                                Split(SplitArea.C3_Trainstation, frameCounter);
                            }
                            else if(CurrentLevel == 4 && CurrentSection == 1)           //Chapter 4
                            {
                                Split(SplitArea.C4_Countryard, frameCounter);
                            }
                            else if(CurrentLevel == 4 && CurrentSection == 2)
                            {
                                Split(SplitArea.C4_VixenClub, frameCounter);
                            }
                            else if(CurrentLevel == 4 && CurrentSection == 3)
                            {
                                Split(SplitArea.C4_DressingRooms, frameCounter);
                            }
                            else if(CurrentLevel == 4 && CurrentSection == 4)
                            {
                                Split(SplitArea.C4_DerelictBuilding, frameCounter);
                            }                            
                            else if(CurrentLevel == 4 && CurrentSection == 5)
                            {
                                Split(SplitArea.C4_Conveniencestore, frameCounter);
                            }                            
                            else if(CurrentLevel == 4 && CurrentSection == 6)
                            {
                                Split(SplitArea.C4_LoadingArea, frameCounter);
                            }
                            else if(CurrentLevel == 4 && CurrentSection == 7 && IsResultScreen)
                            {
                                Split(SplitArea.C4_ChineseNewYear, frameCounter);
                            }
                            else if(CurrentLevel == 6 && CurrentSection == 1)           //Chapter 6
                            {
                                Split(SplitArea.C6_Victoria, frameCounter);
                            }
                            else if(CurrentLevel == 6 && CurrentSection == 2)
                            {
                                Split(SplitArea.C6_Orphanage, frameCounter);
                            }
                            else if(CurrentLevel == 6 && CurrentSection == 3 && IsResultScreen)
                            {
                                Split(SplitArea.C6_CentralHeating, frameCounter);
                            }
                            else if(CurrentLevel == 7 && CurrentSection == 0 && IsResultScreen) //Chapter 7
                            {
                                Split(SplitArea.C7_BallsOfFire, frameCounter);
                            }                            
                            else if(CurrentLevel == 8 && CurrentSection == 0 && IsResultScreen) //Chapter 8
                            {
                                Split(SplitArea.C8_GunShop, frameCounter);
                            }                            
                            else if(CurrentLevel == 9 && CurrentSection == 1)     //Chapter 9
                            {
                                Split(SplitArea.C9_StreetsOfHope, frameCounter);
                            }                                                        
                            else if(CurrentLevel == 9 && CurrentSection == 2 && IsResultScreen)
                            {
                                Split(SplitArea.C9_Barbershop, frameCounter);
                            }                                                                                    
                            else if(CurrentLevel == 10 && CurrentSection == 0 && IsResultScreen)    //Chapter 10
                            {
                                Split(SplitArea.C10_TheDesert, frameCounter);
                            }                                                                                                                
                            else if(CurrentLevel == 11 && CurrentSection == 2)      //Chapter 11
                            {
                                Split(SplitArea.C11_DeadEnd, frameCounter);
                            }                                                                                                                                            
                            else if(CurrentLevel == 11 && CurrentSection == 3)
                            {
                                Split(SplitArea.C11_OldMill, frameCounter);
                            }                                                                                                                                                                        
                            else if(CurrentLevel == 11 && CurrentSection == 4 && !IsResultScreen)
                            {
                                Split(SplitArea.C11_Descent, frameCounter);
                            }
                            else if(CurrentLevel == 11 && CurrentSection == 4 && IsResultScreen)
                            {
                                Split(SplitArea.C11_FactoryCompound, frameCounter);
                            }                                                                                  
                            else if(CurrentLevel == 12 && CurrentSection == 1)      //Chapter 12
                            {
                                Split(SplitArea.C12_TestFacility, frameCounter);
                            }
                            else if(CurrentLevel == 12 && CurrentSection == 2 && !IsResultScreen)
                            {
                                Split(SplitArea.C12_Decontamination, frameCounter);
                            }
                            else if(CurrentLevel == 12 && CurrentSection == 2 && IsResultScreen)
                            {
                                Split(SplitArea.C12_RnD, frameCounter);
                            }
                            else if(CurrentLevel == 13 && CurrentSection == 1 && !IsResultScreen)      //Chapter 13
                            {
                                Split(SplitArea.C13_PatriotsHangar, frameCounter);
                            }
                            else if(CurrentLevel == 13 && CurrentSection == 1 && IsResultScreen)
                            {
                                Split(SplitArea.C13_Arena, frameCounter);
                            }
                            else if(CurrentLevel == 14 && CurrentSection == 1)      //Chapter 14
                            {
                                Split(SplitArea.C14_Parking, frameCounter);
                            }
                            else if(CurrentLevel == 14 && CurrentSection == 2)
                            {
                                Split(SplitArea.C14_Reception, frameCounter);
                            }
                            else if(CurrentLevel == 14 && CurrentSection == 3 && IsResultScreen)
                            {
                                Split(SplitArea.C14_Cornfield, frameCounter);
                            }
                            else if(CurrentLevel == 17 && CurrentSection == 1)    //Chapter 17
                            {
                                Split(SplitArea.C17_Courthouse, frameCounter);
                            }
                            else if(CurrentLevel == 17 && CurrentSection == 2)
                            {
                                Split(SplitArea.C17_HoldingCells, frameCounter);
                            }
                            else if(CurrentLevel == 17 && CurrentSection == 3 && IsResultScreen)
                            {
                                Split(SplitArea.C17_Prision, frameCounter);
                            }                            
                            else if(CurrentLevel == 18 && CurrentSection == 1)  //Chapter 18
                            {
                                Split(SplitArea.C18_CountyJail, frameCounter);
                            }                            
                            else if(CurrentLevel == 18 && CurrentSection == 2)
                            {
                                Split(SplitArea.C18_Outgunned, frameCounter);
                            }                            
                            else if(CurrentLevel == 18 && CurrentSection == 3)
                            {
                                Split(SplitArea.C18_Burn, frameCounter);
                            }                            
                            else if(CurrentLevel == 18 && CurrentSection == 5 && IsResultScreen)
                            {
                                Split(SplitArea.C18_HopeFiar, frameCounter);
                            }                         
                            else if(CurrentLevel == 21 && CurrentSection == 0 && IsResultScreen)    //Chapter 21
                            {
                                Split(SplitArea.C21_TailorShop, frameCounter);
                            }                         
                            else if(CurrentLevel == 22 && CurrentSection == 1)                      //Chapter 22
                            {
                                Split(SplitArea.C22_BlackwaterPark, frameCounter);
                            }                         
                            else if(CurrentLevel == 22 && CurrentSection == 2 && IsResultScreen)
                            {
                                Split(SplitArea.C22_Penthouse, frameCounter);
                            }                         
                            else if(CurrentLevel == 24 && CurrentSection == 0 && IsResultScreen)    //Chapter 24
                            {
                                Split(SplitArea.C24_BlackwaterRoof, frameCounter);
                            }                         
                            else if(CurrentLevel == 25 && CurrentSection == 1)                      //Chapter 25
                            {
                                Split(SplitArea.C25_CementaryEntrence, frameCounter);
                            }                         
                            else if(CurrentLevel == 25 && CurrentSection == 2 && !IsFinaleExplosion)
                            {
                                Split(SplitArea.C25_BurnwoodFamilyTomb, frameCounter);
                            }                         
                            else if(CurrentLevel == 25 && CurrentSection == 2 && IsFinaleExplosion)
                            {
                                Split(SplitArea.C25_Crematorium, frameCounter);
                            }
                        }

                        if (IsInLoadingScreen != prevIsInLoadingScreen || IsInMenu != prevIsInMenu || IsOutOfFocus != prevIsOutOfFocus || IsTerminusElevatorLoading != prevIsTerminusElevatorLoading || IsRosewoodCutscene != prevIsRosewoodCutscene || CurrentLevel != prevLevel || CurrentSection != prevSection)
                        {
                            if (IsInLoadingScreen == true || IsRosewoodCutscene == true)
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
                                else if ((CurrentLevel == 2 && CurrentSection == 0 && IsTerminusElevatorLoading == 1) || (CurrentLevel == 2 && CurrentSection == 1))
                                {
                                    isActuallyLoading = true;
                                }
                                else
                                    isActuallyLoading = false;
                            }

                            if (isActuallyLoading)
                            {
                                Debug.WriteLine(String.Format("[NoLoads] Load Start - {0}", frameCounter));

                                loadingStarted = true;

                                // pause game timer
                                _uiThread.Post(d =>
                                {
                                    if (this.OnLoadStarted != null)
                                    {
                                        this.OnLoadStarted(this, EventArgs.Empty);
                                    }
                                }, null);

                                if(CurrentLevel==0 && CurrentSection==0 && prevLevel==0 && prevSection==0 && IsInMenu && IsInLoadingScreen)       //That should fix it.
                                {
                                    _uiThread.Post(d =>
                                    {
                                        if (this.OnFirstLevelLoading != null)
                                        {
                                            this.OnFirstLevelLoading(this, EventArgs.Empty);
                                        }
                                    }, null);
                                }

                            }
                            else
                            {
                                Debug.WriteLine(String.Format("[NoLoads] Load End - {0}", frameCounter));

                                if (loadingStarted)
                                {
                                    loadingStarted = false;

                                    // unpause game timer
                                    _uiThread.Post(d =>
                                    {
                                        if (this.OnLoadFinished != null)
                                        {
                                            this.OnLoadFinished(this, EventArgs.Empty);
                                        }
                                    }, null);

                                    if(CurrentLevel==0 && CurrentSection==0)
                                    {
                                        _uiThread.Post(d =>
                                        {
                                            if (this.OnPlayerGainedControl != null)
                                            {
                                                this.OnPlayerGainedControl(this, EventArgs.Empty);
                                            }
                                        }, null);
                                    }
                                }
                            }
                        }

                        prevIsInLoadingScreen = IsInLoadingScreen;
                        prevIsInMenu = IsInMenu;
                        prevIsOutOfFocus = IsOutOfFocus;
                        prevIsTerminusElevatorLoading = IsTerminusElevatorLoading;
                        prevIsRosewoodCutscene = IsRosewoodCutscene;
                        prevLevel = CurrentLevel;
                        prevSection = CurrentSection;
                        prevIsResultScreen = IsResultScreen;
                        prevIsFinaleExplosion = IsFinaleExplosion;

                        frameCounter++;

                        Thread.Sleep(15);

                        if (_cancelSource.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    Thread.Sleep(1000);
                }
            }
        }

        private void Split(SplitArea split, uint frame)
        {
            Debug.WriteLine(String.Format("[NoLoads] split {0} - {1}", split, frame));
            _uiThread.Post(d =>
            {
                if (this.OnSplitCompleted != null)
                {
                    this.OnSplitCompleted(this, split, frame);
                }
            }, null);
        }

        Process GetGameProcess()
        {
            Process game = Process.GetProcesses().FirstOrDefault(p => p.ProcessName.ToLower() == "hma" && !p.HasExited && !_ignorePIDs.Contains(p.Id));
            if (game == null)
            {
                return null;
            }

            if (game.MainModule.ModuleMemorySize != (int)ExpectedDllSizes.HMASteam)
            {
                _ignorePIDs.Add(game.Id);
                _uiThread.Send(d => MessageBox.Show("Unexpected game version. HMA Steam is required.", "LiveSplit.HMA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error), null);
                return null;
            }

            return game;
        }
    }
}
