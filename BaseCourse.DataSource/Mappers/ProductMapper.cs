using BaseCourse.DataSource.Models;
using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Mappers
{
    internal class ProductMapper
    {
        private readonly ProductPriceMapper productPriceMapper;

        public ProductMapper()
        {
            productPriceMapper = new ProductPriceMapper();
        }

        public Product GetProduct(ProductRecord productRecord, IEnumerable<ProductPriceRecord> productPriceRecords)
        {
            IEnumerable<ProductPrice> productPrices = productPriceRecords.Select(productPriceRecord => productPriceMapper.GetProductPrice(productPriceRecord));
            Product product = new Product(productRecord.ProductBusinessId, productRecord.Name, productRecord.Units, productPrices);
            return product;
        }

        public ProductRecord GetProductRecord(long id, Product product)
        {
            ProductRecord record = new ProductRecord()
            {
                Id = id,
                Name = product.Name,
                ProductBusinessId = product.ProductBusinessId,
                Units = product.Units
            };
            return record;
        }
    }
}