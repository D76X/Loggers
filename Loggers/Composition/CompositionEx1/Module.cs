using System.ComponentModel.Composition;

namespace CompositionEx1 {

    [Export]
    internal class Module {

        public Module() {
            Title = "Customers";
        }

        public string Title { get; set; }
    }
}