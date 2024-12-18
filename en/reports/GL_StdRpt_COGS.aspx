<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_COGS.aspx.vb" Inherits="GL_StdRpt_COGS" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Cost Of Good Sold (COGS)</title>
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
					<td class="font9Tahoma" colspan="3"><strong>GENERAL LEDGER - COST OF GOOD SOLD (COGS)</strong> </td>
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
					<td width=40%>Product Sales Average Price CPO :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					Rp.</td>
					<td width=20%><asp:TextBox id="txtSrchPSAVCPO" value= "0" runat="server"/></td>				
					<td width=20%>&nbsp;</td>					
					<td width=20%>&nbsp;</td>
				</tr>
				<tr>
					<td width=40%>Product Sales Average Price IKS :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					Rp.</td>
					<td width=20%><asp:TextBox id="txtSrchPSAVIKS" value= "0" runat="server"/></td>				
					<td width=20%>&nbsp;</td>					
					<td width=20%>&nbsp;</td>
				</tr>
				<tr>
					<td width=40%>Monthly Product Yield CPO :</td>
					<td width=20%><asp:TextBox id="txtSrchMPYCPO" value= "0" runat="server"/>Kg.</td>				
					<td width=20%>&nbsp;</td>					
					<td width=20%>&nbsp;</td>
				</tr>
				<tr>
					<td width=40%>Monthly Product Yield IKS :</td>
					<td width=20%><asp:TextBox id="txtSrchMPYIKS" value= "0" runat="server"/>Kg.</td>				
					<td width=20%>&nbsp;</td>					
					<td width=20%>&nbsp;</td>
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
					<td width=22%>Accounting Period From : </td>  
					<td width=30%>
						<asp:DropDownList id="ddlSrchAccMonthFrom" size=1 width=20% runat=server />
						<asp:DropDownList id="ddlSrchAccYearFrom" size=1 width=30% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server />
					</td>
					<td width=4%>To : </td>
					<td width=44%>
						<asp:DropDownList id="ddlSrchAccMonthTo" size=1 width=20% runat=server />
						<asp:DropDownList id="ddlSrchAccYearTo" size=1 width=30% autopostback=true onselectedindexchanged=OnIndexChage_ToAccPeriod runat=server />
					</td>
				</tr>
				<tr>
					<td width=22%>Suppress zero balance : </td>
					<td width=30%>
						<asp:RadioButton id="rbSuppYes" text="Yes" GroupName="rbSupp" runat="server" />		
						<asp:RadioButton id="rbSuppNo" text="No" checked="true" GroupName="rbSupp" runat="server" />
					</td>
					<td width=4%></td>
					<td width=44%></td>
				</tr>
				
				<tr>
					<td colspan=4>&nbsp;</td>
				</tr>				
				<tr>
					<td colspan=4 align=left><asp:ImageButton id="PrintPrev" ImageUrl="../Images/butt_print_preview.gif" AlternateText="Print Preview" onClick="btnPrintPrev_Click" runat="server" />&nbsp;<asp:Button 
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
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
	</body>
</HTML>
