<!--
/*

showPages v1.1
=================================

Infomation
----------------------
Author : Lapuasi
E-Mail : lapuasi@gmail.com
Web : http://www.lapuasi.com
Date : 2005-11-17


Example
----------------------
var pg = new showPages('pg');
pg.pageCount = 12; //������ҳ��(��Ҫ)
pg.argName = 'p';    //���������(��ѡ,ȱʡΪpage)
pg.printHtml();        //��ʾҳ��


Supported in Internet Explorer, Mozilla Firefox
*/

function showPages(name) { //��ʼ������
	this.name = name;      //��������
	this.page = 1;         //��ǰҳ��
	this.pageCount = 1;    //��ҳ��
	this.argName = 'page'; //������
	this.showTimes = 1;    //��ӡ����
	this.pagename = "";//.htmlǰ��Ĳ��֣����磬Ҫ����list_1_1.html��list_1_2.html,��this.pagename="list_1"
}
showPages.prototype.getPage = function(){ //��url��õ�ǰҳ��,��������ظ�ֻ��ȡ���һ��
/*
//��̬ҳ�治��Ҫ����page����
	var args = location.search;
	var reg = new RegExp('[\?&]?' + this.argName + '=([^&]*)[&$]?', 'gi');
	var chk = args.match(reg);
	this.page = RegExp.$1;
*/
}
showPages.prototype.checkPages = function(){ //���е�ǰҳ������ҳ������֤
	if (isNaN(parseInt(this.page))) this.page = 1;
	if (isNaN(parseInt(this.pageCount))) this.pageCount = 1;
	if (this.page < 1) this.page = 1;
	if (this.pageCount < 1) this.pageCount = 1;
	if (this.page > this.pageCount) this.page = this.pageCount;
	this.page = parseInt(this.page);
	this.pageCount = parseInt(this.pageCount);
}
showPages.prototype.createHtml = function(className){ //����html����
	//alert(className);
	var strHtml = '', prevPage = this.page - 1, nextPage = this.page + 1;
	<!--strHtml += '<span class="count">Pages: ' + this.page + ' / ' + this.pageCount + '</span>';-->
	strHtml += '<div class="'+className+'">';
	if (prevPage < 1) {
		strHtml += '<span title="��1ҳ" class="disabled">&laquo;</span>';
		strHtml += '<span title="��һҳ" class="disabled">&#8249;</span>';
	} else {
		strHtml += '<span title="��һҳ"><a href="javascript:' + this.name + '.toPage(1);">&laquo;</a></span>';
		strHtml += '<span title="��һҳ"><a href="javascript:' + this.name + '.toPage(' + prevPage + ');">&#8249;</a></span>';
	}
	if (this.page != 1) strHtml += '<span title="��1ҳ"><a href="javascript:' + this.name + '.toPage(1);">1</a></span>';
	if (this.page >= 5) strHtml += '<span>...</span>';
	if (this.pageCount > this.page + 2) {
		var endPage = this.page + 2;
	} else {
		var endPage = this.pageCount;
	}
	for (var i = this.page - 2; i <= endPage; i++) {
		if (i > 0) {
			if (i == this.page) {
				strHtml += '<span title="�� ' + i + ' ҳ" class="current">' + i + '</span>';
			} else {
				if (i != 1 && i != this.pageCount) {
					strHtml += '<span title="�� ' + i + ' ҳ"><a href="javascript:' + this.name + '.toPage(' + i + ');">' + i + '</a></span>';
				}
			}
		}
	}
	if (this.page + 3 < this.pageCount) strHtml += '<span>...</span>';
	if (this.page != this.pageCount) strHtml += '<span title="�� ' + this.pageCount + ' ҳ"><a href="javascript:' + this.name + '.toPage(' + this.pageCount + ');">' + this.pageCount + '</a></span>';
	if (nextPage > this.pageCount) {
		strHtml += '<span title="��һҳ" class="disabled">&#8250;</span>';
		strHtml += '<span title="���һҳ" class="disabled">&raquo;</span>';
	} else {
		strHtml += '<span title="��һҳ"><a href="javascript:' + this.name + '.toPage(' + nextPage + ');">&#8250;</a></span>';
		strHtml += '<span title="���һҳ"><a href="javascript:' + this.name + '.toPage(' + this.pageCount + ');">&raquo;</a></span>';
	}
	strHtml += '</div><br />';
	return strHtml;
}
showPages.prototype.createUrl = function (page) { //����ҳ����תurl
	if (isNaN(parseInt(page))) page = 1;
	if (page < 1) page = 1;
	if (page > this.pageCount) page = this.pageCount;
	var url = location.protocol + '//' + location.host + location.pathname;
	var args = location.search;
	var reg = new RegExp('([\?&]?)' + this.argName + '=[^&]*[&$]?', 'gi');
	args = args.replace(reg,'$1');
	if (args == '' || args == null) {
		//url = url.replace("_"+(page-1)+".html","_"+page+".html");
		url = this.pagename+"_"+page+".html"
		//args += '?' + this.argName + '=' + page;
		args = "";
	} else if (args.substr(args.length - 1,1) == '?' || args.substr(args.length - 1,1) == '&') {
			args += this.argName + '=' + page;
	} else {
			args += '&' + this.argName + '=' + page;
	}
	//alert(url + args);
	//document.write(url+arg);
	//��list_5.html���з�ҳ����Ϊ��list_5_1.html,list_5_2.html,list_5_3.html...
	return url+args;
}
showPages.prototype.toPage = function(page){ //ҳ����ת
	var turnTo = 1;
	if (typeof(page) == 'object') {
		turnTo = page.options[page.selectedIndex].value;
	} else {
		turnTo = page;
	}
	self.location.href = this.createUrl(turnTo);
}
showPages.prototype.printHtml = function(mode){ //��ʾhtml����
	this.getPage();
	this.checkPages();
	this.showTimes += 1;
	document.write('<div id="pages_' + this.name + '_' + this.showTimes + '" class="pages"></div>');
	document.getElementById('pages_' + this.name + '_' + this.showTimes).innerHTML = this.createHtml(mode);
	
}
showPages.prototype.formatInputPage = function(e){ //�޶�����ҳ����ʽ
	var ie = navigator.appName=="Microsoft Internet Explorer"?true:false;
	if(!ie) var key = e.which;
	else var key = event.keyCode;
	if (key == 8 || key == 46 || (key >= 48 && key <= 57)) return true;
	return false;
}
//-->