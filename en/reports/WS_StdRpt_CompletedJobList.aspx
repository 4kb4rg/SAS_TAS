<%@ Page Language="vb" src="../../include/reports/WS_StdRpt_CompletedJobList.aspx.vb" Inherits="WS_StdRpt_CompletedJobList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WS_STDRPT_SELECTION_CTRL" src="../include/reports/WS_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Workshop - Completed Job Sheet</title>
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
					<td class="font9Tahoma" colspan="3"><strong> WORKSHOP - COMPLETED JOB SHEET</strong></td>
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
					<td colspan="6"><UserControl:WS_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:label id=lblCompany visible=false runat=server />
									<asp:label id=lblVehicle visible=false runat=server />
									<asp:label id=lblServTypeCode visible=false runat=server /></td>										
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
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma">	
				<tr>
					<td>Job ID From :</td>
					<td><asp:textbox id="txtJobIDFrom" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtJobIDTo" maxlength=20 width="50%" Runat=server /> (blank for all)</td>
				</tr>
				<tr>
					<td><asp:label id=lblTypeOfVeh runat=server /></td>
					<td><asp:dropdownlist id="lstTypeOfVeh" width=50% runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblVehRegNo runat=server /></td>
					<td><asp:textbox id="txtVehRegNo" maxlength=8 width=50% runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td><asp:label id=lblBillPartyCode runat=server /></td>
					<td><asp:TextBox id="txtBillPartyCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>							
				<tr>
					<td width=15%>Print Status :</td>
					<td width=35%><asp:dropdownlist id="lstPrintStatus" width=50% runat="server" /></td>
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
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>			
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
