//import { PassThrough } from "stream";
//import { parse } from "path";

var myconfig = {
    PageSize: 20,
    PageIndex: 1,
    Sort: "create_time desc",
    count: 0,
    elem: "#mytable",
    pageElem: "#mytablePage",
    selectColor: "#fffabb",
    elemClass: "table align-center table-hover"
}
var tableDatas = [];
var optionList = {};
var selectRowId;
var selectRowData;
var _click;

function initFun(id) {
    this.myconfig = myconfig;
    this.elem = id;
    this.table = _table;
    this.options = function (newOptions) {
        $.extend(this.options, newOptions);
        this.table(this.options);
    }
    this.getData = function (rowid) { return _GetTableData(this.elem, rowid); }
    this.getSelRowid = function (id) {
        var tableStr = (this.elem).substr(1);
        if (!optionList[tableStr].multiple) {
            return _GetSelectRowId();
        } else {
            return _GetMultipleRowid(this.elem);
        }
    }
    this.onclick = function (e, fun) {
        if ($.type(fun) == null || $.type(fun) == "undefined") {
            return tableClick(e);
        } else {
            tableClick(e);
            return fun(selectRowId, selectRowData);
        }
    }
    this.target = function (type) {
        var tableStr = (this.elem).substr(1);
        if (type == "reload") {
            this.table(optionList[tableStr]);
        }
    }
    this.setParamAndLoad = function (setparma) {
        var tableStr = (this.elem).substr(1);
        optionList[tableStr].data = setparma;
        console.log(setparma);
        console.log(optionList[tableStr].data);
        this.table(optionList[tableStr]);
    }
    this.LoadPagin = function (pageSize, pageIndex, maxIndex) {
        if (pageIndex > maxIndex) {
            _alert("已经是最后一页了哦", "warning");
            return;
        }
        if (pageIndex == 0) {
            _alert("已经是第一页了哦", "warning");
            return;
        }
        var tableStr = (this.elem).substr(1);
        optionList[tableStr].PageSize = pageSize;
        optionList[tableStr].PageIndex = pageIndex;
        this.table(optionList[tableStr]);
    }
}

var _frame = function (id) {
    return new initFun(id);
}
$.request = function (name) {
    var allUrl = window.location.href;
    var indexq = allUrl.indexOf('?');
    if (indexq == -1) {
        return "";
    }
    var resString = "";
    var url = allUrl.substr(0, indexq);
    var params = allUrl.substr(indexq + 1);
    var param = {};
    var paramArr = params.split('&');
    $.each(paramArr, function (i, item) {
        var tempindex = item.indexOf('=');
        if (item.substr(0, tempindex) == name) {
            resString = item.substr(tempindex + 1);
            return;
        }
    });
    return resString;
}
$(function () {
    var _ajax = $.ajax;
    $.ajax = function (options) {
        if (options.type == null || options.type == null) {
            options.type = 'post';
        }
        var userSuccess = null;
        var userError = null;
        if (options.success != null && options.success != undefined) {
            userSuccess = options.success;
        }
        if (options.error != null && options.error != undefined) {
            userError = options.error;
        }
        options.success = function (data, state, XMLHttpRequest) {
            if (XMLHttpRequest.status > 210 && XMLHttpRequest.status < 240) {
                _alert(JSON.parse(XMLHttpRequest.responseText).msg, "warnning");
            } else if (XMLHttpRequest.status > 240) {
                _alert(JSON.parse(XMLHttpRequest.responseText).msg, "error");
                if (userError != null) {
                    userError(JSON.parse(XMLHttpRequest.responseText).msg);
                }
            } else {
                userSuccess(data);
            }
        }
        if (options.error == null || options.error == undefined) {
            options.error = function (XMLHttpRequest, msg, e) {
                if (XMLHttpRequest.status % 100 > 10) {
                    top.layer.open({
                        title: "提示",
                        content: XMLHttpRequest.responseText
                    });
                } else {
                    _alert("网络连接错误，请刷新页面！", "error");
                }
            }
        }
        _ajax(options);
    }
    $.SubmitForm = function (options) {
        var param = {};
        var fun = function (data) { }
        if (options.data != null && options.data != undefined) {
            param = options.data;
        }
        if (options.success != null && options.success != undefined) {
            fun = options.success;
        }
        $.ajax({
            url: options.url,
            data: options.data,
            type: "POST",
            dataType: "json",
            success: function (res) {
                fun(res);
                top.layer.msg(res.msg);
                var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                parent.layer.close(index); //再执行关闭   
            }
        });
    }
});
$(function () {
    //_frame.table = _table;
    //_frame.getData = function (domId, Rowid) { return _GetTableData(domId, Rowid); }
    //_frame.getSelRow = function () { return _GetSelectRowId();} 
})
//自定义弹出提示
var _alert = function (msg, type) {
    var t = -1;
    switch (type) {
        case 2:
        case "error": t = 2; break;
        case 1:
        case "ok":
        case "success": t = 1; break;
        case 0:
        case "warning": t = 0; break;
        case 3:
        case "quantion": t = 3; break;
        case 4:
        case "lock": t = 4; break;
        case 5:
        case "fail": t = 5; break;
        case 6:
        case "win": t = 6; break;
        default: t = -1; break;
    }
    top.layer.alert(msg, { icon: t });
}

