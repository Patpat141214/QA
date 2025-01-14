using InventoryMgmt.Model;
using System.ComponentModel.DataAnnotations;

// guide: https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022

namespace InventoryMgmtQA.Model
{
    [TestClass]
    public class ProductTest
    {
        
        [TestMethod]
        public void TestAddProduct()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 1,
                Price = 1.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must be valid since all attributes values validated correctly
            Assert.IsTrue(isProductValid);
        }

        [TestMethod]
        public void TestAddProductPriceNegative()
        {
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 1,
                Price = -1.0M // test for negative price
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must NOT be valid since the Price attribute has invalid value
            Assert.IsFalse(isProductValid);
        }

        [TestMethod]

        //Work by Patrick Feniza
        public void TestAddProductWithBlankProduct()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "",
                QuantityInStock = 1,
                Price = 1.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // this will pass the test since assertion is in false, name is required but we input blank/null.
            Assert.IsFalse(isProductValid);
        }

        [TestMethod]

        //Work by Patrick Feniza
        public void TestAddProductWithZeroQty()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 0,
                Price = 1.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // 0 qty is accepted
            Assert.IsTrue(isProductValid);
        }

        [TestMethod]

        //Work by Patrick Feniza
        public void TestAddProductWithZeroPrice()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 0,
                Price = 0.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // 0 price is accepted
            Assert.IsTrue(isProductValid);
        }
        [TestMethod]

        //Work by Patrick Feniza
        public void TestAddProductWithNegativeQty()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = -1,
                Price = 0.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // negative quantity is not acceptable
            Assert.IsFalse(isProductValid);
        }
        [TestMethod]

        //Work by Patrick Feniza
        public void TestAddProductWithSpecialChars()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "!@#$%^&*()_+[]{}|",
                QuantityInStock = 1,
                Price = 0.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // special characters are acceptable
            Assert.IsTrue(isProductValid);
        }

    }
}




