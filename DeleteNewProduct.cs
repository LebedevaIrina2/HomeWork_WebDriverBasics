using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDriverBasics
{
    class DeleteNewProduct
    {
        private IWebDriver driver;
        public DeleteNewProduct(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement searchAllProducts => driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")); // ссылка "All Products"

        private IWebElement removeProduct => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[10])"));


        public void RemoveProducts()
        {
            new Actions(driver).Click(searchAllProducts).SendKeys(Keys.Enter).Build().Perform();
            removeProduct.Click();            
            driver.SwitchTo().Alert().Accept(); //Подтверждаем удаление в всплывающем окне предупреждения
             //Thread.Sleep(500);


        }



    }
}
