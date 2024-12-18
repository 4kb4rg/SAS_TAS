<%@ Page Language="vb" src="../../../include/PopUpBlock.aspx.vb" Inherits="PopUpBlock" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>

<head>
    <title>Green Golden - Charge by block</title> 
          <link href="../css/gopalms.css" rel="stylesheet" type="text/css" />
    <script language="">
		window.returnValue = "";
		function do_confirm() {
			if (document.frmMain.ddlBlock.value != "") {
				window.returnValue = document.frmMain.ddlBlock.value;
				window.close();
			}
			else {
				rfvBlock.style.display="";
			}
		}
		
		function do_cancel() {
			window.returnValue = "";
			window.close();
		}
		
		function do_validate() {
			if (document.frmMain.ddlBlock.value != "") {
				rfvBlock.style.display="none";
			}
			else {
				rfvBlock.style.display="";
			}
		}
    </script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body scroll=no onload="javascript:self.focus();document.frmMain.ddlBlock.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<table id=tblMain width=100% runat=server>
			<tr>
				<td class="mt-h"> <asp:Label id=lblCaption runat=server /> </td>
				<td></td>
			</tr>
			<tr>
				<td colspan=2><hr size="1" noshade></td>
			</tr>
			<tr>
				<td width=40%><asp:Label id=lblBlock runat=server /> Code : </td>
				<td width=60%>
					<asp:DropDownList id="ddlBlock" Width=100% runat=server onchange="javascript:do_validate(); return false;"/>
					<asp:Label id=rfvBlock runat=server style="color:red; display:none;"/>
				</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					<input type=image src="../../images/butt_confirm.gif" alt=Confirm onclick="javascript:do_confirm(); return false;" width="76" height="20">
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:do_cancel(); return false;" width="58" height="20">
				</td>
			</tr>
		</table>
                </div>
            </td>
            </tr>
            </table>
    </form>
</body>
</html>
