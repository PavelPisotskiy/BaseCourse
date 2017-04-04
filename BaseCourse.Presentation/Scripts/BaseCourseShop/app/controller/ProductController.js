Ext.define('BaseCourseShop.controller.ProductController', {
    extend: 'Ext.app.ViewController',

    alias: 'controller.productcontroller',

    mixins: ['Deft.mixin.Injectable'],
    inject: ['ProductService'],

    config: {
        ProductService: null
    },

    initViewModel: function (model) {
        this.loadProductList();
        //       this.loadCart();
        
    },

    loadProductList: function () {
        var model = this.getViewModel();
        var product = model.get('products');
        product.getProxy();
        product.reload();
    },

    loadCart: function(){
        var cart = this.getViewModel().get('cart');
        cart.load();
        //var me = this;
        //var cart = this.getViewModel().get('cart');
        //cart.getProxy();
        //cart.load();
       // cart.getProxy();
       // me.lookupReference('panelCart').getView().getStore().removeAll();
        //cart.load({
        //    scope: this,
        //    failure: function (record, operation) {
        //        // do something if the load failed
        //    },
        //    success: function (record, operation) {
        //        //me.lookupReference('panelCart').getView().refresh();
        //        console.log('231');

        //        //var me = this;
        //        var orderItems = record.orderItems();
        //        //for (var i = 0; i < orderItems.count() ; i++) {
        //        //    var foundOrderItem = orderItems.getAt(i);
        //        //    var productPromise = this.getProductService().getProductById(foundOrderItem.get('productBusinessId'));
        //        //    productPromise.then(
        //        //    function (productJson) {
        //        //        var productObj = JSON.parse(productJson);

        //        //        var product = Ext.create('BaseCourseShop.model.Product', {
        //        //            productBusinessId: productObj.productBusinessId,
        //        //            name: productObj.name,
        //        //            units:  productObj.units,
        //        //            price: productObj.price,
        //        //        });
                        
        //        //        foundOrderItem.set('productName', product.get('name'));
        //        //        foundOrderItem.set('productUnits', product.get('units'));
        //        //        foundOrderItem.set('productPrice', product.get('price'));
        //        //    },
        //        //    function (error) {
        //        //        var resp = JSON.parse(error);
        //        //        Ext.Msg.alert('Error Message', resp.message);
        //        //    });
        //        //}
        //    },
        //    callback: function (record, operation, success) {
        //        // do something whether the load succeeded or failed
        //    }
        //});

        console.log('s');
    },

    

    onAddOneProductToCart: function (btn) {
        var me = this;
        var productBusinessId = btn.getWidgetRecord().get('productBusinessId');
        var promise = this.getProductService().AddToCart(productBusinessId);
        promise.then(
                    function (json) {
                        me.loadCart();
                    },
                    function (error) {
                        var resp = JSON.parse(error);
                        Ext.Msg.alert('Error Message', resp.message);
                    });
        //var cart = this.getViewModel().get('cart');
        //var product = cart.get('OrderItems');
        //var productItem = product.getById(productBusinessId);

        //var promise = this.getOrderService().UpdateCart(productBusinessId);
        //promise.then(function (response) {

        //    var resp = JSON.parse(response);
        //    if (!resp.success) {
        //        Ext.Msg.alert('Error Message', resp.message);
        //    }

        //    me.loadCart();
        //});
    }
});