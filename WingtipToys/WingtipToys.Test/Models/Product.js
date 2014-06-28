(function(wingtiptoys, $, undefined) {
    
    wingtiptoys.Product = Backbone.Model.extend({
        defaults: function () {
            return {
                Category: null,
                Description: null,
                ImagePath: null,
                ProductID: -1,
                ProductName: null,
                UnitPrice: 0
            };
        }
    });

    wingtiptoys.ProductList = Backbone.Collection.extend({
        url: '/api/Product/GetProducts',
        model: wingtiptoys.Product
    });
    
    wingtiptoys.ProductView = Backbone.View.extend({
        tagName: 'tr',
        
        template: _.template($('#product-template').html()),

        initialize: function () {
            this.listenTo(this.model, 'change', this.render);
            this.listenTo(this.model, 'destroy', this.remove);
        },

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        }
    });

    wingtiptoys.ProductListView = Backbone.View.extend({
        el: $('table.products'),

        initialize: function () {
            this.listenTo(this.model, 'request', this.renderLoading);
            this.listenTo(this.model, 'add change sync', this.render);
        },

        renderLoading: function() {
            $('table.products > tbody').html('<tr><td>Loading...</td></tr>');
        },
        
        render: function () {
            var $products = $('table.products > tbody').empty();

            if (this.model.length) {
                $.each(this.model.models, function() {
                    var productView = new wingtiptoys.ProductView({ model: this });
                    $products.append(productView.render().el);
                });
            } else {
                $products.append('<tr><td>No products found</td></tr>');
            }
        }
    });
    
}(window.wingtiptoys = window.wingtiptoys || {}, jQuery));