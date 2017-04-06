Ext.define('BaseCourseShop.model.OrderItem', {
    extend: 'Ext.data.Model',
    idProperty: 'productBusinessId',

    fields: [
        { name: 'orderBusinessId', type: 'string' },
        { name: 'productBusinessId', type: 'string' },
        { name: 'quantity', type: 'number' },
        {
            name: 'product',
            reference: {
                type: 'BaseCourseShop.model.Product',
                unique: true
            }
        }
        //{ name: 'productName', type: 'string' },
        //{ name: 'productUnits', type: 'string' },
        //{ name: 'productPrice', type: 'number' }
    ]
});