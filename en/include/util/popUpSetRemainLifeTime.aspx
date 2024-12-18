<%@ Page Language="vb" trace=false src="../../../include/popUpSetRemainLifeTime.aspx.vb" Inherits="popUpSetRemainLifeTime" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>

<html>
<head>
    <title>G2 - Find</title> 
    <link rel="stylesheet" type="text/css" href="../css/gopalms.css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
		
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();onload_setfocus();" leftmargin="2" topmargin="2">
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
       		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 


		<table id="tblMain" width="100%" class="font9Tahoma" runat="server">
			<tr>
				<td width="20%" class="mt-h">
                    SETTING</td>
				<td align="right" width="60%"><asp:label id="lblTracker" runat="server"/></td>
			</tr>
			<tr>
				<td colspan="2"><asp:label id="lblHiddenSts" runat="server" Visible="False"/></td>
			</tr>
			
			<tr>
				<td width="30%" style="height: 22px">
                    <hr style="width :100%" />
                    </td>
				<td width="70%" style="height: 22px">
                    &nbsp;</td>
			</tr>
			

			<tr>
				<td>
                    Minimum Life Time Screen </td>
				<td>
					<asp:TextBox id="TxtMinScreen" width="40%" maxlength="10" runat="server" />
                    &nbsp;(Hour)</td>
			</tr>
            <tr>
                <td width="30%">
                    Windows Pop Up Screen</td>
                <td width="70%">
                    <asp:RadioButton ID="rdoTampil" runat="server" Checked="True" GroupName="PopUp" Text="  Active"
                        TextAlign="Right" />&nbsp;&nbsp;
                    <asp:RadioButton ID="rdoTampilNo" runat="server" GroupName="PopUp"
                            Text="  Not Active" TextAlign="Right" /></td>
            </tr>
			
			<tr>
				<td width="30%"></td>
				<td width="70%">
                    &nbsp;<asp:ImageButton ID="SaveBtn" runat="server" AlternateText="  Save  " CommandArgument="Save"
                        ImageUrl="../../images/butt_save.gif" OnClick="Button_Click" />
				    <input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
				</td>
			</tr>
			<tr>
				<td align="right" colspan="2" style="text-align: left"><asp:Label id="lblerrisActive" visible=False Text="Input Minimum Life Time Screen" runat=server /></td>
			</tr>
			<tr>
				<td align="left" ColSpan="2">
                    &nbsp;</td>
			</tr>
			</table>
			<table id="Table1" width="100%" runat="server">
			<tr>
					<td colspan="2">					&nbsp;</td>
				</tr>
				
		</table>
		 <asp:Label id="lblErrMessage" visible=false Text="Error while initiating component." runat=server />
		<asp:label id="SortExpression" Visible="False" Runat="server" />
                </div>
            </td>
            </tr>
            </table>
    </form>
</body>
</html>
