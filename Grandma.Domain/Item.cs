using System;

namespace Grandma.Domain
{
    public class Item
    {
        private int _quantity;

        public string Description { get; set; }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
        }

        public void ModifyQuantity(int modifier)
        {
            _quantity += modifier;
        }

        public void AssignQuantity(int quantity)
        {
            _quantity = quantity;
        }

        public string ReportDetails()
        {
            return string.Format("{0}: {1}", Description, Quantity);
        }

    }
}
