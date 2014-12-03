using System.Reflection;
using LiveSplit.HMA;
using LiveSplit.UI.Components;
using System;
using LiveSplit.Model;

[assembly: ComponentFactory(typeof(HMAFactory))]

namespace LiveSplit.HMA
{
    public class HMAFactory : IComponentFactory
    {
        public string ComponentName
        {
            get { return "HMA"; }
        }

        public string Description
        {
            get { return "Load time remover for Hitman: Absolution"; }
        }

        public ComponentCategory Category
        {
            get { return ComponentCategory.Control; }
        }

        public IComponent Create(LiveSplitState state)
        {
            return new HMAComponent(state);
        }

        public string UpdateName
        {
            get { return this.ComponentName; }
        }

        public string UpdateURL
        {
            get { return "https://raw.githubusercontent.com/SuiMachine/LiveSplit.HitmanAbsolution/Master/"; }
        }

        public Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public string XMLURL
        {
            get { return this.UpdateURL + "Components/update.LiveSplit.HMA.xml"; }
        }
    }
}

