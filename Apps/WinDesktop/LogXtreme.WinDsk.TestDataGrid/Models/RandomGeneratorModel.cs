using LogXtreme.Extensions;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class RandomGeneratorModel : ISampleGeneratorModel {

        private readonly Random generator;
        private readonly IGeneratorDescriptorModel generatorDescriptor;

        public RandomGeneratorModel(IGeneratorDescriptorModel generatorDescriptor) {

            this.generatorDescriptor = generatorDescriptor;
            this.generator = new Random();
        }

        public IGeneratorDescriptorModel Descriptor => this.generatorDescriptor;

        public ISampleModel GenerateSample(ISampleDescriptorModel sampleDescriptor) {
            var nosamples = sampleDescriptor.Length;
            return new Sample(this.generator.GetIntegers(nosamples, 0 , 255).Stringify());
        }
    }
}
