using System;
using bazaaramed.Context;
using bazaaramed.Models;
using System.Collections.Generic;
using System.Linq;

namespace bazaaramed
{
    class Program
    {
        static void Main(string[] args)
        {

            //Brands brand = new Brands();
            //brand.brand_added();
            //PullCategoryChild pullCategoryChild = new PullCategoryChild();
            //pullCategoryChild.category_added();
            //PullCategoryChild2 pullCategoryChild2 = new PullCategoryChild2();

            //using (var contex = new ProductContext())
            //{
            //    List<Category> cat2 = contex.Categories.Where(s => s.State == false).ToList();
            //    foreach (var item in cat2)
            //    {
            //        try
            //        {
            //            pullCategoryChild2.category_added(item.Address,item.ID);
            //            item.State = true;
            //            contex.SaveChanges();
            //        }
            //        catch (Exception ex)
            //        {

            //            continue;
            //        }
            //    }

            //}
            //PullingProductAddressInCategory pullingProductAddressInCategory = new PullingProductAddressInCategory();


            //using (var contex = new ProductContext())
            //{
            //    List<Category> cat2 = contex.Categories.Where(s => s.State == false).ToList();
            //    foreach (var item in cat2)
            //    {
            //        try
            //        {
            //            pullingProductAddressInCategory.katpro(item.Address);
            //            item.State = true;
            //            contex.SaveChanges();
            //        }
            //        catch (Exception ex)
            //        {

            //            continue;
            //        }
            //    }

            //}
            urun urun = new urun();

            using (var contex = new ProductContext())
            {
                List<ProductAddress> cat2 = contex.ProductAddresses.Where(s => s.State == true).ToList();
                foreach (var item in cat2)
                {
                    try
                    {
                        urun.urun_added(item.Path);
                        item.State = false;
                        contex.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        //continue;
                    }
                }

            }
        }
    }
}
