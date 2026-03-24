using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DynamicDemo
{
    [TestFixture]
    public class TestBoxAndInput
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.selenium.dev/selenium/web/dynamic.html";
        }

        [Test, Order(1)]
        public void AddBoxWithoutWaitsFail()
        {
            driver.FindElement(By.Id("adder")).Click();

            IWebElement newBox = driver.FindElement(By.Id("box0"));

            // Assert that the new box element is displayed
            //Assert.That(newBox.Displayed, Is.True);

            Assert.Throws<NoSuchElementException>(
                () => driver.FindElement(By.Id("box0"))
            );
        }

        [Test, Order(2)]
        public void RevealInputWithoutWaitsFail()
        {
            // Click the button to reveal the input
            driver.FindElement(By.Id("reveal")).Click();

            // Attempt to find the revealed element (which is hidden initially)
            IWebElement revealed = driver.FindElement(By.Id("revealed"));

            // Try to interact with the revealed element
            //revealed.SendKeys("Displayed");

            // Assert that the value was set correctly
            //Assert.That(revealed.GetAttribute("value"), Is.EqualTo("Displayed"));

            Assert.Throws<ElementNotInteractableException>(
                () => revealed.SendKeys("Displayed")
            );
        }

        [Test, Order(3)]
        public void AddBoxWithThreadSleep()
        {
            // Click the button to add a box
            driver.FindElement(By.Id("adder")).Click();

            // Wait for a fixed amount of time (2 seconds)
            Thread.Sleep(2000);

            // Attempt to find the newly added box element
            IWebElement newbox = driver.FindElement(By.Id("box0"));

            // Assert that the new box element is displayed
            Assert.That(newbox.Displayed, Is.True);
        }

        [Test, Order(4)]
        public void AddBoxWithImplicitWait()
        {
            // Click the button to add a box
            driver.FindElement(By.Id("adder")).Click();

            // Set up implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Attempt to find the newly added box element
            IWebElement newbox = driver.FindElement(By.Id("box0"));

            // Assert that the new box element is displayed
            Assert.That(newbox.Displayed, Is.True);
        }

        [Test, Order(5)]
        public void RevealInputWithImplicitWaits()
        {
            // Set up implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Click the button to reveal the input
            driver.FindElement(By.Id("reveal")).Click();

            // Find the revealed element (implicit wait will handle the wait)
            IWebElement revealed = driver.FindElement(By.Id("revealed"));

            Assert.That(revealed.TagName, Is.EqualTo("input"));
        }

        [Test, Order(6)]
        public void RevealInputWithExplicitWaits()
        {
            IWebElement revealed = driver.FindElement(By.Id("revealed"));
            driver.FindElement(By.Id("reveal")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(d => revealed.Displayed);

            revealed.SendKeys("Displayed");

            Assert.That(revealed.GetAttribute("value"), Is.EqualTo("Displayed"));
        }

        [Test, Order(7)]
        public void AddBoxWithFluentWaitExpectedConditionsAndIgnoredExceptions()
        {
            // Click the button to add a box
            driver.FindElement(By.Id("adder")).Click();

            // Set up FluentWait with ExpectedConditions
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            // Wait until the new box element is present and displayed
            IWebElement newBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box0")));

            // Assert that the new box element is displayed
            Assert.That(newBox.Displayed, Is.True);
        }

        [Test, Order(8)]
        public void RevealInputWithCustomFluentWait()
        {
            IWebElement revealed = driver.FindElement(By.Id("revealed"));
            driver.FindElement(By.Id("reveal")).Click();

            var wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(5),
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };

            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));

            wait.Until(d => {
                revealed.SendKeys("Displayed");
                return true;
            });

            Assert.That(revealed.TagName, Is.EqualTo("input"));
            Assert.That(revealed.GetAttribute("value"), Is.EqualTo("Displayed"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

    }
}