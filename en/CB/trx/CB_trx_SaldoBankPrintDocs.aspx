<%@ Page Language="vb" Src="../../../include/CB_trx_SaldoBankPrintDocs.aspx.vb" Inherits="CB_trx_SaldoBankPrintDocs" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>SALDO BANK</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  


			<asp:label id="lblPleaseSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server"/>
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table  class="font9Tahoma" cellspacing="1" cellpadding="1" width="100%" border="0" id="TABLE1"">
 				<tr>
					<td colspan="6">
					&nbsp;
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4"> <strong> SALDO BANK</strong></td>
				</tr>
				<tr>
					<td colspan="6" >&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 25px; width:25%">
                        Tanggal&nbsp;</td>
					<td style="height: 25px; width:45%" colspan="3">
                        :<a href="javascript:PopCal('txtTgl');">
                        <asp:TextBox ID="txtTgl" runat="server" MaxLength="20" Width="40%"></asp:TextBox>
                        <asp:Image ID="btnDateCreated" runat="server" ImageUrl="../../Images/calendar.gif" /></td>                        
					<td style="height: 25px; width:15%"></td>
					<td style="height: 25px; width:15%"></td>
					
				
				<tr>
					<td style="height: 25px; width:25%">
                        Nama PT</td>
					<td style="height: 25px; width:45%" colspan="3">
                        :
                        <asp:DropDownList ID="cbPT" runat="server" Width="40%">
                            <asp:ListItem>All PT</asp:ListItem>
                            <asp:ListItem>ABM</asp:ListItem>
                            <asp:ListItem>BAL</asp:ListItem>
                            <asp:ListItem>JSA</asp:ListItem>
                            <asp:ListItem>KAS</asp:ListItem>
                            <asp:ListItem>KSUP</asp:ListItem>
                            <asp:ListItem>MAL</asp:ListItem>
                            <asp:ListItem>PBS</asp:ListItem>
                            <asp:ListItem>PML</asp:ListItem>
                            <asp:ListItem>SPP</asp:ListItem>
                            <asp:ListItem>STA</asp:ListItem>
                        </asp:DropDownList></td>
					<td style="height: 25px; width:15%"></td>
					<td style="height: 25px; width:15%"></td>
					
				</tr>
				<tr>
					<td style="height:25px">
                        Nama Bank
                    </td>
					<td valign="middle" colspan="3" style="height: 25px">
                        :
                        <asp:TextBox ID="txtBank" runat="server" MaxLength="8" Width="40%"></asp:TextBox></td>
					<td style="height: 25px"></td>
					<td style="height: 25px"></td>
				</tr>
				<tr>
					<td style="height:25px">
                        Group By</td>
					<td colspan="3" style="height: 25px">
					:&nbsp;<asp:RadioButton ID="rbLok" runat="server" GroupName="rbType" Checked="true" Text="Lokasi" Width="66px" />
                        <asp:RadioButton
                            ID="rbBank" runat="server"  GroupName="rbType" Text="Bank" />
                     </td>
					<td style="height: 25px"></td>
					<td style="height: 25px"></td>
				</tr>
				<tr>
					<td style="height:25px">
                        </td>
					<td style="height: 25px" colspan="3">
                        &nbsp;
					</td>
					<td style="height: 25px"> </td>
					<td style="height: 25px"></td>
				</tr>
				
				<tr>
				    <td style="height:25px">
                        <asp:ImageButton ID="ibConfirm" runat="server" AlternateText="Confirm" ImageUrl="../../images/butt_confirm.gif"
                            OnClick="btnPreview_Click" />
                        <input alt="Cancel" height="20" onclick="javascript:window.close();" src="../../images/butt_cancel.gif"
                            type="image" width="58" /></td>
				    <td style="height: 25px" colspan="3">
                        &nbsp;&nbsp;<%--<asp:CompareValidator id="cvAmount" display="dynamic" runat="server" 
							ControlToValidate="txtAmount" Text="The value must greater then 0. " 
							ValueToCompare="0" Operator="GreaterThan" Type="Double"/>--%>
					</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>
				</tr>
				
				
								
				
				<tr>
				    <td colspan="6" style="height: 2px">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<%--<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />&nbsp;
						<asp:ImageButton id="btnDelete" imageurl="../../images/butt_delete.gif" visible="true" CausesValidation="false" AlternateText=" Delete " onclick="btnDelete_Click" runat="server" />
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="False" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />--%>
					</td>
				</tr>
			</table>
            <input type=hidden id=hidTrxID value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
