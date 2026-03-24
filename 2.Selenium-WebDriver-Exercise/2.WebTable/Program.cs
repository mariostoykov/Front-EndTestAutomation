using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebTable
{
    [TestFixture]
    public class WebTable
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Create object of ChromeDriver
            driver = new ChromeDriver();

            // Add implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestExtractProductInformation()
        {
            // Launch Chrome browser with the given URL
            driver.Url = "http://practice.bpbonline.com/";

            // Identify the web table
            IWebElement productTable = driver.FindElement(By.XPath("//*[@id='bodyContent']/div/div[2]/table"));

            // Find the number of rows
            ReadOnlyCollection<IWebElement> tableRows = productTable.FindElements(By.XPath("//tbody/tr"));

            // Path to save the CSV file
            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

            // If the file exists in the location, delete it
            if (File.Exists(path))
            File.Delete(path);

            // Traverse through table rows to find the table columns
            foreach (IWebElement trow in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableCols = trow.FindElements(By.XPath("td"));
                foreach(IWebElement tcol in tableCols)
                {
                    // Extract product name and cost
                    String data = tcol.Text;
                    String[] productInfo = data.Split('\n');
                    String printProductInfo = productInfo[0].Trim() + "," + productInfo[1].Trim() + "\n";

                    // Write product information extracted to the file
                    File.AppendAllText(path, printProductInfo);
                }
            }

            // Verify the file was created and has content
            Assert.That(File.Exists(path), "CSV file was not created");
            Assert.That(new FileInfo(path).Length > 0, "CSV file is empty");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
