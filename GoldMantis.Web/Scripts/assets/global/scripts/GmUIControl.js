var GmUIControl = function () {
    var pagerControls = {};
    var selectControls = {};
    var gridTable = {};

    return {
        init: function() {

        },

        PagerControl: function (id) {
            if (pagerControls[id]) {
                return pagerControls[id];
            }

            var jumptoId = '#' + id + '_gmPagination_jumpto';
            var totalCountId = '#' + id + '_gmPagination_paginTotalCount';
            var totalPageId = '#' + id + '_gmPagination_paginTotalPages';
            var pageIndexId = '#' + id + '_gmPagination_pageIndex';
            var pageSizeId = '#' + id + '_gmPagination_paginPageSize';

            var pageSizeChange = function () { $("form").submit(); };

            var getPage = function() {
                var $totalPage = $(totalPageId);
                var $jumpto = $(jumptoId);
                var $pageIndex = $(pageIndexId);

                var totalPages = parseInt($totalPage.val());
                var pageIndex = parseInt($jumpto.val());

                if (pageIndex > totalPages && totalPages > 0) {
                    $pageIndex.val(totalPages - 1);
                } else if (pageIndex <= totalPages && totalPages > 0) {
                    $pageIndex.val(pageIndex - 1);
                }

                pageSizeChange();
            };

            var navigation = function(sender) {
                var $totalPage = $(totalPageId);
                var $jumpto = $(jumptoId);

                var totalPages = parseInt($totalPage.val());
                var pageIndex = parseInt($jumpto.val());

                if ($(sender).hasClass("page-prev") && (!$(sender).parent().hasClass("disabled"))) {
                    $jumpto.val(pageIndex - 1);
                    getPage();
                } else if ($(sender).hasClass("page-next") && (!$(sender).parent().hasClass("disabled"))) {
                    $jumpto.val(pageIndex + 1);
                    getPage();
                } else if ($(sender).hasClass("page-first") && (!$(sender).parent().hasClass("disabled"))) {
                    $jumpto.val(1);
                    getPage();
                } else if ($(sender).hasClass("page-last") && (!$(sender).parent().hasClass("disabled"))) {
                    $jumpto.val(totalPages);
                    getPage();
                }

                return false;
            };

            var keypress = function(e) {
                if (e.keyCode === 13) {
                    getPage();
                }
            };


            pagerControls[id] = {
                'getPage': getPage,
                'navigation': navigation,
                'keypress': keypress,
                'pageSizeChange': pageSizeChange
            };

            return pagerControls[id];
        },

        SelectControl: function (id) {
            if (selectControls[id]) {
                return selectControls[id];
            }

            var valueInputId = id;
            var textInputId = id + "_Text";
            var clearSpanId = id + "_Clear";
            var selectSpanId = id + "_Select";
            var containerDivId = id + "_Container";
            var wrapDivId = id + "_Wrap";

            var callBack;

            var valueInput = function() {
                return $("#" + valueInputId);
            };

            var textInput = function () {
                return $("#" + textInputId);
            };

            var clearSpan = function () {
                return $("#" + clearSpanId);
            };

            var selectSpan = function () {
                return $("#" + selectSpanId);
            };

            var containerDiv = function () {
                return $("#" + containerDivId);
            };

            var wrapDiv = function () {
                return $("#" + wrapDivId);
            };

            var value = function (val) {
                if (arguments.length === 0) {
                    return valueInput().val();
                }

                valueInput().val(val);
            };

            var text = function(val) {
                if (arguments.length === 0) {
                    return textInput().val();
                }

                textInput().val(val);
            };

            var selectingAction = function (selectOption, funCallback) {
                if (jQuery.isFunction(callBack)) {
                    callBack = funCallback;
                }

                window.top.bootstrapGM.dialog(selectOption, funCallback);
            };

            var selectedAction = function (obj) {
                var text = '', value = '';

                if (jQuery.isArray(obj) && obj.length > 0) {
                    for (var i = 0; i < obj.length; i++) {
                        text += obj[i].Text + ',';
                        value += obj[i].Value + ',';
                    }
                }

                if (text.length > 0) {
                    this.text(text.substr(0, text.length - 1));
                }

                if (value.length > 0) {
                    this.value(value.substr(0, value.length - 1));
                }

                if (jQuery.isFunction(callBack)) {
                    callBack(obj);
                }
            }

            var clearAction = function() {
                value('');
                text('');
            };

            var openLinkView = function(url) {
                window.top.bootstrapGM.dialog({ 'url': url }, null);
            };

            var returnObj = {
                'valueInput': valueInput,
                'textInput': textInput,
                'clearSpan': clearSpan,
                'selectSpan': selectSpan,
                'containerDiv': containerDiv,
                'wrapDiv': wrapDiv,
                'value': value,
                'text': text,
                'selectingAction': selectingAction,
                'selectedAction': selectedAction,
                'clearAction': clearAction,
                'openLinkView': openLinkView
            };

            selectControls[id] = returnObj;

            return returnObj;
        },

        GridTable: function(id) {
            if (gridTable[id]) {
                return gridTable[id];
            }

            // 模型添加一个空对象在最后，以此作为添加行的模板
            var regex = new RegExp('(^' + id + '[\\[|_])(\\d*)([(\\]\\.)|_].*$)', 'g');
            var regex2 = new RegExp(id + '\\[\\d+\\]', 'g');
            var regex3 = new RegExp(id + '_\\d+_', 'g');

            var grid = function() {
                return $('#' + id)[0];
            };

            var templete = function() {
                return $('#grid_' + id + '_templete').html();
            };

            var rowCount = function(count) {
                if (arguments.length === 0) {
                    return parseInt($('#grid_' + id + '_rows').val());
                }

                $('#grid_' + id + '_rows').val(count);
            };

            var sortRow = function() {
                var rows = grid().tBodies[0].rows;

                if (rows && rows.length > 1) {
                    for (var i = 0; i < rows.length; i++) {
                        var formControls = $(rows[i]).find('[name^="' + id + '["]');

                        for (var j = 0; j < formControls.length; j++) {
                            formControls[j].name = formControls[j].name.replace(regex, '$1' + i + '$3');
                            //formControls[j].id = formControls[j].id.replace(regex, '$1' + i + '$3');
                        }
                    }
                }
            };

            var rowPlus = function() {
                var index = rowCount();
                $('#' + id).append(templete().replace(regex2, id + '[' + index + ']').replace(regex3, id + '_' + index + '_'));
                rowCount(index + 1);

                GmUIControl.init();
                commonPage.resize();
            };

            var rowMinus = function(arg) {
                var row = $(arg).parents('tr')[0];
                grid().deleteRow(row.rowIndex);

                commonPage.resize();
                //sortRow();
            };

            var returnObj = {
                'rowPlus': rowPlus,
                'rowMinus': rowMinus,
                'sortRow': sortRow
            };

            gridTable[id] = returnObj;

            return returnObj;
        }
    }
}();
