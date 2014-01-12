<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsAdd.aspx.cs" Inherits="web1.Admin.NewsAdd" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="Styles/admin.css" type="text/css" rel="stylesheet" />
	<script type="text/javascript" src="../swfupload/swfupload.js"></script>
	<script type="text/javascript" src="../swfupload/handlers.js"></script>
	<script type="text/javascript">
	    function showOldPic() {
	        var pics = $("#hPic").val().replace("||","|");
	        if (pics.replace("|", "") == "") {
	            $("#picContent2").hide();
	        } else {
	            $("#picContent2").show();
	            var picListObj = document.getElementById("picList");
	            picListObj.innerHTML = "";
//	            var childs = picListObj.childNodes;
//	            for (var i = 0; i < childs.length; i++) {
//	                alert(childs[i].nodeName);
//	                picListObj.removeChild(childs[i]);
//	            }
	            var n = 0;
	            //alert(pics);
	            var imgs = pics.split("|");
	            for (var i = 0; i < imgs.length; i++) {
	                if (imgs[i] != "") {
	                    n++;
	                    var newLi = document.createElement("li");
	                    newLi.setAttribute("id", "newLi" + i.toString());

	                    var newLink = document.createElement("a");
	                    newLink.href = "#";
	                    newLink.target = "_self";
	                    newLink.innerHTML = "<BR>删除"; 

	                    var newImg = document.createElement("img");
	                    newImg.setAttribute("src", imgs[i]);
	                    newImg.width = 100;
	                    newImg.height = 100;
	                    document.getElementById("picList").appendChild(newLi);
	                    
	                    document.getElementById("newLi" + i.toString()).appendChild(newImg);
	                    document.getElementById("newLi" + i.toString()).appendChild(newLink);
	                    //$("#picList").appendChild(newLi);
	                    //$("#newLi" + i).appendChild(newImg).appendChild(newLink);

	                    //document.getElementById("thumbnails").appendChild(newImg);
	                }
	            }
	            //alert(n + "张图片载入完成。")
	            addClickEvent();
	        }
	    }
	    function showOldSmallPic() {
	        var pic = $("#hSmallPic").val();
	        if (pic != "") {
	            $("#picContent1").show();
	            $("#PicSmall").attr("src", pic);
	        } else {
	            $("#picContent1").hide();
	        }
	    }

	    function addClickEvent() {
	        $("#picList li a").click(function () {
	            //alert(this.attr("src"));
	            var src = $(this).parents("li").children("img").attr("src");
	            //alert(src);
	            //删除图片，并更新数据库
	            $.post("NewsAdd.aspx", "act=delpic&id=<%=hID.Value %>&imgsrc=" + src, function (data) {
	                if (data != "") {
	                    //$(this).parents("li").remove();//.hide();
	                    var pic = $("#hPic").val();
	                    pic = pic.replace("|"+src, "");
	                    $("#hPic").val(pic);
	                    //alert("删除成功，还有图片共" + (pic.split("|").length-1) + "张");
	                    showOldPic();
	                }
	            }, "text");

	        });
	    }
    </script>

	<script type="text/javascript">
	    var swfu;
	    var swfu_small;
	    window.onload = function () {
	        showOldSmallPic();
	        showOldPic();

	        swfu = new SWFUpload({
	            // Backend Settings
	            upload_url: "upfile.aspx",
	            post_params: {
	                "ASPSESSID": "<%=Session.SessionID %>",
	                "newsid": "<%=NewsID %>",
	                "guid": "<%=hGuid.Value%>"
	            },

	            // File Upload Settings
	            file_size_limit: "1 MB",
	            file_types: "*.jpg;*.gif",
	            file_types_description: "JPG Images;GIF Images",
	            file_upload_limit: "0",    // Zero means unlimited

	            // Event Handler Settings - these functions as defined in Handlers.js
	            //  The handlers are not part of SWFUpload but are part of my website and control how
	            //  my website reacts to the SWFUpload events.
	            file_queue_error_handler: fileQueueError,
	            file_dialog_complete_handler: fileDialogComplete,
	            upload_progress_handler: uploadProgress,
	            upload_error_handler: uploadError,
	            upload_success_handler: function (file, data) {
	                //alert("上传完成: " + data);
	                //$("#picContent2").show();
	                addImage(data);
	                document.getElementById("hPic_new").value += "|" + data;
	            },
	            upload_complete_handler: uploadComplete,

	            // Button settings
	            button_image_url: "images/XPButtonNoText_160x22.png",
	            button_placeholder_id: "spanButtonPlaceholder",
	            button_width: 160,
	            button_height: 22,
	            button_text: '<span class="button">浏览<span class="buttonSmall">(1 MB Max)</span></span>',
	            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
	            button_text_top_padding: 1,
	            button_text_left_padding: 5,

	            // Flash Settings
	            flash_url: "../swfupload/swfupload.swf", // Relative to this file

	            custom_settings: {
	                upload_target: "divFileProgressContainer"
	            },

	            // Debug Settings
	            debug: false
	        });

	        swfu_small = new SWFUpload({
	            //upload_url作用：保存图片文件，并返回文件名。后面将得到的文件名保存到hidden中
	            upload_url: "upfile.aspx",
	            post_params: {
	                "ASPSESSID": "<%=Session.SessionID %>",
	                "newsid": "<%=NewsID %>",
	                "guid": "<%=hGuid.Value%>",
	                "type": "small"
	            },

	            // File Upload Settings
	            file_size_limit: "200 KB",
	            file_types: "*.jpg;*.gif",
	            file_types_description: "JPG Images;GIF Images",
	            file_upload_limit: "1",    // Zero means unlimited

	            // Event Handler Settings - these functions as defined in Handlers.js
	            //  The handlers are not part of SWFUpload but are part of my website and control how
	            //  my website reacts to the SWFUpload events.
	            file_queue_error_handler: fileQueueError,
	            file_dialog_complete_handler: fileDialogComplete,
	            upload_progress_handler: uploadProgress,
	            upload_error_handler: uploadError,
	            upload_success_handler: function (file, data) {
	                //alert("上传完成: " + data);
	                //$("#PicSmall").attr("src", data);
	                document.getElementById("PicSmall").src = data;
	                document.getElementById("hSmallPic_new").value = data;
	                //$("#spanButtonPlaceholder_small").hide();
	                $("#picContent1").show();
	                document.getElementById("upSmallButton").style.display = "none";
	            },
	            upload_complete_handler: uploadComplete,

	            // Button settings
	            button_image_url: "images/XPButtonNoText_160x22.png",
	            button_placeholder_id: "spanButtonPlaceholder_small",
	            button_width: 160,
	            button_height: 22,
	            button_text: '<span class="button">浏览<span class="buttonSmall">(200 KB Max)</span></span>',
	            button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
	            button_text_top_padding: 1,
	            button_text_left_padding: 5,

	            // Flash Settings
	            flash_url: "../swfupload/swfupload.swf", // Relative to this file

	            custom_settings: {
	                upload_target: "divFileProgressContainer_small"
	            },

	            // Debug Settings
	            debug: false
	        });

	    }
	</script>
