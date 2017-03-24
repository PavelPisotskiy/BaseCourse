using BaseCourse.DataSource.Models;
using BaseCourse.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.DataSource.Mappers
{
    internal class ProductPriceMapper
    {
        public ProductPrice GetProductPrice(ProductPriceRecord productPriceRecord)
        {
            return new ProductPrice(productPriceRecord.ProductBusinessId, productPriceRecord.Price, productPriceRecord.EffectiveDateUtc);
        }

        public ProductPriceRecord GetProductPriceRecord(long id, ProductPrice productPrice)
        {
            ProductPriceRecord record = new ProductPriceRecord()
            {
                Id = id,
                ProductBusinessId = productPrice.ProductBusinessId,
                EffectiveDateUtc = productPrice.EffectiveDateUtc,
                Price = productPrice.Price
            };

            return record;
        }
    }
}