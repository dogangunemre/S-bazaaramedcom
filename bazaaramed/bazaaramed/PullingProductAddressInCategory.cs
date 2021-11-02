using bazaaramed.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using bazaaramed.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace bazaaramed
{
    public class PullingProductAddressInCategory
    {
        public void katpro(string katurl)
        {
           
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(katurl);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            IReadOnlyCollection<IWebElement> LİstProduct = driver.FindElements(By.XPath("/html/body/b/b/b/b/section/div/div/div[3]/div/div[2]/div"));

            foreach (IWebElement LİstProductone in LİstProduct)
            {
                bool d = true;

                try
                {

                    LİstProductone.FindElement(By.XPath("div/h4"));

                }
                catch (Exception)
                {
                    d = false;
                }
                if (d)
                {
                    IWebElement katname = LİstProductone.FindElement(By.XPath("div/h4"));

                    string urunurl = katname.FindElement(By.TagName("A")).GetAttribute("href");

                    ProductAddress productAddress = new ProductAddress();
                    productAddress.State = true;
                    productAddress.Path = urunurl;
                    Console.WriteLine(urunurl);

                    using (var context = new ProductContext())
                    {
                        context.ProductAddresses.AddRange(productAddress);

                        context.SaveChanges();
                    }
                }
            }

                IWebElement nexturl = driver.FindElement(By.ClassName("next"));

                string nexturl2 = nexturl.FindElement(By.TagName("A")).GetAttribute("href");

                 if (nexturl2 != katurl)
                 {
                    driver.Close();
                    this.katpro(nexturl2);
                 }
                 else
                 {
                 Console.WriteLine("bitti");
                  }

           
               
            }


            

        }



            }
        
    
