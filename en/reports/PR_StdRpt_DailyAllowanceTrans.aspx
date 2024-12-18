<%@ Page Language="vb" src="../../include/reports/PR_StdRpt_DailyAllowanceTrans.aspx.vb" Inherits="PR_StdRpt_DailyAllowanceTrans" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="PR_STDRPT_SELECTION_CTRL" src="../include/reports/PR_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Payroll - Daily Attendance Transactions Listing</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">	
         		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
                		
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="mt-h">PAYROLL - DAILY ATTENDANCE TRANSACTIONS LISTING</td>
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
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table2" runat="server">
				<tr>
					<td width=17%>Employee Code From : </td>
					<td width=39%><asp:Textbox id=txtFromEmp maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td width=4%>To : </td>	
					<td width=40%><asp:Textbox id=txtToEmp maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				<tr>
					<td>Employee Status : </td>
					<td><asp:dropdownlist id=lstStatus width=50% runat=server>
							<asp:ListItem text="All" value="All" />
							<asp:ListItem text="Active" value="1" />
							<asp:ListItem text="Terminated" value="2" />
						</asp:DropDownList>&nbsp;
					</td>			
					<td>&nbsp;</td>	
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Document No From : </td>
					<td><asp:Textbox id=txtDocNoFrom maxlength=20 width=50% runat=server/> (leave blank for all)</td>			
					<td>To : </td>	
					<td><asp:Textbox id=txtDocNoTo maxlength=20 width=50% runat=server/> (leave blank for all)</td>
				</tr>	
				<tr>
					<td>Gang Code : </td>
					<td><asp:Textbox id=txtGangCode maxlength=8 width=50% runat=server /> (leave blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td><asp:label id="lblAccCode" runat=server /> : </td>  
					<td>
						<asp:TextBox id="txtSrchAccCode" maxlength=32 width=50% runat="server"/> (blank for all)
					</td>	
					<td>&nbsp;</td>
					<td>&nbsp;</td>		
				</tr>

				<tr>
					<td><asp:label id="lblBlock" runat="server" /> Type : </td>
					<td>
						<asp:DropDownList id="ddlBlkType" width=50% AutoPostBack=true runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>	
				</tr>
				<tr id=TrBlkGrp>
					<td><asp:label id="lblBlkGrpCode" runat="server" /> : </td>
					<td>
						<asp:textbox id="txtSrchBlkGrpCode" maxlength=8 width=50% runat="server" /> (blank for all)
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>	
				</tr>	
				<tr id=TrBlk>
					<td><asp:label id="lblBlkCode" runat="server" /> : </td>
					<td>
						<asp:textbox id="txtSrchBlkCode" maxlength=8 width=50% runat="server"/> (blank for all)
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr id=TrSubBlk>
					<td><asp:label id="lblSubBlkCode" runat="server" /> : </td>
					<td>
						<asp:textbox id="txtSrchSubBlkCode" maxlength=8 width=50% runat="server"/> (blank for all)
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>			
				</tr>	
				<tr>
					<td><asp:label id="lblVehCode" runat="server" /> : </td>
					<td>
						<asp:TextBox id="txtSrchVehCode" maxlength=8 width=50% runat="server"/> (blank for all)
					</td>	
					<td>&nbsp;</td>
					<td>&nbsp;</td>			
				</tr>	
				<tr>
					<td><asp:label id="lblVehExpCode" runat="server" /> : </td>
					<td>
						<asp:TextBox id="txtSrchVehExpCode" maxlength=8 width=50% runat="server"/> (blank for all)
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>										
				<tr>
					<td>&nbsp;</td>
					<td colspan=3>&nbsp;</td>
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
		<asp:Label id="lblLocation" visible="false" runat="server" />
		<asp:Label id="lblCode" text=" Code" visible="false" runat="server" />
		<asp:Label id="lblHidCostLevel" visible="false" runat="server" />
	</body>
</HTML>
