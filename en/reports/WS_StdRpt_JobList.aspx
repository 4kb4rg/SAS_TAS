<%@ Page Language="vb" src="../../include/reports/WS_StdRpt_JobList.aspx.vb" Inherits="WS_StdRpt_JobList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="WS_STDRPT_SELECTION_CTRL" src="../include/reports/WS_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Workshop - Job Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id=lblBlankForAll visible=false Text=" (blank for all)" runat=server />
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>WORKSHOP - JOB LISTING</strong> </td>
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
				<tr class="font9Tahoma">
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" />
									<asp:label id=lblCompany visible=false runat=server />
									<asp:label id=lblVehicle visible=false runat=server />
									<asp:label id=lblAccCode visible=false runat=server />
									<asp:label id=lblBlkCode visible=false runat=server />
									<asp:label id=lblWorkCode visible=false runat=server />
									<asp:label id=lblVehExpCode visible=false runat=server />
									<asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" />
					</td>				
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
					<td>Job ID From :</td>
					<td><asp:textbox id="txtJobIDFrom" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox ID="txtJobIDTo" maxlength=20 width="50%" Runat=server /> (blank for all)</td>
				</tr>
				<tr>
					<td>Job Start Date From :</td>
					<td><asp:TextBox id="txtJobStartDateFrom" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtJobStartDateFrom');">
  								  <asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>						
					<td>To :</td>
					<td><asp:TextBox id="txtJobStartDateTo" size="12" width=50% maxlength="10" runat="server"/>
  								  <a href="javascript:PopCal('txtJobStartDateTo');">
  								  <asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>										
				</tr>
				<tr>
					<td>Job Type :</td>
					<td><asp:dropdownlist id="lstJobType" AutoPostBack="True" onSelectedIndexChanged="JobTypeChange" width=50% runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>					
				<tr id=rowBillPartyCode>
					<td><asp:label id=lblBillPartyCode runat=server /> :</td>
					<td><asp:TextBox id="txtBillPartyCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>	
				</tr>
				<tr id=rowEmpCode>
					<td>Employee Code :</td>
					<td><asp:textbox id="txtEmpID" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
				</tr>	
				<tr>
					<td width=15%>Job Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" width="50%" size="1" runat="server" /></td>
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
