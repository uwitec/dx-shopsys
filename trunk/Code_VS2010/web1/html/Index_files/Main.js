/*ShowLeagueArea*/
function ShowLeagueArea(ObjName,ObjNameIn,Line,MarqueeWidth,MinLine){
	LeagueAreaObjName = ObjName;
	LeagueAreaObjNameIn = ObjNameIn;
	LeagueAreaMarqueeWidth = MarqueeWidth;
	var Obj = document.getElementById(ObjName);
	var ObjIn = document.getElementById(ObjNameIn);
	if(Line>0 && Line>=MinLine){
		LeagueAreaAllWidth = Line*MarqueeWidth;
		LeagueAreaStopScroll = false;
		with(ObjIn)
		{
			style.width=LeagueAreaAllWidth*3+"px";
			noWrap=true;
			onmouseover = new Function("LeagueAreaStopScroll=true");
			onmouseout = new Function("LeagueAreaStopScroll=false");
		}
		LeagueAreaCurrentLeft = MarqueeWidth;
		LeagueAreaStopTime = 0;
		ObjIn.innerHTML += ObjIn.innerHTML + ObjIn.innerHTML;
		Obj.scrollLeft=LeagueAreaAllWidth;
		LeagueAreaInterval = setInterval("LeagueAreaScrollTextUp()",1);
	}else{
		//document.getElementById("LeftArrow").className = "LeftArrowNonAct";
		//document.getElementById("RightArrow").className = "RightArrowNonAct";		
	}
}
function LeagueAreaScrollTextUp()
{
	var Obj = document.getElementById(LeagueAreaObjName);
	if(LeagueAreaStopScroll==true) return;
	var Count = LeagueAreaMarqueeWidth%LeagueAreaSpeed;
	var LeagueAreaSpeedTmp = LeagueAreaSpeed;
	LeagueAreaCurrentLeft+=LeagueAreaSpeedTmp;	
	if(LeagueAreaCurrentLeft>LeagueAreaMarqueeWidth || LeagueAreaCurrentLeft<-LeagueAreaMarqueeWidth){
		LeagueAreaCurrentLeft-=LeagueAreaSpeedTmp;
		LeagueAreaSpeedTmp = Count * (LeagueAreaSpeed / Math.abs(LeagueAreaSpeed));
		LeagueAreaCurrentLeft+=LeagueAreaSpeedTmp;	
	}
	if(LeagueAreaCurrentLeft>=LeagueAreaMarqueeWidth+LeagueAreaSpeedTmp || LeagueAreaCurrentLeft<=-(LeagueAreaMarqueeWidth)+LeagueAreaSpeedTmp)
	{
		LeagueAreaScrollNow = false;
		LeagueAreaStopTime+=1;
		LeagueAreaCurrentLeft-=LeagueAreaSpeedTmp;
		if(LeagueAreaStopTime==LeagueAreaDelay) 
		{
			LeagueAreaCurrentLeft=0;
			LeagueAreaStopTime=0;    
		}
	}else{
		LeagueAreaScrollNow = true;
		Obj.scrollLeft+=LeagueAreaSpeedTmp;
		if(Obj.scrollLeft>=LeagueAreaAllWidth*2-5 || Obj.scrollLeft<=0)
		{
			Obj.scrollLeft=LeagueAreaAllWidth;
			LeagueAreaCurrentLeft=LeagueAreaMarqueeWidth;
		}
	}
}
function LeagueAreaScrollLR(d)
{
	if(!LeagueAreaScrollNow){
		clearInterval(LeagueAreaInterval);
		if(d=="L"){LeagueAreaSpeed=-Math.abs(LeagueAreaSpeed);}else{LeagueAreaSpeed=Math.abs(LeagueAreaSpeed);}
		LeagueAreaCurrentLeft=0;
		LeagueAreaStopTime=0; 
		LeagueAreaInterval = setInterval("LeagueAreaScrollTextUp()",1);
	}
}
/*ShowLeagueArea*/