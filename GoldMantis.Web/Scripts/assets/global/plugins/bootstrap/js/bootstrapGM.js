$.fn.qdata = function () {
    var res = {};

    var data = $(this).attr('data');
    if (data) {
        var options = data.split(';');
        for (var i = 0; i < options.length; i++) {
            if (options[i]) {
                var opt = options[i].split(':');
                res[opt[0]] = opt[1];
            }
        }
    }

    return res;
};


var bootstrapAjax = {};

bootstrapAjax.ajaxoptions = {
    url: '',
    data: {},
    type: 'post',
    dataType: 'json',
    async: false
};
bootstrapAjax.ajaxopt = function (options) { 
    var opt = $.extend({}, bootstrapAjax.ajaxoptions);
    if (typeof options == 'string') {
        opt.url = options;
    } else {
        $.extend(opt, options);
    }

    return opt;
};
bootstrapAjax.ajax = function (options) {
    if (!options) {
        alert('need options');
    } else {
        var res;
        $.ajax(bootstrapAjax.ajaxopt(options)).done(function (obj) { res = obj; });

        return res;
    }
};
bootstrapAjax.on = function (obj, event, func) {
    $(document).off(event, obj).on(event, obj, func);
};




bootstrapGM = {};
bootstrapGM.modaloptions = {
    url: '',
    fade: 'fade',
    close: true,
    title: 'title',
    head: true,
    foot: true,
    btn: false,
    okbtn: '\u786e\u5b9a',
    qubtn: '\u53d6\u6d88',
    msg: 'msg',
    big: false,
    show: false,
    remote: false,
    backdrop: false,
    keyboard: true,
    style: '',
    width: '',
    sourceSelectControlId: ''
};
bootstrapGM.modalstr = function (opt) {
    var start = '<div class="modal draggable-modal ' + opt.fade + '" id="bsmodal"  role="dialog" aria-labelledby="bsmodaltitle" aria-hidden="true" style="position:fixed;top:20px;' + opt.style + '">';
    if (opt.big) {
        start += '<div class="modal-dialog modal-lg" style="width:' + opt.width + 'px"><div class="modal-content">';
    } else {
        start += '<div class="modal-dialog modal-sm" style="width:' + opt.width + 'px"><div class="modal-content">';
    }
    var end = '</div></div></div>';

    var head = '';
    if (opt.head) {
        head += '<div class="modal-header">';
        if (opt.close) {
            head += '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>';
        }
        head += '<h3 class="modal-title" id="bsmodaltitle">' + opt.title + '</h3></div>';
    }

    var body = '<div class="modal-body"><p><h4>' + opt.msg + '</h4></p></div>';

    var foot = '';
    if (opt.foot) {
        foot += '<div class="modal-footer"><button type="button" class="btn btn-primary bsok">' + opt.okbtn + '</button>';
        if (opt.btn) {
            foot += '<button type="button" class="btn btn-default bscancel">' + opt.qubtn + '</button>';
        }
        foot += '</div>';
    }

    return start + head + body + foot + end;
};

bootstrapGM.modalstr2 = function (opt) {
    var start = '<div class="modal draggable-modal ' + opt.fade + '" id="' + opt["bsID"] + '"  role="dialog" aria-labelledby="bsmodaltitle" aria-hidden="true" style="position:fixed;top:20px;' + opt.style + '">';
    if (opt.big) {
        start += '<div class="modal-dialog modal-lg" style="width:' + opt.width + 'px"><div class="modal-content">';
    } else {
        start += '<div class="modal-dialog modal-sm" style="width:' + opt.width + 'px"><div class="modal-content">';
    }
    var end = '</div></div></div>';

    var head = '';
    if (opt.head) {
        head += '<div class="modal-header">';
        if (opt.close) {
            head += '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>';
        }
        head += '<h3 class="modal-title" id="bsmodaltitle">' + opt.title + '</h3></div>';
    }

    var body = '<div class="modal-body" style="overflow-y: auto;overflow-x: hidden;"><p><h4>' + opt.msg + '</h4></p></div>';

    var foot = '';
    if (opt.foot) {
        foot += '<div class="modal-footer"><button type="button" class="btn btn-primary bsok">' + opt.okbtn + '</button>';
        if (opt.btn) {
            foot += '<button type="button" class="btn btn-default bscancel">' + opt.qubtn + '</button>';
        }
        foot += '</div>';
    }

    return start + head + body + foot + end;
};

