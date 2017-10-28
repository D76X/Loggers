using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class RandomGeneratorDescriptor : IGeneratorDescriptor {

        private readonly IEnumerable<string> sourceNames;
        private readonly int length;

        public RandomGeneratorDescriptor(IEnumerable<string> sourceNames) {
            this.sourceNames = sourceNames;
            this.length = this.sourceNames.Count();
        }

        public IEnumerable<string> SourceNames => this.sourceNames;

        public int Length => this.length;
    }
}
