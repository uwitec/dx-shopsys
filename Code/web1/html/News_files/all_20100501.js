var TQKF = {
	version: "20100501",
	date: "2013-11-11",
	js_url: "http://sysimages.tq.cn/js/vip/100501/",
	image_url: "http://sysimages.tq.cn/",
	swf_url: "http://sysimages.tq.cn/js/vip/",
	vip_server_url: "http://vip.tq.cn",
	invite_server_url: "http://vipwebscreen.tq.cn",
	webchat_url: "http://vipwebchat.tq.cn",
	domain_url: "http://weibo.com/tqcn?m=tqpf_" + tq_adminid,
	isUpdateOnlineFlag: (typeof(tq_is_update_online_flag) == 'undefined' || tq_is_update_online_flag == '') ? 0 : tq_is_update_online_flag,
	float_server_url: (typeof(tq_floatserver_url) == 'undefined' || tq_floatserver_url == '') ? "http://float2006.tq.cn": tq_floatserver_url,
	getOnlineFlagInteval: 1000 * 2,
	hashChatURL: (typeof(tq_hashchat_url) == 'undefined' || tq_hashchat_url == '') ? "": tq_hashchat_url,
	local_url_length: 400,
	reffer_url_length: 300,
	getIsNewMsgIntever: 1000 * 10,
	appDelayTime: 1000 * 1,
	appInterval: tq_if_tryout == 0 ? 6 * 1000 : 5 * 1000,
	not_reshow_time: (typeof(tq_invit_show_time) == 'undefined' || tq_invit_show_time == '') ? 3 : tq_invit_show_time,
	not_reshow_time_site: (typeof(tq_invit_domain_show_time) == 'undefined' || tq_invit_domain_show_time == '') ? 10 : tq_invit_domain_show_time,
	auto_reshow_time: (typeof(tq_invit_reshow_inteval) == 'undefined' || tq_invit_reshow_inteval == '') ? 15 : tq_invit_reshow_inteval,
	mini_interval_of_bad_click: 60,
	source_page_life_time: 30,
	open_win_w: 600,
	open_win_h: 480,
	tq_media_param_name: "tq_media_param",
	tq_media_param_value: "tq_media_param",
	filterURLs: [{
		domain: ["baidu.com", "baidu.com.cn", "baidu.cn"],
		keyPara: "wd"
	},
	{
		domain: ["google.com", "google.com.cn", "google.cn", "google.com.hk"],
		keyPara: "q"
	},
	{
		domain: ["sogou.com", "sogou.com.cn", "sogou.cn"],
		keyPara: "query"
	},
	{
		domain: ["soso.com", "soso.com.cn", "soso.cn"],
		keyPara: "w"
	},
	{
		domain: ["yahoo.com"],
		keyPara: "p"
	},
	{
		domain: ["yahoo.cn"],
		keyPara: "q"
	}],
	color: ["#AECEF8", "#58B7F6", "#EEB3F7", "#F67171", "#FAB776", "#F9DB9F", "#90DA94", "#8FD2C3", "#DDE0E2", "#606060"],
	isFlashFixed: false,
	OFFLINE: "0",
	ONLINE: "22|28",
	BUSY: "24|33",
	LEAVE: "26|25|23|20",
	userDefined: {
		floatJS: "http://sysimages.tq.cn/js/vip/100501/float.js",
		inviteJS: "http://sysimages.tq.cn/js/vip/100501/invite.js",
		kefuImgJS: "http://sysimages.tq.cn/js/vip/100501/kefuimg.js",
		minimessJS: "http://sysimages.tq.cn/js/vip/100501/minimess_core.js",
		asJS: "http://sysimages.tq.cn/js/vip/100501/as.js",
		antibadclickJS: "http://sysimages.tq.cn/js/vip/100501/antibadclick.js",
		minichatJS: "http://sysimages.tq.cn/js/vip/100501/minichat.js",
		skypeJS: "http://skype.tom.com/script/skypeCheck40.js",
		inviteInitDelay: 500,
		defer: false,
		forceLoadRequiredJS: 3,
		forceWriteCSS: 0,
		debug: 0,
		creatDOMType: 1,
		loadCSSType: "auto",
		tacticFloaterWidth: "auto",
		appendDOMType: "insertBefore",
		isfloat: 1,
		float_member_h: "auto",
		maxFloatMemberNameChars: 8,
		openWinType: 1,
		isNotCreatInvit: "0",
		kefuCloseArea: {
			isShow: 0,
			width: 20,
			height: "20",
			rightSpace: "5",
			top: "5"
		},
		kefuMiniArea: {
			isShow: 0,
			width: "20",
			height: "20",
			rightSpace: "5",
			miniImgOffline: "a.gif",
			miniImgOnline: "b.gif",
			miniImgWidth: "80",
			miniImgHeight: "30",
			miniPos: "RB"
		},
		kefuClickEvent: null
	}
};
if (typeof(tq_webadmin_url) != "undefined" && tq_webadmin_url != "") {
	TQKF.vip_server_url = tq_webadmin_url
}
if (typeof(tq_WebAdmin_url) != "undefined" && tq_WebAdmin_url != "") {
	TQKF.vip_server_url = tq_WebAdmin_url
}
if (TQKF.vip_server_url.indexOf("/vip/") != -1) {
	TQKF.vip_server_url = TQKF.vip_server_url.replace("/vip/", "")
}
if (typeof(tq_admin_url) != "undefined" && tq_admin_url != "") {
	TQKF.vip_server_url = tq_admin_url
}
TQKF.isAgent = (typeof(tq_chat_url) != "undefined" || typeof(tq_admin_url) != "undefined" || typeof(tq_card_url) != "undefined" || typeof(tq_chat_logo) != "undefined");
TQKF.page_url = "";
TQKF.local_url = "";
TQKF.source_page_url = "";
TQKF.reffer_url = "";
TQKF.visit_title = "";
TQKF.float_image_url = TQKF.image_url + "images/vip/float/100501/",
TQKF.invite_image_url = TQKF.image_url + "images/vip/invit/100501/",
TQKF.kefuimg_image_url = TQKF.image_url + "images/vip/kefuimg/100501/",
TQKF.leavemsg_url = "http://vipwebchat.tq.cn/pageinfo.jsp";
TQKF.webchat_url2 = "http://vipwebchat.tq.cn/pageinfo.jsp";
TQKF.pageinfo_url = "http://vipwebchat.tq.cn/pageinfo.jsp";
TQKF.winSize = window.screen.width + "*" + window.screen.height;
TQKF.winColor = window.screen.colorDepth;
TQKF.isFloater = (tq_type == "" || tq_type == "1" || tq_type == "4");
TQKF.isKefuImg = (tq_type == "2");
if ((typeof(tq_useraccount) == "undefined" || tq_useraccount == "" || tq_useraccount < "1") && tq_type == "3") TQKF.isInviter = 1;
else TQKF.isInviter = tq_useraccount;
TQKF.isMinimess = (typeof(tq_is_minimess) != "undefined" && tq_is_minimess == 1);
TQKF.isMinimessLoaded = false;
TQKF.jsloaded = false;
TQKF.ltype_str = "";
TQKF.uingroup = "";
TQKF.onlineUins = new Array();
TQKF.words = new Array();
TQKF.words_zh = new Array();
TQKF.words_en = new Array();
TQKF.floater = new Object();
TQKF.inviter = new Object();
TQKF.AS = new Object();
TQKF.miniChat = new Object();
TQKF.miniChatmini = new Object();
TQKF.mess = new Object();
TQKF.antiBadClick = new Object();
TQKF.clientInfo = new Object();
TQKF.cookieObject_all = new Object();
TQKF.words_zh[0] = unescape("TQ%u6D3D%u8C08%u901A");
TQKF.words_zh[1] = unescape("%u5173%u95ED");
TQKF.words_zh[2] = unescape("%u7F51%u7AD9%u7BA1%u7406%u5458%u8BF7%u6C42%u548C%u60A8%u901A%u8BDD%uFF08%u672C%u7CFB%u7EDF%u7531TQ%u63D0%u4F9B%uFF09");
TQKF.words_zh[3] = unescape("%u70B9%u51FB%u54A8%u8BE2");
TQKF.words_zh[4] = unescape("%u60A8%u597D%uFF01%u80FD%u4E3A%u60A8%u505A%u4E9B%u4EC0%u4E48%u5417%uFF1F");
TQKF.words_zh[5] = unescape("%u5B8C%u6BD5");
TQKF.words_zh[6] = unescape("%u70B9%u51FB%u7559%u8A00");
TQKF.words_zh[7] = unescape("%u5728%u65B0%u7A97%u53E3%u4E2D%u6253%u5F00");
TQKF.words_zh[8] = unescape("%u6700%u5C0F%u5316");
TQKF.words_zh[9] = unescape("\n\n%u8B66%u544A%uFF01%u672C%u7AD9%u53D1%u73B0%u6076%u610F%u70B9%u51FB%u5ACC%u7591%uFF0C%u60A8%u5DF2%u88AB%u5217%u5165%u9ED1%u540D%u5355%uFF0C\n\n\n\n%u8BF7%u7ACB%u5373%u7EC8%u6B62%u6076%u610F%u884C%u4E3A%u5E76%u91CD%u542F%u7535%u8111%uFF0C%u5426%u5219%u7535%u8111%u5C06%u53D7%u635F%uFF01\n\n");
TQKF.words_zh[10] = unescape("%u8BA4%u5B9A%u6076%u610F%u70B9%u51FB%uFF01");
TQKF.words_zh[11] = unescape("%u786E%u5B9A%u8981%u9000%u51FA%u5417?");
TQKF.words_zh[12] = unescape("%u5728%u7EBF");
TQKF.words_zh[13] = unescape("%u7559%u8A00");
TQKF.words_zh[14] = unescape("%u514D%u8D39%u7535%u8BDD");
TQKF.words_zh[15] = unescape("%u6B22%u8FCE%u60A8%uFF0C%u6765%u81EA%24%7Barea%7D%u7684%u5BA2%u4EBA%uFF01");
TQKF.words_zh[16] = unescape("%u8FD8%u539F");
TQKF.words_zh[17] = unescape("%u7E41%u5FD9");
TQKF.words_zh[18] = unescape("%u7F51%u7AD9%u7BA1%u7406%u5458%u8BF7%u6C42%u548C%u60A8%u901A%u8BDD");
TQKF.words_zh[19] = unescape("%u79BB%u5F00");
TQKF.words_en[0] = "TQ Messenger";
TQKF.words_en[1] = "close";
TQKF.words_en[2] = "Apply to talk with you! (Powered by TQ.CN)";
TQKF.words_en[3] = "Click here";
TQKF.words_en[4] = "Hello!Can I help you?";
TQKF.words_en[5] = "finish";
TQKF.words_en[6] = "Message";
TQKF.words_en[7] = "Open in new Window";
TQKF.words_en[8] = "Minimize";
TQKF.words_en[9] = "Worning!You had written in blacklist for your bad clicking,please stop to do so,or your computer will be harmed!";
TQKF.words_en[10] = "bad click!";
TQKF.words_en[11] = "are you sure to exit?";
TQKF.words_en[12] = "Online";
TQKF.words_en[13] = "Msg";
TQKF.words_en[14] = "Free Call";
TQKF.words_en[15] = "Welcome, visitor from ${area}!";
TQKF.words_en[16] = "Restore";
TQKF.words_en[17] = "Busy";
TQKF.words_en[18] = "Apply to talk with you!";
TQKF.words_en[19] = "Leave";
TQKF.CreatURL = function(url, action, uin, typecode, clickSource) {
	if (TQUtils.IsUndefined('TQ_RQF')) {
		TQ_RQF = "-1";
		if (clickSource == "float") TQ_RQF = 5;
		else if (clickSource == "invite") TQ_RQF = 1;
		else if (clickSource == "kefuimg") TQ_RQF = 6;
		else if (clickSource == "minichat") TQ_RQF = 4;
		else if (clickSource == "minichatMax") TQ_RQF = 7;
		else if (clickSource == "unknown") TQ_RQF = "-1"
	}
	if (TQUtils.IsUndefined('TQ_RQC')) {
		TQ_RQC = "-1";
		if (clickSource == "invite") {
			TQ_RQC = 1
		}
	}
	if (!TQUtils.IsUndefined('tq_clientname')) {
		if (tq_clientname.indexOf("^") == -1) {
			tq_clientname = encodeURI(tq_clientname).replace(/%/gi, "^")
		}
	}
	var argv = TQKF.CreatURL.arguments;
	var argc = TQKF.CreatURL.arguments.length;
	var str = "?";
	var _siteid = "";
	if (! (TQUtils.IsUndefined('tq_siteid') || tq_siteid == "")) _siteid = tq_siteid;
	if (url.indexOf("?") != -1) str = "&";
	var tracq_url = url + str + "action=" + action + "&admiuin=" + tq_adminid + ((uin == "") ? "": "&uin=" + uin) + ((action.indexOf("acd") == -1) ? "": ("&acd=1&type_code=" + (typecode == "undefined" || typecode == "" || typecode == null ? tq_tactic_id: typecode))) + "&RQF=" + TQ_RQF + "&RQC=" + TQ_RQC + "&ltype=" + tq_language + "&sort=" + tq_sort + "&lasttalkuin=" + ((TQUtils.IsUndefined('TQKF.clientInfo') || TQUtils.IsUndefined('TQKF.clientInfo.pu') || TQKF.clientInfo.pu == "undefined") ? "": this.clientInfo.pu) + ((this.chattype == '') ? "": "&chattype=" + this.chattype + "&isnoshowuser=" + tq_kefuimg_cfg.isnoshowuser + "&uingroup=" + this.uingroup) + "&rand=" + ((TQUtils.IsUndefined('TQKF.clientInfo') || TQUtils.IsUndefined('TQKF.clientInfo.r')) ? "": this.clientInfo.r) + "&comtimes=" + ((TQUtils.IsUndefined('TQKF.clientInfo') || TQUtils.IsUndefined('TQKF.clientInfo.ct')) ? "": this.clientInfo.ct) + "&iscallback=" + tq_iscallback + ((TQUtils.IsUndefined('tq_isFullScreenLeavmsg') || tq_isFullScreenLeavmsg == '') ? "": "&isFullScreenLeavmsg=" + tq_isFullScreenLeavmsg) + ((this.tq_media_param_value == "") ? "": "&" + this.tq_media_param_name + "=" + this.tq_media_param_value) + "&agentid=" + tq_agentid + ((tq_clientid == "") ? "": "&clientid=" + tq_clientid) + ((_siteid == "") ? "": "&siteid=" + _siteid) + ((TQUtils.IsUndefined('tq_page_templete_id') || tq_page_templete_id == '') ? "": "&page_templete_id=" + tq_page_templete_id) + ((TQUtils.IsUndefined('tq_software_name') || tq_software_name == '') ? "": "&software_name=" + tq_software_name) + ((TQKF.hashChatURL == '') ? "": "&hashChatURL=" + TQKF.hashChatURL) + "&isAgent=" + (TQKF.isAgent == true ? "1": "0") + ((TQUtils.IsUndefined('tq_is_message_sms') || tq_is_message_sms == '') ? "": "&is_message_sms=" + tq_is_message_sms) + ((TQUtils.IsUndefined('tq_is_send_mail') || tq_is_send_mail == '') ? "": "&is_send_mail=" + tq_is_send_mail) + ((TQUtils.IsUndefined('tq_chat_url') || tq_chat_url == '') ? "": "&chat_url=" + tq_chat_url) + ((TQUtils.IsUndefined('tq_card_url') || tq_card_url == '') ? "": "&card_url=" + tq_card_url) + ((TQUtils.IsUndefined('tq_chat_logo') || tq_chat_logo == '') ? "": "&chat_logo=" + tq_chat_logo) + ((TQUtils.IsUndefined('tq_logoLink') || tq_logoLink == '') ? "": "&logoLink=" + tq_logoLink) + ((TQUtils.IsUndefined('tq_admin_url') || tq_admin_url == '') ? "": "&admin_url=" + tq_admin_url) + ((TQUtils.IsUndefined('tq_clientname') || tq_clientname == '') ? "": "&clientname=" + tq_clientname) + ((TQUtils.IsUndefined('tq_infocard_url') || tq_infocard_url == '') ? "": "&tq_right_infocard_url=" + tq_infocard_url) + ((TQUtils.IsUndefined('tq_para1') || tq_para1 == '') ? "": "&para1=" + tq_para1) + ((TQUtils.IsUndefined('tq_para2') || tq_para2 == '') ? "": "&para2=" + tq_para2) + ((TQUtils.IsUndefined('tq_para3') || tq_para3 == '') ? "": "&para3=" + tq_para3) + ((TQUtils.IsUndefined('tq_para4') || tq_para4 == '') ? "": "&para4=" + tq_para4) + ((TQUtils.IsUndefined('tq_para5') || tq_para5 == '') ? "": "&para5=" + tq_para5) + ((url.indexOf("sendmain.jsp") == -1 && url.indexOf("pageinfo.jsp") == -1) ? ("&page=" + this.page_url) : ("&page=" + this.page_url.replace(/\^/g, "%"))) + "&localurl=" + this.local_url + ((url.indexOf("sendmain.jsp") == -1 && url.indexOf("pageinfo.jsp") == -1) ? ("&spage=" + this.source_page_url) : ("&spage=" + this.source_page_url.replace(/\^/g, "%"))) + (TQUtils.IsUndefined('_tq_collect_page_type') ? "": "&cpage=" + _tq_collect_page_type);
	return tracq_url
};
TQKF.OpenWindow = function(url, uin) {
	if (TQKF.userDefined.openWinType == 1) {
		if (url.indexOf("leavemsg") != -1 && tq_isFullScreenLeavmsg) window.open(url);
		else {
			window.open(url, "tq_webchat", "width=" + this.open_win_w + ",height=" + this.open_win_h + ",location=no,resizable=1,scrollbars=0,status=no,toolbar=no,menu=no,top=100,left=200")
		}
	} else {
		var link = document.createElement('A');
		link.href = url;
		link.innerHTML = "_";
		link.target = '_blank';
		TQUtils.AppendDOM(link);
		var callClick = function(element) {
			if (element.click) {
				element.click()
			} else if (element.fireEvent) {
				element.fireEvent('onclick')
			} else if (document.createEvent) {
				var evt = document.createEvent("MouseEvents");
				evt.initEvent("click", true, true);
				element.dispatchEvent(evt)
			}
		};
		callClick(link);
		link.parentNode.removeChild(link)
	}
};
TQKF.setWindowStatus = function() {
	try {
		window.status = TQKF.words[5]
	} catch(e) {}
	setTimeout(TQKF.setWindowStatus, 500)
};
TQKF.loadRequiredJS = function() {
	if (TQKF.jsloaded == true) {
		return
	}
	try {
		TQ_DEBUG("TQKF.loadRequiredJS", 3)
	} catch(e) {}
	TQKF.jsloaded = true;
	if (typeof(tq_member_uins) != "undefined" && tq_member_uins != "" && tq_member_uins.indexOf("skype") != -1) TQUtils.LoadJS(TQKF.userDefined.skypeJS, TQKF.userDefined.defer);
	if (TQKF.isFloater && TQKF.userDefined.floatJS != "none") TQUtils.LoadJS(TQKF.userDefined.floatJS, TQKF.userDefined.defer);
	if (TQKF.isKefuImg && TQKF.userDefined.kefuImgJS != "none") TQUtils.LoadJS(TQKF.userDefined.kefuImgJS, TQKF.userDefined.defer);
	if (TQKF.isInviter > 0 && TQKF.userDefined.inviteJS != "none") {
		TQUtils.LoadJS(TQKF.userDefined.inviteJS, TQKF.userDefined.defer)
	}
	if (TQKF.isMinimess) {
		TQUtils.LoadJS(TQKF.userDefined.minimessJS, TQKF.userDefined.defer);
		TQKF.isMinimessLoaded = true
	}
};
TQKF.SetValue = function(from, to) {
	for (var prop in to) {
		var temp = eval("typeof(tq_" + prop + ")!='undefined' && tq_" + prop + "!= null && tq_" + prop + " != 'null' && tq_" + prop + " != ''");
		if (temp) {
			to[prop] = eval("tq_" + prop)
		}
	}
};
TQKF.Binding = function(target, type, uin, tactic_code, is_hide_ui, ui) {
	if (typeof target == 'string') target = TQUtils.GetObj(target);
	if (target) {
		target.style.cursor = "pointer";
		TQUtils.AddEvent(target, "click",
		function() {
			try {
				TQKF.OpenWindow(TQKF.CreatURL(type == "leavemsg" ? TQKF.leavemsg_url: TQKF.webchat_url2, type, uin, tactic_code ? tactic_code: tq_tactic_id, "unknown"), uin);
				if (is_hide_ui) {
					if (typeof ui == 'string') ui = TQUtils.GetObj(ui);
					ui && (ui.style.display = "none")
				}
			} catch(e) {}
		})
	}
};
var TQUtils = {
	tq_fixJQueryMouseMoveEvent: null,
	browserType: navigator.userAgent.toLowerCase(),
	isDOM: (document.getElementById ? true: false),
	isNS4: (document.layers ? true: false),
	isNS: navigator.appName == "Netscape",
	IE: ((navigator.appName.toLowerCase() == "microsoft internet explorer") && (parseInt(navigator.appVersion) >= 4)),
	Chrome: navigator.userAgent.toLowerCase().indexOf("chrome") != -1,
	NS: (document.layers) ? 1 : 0,
	isIE4: document.all && !(document.getElementById),
	FF: navigator.userAgent.toLowerCase().indexOf("firefox") != -1,
	Se360: navigator.userAgent.toLowerCase().indexOf("360se") != -1,
	IsUndefined: function(name) {
		return eval("typeof(" + name + ")=='undefined'")
	},
	SetValueIfUndefined: function(name, value) {
		if (this.IsUndefined(name)) {
			var tqtemp = value;
			eval(name + "=tqtemp")
		}
	},
	SetValueIfUndefinedOrEmpty: function(name, value) {
		if (this.IsUndefined(name) || eval(name) === "") {
			var tqtemp = value;
			eval(name + "=tqtemp")
		}
	},
	SetDefaultValue: function(name, value) {
		if (name == "") name = value
	},
	LoadJS: function(url, isDefer) {
		try {
			var jsID = "TQJS" + Math.random();
			var jsDOM = document.createElement("script");
			document.getElementsByTagName("head")[0].appendChild(jsDOM);
			jsDOM.id = jsID;
			if (isDefer == true && TQUtils.IE == true) jsDOM.defer = "defer";
			jsDOM.src = url
		} catch(e) {}
	},
	LoadJS2: function(url, callback) {
		var jsID = "TQJS" + Math.random();
		var jsDOM = document.createElement("script");
		document.getElementsByTagName("head")[0].appendChild(jsDOM);
		jsDOM.id = jsID;
		jsDOM.src = url;
		try {
			jsDOM.onreadystatechange = function() {
				if (jsDOM.readyState == "loaded") callback()
			}
		} catch(e) {}
		try {
			jsDOM.onload = callback
		} catch(e) {}
	},
	LoadJSAndAutoRemoveSelf: function(scriptId, url) {
		{
			var jsID = "TQJS" + Math.random();
			var jsDOM = document.createElement("script");
			document.getElementsByTagName("head")[0].appendChild(jsDOM);
			jsDOM.id = jsID;
			jsDOM.src = url; {
				var callback = function() {
					try {
						setTimeout(function() {
							if (jsDOM && jsDOM.parentNode) jsDOM.parentNode.removeChild(jsDOM)
						},
						2000)
					} catch(e) {}
				};
				try {
					jsDOM.onreadystatechange = function() {
						if (jsDOM.readyState == "complete" || jsDOM.readyState == "loaded") callback()
					}
				} catch(e) {}
				try {
					jsDOM.onload = callback
				} catch(e) {}
			}
		}
	},
	LoadCSS: function(url) {
		if (TQKF.userDefined.loadCSSType == "auto") {
			try {
				var cssID = "TQCSS" + Math.random();
				var cssDOM = document.createElement("link");
				cssDOM.setAttribute("rel", "stylesheet");
				cssDOM.setAttribute("type", "text/css");
				cssDOM.rel = "stylesheet";
				cssDOM.type = "text/css";
				document.getElementsByTagName("head")[0].appendChild(cssDOM);
				cssDOM.id = cssID;
				cssDOM.href = url
			} catch(e) {}
			if (TQKF.userDefined.forceWriteCSS == 1) document.write('<link rel="stylesheet" type="text/css" href="' + url + '"/>')
		}
	},
	AppendDOM: function(dom) {
		if (TQKF.userDefined.appendDOMType == "appendChild") {
			try {
				document.body.appendChild(dom)
			} catch(e) {
				try {
					setTimeout(function() {
						document.body.appendChild(dom)
					},
					10000)
				} catch(e) {}
			}
		} else {
			try {
				document.body.insertBefore(dom, document.body.firstChild)
			} catch(e) {
				try {
					setTimeout(function() {
						document.body.appendChild(dom)
					},
					10000)
				} catch(e) {}
			}
		}
	},
	toJSONString: function(json) {
		if (Object.prototype.toJSONString) return json.toJSONString();
		else {
			var s = "{";
			for (var t in json) {
				if (typeof(json[t]) == "function") continue;
				if (typeof(json[t]) == "object" && json[t] != null) {
					s += (t + ":" + this.toJSONString(json[t]) + ",")
				} else if (json[t] != null) {
					s += (typeof(json[t]) == "number" ? (t + ":" + json[t] + ",") : (t + ":\"" + json[t] + "\","))
				}
			}
			s = s.substring(0, s.length - 1) + "}";
			return s
		}
	},
	Drag: {
		canDrag: true,
		obj: null,
		init: function(o, oRoot, minX, maxX, minY, maxY, bSwapHorzRef, bSwapVertRef, fXMapper, fYMapper) {
			o.onmousedown = TQUtils.Drag.start;
			o.hmode = bSwapHorzRef ? false: true;
			o.vmode = bSwapVertRef ? false: true;
			o.root = oRoot && oRoot != null ? oRoot: o;
			if (o.hmode && isNaN(parseInt(o.root.style.left))) o.root.style.left = "0px";
			if (o.vmode && isNaN(parseInt(o.root.style.top))) o.root.style.top = "0px";
			if (!o.hmode && isNaN(parseInt(o.root.style.right))) o.root.style.right = "0px";
			if (!o.vmode && isNaN(parseInt(o.root.style.bottom))) o.root.style.bottom = "0px";
			o.minX = typeof minX != 'undefined' ? minX: null;
			o.minY = typeof minY != 'undefined' ? minY: null;
			o.maxX = typeof maxX != 'undefined' ? maxX: null;
			o.maxY = typeof maxY != 'undefined' ? maxY: null;
			o.xMapper = fXMapper ? fXMapper: null;
			o.yMapper = fYMapper ? fYMapper: null;
			o.root.onDragStart = new Function();
			o.root.onDragEnd = new Function();
			o.root.onDrag = new Function()
		},
		start: function(e) {
			if (! (TQUtils.Drag.canDrag)) return;
			var o = TQUtils.Drag.obj = this;
			e = TQUtils.Drag.fixE(e);
			var y = parseInt(o.vmode ? o.root.style.top: o.root.style.bottom);
			var x = parseInt(o.hmode ? o.root.style.left: o.root.style.right);
			o.root.onDragStart(x, y);
			o.lastMouseX = e.clientX;
			o.lastMouseY = e.clientY;
			if (o.hmode) {
				if (o.minX != null) o.minMouseX = e.clientX - x + o.minX;
				if (o.maxX != null) o.maxMouseX = o.minMouseX + o.maxX - o.minX
			} else {
				if (o.minX != null) o.maxMouseX = -o.minX + e.clientX + x;
				if (o.maxX != null) o.minMouseX = -o.maxX + e.clientX + x
			}
			if (o.vmode) {
				if (o.minY != null) o.minMouseY = e.clientY - y + o.minY;
				if (o.maxY != null) o.maxMouseY = o.minMouseY + o.maxY - o.minY
			} else {
				if (o.minY != null) o.maxMouseY = -o.minY + e.clientY + y;
				if (o.maxY != null) o.minMouseY = -o.maxY + e.clientY + y
			}
			if (TQUtils.tq_fixJQueryMouseMoveEvent == null) TQUtils.tq_fixJQueryMouseMoveEvent = document.onmousemove;
			document.onmousemove = TQUtils.Drag.drag;
			document.onmouseup = TQUtils.Drag.end;
			return false
		},
		drag: function(e) {
			e = TQUtils.Drag.fixE(e);
			var o = TQUtils.Drag.obj;
			var ey = e.clientY;
			var ex = e.clientX;
			var y = parseInt(o.vmode ? o.root.style.top: o.root.style.bottom);
			var x = parseInt(o.hmode ? o.root.style.left: o.root.style.right);
			var nx, ny;
			if (o.minX != null) ex = o.hmode ? Math.max(ex, o.minMouseX) : Math.min(ex, o.maxMouseX);
			if (o.maxX != null) ex = o.hmode ? Math.min(ex, o.maxMouseX) : Math.max(ex, o.minMouseX);
			if (o.minY != null) ey = o.vmode ? Math.max(ey, o.minMouseY) : Math.min(ey, o.maxMouseY);
			if (o.maxY != null) ey = o.vmode ? Math.min(ey, o.maxMouseY) : Math.max(ey, o.minMouseY);
			nx = x + ((ex - o.lastMouseX) * (o.hmode ? 1 : -1));
			ny = y + ((ey - o.lastMouseY) * (o.vmode ? 1 : -1));
			if (o.xMapper) nx = o.xMapper(y);
			else if (o.yMapper) ny = o.yMapper(x);
			TQUtils.Drag.obj.root.style[o.hmode ? "left": "right"] = nx + "px";
			TQUtils.Drag.obj.root.style[o.vmode ? "top": "bottom"] = ny + "px";
			TQUtils.Drag.obj.lastMouseX = ex;
			TQUtils.Drag.obj.lastMouseY = ey;
			TQUtils.Drag.obj.root.onDrag(nx, ny);
			return false
		},
		end: function() {
			document.onmousemove = TQUtils.tq_fixJQueryMouseMoveEvent;
			document.onmouseup = null;
			TQUtils.Drag.obj.root.onDragEnd(parseInt(TQUtils.Drag.obj.root.style[TQUtils.Drag.obj.hmode ? "left": "right"]), parseInt(TQUtils.Drag.obj.root.style[TQUtils.Drag.obj.vmode ? "top": "bottom"]));
			TQUtils.Drag.obj = null
		},
		fixE: function(e) {
			if (typeof e == 'undefined') e = window.event;
			if (typeof e.layerX == 'undefined') e.layerX = e.offsetX;
			if (typeof e.layerY == 'undefined') e.layerY = e.offsetY;
			return e
		}
	},
	GetScrollTop: function() {
		var tq_posY = 0;
		var d = document;
		if (d.documentElement && d.documentElement.scrollTop) {
			tq_posY = d.documentElement.scrollTop
		} else if (d.body) {
			tq_posY = d.body.scrollTop
		} else if (window.innerHeight) {
			tq_posY = window.pageYOffset
		}
		if (tq_posY == "undefined") tq_posY = 0;
		return tq_posY
	},
	GetScrollLeft: function() {
		var tq_posX = 0;
		var d = document;
		if (d.documentElement && d.documentElement.scrollLeft) {
			tq_posX = d.documentElement.scrollLeft
		} else if (d.body) {
			tq_posX = d.body.scrollLeft
		} else if (window.innerWidth) {
			tq_posX = window.pageXOffset
		}
		if (tq_posX == "undefined") tq_posX = 0;
		return tq_posX
	},
	GetWinSize: function() {
		var size = new Object();
		var d = document;
		var db = d.body;
		var de = d.documentElement;
		if (de && de.clientHeight) {
			size.w = de.clientWidth;
			size.h = de.clientHeight
		} else if (db) {
			size.w = db.clientWidth;
			size.h = db.clientHeight
		} else if (window.innerHeight) {
			size.w = window.innerWidth;
			size.h = window.innerHeight
		}
		var strict = document.compatMode && document.compatMode == "CSS1Compat";
		if (!strict) {
			size.w = db.clientWidth;
			size.h = db.clientHeight
		}
		return size
	},
	GetObj: function(id) {
		if (this.isDOM) return document.getElementById(id);
		if (this.isIE4) return document.all[id];
		if (this.isNS4) return document.layers[id]
	},
	GetCookieVal: function(offset) {
		var endstr = document.cookie.indexOf(";", offset);
		if (endstr == -1) endstr = document.cookie.length;
		return unescape(document.cookie.substring(offset, endstr))
	},
	GetCookie: function(name) {
		var arg = name + "=";
		var alen = arg.length;
		var clen = document.cookie.length;
		var i = 0;
		while (i < clen) {
			var j = i + alen;
			if (document.cookie.substring(i, j) == arg) {
				return this.GetCookieVal(j)
			}
			i = document.cookie.indexOf(" ", i) + 1;
			if (i == 0) break
		}
		return ""
	},
	SetCookie: function(name, value) {
		var argv = TQUtils.SetCookie.arguments;
		var argc = TQUtils.SetCookie.arguments.length;
		var expires = new Date();
		expires.setTime(expires.getTime() + ((2 < argc) ? argv[2] : 365 * 24) * 60 * 60 * 1000);
		var expires_time = expires;
		var path = (3 < argc) ? argv[3] : null;
		var domain = (4 < argc) ? argv[4] : null;
		var secure = (5 < argc) ? argv[5] : false;
		document.cookie = name + "=" + value + ((expires == null) ? "": ("; expires=" + expires_time.toGMTString())) + ((path == null) ? "": ("; path=" + path)) + ((domain == null) ? "": ("; domain=" + domain)) + ((secure == true) ? "; secure": "")
	},
	SetCookie2: function(name, value) {
		var argv = TQUtils.SetCookie2.arguments;
		var argc = TQUtils.SetCookie2.arguments.length;
		var expires = new Date();
		expires.setTime(expires.getTime() + ((2 < argc) ? argv[2] : 365 * 24) * 60 * 60 * 1000);
		var expires_time = expires;
		var path = (3 < argc) ? argv[3] : null;
		var domain = (4 < argc) ? argv[4] : null;
		var secure = (5 < argc) ? argv[5] : false;
		document.cookie = name + "=" + value + ((expires == null) ? "": ("; expires=" + expires_time.toGMTString())) + ("; path=/") + ((domain == null) ? "": ("; domain=" + domain)) + ((secure == true) ? "; secure": "")
	},
	getRootDomain: function() {
		if (document.domain != null && document.domain != "") {
			try {
				return document.domain.match(/(\w+\.(?:com.cn|com|cn|net|org|cc))(?:\/|$)/)[1]
			} catch(e) {
				return null
			}
		}
		return null
	},
	GetTime: function() {
		var d = new Date();
		var mon = d.getMonth() + 1;
		var nowtime = d.getFullYear() + '-' + mon + '-' + d.getDate() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds();
		return nowtime
	},
	GetTime_invite: function() {
		var d = new Date();
		var mon = d.getMonth() + 1;
		var nowtime = d.getFullYear() + '-' + mon + '-' + d.getDate() + ',' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds();
		return nowtime
	},
	GetTime_debug: function() {
		var d = new Date();
		var mon = d.getMonth() + 1;
		var nowtime = d.getFullYear() + '-' + mon + '-' + d.getDate() + ' ' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() + ':' + d.getMilliseconds();
		return nowtime
	},
	GetPageSize: function() {
		var d = document;
		var b = (d.compatMode != "CSS1Compat") ? d.body: d.documentElement;
		var size = new Object();
		size.w = Math.max(b.scrollWidth, b.clientWidth);
		size.h = Math.max(b.scrollHeight, b.clientHeight);
		return size
	},
	AddEvent: function(target, event, fn) {
		if (typeof target == 'string') target = TQUtils.GetObj(target);
		if (typeof target.addEventListener != "undefined") {
			target.addEventListener(event, fn, false)
		} else if (typeof target.attachEvent != "undefined") {
			this.AddListener(target, "on" + event, fn)
		} else if (typeof eval(target + ".on" + event) == "function") {
			var fnOld = eval(target + ".on" + event);
			eval(target + ".on" + event + " = function() {fnOld();fn();}")
		} else {
			eval(target + ".on" + event + " = fn")
		}
	},
	AddListener: function(target, eventType, fn) {
		if (typeof target == 'string') target = TQUtils.GetObj(target);
		target.attachEvent(eventType, fn)
	},
	HeartBeat: function(obj, inteval, timer, lastPos, isHeartbeat) {
		if (!isHeartbeat) return;
		var tq_diffY1 = TQUtils.GetScrollTop();
		var tq_diffX1 = TQUtils.GetScrollLeft();
		if (tq_diffY1 != lastPos.y) {
			var tq_percent1 = .1 * (tq_diffY1 - lastPos.y);
			if (tq_percent1 > 0) {
				tq_percent1 = Math.ceil(tq_percent1)
			} else {
				tq_percent1 = Math.floor(tq_percent1)
			}
			if (TQUtils.NS) {
				document.tq_float_container.top += tq_percent1
			}
			obj.style.top = tq_percent1 + parseInt(obj.style.top) + "px";
			lastPos.y = lastPos.y + tq_percent1
		}
		if (tq_diffX1 != lastPos.x) {
			var tq_percent1 = .1 * (tq_diffX1 - lastPos.x);
			if (tq_percent1 > 0) {
				tq_percent1 = Math.ceil(tq_percent1)
			} else {
				tq_percent1 = Math.floor(tq_percent1)
			}
			if (TQUtils.NS) {
				document.tq_float_container.left += tq_percent1
			}
			obj.style.left = tq_percent1 + parseInt(obj.style.left) + "px";
			lastPos.x = lastPos.x + tq_percent1
		}
		timer = setTimeout(function() {
			TQUtils.HeartBeat(obj, inteval, timer, lastPos, isHeartbeat)
		},
		inteval)
	},
	GetTitle: function() {
		var tq_visit_title = "";
		try {
			tq_visit_title = document.title;
			if (tq_visit_title != "undefined" && tq_visit_title != null && tq_visit_title != "" && tq_visit_title.length > 22) tq_visit_title = tq_visit_title.substring(0, 22);
			tq_visit_title = encodeURI(tq_visit_title)
		} catch(e) {}
		return tq_visit_title
	},
	GetLocalURL: function() {
		var local_url = window.location.href.replace(/\&/g, "*").replace(/\#/gi, "$").replace(/\?/gi, "!");
		if (local_url.length > TQKF.local_url_length) local_url = local_url.substring(0, TQKF.local_url_length);
		return local_url
	},
	GetSourcePageURL: function(time) {
		var source_page_url = this.GetCookie("tq_source_page_url");
		if (source_page_url == null || source_page_url == "") {
			var reffer = this.GetReffer();
			if (reffer != "") {
				source_page_url = this.ReCreatReffer(reffer);
				this.SetCookie("tq_source_page_url", source_page_url, time)
			}
		}
		return source_page_url
	},
	GetReffer: function() {
		var tq_refTemp = '';
		if (document.referrer.length > 0) {
			tq_refTemp = document.referrer
		}
		try {
			if (tq_refTemp.length == 0 && opener && opener.location.href.length > 0) {
				tq_refTemp = opener.location.href
			}
		} catch(e) {}
		return tq_refTemp
	},
	GetPara: function(url, name) {
		if (url == null || url == "") return "";
		var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
		var r = url.substr(url.indexOf("\?") + 1).match(reg);
		if (r != null) {
			return (r[2])
		}
		return ""
	},
	ReCreatReffer: function(url) {
		if (url == null || url.length == 0) return "";
		var domain = url.substring(0, url.indexOf("?"));
		for (var i = 0; i < TQKF.filterURLs.length; i++) {
			var isSearchEngine = false;
			for (var j = 0; j < TQKF.filterURLs[i].domain.length; j++) {
				if (domain.indexOf(TQKF.filterURLs[i].domain[j]) != -1) {
					isSearchEngine = true
				}
			}
			if (isSearchEngine) {
				var keyword = this.GetPara(url, TQKF.filterURLs[i].keyPara);
				if (keyword != "") {
					return domain + "?" + TQKF.filterURLs[i].keyPara + "=" + keyword.replace(/\%/g, "^")
				}
			}
		}
		if (url.length > TQKF.reffer_url_length) url = url.substring(0, TQKF.reffer_url_length);
		return url.replace(/\&/g, "*").replace(/\%/g, "^").replace(/\#/gi, "%23")
	},
	SetConfigValue: function(prefix, config) {
		for (var v in config) {
			if (eval("typeof(" + prefix + v + ")!='undefined'")) {
				if (eval(prefix + v) != "") config[v] = eval(prefix + v)
			}
		}
	},
	SetValue: function(from, to) {
		for (var prop in from) {
			if (typeof(from[prop]) == "function") continue;
			else if (from[prop] != null && from[prop] != "null" && !(from[prop] === "")) {
				if (typeof(from[prop]) == "object") {
					if (!to[prop]) to[prop] = {};
					this.SetValue(from[prop], to[prop])
				} else to[prop] = from[prop]
			}
			if (from[prop] === "" && !to[prop]) to[prop] = from[prop]
		}
	},
	FillInnertip: function(str) {
		return str.replace(/&quot;/g, '"').replace(/&#039;/g, "'").replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;/g, "&")
	},
	IsFlash: function(url) {
		return url.indexOf(".swf") != -1 || url.indexOf(".flv") != -1
	},
	Disp: function(obj) {
		if (typeof obj == 'string') obj = TQUtils.GetObj(obj);
		obj == null || (obj.style.visibility = "visible")
	},
	Hide: function(obj) {
		if (typeof obj == 'string') obj = TQUtils.GetObj(obj);
		obj == null || (obj.style.visibility = "hidden")
	},
	setDefaultValue: function(def, value) {
		if (value == null || value == "") return def;
		else return value
	},
	FixFlash: function() {
		if (TQKF.isFlashFixed == true) return;
		TQKF.isFlashFixed = true;
		var d = document;
		var fs = d.getElementsByTagName('object'),
		ems = d.getElementsByTagName('embed');
		if (TQUtils.FF) {
			for (var i = 0; i < ems.length; i++) {
				ems[i].setAttribute("wmode", "transparent");
				ems[i].setAttribute("src", ems[i].getAttribute("src"))
			}
		} else {
			for (var i = 0; i < fs.length; i++) {
				var newFlash = document.createElement('object');
				newFlash.setAttribute("codeBase", fs[i].getAttribute("codeBase"));
				newFlash.setAttribute("classid", fs[i].getAttribute("classid"));
				newFlash.setAttribute("width", fs[i].getAttribute("width"));
				newFlash.setAttribute("height", fs[i].getAttribute("height"));
				for (var j = 0; j < fs[i].childNodes.length; j++) {
					newFlash.appendChild(fs[i].childNodes[j])
				}
				var wc = document.createElement('param');
				wc.name = 'wmode';
				wc.value = 'transparent';
				newFlash.appendChild(wc);
				var h = newFlash.outerHTML;
				fs[i].outerHTML = h
			}
		}
	},
	isInCode: function(status, code) {
		var codes = code.split("|");
		for (var i = 0; i < codes.length; i++) {
			if (codes[i] == status) return true
		}
		return false
	}
};
TQUtils.ieVersion = /msie (\d+)/.exec(TQUtils.browserType);
TQUtils.higherThanIE6 = TQUtils.ieVersion && parseInt(TQUtils.ieVersion[1]) > 6;
try {
	TQUtils.onQuirkMode = document.compatMode && document.compatMode.indexOf('Back') == 0;
	TQUtils.strict = document.compatMode && document.compatMode == "CSS1Compat"
} catch(e) {
	TQUtils.strict = false
}
TQUtils.isSupportFixedPos = (window.addEventListener || TQUtils.higherThanIE6 && (!TQUtils.onQuirkMode)) ? true: false;
if (TQUtils.ieVersion && !(TQUtils.higherThanIE6)) {
	try {
		document.execCommand("BackgroundImageCache", false, true)
	} catch(e) {}
}
var tq_debugFrame = null;
var tq_debug = 3;
var tq_info = 2;
var tq_error = 1;
try {
	var tempRef = window.location.href;
	tempRef = TQUtils.GetPara(tempRef, "tqdebug");
	if (tempRef != "") TQKF.userDefined.debug = tempRef
} catch(e) {}
function TQ_DEBUG(info, level) {
	try {
		if (level <= TQKF.userDefined.debug) {
			if (tq_debugFrame == null) {
				tq_debugFrame = window.open("about:blank", "tq_debugFrame", "width=600,height=400,location=no,resizable=1,scrollbars=1,status=no,toolbar=no,menu=no,top=100,left=200");
				if (TQUtils.IE) {
					tq_debugFrame.document.body.style.backgroundColor = "#EBE9ED";
					tq_debugFrame.document.title = "debug info";
					tq_debugFrame.document.body.innerHTML += '<span style="font-size:14px;color:#00A600"><b>debug info:</b></span>';
					tq_debugFrame.document.body.innerHTML += '<div id=debug_div style="word-wrap:break-word;word-break:break-all;font-size:12px;width:100%;	border: 1px solid;	border-color: threedshadow threedhighlight threedhighlight threedshadow;display: block;background-color:#000000;"></div>'
				}
			}
			if (level == 1) color = "red";
			else if (level == 2) color = "yellow";
			else color = "white";
			var functionText = "";
			if (TQUtils.IE) {
				tq_debugFrame.document.getElementById("debug_div").innerHTML += ("<span style='color:white'><b>[" + TQUtils.GetTime_debug() + "]&nbsp;&nbsp;---->></b>" + functionText + "</span><span style='color:" + color + "'>" + info + "</span><br>")
			} else {
				tq_debugFrame.document.body.innerHTML += "<div style='word-wrap:break-word;word-break:break-all;font-size:12px;width:100%;	border: 0px;background-color:#000000;;color:" + color + "'>" + ("<span style='color:white'><b>[" + TQUtils.GetTime_debug() + "]&nbsp;&nbsp;---->></b>" + functionText + "</span><span style='color:" + color + "'>" + info + "</span><br>") + "</div>"
			}
			tq_debugFrame.scroll(0, 90000)
		}
	} catch(e) {}
}
TQKF.reffer_url = TQUtils.GetReffer();
TQKF.page_url = TQUtils.ReCreatReffer(TQKF.reffer_url);
TQKF.visit_title = TQUtils.GetTitle();
TQKF.local_url = TQUtils.GetLocalURL();
TQKF.source_page_url = TQUtils.GetSourcePageURL(TQKF.source_page_life_time * (1 / 60));
TQUtils.SetValueIfUndefinedOrEmpty("tq_version", "");
if (tq_version == "") {
	TQUtils.SetValueIfUndefinedOrEmpty("tq_is_fix_flash", "1");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_float_type", "1");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_float_style", "1");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_invit_style", "1");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_infocard_url", "");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_float_layout_cfg", '');
	TQUtils.SetValueIfUndefinedOrEmpty("tq_float_html", "");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_is_fold_term", "0");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_invit_sound", "");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_invit_layout_cfg", '');
	TQUtils.SetValueIfUndefinedOrEmpty("tq_invit_auto_hide_delay", "0");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_invit_is_hide_float", "0");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_invit_response_uins", "");
	TQUtils.SetValueIfUndefinedOrEmpty("tq_kefuimg_cfg", '')
}
if (tq_is_fix_flash == 1) {
	try {
		TQUtils.FixFlash()
	} catch(e) {}
	TQUtils.AddEvent(window, "load", TQUtils.FixFlash)
}
if (tq_language == "2") tq_language = "100";
if (tq_language == "100") {
	TQKF.ltype_str = "_en";
	TQKF.words = TQKF.words_en
} else {
	TQKF.words = TQKF.words_zh
}
TQUtils.SetValueIfUndefinedOrEmpty("tq_if_tryout", 0);
TQUtils.SetValueIfUndefined("tq_iscallback", "1");
TQUtils.SetValueIfUndefined("TQKF.chattype", "");
TQUtils.SetValueIfUndefined("tq_isnoshowuser", "0");
TQUtils.SetValueIfUndefined("tq_isFullScreenLeavmsg", false);
TQUtils.SetValueIfUndefined("tq_agentid", "0");
TQUtils.SetValueIfUndefined("tq_siteid", "");
TQUtils.SetValueIfUndefined("tq_clientid", "");
TQKF.tq_media_param_value = TQUtils.GetPara(window.location.href, TQKF.tq_media_param_name);
if (tq_if_tryout == 0) {
	if (tq_version == "" || parseInt(tq_version) < parseInt(TQKF.version)) {
		if (tq_invit_style == 1) tq_displaytype = "0";
		else {
			tq_invit_style = 1;
			tq_displaytype = 100
		}
		tq_invit_color = "1"
	}
	TQUtils.SetValueIfUndefinedOrEmpty("tq_invit_style", 1);
	tq_language_type = "";
	TQUtils.SetValueIfUndefinedOrEmpty("tq_type", 1);
	TQKF.invite_server_url = "http://webscreen2006.tq.cn:8000";
	TQKF.webchat_url2 = "http://webchat.tq.cn/sendmain.jsp";
	if (typeof(tq_webchat_url) != "undefined") {
		TQKF.webchat_url2 = tq_webchat_url + "/sendmain.jsp"
	}
	TQKF.leavemsg_url = "http://webchat.tq.cn/pageinfo.jsp";
	tq_acd = 0;
	tq_is_anti_bad_click = 0;
	if (tq_type == 2 && typeof(tq_isnoshowuser) != "undefined" && tq_isnoshowuser == "") tq_isnoshowuser = "0";
	TQKF.open_win_w = 596;
	TQKF.open_win_h = 438
}
if (typeof(tq_webscreen_url) != "undefined") {
	TQKF.invite_server_url = tq_webscreen_url
}
if (typeof(tq_webchat_url) != "undefined") {
	TQKF.webchat_url = tq_webchat_url;
	TQKF.webchat_url2 = tq_webchat_url + "/pageinfo.jsp";
	TQKF.pageinfo_url = tq_webchat_url + "/pageinfo.jsp";
	TQKF.leavemsg_url = tq_webchat_url + "/pageinfo.jsp";
	TQKF.hashChatURL = tq_webchat_url
}
if (typeof(tq_chat_url) != "undefined" && tq_chat_url != "") {
	TQKF.webchat_url = tq_chat_url;
	TQKF.webchat_url2 = tq_chat_url + "/pageinfo.jsp";
	TQKF.pageinfo_url = tq_chat_url + "/pageinfo.jsp";
	TQKF.leavemsg_url = tq_chat_url + "/pageinfo.jsp"
}
if (typeof(tq_leavemsg_url) != "undefined" && tq_leavemsg_url != "") {
	TQKF.leavemsg_url = tq_leavemsg_url
}
if (typeof(tq_is_panel_float) != "undefined" && tq_is_panel_float != "") TQKF.userDefined.isfloat = tq_is_panel_float;
if (!TQUtils.IsUndefined("tqUserDefined")) TQUtils.SetValue(tqUserDefined, TQKF.userDefined);
self.onerror = function() {
	return true
};
window.onerror = function() {
	return true
};
TQKF.setWindowStatus();
try {
	var tqhasbody = document.body.nodeType
} catch(e) {
	TQ_DEBUG("get document.body.nodeType error try creat body tag", 3);
	document.writeln("<body><\/body>")
}
document.writeln('<script id=TQGetrequestUser_JS>\<\/script\>');
document.write("<script id=TQGetIsNewMsg_JS>\<\/script>");
document.writeln('<script src="' + TQKF.userDefined.asJS + '">\<\/script\>');
if (tq_is_anti_bad_click == "1") document.writeln('<script src="' + TQKF.userDefined.antibadclickJS + '">\<\/script\>');
if (tq_type == "1" || tq_type == "4") {
	TQUtils.LoadCSS(TQKF.float_image_url + 't' + tq_float_type + '/s' + tq_float_style + '/style.css')
}
if (TQKF.isInviter == 1) {
	if (tq_invit_style == "") tq_invit_style = "1";
	if (tq_version == "" || parseInt(tq_version) < parseInt(TQKF.version) && tq_invit_style == 2) TQUtils.LoadCSS(TQKF.invite_image_url + "t1/style.css");
	else TQUtils.LoadCSS(TQKF.invite_image_url + "t" + tq_invit_style + "/style.css")
}
var online = new Array();
var tq_QQuin = [];
var tq_isGetQQOnlineStatus = false;
if (tq_type == "1" && typeof(tq_member_uins) != "undefined") {
	eval("tq_member_uins_temp=" + tq_member_uins);
	for (var i = 0; i < tq_member_uins_temp.length; i++) {
		var tq_t = tq_member_uins_temp[i];
		for (var m = 0; m < tq_t.m.length; m++) {
			if (tq_t.m[m].t == "qq" && tq_t.m[m].s > 100 && tq_t.m[m].s < 200) {
				tq_QQuin.push(tq_t.m[m].id);
				tq_isGetQQOnlineStatus = true
			}
		}
	}
}
function tq_getQQUinIndex(uin) {
	for (var w = 0; w < tq_QQuin.length; w++) {
		if (tq_QQuin[w] == uin) {
			return w
		}
	}
	return - 1
}
if (tq_isGetQQOnlineStatus) {
	TQUtils.LoadJS('http://webpresence.qq.com/getonline?Type=1&' + tq_QQuin.join(":") + ':', TQKF.userDefined.defer)
}
if (TQUtils.IE) {
	if (document.readyState == "complete") {
		TQ_DEBUG("IE, all DOM loaded,begin to load TQJS", 3);
		TQKF.loadRequiredJS()
	} else {
		TQ_DEBUG("IE, DOM have not loaded,load TQJS later", 3);
		if (TQKF.userDefined.forceLoadRequiredJS > 0) setTimeout(TQKF.loadRequiredJS, parseInt(TQKF.userDefined.forceLoadRequiredJS) * 1000);
		TQUtils.AddEvent(window, "load", TQKF.loadRequiredJS);
		document.onreadystatechange = function() {
			if (document.readyState == "complete") {
				TQ_DEBUG("IE,document.readyState==complete,load TQJS now", 3);
				TQKF.loadRequiredJS()
			}
		}
	}
} else TQKF.loadRequiredJS();
try {
	var tqnooooouse = [];
	tqnooooouse.push(1, 2, 3);
	if (tqnooooouse.join("") != "123") Array.prototype.push = function() {
		for (var i = 0; i < arguments.length; i++) this[this.length] = arguments[i];
		return this.length
	}
} catch(e) {}
if (TQUtils.GetPara(window.location.href, "tq_debug") == "1") {
	document.write("<a href=\"javascript:(function(F,i,r,e,b,u,g,L,I,T,E){if(F.getElementById(b))return;E=F[i+'NS']&amp;&amp;F.documentElement.namespaceURI;E=E?F[i+'NS'](E,'script'):F[i]('script');E[r]('id',b);E[r]('src',I+g+T);E[r](b,u);(F[e]('head')[0]||F[e]('body')[0]).appendChild(E);E=new%20Image;E[r]('src',I+L);})(document,'createElement','setAttribute','getElementsByTagName','FirebugLite','4','firebug-lite.js','releases/lite/latest/skin/xp/sprite.png','https://getfirebug.com/','#startOpened');\">Firebug Lite</a>")
}