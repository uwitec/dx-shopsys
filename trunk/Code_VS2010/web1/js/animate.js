function getStyle(elem,name){
	if(elem.style[name]){
		return elem.style[name];
	}else if(elem.currentStyle){
		return elem.currentStyle[name];
	}else if(document.defaultView && document.defaultView.getComputedStyle){
		name = name.replace(/([A-Z])/g,"-$1");
		name = name.toLowerCase();
		var s = document.defaultView.getComputedStyle(elem,"");
		return s && s.getPropertyValue(name);
	}else{
		return null;
	}
}

function animate(obj,json,func){
	
	clearInterval(obj.timer);
	
	obj.timer = setInterval(function(){
		var bStop=true;
		for(var name in json){
			
			var cur;
			var iTarget = json[name];
			
			if(name =="opacity"){
				cur = Math.round(parseFloat(getStyle(obj,name))*100);
			}
			else{
			 	cur = parseInt(getStyle(obj,name));
			}
			
			var speed = (iTarget - cur)/8;
			speed = speed>0?Math.ceil(speed):Math.floor(speed);
			
			if(cur!=iTarget){
				bStop = false;
			}

			if(name == "opacity"){
				obj.style.filter = "alpha(opacity:"+(cur+speed)+")";
				obj.style.opacity = (cur+speed)/100;
				
			}
			else{
				obj.style[name] = cur+speed+"px";
			}
	
		}
		if(bStop){
			clearInterval(obj.timer);
			if(func) func();
		}
	},30);
	
}
function mapOver(e,province){
	$("#mapTxt").css({width:0});
	$("#mapTxtSpan").css({opacity:0});	
	var txt,w;
	
		switch(province){		
			case 'hlj':
			    txt = "黑龙江";
				w = 120;
				break;
			case 'jl': 
			    txt = "吉林";
				w = 120
			    break;
			case 'ln':
				txt = "辽宁";
				w = 100
			    break;
			case 'bj':
				txt = "北京";
				w = 150;
			    break;
			case 'sh':
				txt = "上海";
				w = 50;
			    break;
			case 'fj':
				txt = "福建";
				w = 70;
			    break;
			case 'zj':
				txt = "浙江";
				w = 100;
			    break;
			case 'hb':
				txt = "湖北";
				w = 150
			    break;
			case 'sx':
				txt = "陕西";
				w = 200
			    break;	
			case 'jx':
				txt = "江西";
				w = 120
			    break;
			case 'sd':
				txt = "山东";
				w = 120
			    break;
			case 'js':
				txt = "江苏";
				w = 80;
			    break;
			case 'xj':
				txt = "新疆";
				w = 150;
				break;
	}
	provinceMove(txt,province,w);
	changeContent(province);
}
function provinceMove(txt,value,w){
	var pos = $("#map ."+value).offset();
	var x=24;
	if(value=='hlj'){
		x=36;
	}
	$("#mapTxt").css({left:pos.left,top:parseInt(pos.top)});
	$("#mapTxtSpan").css({left:pos.left+w-x,top:parseInt(pos.top-16)}).text(txt);
	animate($("#mapTxt").get(0),{width:w},function(){animate($("#mapTxtSpan").get(0),{opacity:100})})	
}

function changeContent(province){
	$(".mapcontent .tbl_"+province).show().siblings().hide();
}