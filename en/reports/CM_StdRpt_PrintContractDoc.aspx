<%@ Page Language="vb" Inherits="CM_StdRpt_PrintContractDoc" src="../../include/reports/CM_StdRpt_PrintContractDoc.aspx.vb" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Contract Management - Print Contract Document</title> 
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
					<td class="font9Tahoma" colspan="3"><strong>CONTRACT MANAGEMENT - PRINT CONTRACT DOCUMENT</strong> </td>
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
					<td colspan="6">
						<asp:Label id="lblDate" forecolor="red" visible="false" text="Incorrect Date Format. Date Format is "
							runat="server" />
						<asp:Label id="lblDateFormat" forecolor="red" visible="false" runat="server" />
					</td>
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
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1" class="font9Tahoma">
				<tr>
					<td width=17%>Contract No :
					</td>
					<td width=39%><asp:DropDownList id="ddlContractNo" width=100% runat="server" />
					
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Product :
					</td>
					<td><asp:DropDownList id="ddlProduct" maxlength="8" runat="server" />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id="lblBillParty" runat="server" />
						Code :
					</td>
					<td><asp:textbox id="txtBillParty" runat="server" maxlength="8" />
						(blank for all)</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Buyer Representative&nbsp;:</td>
					<td>
						<asp:TextBox id="txtBuyer" runat="server"></asp:TextBox>&nbsp;
						<asp:RequiredFieldValidator id="rfvBuyer" runat="server" ErrorMessage="Field cannot be blank." Display="Dynamic"
							EnableViewState="False" ControlToValidate="txtBuyer"></asp:RequiredFieldValidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr id=Rep1 runat="server">
					<td>Seller Representative&nbsp;:</td>
					<td>
						<asp:TextBox id="txtOwner1" runat="server"></asp:TextBox>&nbsp;
						<asp:RequiredFieldValidator id="rfvOwner1" runat="server" ErrorMessage="Field cannot be blank." Display="Dynamic"
							EnableViewState="False" ControlToValidate="txtOwner1"></asp:RequiredFieldValidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr id=Rep2 runat="server">
					<td>Representative #2&nbsp;:</td>
					<td>
						<asp:TextBox id="txtOwner2" text="sysadm" runat="server"></asp:TextBox>&nbsp;
						<asp:RequiredFieldValidator id="rfvOwner2" runat="server" ErrorMessage="Field cannot be blank." Display="Dynamic"
							EnableViewState="False" ControlToValidate="txtOwner2"></asp:RequiredFieldValidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td>PPN :</td>
				    <td><asp:CheckBox id="ChkPPN" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=PPN_Type runat=server /></td>
				    <td>&nbsp;</td>				    
				</tr>
				<tr>
				    <td>PPN included :</td>
				    <td><asp:CheckBox id="ChkInclude" Text="  No" checked=false enabled=false AutoPostBack=true OnCheckedChanged=Include_Type runat=server /></td>
				    <td>&nbsp;</td>				    
				</tr>
				<tr>
				    <td>Pengiriman :</td>
				    <td><asp:TextBox id="txtPengiriman" width=100% runat="server"></asp:TextBox></td>
				    <td>&nbsp;</td>
				</tr>
				<tr>
				    <td>Asal Barang :</td>
				    <td><asp:TextBox id="txtAsalBarang" width=100% runat="server"></asp:TextBox></td>
				    <td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
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
		<asp:Label id="LocTag" visible="false" runat="server" />
		<asp:Label id="lblAccount" visible="false" runat="server" />
		<asp:Label id="lblBlock" visible="false" runat="server" />
		<asp:Label id="lblErrContract" visible="false" text="Select one contract number." runat="server" />
	</body>
</HTML>