var _table = function (options) {
    if (options.elem == null || options.elem == undefined) {
        options.elem = this.elem;
    }
    options = SetOptions(options);
    var tableStr = (options.elem).substr(1);
    var html = "";
    var columns = options.columns;
    var htmlHead = "";
    var htmlTBody = "";
    //表格头部渲染
    var columnAlign = "";
    if (options.thAlign != null && options.thAlign != undefined) {
        columnAlign = "text-align:" + options.thAlign + ";";
    }
    htmlHead += "<thead><tr>";
    if (options.multiple) {
        htmlHead += "<th style='width:40px;'></th>";
    }
    htmlHead += "<th style='width:40px;max-width:120px;'></th>";
    var lastThWidth = -1;
    var tableWidth = parseInt($(options.elem).css("width").substr(0, $(options.elem).css("width").length - 2));
    var thWidthCount = 0;
    $.each(columns, function (i, item) {
        if (item.index == null || item.index == undefined) {
            return;
        }
        if (item.name == null || item.name == undefined) {
            item.name = item.index;
        }
        if (item.hidden == null || item.hidden == undefined) {
            item.hidden = false;
        }
        var hiddenCode = "";
        var columnWidth = "";
        if (item.hidden) {
            hiddenCode = "display:none;";
        }
        if (item.width != null && item.width != undefined) {
            if ($.type(item.width) == "number") {
                thWidthCount += item.width;
                columnWidth = "width:" + item.width + "px;";
            }
            else if ($.type(item.width) == "string") {
                thWidthCount += parseInt(item.substr(0, item.length - 2));
                columnWidth = "width:" + item.width + ";";
            }
        }
        htmlHead += "<th  style='" + columnWidth + hiddenCode + columnAlign + "'>" + item.name + "</th>";
    });
    lastThWidth = tableWidth - thWidthCount;
    if (lastThWidth > 0) {
        htmlHead += "<th style='" + lastThWidth + "px'></th>";
    }
    htmlHead += "</tr></thead>";
    var listData = [];
    //数据获取
    $.ajax({
        url: options.url,
        type: options.type,
        dataType: 'json',
        data: options.data,
        success: function (res) {
            var page = {};
            var data;
            if (res.pagins == undefined) {
                data = res.model;
            } else {
                page = res.pagins;
                data = res.model;
            }
            htmlTBody += "<tbody>";
            if (options.dataType == "json") {
            } else {
                data = JSON.parse(data);
            }
            if (data == null || data == undefined || data.length == 0) {
                _alert("找不到相关数据", "warning");
                $(options.elem).html(htmlHead);
                return;
            }
            $.each(data, function (i, item) {  //遍历每行数据
                htmlTBody += "<tr>";  //添加行标
                $.each(columns, function (ci, citem) {  //遍历表头单元格
                    if (citem.hidden == null || citem.hidden == undefined) {
                        citem.hidden = false;
                    }
                    var columnWidth = "";
                    var columnAlign = "";
                    var columnClass = "";
                    if (citem.width != null && citem.width != undefined) {
                        if ($.type(citem.width) == "number") {
                            thWidthCount += citem.width;
                            columnWidth = "width:" + citem.width + "px;";
                        }
                        else if ($.type(citem.width) == "string") {
                            thWidthCount += parseInt(citem.substr(0, citem.length - 2));
                            columnWidth = "width:" + citem.width + ";";
                        }
                    }
                    if (citem.align != null && citem.align != undefined) {
                        columnAlign = "text-align:" + citem.align + ";";
                    }
                    if (citem.class != null && citem.class != undefined) {
                        columnClass = "class='" + citem.class + "'";
                    }
                    var onclickParam = "";
                    var hiddenCode = "";
                    if (citem.hidden) {
                        hiddenCode = "display:none;";
                    }
                    if (options.selectRow == null || options.selectRow == undefined) {
                        onclickParam = "";
                    } else {
                        onclickParam = "," + options.selectRow;
                    }
                    if (ci == 0) {  //每行先添加行标顺序
                        if (options.multiple) {
                            htmlTBody += "<th style='width:40px;'><input name='_frame_" + tableStr + "_multiple' type='checkbox' value='" + (i + 1) + "' ></th>";
                        }
                        htmlTBody += "<td id='_frameRowid_" + (i + 1) + "' name='_frameRowid' class='W80th'>" + (i + 1) + "</td>";
                    }
                    if (citem.format == null || citem.format == undefined) {//无格式化
                        if (item[citem.index] == null || item[citem.index] == undefined) { //如果在该行中不存在表头中的索引，则不添加数据
                            htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'></td>";
                        } else {  //
                            htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + item[citem.index] + "</td>";//添加数据，item[citem.index],citem.index为列索引
                        }
                    } else {
                        if ($.type(citem.format) == "function") {
                            //format(rowid,rowdata) 
                            var rowData = item;
                            var UnitContent = citem.format((i + 1), rowData);
                            htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + UnitContent + "</td>";
                        } else if ($.type(citem.format) == "string") {
                            switch (citem.format) {
                                case "text":
                                case "string": htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + item[citem.index] + "</td>"; break;
                                case "number": htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + (item[citem.index] == '' || item[citem.index] == null ? 0 : parseFloat(item[citem.index])) + "</td>"; break;
                                case "money": htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + GetToFixed(item[citem.index], 2) + "</td>"; break;
                                case "date": htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + formatTime(item[citem.index], "yyyy-MM-dd") + "</td>"; break;
                                case "datetime": htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + formatTime(item[citem.index], "yyyy-MM-dd HH:mm:ss") + "</td>"; break;
                                default:
                                    if ((citem.format).substr(0, 5) == "json:") {//json数据格式，根据值进行匹配显示，例：json:{"0","男","1":"女","2":"未知"}
                                        var jj = (citem.format).substr(5);
                                        htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + ColumnFormatJson(item[citem.index], jj) + "</td>";
                                    } else if ((citem.format).substr(0, 5) == "date:") { //自定义时间格式,例：date:yy-MM-dd
                                        var tt = (citem.format).substr(5);
                                        htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + formatTime(item[citem.index], tt) + "</td>";
                                    } else {
                                        htmlTBody += "<td " + columnClass + " id='_frame_" + citem.index + "_" + (i + 1) + "' name='_frame_" + citem.index + "_" + (i + 1) + "' style='" + columnWidth + hiddenCode + columnAlign + "' onclick='_frame(\"" + options.elem + "\").onclick(this" + onclickParam + ")'>" + item[citem.index] + "</td>";
                                    }
                                    break;
                            }
                        }
                    }
                    item.rowid = (i + 1);
                    listData.push(item);
                });
                htmlTBody += "</tr>";
            });
            htmlTBody += "</tbody>";
            html = htmlHead + htmlTBody;
            $(options.elem).html(html);
            tableDatas = ArrDelet(tableDatas, tableStr, "json"); //添加前先删除之前存储的
            var tempTable = '{"' + tableStr + '":' + JSON.stringify(listData) + '}';
            tableDatas.push(JSON.parse(tempTable));
            ArrDelet(optionList, tableStr);
            delete optionList[tableStr];
            optionList[tableStr] = options;
            page.domId = options.pageElem;
            SetPagin(page, options.elem);
            loadedFunction(options.loaded);
        }
    });
}

