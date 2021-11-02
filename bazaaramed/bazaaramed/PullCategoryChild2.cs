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
    public class PullCategoryChild2
    {
        public void category_added(string catUrl,int pID)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(catUrl);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            IReadOnlyCollection<IWebElement> ParentCategoryis22 = driver.FindElements(By.ClassName("leftCat"));


            foreach (IWebElement Categoryi22 in ParentCategoryis22)
            {
                IReadOnlyCollection<IWebElement> ParentCategoryis = Categoryi22.FindElements(By.XPath("ul/li"));

                foreach (IWebElement Categoryi in ParentCategoryis)
                {
                    //Name
                    IWebElement Categoryi2 = Categoryi.FindElement(By.XPath("a"));
                    string katname = Categoryi2.GetAttribute("innerHTML");
                    string value1 = katname;
                    string value2 = value1.Replace("&amp;", "&");
                    katname = value2;
                    string CategoryNameUrl = Categoryi.FindElement(By.TagName("A")).GetAttribute("href");

                    Regex r = new Regex(@".*\/(?<katCode>.*?$)");

                    string CategoryCode = null;
                    if (r.Match(CategoryNameUrl).Success)
                    {
                        CategoryCode = r.Match(CategoryNameUrl).Groups["katCode"].Value;
                    }

                    Category categorya = new Category();
                    categorya.Name = katname;
                    categorya.State = true;
                    categorya.Source = 3;
                    categorya.Address = CategoryNameUrl;
                    categorya.Description = CategoryCode;
                    categorya.ParentCategoryID = pID;
                    using (var context = new ProductContext())
                    {
                        context.Categories.AddRange(categorya);

                        context.SaveChanges();
                    }
                    Console.WriteLine("######################");
                    Console.WriteLine(katname);
                    Console.WriteLine(CategoryNameUrl);
                    Console.WriteLine(CategoryCode);
                    Console.WriteLine(pID);

                }


            }
            }
        }
    }


