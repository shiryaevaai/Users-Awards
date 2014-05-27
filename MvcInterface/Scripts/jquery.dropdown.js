/*
    AUTHOR:  TechDoba, LLC
    LICENSE: Common Development and Distribution License (CDDL-1.0)
*/
(function ($) {
    $.fn.selected = function (fn) {
        return this.each(function () {
            var clicknum = 0;
            $(this).click(function () {
                clicknum++;
                if (clicknum == 2) {
                    clicknum = 0;
                    fn(this);
                }
            });
        });
    }
})(jQuery);