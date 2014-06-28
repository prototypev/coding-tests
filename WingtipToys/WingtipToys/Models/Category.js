(function(wingtiptoys, $, undefined) {

    wingtiptoys.Category = Backbone.Model.extend({
        defaults: function() {
            return {
                CategoryID: -1,
                CategoryName: null,
                Description: null
            };
        }
    });

}(window.wingtiptoys = window.wingtiptoys || {}, jQuery));