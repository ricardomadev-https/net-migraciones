using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudNet4
{
    internal class ProductService
    {
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        public Product Create(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("El nombre es obligatorio.", "name");
            }

            var product = new Product
            {
                Id = _nextId++,
                Name = name.Trim(),
                Price = price
            };

            _products.Add(product);
            return product;
        }

        public List<Product> GetAll()
        {
            return _products.OrderBy(p => p.Id).ToList();
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public bool Update(int id, string name, decimal price)
        {
            var product = GetById(id);
            if (product == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            product.Name = name.Trim();
            product.Price = price;
            return true;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null)
            {
                return false;
            }

            _products.Remove(product);
            return true;
        }
    }
}
