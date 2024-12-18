<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_DetailedTrialBalance.aspx.vb" Inherits="GL_StdRpt_DetailedTrialBalance" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Detailed Trial Balance</title>
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
					<td class="font9Tahoma" colspan="3"><strong>GENERAL LEDGER - DETAILED TRIAL BALANCE</strong> </td>
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
                        <hr style="width :100%" /> 
                        <asp:Label ID="lblPleaseSelect" runat="server" Text="Please select " Visible="false"></asp:Label>
                        <asp:Label ID="lblSelect" runat="server" Text="Select " Visible="false"></asp:Label></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
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
&nbsp;<input id="Find2" class="button-small"  runat="server" causesvalidation="False" 
                            onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode2', 'txtAccName2', 'False');" 
                            type="button" value=" ... " />  
							              
						</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
                <tr>
                    <td width="17%">
                        Charge Level :*
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="ddlChargeLevel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChargeLevel_OnSelectedIndexChanged"
                            Width="24%">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td width="17%">
                        <asp:Label ID="lblPreBlkTag" runat="server"></asp:Label></td>
                    <td colspan="3">
                        <asp:TextBox id="TxtBlkCode" width="23.4%" runat="server"/>&nbsp;
                        <asp:Label ID="Label1" runat="server">(Blank For All)</asp:Label></td>
                </tr>
				
				
				<tr>
					<td width=17%>Transaction ID From : </td>  
					<td width=39%><asp:TextBox id="txtSrchTrxIDFrom" width="50%" runat="server"/> </td>
					<td width=4%>To : </td>
					<td width=40%><asp:TextBox id="txtSrchTrxIDTo" width="50%" runat="server"/> </td>
				</tr>
				<tr>
					<td width=17% style="height: 27px">Document Date From : </td>  
					<td width=39% style="height: 27px">
						<asp:TextBox id="txtSrchDocDateFrom" width="50%" runat="server"/> 
						<a href="javascript:PopCal('txtSrchDocDateFrom');">
							<asp:Image id="btnSrchDocDateFrom" runat="server" ImageUrl="../Images/calendar.gif"/>
						</a>
						<asp:Label id="lblDateFrom" visible="false" forecolor=red runat="server" />
					</td>
					<td width=4% style="height: 27px">To : </td>
					<td width=40% style="height: 27px">
						<asp:TextBox id="txtSrchDocDateTo" width="50%" runat="server"/> 
						<a href="javascript:PopCal('txtSrchDocDateTo');">
							<asp:Image id="btnSrchDocDateTo" runat="server" ImageUrl="../Images/calendar.gif"/>
						</a>
						<asp:Label id="lblDateTo" visible="false" forecolor=red runat="server" />
					</td>
				</tr>
				<tr>
					<td width=17%>Accounting Period From : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlSrchAccMonthFrom" size=1 width=20% runat=server />
						<asp:DropDownList id="ddlSrchAccYearFrom" size=1 width=30% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server />
					</td>
					<td width=4%>To : </td>
					<td width=40%>
						<asp:DropDownList id="ddlSrchAccMonthTo" size=1 width=20% runat=server />
						<asp:DropDownList id="ddlSrchAccYearTo" size=1 width=30% autopostback=true onselectedindexchanged=OnIndexChage_ToAccPeriod runat=server />
					</td>
				</tr>
				
				<tr>
					<td width=17%>Suppress zero balance : </td>
					<td width=39%>
						<asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />		
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>
					<td width=4%></td>
					<td width=40%></td>
				</tr>	
				<tr>
					<td colspan="4">
                        <asp:CheckBox id="cbTrans" text=" Display By Transaction" checked="true" runat="server" /></td>
				</tr>	
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>
														
				<tr>
					<td colspan=4>&nbsp;<asp:Label ID="lblFromLocCode" runat="server" Text="From Location :*" Visible="false"></asp:Label><asp:DropDownList
                            ID="ddlFromLocCode" runat="server" Visible="false" Width="100%">
                        </asp:DropDownList><asp:Label ID="lblErrFromLocCode" runat="server" ForeColor="red"
                            Text="Please select Location Code" Visible="false"></asp:Label>&nbsp;</td>
				</tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
				<tr>
					<td colspan=4 align=left><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>					
				</tr>				
                <tr>
                    <td align="left" colspan="4">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <input id="hidBlockCharge" runat="server" type="hidden" /></td>
                </tr>
			</table>
                </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
	</body>
</HTML>
