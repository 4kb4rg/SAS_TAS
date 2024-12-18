<%@ Page Language="vb" codefile="../../include/reports/AR_StdRpt_MutasiAccountReceivable.aspx.vb" Inherits="AR_StdRpt_MutasiAccountReceivable" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<HTML>
	<HEAD>
		<title>Account Receivable - Mutasi Account Receivable</title>
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
            
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>

   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

		    <table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> ACCOUNT RECEIVABLE - MUTASI ACCOUNT RECEIVABLE </strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
			
				<tr>
					<td width=17%>Customer : </td>  
					<td width=39%>
                        <telerik:RadComboBox   CssClass="fontObject" ID="RadCmbCustCode"  HighlightTemplatedItems="true" 
                            Runat="server" AllowCustomText="True"   
                            EmptyMessage="Please Select Customer Code" Height="100" Width="100%" 
                            ExpandDelay="50" Filter="Contains" Sort="Ascending" 
                            EnableVirtualScrolling="True" AutoPostBack="True">
                            <CollapseAnimation Type="InQuart" />
                        </telerik:RadComboBox>
					</td>
				</tr>
				<tr runat=server visible=false>
					<td>Supplier Type :</td>
					<td><asp:DropDownList width=50% id=ddlSplType runat=server>
					    <asp:ListItem value="0" Selected >-All-</asp:ListItem>
					    <asp:ListItem value="1">Supplier PO</asp:ListItem>
					    <asp:ListItem value="2">Supplier FFB</asp:ListItem>

					    <asp:ListItem value="3">Other</asp:ListItem>
					</asp:DropDownList></td>
				</tr>
				<tr runat=server>
					<td>Report Type :</td>
					<td><asp:DropDownList width=50% id=ddlRptType runat=server>
					<asp:ListItem value="0" Selected >Summary</asp:ListItem>
					<asp:ListItem value="1">Detail</asp:ListItem>
					</asp:DropDownList></td>
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
					<td colspan=4 align=left><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;</td>					
				</tr>				
			</table>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</HTML>
