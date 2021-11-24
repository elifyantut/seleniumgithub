using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace githubRepos
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://github.com/login");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Siteye Gidildi.");
            Thread.Sleep(5000);

            IWebElement userName = driver.FindElement(By.Name("login"));
            IWebElement password = driver.FindElement(By.Name("password"));
            IWebElement loginBtn = driver.FindElement(By.CssSelector(".btn.btn-primary.btn-block.js-sign-in-button"));

            //github giriş bilgileri doldurulacak.
            userName.SendKeys("   ");
            password.SendKeys("   ");
            loginBtn.Click();
            Thread.Sleep(5000);

            driver.Navigate().GoToUrl("https://github.com/elifyantut");
            Thread.Sleep(5000);

            IWebElement repoLink = driver.FindElement(By.CssSelector("#js-pjax-container > div.mt-4.position-sticky.top-0.d-none.d-md-block.color-bg-default.width-full.border-bottom.color-border-muted > div > div > div.Layout-main > div > nav > a:nth-child(2)"));
            repoLink.Click();
            Thread.Sleep(5000);


            //scrolldown start
            //#user-repositories-list > ul > li:nth-child(1) > div.col-10.col-lg-9.d-inline-block
            //string jsCommand = "" +
            //  "sayfa=document.querySelector('');" +
            //  "sayfa.scrollTo(0,sayfa.scrollHeight);" +
            //  "var sayfaSonu=sayfa.scrollHeight;" +
            //  "return sayfaSonu;";



            //var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));

            // while(true)
            // {
            //    var son = sayfaSonu;
            //    Thread.Sleep(2500);
            //    sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
            //   if (son == sayfaSonu)
            //       break;
            //

    


            // repo listeleme start

            int sayac = 1;
            IReadOnlyCollection<IWebElement> repos = driver.FindElements(By.CssSelector("#user-repositories-list > ul > li:nth-child(1) > div.col-10.col-lg-9.d-inline-block > div.d-inline-block.mb-1 > h3 > a"));
            foreach(IWebElement repo in repos )
            {
                Console.WriteLine(sayac+" ==>"+repo.Text );
                sayac++;
            
            }

            // repo listeleme end



            //scroll kaydırma
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollBy(0,950);");
            Console.Read();



        }
    }
}