///删除数组中指定的元素
///param(arr):原始数组
///param(index):对应要删除的索引
///param(type):可选参数，如果是json，则传递type="json"，删除的时候只会匹配json的前标识，例如：[{"arr1":[{},{}]},{"arr2":[{},{}]}],传递arr1则只会删除arr1
///示经过详细测试，如遇到不可处理的数组时再进行升级
function ArrDelet(arr, index, type) {
    var resArr = [];
    if ($.type(index) == "number") {
        $.each(arr, function (i, item) {
            if (i == index) {
            } else {
                resArr.push(item);
            }
        });
    }
    else if ($.type(index) == "string") {
        $.each(arr, function (i, item) {
            if (type == "json") {
                if (item[index] == null || item[index] == undefined) {
                    resArr.push(item);
                }
            } else {
                if (item == index) {

                } else {
                    resArr.push(item);
                }
            }
        });
    }
    return resArr;
}
//table请求参数检测
function SetOptions(options) {
    if (options.url == null || options.url == undefined) {
        return;
    }
    if (options.elem == null || options.elem == undefined) {
        options.elem = this.myconfig.elem;
    }
    if (options.pageElem == null || options.pageElem == undefined) {
        options.pageElem = this.myconfig.pageElem;
    }
    if (!$(options.elem)) {
        return;
    }
    if (!($.type(options.columns) == "array")) {
        return;
    }
    if (options.loaded == null || options.loaded == undefined) {
        options.loaded = function () { }
    }
    if (options.type == null || options.type == undefined) {
        options.type = 'post';
    }
    if (options.data == null || options.data == undefined) {
        options.data = {};
        options.time = new Date().getTime();
    }
    if (options.PageSize == null || options.PageSize == undefined) {
        options.PageSize = this.myconfig.PageSize;
    }
    if (options.Sort == null || options.Sort == undefined) {
        options.Sort = this.myconfig.Sort;
    }
    if (options.PageIndex == null || options.PageIndex == undefined) {
        options.PageIndex = this.myconfig.PageIndex;
    }
    if (options.multiple == null || options.multiple == undefined) {
        options.multiple = false;
    }
    if (options.width != null && options.width != undefined) {
        if ($.type(options.width) == "number") {
            $(options.elem).css("width", options.width + "px");
        }
        else if ($.type(options.width) == "string") {
            $(options.elem).css("width", options.width);
        }
    }
    if (options.height != null && options.height != undefined) {
        if ($.type(options.width) == "number") {
            $(options.elem).css("height", options.height + "px");
        }
        else if ($.type(options.width) == "string") {
            $(options.elem).css("height", options.height);
        }
    }
    $(options.elem).attr("class", this.myconfig.elemClass);
    if (options.elemClass != null && options.elemClass != undefined && options != "undefined") {
        $(options.elem).addClass(options.elemClass)
    }
    options.data.Sort = options.Sort;
    options.data.PageSize = options.PageSize;
    options.data.PageIndex = options.PageIndex;
    //$(options.elem).attr("class", "table align-center");
    return options;
}
//保留指定小数位数
function GetToFixed(number, leng) {
    var caculatorNumber = 10;
    for (i = 0; i < leng; i++) {
        caculatorNumber *= 10;
    }
    number = parseInt(number * caculatorNumber);
    if (number > 0) {
        number = (number % 10 >= 5 ? (parseInt(number / 10) + 1) : parseInt(number / 10));
    } else {
        number = (number % 10 <= -5 ? (parseInt(number / 10) - 1) : parseInt(number / 10));
    }
    number = parseFloat(number / (caculatorNumber / 10));
    var tailNumber = String(number).substr(String(number).indexOf('.')); for (var i = tailNumber.length - 1; i < leng; i++) {
        if (i == 0) { number = String(number) + "."; }
        number = String(number) + "0";
    }
    return number;
}

