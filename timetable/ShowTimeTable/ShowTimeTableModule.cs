using Prism.Modularity;
using Prism.Regions;
using System;

namespace timetable.Modules.ShowTime
{
    public class ShowTimeTableModule : IModule
    {
        IRegionManager _regionManager;

        public ShowTimeTableModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ShowTimeTable",typeof(timetable.Modules.ShowTime.Views.ShowTimetable));
        }
    }
}