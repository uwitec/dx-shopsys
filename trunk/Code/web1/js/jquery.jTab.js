;(function($){
	$.fn.extend({
		"jTab":function(o){
			   o = $.extend({
					  menutag:"",          //选项卡按钮标签
					  boxtag:"",           //选项卡内容标签
					  cur:0,               //默认显示索引
					  act:"click",         //切换动作，默认是点击click，还可以是鼠标经过mousemover
					  fade:0,              //淡入时间（毫秒），0表示无淡入效果
					  auto:false,          //自动播放
					  autotime:2000,       //自动播放间隔时间（毫秒）
					  callback:function(){} //动态写入数据（2个参数，1要显示的内容，2索引从0开始）
			   },o);
			  $(o.menutag).eq(o.cur).addClass("cur");
			  $(o.boxtag).eq(o.cur).siblings().hide();
			  var index = o.cur;
			  var $this = $(this);
			   $(o.menutag).bind(o.act,function(){
					  $(this).addClass("cur").siblings().removeClass("cur");
					  index = $(o.menutag).index(this);
					  $(o.boxtag).eq(index).fadeIn(o.fade).siblings().hide();
					  o.callback($(o.boxtag).eq(index),index);

			   }).hover(
			   function(){
				   $(this).addClass("hover");
			   },function(){
				   $(this).removeClass("hover");
			   })
			   //自动播放
			   if(o.auto){
					var len = $(o.menutag).length;
					var drive = function(){
						   $(o.menutag).eq(index).addClass("cur").siblings().removeClass("cur");
						   $(o.boxtag).eq(index).fadeIn(o.fade).siblings().hide();
						   index++;
						   if(index==len) index = 0;
					}
				   $this[0].t = null;
				   $this.hover(
				   function(){
					   clearInterval($this[0].t);
				   },
				   function(){
					   $this[0].t = setInterval(drive,o.autotime);
				   }).trigger("mouseleave");
			 }
		}
	});
})(jQuery);

