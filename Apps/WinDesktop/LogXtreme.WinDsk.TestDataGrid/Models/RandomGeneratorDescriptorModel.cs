using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class RandomGeneratorDescriptorModel : IGeneratorDescriptorModel {

        private readonly IEnumerable<string> sourceNames;
        private readonly int length;

        public RandomGeneratorDescriptorModel(IEnumerable<string> sourceNames) {
            this.sourceNames = sourceNames;
            this.length = this.sourceNames.Count();
        }

        public IEnumerable<string> SourceNames => this.sourceNames;

        public int Length => this.length;
    }
}
