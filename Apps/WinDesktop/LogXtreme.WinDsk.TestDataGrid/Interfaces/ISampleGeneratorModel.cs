namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISampleGeneratorModel {        

        IGeneratorDescriptorModel Descriptor { get; }
        ISampleModel GenerateSample(ISampleDescriptorModel sampleDescriptor);
    }
}
