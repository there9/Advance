using Prism.Modularity;
using Prism.Regions;
using System;

namespace TimeTable.Modules.TotalGrid
{
    public class TotalGridModule : IModule
    {
        IRegionManager _regionManager;

        public TotalGridModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("TotalContents",typeof(TimeTable.Modules.TotalGrid.Views.TotalContents));
        }
    }
}