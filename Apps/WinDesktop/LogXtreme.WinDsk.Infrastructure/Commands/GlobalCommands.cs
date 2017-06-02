using Prism.Commands;

namespace LogXtreme.WinDsk.Infrastructure.Commands {

    /// <summary>
    /// http://prismlibrary.readthedocs.io/en/latest/WPF/09-Communication/
    /// </summary>
    public static class GlobalCommands {
        public static CompositeCommand MyCompositeCommand = new CompositeCommand();
    }
}
