Ext.define('BaseCourseShop.view.OrdersHistoryView', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.ordershistoryview',
    
    bind: {
        store: '{ordersHistory}'
    },

    columns: [
            { header: 'Order ID', dataIndex: 'orderBusinessId', flex: 1 },
            {
                header: 'Placing date', dataIndex: 'placingDateUtc', flex: 1,
                renderer: Ext.util.Format.dateRenderer('m-d-Y g:i A')
            },
            {
                header: 'Status', dataIndex: 'status', flex: 1,
                renderer: function (status) {
                    switch (status) {
                        case 0: return 'Open';
                        case 1: return 'Processing';
                        case 2: return 'Accepted';
                        case 3: return 'Rejected';
                        default:
                            return 'unknow status';
                    }
                }
            },
            { header: 'Total price', dataIndex: 'totalPrice', flex: 1 },
        ]

});

//Ext.util.Format.dateRenderer('m-d-Y g:i A')