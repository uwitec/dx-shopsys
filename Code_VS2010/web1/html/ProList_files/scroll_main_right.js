var lastScrollY=135;
var diffY=0,percent=0,oH_Now=0;
function heartBeat(){
	diffY=document.body.scrollTop?document.body.scrollTop:document.documentElement.scrollTop;
	oH_Now=document.all.main_qqright.style.pixelTop
	if(diffY-lastScrollY>0 || oH_Now!=135){
		percent=.1*(diffY-lastScrollY);
		if(percent>0)percent=Math.ceil(percent);
		else percent=Math.floor(percent);
		
		if(oH_Now<135)
			document.all.main_qqright.style.pixelTop=135;
		else
			document.all.main_qqright.style.pixelTop+=percent;
		
		lastScrollY=lastScrollY+percent;
	}
}
window.setInterval("heartBeat()",5);