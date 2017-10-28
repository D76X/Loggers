using LogXtreme.Extensions;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class RandomGenerator : ISampleGenerator {

        private readonly Random generator;
        private readonly IGeneratorDescriptor generatorDescriptor;

        public RandomGenerator(IGeneratorDescriptor generatorDescriptor) {

            this.generatorDescriptor = generatorDescriptor;
            this.generator = new Random();
        }

        public IGeneratorDescriptor Descriptor => this.generatorDescriptor;

        public ISample GenerateSample(ISampleDescriptor sampleDescriptor) {
            var nosamples = sampleDescriptor.Length;
            return new Sample(this.generator.GetIntegers(nosamples, 0 , 255).Stringify());
        }
    }
}
