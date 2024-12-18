<%@ Page Language="vb" src="../../include/reports/AP_StdRpt_Supplier_AgeingList.aspx.vb" Inherits="AP_StdRpt_Supplier_AgeingList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AP_STDRPT_SELECTION_CTRL" src="../include/reports/AP_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Account Payable - Mutasi Account Payable</title>
             <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

		    <table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>ACCOUNT PAYABLE - SUPPLIER AGEING LIST</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AP_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
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
					<td width=17%>Supplier Code : </td>  
					<td width=39%>
						<asp:TextBox id="txtSupplierCode" width="50%" runat="server"/> 
						<input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'ddlSupplier', 'True');" CausesValidation=False runat=server /> (blank for all)
					</td>
					<td><asp:DropDownList width=0% id=ddlSupplier visible=true AutoPostBack=true OnSelectedIndexChanged=onSelect_Supplier runat=server /></td>
				</tr>
				<tr>
					<td width=17% style="height: 24px">Accounting Period From : </td>  
					<td width=39% style="height: 24px">
						<asp:DropDownList id="ddlSrchAccMonthFrom" size=1 width=20% runat=server />
						<asp:DropDownList id="ddlSrchAccYearFrom" size=1 width=30% autopostback=true onselectedindexchanged=OnIndexChage_FromAccPeriod runat=server />
					</td>
					<td width=4% style="height: 24px"> </td>
					<td width=40% style="height: 24px">
                        &nbsp;
					</td>
				</tr>			
                <tr>
                    <td style="height: 24px" width="17%">
                        Ageing Category 1 :
                    </td>
                    <td style="height: 24px" width="39%">
                        <asp:TextBox ID="txtFromAge1" runat="server" ReadOnly="" Text="1" Width="20%"></asp:TextBox>-
                        <asp:TextBox ID="txtToAge1" runat="server" MaxLength="3" Text="30" Width="20%"></asp:TextBox>
                        Days
                        <asp:RequiredFieldValidator ID="rfvToAge1" runat="server" ControlToValidate="txtToAge1"
                            Display="dynamic" Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvToAge1" runat="server" ControlToValidate="txtToAge1" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrToAge1" runat="server" ForeColor="red"
                                    Text="Invalid Range " Visible="false"></asp:Label></td>
                    <td style="height: 24px" width="4%">
                    </td>
                    <td style="height: 24px" width="40%">
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px" width="17%">
                        Ageing Category 2 :
                    </td>
                    <td style="height: 24px" width="39%">
                        <asp:TextBox ID="txtFromAge2" runat="server" MaxLength="3" Text="31" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvFromAge2" runat="server" ControlToValidate="txtFromAge2" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvFromAge2" runat="server" ControlToValidate="txtFromAge2" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrFromAge2" runat="server"
                                    ForeColor="red" Text="Invalid Range " Visible="false"></asp:Label>
                        -
                        <asp:TextBox ID="txtToAge2" runat="server" MaxLength="3" Text="60" Width="20%"></asp:TextBox>
                        Days<asp:RequiredFieldValidator ID="rfvToAge2" runat="server" ControlToValidate="txtToAge2"
                            Display="dynamic" Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvToAge2" runat="server" ControlToValidate="txtToAge2" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrToAge2" runat="server" ForeColor="red"
                                    Text="Invalid Range " Visible="false"></asp:Label></td>
                    <td style="height: 24px" width="4%">
                    </td>
                    <td style="height: 24px" width="40%">
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px" width="17%">
                        Ageing Category 3 :
                    </td>
                    <td style="height: 24px" width="39%">
                        <asp:TextBox ID="txtFromAge3" runat="server" MaxLength="3" Text="61" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvFromAge3" runat="server" ControlToValidate="txtFromAge3" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvFromAge3" runat="server" ControlToValidate="txtFromAge3" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrFromAge3" runat="server"
                                    ForeColor="red" Text="Invalid Range " Visible="false"></asp:Label>
                        -
                        <asp:TextBox ID="txtToAge3" runat="server" MaxLength="3" Text="90" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvToAge3" runat="server" ControlToValidate="txtToAge3" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvToAge3" runat="server" ControlToValidate="txtToAge3" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrToAge3" runat="server" ForeColor="red"
                                    Text="Invalid Range " Visible="false"></asp:Label></td>
                    <td style="height: 24px" width="4%">
                    </td>
                    <td style="height: 24px" width="40%">
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px" width="17%">
                        Ageing Category 4 :
                    </td>
                    <td style="height: 24px" width="39%">
                        <asp:TextBox ID="txtFromAge4" runat="server" MaxLength="3" Text="91" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvFromAge4" runat="server" ControlToValidate="txtFromAge4" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvFromAge4" runat="server" ControlToValidate="txtFromAge4" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrFromAge4" runat="server"
                                    ForeColor="red" Text="Invalid Range " Visible="false"></asp:Label>
                        -
                        <asp:TextBox ID="txtToAge4" runat="server" MaxLength="3" Text="120" Width="20%"></asp:TextBox>
                        Days<asp:RequiredFieldValidator ID="rfvToAge4" runat="server" ControlToValidate="txtToAge4"
                            Display="dynamic" Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvToAge4" runat="server" ControlToValidate="txtToAge4" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrToAge4" runat="server" ForeColor="red"
                                    Text="Invalid Range " Visible="false"></asp:Label></td>
                    <td style="height: 24px" width="4%">
                    </td>
                    <td style="height: 24px" width="40%">
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px" width="17%">
                        Ageing Category 5 :
                    </td>
                    <td style="height: 24px" width="39%">
                        <asp:TextBox ID="txtFromAge5" runat="server" MaxLength="3" Text="121" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvFromAge5" runat="server" ControlToValidate="txtFromAge5" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvFromAge5" runat="server" ControlToValidate="txtFromAge5" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrFromAge5" runat="server"
                                    ForeColor="red" Text="Invalid Range " Visible="false"></asp:Label>
                        Days and above
                    </td>
                    <td style="height: 24px" width="4%">
                    </td>
                    <td style="height: 24px" width="40%">
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px" width="17%">
                    </td>
                    <td style="height: 24px" width="39%">
                    </td>
                    <td style="height: 24px" width="4%">
                    </td>
                    <td style="height: 24px" width="40%">
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
	</body>
</HTML>
