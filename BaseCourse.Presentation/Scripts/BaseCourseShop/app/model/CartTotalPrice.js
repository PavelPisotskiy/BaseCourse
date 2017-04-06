Ext.define('BaseCourseShop.model.CartTotalPrice', {
    extend: 'Ext.data.Model',

    proxy: {
        type: 'ajax',
        url: orderUrls.GetCartTotalPrice,
        autoLoad: false,
        reader: {
            type: 'json',
            rootProperty: '',
        }
    },

    fields: [
        { name: 'totalPrice', type: 'float' },
    ]
});