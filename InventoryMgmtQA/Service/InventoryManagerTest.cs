using InventoryMgmt.Model;
using InventoryMgmt.Service;
using InventoryMgmt.Interface;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.Utilities;

// guide: https://www.aligrant.com/web/blog/2020-07-20_capturing_console_outputs_with_microsoft_test_framework

namespace InventoryMgmtQA.Service
{
    [TestClass]
    public class InventoryManagerTest
    {
        private IInventoryManager _inventoryManager = new InventoryManager();

        [TestMethod]
        public void TestAddProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    1.23M
                );

                // console output should contain 'success'
                Console.WriteLine(sw.ToString());
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

     
        [TestMethod]
        public void TestAddProductPriceNegative()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    -1.0M
                );
                Assert.IsFalse(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        public void TestGetTotalValue()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    2.56M
                );
                _inventoryManager.GetTotalValue();
                Debug.WriteLine(sw.ToString());
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }
        [TestMethod]
        //Work by Patrick Feniza
        public void TestAddProductWithBlankProductName()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "",
                    1,
                    1.23M
                );
                //Blank/null vaue in product name is invaliid
                //the assertion is false. The test will pass if sw does not contain 'success'
                Assert.IsFalse(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        //Work by Patrick Feniza
        public void TestAddProductWithZeroPrice()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    1,
                    0.0M
                );
                Debug.WriteLine(sw.ToString());
                // adding 0 value in price is valid
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }
        [TestMethod]
        //Work by Patrick Feniza
        public void TestAddProductWithZeroQuantity()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    0,
                    1.0M
                );
                Debug.WriteLine(sw.ToString());
                // adding 0 value in qty is valid
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }
        [TestMethod]
        //Work by Patrick Feniza
        public void TestAddProductWithNegativeQty()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "TestProduct",
                    -1,
                    1.0M
                );
                Debug.WriteLine(sw.ToString());
                // adding negative value in qty is invalid
                //the assertion is false. The test will pass if sw does not contain 'success'
                Assert.IsFalse(sw.ToString().Contains("success"));
            }
        }
        [TestMethod]
        //Work by Patrick Feniza
        public void TestAddProductWithSpecialChar()
        {
            using (StringWriter sw = new StringWriter())
            {
                // capture console output
                Console.SetOut(sw);

                // create a new product with valid attribute values
                _inventoryManager.AddNewProduct(
                    "!@#$%^&*()_+[]{}|",
                    1,
                    1.0M
                );
                Debug.WriteLine(sw.ToString());
                // special characters are valid
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }
        [TestMethod]
        //Work by Patrick Feniza
        public void TestAddingRemovingProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  1,
                  1.23M
              );
                // get id to remove the product
                _inventoryManager.RemoveProduct(
                    1
                );
                Debug.WriteLine(sw.ToString());
                // successfully saved and removed product
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }
        [TestMethod]
        public void TestAddingRemovingProductWithInvalidId()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  1,
                  1.23M
              );
                // get id to remove the product
                _inventoryManager.RemoveProduct(
                    2
                );
                Debug.WriteLine(sw.ToString());
                // removing product, but id 2 does not exist
                //the assertion is true. The test will pass if sw does contain 'Product not found, please try again'
                Assert.IsTrue(sw.ToString().Contains("Product not found, please try again"));

                //successsfully add product
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        //Work by Patrick Feniza
        public void TestUpdateProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  1,
                  1.23M
              );
                // get id to remove the product
                _inventoryManager.UpdateProduct(
                    1,
                    5
                );
                Debug.WriteLine(sw.ToString());
                
                //successfully update a product
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }


        [TestMethod]
        //Work by Patrick Feniza
        public void TestGetListOfProducts()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  3,
                  6.23M
              );

                _inventoryManager.AddNewProduct(
                 "TestProduct1",
                 5,
                 1.23M
             );
                // get id to remove the product
                _inventoryManager.ListProducts(
                  
                );
                Debug.WriteLine(sw.ToString());

                //Successfully fetch products
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        //Work by Patrick Feniza
        public void TestGetListOfProductsWithUpdate()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  3,
                  6.23M
              );

                _inventoryManager.UpdateProduct(
                 1,
                 5
             );
                // get id to remove the product
                _inventoryManager.ListProducts(

                );
                Debug.WriteLine(sw.ToString());

                //Successfully add, update, and ready products
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        //Work by Patrick Feniza
        public void TestGetListOfProductsWithUpdateAndRemoveProduct()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  3,
                  6.23M
              );
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  4,
                  7.23M
              );

                _inventoryManager.UpdateProduct(
                 1,
                 5
             );
                _inventoryManager.RemoveProduct(
                 2                
             );
                // get id to remove the product
                _inventoryManager.ListProducts(

                );
                Debug.WriteLine(sw.ToString());

                //Successfully add, update, delete, read a product
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        //Work by Patrick Feniza
        public void TestGetTotalValueAndListOfProducts()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  3,
                  6.23M
              );
                _inventoryManager.AddNewProduct(
                  "TestProduct",
                  4,
                  7.23M
              );

                _inventoryManager.AddNewProduct(
                 "TestProduct",
                 4,
                 20.23M
             );
                _inventoryManager.AddNewProduct(
               "TestProduct",
               8,
               50.23M
           );
                // get id to remove the product
                _inventoryManager.ListProducts(

                );
                _inventoryManager.GetTotalValue(

                );
                Debug.WriteLine(sw.ToString());

                //Successfully fetch sum of all products price
                Assert.IsTrue(sw.ToString().Contains("success"));
            }
        }

        [TestMethod]
        //Work by Patrick Feniza
        public void TestFetchWithoutProducts()
        {
            using (StringWriter sw = new StringWriter())
            {
                //if we not add product, no id will be generated.
                Console.SetOut(sw);
                
                // get id to remove the product
                _inventoryManager.ListProducts(

                );
              
                Debug.WriteLine(sw.ToString());

                //invalid, since there are no products added
                //sw does not contain 'success' which pass the test
                Assert.IsFalse(sw.ToString().Contains("success"));
            }
        }
    }
}