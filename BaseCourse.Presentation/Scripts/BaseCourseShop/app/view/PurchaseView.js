Ext.define('BaseCourseShop.view.PurchaseView', {
    extend: 'Ext.Panel',
    alias: 'widget.purchaseview',
    border: 0,
    controller: 'productcontroller',
    viewModel: 'productviewmodel',
    layout: {
        type: 'hbox',
        align: 'stretch'
    },


    items: [{
        xtype: 'productlist',
        flex: 2,
        margin: '0 5 0 0'
    }, {
        xtype: 'cartview',
        flex: 1,
        margin: '0 0 0 5'
    }]
});