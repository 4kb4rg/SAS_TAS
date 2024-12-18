<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_MthWorkShopExpSummary.aspx.vb" Inherits="GL_StdRpt_MthWorkShopExpSummary" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" TagName="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<html>
	<head>
		<title>General Ledger - Monthly Workshop Expenditure Report Summary</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" RunAt="server" />
	</head>
	<body>
		<form RunAt="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:label id="lblActGrpText" visible="false" text=" Code" runat=server />
			<asp:label id="lblActGrpCode" visible="false" text=" Code" runat=server />
			<table Border="0" CellSpacing="1" CellPadding="1" Width="100%" class="font9Tahoma">
				<tr>
					<td Class="font9Tahoma" ColSpan="3"><strong> GENERAL LEDGER - MONTHLY WORKSHOP EXPENDITURE REPORT SUMMARY</strong></td>
					<td Align="right" ColSpan="3"><asp:Label ID="lblTracker" RunAt="server" /></td>
				</tr>
				<tr>
					<td ColSpan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"><UserControl:GL_StdRpt_Selection_Ctrl ID="RptSelect" RunAt="server" /></td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"><asp:Label ID="lblErrMsg" ForeColor="red" Visible="False" RunAt="server" /></td>
				</tr>
				<tr>
					<td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"><hr Size="1" NoShade></td>
				</tr>
			</table>
			<table Width="100%" Border="0" CellSpacing="2" CellPadding="1" class="font9Tahoma" RunAt="server">
				<tr>
					<td colspan=2><asp:label id=lblErrActGrp forecolor=red visible=false runat=server /></td>
				</tr>
				<tr>
					<td valign=top><asp:label id=lblActGrp runat=server/> :</td>
					<td colspan=2>
						<table width=100% cellpadding=0 cellspadding=0 border=0>
							<tr>
								<td width=5% valign=top><asp:CheckBox text=All id="cbActGrpAll" OnCheckedChanged=Check_Clicked AutoPostBack=true runat=server /></td>
								<td width=95% valign=top><asp:CheckBoxList id="cblActGrp" OnSelectedIndexChanged=ActGrpCheckList AutoPostBack=True RepeatColumns="1" RepeatDirection="Vertical" runat=server /></td>			
							</tr>
						</table>
					</td>
				</tr>
				
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td width=5%><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" /></td>			
					<td width=70%><asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>
				</tr>											
				<tr>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan=4><asp:ImageButton ID="ibPrintPreview" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" OnClick="ibPrintPreview_OnClick" RunAt="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
                    </td>
				</tr>
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label ID="lblErrMessage" Visible="False" Text="Error while initiating component." RunAt="server" />
		<asp:Label ID="lblLangCapLocation" Visible="False" RunAt="server" />
		<asp:Label ID="lblLangCapBlock" Visible="False" RunAt="server" />
		<asp:Label ID="lblLangCapSubBlock" Visible="False" RunAt="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat=server />
	</body>
</html>
