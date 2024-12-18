<%@ Page Language="vb" src="../../include/reports/BD_StdRpt_FFBProductionSum.aspx.vb" Inherits="BD_StdRpt_FFBProductionSum" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="BD_STDRPT_SELECTION_CTRL" src="../include/reports/BD_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Budgeting - FFB Production Budget Summary</title>
               <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
          		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
                	
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1"  class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> BUDGETING - FFB PRODUCTION BUDGET SUMMARY</strong></td>
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
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=17%>Year :</td>
					<td width=39%>
						<asp:DropDownList id="ddlYear" width="40%" runat="server" />
						<asp:RequiredFieldValidator id=validateYear display=Dynamic runat=server 
									ControlToValidate=ddlYear
									ErrorMessage="<BR>Please select a year" />	
					</td>
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
