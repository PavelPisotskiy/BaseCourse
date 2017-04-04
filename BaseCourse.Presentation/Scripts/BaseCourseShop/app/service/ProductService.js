Ext.define('BaseCourseShop.service.ProductService', {

    alias: 'service.productservice',

    getProductById: function (productBusinessId) {
        
        return new Ext.Promise(function (resolve, reject) {
            Ext.Ajax.request({
                url: productUrls.GetGetProductById + '/' + productBusinessId,
                method: 'GET',
                success: function (response) {
                    resolve(response.responseText);
                },
                failure: function (response) {
                    reject(response.responseText);
                }
            });
        });
    },
    AddToCart: function (productBusinessId) {

        return new Ext.Promise(function (resolve, reject) {
            Ext.Ajax.request({
                url: orderUrls.AddToCart,
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                params: '"'+productBusinessId+'"',
                success: function (response) {
                    resolve(response.responseText);
                },
                failure: function (response) {
                    reject(response.responseText);
                }
            });
        });
    },
});