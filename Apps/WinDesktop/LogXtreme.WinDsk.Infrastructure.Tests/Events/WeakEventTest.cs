﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reactive.Linq;

namespace LogXtreme.WinDsk.Infrastructure.Tests.Events {
    [TestClass]
    public class WeakEventTest {

        [TestMethod]
        public void ListenerIsGCedWhenThereAreNoRefsOtherThanWeakEvent() { Assert.Fail(); }
    }
}
