﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace ProductsApp.Tests
{
    public class ProductsAppShould
    {
        private Products _products;
        public ProductsAppShould()
        {
            _products = new Products();
        }

        [Fact]
        public void ThrowError_When_Product_IsNotSpecified_Or_Null_()
        {
            //Arrange
            Product product = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => _products.AddNew(product));
        }

        [Fact]
        public void AddingNewProduct()
        {
            //Arrange
            Product product = new Product() { Name = "Product A" };

            //Act
            _products.AddNew(product);

            int itemCOunt = _products.Items.Count();
            //Assertions
            Assert.Equal(1,itemCOunt);
            Assert.Contains(product, _products.Items);
        }

        [Fact]
        public void ThrowError_When_ProductName_IsNotSpecified_Or_Null_()
        {
            //Arrange
            Product product = new Product();

            //Act & Assert
            Assert.Throws<NameRequiredException>(() => _products.AddNew(product));
        }
    }

    internal class Products
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> Items => _products.Where(t => !t.IsSold);

        public void AddNew(Product product)
        {
            product = product ??
                throw new ArgumentNullException();
            product.Validate();
            _products.Add(product);
        }

        public void Sold(Product product)
        {
            product.IsSold = true;
        }

    }

    internal class Product
    {
        public bool IsSold { get; set; }
        public string Name { get; set; }

        internal void Validate()
        {
            Name = Name ??
                throw new NameRequiredException();
        }

    }

    [Serializable]
    internal class NameRequiredException : Exception
    {
        public NameRequiredException() { /* ... */ }

        public NameRequiredException(string message) : base(message) { /* ... */ }

        public NameRequiredException(string message, Exception innerException) : base(message, innerException) { /* ... */ }

        protected NameRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { /* ... */ }
    }
}