using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository() //Constructor method
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit() // Method created to save
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p) //To add
        {
            productCategories.Add(p);

        }

        public void Update(ProductCategory productCategory) // Method created to Update
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p=>p.Id==productCategory.Id);
            if (productCategoryToUpdate==null)
            {
                productCategoryToUpdate = productCategory;
            }else
            {
                throw new Exception("Product Category not found");
            }
        }


        public ProductCategory Find(string Id) // Method created to find the category 
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
            if(productCategory!=null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("ProductCategory Not Found");
            }
        }

        public IQueryable<ProductCategory>Collection()// IQueryable Product category list creation to be able to search on the list created.
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(p => p.Id == Id);
            if(productCategoryToDelete!=null)
            {
                productCategories.Remove(productCategoryToDelete);
            }else
            {
                throw new Exception("Product Category Not Found");
            }

        }
        


    }
}
