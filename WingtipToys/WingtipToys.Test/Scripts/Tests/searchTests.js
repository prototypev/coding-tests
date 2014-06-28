(function () {
    module("Search Tests");

    asyncTest('Test with no keywords', function () {
        expect(1);

        wingtiptoys.search.searchProducts()
            .done(function() {
                ok(true, 'Search ran as expected.');
                start();
            })
            .fail(function() {
                ok(false, 'Search failed unexpectedly.');
                start();
            });
    });
    
    asyncTest('Test with invalid keywords (1 character after trimming whitespace)', function () {
        expect(1);

        wingtiptoys.search.searchProducts(null, 'a')
            .done(function () {
                ok(false, 'Search ran unexpectedly.');
                start();
            })
            .fail(function () {
                ok(true, 'Search failed as expected.');
                start();
            });
    });
    
    asyncTest('Test whitespace trimming', function () {
        expect(1);

        wingtiptoys.search.searchProducts(null, ' a ')
            .done(function () {
                ok(false, 'Search ran unexpectedly.');
                start();
            })
            .fail(function () {
                ok(true, 'Search failed as expected.');
                start();
            });
    });

    asyncTest('Test with valid keywords (2 or more characters after trimming whitespace)', function () {
        expect(1);

        wingtiptoys.search.searchProducts(null, 'ab')
            .done(function () {
                ok(true, 'Search ran as expected.');
                start();
            })
            .fail(function () {
                ok(false, 'Search failed unexpectedly.');
                start();
            });
    });
})();