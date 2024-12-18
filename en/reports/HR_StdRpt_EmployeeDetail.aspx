<%@ Page Language="vb" src="../../include/reports/HR_StdRpt_EmployeeDetail.aspx.vb" Inherits="HR_StdRpt_EmployeeDetail" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="HR_STDRPT_SELECTION_CTRL" src="../include/reports/HR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Human Resource - Employee Details </title>
              <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">  
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>HUMAN RESOURCE - EMPLOYEE DETAILS</strong> </td>
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
					<td colspan="6"><UserControl:HR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
					<td>Employee Code From : </td>
					<td><asp:textbox id="txtEmployeeCodeFrom" width="50%" maxlength=20 runat="server" /> (blank for all)</td>
					<td>To : </td>
					<td><asp:textbox ID="txtEmployeeCodeTo" width="50%" maxlength=20 Runat=server /> (blank for all)</td>								
				</tr>
				<tr>
					<td width=15%>Employee Name : </td>
					<td width=35%><asp:textbox id="txtEmployeeName" width="50%" maxlength=20 runat="server" /> (blank for all)</td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>									
				</tr>			
				<tr>
					<td width=15%>Gang Code : </td>
					<td width=35%><asp:textbox id="txtGangCode" width="50%" maxlength=20 runat="server" /> (blank for all)</td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>									
				</tr>
				<tr>
					<td width=15%>Gender : </td>
					<td width=35%><asp:DropDownList id="lstGender" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td width=15%>Marital Status : </td>
					<td width=35%><asp:DropDownList id="lstMaritalStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td width=15%>Nationality : </td>
					<td width=35%><asp:DropDownList id="lstNationality" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td width=15%>Race : </td>
					<td width=35%><asp:DropDownList id="lstRace" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td width=15%>Religion : </td>
					<td width=35%><asp:DropDownList id="lstReligion" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td>Date of Birth From : </td>
					<td><asp:textbox id="txtDOBFrom" width="50%" maxlength=10 runat="server" />
						<a href="javascript:PopCal('txtDOBFrom');"><asp:Image id="btnSelDateFr" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>					
					<td>To : </td>
					<td><asp:textbox id="txtDOBTo" width="50%" maxlength=10 runat="server" />
						<a href="javascript:PopCal('txtDOBTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a></td>
				</tr>
				<tr>
					<td width=15%>Status : </td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td width=15%><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
					<td width=35%><asp:Label id="lblDateFormat" forecolor=red visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
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
