<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_MaintainHarvestList.aspx.vb" Inherits="GL_StdRpt_MaintainHarvestList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Maintenance & Harvesting Report</title>
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<!--<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>-->
			<input type=hidden id=hidActGrpCode runat="server" NAME="hidActGrpCode"/>
			
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - MAINTENANCE & HARVESTING REPORT</strong></td>
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
					<td colspan="6"><asp:Label id="lblActGrp" forecolor=red visible="false" runat="server" /></td>
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
			<table width=100% border="0" cellspacing="0" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				<tr>
					<td><asp:label id=lblBlkType runat=server /></td>
					<td colspan=2><asp:DropDownList id="lstBlkType" AutoPostBack=true width="25%" runat="server" /></td>
				</tr>			
				<tr id=TrBlkGrp>
					<td><asp:label id=lblBlkGrp runat=server /></td>
					<td colspan=2><asp:textbox id="txtBlkGrp" maxlength="8" width="25%" runat="server" /> (blank for all)</td>
				</tr>				
				<tr id=TrBlk>
					<td><asp:label id=lblBlkCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtBlkCode" maxlength="8" width="25%" runat="server" /> (blank for all)</td>
				</tr>
				<tr id=TrSubBlk>
					<td><asp:label id=lblSubBlkCode runat=server /></td>
					<td colspan=2><asp:textbox id="txtSubBlkCode" maxlength="8" width="25%" runat="server" /> (blank for all)</td>			
				</tr>
				<tr>
					<td valign=top><asp:label id=lblActGrpCode runat=server /></td>
					<td colspan=2>
						<table width=100% cellpadding=0 cellspadding=0>
							<tr>
								<td width=5% valign=top><asp:CheckBox text=All id="cbActGrpAll" OnCheckedChanged=Check_Clicked AutoPostBack=true runat=server /></td>
								<td width=95% valign=top><asp:CheckBoxList id="cblActGrp" OnSelectedIndexChanged=ActGrpCheckList AutoPostBack=True RepeatColumns="1" RepeatDirection="vertical" runat=server /></td>			
							</tr>
						</table>
					</td>												
				</tr>
				<tr id=trsumrprt>
					<td>Summarize report : </td>
					<td><asp:RadioButton id="rbSumYes" text="Yes" GroupName="rbSum" runat="server" /></td>			
					<td><asp:RadioButton id="rbSumNo" text="No" checked="true" GroupName="rbSum" runat="server" /></td>
				</tr>	
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td width=5%><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" /></td>			
					<td width=70%><asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>
				</tr>										
				<tr>
					<td colspan=3><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
				</tr>						
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Visible="False" />
                    </td>					
				</tr>				
			</table>
                    </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:label id="lblHidCostIsBlock" visible=false runat=server />
		<asp:label id="lblHidActGrpCode" visible=false runat=server />
		<asp:label id="lblHidSubBlkCode" visible=false runat=server />
		<asp:label id="lblHidBlkCode" visible=false runat=server />
		<asp:label id="lblHidBlkGrpCode" visible=false runat=server />
		<asp:label id="lblHidSubBlk" visible=false runat=server />
		<asp:label id="lblHidBlk" visible=false runat=server />
		<asp:label id="lblCode" text="Code" visible=false runat=server/>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
