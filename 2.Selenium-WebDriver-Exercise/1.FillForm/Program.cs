using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FillForm
{
    public class FillForm
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [Test]
        public void Test_RegisterUser()
        {
            // Click on My Account Link
            driver.FindElement(By.LinkText("My Account")).Click();

            // Click on Continue Button
            driver.FindElement(By.LinkText("Continue")).Click();

            // Fill in the form
            driver.FindElement(By.CssSelector("input[type=radio][value='m']")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys("Mario");
            driver.FindElement(By.Name("lastname")).SendKeys("Stoykov");
            driver.FindElement(By.Id("dob")).SendKeys("05/21/1970");

            // Generate a unique email address
            Random rnd = new Random();
            // Generate a random number between 1000 and 9999
            int num = rnd.Next(1000, 9999);
            String email = "mario.car" + num.ToString() + "@example.com";

            driver.FindElement(By.Name("email_address")).SendKeys(email);
            driver.FindElement(By.Name("company")).SendKeys("SAP");
            driver.FindElement(By.Name("street_address")).SendKeys("Vasil Levski");
            driver.FindElement(By.Name("suburb")).SendKeys("Mladost");
            driver.FindElement(By.Name("postcode")).SendKeys("4000");
            driver.FindElement(By.Name("city")).SendKeys("Sofia");
            driver.FindElement(By.Name("state")).SendKeys("Sofia");

            // Select country from dropdown
            new SelectElement(driver.FindElement(By.Name("country"))).SelectByText("Bulgaria");

            driver.FindElement(By.Name("telephone")).SendKeys("0775566990");
            driver.FindElement(By.Name("newsletter")).Click();

            driver.FindElement(By.Name("password")).SendKeys("mario_123");
            driver.FindElement(By.Name("confirmation")).SendKeys("mario_123");

            // Submit the form
            driver.FindElement(By.Id("tdb4")).Submit();

            // Assert account creation success
            Assert.That(driver.PageSource.Contains("Your Account Has Been Created!"), Is.True, "Account creation failed.");

            // Click on Log Off link
            driver.FindElement(By.LinkText("Log Off")).Click();

            // Click on Continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User Account Created with email: " + email);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}