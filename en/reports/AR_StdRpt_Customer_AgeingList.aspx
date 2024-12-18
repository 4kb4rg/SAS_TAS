<%@ Page Language="vb" codefile="../../include/reports/AR_StdRpt_Customer_AgeingList.aspx.vb" Inherits="AR_StdRpt_Customer_AgeingList" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="AR_STDRPT_SELECTION_CTRL" src="../include/reports/AR_StdRpt_Selection_Ctrl.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<HTML>
	<HEAD>
		<title>Account Payable - Mutasi Account Payable</title>
             <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id="PrefHdl" runat="server" />
	    <style type="text/css">
.RadComboBox *{margin:0;padding:0}.RadComboBox *{margin:0;padding:0}.RadComboBox *{margin:0;padding:0}
            .RadComboBox *{margin:0;padding:0}.RadComboBox_Default .rcbInputCellLeft{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbInputCellLeft{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCellLeft{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbInputCellLeft{background-color:transparent;background-repeat:no-repeat}.RadComboBox .rcbInputCellLeft{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCellLeft{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbInputCellLeft{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCellLeft{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox_Default .rcbInput{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox .rcbInput{text-align:left}.RadComboBox_Default .rcbInput{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox .rcbInput{text-align:left}.RadComboBox .rcbInput{text-align:left}.RadComboBox_Default .rcbInput{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox .rcbInput{text-align:left}.RadComboBox_Default .rcbInput{font:12px "Segoe UI",Arial,sans-serif;color:#333}.RadComboBox_Default .rcbArrowCellRight{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbArrowCellRight{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCellRight{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbArrowCellRight{background-color:transparent;background-repeat:no-repeat}.RadComboBox .rcbArrowCellRight{background-color:transparent;background-repeat:no-repeat}
            .RadComboBox_Default .rcbArrowCellRight{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}.RadComboBox .rcbArrowCellRight{background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCellRight{background-image:url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png')}</style>
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">

		    <table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1">
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>ACCOUNT RECEIVABLE -CUSTOMER AGING LIST</strong> </td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:AR_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <asp:Label ID="lblErrMessage" runat="server" Visible="False"></asp:Label>
                    </td>
				</tr>			
				<tr>
					<td colspan="6"><hr style="width :100%" /></td>
				</tr>
			</table>
			<table width=100% border="0" cellspacing="2" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
			
				<tr>
					<td width=17%>Customer&nbsp; Code : </td>  
					<td width=39%>
		
                        <telerik:RadComboBox   CssClass="fontObject" ID="RadCmbCustCode"  
                            Runat="server" AllowCustomText="True"   
                            EmptyMessage="Please Select Customer Code" Height="23px" Width="80%" 
                            ExpandDelay="50" Filter="Contains" Sort="Ascending" 
                            EnableVirtualScrolling="True" AutoPostBack="True">
                            <CollapseAnimation Type="InQuart" />
                        </telerik:RadComboBox>
						&nbsp;(blank for all)
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
                        <asp:TextBox ID="txtToAge1" runat="server" MaxLength="3" Width="20%"></asp:TextBox>
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
                        <asp:TextBox ID="txtFromAge2" runat="server" MaxLength="3" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvFromAge2" runat="server" ControlToValidate="txtFromAge2" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvFromAge2" runat="server" ControlToValidate="txtFromAge2" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrFromAge2" runat="server"
                                    ForeColor="red" Text="Invalid Range " Visible="false"></asp:Label>
                        -
                        <asp:TextBox ID="txtToAge2" runat="server" MaxLength="3" Width="20%"></asp:TextBox>
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
                        <asp:TextBox ID="txtFromAge3" runat="server" MaxLength="3" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvFromAge3" runat="server" ControlToValidate="txtFromAge3" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvFromAge3" runat="server" ControlToValidate="txtFromAge3" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrFromAge3" runat="server"
                                    ForeColor="red" Text="Invalid Range " Visible="false"></asp:Label>
                        -
                        <asp:TextBox ID="txtToAge3" runat="server" MaxLength="3" Width="20%"></asp:TextBox>&nbsp;Days<asp:RequiredFieldValidator
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
                        <asp:TextBox ID="txtFromAge4" runat="server" MaxLength="3" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvFromAge4" runat="server" ControlToValidate="txtFromAge4" Display="dynamic"
                            Text="Please enter number of days. "></asp:RequiredFieldValidator><asp:RangeValidator
                                ID="rgvFromAge4" runat="server" ControlToValidate="txtFromAge4" Display="dynamic"
                                EnableClientScript="True" MaximumValue="999" MinimumValue="2" Text="Invalid format or range. "
                                Type="integer"></asp:RangeValidator><asp:Label ID="lblErrFromAge4" runat="server"
                                    ForeColor="red" Text="Invalid Range " Visible="false"></asp:Label>
                        -
                        <asp:TextBox ID="txtToAge4" runat="server" MaxLength="3" Width="20%"></asp:TextBox>
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
                        <asp:TextBox ID="txtFromAge5" runat="server" MaxLength="3" Width="20%"></asp:TextBox><asp:RequiredFieldValidator
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
