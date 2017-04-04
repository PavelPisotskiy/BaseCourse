Ext.define('BaseCourseShop.viewmodel.ProductViewModel', {
    extend: 'Ext.app.ViewModel',

    alias: 'viewmodel.productviewmodel',
    stores: {
        products: {
            type: 'productstore'
        }
    },
    links: {
        cart: {
            type: 'BaseCourseShop.model.Order',
            create: true
        }
    },

    formulas: {
        totalPrice: {
            bind: '{cart.totalPrice}',
            get: function (totalPrice) {
                return 'Total price: ' + totalPrice;
            }
        }
    }
});