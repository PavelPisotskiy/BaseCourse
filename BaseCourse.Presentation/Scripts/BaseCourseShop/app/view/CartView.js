Ext.define('BaseCourseShop.view.CartView', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.cartview',
    title: 'Cart',


    mixins: ['Deft.mixin.Injectable'],
    inject: ['ProductService'],

    config: {
        ProductService: null
    },

    bind:'{cart.orderItems}',
    reference: 'panelCart',
    bbar: [ 
    {
        xtype: 'label',
        bind: '{totalPrice}',
    }, '->', {
        xtype: 'button',
        text: 'Checkout',
        cls: 'button button-small'
    }],

    columns: [
            { header: 'Name', dataIndex: 'productName', flex: 1 },
            { header: 'Price', dataIndex: 'productPrice', flex: 1 },
            { header: 'Quantity', dataIndex: 'quantity', flex: 1 },
            {
                header: 'Units',
                dataIndex: 'productUnits',
                flex: 1
            },
    ],
});