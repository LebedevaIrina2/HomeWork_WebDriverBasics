using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

    
    namespace NUnitTestProject1
{
    public class Tests
    {
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5000");   
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }


        //// Autorization
        [Test]
        public void Test1_Autorization()
        {
            Assert.IsTrue(driver.PageSource.Contains("Login")); // Проверка, что загрузилась нужная страница авторизации с текстом "Login" на ней 
               
            driver.FindElement(By.XPath("//input[@id=\"Name\"]")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@id=\"Password\"]")).SendKeys("user");

            Assert.IsTrue(driver.PageSource.Contains("Name")); //Проверка наличия текста "Name" на странице
            Assert.IsTrue(driver.PageSource.Contains("Password")); //Проверка наличия текста "Password" на странице

            driver.FindElement(By.XPath("//input[@type='submit']")).Click();

            Assert.IsTrue(driver.PageSource.Contains("Home page")); // Проверка успешной авторизации - должна быть загружена страница "Home page"
        }



        // Adding a New Product
        [Test]
        public void Test2_Add_Product()
        {
            // Autorization
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click(); 
            
            
            driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")).Click(); 

            Assert.IsTrue(driver.PageSource.Contains("Create new")); // Проверка,что на странице есть  кнопка с текстом "Create new"

            driver.FindElement(By.XPath("//a[contains(text(), 'Create new')]")).Click(); 
            driver.FindElement(By.Id("ProductName")).SendKeys("King prawns");

            SelectElement dropdown1 = new SelectElement(driver.FindElement(By.Id("CategoryId")));
            dropdown1.SelectByText("Seafood");

            SelectElement dropdown2 = new SelectElement(driver.FindElement(By.Id("SupplierId")));
            dropdown2.SelectByText("Pavlova, Ltd.");
                        
            driver.FindElement(By.Id("UnitPrice")).SendKeys("500");
            driver.FindElement(By.Id("QuantityPerUnit")).SendKeys("24 pieces");
            driver.FindElement(By.Id("UnitsInStock")).SendKeys("20");
            driver.FindElement(By.Id("UnitsOnOrder")).SendKeys("3");
            driver.FindElement(By.Id("ReorderLevel")).SendKeys("2");
            driver.FindElement(By.Id("Discontinued")).Click();

            Assert.IsTrue(driver.FindElement(By.XPath("//input[@id=\"Discontinued\"]")).Enabled);// проверка,включен ли элемент "Discontinued"
            Assert.IsTrue(driver.PageSource.Contains("Product editing")); // Проверка, что загрузилась нужная страница редакт-я продукта  "Product editing" 

            driver.FindElement(By.XPath("//input[@type=\"submit\"]")).Click();

            Assert.IsTrue(driver.PageSource.Contains("King prawns")); // Проверка,что продукт создан - название есть на странице
            
        }


        //Test 3. New Product. Check Values
        [Test]
        public void Test3_Check_Value()
        {
            // Autorization
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();

            driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")).Click(); 
            Assert.AreEqual("King prawns", driver.FindElement(By.XPath("//a[text()=\"King prawns\"]")).Text); // Проверка ProductName
            Assert.AreEqual("Seafood", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[1])")).Text); // CategoryName
            Assert.AreEqual("Pavlova, Ltd.", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[2])")).Text); // Проверка SupplierName
            Assert.AreEqual("24 pieces", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[3])")).Text); // Проверка QuantityPerUnit
            Assert.AreEqual("500,0000", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[4])")).Text); // Проверка UnitPrice
            Assert.AreEqual("20", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[5])")).Text); // Проверка UnitsInStock
            Assert.AreEqual("3", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[6])")).Text); // Проверка UnitsOnOrder
            Assert.AreEqual("2", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[7])")).Text); // Проверка ReorderLevel
            Assert.AreEqual("True", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[8])")).Text); // Проверка Discontinued


            
            Assert.IsTrue(driver.PageSource.Contains("ProductName")); // Проверка,что на странице есть "ProductName"
            Assert.IsTrue(driver.PageSource.Contains("King prawns")); // Проверка,что на странице есть "ProductId"
            Assert.IsTrue(driver.PageSource.Contains("CategoryName")); // Проверка,что на странице есть "CategoryName"


        }

        //Test 4. Delete Created Products
        [Test]
        public void Test4_Delete()
        {
            // Autorization
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();


            driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")).Click(); // Переходим в меню "Продукты"
            driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[10])")).Click(); // нажимаем ""Удалить"
            driver.SwitchTo().Alert().Accept();

            Assert.IsTrue(driver.PageSource.Contains("King prawns"));  // Проверка, действительно ли продукт удален (присутствует ли он в списке)

        }


        //Test 5. Logout
                [Test]
        public void Test5_Logout()
        {


            // Autorization
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();


            Assert.IsTrue(driver.PageSource.Contains("Logout")); //Проверка наличия текста ссылки "Logout" на странице
            
            driver.FindElement(By.XPath("//a[text()='Logout']")).Click(); //LOGOUT
            AssertionException.Equals("user", driver.FindElement(By.Id("Name"))); // проверка,что поле не содержит ранее введенный текст "user"
            AssertionException.Equals("user", driver.FindElement(By.Id("Password"))); // проверка,что поле не содержит ранее введенный текст "user"


            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            

            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }




        // Закрыть окно браузера после выполнения теста
        [TearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
}
    }

}