<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_TrialBalanceTrial.aspx.vb" Inherits="GL_StdRpt_TrialBalanceTrial" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Trial Balance Report</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
            <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu" ID="frmMain">
           		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - TRIAL BALANCE</strong></td>
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
					<td colspan="6" style="height: 36px">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
					<td width=17%>Accounting Period From : </td>  
					<td width=39%>
						<asp:DropDownList id="ddlSrchAccMonthFrom" size=1 width=20% runat=server />
					</td>
					<td width=4%>- </td>
					<td width=40%>
						<asp:DropDownList id="ddlSrchAccMonthTo" size=1 width=20% runat=server />
						<asp:DropDownList id="ddlSrchAccYear" size=1 width=30% runat=server />
					</td>
				</tr>	
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode" runat="server" /> : </td>
					<td colspan="4"> 
                        <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" MaxLength="15" Width="24%"></asp:TextBox><asp:TextBox
                            ID="txtAccName" runat="server" Font-Bold="True" MaxLength="10"
                            Width="61%"></asp:TextBox>&nbsp;
                        <input type="button" class="button-small" value=" ... " id="Find" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');" CausesValidation=False runat=server />  		   	
							</td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode2" runat="server" /> : </td>
					<td colspan="4"> 
                        <asp:TextBox ID="txtAccCode2" runat="server" AutoPostBack="True" MaxLength="15" Width="24%"></asp:TextBox><asp:TextBox
                            ID="txtAccName2" runat="server" Font-Bold="True" MaxLength="10"
                            Width="61%"></asp:TextBox>&nbsp;
							              <input type="button" class="button-small"  value=" ... " id="Find2" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode2', 'txtAccName2', 'False');"  CausesValidation=False runat=server /></td>
					<td width="5%">&nbsp;</td>
					
				</tr>
				<tr id="RowChargeLevel">
					<td height="15%">Charge Level :* </td>
					<td colspan=4><asp:DropDownList id="ddlChargeLevel" Width=90% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
				</tr>
				<tr id="RowPreBlk">
					<td height="15%"><asp:label id=lblPreBlkTag Runat="server"/> </td>
					<td colspan=4><asp:DropDownList id="ddlPreBlock" Width=90% runat=server />
								  <asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
				</tr>
				<tr id="RowBlk">
					<td height=15%><asp:label id=lblBlkTag Runat="server"/></td>
					<td colspan=4><asp:DropDownList id="lstBlock" Width=90% runat=server />
								  <asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
				</tr>		
				<tr id="RowPreBlkTo">
					<td height="15%"><asp:label id=lblPreBlkTagTo Runat="server"/> </td>
					<td colspan=4><asp:DropDownList id="ddlPreBlockTo" Width=90% runat=server />
								  <asp:label id=lblPreBlockErrTo Visible=False forecolor=red Runat="server" /></td>
				</tr>
				<tr id="RowBlkTo">
					<td height=15%><asp:label id=lblBlkTagTo Runat="server"/></td>
					<td colspan=4><asp:DropDownList id="lstBlockTo" Width=90% runat=server />
								  <asp:label id=lblBlockErrTo Visible=False forecolor=red Runat="server" /></td>
				</tr>		
				<tr>
					<td colspan=6></td>
				</tr>
				<tr>
					<td colspan=6></td>
				</tr>
				
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td colspan=2><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>			
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td width=25%><asp:label id=lblChartofAccType runat=server /> : </td>
					<td colspan=2 class="font9Tahoma">
						<asp:CheckBoxList id="cblAccType" RepeatColumns="2" repeatdirection="horizontal" autopostback="true" runat="server">
							<asp:listitem id="option1" text=" Balance Sheet" value="1" runat="server"/>
							<asp:listitem id="option2" text=" Profit and Loss" value="2" runat="server"/>
						</asp:CheckBoxList>
					</td>
					<td colspan=3>&nbsp;</td>			
				</tr>	
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>								
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" /></td>					
				</tr>				
			</table>
            </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:label id="lblType" visible="false" text=" Type" runat="server" />
		<asp:label id="lblLocation" visible="false" runat="server" />
		<asp:label id="lblAccDesc" visible="false" runat="server" />
		<Input type=hidden id=hidBlockCharge value="" runat=server/>
		<Input type=hidden id=hidChargeLocCode value="" runat=server/>
	</body>
</HTML>
