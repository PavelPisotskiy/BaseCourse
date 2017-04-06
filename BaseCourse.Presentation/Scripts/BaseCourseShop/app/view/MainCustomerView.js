Ext.define('BaseCourseShop.view.MainCustomerView', {
    extend: 'Ext.tab.Panel',
    alias: 'widget.maincustomerview',
    viewModel: 'ordershistoryviewmodel',
    height:'600px',
    items: [{
        title: 'Shop',
        xtype: 'purchaseview'
    }, {
        title: 'History',
        xtype: 'ordershistoryview'
    }]
});