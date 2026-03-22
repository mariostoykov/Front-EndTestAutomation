using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FillTheForm
{
    public class FillTheForm
    {
        private IWebDriver driver;
        private string baseUrl = "C:\\Users\\TOSHIBA\\Downloads\\QA path\\Front-End Test Automation\\Front-End Technologies Automation\\03.Selenium-WebDriver\\SimpleForm\\Locators.html";

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1200");
            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl(baseUrl);

        }

        [Test]
        public void TestFormSubmission()
        {
            // Asserts the Contact Form title
            var formTitle = driver.FindElement(By.TagName("h2")).Text;
            Assert.That(formTitle, Is.EqualTo("Contact Form"));

            // Selects the male radio button and asserts its selection
            var maleRadioButton = driver.FindElement(By.XPath("//input[@value='m']"));
            maleRadioButton.Click();
            Assert.That(maleRadioButton.Selected, Is.True);

            // Write "Butch" as the first name
            var firstNameField = driver.FindElement(By.Id("fname"));
            firstNameField.Clear();
            firstNameField.SendKeys("Butch");
            Assert.That(firstNameField.GetAttribute("value"), Is.EqualTo("Butch"));

            // Write "Coolidge" as the last name
            var lastNameField = driver.FindElement(By.Id("lname"));
            lastNameField.Clear();
            lastNameField.SendKeys("Coolidge");
            Assert.That(lastNameField.GetAttribute("value"), Is.EqualTo("Coolidge"));

            // Assert the "Additional Information" section
            var additionalInHeader = driver.FindElement(By.TagName("h3")).Text;
            Assert.That(additionalInHeader, Is.EqualTo("Additional Information"));

            // Add "0888999777" as the phone number
            var phoneNumberField = driver.FindElement(By.XPath("//div[@class='additional-info']//input[@type='text']"));
            phoneNumberField.Clear();
            phoneNumberField.SendKeys("0888999777");
            Assert.That(phoneNumberField.GetAttribute("value"), Is.EqualTo("0888999777"));

            // Check the newsletter checkbox
            var newsletterCheckBox = driver.FindElement(By.Name("newsletter"));
            newsletterCheckBox.Click();
            Assert.That(newsletterCheckBox. Selected, Is.True);

            // Click the submit button
            var submitButton = driver.FindElement(By.ClassName("button"));
            submitButton.Click();

            // Assert the "Thank You!" message on the next page
            driver.Navigate().GoToUrl("C:\\Users\\TOSHIBA\\Downloads\\QA path\\Front-End Test Automation\\Front-End Technologies Automation\\03.Selenium-WebDriver\\SimpleForm\\Thankyou.html");
            var thankYouMessage = driver.FindElement(By.TagName("h1")).Text;
            Assert.That(thankYouMessage, Is.EqualTo("Thank You!"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}