<style>
#divFileProgressContainer_small
{
    border:1px solid #ccc;
}
ul#picList
{
    padding:0; margin:0;
}
ul#picList li
{
    padding:0; margin:0; width:120px; height:120px; float:left; border:1px solid #ccc; margin:2px; text-align:center;
}
ul#picList li img
{
    width:100px; height:100px;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
    <h4>
        &nbsp;&nbsp;<%Response.Write(lbname); %></h4>
    <asp:HiddenField ID="hLbid" runat="server" />
    <asp:HiddenField ID="hID" runat="server" />
    <asp:HiddenField ID="hPic_new" runat="server" />
    <asp:HiddenField ID="hPic" runat="server" />
    <asp:HiddenField ID="hSmallPic_new" runat="server" />
    <asp:HiddenField ID="hSmallPic" runat="server" />
    <asp:HiddenField ID="hGuid" runat="server" />
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" class="tabelStyle">
        <tr>
            <th colspan="2">
                <asp:Label ID="lblOper" runat="server" Text="添加"></asp:Label>
            </th>
        </tr>
        <tr>
            <td width="21%">
                标题：
            </td>
            <td width="79%">
                <asp:TextBox ID="txtTitle" runat="server" Width="200px" />
            </td>
        </tr>
        <tr>
            <td width="21%">
                发布时间：
            </td>
            <td width="79%">
                <asp:TextBox ID="txtAddTime" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                小图标：
            </td>
            <td>
            
            <div id="upSmallButton"><span id="spanButtonPlaceholder_small"></span></div>
                <div id="divFileProgressContainer_small" style=" display:none;"></div>
                <div id="picContent1" style="display:none">
                <img id="PicSmall" alt="小图标" width="200" />
                </div>
                
            </td>
        </tr>

        <tr>
            <td>
                图片集：
            </td>
            <td>
                <span id="spanButtonPlaceholder"></span>
                <div id="picContent2" style="display:none;">
                旧图：
                <ul id="picList">
                <%--<li id="img1"><img src="/UpFile/20120409115500603.jpg"/><br /><a href="#" id="a1">删除</a></li>
                <li id="img2"><img src="/UpFile/20120409115500603.jpg"/><br /><a href="#">删除</a></li>
                <li id="img3"><img src="/UpFile/20120409115500603.jpg" /><br /><a href="#">删除</a></li>--%>
                </ul>
                </div>
                <div id="thumbnails"></div>
            </td>
        </tr>
        <tr>
            <td>
                说明：
            </td>
            <td>
                上传图片：上传文件后，单击下面的保存按钮才会生效。<br />
                删除图片：单击删除立刻生效，不需要点保存按钮。
            </td>
</tr>
        <tr>
            <td>
                内容：
            </td>
            <td><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="300">
                </FCKeditorV2:FCKeditor></td>
        </tr>
        <tr>
            <td colspan="2" align="center" valign="middle">
                <asp:Button ID="btnAdd" Text="修改并保存" runat="server" 
                    OnClick="btnAdd_Click" CssClass="tbutton" />
                &nbsp;
                <input type="button" value="返回" onclick="window.history.back();" class="tbutton" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
