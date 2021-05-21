using System;
using System.Threading;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace proyecto_final_selenium.lib
{
    public class BlazeTest
    {
        IWebDriver Driver;

        public BlazeTest StartBrowser(){
            this.Driver = new ChromeDriver();
            this.Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0,0,4);
            return this;
        }

        public BlazeTest StartTest(string url){
            this.Driver.Url = url.Trim();
            this.Driver.Manage().Window.Maximize();
            // Do stuff
            ExecuteLogicalManagment();
            return this;
        }

        public BlazeTest CloseBrowser(){
            this.Driver.Close();
            return this;
        }

        public void Wait(int milliseconds) => Thread.Sleep(milliseconds);

        private void ExecuteLogicalManagment(){
            /*
                PROCESO:
                    CREAR CUENTA
                    INICIAR SESION
                    AGREGAR UN PRODUCTO AL CARRITO DE COMPRAS
            */
            var(username, password) = RegisterProcess(); // Crear cuenta.
            LoginProcess(username, password);            // Iniciar sesion.
            AddToCart();                                 // Agregar producto y revisar que se haya agregado.
            this.Driver.FindElement(By.CssSelector("#logout2")).Click(); // Cerrar sesion
        }

        private void AddToCart()
        {
            this.Wait(3000);
            this.Driver.FindElements(By.CssSelector(".container .row .col-lg-9 #tbodyid:first-child:first-child a"))[0].Click();
            this.Wait(3000);
            this.Driver.FindElement(By.CssSelector(".row .btn-success")).Click();
            this.Wait(3000);
            var alert = this.Driver.SwitchTo().Alert();
            System.Console.WriteLine("");
            System.Console.WriteLine($"!!!!!!!!!!!!!!!!! RESULTADO DEL CARRITO: {alert.Text} !!!!!!!!!!!!!!!");
            System.Console.WriteLine("");
            alert.Accept();
            this.Wait(5000);
            this.Driver.FindElement(By.CssSelector("#cartur")).Click();
            this.Wait(3000);
        }

        private (string u, string p) RegisterProcess(){
            var random = new Random();
            string usr = $"TestedUser{random.Next(1000)}";
            string pwd = "123456";
            // Html elements
            this.Driver.FindElement(By.Id("signin2")).Click();
            this.Wait(2000);
            this.Driver.FindElement(By.CssSelector("#signInModal .modal-dialog .modal-content .modal-body .form-group #sign-username")).SendKeys(usr);
            this.Driver.FindElement(By.CssSelector("#signInModal .modal-dialog .modal-content .modal-body .form-group #sign-password")).SendKeys(pwd);
            this.Driver.FindElement(By.CssSelector("#signInModal .modal-dialog .modal-content .modal-footer .btn-primary")).Click();
            this.Wait(2000);
            var alert = this.Driver.SwitchTo().Alert();
            System.Console.WriteLine("");
            System.Console.WriteLine($"!!!!!!!!!!!!!!!!! RESULTADO DEL REGISTRO: {alert.Text} !!!!!!!!!!!!!!!");
            System.Console.WriteLine("");
            alert.Accept();
            return(usr, pwd);
        }

        private void LoginProcess(string username, string password){
            this.Driver.FindElement(By.Id("login2")).Click();
            this.Driver.FindElement(By.CssSelector("#logInModal .modal-dialog .modal-content .modal-body .form-group #loginusername")).SendKeys(username);
            this.Driver.FindElement(By.CssSelector("#logInModal .modal-dialog .modal-content .modal-body .form-group #loginpassword")).SendKeys(password);
            this.Driver.FindElement(By.CssSelector("#logInModal .modal-dialog .modal-content .modal-footer .btn-primary")).Click();
        }
    }
}