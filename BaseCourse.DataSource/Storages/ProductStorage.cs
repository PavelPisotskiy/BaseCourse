using BaseCourse.DataSource.Models;
using BaseCourse.DataSource.Mappers;
using BaseCourse.Domain.Models.Entities;
using BaseCourse.Domain.Storages;
using Orchard.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Storages
{
    public class ProductStorage : IProductStorage
    {
        private readonly IRepository<ProductRecord> productRecordRepository;
        private readonly IRepository<ProductPriceRecord> productPriceRecordRepository;
        private readonly ProductMapper productMapper;
        private readonly ProductPriceMapper productPriceMapper;

        public ProductStorage(IRepository<ProductRecord> productRecordRepository, IRepository<ProductPriceRecord> productPriceRecordRepository)
        {
            this.productRecordRepository = productRecordRepository;
            this.productPriceRecordRepository = productPriceRecordRepository;
            this.productMapper = new ProductMapper();
            this.productPriceMapper = new ProductPriceMapper();
        }

        public Product GetByBusinessId(string businessId)
        {
            ProductRecord productRecord = productRecordRepository.Table.FirstOrDefault(pr => pr.ProductBusinessId.Equals(businessId));
            IEnumerable<ProductPriceRecord> productPriceRecords = productPriceRecordRepository.Table.Where(pp => pp.ProductBusinessId.Equals(businessId));
            return productMapper.GetProduct(productRecord, productPriceRecords);
        }

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<ProductRecord> productRecords = productRecordRepository.Table;
            List<Product> products = new List<Product>();
            foreach (ProductRecord productRecord in productRecords)
            {
                IEnumerable<ProductPriceRecord> productPriceRecords = productPriceRecordRepository.Table.Where(pp => pp.ProductBusinessId.Equals(productRecord.ProductBusinessId));
                Product product = productMapper.GetProduct(productRecord, productPriceRecords);
                products.Add(product);
            }

            return products;
        }

        public void Create(Product product)
        {
            ProductRecord productRecord = productMapper.GetProductRecord(0, product);
            productRecordRepository.Create(productRecord);
            IEnumerable<ProductPriceRecord> productPriceRecordsForAdding = product.Prices.Select(productPrice => productPriceMapper.GetProductPriceRecord(0, productPrice));
            foreach (var productPriceRecord in productPriceRecordsForAdding)
            {
                productPriceRecordRepository.Create(productPriceRecord);
            }
        }

        public void Update(Product product)
        {
            ProductRecord productRecord = productRecordRepository.Table.FirstOrDefault(p => p.ProductBusinessId.Equals(product.ProductBusinessId));
            productRecord.Name = product.Name;
            productRecord.Units = product.Units;
            productRecordRepository.Update(productRecord);

            IEnumerable<ProductPriceRecord> existingProductPriceRecords = productPriceRecordRepository
                .Table
                .Where(productPriceRecord => productPriceRecord.ProductBusinessId.Equals(productRecord.ProductBusinessId));

            IEnumerable<ProductPriceRecord> productPriceRecordsForAdding = product.Prices.Select(productPrice => productPriceMapper.GetProductPriceRecord(0, productPrice))
                .Where(productPriceRecord => !existingProductPriceRecords
                    .Any(existingProductPriceRecord => existingProductPriceRecord.ProductBusinessId.Equals(productPriceRecord.ProductBusinessId)))
                .ToList();

            foreach (var productPriceRecord in productPriceRecordsForAdding)
            {
                productPriceRecordRepository.Create(productPriceRecord);
            }
        }
    }
}