bootstrapGM.currntInstance = '';
bootstrapGM.alert = function (options, func) {
    // options
    var opt = $.extend({}, bootstrapGM.modaloptions);

    opt.title = '\u63d0\u793a';
    if (typeof options == 'string') {
        opt.msg = options;
    } else {
        $.extend(opt, options);
    }

    // add
    $('body').append(bootstrapGM.modalstr(opt));

    // init
    var $modal = $('#bsmodal');
    bootstrapGM.currntInstance = $modal.modal(opt);


    // bind

    $('button.bsok', $modal).on('click', function () {
        if (func) func();
        $modal.remove();
    });

    $('button.close', $modal).on('click', function () {
        $modal.remove();
    });


    // show
    $modal.modal('show');
    $modal.draggable({
        handle: ".modal-header"
    });

};

bootstrapGM.confirm = function (options, ok, cancel) {
    // options
    var opt = $.extend({}, bootstrapGM.modaloptions);

    opt.title = '\u786e\u8ba4\u64cd\u4f5c';
    if (typeof options == 'string') {
        opt.msg = options;
    } else {
        $.extend(opt, options);
    }
    opt.btn = true;

    // append
    $('body').append(bootstrapGM.modalstr(opt));

    // init
    var $modal = $('#bsmodal');
    $modal.modal(opt);
    bootstrapGM.currntInstance = $modal.modal(opt);


    $('button.bsok', $modal).on('click', function () {
        if (ok) ok();
        $modal.remove();
    });

    $('button.bscancel', $modal).on('click', function () {
        $modal.remove();
    });

    $('button.close', $modal).on('click', function () {
        if (cancel) cancel();
        $modal.remove();
    });


    // show
    $modal.modal('show');
    $modal.draggable({
        handle: ".modal-header"
    });
};

var arrDialog = new Array();
var arrModal = new Array();

bootstrapGM.dialog = function (options, func) {
    // options
    var opt = $.extend({}, bootstrapGM.modaloptions, options);
    opt.big = true;
    opt.foot = false;
    var bsmodal = "bsmodal" + new Date().getTime();
    opt.bsID = bsmodal;
    // append
    $('body').append(bootstrapGM.modalstr2(opt));


    var dailogObject = $('<iframe width="100%" allowTransparency= "true" border="0" frameborder="0"  framespacing="0" marginheight="0" marginwidth="0" id="iframe$' + options.sourceSelectControlId + '"/>');
    $(dailogObject).attr("src", options.url); 

    arrDialog.push(dailogObject);

    $('#' + bsmodal + ' div.modal-body').empty().append(dailogObject);

    // init
    var $modal = $('#' + bsmodal);
    $modal.modal(opt);

    arrModal.push($modal);

    // bind
    bootstrapAjax.on('button.bsok', 'click', function () {
        var flag = true;
        if (func) {
            flag = func();
        }

        if (flag) {
            bootstrapGM.closeDialog($modal);
        }
    });
    bootstrapAjax.on('#' + bsmodal, 'hidden.bs.modal', function () {
        bootstrapGM.closeDialog($modal);
    });

    // show
    $modal.modal('show');
    

    Metronic.blockUI({
        target: '.modal-body',
        boxed: true,
        message: '数据加载中，请稍后...'
    });


    $modal.draggable({
        handle: ".modal-header"
    });

};

bootstrapGM.unblockUI= function() {
    Metronic.unblockUI('.modal-body');
}

