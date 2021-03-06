﻿using LogXtreme.Extensions;
using LogXtreme.WinDsk.TestDataGrid.Interfaces;
using System.Collections.Generic;

namespace LogXtreme.WinDsk.TestDataGrid.Models {

    public class Sample : ISampleModel {

        private IEnumerable<string> values;

        public Sample(IEnumerable<string> values) {
            this.values = values;
        }

        public IEnumerable<string> Values => this.values;

        public override string ToString() =>
            values.Stringify(StringExtentions.SingleSpace);
    }
}
