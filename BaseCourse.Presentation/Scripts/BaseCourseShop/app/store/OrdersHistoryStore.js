Ext.define('BaseCourseShop.store.OrdersHistoryStore', {
    extend: 'Ext.data.Store',
    model: 'BaseCourseShop.model.Order',
    alias: 'store.ordershistorystore',
    storeId: 'OrdersHistoryStore',
    proxy: {
        type: 'ajax',
        url: orderUrls.GetCurrentCustomerOrders,
        reader: {
            type: 'json',
            rootProperty: 'orders',
            successProperty: 'success'
        }
    },
    autoLoad: true
});