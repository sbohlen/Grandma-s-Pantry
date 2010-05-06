using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Grandma.Domain;

namespace Grandma.Domain.Tests
{

    public class PantryTests
    {

        [TestFixture]
        public class When_Changing_The_Description_of_an_Item_That_is_In_the_Pantry
        {
            [Test]
            public void Can_Change_Item_Description()
            {
                const string ITEM_TO_RENAME = "sugar";
                const string ITEM_NOT_TO_RENAME = "flour";
                const string NEW_NAME = "brown sugar";

                Pantry pantry = new Pantry();
                
                Item theItem = new Item() { Description = ITEM_TO_RENAME };
                Item notTheItem = new Item() { Description = ITEM_NOT_TO_RENAME };

                pantry.AddItem(theItem);
                pantry.AddItem(notTheItem);
                
                Assert.IsFalse(pantry.Items.Any(i => i.Description == NEW_NAME), "PRECONDITION ASSERT FAILURE: Items collection should not contain item with the new item name!");

                Assert.IsTrue(pantry.TryUpdateItemDescription(ITEM_TO_RENAME, NEW_NAME));
                Assert.IsTrue(pantry.Items.Any(i => i.Description == NEW_NAME));


            }

            [Test]
            public void Can_Prevent_Name_Collisions()
            {
                const string INITIAL_DESCRIPTION = "sugar";
                const string NEW_DESCRIPTION = "brown sugar";
                
                Pantry pantry = new Pantry();
                
                Item sugar = new Item() { Description = INITIAL_DESCRIPTION };
                
                Item brownSugar = new Item() { Description = NEW_DESCRIPTION };

                pantry.AddItem(sugar);
                pantry.AddItem(brownSugar);

                Assert.IsFalse(pantry.TryUpdateItemDescription(NEW_DESCRIPTION, INITIAL_DESCRIPTION));
            }
        }


        [TestFixture]
        public class When_Changing_The_Description_of_an_Item_That_is_Not_In_the_Pantry
        {
            [Test]
            public void Can_Indicate_Invalid_Action()
            {
                const string NOT_THE_DESCRIPTION = "flour";
                const string INITIAL_DESCRIPTION = "sugar";
                const string NEW_DERSCRIPTION = "brown sugar";

                Pantry pantry = new Pantry();

                Item notTheItem = new Item() { Description = NOT_THE_DESCRIPTION };

                pantry.AddItem(notTheItem);

                Assert.IsFalse(pantry.Items.Any(i => i.Description == INITIAL_DESCRIPTION), "PRECONDITION ASSERT FAILURE: cannot enforce needed pre-test condition re: collection members!");
                Assert.IsFalse(pantry.Items.Any(i => i.Description == NEW_DERSCRIPTION), "PRECONDITION ASSERT FAILURE: cannot enforce needed pre-test condition re: collection members!");

                Assert.IsFalse(pantry.TryUpdateItemDescription(INITIAL_DESCRIPTION, NEW_DERSCRIPTION));
            }
        }

        [TestFixture]
        public class When_Pantry_Has_Items
        {
            [Test]
            public void Can_Report_Items_as_Expected()
            {
                const string ITEM1_DESCRIPTION = "sugar";
                const int ITEM1_QUANTITY = 3;

                const string ITEM2_DESCRIPTION = "flour";
                const int ITEM2_QUANTITY = 1;

                Pantry pantry = new Pantry();
                
                Item sugar = new Item() { Description = ITEM1_DESCRIPTION };
                sugar.AssignQuantity(ITEM1_QUANTITY);
                
                Item flour = new Item() { Description = ITEM2_DESCRIPTION };
                flour.AssignQuantity(ITEM2_QUANTITY);

                pantry.AddItem(sugar);
                pantry.AddItem(flour);

                string itemsReport = pantry.ReportOnItems();

                Assert.AreEqual(string.Format("{0}: {1}\r\n{2}: {3}\r\n", ITEM1_DESCRIPTION, ITEM1_QUANTITY, ITEM2_DESCRIPTION, ITEM2_QUANTITY), itemsReport);

            }
        }


        [TestFixture]
        public class When_Deleting_An_Item
        {
            [Test]
            public void Item_Is_Removed_from_the_Pantry()
            {
                const string DESCRIPTION_TO_REMOVE = "sugar";
                const string DESCRIPTION_NOT_TO_REMOVE = "flour";

                Pantry pantry = new Pantry();
                
                Item sugar = new Item() { Description = DESCRIPTION_TO_REMOVE };
                Item flour = new Item() { Description = DESCRIPTION_NOT_TO_REMOVE };

                pantry.AddItem(sugar);
                pantry.AddItem(flour);

                CollectionAssert.Contains(pantry.Items, sugar, "PRECONDITION ASSERT FAILURE: pantry.Items doesn't contain expected item!");
                CollectionAssert.Contains(pantry.Items, flour, "PRECONDITION ASSERT FAILURE: pantry.Items doesn't contain expected item!");

                pantry.RemoveItem(DESCRIPTION_TO_REMOVE);

                CollectionAssert.DoesNotContain(pantry.Items, sugar);
            }
        }


        [TestFixture]
        public class When_Pantry_Has_No_Items
        {
            [Test]
            public void Can_Report_Pantry_Is_Empty()
            {
                const string EMPTY_PANTRY_MESSAGE = "The Pantry is bare!";

                Pantry pantry = new Pantry();

                CollectionAssert.IsEmpty(pantry.Items, "PRECONDITION ASSERT FAILURE: pantry.Items is NOT empty as expected!");
                
                Assert.AreEqual(EMPTY_PANTRY_MESSAGE.ToUpper(), pantry.ReportOnItems().ToUpper());
            }
        }


        [TestFixture]
        public class When_Adding_An_Item_Thats_Already_In_The_Pantry
        {
            [Test]
            public void Can_Increase_Quantity_of_Existing_Item()
            {
                Pantry pantry = new Pantry();

                const string DESCRIPTION = "sugar";

                Item sugar = new Item() { Description = DESCRIPTION };
                sugar.AssignQuantity(3);

                //ensure there aren't already any 'sugar' items in the pantry
                Assert.Throws<InvalidOperationException>(() => pantry.Items.Where(i => i.Description == DESCRIPTION).Single());

                pantry.AddItem(sugar);

                Item moreSugar = new Item() { Description = DESCRIPTION };
                moreSugar.AssignQuantity(4);

                pantry.AddItem(moreSugar);

                var pantrySugar = pantry.Items.Where(i => i.Description == DESCRIPTION).Single();

                Assert.AreEqual(7, pantrySugar.Quantity);

            }
        }


        [TestFixture]
        public class When_Adding_A_New_Item
        {
            [Test]
            public void Item_Is_In_Pantry()
            {
                Pantry pantry = new Pantry();

                const string DESCRIPTION = "description";
                
                Item item = new Item() { Description = DESCRIPTION };

                CollectionAssert.DoesNotContain(pantry.Items, item, "PRECONDITION FAILURE: item is already in the collection!");

                pantry.AddItem(item);

                CollectionAssert.Contains(pantry.Items, item);

            }

            [Test]
            public void Item_Is_Not_Added_If_Description_Is_Not_Provided()
            {
                Pantry pantry = new Pantry();

                Item item = new Item();

                CollectionAssert.DoesNotContain(pantry.Items, item, "PRECONDITION FAILURE: item is already in the collection!");

                pantry.AddItem(item);

                CollectionAssert.DoesNotContain(pantry.Items, item);

            }

        }
    }
}