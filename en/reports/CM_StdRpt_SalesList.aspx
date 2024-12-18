<%@ Page Language="vb" Inherits="CM_StdRpt_SalesList" src="../../include/reports/CM_StdRpt_SalesList.aspx.vb" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<html>
	<head>
		<title>Contract Management - Sales</title> 
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</head>
	<body>
		<form runat="server"  class="main-modul-bg-app-list-pu" id="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> CONTRACT MANAGEMENT - SALES</td>
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
					<td colspan="6"><UserControl:CM_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
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
			<table width="100%" border="0" cellspacing="1" cellpadding="1" class="font9Tahoma">
				
				<tr>
					<td width=17%>Bill Party Code : </td>  
					<td width=39%>
						<asp:TextBox id="txtBuyerCode" width="50%" runat="server"/> 
						<input type=button value=" ... " id="Find" onclick="javascript:PopBillParty('frmMain', '', 'ddlBuyer', 'True');" CausesValidation=False runat=server /> (blank for all)
					</td>
					<td><asp:DropDownList width=0% id=ddlBuyer visible=true AutoPostBack=true OnSelectedIndexChanged=onSelect_Buyer runat=server /></td>
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
					<td>Product : </td>
					<td><asp:DropDownList id="ddlProduct" maxlength=8  width="50%" runat="server" /> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
				 <td width=17%>Report Criteria: </td>  
				 <td width=39%>
				 <asp:DropDownList width=50% id=ddlRptType runat=server>
							<asp:ListItem value="0" Selected>Listing</asp:ListItem>
						    <asp:ListItem value="1">Rekapitulasi</asp:ListItem> 
						    <asp:ListItem value="2">Rekapitulasi Komparasi (Nett)</asp:ListItem> 
						    <asp:ListItem value="3">Rekapitulasi Komparasi (Incl. PPN)</asp:ListItem> 
						    <asp:ListItem value="4">Rekapitulasi Pengiriman</asp:ListItem> 
					</asp:DropDownList></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>						
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="3"><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview"
							onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
                            ID="Issue6" runat="server" class="button-small" Text="Print Preview" 
                            Height="26px" Visible="False" />
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
