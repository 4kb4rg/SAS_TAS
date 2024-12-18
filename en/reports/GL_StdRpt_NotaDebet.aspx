<%@ Page Language="vb" src="../../include/reports/GL_StdRpt_NotaDebet.aspx.vb" Inherits="GL_StdRpt_NotaDebet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>General Ledger - Trial Balance Temporary Report</title>
                <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<input type=Hidden id=hidUserLocPX runat="server" NAME="hidUserLocPX"/>
			<input type=Hidden id=hidAccMonthPX runat="server" NAME="hidAccMonthPX"/>
			<input type=Hidden id=hidAccYearPX runat="server" NAME="hidAccYearPX"/>
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>GENERAL LEDGER - NOTA DEBET REPORT</strong> </td>
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
			<table width="100%" border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
			    <tr>
					<td width="15%">No. : </td>
					<td colspan="4"><asp:textbox id="txtNo" width="50%" runat="server" /></td>      
					<td width="5%">&nbsp;</td>
				</tr>
				 <tr>
					<td width="15%">Print Date : </td>
					<td colspan="4"><asp:TextBox id=txtDateCreated width=30% maxlength="10" runat="server"/>
					    <a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" runat="server" ImageUrl="../Images/calendar.gif"/></a>
				        <asp:RequiredFieldValidator	id="rfvDateCreated" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
				        <asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
				        <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
			        </td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td width="15%">To : </td>
					<td colspan="4"><asp:textbox id="txtTo" width="50%" runat="server" /></td>      
					<td width="5%">&nbsp;</td>
				</tr>
				 <tr>
					<td width="15%">Address : </td>
					<td colspan="4"><textarea rows=6 id=txtAlamat cols=50 runat=server></textarea></td>      
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td width="15%"><asp:label id="lblChartofAccCode" runat="server" /> : </td>
					<td colspan="4"> <GG:AutoCompleteDropDownList id="lstAccCode" Width="50%"  runat="server" /> 	
							              <input type="button" value=" ... " id="Find" onclick="javascript:PopCOA('frmMain', '', 'lstAccCode', 'False');" CausesValidation=False runat=server />  
									   	  <asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
							</td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td width=15%>Charging Between : </td>
					<td colspan=4><asp:RadioButton id="rbComp" text=" Company" checked="true" GroupName="rbCharge" runat="server" />
						<asp:RadioButton id="rbLoc" text=" Location" GroupName="rbCharge" runat="server" />
					</td>			
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td width=15%>Print From : </td>
					<td colspan=4><asp:RadioButton id="rbTemporary" text=" Temporary" checked="true" GroupName="rbPrint" runat="server" />
						<asp:RadioButton id="rbActual" text=" Actual" GroupName="rbPrint" runat="server" />
					</td>			
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td width="15%">Diketahui oleh : </td>
					<td colspan="4"><asp:textbox id="txtAssign" width="30%" runat="server" /> Jabatan 
                        <asp:DropDownList ID="ddlJbtn1" runat="server" Width="20%">
                            <asp:ListItem>VPAT</asp:ListItem>
                            <asp:ListItem>Manager</asp:ListItem>
                        </asp:DropDownList></td>      
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td width="15%">Diperiksa Oleh : </td>
					<td colspan="4"><asp:textbox id="txtCheck" width="30%" runat="server" /> Jabatan 
                        <asp:DropDownList ID="ddlJbtn2" width="20%" runat="server">
                            <asp:ListItem>Spv. Accounting</asp:ListItem>
                            <asp:ListItem>KTU</asp:ListItem>
                        </asp:DropDownList></td>      
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
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
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
	</body>
</HTML>
