using System;

namespace LogXtreme.WinDsk.TestDataGrid.Models {
    public class ItemModel {

        public readonly int Id;

        public ItemModel(
            int id,
            string name, 
            string code, 
            int quantity, 
            DateTime dt,
            Uri hypelink) {

            this.Id = id;

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
