using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class SampleDescriptor : ISampleDescriptor {

        private readonly IEnumerable<string> description;
        private readonly int lenght;

        public SampleDescriptor(IGeneratorDescriptor generatorDescriptor) {

            this.description = new List<string>(generatorDescriptor.SourceNames);
            this.lenght = this.description.Count();
        }

        public IEnumerable<string> ValueNames => this.description;

        public int Length => this.lenght;
    }
}
