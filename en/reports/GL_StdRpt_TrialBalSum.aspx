<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_TrialBalSum.aspx.vb" Inherits="GL_StdRpt_TrialBalSum" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Trial Balance Temporary Report</title>
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>

             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>GENERAL LEDGER - TRIAL BALANCE TEMPORARY REPORT</strong> </td>
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
                    <td colspan="6">
                    </td>
                </tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                        <input id="hidBlockCharge" runat="server" type="hidden" />
                        <asp:Label ID="Label2" runat="server" Text=" Code" Visible="false"></asp:Label></td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode" runat="server" /> : </td>
					<td colspan="4"> 
                        <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" MaxLength="15" 
                            Width="22%"></asp:TextBox>
                        <asp:TextBox ID="txtAccName" runat="server" Font-Bold="True" MaxLength="10" 
                            Width="53%"></asp:TextBox>
&nbsp;<input id="Find" class="button-small" runat="server" causesvalidation="False" 
                            onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');" 
                            type="button" value=" ... " />  
									   	  <asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode2" runat="server" /> : </td>
					<td colspan="4"> 
                        <asp:TextBox ID="txtAccCode2" runat="server" AutoPostBack="True" MaxLength="15" 
                            Width="22%"></asp:TextBox>
                        <asp:TextBox ID="txtAccName2" runat="server" Font-Bold="True" MaxLength="10" 
                            Width="53%"></asp:TextBox>
&nbsp;<input id="Find2"  class="button-small"  runat="server" causesvalidation="False" 
                            onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode2', 'txtAccName2', 'False');" 
                            type="button" value=" ... " />  
							              
						</td>
					<td width="5%">&nbsp;</td>
					
			    </tr>
                <tr>
                    <td width="15%">
                        Charge Level :*
                    </td>
                    <td colspan="4">
                        <asp:DropDownList ID="ddlChargeLevel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChargeLevel_OnSelectedIndexChanged"
                            Width="24%">
                        </asp:DropDownList></td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td width="15%">
                        <asp:Label ID="lblPreBlkTag" runat="server"></asp:Label></td>
                    <td colspan="4">
                        <asp:TextBox ID="TxtBlkCode" runat="server" Width="23.4%"></asp:TextBox>
                        <asp:Label ID="Label1" runat="server">(Blank For All)</asp:Label></td>
                    <td width="5%">
                    </td>
                </tr>
					
				<tr>
					<td colspan=6>OR</td>
				</tr>
					
			
				
					<tr>
					<td width=17%>Transaction ID From : </td>  
					<td width=39%><asp:TextBox id="txtSrchTrxIDFrom" width="70%" runat="server"/> </td>
					<td width=4%>To : </td>
					<td width=40%><asp:TextBox id="txtSrchTrxIDTo" width="70%" runat="server"/> </td>
				</tr>						
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr class="font9Tahoma">
					<td colspan="6">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>					
				</tr>				
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
	</body>
</HTML>
