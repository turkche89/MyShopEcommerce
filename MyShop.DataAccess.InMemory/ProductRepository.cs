﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
       

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products==null)
            {
                products = new List<Product>();
            }//products is our product list
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.Id == product.Id);
            if (productToUpdate !=null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
            
        }

        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();//product listesini sorgulanabilir bir liste plarak döndürüyoruz.
        }

        public void Delete (string Id)
        {
            Product productsToDelete = products.Find(p => p.Id == Id);
            if (productsToDelete!=null)
            {
                products.Remove(productsToDelete);
            }
            else
            {
                throw new Exception("product not found");
            }
        }


    }
}
