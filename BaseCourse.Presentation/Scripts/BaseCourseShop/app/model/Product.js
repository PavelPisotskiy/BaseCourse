Ext.define('BaseCourseShop.model.Product', {
    extend: 'Ext.data.Model',
    idProperty: 'productBusinessId',
    fields: [
        { name: 'productBusinessId', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'units', type: 'string' },
        { name: 'price', type: 'number' }
    ]
});