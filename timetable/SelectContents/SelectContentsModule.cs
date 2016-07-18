using Prism.Modularity;
using Prism.Regions;
using System;

namespace timetable.Modules.SelectGrid
{
    public class SelectContentsModule : IModule
    {
        IRegionManager _regionManager;

        public SelectContentsModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("SelectContents",typeof(timetable.Modules.SelectGrid.Views.SelectContents));
        }
    }
}