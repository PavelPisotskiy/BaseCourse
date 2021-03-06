using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace BaseCourse.DataSource
{
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("OrderRecord", 
                table => table
                    .Column<long>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("CustomerId", column => column.NotNull())
                    .Column<string>("OrderBusinessId", column => column.NotNull().WithLength(36))
                    .Column<DateTime>("PlacingDateUtc", column => column.NotNull())
                    .Column<int>("Status", column => column.NotNull())
                    .Column<double>("TotalPrice", column => column.NotNull())
                    );

            SchemaBuilder.CreateTable("OrderItemRecord",
                table => table
                    .Column<long>("Id", column => column.PrimaryKey().Identity())
                    .Column<long>("OrderId", column => column.NotNull())
                    .Column<long>("ProductId", column => column.NotNull())
                    .Column<string>("OrderBusinessId", column => column.NotNull().WithLength(36))
                    .Column<string>("ProductBusinessId", column => column.NotNull().WithLength(36))
                    .Column<int>("Quantity", column => column.NotNull())
                    );

            SchemaBuilder.CreateTable("ProductRecord",
                table => table
                    .Column<long>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("ProductBusinessId", column => column.NotNull().WithLength(36))
                    .Column<string>("Name", column => column.NotNull().WithLength(50))
                    .Column<string>("Units", column => column.NotNull().WithLength(25))
                    );

            SchemaBuilder.CreateTable("ProductPriceRecord",
                table => table
                    .Column<long>("Id", column => column.PrimaryKey().Identity())
                    .Column<long>("ProductId", column => column.NotNull())
                    .Column<string>("ProductBusinessId", column => column.NotNull().WithLength(36))
                    .Column<double>("Price", column => column.NotNull())
                    .Column<DateTime>("EffectiveDateUtc", column => column.NotNull())
                    );

            SchemaBuilder.CreateForeignKey("FK_ProductPriceRecord_ProductRecord",
                "ProductPriceRecord", new string[] { "ProductId" },
                "ProductRecord", new string[] { "Id" }
                );

            SchemaBuilder.CreateForeignKey("FK_ProductRecord_OrderItemRecord",
                "OrderItemRecord", new string[] { "ProductId" },
                "ProductRecord", new string[] { "Id" }
                );

            SchemaBuilder.CreateForeignKey("FK_OrderRecord_OrderItemRecord",
                "OrderItemRecord", new string[] { "OrderId" },
                "OrderRecord", new string[] { "Id" }
                );

            return 1;
        }
    }
}