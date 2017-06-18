using System;

namespace LogXtreme.WinDsk.Infrastructure.Tests {
    internal static class Utils {
        internal static void TriggerGC() {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
