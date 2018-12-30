using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aspnetWKo.Models;
using aspnetWKo.Interfaces;

namespace aspnetWKo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly localdb_DEntities ProductDB = new localdb_DEntities();
        public IEnumerable<tblProduct> GetAll()
        {
            return ProductDB.tblProducts;
        }
        public tblProduct Get(int id)
        {
            return ProductDB.tblProducts.Find(id);
        }
        public tblProduct Add(tblProduct item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("Item");
            }

            ProductDB.tblProducts.Add(item);
            ProductDB.SaveChanges();
            return item;
        }
        public bool Update(tblProduct item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Item");
            }

            var products = ProductDB.tblProducts.Single(a => a.Id == item.Id);
            products.Name = item.Name;
            products.Category = item.Category;
            products.Price = item.Price;
            ProductDB.SaveChanges();

            return true;
        }
        public bool Delete(int id)
        {
            tblProduct products = ProductDB.tblProducts.Find(id);
            ProductDB.tblProducts.Remove(products);
            ProductDB.SaveChanges();

            return true;
        }
    }
}