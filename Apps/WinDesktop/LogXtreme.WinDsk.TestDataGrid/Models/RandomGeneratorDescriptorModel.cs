using LogXtreme.Infrastructure.ContractValidators;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    /// <summary>
    /// Descriptor for the random generator model. 
    /// </summary>
    public class RandomGeneratorDescriptorModel : IGeneratorDescriptorModel {

        private readonly IEnumerable<string> sourceNames;
        private readonly int length;

        public RandomGeneratorDescriptorModel(IEnumerable<string> sourceNames) {

            //TODO should reference the interface and use injection, perhaps attributes?
            var validator = InvariantValidator.CreateValidator();
            validator.VerifyNonNull<ArgumentNullException>(sourceNames);

            this.sourceNames = sourceNames;
            this.length = this.sourceNames.Count();
        }

        public IEnumerable<string> SourceNames => this.sourceNames;

        public int Length => this.length;
    }
}
