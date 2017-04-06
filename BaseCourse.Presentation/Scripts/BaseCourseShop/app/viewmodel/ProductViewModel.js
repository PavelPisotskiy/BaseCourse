Ext.define('BaseCourseShop.viewmodel.ProductViewModel', {
    extend: 'Ext.app.ViewModel',

    alias: 'viewmodel.productviewmodel',
    stores: {
        products: {
            type: 'productstore'
        },
        orderItems: {
            type: 'orderitemstore'
        }
    },
    links: {
        cart: {
            type: 'BaseCourseShop.model.Order',
            create: true
        },
        cartTotalPrice: {
            type: 'BaseCourseShop.model.CartTotalPrice',
            create: true
        }
    }

});