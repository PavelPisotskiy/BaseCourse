Ext.define('BaseCourseShop.model.OrderItem', {
    extend: 'Ext.data.Model',
    idProperty: 'productBusinessId',

    fields: [
        { name: 'orderBusinessId', type: 'string' },
        { name: 'productBusinessId', type: 'string' },
        { name: 'quantity', type: 'number' },
        { name: 'productName', type: 'string' },
        { name: 'productUnits', type: 'string' },
        { name: 'productPrice', type: 'number' }
    ]
});