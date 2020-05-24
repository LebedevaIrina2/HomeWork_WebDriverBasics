using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebDriverBasics
{
    class AllProducts
    {
        private IWebDriver driver;
        public AllProducts(IWebDriver driver)
        {
            this.driver = driver;
        }

        private IWebElement searchAllProducts => driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")); // ссылка "All Products"
        private IWebElement searchProductName => driver.FindElement(By.XPath("//a[text()=\"King prawns\"]")); // Поле ввода названия продукта
        private IWebElement selectCategoryId => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[1])"));  // Поле выбора категории продукта        
        private IWebElement selectSupplierId => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[2])")); // Поле выбора поставщика...
        private IWebElement serachQuantityPerUnit => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[3])"));
        private IWebElement searchUnitPrice => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[4])"));
        private IWebElement searchUnitsInStock => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[5])"));
        private IWebElement searchUnitsOnOrder => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[6])"));
        private IWebElement searchReorderLevel => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[7])"));
        private IWebElement searchDiscontinued => driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[8])"));       
        private IWebElement checkNameAllProductsPage => driver.FindElement(By.CssSelector("h2"));   // страница Product Editing


        public string SelectProductName()   //Проверяем строку с названием продукта
        {
            //searchAllProducts.Click();
            new Actions(driver).Click(searchAllProducts).SendKeys(Keys.Enter).Build().Perform();

            return searchProductName.Text; 
        }

        public string SelectCategoryID() { return selectCategoryId.Text; }  //Проверяем строку с названием категории продуктов...
        public string SelectSupplierID() { return selectSupplierId.Text; }
        public string SelectQuantityPerUnit () { return serachQuantityPerUnit.Text; }
        public string SelectUnitPrice() { return searchUnitPrice.Text; }         
        public string SelectUnitsInStock() { return searchUnitsInStock.Text; }
        public string SelectUnitsOnOrder() { return searchUnitsOnOrder.Text; }
        public string SelectReorderLevel() { return searchReorderLevel.Text; }
        public string SelectDiscontinuedl() { return searchDiscontinued.Text; }


        public string CheckNamePage() { return checkNameAllProductsPage.Text; }
    }
}