//JSON格式的数据格式化显示成对应的数据
function ColumnFormatJson(content, json) {
    json = JSON.parse(json);
    return json[content];
}

//时间格式化
function formatIntNumberToStr(number, len) {
    var numberlen = String(number).length;
    if (!(len > 0)) {
        return String(number);
    }
    if (!(numberlen < len)) {
        return String(number).substr(0, len);
    }
    var res = "";
    for (var i = numberlen; i < len; i++) {
        res += "0";
    }
    res += String(number);
    return res;

}
//格式化时间
function formatTime(datetime, formatStr) {
    var d = new Date(datetime);
    var year = d.getFullYear();
    var year2 = parseInt(String(year).substr(2));
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var hour = d.getHours();
    var min = d.getMinutes();
    var sec = d.getSeconds();
    var mill = d.getMilliseconds();
    formatStr = formatStr.replace("yyyy", formatIntNumberToStr(year, 4));
    formatStr = formatStr.replace("yy", formatIntNumberToStr(year2, 2));
    formatStr = formatStr.replace("MM", formatIntNumberToStr(month, 2));
    formatStr = formatStr.replace("dd", formatIntNumberToStr(day, 2));
    formatStr = formatStr.replace("HH", formatIntNumberToStr(hour, 2));
    formatStr = formatStr.replace("mm", formatIntNumberToStr(min, 2));
    formatStr = formatStr.replace("ss", formatIntNumberToStr(sec, 2));
    formatStr = formatStr.replace("fff", formatIntNumberToStr(mill, 3));
    formatStr = formatStr.replace("ff", formatIntNumberToStr(mill, 2));
    formatStr = formatStr.replace("f", formatIntNumberToStr(mill, 1));
    return formatStr;
}

