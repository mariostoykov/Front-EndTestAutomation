using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DataDriven
{
    [TestFixture]
    public class TestCalculator
    {
        IWebDriver driver;
        IWebElement textBoxFirstNum;
        IWebElement textBoxSecondNum;
        IWebElement dropDownOperation;
        IWebElement calcBtn;
        IWebElement resetBtn;
        IWebElement divResult;

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";

            textBoxFirstNum = driver.FindElement(By.Id("number1"));
            dropDownOperation = driver.FindElement(By.Id("operation"));
            textBoxSecondNum = driver.FindElement(By.Id("number2"));
            calcBtn = driver.FindElement(By.Id("calcButton"));
            resetBtn = driver.FindElement(By.Id("resetButton"));
            divResult = driver.FindElement(By.Id("result"));
        }

        public void PerformCalculation(string firstNumber, string operation, string secondNumber, string expectedResult)
        {
            // Click the [Reset] button
            resetBtn.Click();

            // Send values to the corresponding fields if they are not empty
            if (!string.IsNullOrEmpty(firstNumber))
            {
                textBoxFirstNum.SendKeys(firstNumber);
            }

            if (!string.IsNullOrEmpty(secondNumber))
            {
                textBoxSecondNum.SendKeys(secondNumber);
            }

            if (!string.IsNullOrEmpty(operation))
            {
                new SelectElement(dropDownOperation).SelectByText(operation);
            }

            // Click the [Calculate] button
            calcBtn.Click();

            // Assert the expected and actual result text are equal
            Assert.That(divResult.Text, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("20", "+ (sum)", "10", "Result: 30")]
        [TestCase("8.5", "- (subtract)", "1.5", "Result: 7")]
        [TestCase("2e2", "* (multiply)", "1.5", "Result: 300")]
        [TestCase("2", "/ (divide)", "0", "Result: Infinity")]
        [TestCase("invalid", "+ (sum)", "88", "Result: invalid input")]
        public void TestNumberCalculator(string firstNumber, string operation,string secondNumber, string expectedResult)
        {
            PerformCalculation(firstNumber, operation, secondNumber, expectedResult);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}