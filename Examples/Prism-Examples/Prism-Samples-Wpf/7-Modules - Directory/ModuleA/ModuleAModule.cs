using LogXtreme.WinDsk.Modules.TestModules.Views;
using Prism.Modularity;
using Prism.Regions;
using System;

namespace LogXtreme.WinDsk.Modules.TestModules
{
    public class ModuleAModule : IModule
    {
        IRegionManager _regionManager;

        public ModuleAModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(ViewA));
        }
    }
}