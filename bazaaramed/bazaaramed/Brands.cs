using bazaaramed.Models;
using OpenQA.Selenium;
using bazaaramed.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using bazaaramed.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;

namespace bazaaramed
{
    public class Brands
    {
        public void brand_added()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.bazaaramed.com/ac/arama");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            IReadOnlyCollection<IWebElement> Brandis = driver.FindElements(By.Name("markaID"));

            List<Brand> brand = new List<Brand>();

            foreach (IWebElement Brandii in Brandis)
            {
                IReadOnlyCollection<IWebElement> Brandis2 = Brandii.FindElements(By.XPath("option"));

                foreach (IWebElement Brandii2 in Brandis2)
                {

                    string brandname = Brandii2.GetAttribute("innerHTML");

                    Console.WriteLine(brandname);

                    Brand branda = new Brand();
                    branda.Name = brandname;
                    branda.State = true;
                    branda.Source = 3;//bazaaramed

                    using (var context = new ProductContext())
                    {
                        context.Brands.AddRange(branda);

                        context.SaveChanges();
                    }

                }
            }




            }

        }

    }

