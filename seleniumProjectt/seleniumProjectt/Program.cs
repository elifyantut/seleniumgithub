using MySql.Data.MySqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Threading;

namespace seleniumProjectt
{
    class Program
    {
        public const string ConnectionString = @"server=localhost;port=3306;database=selenium2;user=root;";

        static void Main(string[] args)
        {

            GetMySqlConnection();

            var chromeOptions = new ChromeOptions();

            IWebDriver browser = new ChromeDriver(chromeOptions);

            browser.Navigate().GoToUrl("https://www.etsy.com/");
            Thread.Sleep(5000);

            //Login button
            IWebElement btnLoginWindow = browser.FindElement(By.CssSelector("button.select-signin"));
            if (btnLoginWindow == null)
            {
                throw new NullReferenceException("Login button is not found");
            }

            btnLoginWindow.Click();
            Thread.Sleep(5000);

            //Login
            IWebElement email = browser.FindElement(By.Id("join_neu_email_field"));
            IWebElement password = browser.FindElement(By.Id("join_neu_password_field"));
            IWebElement btnSignin = browser.FindElement(By.CssSelector("button[name='submit_attempt']"));


            email.SendKeys("...............");
            password.SendKeys("..........");
            btnSignin.Click();
            Thread.Sleep(8000);



            List<string> Sellers = new List<string>
           {
               "Stephaniestoredress",
               "TheGVB",
               "seawnd"

           };

            foreach (var seller in Sellers)
            {




                //Seller page
                browser.Navigate().GoToUrl($"https://www.etsy.com/shop/{seller}");
                Thread.Sleep(7000);

                //Getting the list of items
                IWebElement featuredArea = browser.FindElement(By.ClassName("featured-products-area"));
                if (featuredArea == null)
                {
                    throw new NullReferenceException("Featured Area is not found");
                }

                IReadOnlyCollection<IWebElement> featuredProducts = featuredArea.FindElements(By.ClassName("v2-listing-card"));
                foreach (var featuredProduct in featuredProducts)
                {
                    var listingId = featuredProduct.GetAttribute("data-listing-id");

                    var listingTag = featuredProduct.FindElement(By.TagName("a"));
                    var listingUrl = listingTag.GetAttribute("href");
                    var listingTitle = listingTag.GetAttribute("title");

                    Console.WriteLine($"{listingId} - {listingTitle}");
                    Model.Dto.Product p = new Model.Dto.Product()
                    {
                        ListingId = int.Parse(listingId),
                        Title = listingTitle,
                        Url = listingUrl,
                        SellerName = seller
                    };


                    InsertProduct(p);
                }



                //InsertSellers(item);
            }





        }

        static void GetMySqlConnection()
        {
         

            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("bağlantı sağlandı.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("BAĞLANAMADI.");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

        }


        static void InsertSellers(string Name)
        {
            
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string Sql = "insert into sellers (sellername) " +
                        "Values (@sellername)";
                    var command = new MySqlCommand(Sql, connection);
                    command.Parameters.AddWithValue("sellername", Name);

                    int result = command.ExecuteNonQuery();
                    Console.WriteLine($"{result} adet ürün eklendi.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ürün eklerken bir hata oluştu.");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }



        static void InsertProduct(Model.Dto.Product product)
        {
            if(IsProductExists(product.ListingId)) { return; }

            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {                 


                    string Sql = "insert into products (id,products,url,title) " +
                        "Values (@productid,@url,@title)";
                    var command = new MySqlCommand(Sql, connection);
                    command.Parameters.AddWithValue("productid", listingId);
                    command.Parameters.AddWithValue("url", listingUrl);
                    command.Parameters.AddWithValue("title", listingTitle);
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine($"{result} adet ürün eklendi.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ürün eklerken bir hata oluştu.");
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }










        }

        static bool IsProductExists(int listingId) {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string IsExsistSql = "select * where listingId=@listingid";
                    var command = new MySqlCommand(IsExsistSql, connection);
                    command.Parameters.AddWithValue("@listingId", listingId); 
                    int result = command.ExecuteNonQuery();
                    return result==0?false:true;
                }
                catch (MySql.Data.MySqlClient.MySqlException exception)
                {

                    Console.WriteLine(exception.Message);
                    throw new Exception("MySql Client can not connet");
                }

            }

            }


        static bool IsSellerExists(string name)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string IsExsistSql = "select *  where name=@name";
                    var command = new MySqlCommand(IsExsistSql, connection);
                    command.Parameters.AddWithValue("@name", name);
                    int result = command.ExecuteNonQuery();
                    return result == 0 ? false : true;
                }
                catch (MySql.Data.MySqlClient.MySqlException exception)
                {

                    Console.WriteLine(exception.Message);
                    throw new Exception("MySql Client can not connet");
                }

            }

        }
    }
}
