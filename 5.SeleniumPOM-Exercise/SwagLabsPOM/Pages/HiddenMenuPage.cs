using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SwagLabsPOM.Pages
{
    public class HiddenMenuPage : BasePage
    {
        private readonly By menuButton = By.CssSelector(".bm-burger-button");
        private readonly By logoutButton = By.Id("logout_sidebar_link");

        public HiddenMenuPage(IWebDriver driver) : base(driver) { }

        public void ClickMenuButton()
        {
            Click(menuButton);
        }

        public void ClickLogoutButton()
        {
            Click(logoutButton);
        }

        public bool IsMenuOpen()
        {
            return FindElement(logoutButton).Displayed;
        }
    }
}
