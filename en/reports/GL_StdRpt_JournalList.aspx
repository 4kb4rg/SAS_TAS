<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_JournalList.aspx.vb" Inherits="GL_StdRpt_JournalList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Journal Listing</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" ID="frmMain" class="main-modul-bg-app-list-pu">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<!--<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>GENERAL LEDGER - JOURNAL LISTING</strong> </td>
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
					<td colspan="6"><UserControl:GL_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td>Journal ID From :</td>
					<td><asp:textbox id="txtJournalIDFrom" width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtJournalIDTo" width="50%" Runat=server /> (blank for all)</td>
				</tr>			
				<tr>
					<td>Reference Number :</td>
					<td><asp:textbox id="txtRefNo" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Transaction Type :</td>
					<td><asp:DropDownList id="lstTransType" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td>Update By :</td>
					<td><asp:textbox id="txtUpdatedBy" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td><asp:label id=lblAccCode runat=server /></td>
					<td><asp:textbox id="txtAccCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehType runat=server /></td>
					<td><asp:textbox id="txtVehType" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>																
				<tr>
					<td><asp:label id=lblVehCode runat=server /></td>
					<td><asp:textbox id="txtVehCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehExpCode runat=server /></td>
					<td><asp:textbox id="txtVehExpCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblBlkType runat=server /></td>
					<td><asp:DropDownList id="lstBlkType" AutoPostBack=true width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>			
				<tr id=TrBlkGrp>
					<td><asp:label id=lblBlkGrp runat=server /></td>
					<td><asp:textbox id="txtBlkGrp" maxlength="8" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>										
				</tr>																		
				<tr id=TrBlk>
					<td><asp:label id=lblBlkCode runat=server /></td>
					<td><asp:textbox id="txtBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr id=TrSubBlk>
					<td><asp:label id=lblSubBlkCode runat=server /></td>
					<td><asp:textbox id="txtSubBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>			
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td width=15%>Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>	
				 <tr>
					<td colspan="6">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>	
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>						
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
		
	</body>
</HTML>
