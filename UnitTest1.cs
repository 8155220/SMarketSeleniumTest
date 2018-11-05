using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SmarketSelenium
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Producto a guardar
            string nombreProducto = "DiscoDuroa45136";

            string url = "http://localhost:4200/products";
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            //[Add Product] Click()
            var ddd = driver.FindElement(By.CssSelector("a[href='/products/create']"));
            ddd.Click();

            //[seleccionar una imagen]
            ddd = driver.FindElement(By.XPath("//input[@type='file']"));
            ddd.SendKeys("C://Users/Shep/Pictures/Productos/producto.jpg");

            //Llenar datos del formulario
            var name = driver.FindElement(By.CssSelector("input[formcontrolname='name']"));
            name.SendKeys(nombreProducto);
            var providerName = driver.FindElement(By.CssSelector("input[formcontrolname='provider']"));
            providerName.SendKeys("Samsung");
            var description = driver.FindElement(By.CssSelector("input[formcontrolname='description']"));
            description.SendKeys("Disco Duro");
            var buyPrice = driver.FindElement(By.CssSelector("input[formcontrolname='buyPrice']"));
            buyPrice.SendKeys("456");
            var sellPrice = driver.FindElement(By.CssSelector("input[formcontrolname='sellPrice']"));
            sellPrice.SendKeys("650");
            var expirationDate = driver.FindElement(By.CssSelector("input[formcontrolname='expirationDate']"));
            expirationDate.SendKeys("2018-11-01");
            var productType = driver.FindElement(By.CssSelector("select[formcontrolname='productTypeId']"));
            SelectElement productTypeSelect = new SelectElement(productType);
            productTypeSelect.SelectByIndex(0);
            var unitType = driver.FindElement(By.CssSelector("select[formcontrolname='unitTypeId']"));
            SelectElement unitTypeSelect = new SelectElement(unitType);
            unitTypeSelect.SelectByIndex(0);
            //Terminar Seleccionar Datos

            //[Save] click();
            var saveButton = driver.FindElement(By.CssSelector("button.btn.btn-success"));
            saveButton.Click();

            //Esperar a que el producto sea subido
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("a[href='/products/create']")));

            //Esperar 3 segundos
            Thread.Sleep(5000);
            var productTypeIndex = driver.FindElement(By.CssSelector("a[href='/product-type']"));
            productTypeIndex.Click();
            var unitTypeIndex = driver.FindElement(By.CssSelector("a[href='/unit-type']"));
            unitTypeIndex.Click();
            //----------------------------
            
            //Ir a la pagina de product/index verificar que se encuentre en la lista
            var productIndex = driver.FindElement(By.CssSelector("a[href='/products']"));
            productIndex.Click();
            var labelProductCreated = driver.FindElement(By.XPath("//h5[contains(text(),'"+ nombreProducto +"')]"));

            Assert.IsTrue(labelProductCreated.Displayed);
        }
    }
}
