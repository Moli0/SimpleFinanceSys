//import { setTimeout } from "timers";

var _open = function (options) {
    if (options.type == "2") {
        if ($.type(options.content) == "string") {
            options.content = [options.content, "no"]
        }
    }
    if ((!!options.width) && (!!options.height)) {
        if ($.type(options.width) == "string") {
            options.area = [options.width, options.height];
        } else if ($.type(options.width) == "number") {
            options.area = [options.width + "px", options.height + "px"];
        }
        delete options.width;
        delete options.height;
    } else if (!!options.width) {
        if ($.type(options.width) == "string") {
            options.area = options.width;
        } else if ($.type(options.width) == "number") {
            options.area = options.width + "px";
        }
        delete options.width;
    }
    layer.open(options);
}

var _form = function (id) {
    return new initForm(id);
}

function initForm(id) {
    this.elem = id;
    this.check = function () { //表单必填验证
        var res = true;
        var elems = $(this.elem).find("[required]");
        $.each(elems, function (i, item) {
            if ($(item).val() == '') {
                $(item).fadeToggle(500, function () {
                    $(item).fadeToggle(500, "linear", function () {
                        $(item).focus();
                    });
                });
                res = false;
                return;
            }
        });
        if (!res) {
            _alert("请将数据填写完整", "warning");
        }
        return res;
    }
    this.serializeArray = function (data) {
        for (key in data) {
            $(this.elem).find("#" + key).val(data[key]);
        }
    }
    this.bindSelect = function (options) {
        var param = {};
        var idCulomnName = "";
        var textCulomnName = "";
        var ajaxType = "POST";
        var dataType = "json";
        var selectElem = this.elem;
        if (options.url == null || options.url == undefined) {
            return;
        }
        if (options.data != null && options.data != undefined) {
            param = options.data;
        }
        if (options.id == null || options.id == undefined) {
            idCulomnName = "id";
        } else {
            idCulomnName = options.id;
        }
        if (options.text == null || options.text == undefined) {
            textCulomnName = "text";
        } else {
            textCulomnName = options.text;
        }
        if (options.type == null || options.type == undefined) {
            ajaxType = "POST";
        } else {
            ajaxType = options.type;
        }
        if (options.dataType == null || options.dataType == undefined) {
            dataType = "json";
        } else {
            dataType = options.dataType;
        }
        $.ajax({
            url: options.url,
            data: param,
            async: false,
            dataType: "json",
            type: ajaxType,
            success: function (res) {
                if (dataType == "json") {
                } else {
                    res.model = JSON.parse(res.model);
                }
                var html = "";
                if (options.defaultNull == null || options.defaultNull == undefined) {
                    html += "<option value=''>==请选择==</option>";
                } else {
                    html += $(selectElem).html();
                }
                $.each(res.model, function (i, item) {
                    html += "<option value='" + item[idCulomnName] + "'>" + item[textCulomnName] + "</option>";
                });
                $(selectElem).html(html);
            }
        });
    }
}

function _getCookie(key) {
    var idIndex = document.cookie.indexOf(key + "="); //expire=
    if (idIndex != -1) {
        idIndex += 7;
        var indexEnd = top.document.cookie.indexOf(";", idIndex);
        if (indexEnd != -1) {
            var useridCookie = document.cookie.substring(idIndex, indexEnd);
        } else {
            var useridCookie = document.cookie.substring(idIndex, document.cookie.length);
        }
    }
    return useridCookie;
}

///
///上传文件
///file:文件
///function(res)：上传成功的回调函数
///
function UploadFile(file, fun) {
    var fr = new FileReader();
    fr.readAsDataURL(file);
    var lastName = file.name.substr((file.name).lastIndexOf('.') + 1);
    var lastNameIndex = 0;
    switch (lastName.toLowerCase()) {
        case "jpg":
        case "jpeg": lastNameIndex = 0; break;
        case "bmp": lastNameIndex = 1; break;
        case "gif": lastNameIndex = 2; break;
        case "png": lastNameIndex = 3; break;
        default: _alert("无法识别的类型"); return;
    }
    fr.onloadend = function (e) {
        var base64Data = e.target.result;
        var loadShard = layer.load(1, {
            skin: 'align-center',
            area: ['auto', '80px'],
            shade: [0.3, '#000'], //0.3透明度的白色背景
            content: '<div style="position:absolute;bottom:0px;font-size:20px;width:120px;margin-left:-30px;color:white;">正在上传...</div>'
        });
        $.ajax({
            //url: '/Share/UploadImgFile',
            url: '/Public/UploadImgFile',
            type:'post',
            dataType: "json",
            async: false,
            data: { fileBase64Str: base64Data, fileType: lastNameIndex },
            success: function (res) {
                layer.close(loadShard);
                fun(res.msg);
            },
            error: function (msg) {
                layer.close(loadShard)
                _alert(msg, "error");
            }
        });
    }
}
successFileIndex = 0;
///
///上传文件列表
///files:文件列表
///fun(url,file)单个文件上传成功的回调函数
///
function UploadFiles(files, fun) {
    var passLoad = true;
    successFileIndex = 0;
    if (successFileIndex < files.length) {
        recLoad(files, fun, passLoad);
    } else {
        passLoad = true;
    }
}

function recLoad(files, fun, passLoad) {
    if (successFileIndex < files.length) {
        if (passLoad) {
            passLoad = false;
            UploadFile(files[successFileIndex], function (res) {
                successFileIndex++;
                passLoad = true;
                fun(res, files[successFileIndex - 1]);
                recLoad(files, fun, passLoad);
            });
            recLoad(files, fun, passLoad);
        } else {
            setTimeout(function () {
                recLoad(files, fun, passLoad);
            }, 500)
        }
    } else {
        successFileIndex = 999999;
        passLoad = true;
    }
}