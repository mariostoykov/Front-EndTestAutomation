using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SwagLabsPOM.Pages;

namespace SwagLabsPOM.Tests
{
    [TestFixture]
    public class CheckoutPageTests : BaseTest
    {
        [SetUp]
        public void LoginAndAddItemToCart()
        {
            Login("standard_user", "secret_sauce");
            var inventoryPage = new InventoryPage(driver);
            inventoryPage.AddToCartByIndex(0); // Add the first item to the cart
            inventoryPage.ClickCartLink(); // Navigate to the cart page
            var cartPage = new CartPage(driver);
            cartPage.ClickCheckOut(); // Proceed to the checkout page
        }

        [Test]
        public void TestCheckoutPageLoaded()
        {
            var checkoutPage = new CheckoutPage(driver);
            Assert.That(checkoutPage.IsPageLoaded(), Is.True, "The checkout page did not load correctly.");
        }

        [Test]
        public void TestContinueToNextStep()
        {
            var checkoutPage = new CheckoutPage(driver);
            checkoutPage.EnterFirstName("Ivan");
            checkoutPage.EnterLastName("Ivanov");
            checkoutPage.EnterPostalCode("0123");
            checkoutPage.ClickContinue();

            // Assert that the user is redirected to the next step in the checkout process
            Assert.That(driver.Url.Contains("checkout-step-two.html"), Is.True, "The user was not redirected to the next step in the checkout process.");
        }

        [Test]
        public void TestContinueOrder()
        {
            var checkoutPage = new CheckoutPage(driver);
            checkoutPage.EnterFirstName("Ivan");
            checkoutPage.EnterLastName("Ivanov");
            checkoutPage.EnterPostalCode("0123");
            checkoutPage.ClickContinue();

            // Click the "Finish" button
            checkoutPage.ClickFinish();

            // Assert that the user is redirected to the checkout complete page
            Assert.That(driver.Url.Contains("checkout-complete.html"), Is.True, "The user was not redirected to the checkout complete page.");

            // Assert that the order completion message is displayed
            Assert.That(checkoutPage.IsCheckoutComplete(), Is.True, "The order completion message was not displayed.");
        }
    }
}
