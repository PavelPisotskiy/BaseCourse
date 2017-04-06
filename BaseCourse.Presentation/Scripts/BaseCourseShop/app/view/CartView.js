Ext.define('BaseCourseShop.view.CartView', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.cartview',
    title: 'Cart',


    mixins: ['Deft.mixin.Injectable'],
    inject: ['ProductService'],

    config: {
        ProductService: null
    },

    bind:'{orderItems}',
    bbar: [ 
    {
        xtype: 'label',
        bind: 'Total price: {cartTotalPrice.totalPrice}',
    },
    '->',
    {
        xtype: 'button',
        text: 'Checkout',
        cls: 'button button-small',
        handler: 'checkout'
    }],

    columns: [
            {
                header: 'Name',
                dataIndex: 'product',
                flex: 1,
                renderer: function (val, meta, rec) {
                    if (val) {
                        return val.get('name');
                    }
                    return '';
                }
            },
            {
                header: 'Price',
                dataIndex: 'product',
                flex: 1,
                renderer: function (val, meta, rec) {
                    if (val) {
                        return val.get('price');
                    }
                    return '';
                }
            },
            {
                xtype: 'actioncolumn',
                width: 25,
                align: 'center',
                sortable: false,
                items: [
                {
                    tooltip: 'Decrement',
                    icon: '../Modules/BaseCourse.Presentation/Styles/images/dd/minus.png',
                    handler: 'decrementProductQuantity'
                }]
            },
            {
                header: 'Quantity',
                align: 'center',
                dataIndex: 'quantity',
                flex: 1
            },
            {
                xtype: 'actioncolumn',
                width: 25,
                align: 'center',
                items: [
                {
                    tooltip:'Increment',
                    icon: '../Modules/BaseCourse.Presentation/Styles/images/dd/plus.png',
                    handler: 'incrementProductQuantity'
                }]
            },
            {
                header: 'Units',
                dataIndex: 'product',
                flex: 1,
                renderer: function (val, meta, rec) {
                    if (val) {
                        return val.get('units');
                    }
                    return '';
                }
            },
            {
                xtype: 'actioncolumn',
                width: 25,
                align: 'center',
                items: [
                {
                    
                    tooltip:'Remove from cart',
                    icon: '../Modules/BaseCourse.Presentation/Styles/images/dd/delete.png',
                    handler: 'removeProductFromCart'
                }]
            }
    ],
});