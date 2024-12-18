<%@ Page Language="vb" Inherits="CM_StdRpt_ContractBook" src="../../include/reports/CM_StdRpt_ContractBook.aspx.vb" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CM_STDRPT_SELECTION_CTRL" src="../include/reports/CM_StdRpt_Selection_Ctrl.ascx"%>
<html>
	<head>
		<title>Contract Management - Print Buku Kontrak</title> 
         <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</head>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" id="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> CONTRACT MANAGEMENT - PRINT BUKU KONTRAK</strong></td>
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
					<td width="30%" >No Kontrak : &nbsp;</td>
					<td width="60%" >
						<asp:TextBox id="txtContractNo" width="70%"  runat="server"></asp:TextBox>
					</td>
					<td width="10%">&nbsp;</td>
				</tr>
				<tr>
					<td>Atau</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td>No DO :</td>
				    <td><asp:TextBox id="txtDONo" width="70%" runat="server"></asp:TextBox></td>
				    <td>&nbsp;</td>
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