bootstrapGM.closeDialog = function (option) {
    option = option || {};
    var _default = {
        isRefreshParent: false,
        isShowMessage: false,
        callback: "",
        message: "",
        form: "form",
        action: ''
    };
    $.extend(_default, option);

    if (arrDialog.length == 1) {
        if ($('iframe', window.top.$("div.active")).length > 0) {
            var contentWindow = $('iframe', window.top.$("div.active"))[0].contentWindow;
            if (_default.highlightData != undefined && _default.highlightData != null)
                contentWindow.commonPage.highlightData = _default.highlightData;

            try {
                if (option.action === 'confirm') {
                    var iframe = arrDialog[arrDialog.length - 1][0];
                    var iframeId = iframe.id;

                    if (iframeId && iframeId.split('$').length === 2) {
                        var selectControlId = iframeId.split('$')[1];
                        contentWindow.GmUIControl.SelectControl(selectControlId).selectedAction(iframe.contentWindow.returnObject);
                    }
                }
            } catch(e) {
            }

            if (_default.callback != undefined && _default.callback != "") {
                var contentWindowDialog = arrDialog[arrDialog.length - 1][0].contentWindow;
                
                if (contentWindowDialog.returnValue) {
                    contentWindow.eval(option.callback + '(' + contentWindowDialog.eval('returnValue()') + ')');
                }
                else {
                    contentWindow.eval(option.callback + '()');
                }
            }
            if (_default.isRefreshParent) {
                contentWindow.commonPage.ajaxSubmit();
            }
        }
    }
    else if (arrDialog.length == 0) {
        bootstrapGM.currntInstance.remove();

    } else {
        bootstrapGM.refreshDialog(_default);
    }

    arrDialog.pop();
    arrModal.pop().remove();
    
    if (_default.isShowMessage) {
        bootstrapGM.alert(_default.message);
    }

};

bootstrapGM.refreshDialog = function (option) {
    var contentWindowOpener = arrDialog[arrDialog.length - 2][0].contentWindow;
    var contentWindowDialog = arrDialog[arrDialog.length - 1][0].contentWindow;

    try {
        if (option.action === 'confirm') {
            var iframe = arrDialog[arrDialog.length - 1][0];
            var iframeId = iframe.id;

            if (iframeId && iframeId.split('$').length === 2) {
                var selectControlId = iframeId.split('$')[1];
                contentWindowOpener.GmUIControl.SelectControl(selectControlId).selectedAction(contentWindowDialog.returnObject);
            }
        }
    } catch (e) {
    }

    if (option.callback != undefined && option.callback != "") {
        if (contentWindowDialog.returnValue) {
            contentWindowOpener.eval(option.callback + '(' + contentWindowDialog.eval('returnValue()') + ')');
        }
        else {
            contentWindowOpener.eval(option.callback + '()');
        }
    }
    if (option.isRefreshParent) {
        contentWindowOpener.commonPage.ajaxSubmit({ form: option.form });
    }
}

bootstrapGM.msgoptions = {
    msg: 'msg',
    type: 'info',
    time: 2000,
    position: 'top'
};

bootstrapGM.msgstr = function (msg, type, position) {
    return '<div class="alert alert-' + type + ' alert-dismissible" role="alert" style="display:none;position:fixed;' + position + ':0;left:0;width:100%;z-index:2001;margin:0;text-align:center;" id="bsalert"><button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>' + msg + '</div>';
};

bootstrapGM.msg = function (options) {
    var opt = $.extend({}, bootstrapGM.msgoptions);

    if (typeof options == 'string') {
        opt.msg = options;
    } else {
        $.extend(opt, options);
    }

    $('body').prepend(bootstrapGM.msgstr(opt.msg, opt.type, opt.position));
    $('#bsalert').slideDown();
    setTimeout(function () {
        $('#bsalert').slideUp(function () {
            $('#bsalert').remove();
        });
    }, opt.time);
};

bootstrapGM.popoptions = {
    animation: true,
    container: 'body',
    content: 'content',
    html: true,
    placement: 'bottom',
    title: '',
    trigger: 'hover'//click | hover | focus | manual.
};

$.fn.bstip = function (options) {
    var opt = $.extend({}, bootstrapGM.popoptions);
    if (typeof options == 'string') {
        opt.title = options;
    } else {
        $.extend(opt, options);
    }

    $(this).data(opt).tooltip();
};

$.fn.bspop = function (options) {
    var opt = $.extend({}, bootstrapGM.popoptions);
    if (typeof options == 'string') {
        opt.content = options;
    } else {
        $.extend(opt, options);
    }

    $(this).popover(opt);
};

bootstrapGM.tree = {};

bootstrapGM.tree.options = {
    url: '/ucenter/menu',
    height: '600px',
    open: true,
    edit: false,
    checkbox: false,
    showurl: true
};

