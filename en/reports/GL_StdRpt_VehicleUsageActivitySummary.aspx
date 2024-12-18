<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_VehicleUsageActivitySummary.aspx.vb" Inherits="GL_StdRpt_VehicleUsageActivitySummary" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Vehicle Usage Activity Summary</title>
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
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - VEHICLE USAGE ACTIVITY SUMMARY REPORT</strong></td>
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
                    </td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>
				<tr>
					<td width=25%><asp:label id="lblVehUsgId" runat="server" /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchVehUsgId" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr>
					<td width=25%><asp:label id="lblVehCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchVehCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr>
					<td width=25%><asp:label id="lblVehTypeCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:TextBox id="txtSrchVehTypeCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr>
					<td width=25%><asp:label id="lblAccCode" runat=server /> : </td>  
					<td colspan=2>
						<asp:TextBox id="txtSrchAccCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr>
					<td><asp:label id="lblBlock" runat="server" /> Type : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlBlkType" AutoPostBack=true width="25%" runat="server" />
					</td>
				</tr>
				<tr id=TrBlkGrp>
					<td style="height: 26px"><asp:label id="lblBlkGrpCode" runat="server" /> : </td>
					<td colspan=2 style="height: 26px">
						<asp:textbox id="txtSrchBlkGrpCode" maxlength="8" width="25%" runat="server" /> (blank for all)
					</td>
				</tr>	
				<tr id=TrBlk>
					<td width=25%><asp:label id="lblBlkCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:textbox id="txtSrchBlkCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr id=TrSubBlk>
					<td width=25%><asp:label id="lblSubBlkCode" runat="server" /> : </td>
					<td colspan=2>
						<asp:textbox id="txtSrchSubBlkCode" width="25%" runat="server"/> (blank for all)
					</td>			
				</tr>
				<tr>
					<td>Activity : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlVehAct" width="25%" runat="server" />
					</td>
				</tr>
				<tr>
					<td>Status : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlStatus" width="25%" runat="server" />
					</td>
				</tr>
				<tr>
					<td>Perhitungan : </td>
					<td colspan=2>
						<asp:DropDownList id="ddlHitung" width="25%" runat="server" >
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Sewa</asp:ListItem>
                            <asp:ListItem>Alokasi BBM</asp:ListItem>
                        </asp:DropDownList>
					</td>
				</tr>					
				<tr>
					<td width=25%>					
                        Rekap Type :</td>
					<td width=75%><asp:RadioButton ID="rbUsage" runat="server" Checked="true" GroupName="rbType" Text=" Usage" />&nbsp;
                        &nbsp;<asp:RadioButton ID="rbAct" runat="server" Checked="false" GroupName="rbType" Text=" Activity" /></td>
				</tr>									
				<tr>
					<td width=25%>Suppress zero balance : </td>
					<td width=75%><asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />
					&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                        <asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" /></td>			
				</tr>										
				<tr>
					<td colspa=3>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" />
                    </td>					
				</tr>				
			</table>
                        </div>
            </td>
            </tr>
            </table>
		</form>
		<asp:label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:label id="lblID" visible="false" text=" ID" runat="server" />
		<asp:label id="lblHidCostLevel" visible="false" runat="server" />
		<asp:label id="lblVehUsg" visible="false" runat="server" />
		<asp:label id="lblVehicle" visible="false" runat="server" />
		<asp:label id="lblVehType" visible="false" runat="server" />
		<asp:label id="lblAccount" visible="false" runat="server" />
		<asp:label id="lblSubBlk" visible="false" runat="server" />
		<asp:label id="lblBlkGrp" visible="false" runat="server" />
		<asp:label id="lblRunUnit" visible="false" runat="server" />
		<asp:label id="lblLocation" visible="false" runat="server" />
	</body>
</HTML>
