Ext.define('BaseCourseShop.store.ProductStore', {
    extend: 'Ext.data.Store',
    model: 'BaseCourseShop.model.Product',
    alias: 'store.productstore',
    storeId: 'ProductStore',
    proxy: {
        type: 'ajax',
        url: productUrls.GetProducts,
        reader: {
            type: 'json',
            rootProperty: 'products',
            successProperty: 'success'
        }
        
    },
    autoLoad: false
});