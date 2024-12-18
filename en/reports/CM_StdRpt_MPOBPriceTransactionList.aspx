<%@ Page Language="vb" src="../../include/reports/CM_StdRpt_MPOBPRicetransactionList.aspx.vb" Inherits="CM_StdRpt_MPOBPRiceTransactionList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Contract Management - MPOB Price Transaction Listing</title>
            <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<!--<input type=hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> CONTRACT MANAGEMENT - MPOB PRICE TRANSACTION LISTING</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>	
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="80%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">
				<tr>
					<td>Month : </td>
					<td>
						<asp:DropDownList id="ddlAccMonth" width="22%" runat=server>
							<asp:ListItem text="Jan" value="1" />
							<asp:ListItem text="Feb" value="2" />
							<asp:ListItem text="Mar" value="3" />
							<asp:ListItem text="Apr" value="4" />
							<asp:ListItem text="May" value="5" />
							<asp:ListItem text="Jun" value="6" />
							<asp:ListItem text="Jul" value="7" />
							<asp:ListItem text="Aug" value="8" />
							<asp:ListItem text="Sep" value="9" />
							<asp:ListItem text="Oct" value="10" />
							<asp:ListItem text="Nov" value="11" />
							<asp:ListItem text="Dec" value="12" />
							<asp:ListItem text="All" value="" Selected />																	
						</asp:DropDownList>&nbsp;
					<asp:DropDownList id="ddlAccYear" width="25%" runat="server" />
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="17%">Product : </td>
					<td width="39%"><asp:DropDownList id="ddlProductCode" width="50%" runat="server" /> </td>
					<td width="4%">&nbsp;</td>
					<td width="40%">&nbsp;</td>
				</tr>					
				<tr>
					<td>Status :</td>
					<td><asp:DropDownList id="lstStatus" width="50%" size="1" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>	
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Height="26px" Visible="False" />
                    </td>
				</tr>				
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:Label id="lblLocation" visible="false" runat="server" />
	</body>
</HTML>
