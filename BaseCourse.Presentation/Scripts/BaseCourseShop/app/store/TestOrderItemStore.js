Ext.define('BaseCourseShop.store.TestOrderItemStore', {
    extend: 'Ext.data.ArrayStore',
    model: 'BaseCourseShop.model.OrderItem',
    alias: 'store.testorderitemstore',
    expandData: true
});