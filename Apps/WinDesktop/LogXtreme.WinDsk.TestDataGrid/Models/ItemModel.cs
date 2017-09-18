﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogXtreme.WinDsk.TestDataGrid.Models {
    public class ItemModel {

        public ItemModel(
            string name, 
            string code, 
            int quantity, 
            DateTime dt,
            Uri hypelink) {

            this.Name = name;
            this.Code = code;
            this.Quantity = quantity;
            this.Time = dt;
            this.Web = hypelink;
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Quantity { get; set; }

        public DateTime Time { get; set; }

        public Uri Web { get; set; }
    }
}
