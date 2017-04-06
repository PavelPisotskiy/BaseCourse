Ext.Loader.setConfig({
    enabled: true,
    disableCaching: true
});

Ext.application({
    name: 'BaseCourseShop',
    extend: 'BaseCourseShop.Application',
    


    launch: function () {
        
        Deft.Injector.configure({
            ProductService: {
                className: 'BaseCourseShop.service.ProductService',
                singleton: true
            },
            OrderService: {
                className: 'BaseCourseShop.service.OrderService',
                singleton: true
            }
        });

        Ext.create('BaseCourseShop.view.MainCustomerView', {
            renderTo: Ext.getDom('content')
        });
    }
});