using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SemanticLogging.Tests {

    /// <summary>
    /// The EventSourceAnalyzer class checks your custom EventSource class using the following techniques.
    /// 
    /// 1- It attempts to enable a listener using the custom EventSource class to check for basic problems.
    /// 2- It attempts to retrieve the event schemas by generating a manifest from the custom EventSource class.
    /// 3- It attempts to invoke each event method in the custom EventSource class to check that it returns without error, supplies the correct event ID, and that all the payload parameters are passed in the correct order.
    /// Refs
    /// https://msdn.microsoft.com/en-us/library/dn440729(v=pandp.60).aspx#sec9
    /// https://dzimchuk.net/troubleshooting-slab-out-of-process-logging/
    /// </summary>
    [TestClass]
    public class EventSourceValidationTest {

        [TestMethod]
        public void ShouldValidateApplicationEventSource() {

            EventSourceAnalyzer.InspectAll(ApplicationEventSource.Logger);
        }
    }
}
