<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_Cheque.aspx.vb" Inherits="PR_StdRpt_Cheque" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Cheque Listing</title>
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
					<td class="font9Tahoma"><strong>PAYROLL - CHEQUE LISTING</strong> </td>
					<td align="right"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="2"><hr size="1" noshade>
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
					<td colspan="2"><hr size="1" noshade>
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat="server">
				<tr>
					<td width=15%>Bank Code : </td>
					<td width=35%><asp:DropDownList id=ddlBankCode AutoPostBack=true onSelectedIndexChanged=IndexBankChanged width="100%" runat=server/></td>
					<td width=15%>&nbsp;</td>
					<td width=35%><asp:TextBox id=txtProgramPath visible=false maxlength=20 width=50% runat=server /></td>
				</tr>
				<tr>
					<td>Company Code :</td>
					<td><asp:DropDownList id="ddlCompCode" AutoPostBack=true width="100%" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td>Department Code :</td>
					<td><asp:DropDownList id="ddlDeptCode" AutoPostBack=true width="50%" runat="server" /></td>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td>Employee ID From : </td>
					<td><asp:Textbox id=txtFromEmp maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td>To : </td>	
					<td><asp:Textbox id=txtToEmp maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				<tr>
					<td>Terminate Date From : </td>
					<td><asp:TextBox id=txtTerminateDateFr width=50% maxlength=10 runat=server />
						<a href="javascript:PopCal('txtTerminateDateFr');"><asp:Image id="btnSelDateFr" runat="server" ImageUrl="../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrTerminateDateFr visible=False forecolor=red text="<br>Invalid date format." runat=server /></td>
					<td>To : </td>	
					<td><asp:TextBox id=txtTerminateDateTo width=50% maxlength=10 runat=server />
						<a href="javascript:PopCal('txtTerminateDateTo');"><asp:Image id="btnSelDateTo" runat="server" ImageUrl="../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrTerminateDateTo visible=False forecolor=red text="<br>Invalid date format." runat=server /></td>
				</tr>														
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>	
				<tr>
					<td>Starting Cheque No : </td>
					<td><asp:Textbox id=txtStartChequeNo maxlength=20 width=50% runat=server/>
						<asp:RequiredFieldValidator 
							id="validStartChequeNo" 
							ControlToValidate="txtStartChequeNo"
							ErrorMessage="Starting Cheque No is a required field."
							ForeColor="Red" 
							runat="server" />
						</td>
					<td>No of Cheque : </td>	
					<td><asp:Textbox id=txtNoOfCheque maxlength=20 width=50% runat=server/>
						<asp:RequiredFieldValidator 
							id="validNoOfCheque" 
							ControlToValidate="txtNoOfCheque"
							ErrorMessage="No of Cheque is a required field."
							ForeColor="Red" 
							runat="server" />
						</td>
				</tr>	
				<tr>
					<td colspan=2><asp:Label id="lblErrChequeFormat" text="Invalid Cheque Format." visible="false" runat="server" /></td>
					<td colspan=2><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="4"><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />
					<!-- Add by YWC on 3/6/2004 on BFR 00027 item (25) -->
					<asp:Label id=lblErrBankCode text='Printing is disallowed as the Bank Code is empty' ForeColor=Red visible=false runat=server />
					<!-- End Add -->
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
