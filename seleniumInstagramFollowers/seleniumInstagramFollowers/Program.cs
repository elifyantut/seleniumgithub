using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace seleniumInstagramFollowers
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.instagram.com");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Siteye Gidildi.");
            Thread.Sleep(2000);


            IWebElement userName = driver.FindElement(By.Name("username"));
            IWebElement password = driver.FindElement(By.Name("password"));
            IWebElement loginBtn = driver.FindElement(By.CssSelector(".sqdOP.L3NKy.y3zKF"));
            
            //kullanıcı bilgilerini doldurunuz.
            userName.SendKeys("     ");
            password.SendKeys("     ");
            loginBtn.Click();
            Console.WriteLine("Hesap bilgileri girildi.");
            Thread.Sleep(2500);

            //profil linkini ekleyiniz.
            driver.Navigate().GoToUrl("https://www.instagram.com/?????");
            Console.WriteLine("Profile yönlendirildi.");
            Thread.Sleep(2500);

            IWebElement followerLink = driver.FindElement(By.CssSelector("#react-root > section > main > div > header > section > ul > li:nth-child(2) > a"));
            followerLink.Click();
            Thread.Sleep(2500);

            //scrolldown start
            //isgrP
            string jsCommand = "" +
                "sayfa=document.querySelector('.isgrP');" +
                "sayfa.scrollTo(0,sayfa.scrollHeight);" +
                "var sayfaSonu=sayfa.scrollHeight;" +
                "return sayfaSonu;";

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));

            while(true)
            {
                var son = sayfaSonu;
                Thread.Sleep(2500);
                sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                if (son == sayfaSonu)
                    break;
            }


            // takipçi listeleme start

            int sayac = 1;
            IReadOnlyCollection<IWebElement> followers = driver.FindElements(By.CssSelector(".FPmhX.notranslate._0imsa"));
            foreach(IWebElement follower in followers )
            {
                Console.WriteLine(sayac+" ==>"+follower.Text );
                sayac++;

            }

            // takipçi listeleme end
        }
    }
}