///取表格中的一行数据
///param(domId):table标签的id，例如<table id="#mytable"></table>,param(domId)=#mytable
///param(rowid):要取的行id，如果rowid为空，则返回全部数据
function _GetTableData(domId, rowid) {
    var rowData = {};
    var tableData = [];
    domId = String(domId).substr(1);
    $.each(tableDatas, function (i, item) {
        if (item[domId] != null && item[domId] != undefined) {
            tableData = item[domId];
        }
    });
    if (rowid == null || rowid == undefined) {
        return tableData;
    }
    $.each(tableData, function (i, item) {
        if (item.rowid == rowid) {
            rowData = item;
        }
    });
    return rowData;
}

//设置选中的RowId
function _GetSelectRowId() {
    return parseInt(selectRowId);
}

function _GetMultipleRowid(domId) {
    var arr = new Array();
    $.each($(domId).find("input[type='checkbox']:checked"), function (i, item) {
        arr.push($(item).val());
    });
    return arr;
}

//表格单击选中效果
function tableClick(e) {
    var _selectColor = this.myconfig.selectColor;
    selectRowId = $(e).parent().find("td[name='_frameRowid']").html();
    var domId = $(e).parent().parent().parent().attr("id");
    $("#" + domId).find("td").css("background-color", "");
    //$(e).parent().css("background-color", this.myconfig.selectColor);
    $.each($("#" + domId).find("td"), function (i, item) {
        $(item).css("background-color", "");
    });
    $.each($(e).parent().find("td"), function (i, item) {
        $(item).css("background-color", _selectColor);
    });
    selectRowData = _GetTableData("#" + domId, selectRowId);

    var tableStr = domId
    if (optionList[tableStr].multiple) {
        if (!($(e).parent().find("input[type='checkbox']").attr("checked"))) {
            $(e).parent().find("input[type='checkbox']").attr("checked", true);
        } else {
            $(e).parent().find("input[type='checkbox']").attr("checked", false);
        }
    }
}

