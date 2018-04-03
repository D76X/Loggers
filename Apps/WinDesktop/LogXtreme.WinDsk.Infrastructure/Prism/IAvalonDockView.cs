using LogXtreme.Ifrastructure.Enums;

namespace LogXtreme.WinDsk.Infrastructure.Prism {
    public interface IAvalonDockView {

        AvalonDockViewTypeEnum AvalonDockViewType { get; }
        AvalonDockViewAnchorEnum AvalonDockViewAnchor { get; }
    }
}
