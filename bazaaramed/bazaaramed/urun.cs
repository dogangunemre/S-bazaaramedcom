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
    public class urun
    {
        public void urun_added(string proURL)
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            IWebDriver driver = new ChromeDriver(options);

            //IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(proURL);
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Siteye Gidildi!");
            Thread.Sleep(2000);
            var contex = new ProductContext();

            List<Product> pro = new List<Product>();
            Product Producta = new Product();


            //Brand

            IWebElement brandPath = driver.FindElement(By.ClassName("d-marka"));

            string brandbul = brandPath.GetAttribute("innerHTML");
            Brand brandbul2 = contex.Brands.FirstOrDefault(o => o.Name == brandbul);
            if (brandbul2 != null)
            {
                Producta.BrandID = brandbul2.ID;

            }
          


            //Unit

            IWebElement UnitPath = driver.FindElement(By.XPath("/html/body/b/b/b/b/section/div/div/div[3]/section/div/div/div[1]/div[3]/div[7]"));
            string Unitbul = UnitPath.GetAttribute("innerHTML");
            Unit Unitbul2 = contex.Unit.FirstOrDefault(o => o.Name == Unitbul);
            if (Unitbul2!=null)
            {
                Producta.UnitID = Unitbul2.ID;

            }
            else
            {
                Unit unita = new Unit();
                unita.Name = Unitbul;
                unita.State = true;
                unita.Source = 3;
                unita.Description = Unitbul;
                unita.Code = Unitbul;
                using (var context = new ProductContext())
                {
                    context.Unit.AddRange(unita);

                    context.SaveChanges();
                }
                Producta.UnitID = unita.ID;

            }


            //Barcode
            IWebElement BarcodePath = driver.FindElement(By.XPath("/html/body/b/b/b/b/section/div/div/div[3]/section/div/div/div[1]/div[3]/div[5]/div/span"));
            string BarcodePathi = BarcodePath.GetAttribute("innerHTML");
            Producta.Barcode = BarcodePathi;

            //MaterialCode
            IWebElement MaterialCodePath = driver.FindElement(By.XPath("/html/body/b/b/b/b/section/div/div/div[3]/section/div/div/div[1]/div[3]/div[4]/div/span"));
            string MaterialCodePathi = MaterialCodePath.GetAttribute("innerHTML");
            Producta.MaterialCode = MaterialCodePathi;


            //Name
            IWebElement NamePath = driver.FindElement(By.XPath("/html/body/b/b/b/b/section/div/div/div[3]/section/div/div/div[1]/div[3]/h3"));
            string NamePathi = NamePath.GetAttribute("innerHTML");
            Producta.Name = NamePathi;


            //Desciription
            IWebElement DesciriptionPath = driver.FindElement(By.ClassName("d-urunaciklama"));
            string DesciriptionPathi = DesciriptionPath.GetAttribute("innerHTML");
            Producta.Description = DesciriptionPathi;

            //State
            Producta.State = true;

            //Category
            IWebElement CategoryPath = driver.FindElement(By.XPath("/html/body/b/b/b/b/section/div/div/div[2]/a[last()]"));
            //IWebElement CategoryPath2 = driver.FindElement(By.ClassName("BreadCrumb[last()]"));

            string katURLbul = CategoryPath.GetAttribute("href");

            Regex r4 = new Regex(@".*\/(?<katCode>.*$)");

            string CatgoryCode = null;
            if (r4.Match(katURLbul).Success)
            {
                CatgoryCode = r4.Match(katURLbul).Groups["katCode"].Value;
            }

            Category bul = contex.Categories.FirstOrDefault(o => o.Description == CatgoryCode);
            if (bul != null)
            {
                Producta.CategoryID = bul.ID;
                Console.WriteLine(CatgoryCode);
            }
            else
            {
                Producta.CategoryID = 460;

            }

            //Address 
            Producta.Address = proURL;

            //Source 
            Producta.Source = 3;//bazaaramed

            //Price
            IWebElement PricePath = driver.FindElement(By.XPath("//*[@id='shopPHPUrunFiyatOrg']"));
            string PricePathi = PricePath.GetAttribute("innerHTML");
            Producta.Price = Convert.ToDecimal(PricePathi)/100;


            //Code
            Regex r5 = new Regex(@".*\/(?<katCode>.*$)");

            string productCode = null;
            if (r5.Match(proURL).Success)
            {
                productCode = r5.Match(proURL).Groups["katCode"].Value;
            }
            Producta.Code = productCode;


            //File
            IWebElement File2Path = driver.FindElement(By.XPath("/html/body/b/b/b/b/section/div/div/div[3]/section/div/div/div[1]/div[2]/a"));
            string File2Pathi = File2Path.GetAttribute("href");

            Regex r6 = new Regex(@".*\/(?<katCode>.*$)");

            
             using (var context = new ProductContext())
             {
                 context.Products.AddRange(Producta);
                 context.SaveChanges();
             }

             File file = new File();
             file.Path = File2Pathi;
             file.State = true;
             file.RelativePath = productCode + ".jpg";
             file.ProductID = Producta.ID;

             System.Net.WebClient wc = new System.Net.WebClient();
             wc.DownloadFile(File2Pathi, String.Concat(@"C:\Users\Emre\source\repos\bazaaramed\bazaaramed\images\", productCode, ".jpg"));


             using (var context = new ProductContext())
             {
                 context.Files.Add(file);
                 context.SaveChanges();
             }


            driver.Close();

        }
    }
}