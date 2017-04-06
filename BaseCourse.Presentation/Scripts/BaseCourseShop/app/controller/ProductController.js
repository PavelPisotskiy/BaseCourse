Ext.define('BaseCourseShop.controller.ProductController', {
    extend: 'Ext.app.ViewController',

    alias: 'controller.productcontroller',

    mixins: ['Deft.mixin.Injectable'],
    inject: ['ProductService', 'OrderService'],

    config: {
        ProductService: null,
        OrderService: null
    },

    initViewModel: function (model) {
        this.loadProductList();
        this.loadOrderItems();
        this.loadCartTotalPrice();
    },

    loadCartTotalPrice: function () {
        var model = this.getViewModel();
        var product = model.get('cartTotalPrice');
        product.getProxy();
        product.load();
    },

    loadProductList: function () {
        var model = this.getViewModel();
        var product = model.get('products');
        product.getProxy();
        product.reload();
    },

    loadOrderItems: function () {
        var me = this;
        var model = this.getViewModel();
        var product = model.get('orderItems');
        product.getProxy();
        product.reload({
            callback: function (records, options, success) {
                if (success) {
                    Ext.each(records, function (value) {
                        var productPromise = me.getProductService().getProductById(value.get('productBusinessId'));
                        productPromise.then(
                            function (productJson) {
                                var productObj = new BaseCourseShop.model.Product(JSON.parse(productJson));
                                value.set('product', productObj);
                                value.commit();
                            },
                            function (error) {
                                var resp = JSON.parse(error);
                                Ext.Msg.alert('Error Message', resp.message);
                            });
                    });
                }
            }
        });
    },

    onAddOneProductToCart: function (btn) {
        var me = this;
        var productBusinessId = btn.getWidgetRecord().get('productBusinessId');
        var promise = this.getOrderService().AddToCart(productBusinessId);
        promise.then(
                    function (json) {
                        me.loadOrderItems();
                        me.loadCartTotalPrice();
                    },
                    function (error) {
                        var resp = JSON.parse(error);
                        Ext.Msg.alert('Error Message', resp.message);
                    });
    },

    removeProductFromCart: function (view, rowIndex, colIndex, item, e, record, row) {
        var me = this;
        var promise = this.getOrderService().RemoveFromCart(record.get('productBusinessId'));
        promise.then(
                    function (json) {
                        me.loadOrderItems();
                        me.loadCartTotalPrice();
                    },
                    function (error) {
                        var resp = JSON.parse(error);
                        Ext.Msg.alert('Error Message', resp.message);
                    });
    },

    incrementProductQuantity: function (view, rowIndex, colIndex, item, e, record, row) {
        var me = this;
        var promise = this.getOrderService().SetProductQuantity(record.get('productBusinessId'), record.get('quantity') + 1);
        promise.then(
                    function (json) {
                        me.loadOrderItems();
                        me.loadCartTotalPrice();
                    },
                    function (error) {
                        var resp = JSON.parse(error);
                        Ext.Msg.alert('Error Message', resp.message);
                    });
    },

    decrementProductQuantity: function (view, rowIndex, colIndex, item, e, record, row) {
        var me = this;
        if (record.get('quantity') > 1) {
            var promise = this.getOrderService().SetProductQuantity(record.get('productBusinessId'), record.get('quantity') - 1);
            promise.then(
                        function (json) {
                            me.loadOrderItems();
                            me.loadCartTotalPrice();
                        },
                        function (error) {
                            var resp = JSON.parse(error);
                            Ext.Msg.alert('Error Message', resp.message);
                        });
        }
    },

    checkout: function () {
        var me = this;
        var model = this.getViewModel();
        var cart = model.get('cart');
        cart.load({
            scope: this,
            callback: function (record, operation, success) {
                if (success) {
                    var orderId = record.get('orderBusinessId');
                    var promise = me.getOrderService().Checkout(orderId);
                    promise.then(
                                function (json) {
                                    me.loadOrderItems();
                                    me.loadCartTotalPrice();
                                },
                                function (error) {
                                    var resp = JSON.parse(error);
                                    Ext.Msg.alert('Error Message', resp.message);
                                });
                } else {
                    console.log('error');
                }
            }
        });
        
    },
});