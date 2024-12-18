<%@ Page Language="vb" src="../../include/reports/BD_StdRpt_ExtractionRateList.aspx.vb" Inherits="BD_StdRpt_ExtractionRateList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="BD_STDRPT_SELECTION_CTRL" src="../include/reports/BD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Budgeting - Extraction Rate Budget Transaction Listing</title>
           <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">	
         		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
                	
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> BUDGETING - EXTRACTION RATE TRANSACTION LISTING</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" />
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:BD_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><asp:label id="lblErrMsg" forecolor=red text="" runat="server" /></td>
				</tr>				
				<tr>
					<td colspan="6"><hr style="width :100%" />
				</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" runat="server">
				<tr>
					<td width=17%>Period From :</td>
					<td width=39%>
						<asp:TextBox id="txtMonthFrom" maxlength=7 width="50%" runat="server" />
						<asp:RegularExpressionValidator id=revMonthFrom 
								ControlToValidate="txtMonthFrom"
								ValidationExpression="([0][123456789]|[1][012])\/\d{4}"
								Display="Dynamic"
								text="<BR>2 digits Month and 4 digits year separated with slash."
								runat="server"/>
					</td>
					<td width=4%>To :</td>
					<td width=40%>
						<asp:TextBox id="txtMonthTo" maxlength=7 width="50%" runat="server" /> (blank for all)
						<asp:RegularExpressionValidator id=revMonthTo 
								ControlToValidate="txtMonthTo"
								ValidationExpression="([0][123456789]|[1][012])\/\d{4}"
								Display="Dynamic"
								text="<BR>2 digits Month and 4 digits year separated with slash."
								runat="server"/>
					</td>									
				</tr>						
				<tr>
					<td width=17%>Updated Date From :</td>
					<td width=39%>
						<asp:TextBox id="txtUpdatedDateFrom" maxlength=10 width="50%" runat="server" />
						<a href="javascript:PopCal('txtUpdatedDateFrom');">
						<asp:Image id="btnUpdatedDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:Label id="lblUpdatedDateFrom" visible="false" forecolor=red runat="server" />
					</td>
					<td width=4%>To :</td>
					<td width=40%>
						<asp:TextBox id="txtUpdatedDateTo" maxlength=10 width="50%" runat="server" />
						<a href="javascript:PopCal('txtUpdatedDateTo');">
						<asp:Image id="btnUpdatedDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>
						<asp:Label id="lblUpdatedDateTo" visible="false" forecolor=red runat="server" />
					</td>									
				</tr>
				<tr>
					<td width=17%>Updated By :</td>
					<td width=39%><asp:TextBox id="txtUpdatedBy" maxlength=7 width="50%" runat="server" /> (blank for all)</td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>									
				</tr>
				<tr>
					<td width=17%>Status :</td>
					<td width=39%><asp:DropDownList id="ddlStatus" width="50%" runat="server" /></td>
					<td width=4%>&nbsp;</td>
					<td width=40%>&nbsp;</td>									
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>						
				<!--<tr>
					<td colspan=5><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>-->
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>
				</tr>
			</table>
            </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:Label id="lblLocationTag" visible="false" runat="server" />
	</body>
</HTML>
