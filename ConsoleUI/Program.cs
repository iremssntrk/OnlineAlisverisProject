using Business.Abstract;
using Business.Concreate;
using DataAccess.Abstract;
using DataAccess.Concreate.EntityFramework;
using DataAccess.Concreate.InMemory;
using Entities.Concreate;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            //CategoryTest();
            //Dto();


        }

        private static void Dto()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

            var result = productManager.GetProductDetails();
            if (result.Success)
            { 
                foreach (var p in result.Data)
                {
                    Console.WriteLine(p.ProductName + p.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));



            var result = productManager.GetByUnitPrice(50, 150);
            if (result.Success)
            {
                foreach (var p in result.Data)
                {
                    Console.WriteLine(p.ProductName + p.ProductId);
                    Console.WriteLine(result.Message);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }



        }
    }
}
