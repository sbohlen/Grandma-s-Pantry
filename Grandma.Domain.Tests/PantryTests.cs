using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
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
                Pantry pantry = new Pantry();

                Item theItem = new Item() { Description = "sugar" };

                Item notTheItem = new Item() { Description = "flour" };

                pantry.AddItem(theItem);
                pantry.AddItem(notTheItem);

                Assert.IsFalse(pantry.Items.Any(i => i.Description == "brown sugar"), "cannot enforce needed pre-test condition re: collection members!");

                Assert.IsTrue(pantry.TryUpdateItemDescription("sugar", "brown sugar"));

                Assert.IsTrue(pantry.Items.Any(i => i.Description == "brown sugar"));


            }

            [Test]
            public void Can_Prevent_Name_Collisions()
            {
                Pantry pantry = new Pantry();

                Item sugar = new Item() { Description = "sugar" };
                Item brownSugar = new Item() { Description = "brown sugar" };

                pantry.AddItem(sugar);
                pantry.AddItem(brownSugar);

                Assert.IsFalse(pantry.TryUpdateItemDescription("brown sugar", "sugar"));
            }
        }


        [TestFixture]
        public class When_Changing_The_Description_of_an_Item_That_is_Not_In_the_Pantry
        {
            [Test]
            public void Can_Indicate_Invalid_Action()
            {
                Pantry pantry = new Pantry();

                Item notTheItem = new Item() { Description = "flour" };

                pantry.AddItem(notTheItem);

                Assert.IsFalse(pantry.Items.Any(i => i.Description == "sugar"), "cannot enforce needed pre-test condition re: collection members!");
                Assert.IsFalse(pantry.Items.Any(i => i.Description == "brown sugar"), "cannot enforce needed pre-test condition re: collection members!");

                Assert.IsFalse(pantry.TryUpdateItemDescription("sugar", "brown sugar"));
            }
        }

        [TestFixture]
        public class When_Pantry_Has_Items
        {
            [Test]
            public void Can_Report_Items_as_Expected()
            {
                Pantry pantry = new Pantry();

                Item sugar = new Item() { Description = "sugar" };
                sugar.AssignQuantity(3);

                Item flour = new Item() { Description = "flour" };
                flour.AssignQuantity(1);

                pantry.AddItem(sugar);
                pantry.AddItem(flour);

                string itemsReport = pantry.ReportOnItems();

                Assert.AreEqual("sugar: 3\r\nflour: 1\r\n", itemsReport);

            }
        }


        [TestFixture]
        public class When_Deleting_An_Item
        {
            [Test]
            public void Item_Is_Removed_from_the_Pantry()
            {
                Pantry pantry = new Pantry();

                Item sugar = new Item() { Description = "sugar" };
                Item flour = new Item() { Description = "flour" };

                pantry.AddItem(sugar);
                pantry.AddItem(flour);

                Assert.Contains(pantry.Items, sugar, "PRECONDITION ASSERT FAILURE: pantry.Items doesn't contain expected item!");
                Assert.Contains(pantry.Items, flour, "PRECONDITION ASSERT FAILURE: pantry.Items doesn't contain expected item!");

                pantry.RemoveItem("sugar");

                Assert.DoesNotContain(pantry.Items, sugar);
            }
        }


        [TestFixture]
        public class When_Pantry_Has_No_Items
        {
            [Test]
            public void Can_Report_Pantry_Is_Empty()
            {
                Pantry pantry = new Pantry();

                Assert.IsEmpty(pantry.Items, "PRECONDITION ASSERT FAILURE: pantry.Items is NOT empty as expected!" );

                Assert.AreEqual("The Pantry is bare!".ToUpper(), pantry.ReportOnItems().ToUpper());
            }
        }


        [TestFixture]
        public class When_Adding_An_Item_Thats_Already_In_The_Pantry
        {
            [Test]
            public void Can_Increase_Quantity_of_Existing_Item()
            {
                Pantry pantry = new Pantry();

                Item sugar = new Item() { Description = "sugar" };
                sugar.AssignQuantity(3);

                //ensure there aren't already any 'sugar' items in the pantry
                Assert.Throws<Exception>(() => pantry.Items.Where(i => i.Description == "sugar").Single());

                pantry.AddItem(sugar);

                Item moreSugar = new Item() { Description = "sugar" };
                moreSugar.AssignQuantity(4);

                pantry.AddItem(moreSugar);

                var pantrySugar = pantry.Items.Where(i => i.Description == "sugar").Single();

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

                Assert.IsEmpty(pantry.Items);

                Item item = new Item() { Description = "something" };

                pantry.AddItem(item);

                Assert.Contains(pantry.Items, item);

            }

            [Test]
            public void Item_Is_Not_Added_If_Description_Is_Not_Provided()
            {
                Pantry pantry = new Pantry();

                Assert.IsEmpty(pantry.Items);

                Item item = new Item();

                pantry.AddItem(item);

                Assert.DoesNotContain(pantry.Items, item);

            }

        }
    }
}