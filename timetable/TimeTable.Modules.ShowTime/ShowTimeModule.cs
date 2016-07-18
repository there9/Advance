using Prism.Modularity;
using Prism.Regions;
using System;

namespace TimeTable.Modules.ShowTime
{
    public class ShowTimeModule : IModule
    {
        IRegionManager _regionManager;

        public ShowTimeModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ShowTimeTable",typeof(TimeTable.Modules.ShowTime.Views.ShowTimetable));
        }
    }
}