$.fn.bstree = function (options) {
    var opt = $.extend({}, bootstrapGM.tree.options);
    if (options) {
        if (typeof options == 'string') {
            opt.url = options;
        } else {
            $.extend(opt, options);
        }
    }

    var res = '\u52a0\u8f7d\u5931\u8d25\uff01';
    var json = bootstrapAjax.ajax(opt.url + '/tree');
    if (json && json.object) {
        var tree = json.object;

        var start = '<div class="panel panel-info"><div class="panel-body" ';
        if (opt.height != 'auto')
            start += 'style="height:600px;overflow-y:auto;"';
        start += '><ul class="nav nav-list sidenav" id="treeul" data="url:' + opt.url + ';">';
        var children = bootstrapGM.tree.sub(tree, opt);
        var end = '</ul></div></div>';
        res = start + children + end;
    }

    $(this).empty().append(res);
    bootstrapGM.tree.init();
};

bootstrapGM.tree.sub = function (tree, opt) {
    var res = '';
    if (tree) {
        var res =
			'<li>' +
				'<a href="javascript:void(0);" data="id:' + tree.id + ';url:' + tree.url + ';">' +
					'<span class="glyphicon glyphicon-minus"></span>';
        if (opt.checkbox) {
            res += '<input type="checkbox" class="treecheckbox" ';
            if (tree.checked) {
                res += 'checked';
            }
            res += '/>';
        }
        res += tree.text;
        if (opt.showurl) {
            res += '(' + tree.url + ')';
        }
        if (opt.edit)
            res +=
				'&nbsp;&nbsp;<span class="label label-primary bstreeadd">\u6dfb\u52a0\u5b50\u83dc\u5355</span>' +
				'&nbsp;&nbsp;<span class="label label-primary bstreeedit">\u4fee\u6539</span>' +
				'&nbsp;&nbsp;<span class="label label-danger  bstreedel">\u5220\u9664</span>';
        res += '</a>';
        var children = tree.children;
        if (children && children.length > 0) {
            res += '<ul style="padding-left:20px;" id="treeid_' + tree.id + '" class="nav collapse ';
            if (opt.open)
                res += 'in';
            res += '">';
            for (var i = 0; i < children.length; i++) {
                res += bootstrapGM.tree.sub(children[i], opt);
            }
            res += '</ul>';
        }
        res += '</li>';
    }

    return res;
};

bootstrapGM.tree.init = function () {
    bootstrapAjax.on('#treeul .glyphicon-minus', 'click', function () {
        if ($(this).parent().next().length > 0) {
            $('#treeid_' + $(this).parents('a').qdata().id).collapse('hide');
            $(this).removeClass('glyphicon-minus').addClass('glyphicon-plus');
        }
    });
    bootstrapAjax.on('#treeul .glyphicon-plus', 'click', function () {
        if ($(this).parent().next().length > 0) {
            $('#treeid_' + $(this).parents('a').qdata().id).collapse('show');
            $(this).removeClass('glyphicon-plus').addClass('glyphicon-minus');
        }
    });
    bootstrapAjax.on('input.treecheckbox', 'change', function () {
        // \u68c0\u6d4b\u5b50\u7ea7\u7684
        var subFlag = $(this).prop('checked');
        $(this).parent().next().find('input.treecheckbox').each(function () {
            $(this).prop('checked', subFlag);
        });

        // \u68c0\u6d4b\u7236\u8f88\u7684
        var parentFlag = true;
        var $ul = $(this).parent().parent().parent();
        $ul.children().each(function () {
            var checked = $(this).children().children('input').prop('checked');
            if (!checked) parentFlag = false;
        });
        $ul.prev().children('input').prop('checked', parentFlag);
    });

    bootstrapGM.tree.url = $('#treeul').qdata().url;
    if (bootstrapGM.tree.url) {
        bootstrapAjax.on('.bstreeadd', 'click', bootstrapGM.tree.addp);
        bootstrapAjax.on('.bstreedel', 'click', bootstrapGM.tree.del);
        bootstrapAjax.on('.bstreeedit', 'click', bootstrapGM.tree.editp);
    }
};

bootstrapGM.tree.addp = function () {
    bootstrapGM.dialog({
        url: bootstrapGM.tree.url + '/add/' + $(this).parent().qdata().id,
        title: '\u6dfb\u52a0\u5b50\u83dc\u5355',
        okbtn: '\u4fdd\u5b58'
        //	}, bootstrapGM.tree.add);
    }, function () { });
};

