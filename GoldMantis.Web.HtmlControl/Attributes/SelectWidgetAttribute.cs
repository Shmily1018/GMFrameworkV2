using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Web.Mvc;
using System.Web.Routing;
using GoldMantis.Common;
using GoldMantis.Web.HtmlControl.Classes;
using GoldMantis.Web.HtmlControl.Styles;
using Microsoft.SqlServer.Server;

namespace GoldMantis.Web.HtmlControl
{
    public class SelectWidgetPartAttribute : HtmlAttribute
    {
        public SelectWidgetPartAttribute(string selectUrl)
            : this(selectUrl, null)
        {
        }

        public SelectWidgetPartAttribute(string selectUrl, IDictionary<string, object> selectParams)
        {
            this.Visible = true;
            base.Class.Add(Bootstrap.Form.InputGroup);

            this.ClearSpan = new HtmlAttribute();
            this.ClearSpan.Class.Add(Bootstrap.Form.InputGroupAddon);
            this.ClearSpan.HtmlAttributes.Add("title", "清空");

            this.SelectSpan = new HtmlAttribute();
            this.SelectSpan.Class.Add(Bootstrap.Form.InputGroupAddon);
            this.SelectSpan.HtmlAttributes.Add("title", "选择");

            this.SelectDialog = new SelectDialog();
            //this.SelectDialog.SelectId = name;
            this.SelectDialog.SelectUrl = selectUrl;
            this.SelectDialog.SelectParams = selectParams;
            this.SelectDialog.SelectWidth = 900;
            this.SelectDialog.SelectTitle = "选择页面";
        }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        public SelectDialog SelectDialog { get; protected set; }

        /// <summary>
        /// 是否隐藏清楚按钮
        /// </summary>
        public bool HiddenClearButton { get; set; }

        /// <summary>
        /// 是否隐藏选择按钮
        /// </summary>
        public bool HiddenSelectButton { get; set; }

        /// <summary>
        /// 是否启动自动完成
        /// </summary>
        public bool IsAutoComplete { get; set; }

        /// <summary>
        /// 自动完成URL
        /// </summary>
        public string AutoCompleteUrl { get; set; }

        /// <summary>
        /// 是否启用链接
        /// </summary>
        public bool IsLink { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 是否多选
        /// </summary>
        public bool IsMultiSelect { get; set; }

        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible { get; set; }

        private string _onSelecting;

        /// <summary>
        /// 点击选择时候的脚本
        /// </summary>
        public string OnSelecting
        {
            get
            {
                if (_onSelecting.IsNullOrEmpty())
                {
                    _onSelecting = string.Format("GmUIControl.SelectControl('{0}').selectingAction({1}, {2})",
                        this.Name, this.SelectDialog.ToString(), this.OnSelected.IsNullOrEmpty() ? "null" : this.OnSelected);
                }

                return _onSelecting;
            }
            set { _onSelecting = value; }
        }

        /// <summary>
        /// 选择完成执行的脚本
        /// </summary>
        public string OnSelected { get; set; }

        private string _onClear;

        /// <summary>
        /// 清除按钮执行脚本
        /// </summary>
        public string OnClear
        {
            get
            {
                if (_onClear.IsNullOrEmpty())
                {
                    _onClear = string.Format("GmUIControl.SelectControl('{0}').clearAction()", this.Name);
                }

                return _onClear;
            }
            set { _onClear = value; }
        }

        /// <summary>
        /// 双击选择
        /// </summary>
        public bool IsDbClickSelect { get; set; }

        /// <summary>
        /// 一个class，控制控件的宽度
        /// </summary>
        public string WidthClass { get; set; }

        public HtmlAttribute ClearSpan { get; set; }

        public HtmlAttribute SelectSpan { get; set; }

        public override IDictionary<string, object> GetHtmlAttribute()
        {
            if (this.IsAutoComplete || this.ReadOnly || (this.HiddenClearButton && this.HiddenSelectButton))
            {
                //this.Class.Clear();
                base.Class.Remove(Bootstrap.Form.InputGroup);
            }

            string id = string.Format("{0}_Container", this.Name);
            base.MergeAttributeValue(base.HtmlAttributes, "id", id);

            return base.GetHtmlAttribute();
        }

        public virtual void PrepareWidgetHtmlAttribute()
        {
            PrepareTextHtmlAttribute();
            PrepareValueHtmlAttribute();
            PrepareClearHtmlAttribute();
            PrepareSelectHtmlAttribute();
        }

        protected virtual void PrepareTextHtmlAttribute()
        {

        }

        protected virtual void PrepareValueHtmlAttribute()
        {
        }

        protected virtual void PrepareClearHtmlAttribute()
        {
            if (this.HiddenClearButton || this.IsAutoComplete || this.ReadOnly)
            {
                this.ClearSpan.Class.Add(Bootstrap.Typesetting.Hidden);
            }

            if (!this.OnClear.IsNullOrEmpty())
            {
                base.MergeAttributeValue(this.ClearSpan.HtmlAttributes, "onclick", this.OnClear);
            }

            string id = string.Format("{0}_Clear", this.Name);
            base.MergeAttributeValue(this.ClearSpan.HtmlAttributes, "id", id);
        }

        protected virtual void PrepareSelectHtmlAttribute()
        {
            if (this.HiddenSelectButton || this.IsAutoComplete || this.ReadOnly)
            {
                this.SelectSpan.Class.Add(Bootstrap.Typesetting.Hidden);
            }

            if (!(this.HiddenSelectButton || this.IsAutoComplete || this.ReadOnly))
            {
                base.MergeAttributeValue(this.SelectSpan.HtmlAttributes, "onclick", this.OnSelecting);
            }

            string id = string.Format("{0}_Select", this.Name);
            base.MergeAttributeValue(this.SelectSpan.HtmlAttributes, "id", id);
        }
    }

