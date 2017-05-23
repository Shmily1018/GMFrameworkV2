var menu = function () {
    var tabMap = {};

    this.addTabs = function (title, url, closable) {
        $('li[role = "presentation"].active').removeClass('active');
        $('div[role = "tabpanel"].active').removeClass('active');
        var id = tabMap[url];

        if (id == undefined) {
            id = new Date().getTime();
        }

        if (!$("#" + id)[0]) {
            var obj = {};
            obj.url = url;
            obj.title = title;
            obj.close = closable == null ? true : false;
            tabMap[url] = id;

            var content; var mainHeight = 5000;

            title = '<li data-toggle="tab" role="presentation" id="tab_' + id + '"><a style="float:right" href="#' + id + '" aria-controls="' + id + '" role="tab" data-toggle="tab">' + //obj.title
                '<i class="glyphicon glyphicon-star-empty"></i>' + obj.title;

            if (obj.close) {
                title += '<span class="close" style="float:right;margin-left:5px;" class="closeTag" tabclose="' + id + '"  onclick="menu.closeTab(\'' + id + '\')"  ></span>';
            }
            title += '</a></li>';

            if (obj.content) {
                content = '<div role="tabpanel" class="tab-pane" id="' + id + '">' + obj.content + '</div>';
            } else {
                content = '<div role="tabpanel" class="tab-pane" id="' + id + '"><iframe src="' + obj.url + '" width="100%" id=iframe_' + id + ' height="' + mainHeight +
                    '" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="yes" allowtransparency="yes"></iframe></div>';
            }

            window.top.$(".nav-tabs").append(title);
            window.top.$(".tab-content").append(content);

            window.top.$("#iframe_" + id).load(function () {
                window.top.$("#iframe_" + id).height(window.top.$(window.top.$("#iframe_" + id)[0].contentWindow.document.body).height());
            });

        }

        window.top.$("#tab_" + id).addClass('active');
        window.top.$("#" + id).addClass("active");


        //关闭侧边栏
        if (!window.top.$('body').hasClass('page-sidebar-closed')) {
            window.top.$('.menu-toggler').click();
        }
    };

    this.closeTab = function (id) {
        //如果关闭的是当前激活的TAB，激活他的前一个TAB
        if (window.top.$('#' + id).hasClass('active')) {
            $("#tab_" + id).prev().addClass('active');
            $("#" + id).prev().addClass('active');
        }

        //关闭TAB
        window.top.$("#tab_" + id).remove();
        window.top.$("#" + id).remove();
    };

    this.closeCurrentTab = function () {
        var id = window.top.$("div.active").attr("id");
        this.closeTab(id);
    };

    this.closeTabCallBack = function (highlightID) {
        var id = window.top.$("div.active").attr("id");
        var contentWindow = window.top.$("#iframe_" + window.top.$("div.active").prev().attr("id"))[0].contentWindow;
        contentWindow.commonPage.highlightData = highlightID;
        contentWindow.eval("refreshPage()");
        window.top.toastr.info("操作成功!");
        this.closeTab(id);
    };

    this.closeDialogAndTabRefresh = function () {
        window.top.bootstrapGM.closeDialog();
        var id = window.top.$("div.active").attr("id");
        var contentWindow = window.top.$("#iframe_" + id)[0].contentWindow;
        contentWindow.eval("refreshPage()");
        window.top.toastr.info("操作成功!");
    };

    this.closeDialogAndRefreshParent = function () {
        window.top.bootstrapGM.closeDialog({ isRefreshParent: true });
        window.top.toastr.info("操作成功!");
    };

    return this;
}();