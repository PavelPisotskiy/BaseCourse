Ext.define('BaseCourseShop.viewmodel.OrdersHistoryViewModel', {
    extend: 'Ext.app.ViewModel',

    alias: 'viewmodel.ordershistoryviewmodel',
    stores: {
        ordersHistory: {
            type: 'ordershistorystore'
        }
    }

});