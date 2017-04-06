Ext.define('BaseCourseShop.store.OrderItemStore', {
    extend: 'Ext.data.Store',
    model: 'BaseCourseShop.model.OrderItem',
    alias: 'store.orderitemstore',
    storeId: 'OrderItemStore',
    proxy: {
        type: 'ajax',
        url: orderUrls.GetCart,
        reader: {
            type: 'json',
            rootProperty: 'cart.orderItems',
            successProperty: 'success'
        }
    },
    autoLoad: false
});