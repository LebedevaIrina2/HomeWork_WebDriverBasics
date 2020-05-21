using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace NUnitTestProject1
{
    public class Tests
    {
        private IWebDriver driver;

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
            Assert.AreEqual("Login", driver.FindElement(By.CssSelector("h2")).Text);  // �������� �������� ������ �������� ����������� � ������� "Login" 

            driver.FindElement(By.XPath("//input[@id=\"Name\"]")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@id=\"Password\"]")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();

            Assert.AreEqual("Home page", driver.FindElement(By.CssSelector("h2")).Text); // �������� �������� ����������� - ������ ���� ��������� �������� "Home page"
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
            driver.FindElement(By.XPath("//a[contains(text(), 'Create new')]")).Click();

            Assert.AreEqual("Product editing", driver.FindElement(By.CssSelector("h2")).Text); // �������� �������� ������ �������� �������������� �������� "Product editing" 

            driver.FindElement(By.Id("ProductName")).SendKeys("King prawns");
            new SelectElement(driver.FindElement(By.Id("CategoryId"))).SelectByText("Seafood");
            new SelectElement(driver.FindElement(By.Id("SupplierId"))).SelectByText("Pavlova, Ltd.");
            driver.FindElement(By.Id("UnitPrice")).SendKeys("500");
            driver.FindElement(By.Id("QuantityPerUnit")).SendKeys("24 pieces");
            driver.FindElement(By.Id("UnitsInStock")).SendKeys("20");
            driver.FindElement(By.Id("UnitsOnOrder")).SendKeys("3");
            driver.FindElement(By.Id("ReorderLevel")).SendKeys("2");
            driver.FindElement(By.Id("Discontinued")).Click();

            Assert.IsTrue(driver.FindElement(By.XPath("//input[@id=\"Discontinued\"]")).Enabled);// ��������, ������� �� ������� "Discontinued"

            driver.FindElement(By.XPath("//input[@type=\"submit\"]")).Click();
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
            Assert.AreEqual("King prawns", driver.FindElement(By.XPath("//a[text()=\"King prawns\"]")).Text); // �������� ProductName
            Assert.AreEqual("Seafood", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[1])")).Text); // CategoryName
            Assert.AreEqual("Pavlova, Ltd.", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[2])")).Text); // �������� SupplierName
            Assert.AreEqual("24 pieces", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[3])")).Text); // �������� QuantityPerUnit
            Assert.AreEqual("500,0000", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[4])")).Text); // �������� UnitPrice
            Assert.AreEqual("20", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[5])")).Text); // �������� UnitsInStock
            Assert.AreEqual("3", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[6])")).Text); // �������� UnitsOnOrder
            Assert.AreEqual("2", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[7])")).Text); // �������� ReorderLevel
            Assert.AreEqual("True", driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[8])")).Text); // �������� Discontinued
        }


        //Test 4. Delete Created Products
        [Test]
        public void Test4_Delete()
        {
            // Autorization
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();


            driver.FindElement(By.XPath("//a[contains(text(), 'All Products')]")).Click();
            driver.FindElement(By.XPath("(//td/a[text()=\"King prawns\"]/..//following-sibling::td[10])")).Click(); // �������� ""�������"
            driver.SwitchTo().Alert().Accept(); //������������� �������� � ����������� ���� ��������������

        }


        //Test 5. Logout
        [Test]
        public void Test5_Logout()
        {


            // Autorization
            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();


            driver.FindElement(By.XPath("//a[text()='Logout']")).Click(); //LOGOUT
            AssertionException.Equals("user", driver.FindElement(By.Id("Name"))); // ��������,��� ���� �� �������� ����� ��������� ����� "user"
            AssertionException.Equals("user", driver.FindElement(By.Id("Password"))); // ��������,��� ���� �� �������� ����� ��������� ����� "user"

            driver.FindElement(By.Id("Name")).SendKeys("user");
            driver.FindElement(By.Id("Password")).SendKeys("user");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }



        // ������� ���� �������� ����� ���������� �����
        [TearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }

}
