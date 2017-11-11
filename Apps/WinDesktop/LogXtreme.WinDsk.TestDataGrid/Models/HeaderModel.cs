using LogXtreme.WinDsk.TestDataGrid.Interfaces;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class HeaderModel : IHeaderModel {

        private string name; 

        public HeaderModel(string name) {
            this.name = name;
        }

        public string Name {
            get => this.name;
            set => this.name = value;
        }
    }
}
