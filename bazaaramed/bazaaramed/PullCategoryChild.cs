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
    public class PullCategoryChild
    {
        public void category_added()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.bazaaramed.com/index.php");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);

            IReadOnlyCollection<IWebElement> ParentCategoryis = driver.FindElements(By.ClassName("mainLi"));


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
                categorya.Source = 2;
                categorya.Address = CategoryNameUrl;
                categorya.Description = CategoryCode;
                using (var context = new ProductContext())
                {
                    context.Categories.AddRange(categorya);

                    context.SaveChanges();
                }
                Console.WriteLine(katname);
                Console.WriteLine(CategoryNameUrl);
                Console.WriteLine(CategoryCode);


                IReadOnlyCollection<IWebElement> ChildCategory = Categoryi.FindElements(By.XPath("div/ul/li"));

                foreach (IWebElement ChildCategorychild in ChildCategory)
                {
                    //Name
                    IWebElement ChildCategorychild2 = ChildCategorychild;
                    bool c = true;

                    try
                    {

                        ChildCategorychild.FindElement(By.XPath("h4"));

                    }
                    catch (Exception)
                    {
                        c = false;
                    }
                    if (c)
                    {
                         ChildCategorychild2 = ChildCategorychild.FindElement(By.XPath("h4"));

                    }
                    

                    string ChildCategorychildname = ChildCategorychild2.FindElement(By.TagName("A")).GetAttribute("innerHTML");
                    string ChildCategorychildaddress = ChildCategorychild2.FindElement(By.TagName("A")).GetAttribute("href");

                    Regex r2 = new Regex(@".*\/(?<katCode>.*?$)");

                    string CategoryCode2 = null;
                    if (r2.Match(ChildCategorychildaddress).Success)
                    {
                        CategoryCode2 = r2.Match(ChildCategorychildaddress).Groups["katCode"].Value;
                    }

                    Console.WriteLine(ChildCategorychildname);
                    Console.WriteLine(ChildCategorychildaddress);
                    Console.WriteLine(CategoryCode2);

                    Category categorya2 = new Category();
                    categorya2.Name = ChildCategorychildname;
                    categorya2.State = true;
                    categorya2.Source = 3;
                    categorya2.Address = ChildCategorychildaddress;
                    categorya2.Description = CategoryCode2;
                    categorya2.ParentCategoryID = categorya.ID;
                    using (var context = new ProductContext())
                    {
                        context.Categories.AddRange(categorya2);

                        context.SaveChanges();
                    }


                }

            }
            }
        }
    }


