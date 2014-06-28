(function (wingtiptoys, $, undefined) {
    
    var products = new wingtiptoys.ProductList();

    wingtiptoys.fetchProducts = function(categoryName, searchKeywords) {
        products.fetch({
            data: {
                categoryName: $.trim(categoryName),
                searchKeywords: $.trim(searchKeywords)
            }
        });

        return products;
    };

}(window.wingtiptoys = window.wingtiptoys || {}, jQuery));