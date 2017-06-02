using LogXtreme.WinDsk.Infrastructure.Menu;

namespace LogXtreme.WinDsk.Infrastructure.Services {
    public interface IMenuService {
        void Add(IMenuItem menuItem, IMenuItem parent);
    }
}
