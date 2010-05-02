using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Grandma.Domain
{
    public class Pantry
    {
        IList<Item> _items = new List<Item>();

        public IEnumerable<Item> Items
        {
            get
            {
                return _items.AsEnumerable();
            }
        }

        public void AddItem(Item item)
        {
            if (string.IsNullOrEmpty(item.Description))
                return;

            if (_items.Any(i => i.Description.ToLower() == item.Description.ToLower()))
                _items.Where(i => i.Description.ToLower() == item.Description.ToLower()).Single().ModifyQuantity(item.Quantity);
            else
                _items.Add(item);
        }

        public void RemoveItem(string description)
        {
            var items = _items.Where(i => i.Description.ToLower() == description.ToLower()).ToList();

            items.ForEach(i => { _items.Remove(i); });
        }

        public string ReportOnItems()
        {
            if (_items.Count == 0)
                return "The Pantry is Bare!";

            StringBuilder report = new StringBuilder();

            foreach (Item item in _items)
            {
                report.AppendLine(item.ReportDetails());
            }

            return report.ToString();
        }

        public bool TryUpdateItemDescription(string currentDescription, string newDescription)
        {
            //if any item exists with the new description we cannot proceed so bail out!
            if (_items.Any(i => i.Description.ToLower() == newDescription.ToLower()))
                return false;

            try
            {
                Item itemToUpdate = _items.Where(i => i.Description.ToLower() == currentDescription.ToLower()).Single();
                itemToUpdate.Description = newDescription;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
