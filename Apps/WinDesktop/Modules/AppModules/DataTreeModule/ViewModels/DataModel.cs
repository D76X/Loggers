using System.Collections.Generic;

namespace LogXtreme.WinDsk.DataTreeModule.ViewModels {
    public class DataModel {

        readonly List<DataModel> _children = new List<DataModel>();

        public DataModel() { }

        public DataModel(List<DataModel> children) {
            this._children = children;
        }

        public IList<DataModel> Children {
            get { return _children; }
        }

        public string Name { get; set; }
    }
}