    public class SelectWidgetAttribute : SelectWidgetPartAttribute
    {
        public SelectWidgetAttribute(string name, string selectUrl)
            : this(name, selectUrl, null)
        {
        }

        public SelectWidgetAttribute(string name, string selectUrl, IDictionary<string, object> selectParams)
            : base(selectUrl, selectParams)
        {
            this.Name = name;
            this.TextInput = new TextBoxAttribute(string.Format("{0}_Text", name));
            this.ValueInput = new TextBoxAttribute(name);
        }

        public TextBoxAttribute TextInput { get; set; }

        public TextBoxAttribute ValueInput { get; set; }

        protected override void PrepareTextHtmlAttribute()
        {
            this.TextInput.Value = this.Text;

            if (!this.IsAutoComplete || this.ReadOnly)
            {
                base.MergeAttributeValue(this.TextInput.HtmlAttributes, "readonly", "readonly");
                /***待补充***/
            }

            if (this.IsLink)
            {
                this.TextInput.Class.Add("select-control-link");
                /***待优化***/
                base.MergeAttributeValue(this.TextInput.HtmlAttributes, "onclick",
                    string.Format("GmUIControl.SelectControl('{3}').openLinkView('{0}{1}KeyID={2}')",
                    this.LinkUrl, (this.LinkUrl ?? string.Empty).Contains("?") ? "&" : "?", Value, Name));
            }


            if (this.IsDbClickSelect && !this.ReadOnly)
            {
                base.MergeAttributeValue(this.TextInput.HtmlAttributes, "ondblclick", this.OnSelecting);
            }
        }

        protected override void PrepareValueHtmlAttribute()
        {
            this.ValueInput.Value = this.Value;
        }
    }

    public class SelectWidgetAttribute<TModel, TPText, TPValue> : SelectWidgetPartAttribute
    {
        public SelectWidgetAttribute(Expression<Func<TModel, TPText>> expText,
            Expression<Func<TModel, TPValue>> expValue, string selectUrl)
            : this(expText, expValue, selectUrl, null)
        {
        }

        public SelectWidgetAttribute(Expression<Func<TModel, TPText>> expText,
            Expression<Func<TModel, TPValue>> expValue, string selectUrl, IDictionary<string, object> selectParams)
            : base(selectUrl, selectParams)
        {
            this.TextInput = new TextBoxAttribute<TModel, TPText>(expText);
            this.ValueInput = new TextBoxAttribute<TModel, TPValue>(expValue);
        }

        public TextBoxAttribute<TModel, TPText> TextInput { get; set; }

        public TextBoxAttribute<TModel, TPValue> ValueInput { get; set; }

        public void PrepareWidgetHtmlAttribute(string name, string text, string value)
        {
            this.Name = name;
            this.Text = text;
            this.Value = value;
            this.SelectDialog.SelectId = name;

            base.PrepareWidgetHtmlAttribute();
        }

        public override void PrepareWidgetHtmlAttribute()
        {
            
        }

        protected override void PrepareTextHtmlAttribute()
        {
            if (!this.IsAutoComplete || this.ReadOnly)
            {
                base.MergeAttributeValue(this.TextInput.HtmlAttributes, "readonly", "readonly");
                /***待补充***/
            }

            if (this.IsLink)
            {
                this.TextInput.Class.Add("select-control-link");
                /***待优化***/
                base.MergeAttributeValue(this.TextInput.HtmlAttributes, "onclick",
                    string.Format("GmUIControl.SelectControl('{3}').openLinkView('{0}{1}KeyID={2}')",
                    this.LinkUrl, (this.LinkUrl ?? string.Empty).Contains("?") ? "&" : "?", Value, Name));
            }


            if (this.IsDbClickSelect && !this.ReadOnly)
            {
                base.MergeAttributeValue(this.TextInput.HtmlAttributes, "ondblclick", this.OnSelecting);
            }
        }
    }

    public class SelectDialog
    {
        public string SelectId { get; set; }

        /// <summary>
        /// 选择页面URL
        /// </summary>
        public string SelectUrl { get; set; }

        public string SelectTitle { get; set; }

        public int SelectWidth { get; set; }

        /// <summary>
        /// 选择页面URL参数
        /// </summary>
        public IDictionary<string, object> SelectParams { get; set; }

        public override string ToString()
        {
            return string.Format("{{url: '{0}', title: '{1}', width: {2}, sourceSelectControlId: '{3}'}}",
                BuildUrlWithParams(SelectUrl, SelectParams), SelectTitle, SelectWidth, SelectId);
        }

        protected string BuildUrlWithParams(string urlPart, IDictionary<string, object> urlParams)
        {
            if (urlPart.IsNullOrEmpty())
            {
                return string.Empty;
            }


            if (urlParams.IsNull() || urlParams.Count == 0)
            {
                return urlPart;
            }

            string paramStr = string.Join("&", urlParams.Select(p => string.Format("{0}={1}", p.Key, p.Value)));

            return string.Format("{0}{1}{2}", urlPart, urlPart.Contains("?") ? "&" : "?", paramStr);
        }
    }


    
}