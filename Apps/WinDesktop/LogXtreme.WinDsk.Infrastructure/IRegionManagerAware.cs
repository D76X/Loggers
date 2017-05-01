using Prism.Regions;

namespace LogXtreme.WinDsk.Infrastructure {

    /// <summary>
    /// This interface may be supported by any descendant that needs to 
    /// hold a reference to an instance of IRegionManager.
    /// </summary>
    public interface IRegionManagerAware {
        IRegionManager RegionManager { get; set; }
    }
}
