namespace LogXtreme.WinDsk.TestDataGrid.Interfaces {

    public interface ISampleGenerator {        

        IGeneratorDescriptor Descriptor { get; }
        ISample GenerateSample(ISampleDescriptor sampleDescriptor);
    }
}
