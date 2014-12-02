﻿using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.HMA
{
    class HMAComponent : LogicComponent
    {
        private GameMemory _gameMemory;
        private LiveSplitState _state;

        public HMAComponent(LiveSplitState state)
        {
            _state = state;

            _gameMemory = new GameMemory();
            _gameMemory.OnLoadingChanged += gameMemory_OnLoadingChanged;
            _gameMemory.StartReading();
        }

        public override void Dispose()
        {
            if (_gameMemory != null)
                _gameMemory.Stop();
        }

        void gameMemory_OnLoadingChanged(object sender, LoadingChangedEventArgs e)
        {
            _state.IsGameTimePaused = e.IsLoading;
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return null;
        }

        public override void RenameComparison(string oldName, string newName)
        {
            
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            
        }

        public override string ComponentName
        {
            get { return "HMA"; }
        }

        public override void SetSettings(XmlNode settings)
        {
           
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return document.CreateElement("Settings");
        }
    }
}
