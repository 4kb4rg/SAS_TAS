<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_BalDueOutLeave.aspx.vb" Inherits="PR_StdRpt_BalDueOutLeave" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Balance Due from Outstanding Leave</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<asp:Label id="lblLocation" visible="false" runat="server" />		
			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma" ID="Table1">
				<tr>
					<td class="font9Tahoma">PAYROLL - BALANCE DUE FROM OUTSTANDING LEAVE</td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2"><UserControl:PR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server" >			
				<tr>
					<td>Attendance Code :* </td>
					<td><asp:Textbox id=txtAttdCode maxlength=128 width=50% runat=server/> (E.g. : A, B, C, D)
						<asp:RequiredFieldValidator id=rvfAttdCode display=Dynamic runat=server
							ControlToValidate=txtAttdCode
							text="Please key in Attendance Code." />
						<asp:label id=lblAttdCode visible=false runat=server /></td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>											
				<tr>
					<td>Pay Type : </td>
					<td><asp:dropdownlist id=ddlPayType width=50% AutoPostBack=true OnSelectedIndexChanged=CheckPayType runat=server/></td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>											
				<tr id=trWorkDays visible=false>
					<td>No. of Working Days :* </td>
					<td><asp:textbox id=txtNoWorkDays maxlength=2 width=25% runat=server/>		
						<asp:RequiredFieldValidator id=rvfNoWorkDays display=Dynamic runat=server
							ControlToValidate=txtNoWorkDays
							text="Please key in No. of Working Days." />
						<asp:CompareValidator id="cvNoWorkDays" display=dynamic runat="server" 
							ControlToValidate="txtNoWorkDays" 
							Text="The value must be a whole number" 
							Type="Integer" Operator="DataTypeCheck"/></td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>											
				<tr>
					<td width=17%>Employee Code From : </td>
					<td width=39%><asp:Textbox id=txtEmpCodeFrom maxlength=20 width=50% runat=server/> (leave blank for all)
								  <asp:Label id=lblErrEmpCodeFrom text="Please key in Employee Code From." visible=false runat=server /></td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtEmpCodeTo maxlength=20 width=50% runat=server/> (leave blank for all)
								  <asp:Label id=lblErrEmpCodeTo text="Please key in Employee Code To." visible=false runat=server /></td>								
				</tr>	
				<tr>
					<td>Employee Status : </td>
					<td><asp:dropdownlist id=ddlEmpStatus width=50% runat=server /></td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td>Gang Code : </td>
					<td><asp:Textbox id=txtGangCode maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>											
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>
				</tr>
			</table>
            </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</HTML>
