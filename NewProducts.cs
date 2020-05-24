using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WebDriverBasics
{
    class NewProducts
    {
        private IWebDriver driver;
        public NewProducts(IWebDriver driver)
        {
            this.driver = driver;
        }


        private IWebElement searchAllProducts => driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")); // ссылка "All Products"
        private IWebElement searchCreateNew => driver.FindElement(By.XPath("//a[contains(text(), 'Create new')]")); // Кнопка "создать новый продукт"
        private IWebElement searchProductName => driver.FindElement(By.Id("ProductName")); // Поле ввода названия продукта
        private IWebElement selectCategoryId => driver.FindElement(By.Id("CategoryId"));  // Поле выбора категории продукта        
        private IWebElement selectSupplierId => driver.FindElement(By.Id("SupplierId")); // Поле выбора поставщика...
        private IWebElement searchUnitPrice => driver.FindElement(By.Id("UnitPrice"));
        private IWebElement serachQuantityPerUnit => driver.FindElement(By.Id("QuantityPerUnit"));
        private IWebElement searchUnitsInStock => driver.FindElement(By.Id("UnitsInStock"));
        private IWebElement searchUnitsOnOrder => driver.FindElement(By.Id("UnitsOnOrder"));
        private IWebElement searchReorderLevel => driver.FindElement(By.Id("ReorderLevel"));
        private IWebElement searchDiscontinued => driver.FindElement(By.Id("Discontinued"));
        private IWebElement searchButtonSend => driver.FindElement(By.XPath("//input[@type=\"submit\"]"));
        private IWebElement productEditing => driver.FindElement(By.CssSelector("h2"));   // страница Product Editing


        // СОЗДАЕМ МЕТОДЫ
        public void CreateNewProductsName(string productDescription)  // Переходим по ссылкам All Products => Create New=> Создаем новое имя продукта
        {
            searchAllProducts.Click();
            searchCreateNew.Click();           
            searchProductName.SendKeys(productDescription);
        }

        public void SelectNewCategoryId(string productDescription) { new SelectElement(selectCategoryId).SelectByText(productDescription); } // Выбираем категорию для нового продукта...
        public void SelectNewSupplierId(string productDescription) { new SelectElement(selectSupplierId).SelectByText(productDescription); }
        public void CreateNewUnitPrice(string productDescription) { searchUnitPrice.SendKeys(productDescription); }
        public void CreateNewQuantityPerUnit(string productDescription) { serachQuantityPerUnit.SendKeys(productDescription); }
        public void CreateNewUnitsInStock(string productDescription) { searchUnitsInStock.SendKeys(productDescription); }
        public void CreateNewUnitsOnOrder(string productDescription) { searchUnitsOnOrder.SendKeys(productDescription); }

        public AllProducts CreateNewReorderLevel(string productDescription) // + отмечаем скидку,нажимаем "отправить" и переходим на страницу AllProducts
        {
            //searchReorderLevel.SendKeys(productDescription);
            new Actions(driver).Click(searchReorderLevel).SendKeys(productDescription).Build().Perform();

            searchDiscontinued.Click();
          
            //searchButtonSend.Click();
            new Actions(driver).Click(searchButtonSend).SendKeys(Keys.Enter).Build().Perform();    

            return new AllProducts(driver);
        }

    }
}

