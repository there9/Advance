using Prism.Modularity;
using Prism.Regions;
using System;

namespace timetable.Modules.TotalGrid
{
    public class TotalContentsModule : IModule
    {
        IRegionManager _regionManager;

        public TotalContentsModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        
        public void Initialize()
        {
            this._regionManager.RegisterViewWithRegion("TotalContents", typeof(timetable.Modules.TotalGrid.Views.TotalContents));
        }
    }
}