//设置分页
function SetPagin(options, tableElem) {
    if (options.domId == null || options.domId == undefined) {
        options.domId = this.myconfig.pageElem;
    }
    if (options.paginSize == null || options.paginSize == undefined) {
        domId = this.myconfig.PageSize;
    }
    if (options.PageIndex == null || options.PageIndex == undefined) {
        options.PageIndex = 1;
    }
    if (options.counts == null || options.counts == undefined) {
        options.counts = 0;
    }
    var html = "";
    var pageCount = parseInt((parseInt(options.counts) / parseInt(options.PageSize)) + 1);
    var PreviousPageIndex = options.PageIndex - 1;
    html += '<nav aria-label="Page navigation"><ul class="pagination"><li><a href="javascript:LoadPagin(' + options.PageSize + ',' + PreviousPageIndex + ',' + pageCount + ',\'' + tableElem + '\');" aria-label="Previous"><span aria-hidden="true">&laquo;</span></a></li>';
    if (options.PageIndex < 3) {
        for (var i = 0; i < pageCount; i++) {
            if (i == 6) {
                html += '<li><a href="#">...</a></li>';
                break;
            } else {
                html += '<li><a href="javascript:LoadPagin(' + options.PageSize + ',' + (i + 1) + ',' + pageCount + ',\'' + tableElem + '\');">' + (i + 1) + '</a></li>';
            }
        }
    } else {
        for (var i = 0; i < pageCount; i++) {
            var DValue = parseInt(options.PageIndex) - i;
            if (DValue > -3 && DValue < 3) {
                html += '<li><a href="javascript:LoadPagin(' + options.PageSize + ',' + (i + 1) + ',' + pageCount + ',\'' + tableElem + '\');">' + (i + 1) + '</a></li>';
            } else {
                if (DValue == -3) {
                    html += '<li><a href="javascript:LoadPagin(' + options.PageSize + ',1,' + pageCount + ',\'' + tableElem + '\');">1</a></li>';
                    html += '<li><a href="#">...</a></li>';
                }
                if (DValue == 3) {
                    html += '<li><a href="#">...</a></li>';

                }
            }
        }
    }
    var nextPageIndex = options.PageIndex + 1;
    html += '<li><a href="javascript:LoadPagin(' + options.PageSize + ',' + nextPageIndex + ',' + pageCount + ',\'' + tableElem + '\');" aria-label="Next"><span aria-hidden="true">&raquo;</span></a></li>';
    html += '<li><span style="border:none;background-color:transparent;color:#000;">共' + options.counts + '条数据，每页' + options.PageSize + '条，当前第' + options.PageIndex + '页,共' + parseInt(pageCount) + '页</a></li></ul></nav>';
    $(options.domId).html(html);
}

function LoadPagin(pageSize, pageIndex, maxIndex, tableElem) {
    _frame(tableElem).LoadPagin(pageSize, pageIndex, maxIndex);
}

function loadedFunction(fun) {
    if ($.type(fun) != "function") {
        return false;
    }
    fun();
}

//日期计算
//<AddDayCount>:数值，-1表示前一天，1表示后一天
function GetDateStr(AddDayCount) {
    var dd = new Date();
    dd.setDate(dd.getDate() + AddDayCount);//获取AddDayCount天后的日期
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1;//获取当前月份的日期
    var d = dd.getDate();
    return formatIntNumberToStr(y, 4) + "-" + formatIntNumberToStr(m, 2) + "-" + formatIntNumberToStr(d,2);
} 
///
// 调用示例
///
//_frame("#mytable").table({
//    url: "",
//    columns: [
//        { name: "菜单名称", index: "PowerName", width: 80 },
//        { name: "对应URL", index: "PowerUrl", width: 200 },
//        { name: "归属类", index: "PowerClass", width: 80 },
//        { name: "排序", index: "PowerSort", width: 80 },
//        {
//            name: "目录级别", index: "PowerSort", width: 80, format: function (rowid, rowdata) {
//                if (rowdata.PowerUrl == null || rowdata.PowerUrl == undefined) {
//                    return '1';
//                }
//                return '2';
//            }
//        },
//        { name: "创建时间", index: "create_time", width: 80, format: "date" },
//    ],
//    selectRow: function (rowid, rowData) {
//        console.log(rowid);
//        console.log(rowData);
//    }
//});


