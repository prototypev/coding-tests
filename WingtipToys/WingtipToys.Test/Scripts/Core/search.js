(function (wingtiptoys, $, undefined) {
    (function (search) {
        
        var searchTimeout;
        
        search.searchProducts = function (categoryName, searchKeywords) {
            var deferred = $.Deferred(), // Create a deferred object to keep track of the "state" of the search operation
                trimmedSearchKeywords = $.trim(searchKeywords);
            
            // Prevent server spam by starting a timeout which will fire off after 500ms.
            clearTimeout(searchTimeout);
            
            if (!trimmedSearchKeywords || trimmedSearchKeywords.length >= 2) {
                // Only attempt to trigger a search if at least 2 non-whitespace characters were input, 
                // or if no characters were input (i.e. search box was cleared)
                searchTimeout = setTimeout(function() {
                    var products = wingtiptoys.fetchProducts(categoryName, trimmedSearchKeywords);

                    products.once('error', deferred.reject);
                    products.once('sync', deferred.resolve);
                }, 500);
            } else {
                // Search not performed
                deferred.reject();
            }

            return deferred;
        };
        
    }(wingtiptoys.search = wingtiptoys.search || {}));
}(window.wingtiptoys = window.wingtiptoys || {}, jQuery));