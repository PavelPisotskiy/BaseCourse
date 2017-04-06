Ext.define('BaseCourseShop.model.Order', {
    extend: 'Ext.data.Model',
    idProperty: 'orderBusinessId',

    hasMany: {
        model: 'BaseCourseShop.model.OrderItem',
        name: 'orderItems',
        associationKey: 'orderItems'
    },

    schema: {
        namespace: 'BaseCourseShop.model',
        proxy: {
            type: 'ajax',
            url: orderUrls.GetCart,
            autoLoad: false,
            reader: {
                type: 'json',
                rootProperty: 'cart',
                successProperty: 'success'
            }
        },
    },

    fields: [
        { name: 'customerId', type: 'number' },
        { name: 'orderBusinessId', type: 'string' },
        { name: 'placingDateUtc', type: 'date' },
        { name: 'status', type: 'number' },
        { name: 'totalPrice', type: 'float' },
    ]
});