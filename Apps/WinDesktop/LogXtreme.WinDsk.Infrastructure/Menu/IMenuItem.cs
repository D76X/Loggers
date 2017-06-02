using System.Collections.Generic;

namespace LogXtreme.WinDsk.Infrastructure.Menu {
    public interface IMenuItem {

        string Header { get; }

        IMenuItem Parent { get; }

        IEnumerable<IMenuItem> Children { get; }
    }
}
