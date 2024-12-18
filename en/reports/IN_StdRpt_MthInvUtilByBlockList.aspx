<%@ Page Language="vb" src="../../include/reports/IN_StdRpt_MthInvUtilByBlockList.aspx.vb" Inherits="IN_StdRpt_MthInvUtilByBlockList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="IN_STDRPT_SELECTION_CTRL" src="../include/reports/IN_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Inventory - Monthly Inventory Utilisation </title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
       		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
        <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong><asp:label id=lblHeader text="INVENTORY - MONTHLY INVENTORY UTILISATION " runat=server /></strong> </td>
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
					<td colspan="6"><UserControl:IN_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><asp:Label id="lblDate" forecolor=red visible="false" text="Incorrect Date Format. Date Format is " runat="server" /></td>
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
					<td><asp:label id=lblAccCode runat=server /></td>
					<td><asp:textbox id="txtAccCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblBlkType runat=server /></td>
					<td><asp:DropDownList id="lstBlkType" AutoPostBack=true width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>			
				<tr id=TrBlkGrp>
					<td><asp:label id=lblBlkGrp runat=server /></td>
					<td><asp:textbox id="txtBlkGrp" maxlength="8" width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>										
				</tr>							
				<tr id=TrBlk>
					<td><asp:label id=lblBlkCode runat=server /></td>
					<td><asp:textbox id="txtBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>					
				</tr>
				<tr id=TrSubBlk>
					<td><asp:label id=lblSubBlkCode runat=server /></td>
					<td><asp:textbox id="txtSubBlkCode" maxlength=8 width="50%" runat="server" /> (blank for all)</td>			
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>
				<tr>
					<td>Issue Type :</td>
					<td><asp:dropdownlist id="lstIssueType" width="50%" runat="server" /></td>			
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
				</tr>			
				<tr>
					<td><asp:label id=lblProdTypeCode runat=server /></td>
					<td><asp:textbox id="txtProdType" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdBrandCode runat=server /></td>
					<td><asp:textbox id="txtProdBrand" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblProdModelCode runat=server /></td>
					<td><asp:textbox id="txtProdModel" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td><asp:label id=lblProdCatCode runat=server /></td>
					<td><asp:textbox id="txtProdCat" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id=lblProdMatCode runat=server /></td>
					<td><asp:textbox id="txtProdMaterial" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:label id=lblStkAnaCode runat=server /></td>
					<td><asp:textbox id="txtStkAna" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>															
				<tr>
					<td width=15%>Item Code :</td>
					<td width=35%><asp:textbox id="txtItemCode" maxlength=20 width="50%" runat="server" /> (blank for all)</td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>		
				<tr>
					<td width=20%>Include Workshop Item : </td>
					<td width=5%><asp:checkbox id="ckWS" checked="false" runat="server" /></td>			
					<td width=15%>&nbsp;</td>
					<td width=30%>&nbsp;</td>								
				</tr>										
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>															
				<tr>
					<td colspan=4><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>					
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
