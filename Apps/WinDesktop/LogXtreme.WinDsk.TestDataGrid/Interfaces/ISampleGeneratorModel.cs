namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    /// <summary>
    /// Describes a generator of samples.
    /// </summary>
    public interface ISampleGeneratorModel {        

        IGeneratorDescriptorModel Descriptor { get; }
        ISampleModel GenerateSample(ISampleDescriptorModel sampleDescriptor);
    }
}
