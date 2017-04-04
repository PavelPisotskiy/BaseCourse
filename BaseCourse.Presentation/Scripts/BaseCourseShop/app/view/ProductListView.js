Ext.define('BaseCourseShop.view.ProductListView', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.productlist',
    title: 'Products',


    bind: {
        store: '{products}'
    },

    initComponent: function () {
        this.columns = [
            { header: 'Product ID', dataIndex: 'productBusinessId', flex: 1 },
            { header: 'Name', dataIndex: 'name', flex: 1 },
            { header: 'Units', dataIndex: 'units', flex: 1 },
            { header: 'Price', dataIndex: 'price', flex: 1 },
            {
                xtype: 'widgetcolumn',
                widget: {
                    xtype: 'button',
                    flex: 1,
                    text: 'Add to cart',
                    padding: 5,
                    handler: 'onAddOneProductToCart'
                }
            }
        ];

        this.callParent(arguments);
    }

});