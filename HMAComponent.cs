using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;

namespace LiveSplit.HMA
{
    class HMAComponent : LogicComponent
    {
        public override string ComponentName
        {
            get { return "HMA"; }
        }

        public HMASettings Settings { get; set; }

        public bool Disposed { get; private set; }
        public bool IsLayoutComponent { get; private set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;

        public HMAComponent(LiveSplitState state, bool isLayoutComponent)
        {
            _state = state;
            this.IsLayoutComponent = isLayoutComponent;

            this.Settings = new HMASettings();

           _timer = new TimerModel { CurrentState = state };

            _gameMemory = new GameMemory(this.Settings);
            _gameMemory.OnFirstLevelLoading += gameMemory_OnFirstLevelLoading;
            _gameMemory.OnPlayerGainedControl += gameMemory_OnPlayerGainedControl;
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            _gameMemory.OnSplitCompleted += gameMemory_OnSplitCompleted;
            state.OnStart += State_OnStart;
            _gameMemory.StartMonitoring();
        }

        public override void Dispose()
        {
            this.Disposed = true;

            _state.OnStart -= State_OnStart;

            if (_gameMemory != null)
            {
                _gameMemory.Stop();
            }

        }

        void State_OnStart(object sender, EventArgs e)
        {
            _gameMemory.resetSplitStates();
        }

        void gameMemory_OnFirstLevelLoading(object sender, EventArgs e)
        {
            if (this.Settings.AutoReset)
            {
                _timer.Reset();
            }
        }

        void gameMemory_OnPlayerGainedControl(object sender, EventArgs e)
        {
            if (this.Settings.AutoStart)
            {
                _timer.Start();
            }
        }

        void gameMemory_OnLoadStarted(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = true;
        }

        void gameMemory_OnLoadFinished(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = false;
        }

        void gameMemory_OnSplitCompleted(object sender, GameMemory.SplitArea split, uint frame)
        {
            Debug.WriteLineIf(split != GameMemory.SplitArea.None, String.Format("[NoLoads] Trying to split {0}, State: {1} - {2}", split, _gameMemory.splitStates[(int)split], frame));
            if (_state.CurrentPhase == TimerPhase.Running && !_gameMemory.splitStates[(int)split] &&
                ((split == GameMemory.SplitArea.C0_Garden && this.Settings._00Garden) ||
                (split == GameMemory.SplitArea.C0_Greenhouse && this.Settings._00Greenhouse) ||
                (split == GameMemory.SplitArea.C0_Cliffside && this.Settings._00Cliffside) ||
                (split == GameMemory.SplitArea.C0_Mansion && this.Settings._00Mansion) ||
                (split == GameMemory.SplitArea.C0_Kottidor && this.Settings._00MansionUpper) ||
                (split == GameMemory.SplitArea.C1_Chinatown && this.Settings._01Chinatown) ||
                (split == GameMemory.SplitArea.C2_TerminusBottom && this.Settings._02TerminusBottom) ||
                (split == GameMemory.SplitArea.C2_TerminusUpper && this.Settings._02TerminusUpper) ||
                (split == GameMemory.SplitArea.C3_BurningHotel && this.Settings._03BurningHotel) ||
                (split == GameMemory.SplitArea.C3_Libary && this.Settings._03Libary) ||
                (split == GameMemory.SplitArea.C3_PigeonCoop && this.Settings._03PigeonCoop) ||
                (split == GameMemory.SplitArea.C3_ShangriLa && this.Settings._03ShangriLa) ||
                (split == GameMemory.SplitArea.C3_Trainstation && this.Settings._03Trainstation) ||
                (split == GameMemory.SplitArea.C4_Countryard && this.Settings._04Countryard) ||
                (split == GameMemory.SplitArea.C4_VixenClub && this.Settings._04VixenClub) ||
                (split == GameMemory.SplitArea.C4_DressingRooms && this.Settings._04DressingRooms) ||
                (split == GameMemory.SplitArea.C4_DerelictBuilding && this.Settings._04DerelictBuilding) ||
                (split == GameMemory.SplitArea.C4_Conveniencestore && this.Settings._04Conveniencestore) ||
                (split == GameMemory.SplitArea.C4_LoadingArea && this.Settings._04LoadingArea) ||
                (split == GameMemory.SplitArea.C4_ChineseNewYear && this.Settings._04ChineseNewYear) ||
                (split == GameMemory.SplitArea.C6_Victoria && this.Settings._06Victoria) ||
                (split == GameMemory.SplitArea.C6_Orphanage && this.Settings._06Orphanage) ||
                (split == GameMemory.SplitArea.C6_CentralHeating && this.Settings._06CentralHeating) ||
                (split == GameMemory.SplitArea.C7_BallsOfFire && this.Settings._07BallsOfFire) ||
                (split == GameMemory.SplitArea.C8_GunShop && this.Settings._08GunShop) ||
                (split == GameMemory.SplitArea.C9_StreetsOfHope && this.Settings._09StreetsOfHope) ||
                (split == GameMemory.SplitArea.C9_Barbershop && this.Settings._09Barbershop) ||
                (split == GameMemory.SplitArea.C10_TheDesert && this.Settings._10TheDesert) ||
                (split == GameMemory.SplitArea.C11_DeadEnd && this.Settings._11DeadEnd) ||
                (split == GameMemory.SplitArea.C11_OldMill && this.Settings._11OldMill) ||
                (split == GameMemory.SplitArea.C11_Descent && this.Settings._11Descent) ||
                (split == GameMemory.SplitArea.C11_FactoryCompound && this.Settings._11FactoryCompound) ||
                (split == GameMemory.SplitArea.C12_TestFacility && this.Settings._12TestFacility) ||
                (split == GameMemory.SplitArea.C12_Decontamination && this.Settings._12Decontamination) ||
                (split == GameMemory.SplitArea.C12_RnD && this.Settings._12RnD) ||
                (split == GameMemory.SplitArea.C13_PatriotsHangar && this.Settings._13PatriotsHangar) ||
                (split == GameMemory.SplitArea.C13_Arena && this.Settings._13Arena) ||
                (split == GameMemory.SplitArea.C14_Parking && this.Settings._14Parking) ||
                (split == GameMemory.SplitArea.C14_Reception && this.Settings._14Reception) ||
                (split == GameMemory.SplitArea.C14_Cornfield && this.Settings._14Cornfield) ||
                (split == GameMemory.SplitArea.C17_Courthouse && this.Settings._17Courthouse) ||
                (split == GameMemory.SplitArea.C17_HoldingCells && this.Settings._17HoldingCells) ||
                (split == GameMemory.SplitArea.C17_Prision && this.Settings._17Prision) ||
                (split == GameMemory.SplitArea.C18_CountyJail && this.Settings._18CountyJail) ||
                (split == GameMemory.SplitArea.C18_Outgunned && this.Settings._18Outgunned) ||
                (split == GameMemory.SplitArea.C18_Burn && this.Settings._18Burn) ||
                (split == GameMemory.SplitArea.C18_HopeFiar && this.Settings._18HopeFiar) ||
                (split == GameMemory.SplitArea.C21_TailorShop && this.Settings._21TailorShop) ||
                (split == GameMemory.SplitArea.C22_BlackwaterPark && this.Settings._22BlackwaterPark) ||
                (split == GameMemory.SplitArea.C22_Penthouse && this.Settings._22Penthouse) ||
                (split == GameMemory.SplitArea.C24_BlackwaterRoof && this.Settings._24BlackwaterRoof) ||
                (split == GameMemory.SplitArea.C25_CementaryEntrence && this.Settings._25CementaryEntrence) ||
                (split == GameMemory.SplitArea.C25_BurnwoodFamilyTomb && this.Settings._25BurnwoodFamilyTomb) ||
                (split == GameMemory.SplitArea.C25_Crematorium && this.Settings._25Crematorium)))


            {
                Trace.WriteLine(String.Format("[NoLoads] {0} Split - {1}", split, frame));
                _timer.Split();
                _gameMemory.splitStates[(int)split] = true;
            }
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return this.Settings.GetSettings(document);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return this.Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            this.Settings.SetSettings(settings);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
        //public override void RenameComparison(string oldName, string newName) { }
    }
}
