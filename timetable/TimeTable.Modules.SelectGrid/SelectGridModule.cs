using Prism.Modularity;
using Prism.Regions;
using System;

namespace TimeTable.Modules.SelectGrid
{
    public class SelectGridModule : IModule
    {
        IRegionManager _regionManager;

        public SelectGridModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("SelectContents", typeof(TimeTable.Modules.SelectGrid.Views.SelectContents));
        }
    }
}