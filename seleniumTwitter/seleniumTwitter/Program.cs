using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace seleniumTwitter
{
    class Program
    {
        static void Main(string[] args)
        {

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://github.com/login");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Siteye Gidildi.");
            Thread.Sleep(10000);

            IWebElement userName = driver.FindElement(By.Name("login"));
            IWebElement password = driver.FindElement(By.Name("password"));
            IWebElement loginBtn = driver.FindElement(By.CssSelector(".btn.btn-primary.btn-block.js-sign-in-button"));

            //github giriş bilgileri doldurulacak.
            userName.SendKeys("kullanıcı adı");
            password.SendKeys("şifre");
            loginBtn.Click();
            Thread.Sleep(5000);

            driver.Navigate().GoToUrl("https://github.com/settings/profile");
            Thread.Sleep(5000);

            IWebElement edit = driver.FindElement(By.CssSelector(".position-absolute.color-bg-default.rounded-2.color-fg-default.px-2.py-1.left-0.bottom-0.ml-2.mb-2.border"));
            edit.Click();

            IWebElement photo = driver.FindElement(By.CssSelector("#js-pjax-container > div > div.d-flex.flex-md-row.flex-column.px-md-0.px-3 > div.col-md-9.col-12 > div.clearfix.gutter.d-flex.flex-shrink-0.flex-column-reverse.flex-md-row > div.col-12.col-md-4 > dl > dd > div > details > details-menu > label"));
            photo.SendKeys("C:\\Users\\ASUS\\siyah.jpg"); 
            
            Thread.Sleep(1000);



        

            

        }
    }
} 