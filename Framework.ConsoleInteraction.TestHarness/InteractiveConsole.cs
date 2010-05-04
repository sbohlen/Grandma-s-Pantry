using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grandma.Domain;

/*
 * ***************************************************************************** 
 * EXTEND THE INTERACTIVE MENU IN THIS CLASS; DO NOT WRITE TESTS FOR THIS CLASS 
 * ***************************************************************************** 
 */

namespace Framework.ConsoleInteraction.TestHarness
{
    public class InteractiveConsole
    {
        private Pantry _pantry = new Pantry();

        public bool Run()
        {
            while (true)
            {
                MainMenu();
            }
        }

        private void AddItem(string response)
        {
            var description = InputRequester.GetStringInput("Enter Item Description");
            var quantity = InputRequester.GetIntegerInput("Enter Item quantity");

            Item item = new Item() { Description = description };
            item.AssignQuantity(quantity);
            _pantry.AddItem(item);
        }

        private void AddItems(string response)
        {
            string description;
            
            while (!string.IsNullOrEmpty(description = InputRequester.GetStringInput("Enter Item Description (enter blank when finished)")))
            {
                var quantity = InputRequester.GetIntegerInput("Enter Item quantity");

                Item item = new Item() { Description = description };
                item.AssignQuantity(quantity);
                _pantry.AddItem(item);
            }
        }

        private void EditItem(string itemDescription)
        {
            string newDescription = InputRequester.GetStringInput("Enter the new description for this Item");

            bool result = _pantry.TryUpdateItemDescription(itemDescription, newDescription);

            if (!result)
                Console.WriteLine("Unable to update description from {0} to {1} for item.", itemDescription, newDescription);

        }

        private void EditItemMenu(string response)
        {
            InteractiveChooser chooser = new InteractiveChooser("Select an Item to Edit");

            foreach (Item item in _pantry.Items)
            {
                chooser.AddChoice(new Choice(item.Description, EditItem));
            }

            chooser.AskForChoice();

        }

        private void ItemsMenu(string response)
        {
            InteractiveChooser chooser = new InteractiveChooser("Select an operation to perform");
            chooser.AddChoice(new Choice("Add an Item", AddItem));
            chooser.AddChoice(new Choice("Add Items", AddItems));
            chooser.AddChoice(new Choice("Edit an Item", EditItemMenu));
            chooser.AddChoice(new Choice("List All Items in the Pantry", ShowItemsReport));

            chooser.AskForChoice();
        }

        private void MainMenu()
        {
            InteractiveChooser chooser = new InteractiveChooser("Select a category of things to manage");

            chooser.AddChoice(new Choice("Item SubMenu", ItemsMenu));
            chooser.AddChoice(new Choice("Recipe SubMenu", RecipesMenu));

            chooser.AskForChoice();
        }

        private void NullAction(string response)
        {
            Console.WriteLine("this is a placeholder for an actual action to be executed when wired up later!");
        }

        private void RecipesMenu(string response)
        {
            InteractiveChooser chooser = new InteractiveChooser("Select an action to perform");
            chooser.AddChoice(new Choice("Add a Recipe", NullAction));
            chooser.AddChoice(new Choice("Edit a Recipe", NullAction));

            chooser.AskForChoice();
        }

        private void ShowItemsReport(string response)
        {
            Console.WriteLine(_pantry.ReportOnItems());
        }

    }
}
