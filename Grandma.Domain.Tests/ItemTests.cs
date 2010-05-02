using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Grandma.Domain;

namespace Grandma.Domain.Tests
{

    public class ItemTests
    {

        [TestFixture]
        public class When_Item_Is_Asked_About_Its_Details
        {
            [Test]
            public void Can_Report_in_Expected_Format()
            {
                Item item = new Item();
                item.AssignQuantity(3);
                item.Description = "sugar";

                Assert.AreEqual("sugar: 3", item.ReportDetails());

            }
        }

        [TestFixture]
        public class When_Adjusting_Quantities
        {
            [Test]
            [TestCase(2, 3, 5, "Increase Test FAILED")]
            [TestCase(12, -3, 9, "Decrease Test FAILED")]
            public void Quantity_Is_Incremented_and_Decremented_As_Expected(int initial, int modifier, int expected, string failureMessage)
            {
                Item item = new Item();

                item.AssignQuantity(initial);
                item.ModifyQuantity(modifier);

                Assert.AreEqual(expected, item.Quantity, failureMessage);

            }

        }

        [TestFixture]
        public class When_Creating_An_Item
        {
            [Test]
            public void Can_Assign_Quantity_To_Item()
            {
                Item item = new Item();
                item.AssignQuantity(3);

                Assert.AreEqual(3, item.Quantity);
            }

            [Test]
            public void Can_Describe_Item()
            {
                Item item = new Item();
                item.Description = "sugar";

                Assert.AreEqual("sugar", item.Description);
            }

        }
    }
}
