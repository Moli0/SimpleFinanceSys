var imgs = $("#box img"); //所有要轮播的img,位置以重叠的方式(即所有的img的位置都在一个位置)
var ifps = 0; //帧数控制变量
var no = 0; //图片序号
var t; //setTimeout控制变量
var changet; //过渡时的setTimeOut控制变量

/**
 * 图片选择与轮换
 **/
function play() {
    var len = $(imgs).length;
    var oldObj = $($(imgs)[no]);
    var newObj;
    if (no < len - 1) {
        no++;
    } else {
        no = 0;
    }
    newObj = $($(imgs)[no]);
    change(oldObj, newObj);
}

/**
 * 过渡到下一个画面
 * oldObj 当前的DOM
 * newObj 下一个DOM
 **/
function change(oldObj, newObj) {
    if (ifps < 1) {
        ifps += 0.05; //20帧,帧数控制,计算方式 (1 / ifps)
        $(oldObj).css("opacity", 1 - ifps);
        $(newObj).css("opacity", ifps);
        changet = setTimeout(function () {
            change(oldObj, newObj);
        }, 30); //.6s完成动画过渡,完成过江的时间,单位毫秒,总用时等于(帧数*毫秒),例30*20=600ms=0.6s
    } else {
        ifps = 0; //重围帧数控制变量
        $(oldObj).css("opacity", "0");
        $(newObj).css("opacity", "1");
        return 0;
    }
}

//循环轮播
function load() {
    play();
    t = setTimeout("load()", 5000);
}

/**
 *下一张
 **/
function nextImg() {
    clearTimeout(t);
    clearTimeout(changet);
    $($(imgs)[no]).css("opacity", "1");
    play();
    t = setTimeout("load()", 5000);
}

/**
 *上一张
 **/
function lastImg() {
    clearTimeout(t);
    clearTimeout(changet);
    $($(imgs)[no]).css("opacity", "1");
    var len = $(imgs).length;
    var oldObj = $($(imgs)[no]);
    var newObj;
    if (no < 1) {
        no = len - 1;
    } else {
        no--;
    }
    newObj = $($(imgs)[no]);
    change(oldObj, newObj);
    t = setTimeout("load()", 5000);
}


$(function () {
    //选项卡点击切换样式
    //运行轮播
    load();
    $("#myTab a").click(function (e) {
        $.each($($($(this).parent()).parent().find("li")), function (i, item) {
            $(item).attr("class", null);
        });
        $($(this).parent()).attr("class", "active");
    });
//html参照代码
//<ul id="myTab" class="nav nav-tabs">
//    <li class="active"><a href="#bulletin" data-toggle="tab">详细介绍</a></li>
//    <li><a href="#rule" data-toggle="tab">交流中心</a></li>
//</ul>
});
