<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_FS.aspx.vb" Inherits="GL_StdRpt_FS" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_STDRPT_SELECTION_CTRL.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Financial Statement Reports</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
                 <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu"  ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> GENERAL LEDGER - FINANCIAL STATEMENT REPORTS</strong></td>
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
			
				<tr id="TrDisplay" visible=false>
					<td width=25%>Display Type : </td>
						
					<td colspan="2">
					     <asp:DropDownList id="lstDisplayType" width=30% runat=server>
										<asp:ListItem value="0" Selected>1 Kolom</asp:ListItem>
										<asp:ListItem value="1">2 Kolom</asp:ListItem>
										<asp:ListItem value="2">3 Kolom</asp:ListItem>										
							</asp:DropDownList>
					  </td>
				</tr>
				<tr>
					<td>Report Name :</td>
					<td colspan="2">
					   <asp:DropDownList id="ddlReport"  width="75%" runat="server" > 
					   </asp:DropDownList>					
					</td>
				</tr>
				<tr>
					<td width=25%>Location Option : </td>
					<td colspan="2"><asp:DropDownList id=ddlLocCode width=75% runat=server />
					    </asp:DropDownList>
				    </td>
				</tr>		
				<tr>
					<td width=25%>Report Option : </td>
					<td colspan="2">
					    <asp:DropDownList id="ddlOption" width=75% runat=server>
						<asp:ListItem value="1" Selected>Yearly</asp:ListItem>
						<asp:ListItem value="2">Monthly</asp:ListItem>										
						</asp:DropDownList>
					</td>
				</tr>	
				<tr><td>&nbsp;&nbsp;&nbsp;</td>
					<td colspan="3">
                    <asp:CheckBox id="chkAudited" text=" Audited" checked="false" runat="server" /></td>
				</tr>
				<tr><td>&nbsp;&nbsp;&nbsp;</td>
					<td colspan="3">
                    <asp:CheckBox id="chkDetail" text=" Detail" checked="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
				</tr>
				
				
				<tr>
					<td></td>
					<td colspan="3">
					   <asp:label id="lblErrRpt" visible="false" text="Please Select Report" forecolor="red" runat="server" />				
					</td>	
				</tr>
				
				<tr>
					<td colspan="3">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
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
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
	</body>
</HTML>
