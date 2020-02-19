///************主程序js文件

var locationX = 0;
var locationY = 0;

function GetPaginParam() {
    var param = {
        NowPage: 1,
        PageSize: 30,
        sortStr: " id desc ",
    };
    return param;
}

//var navTitle = new Vue({
//    el: '#my_nav',
//    data: {
//        navTitle: '后台管理'
//    }
//});

//var navList = new Vue({
//    el: '#mainNav',
//    data: {
//        navs: [],
//        topNavs: [],
//        secondNavs: []
//    }
//});
function showLoad(str) {
    var shade = layer.load(1, {
        skin: 'align-center',
        area: ['auto', '80px'],
        shade: [0.3, '#000'], //0.3透明度的白色背景
        content: '<div style="position:absolute;bottom:0px;font-size:20px;width:100px;margin-left:-30px;color:white;">' + str + '</div>'
    });
    return shade;
}

function showLoadAll(str) {
    layer.load(1, {
        skin: 'align-center',
        area: ['auto', '80px'],
        shade: [1, '#fff'], //0.3透明度的白色背景
        content: '<div style="position:absolute;bottom:0px;font-size:20px;width:100px;margin-left:-30px;color:white;">' + str + '</div>'
    });
}

//load(); //加载导航
function load() {
    $(document).mousedown(function (e) {
        locationX = e.pageX;
        locationY = e.pageY;
    });
    var url = '/AdminLin/Index/GetNavs';
    var lw = showLoad('正在加载');
    $.ajax({
        url: url, async: true, type: 'post', dataType:'json',success: function (json) {
            layer.close(lw);
            if (json.code === 1) {  //导航加载失败
                layer.open({
                    type: 0,
                    title: '提示',
                    btn: ['重新加载', '取消'],
                    content: json.msg,
                    btn1: function () { load(); }
                });
            }
            else {
                json.model = JSON.parse(json.model);
                $.each(json.model, function (i, item) {
                    if (item.POWER === 0) {
                        navList.topNavs.push(item);
                    }
                    else if (item.POWER === 1) {
                        navList.secondNavs.push(item);
                    }
                    else {
                        navList.navs.push(item);
                    }
                });
            }
        }
    });
}

function GetMouseLocation() {
    var location = {
        x: locationX,
        y: locationY
    };
    return location
}

var selecDom;
function ControllBtns(obj) {
    if (selecDom == obj) {
        var dom = obj;
        if ($(dom).find(".tb_btns").css("display") == "none") {
            $(dom).find(".tb_context").hide();
            $(dom).find(".tb_btns").show();
        } else {
            $(dom).find(".tb_btns").hide();
            $(dom).find(".tb_context").show();
        }
    } else {
        dom = selecDom;
        $(dom).find(".tb_btns").hide();
        $(dom).find(".tb_context").show();
        selecDom = obj;
        dom = selecDom;
        $(dom).find(".tb_context").hide();
        $(dom).find(".tb_btns").show();
    }
}