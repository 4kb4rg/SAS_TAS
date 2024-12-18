<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_TrialBalDaily.aspx.vb" Inherits="GL_StdRpt_TrialBalDaily" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Trial Balance Daily Report</title>
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
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>GENERAL LEDGER - TRIAL BALANCE DAILY REPORT</strong> </td>
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
					<td colspan="6" style="height: 21px">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                        <input id="hidBlockCharge" runat="server" type="hidden" /><asp:Label ID="Label2"
                            runat="server" Text=" Code" Visible="false"></asp:Label></td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				
				<tr>
					<td width="15%">Date From:</td>
					<td width="30%">    
						<asp:Textbox id="txtDate" width=60% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>	
					<td width="5%">&nbsp;</td>
					<td width="15%">Date To:</td>
					<td style="width: 30%">    
						<asp:Textbox id="txtDateTo" width=60% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>		
					<td>&nbsp;</td>	
				</tr>	
				
				
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode"  Text= "COA Code :" runat="server" /> </td>
					<td colspan="4"> 
                        <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" MaxLength="15" 
                            width="24%"></asp:TextBox>
                        <asp:TextBox ID="txtAccName" runat="server" Font-Bold="True" maxlength="10" 
                            width="66%"></asp:TextBox>
&nbsp;<input id="Find" class="button-small" runat="server" causesvalidation="False" 
                            onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');" 
                            type="button" value=" ... " />  
									   	  <asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode2" Text= "To COA Code :" runat="server" /> </td>
					<td colspan="4"> <asp:TextBox ID="txtAccCode2" runat="server" 
                            AutoPostBack="True" MaxLength="15" Width="24%"></asp:TextBox>
                        <asp:TextBox ID="txtAccName2" runat="server" Font-Bold="True" MaxLength="10" 
                            Width="66%"></asp:TextBox>
&nbsp;<input id="Find2" class="button-small"  runat="server" causesvalidation="False" 
                            onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode2', 'txtAccName2', 'False');" 
                            type="button" value=" ... " /></td>
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
                        <asp:TextBox ID="TxtBlkCode" runat="server" Width="23.4%"></asp:TextBox>&nbsp;
                        <asp:Label
                            ID="Label1" runat="server">(Blank For All)</asp:Label></td>
                    <td width="5%">
                    </td>
                </tr>
					
				<tr>
					<td colspan=6></td>
				</tr>
					
			    <tr>
					<td colspan="6">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>
											
				<tr>
					<td colspan=6>&nbsp;</td>
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
