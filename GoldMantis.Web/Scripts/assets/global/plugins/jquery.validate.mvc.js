(function ($, window, document, undefined) {
    var pluginName = "tooltipValidation";
    var defaults = {
    };

   
    function Plugin(element, options) {
        this.element = element;
        this.options = $.extend({}, defaults, options);
        this._defaults = defaults;
        this._name = pluginName;
        this.init();
    }

    Plugin.prototype = {

        init: function () {

            var elem = $(this.element),
                name = elem.attr("name"),
                defaultBorderCss = elem.css('border'),
                tooltipPlacement = this.options.placement;

            if (name) {

                var errValElem = $('span[data-valmsg-for="' + name + '"]');
                errValElem.hide();

                function onDOMSubtreeModified(errValElem) {
                    errValElem.bind("DOMSubtreeModified", function () {
                        elem.tooltip("destroy");
                        elem.siblings("div.tooltip").remove();

                        var isTooltipPluginAttached = elem.data("plugin_tooltipValidation");
                        if (!isTooltipPluginAttached) {
                            elem.popover({
                                'content': errValElem.text(),
                                'container':'body',
                                'animation': 'true',
                                'placement': tooltipPlacement || 'bottom',
                                'trigger': 'focus'
                            });
                        }

                        if (errValElem.hasClass("field-validation-error")) {
                            elem.css('border', '1px solid red');
                            $(elem).closest('.form-group').removeClass("has-success").addClass('has-error');
                        }
                        else {
                            elem.css('border', '');
                            elem.popover('destroy');
                            elem.closest('.form-group').removeClass('has-error'); 
                        }

                    });
                };

                function showTooltipOfError(event) {
                    var inputField = event.currentTarget;
                    if ($(errValElem).hasClass("field-validation-error") || $(errValElem).hasClass("field-error")) {
                        $(inputField).siblings("div.tooltip").remove();

                        $(inputField).popover({
                            'content': $(errValElem).text(),
                            'animation': 'true',
                            'container': 'body',
                            'placement': tooltipPlacement || 'bottom',
                            'trigger': 'focus'
                        });
                        $(inputField).popover('show');
                        $(inputField).closest('.form-group').removeClass("has-success").addClass('has-error');
                    }
                };

                if (elem.is('select')) {
                    elem.on("focus mouseenter", function (event) {
                        showTooltipOfError(event);
                    });
                    elem.on("mouseleave", function (event) {
                        var inputField = event.currentTarget;
                        $(inputField).popover('destroy');
                    });
                    onDOMSubtreeModified(errValElem);
                }
                else if (elem.attr('type') == 'date') { 
                    elem.on("focus mouseenter", function (event) {
                        showTooltipOfError(event);
                    });
                    elem.on("mouseleave", function (event) {
                        var inputField = event.currentTarget;
                        $(inputField).popover('destroy');
                    });
                    elem.on("change", function (event) {
                        var inputField = event.currentTarget;
                        if ($(inputField).val() != "") {
                            elem.css('border', defaultBorderCss);
                            elem.popover('destroy');
                            elem.siblings("div.tooltip").remove();
                        }
                        else
                            elem.css('border', '1px solid red');
                    });
                    onDOMSubtreeModified(errValElem);
                }
                else if (elem.attr('type') == 'file') {
                    elem.on("focus mouseenter", function (event) {
                        showTooltipOfError(event);
                    });
                    elem.on("mouseleave", function (event) {
                        var inputField = event.currentTarget;
                        $(inputField).popover('destroy');
                    });
                    onDOMSubtreeModified(errValElem);
                }
                else {
                    elem.on("focus", function (event) {
                        showTooltipOfError(event);
                    });
                    elem.on("focus mouseenter", function (event) {
                        showTooltipOfError(event);
                    });
                    elem.on("mouseleave", function (event) {
                        var inputField = event.currentTarget;
                        $(inputField).popover('destroy');
                    });
                    onDOMSubtreeModified(errValElem);
                }
            }
        }
    };

    $.fn[pluginName] = function (options) {
        return this.each(function () {
            if (!$.data(this, "plugin_" + pluginName)) {
                $.data(this, "plugin_" + pluginName, new Plugin(this, options));
            }
        });
    };

})(jQuery, window, document);