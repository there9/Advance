using Prism.Modularity;
using Prism.Regions;
using System;

namespace TimeTable.Modules.Menu
{
    public class MenuModule : IModule
    {
        IRegionManager _regionManager;

        public MenuModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("MenuContents", typeof(TimeTable.Modules.Menu.Views.Menu));
        }
    }
}