//bootstrapGM.tree.add = function(){
//	var res = bootstrapAjax.ajax({url:bootstrapGM.tree.url + '/save',data:$('#bsmodal').find('form').qser()});
//	bootstrapGM.msg(res);
//
//	if(res && res.type == 'success'){
//		bootstrapAjax.crud.url = bootstrapGM.tree.url;
//		bootstrapAjax.crud.reset();
//		return true;
//	}else{
//		return false;
//	}
//};
bootstrapGM.tree.del = function () {
    var res = bootstrapAjax.ajax({ url: bootstrapGM.tree.url + '/del/' + $(this).parent().qdata().id });
    bootstrapGM.msg(res);

    //	if(res && res.type == 'success'){
    //		bootstrapAjax.crud.url = bootstrapGM.tree.url;
    //		bootstrapAjax.crud.reset();
    //	}
};
bootstrapGM.tree.editp = function () {
    bootstrapGM.dialog({
        url: bootstrapGM.tree.url + '/savep?id=' + $(this).parent().qdata().id,
        title: '\u4fee\u6539\u83dc\u5355',
        okbtn: '\u4fdd\u5b58'
        //	}, bootstrapGM.tree.edit);
    }, function () { });
};
//bootstrapGM.tree.edit = function(){
//	bootstrapAjax.crud.url = bootstrapGM.tree.url;
//	return bootstrapAjax.crud.save();
//};
bootstrapGM.bstrooptions = {
    width: '500px',
    html: 'true',
    nbtext: '\u4e0b\u4e00\u6b65',
    place: 'bottom',
    title: '\u7f51\u7ad9\u4f7f\u7528\u5f15\u5bfc',
    content: 'content'
};
bootstrapGM.bstroinit = function (selector, options, step) {
    if (selector) {
        var $element = $(selector);
        if ($element.length > 0) {
            var opt = $.extend({}, bootstrapGM.bstrooptions, options);
            if (typeof options == 'string') {
                opt.content = options;
            } else {
                $.extend(opt, options);
            }

            $element.each(function () {
                $(this).attr({
                    'data-bootstro-width': opt.width,
                    'data-bootstro-title': opt.title,
                    'data-bootstro-html': opt.html,
                    'data-bootstro-content': opt.content,
                    'data-bootstro-placement': opt.place,
                    'data-bootstro-nextButtonText': opt.nbtext,
                    'data-bootstro-step': step
                }).addClass('bootstro');
            });
        }
    }
};
bootstrapGM.bstroopts = {
    prevButtonText: '\u4e0a\u4e00\u6b65',
    finishButton: '<button class="btn btn-lg btn-success bootstro-finish-btn"><i class="icon-ok"></i>\u5b8c\u6210</button>',
    stopOnBackdropClick: false,
    stopOnEsc: false
};
bootstrapGM.bstro = function (bss, options) {
    if (bss && bss.length > 0) {
        for (var i = 0; i < bss.length; i++) {
            bootstrapGM.bstroinit(bss[i][0], bss[i][1], i);
        }

        var opt = $.extend({}, bootstrapGM.bstroopts);
        if (options) {
            if (options.hasOwnProperty('pbtn')) {
                opt.prevButtonText = options.pbtn;
            }
            if (options.hasOwnProperty('obtn')) {
                if (options.obtn == '') {
                    opt.finishButton = '';
                } else {
                    opt.finishButton = '<button class="btn btn-mini btn-success bootstro-finish-btn"><i class="icon-ok"></i>' + options.obtn + '</button>';
                }
            }
            if (options.hasOwnProperty('stop')) {
                opt.stopOnBackdropClick = options.stop;
                opt.stopOnEsc = options.stop;
            }
            if (options.hasOwnProperty('exit')) {
                opt.onExit = options.exit;
            }
        }

        bootstro.start('.bootstro', opt);
    }
};
bootstrapGM.bsdateoptions = {
    autoclose: true,
    language: 'zh-CN',
    format: 'yyyy-mm-dd'
};
bootstrapGM.bsdate = function (selector, options) {
    if (selector) {
        var $element = $(selector);
        if ($element.length > 0) {
            var opt = $.extend({}, bootstrapGM.bsdateoptions, options);
            $element.each(function () {
                $(this).datepicker(opt);
            });
        }
    }
};
