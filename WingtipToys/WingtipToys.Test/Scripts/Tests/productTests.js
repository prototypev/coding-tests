(function () {
    module("Product Tests");

    asyncTest('Test fetchProducts', function () {
        expect(1);

        var products = wingtiptoys.fetchProducts();
        
        products.once('error', function () {
            ok(false, 'Products failed to load unexpectedly.');
            start();
        });
        
        products.once('sync', function () {
            ok(true, 'Products loaded fine as expected.');
            start();
        });
    });
})();