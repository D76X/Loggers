using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class SampleDescriptor : ISampleDescriptor {

        private readonly IEnumerable<string> description;
        private readonly int lenght;

        public SampleDescriptor(IEnumerable<string> sampleDescription) {

            this.description = sampleDescription;
            this.lenght = this.description.Count();
        }

        public IEnumerable<string> ValueNames => this.description;

        public int Length => this.lenght;
    }
}
