<%@ Page Language="vb" src="../../include/reports/RPT_PremiInvoice_Listing.aspx.vb" Inherits="RPT_PremiInvoice_Listing" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="GL_STDRPT_SELECTION_CTRL" src="../include/reports/GL_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Premi/Invoice Report</title>
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
			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3">PREMI/INVOICE REPORT</td>
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
			<table width="100%" border="0" cellspacing="2" cellpadding="1" ID="Table2"  class="font9Tahoma" runat=server>	
				
				<tr>
					<td width="15%">Date From:</td>
					<td width="30%">    
						<asp:Textbox id="txtDate" width=60% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>	
					<td width="5%">&nbsp;</td>
					<td width="15%">Date To:</td>
					<td width="30%">    
						<asp:Textbox id="txtDateTo" width=60% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>		
					<td>&nbsp;</td>	
				</tr>	
				<tr width="15%">
					<td>Flight No. : </td>
					<td><asp:textbox id="txtFlightNo" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="15%">Listing Type: </td>
					    <td><asp:DropDownList ID="ddlListingType" width="70%" OnSelectedIndexChanged="ListingType_Changed" AutoPostBack=true runat="server">
                            <asp:ListItem value="1">AIRLINE</asp:ListItem>
                            <asp:ListItem value="2">PROVIDER</asp:ListItem>
                        </asp:DropDownList></td>      
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="ListingType1" visible=true runat="server">
					<td width="15%">Airline Type: </td>
					    <td><asp:DropDownList ID="ddlAirlineType" width="70%" OnSelectedIndexChanged="AirlineType_Changed" AutoPostBack=true runat="server">
                            <asp:ListItem value="X">- ALL -</asp:ListItem>
                            <asp:ListItem value="D">DOMESTIC</asp:ListItem>
                            <asp:ListItem value="I">INTERNATIONAL</asp:ListItem>
                        </asp:DropDownList></td>      
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="ListingType2" visible=true runat="server">
					<td width="15%">Airline : </td>
					    <td><GG:AutoCompleteDropDownList id="ddlAirline" Width="70%"  runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="ListingType3" visible=false runat="server">
					<td width="15%">Provider : </td>
					    <td><GG:AutoCompleteDropDownList id="ddlProvider" Width="70%"  runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
					
			    <tr>
					<td colspan="6">
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
