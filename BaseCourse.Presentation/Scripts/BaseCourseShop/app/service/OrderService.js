Ext.define('BaseCourseShop.service.OrderService', {

    alias: 'service.orderservice',

    
    AddToCart: function (productBusinessId) {
        return new Ext.Promise(function (resolve, reject) {
            Ext.Ajax.request({
                url: orderUrls.AddToCart,
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                params: '"' + productBusinessId + '"',
                success: function (response) {
                    resolve(response.responseText);
                },
                failure: function (response) {
                    reject(response.responseText);
                }
            });
        });
    },
    RemoveFromCart: function (productBusinessId) {
        return new Ext.Promise(function (resolve, reject) {
            Ext.Ajax.request({
                url: orderUrls.RemoveFromCart,
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                params: '"' + productBusinessId + '"',
                success: function (response) {
                    resolve(response.responseText);
                },
                failure: function (response) {
                    reject(response.responseText);
                }
            });
        });
    },

    SetProductQuantity: function (productId, productQuantity) {
        return new Ext.Promise(function (resolve, reject) {
            Ext.Ajax.request({
                url: orderUrls.SetProductQuantity,
                method: 'POST',
                jsonData: {
                    productBusinessId: productId,
                    quantity: productQuantity
                },
                success: function (response) {
                    resolve(response.responseText);
                },
                failure: function (response) {
                    reject(response.responseText);
                }
            });
        });
    },

    Checkout: function (orderBusinessId) {
        return new Ext.Promise(function (resolve, reject) {
            Ext.Ajax.request({
                url: orderUrls.Checkout,
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                params: '"' + orderBusinessId + '"',
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