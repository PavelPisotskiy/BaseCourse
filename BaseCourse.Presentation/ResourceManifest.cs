﻿using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaseCourse.Presentation
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineScript("extJs").SetUrl("extJs/ext-all-debug.js");
            manifest.DefineScript("deftjs").SetUrl("deftjs/deft-debug.js");

            manifest.DefineStyle("styles")
                .SetUrl("Styles/Style.css");

            manifest.DefineStyle("extJsStyles").SetUrl("Styles/theme-classic-all.css");

            //Models
            manifest.DefineScript("productModel")
                .SetUrl("BaseCourseShop/app/model/Product.js");
            manifest.DefineScript("orderItemModel")
                .SetUrl("BaseCourseShop/app/model/OrderItem.js");
            manifest.DefineScript("orderModel")
                .SetUrl("BaseCourseShop/app/model/Order.js");

            //Stores
            manifest.DefineScript("productStore")
                .SetUrl("BaseCourseShop/app/store/ProductStore.js");

            //ViewModel
            manifest.DefineScript("productViewModel").SetUrl("BaseCourseShop/app/viewmodel/ProductViewModel.js");

            //View
            manifest.DefineScript("productListView")
                .SetUrl("BaseCourseShop/app/view/ProductListView.js");
            manifest.DefineScript("cartView")
                .SetUrl("BaseCourseShop/app/view/CartView.js");
            manifest.DefineScript("purchaseView")
                .SetUrl("BaseCourseShop/app/view/PurchaseView.js");


            //Services
            manifest.DefineScript("productService").SetUrl("BaseCourseShop/app/service/ProductService.js");

            //Application
            manifest.DefineScript("application").SetUrl("BaseCourseShop/app/Application.js");

            //Controllers
            manifest.DefineScript("productController").SetUrl("BaseCourseShop/app/controller/ProductController.js");

            manifest.DefineScript("app")
                .SetUrl("BaseCourseShop/app/app.js")
                .SetDependencies("extJs",
                                  "deftjs",

                                 "productModel",
                                 "orderItemModel",
                                 "orderModel",

                                 "productStore",

                                 "productViewModel",

                                 "productListView",
                                 "cartView",
                                 "purchaseView",

                                 "productController",

                                 "productService",

                                 "application");
        }
    }
}