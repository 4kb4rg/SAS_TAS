<%@ Page Language="vb" Inherits="CM_StdRpt_PrintDORegDoc" src="../../include/reports/CM_StdRpt_PrintDORegDoc.aspx.vb" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Contract Management - Print DO Registration Document</title> 
            <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>CONTRACT MANAGEMENT - PRINT DO REGISTRATION DOCUMENT</strong> </td>
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
			<table width="100%" border="0" cellspacing="1" cellpadding="1" ID="Table1">
				
				<tr>
					<td width=17%>DO No : </td>
					<td width=39%>
						<asp:DropDownList id=ddlDONo1 maxlength=20 width=100% autopostback=true onselectedindexchanged=onChanged_DONo runat=server/> </asp:DropDownList>						
						<asp:Label id="lblErrDONo1" forecolor="red" visible="false" text="Please Select DO No" runat="server" />	
					<!-- Remark BY ALIM as per 23 Nov 2006 - UAT Ph2 BJM
					<td width=4%>To : </td>	
					<td width=40%><asp:DropDownList id=ddlDONo2 maxlength=20 width=70% runat=server/> (blank for all)</td>
					-->
				</tr>
				
				<tr>
					<td>Representative : &nbsp;</td>
					<td>
						<asp:TextBox id="txtTTD" runat="server"></asp:TextBox>&nbsp;
						<asp:RequiredFieldValidator id="rfvTTD" runat="server" ErrorMessage="Field cannot be blank." Display="Dynamic"
							EnableViewState="False" ControlToValidate="txtTTD"></asp:RequiredFieldValidator></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Jabatan : &nbsp;</td>
					<td>
						<asp:TextBox id="txtJabatan" runat="server"></asp:TextBox>&nbsp;
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td>Packing :</td>
				    <td><asp:TextBox id="txtPacking" width=100% runat="server"></asp:TextBox></td>
				    <td>&nbsp;</td>
				</tr>
				<tr>
				    <td>Penyerahan :</td>
				    <td><asp:TextBox id="txtPenyerahan" width=100% runat="server"></asp:TextBox></td>
				    <td>&nbsp;</td>
				</tr>
				<tr>
				    <td>PPN :</td>
				    <td><asp:CheckBox id="ChkPPN" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=PPN_Type runat=server /></td>
				    <td>&nbsp;</td>				    
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="3"><asp:ImageButton id="PrintPrev" ImageUrl="../images/butt_print_preview.gif" AlternateText="Print Preview"
							onClick="btnPrintPrev_Click" runat="server" /></td>
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
	</body>
</HTML>
