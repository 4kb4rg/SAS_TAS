<%@ Page Language="vb" codefile="../../../include/CM_Trx_ContractRegDet.aspx.vb" Inherits="CM_Trx_ContractRegDet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
 
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Contract Registration Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		
<script src="jquery-3.5.1.js"></script>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<input type="hidden" id="hidPPN" runat="server">
			<input type="hidden" id="hidTermOfWeighing" runat="server">
			<input type="hidden" id="hidQualityCPO" runat="server">
			<input type="hidden" id="hidClaimCPO" runat="server">
			<input type="hidden" id="hidQualityPK" runat="server">
			<input type="hidden" id="hidClaimPK" runat="server">
			<input type="hidden" id="hidTermOfPayment" runat="server">
			<input type="hidden" id="hidConsignment" runat="server">
			
			<table border=0 cellspacing=0 cellpadding=2 width=100%>
				<tr>
					<td colspan="6"><UserControl:menu_CM_Trx id=menu_CM_Trx runat="server" /></td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
				<table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">

				<tr>
					<td class="font9Tahoma" colspan="6"><strong> CONTRACT REGISTRATION DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td width=20% height=25>Contract No :*</td>
					<td width=30%>
						<asp:textbox id=txtContNo maxlength=128 width=100% Enabled=false CssClass="fontObject" runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Type : </td>
					<td><asp:RadioButtonList id=rdContractType repeatdirection=horizontal onSelectedIndexChanged=onChange_ContractType autopostback=true CssClass="font9Tahoma" runat=server>
							<asp:ListItem id=item1 value=1 text=Purchase runat=server />
							<asp:ListItem id=item2 Value=2 text=Sales runat=server />
						</asp:RadioButtonList>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Contract Type : </td>
					<td><asp:RadioButtonList id=rdContType repeatdirection=horizontal onSelectedIndexChanged=onChange_ContType autopostback=true CssClass="font9Tahoma" runat=server>
							<asp:ListItem id=item3 value=1 text=Lokal runat=server />
							<asp:ListItem id=item4 Value=2 text=Export runat=server />
						</asp:RadioButtonList>
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>					
				</tr>

				<tr>
					<td height=25>Contract Date :*</td>
					<td><asp:Textbox id=txtContractDate maxlength=128 width=25% CssClass="fontObject"  runat=server/>
						<a href="javascript:PopCal('txtContractDate');">
						<asp:Image id="btnContractDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator 
							id=rfvContractDate
							display=dynamic 
							runat=server
							ControlToValidate=txtContractDate
							text="<br>Please enter Contract Date." />
						<asp:Label id=lblContractDate text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblContractDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>					
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Contract Category :*</td>
					<td><asp:dropdownlist id=ddlContCategory width=100% CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvContCategory
							display=dynamic 
							runat=server
							ControlToValidate=ddlContCategory
							text="<br>Please select Contract Category." />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr id=trBuyer runat=server>
					<td><asp:label id=lblBillParty runat=server /> :*</td>
					<td>  <telerik:RadComboBox   CssClass="fontObject" ID="ddlBuyer"    AutoPostBack="true"  
												OnSelectedIndexChanged=onChanged_BillParty
                                                Runat="server" AllowCustomText="True" 
                                                EmptyMessage="Please Select Customer" Height="200" Width="100%" 
                                                ExpandDelay="50" Filter="Contains" Sort="Ascending" 
                                                EnableVirtualScrolling="True" auto> 
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox>
						<asp:RequiredFieldValidator 
							id=rfvBuyer
							display=dynamic 
							runat=server
							ControlToValidate=ddlBuyer/>


					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25><asp:label id=lblBillPartyNo runat=server /> Contract No. <i>(optional)</i> :</td>
				    <td><asp:Textbox id=txtBuyerNo maxlength=128 width=100% CssClass="fontObject" runat=server/></td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
					<td class="mt-h">Product Information</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Product :*</td>
					<td><asp:dropdownlist id=ddlProduct AutoPostBack=true onselectedindexchanged=onChanged_Product width=50% CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvProduct
							display=dynamic 
							runat=server
							ControlToValidate=ddlProduct
							text="<br>Please select Product." />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25" valign=top>Contract Quality :*</td>
					<td><textarea rows=5 id=taProductQuality cols=50 class="fontObject" runat=server></textarea></td>					
					<td>&nbsp;</td>
					<td height="25" valign=top>Claim Quality :</td>
					<td><textarea rows=5 id=taProductClaim cols=50 class="fontObject"  runat=server></textarea></td>
				</tr>
								
				<tr>
					<td height=25>Contract Quantity(Kg/Trip) : </td>

					<td>						
                                <telerik:RadNumericTextBox ID="txtContractQty" CssClass="font9Tahoma"  Width="50%" AutoPostBack="true" ontextchanged="chkPPN_Change"   Runat="server" LabelWidth="64px">     <NumberFormat ZeroPattern="n"></NumberFormat>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
								<asp:label id=lblErrContractQty visible=false forecolor=red text="<br>Total of Contract Quantity and Extra Quantity should be equal or bigger than Matched Quantity." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Matched Quantity : </td>
					<td><asp:label id=lblMatchedQty maxlength=128 width=50% runat=server/></td>
				</tr>
				<tr>
					<td height=25>Tolerance Quantity : </td>
					<td><asp:Textbox id=txtExtraQty maxlength=128 width=50% style="text-align:right" CssClass="fontObject" runat=server/>&nbsp;&nbsp;&nbsp;
						<asp:dropdownlist id=ddlExtraQtyType width=15% runat=server/>
						<asp:RegularExpressionValidator id="revExtraQty" 
							ControlToValidate="txtExtraQty"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvExtraQty"
							ControlToValidate="txtExtraQty"
							MinimumValue="0"
							MaximumValue="9999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Balance Quantity : </td>
					<td><asp:label id=lblBalQty maxlength=128 width=50% CssClass="fontObject" runat=server/></td>
				</tr>
				<tr>
					<td height=25>Price :*</td>
					<td><asp:dropdownlist id=ddlCurrency width=15% CssClass="fontObject" runat=server />
                                <telerik:RadNumericTextBox ID="txtPrice"  CssClass="font9Tahoma" Width="35%" ontextchanged="chkPPN_Change"  autopostback="TRUE"  Runat="server" LabelWidth="64px">     <NumberFormat ZeroPattern="n"></NumberFormat>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>

				
				
				<tr>
    				<td>PPN :	</td>
    				<td><asp:CheckBox id="chkPPN" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=chkPPN_Change CssClass="fontObject"  runat="server"/></td>
				</tr>
				<tr>
					<td height=25>Total Amount :*</td>
					<td>
                                <telerik:RadNumericTextBox ID="txttotalamount" width=50%  CssClass="font9Tahoma"   ReadOnly="true"   Runat="server" LabelWidth="64px">    
									<NumberFormat ZeroPattern="n"></NumberFormat>
                                <EnabledStyle HorizontalAlign="Right" />
                                </telerik:RadNumericTextBox>
						
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25></td>
					<td><asp:dropdownlist id=ddlCurrencyOA width=15% runat=server CssClass="fontObject" Visible="False" />
						<asp:Textbox id=txtPriceOA maxlength=128 width=34% Text=0 style="text-align:right" CssClass="fontObject" runat=server Visible="False"/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator1
							display=dynamic 
							runat=server
							ControlToValidate=txtPriceOA
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator1" 
							ControlToValidate="txtPriceOA"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator1"
							ControlToValidate="txtPriceOA"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblPriceBasis runat=server/></td>
					<td><asp:dropdownlist id=ddlPriceBasis width=50% CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvPriceBasis
							display=dynamic 
							runat=server
							ControlToValidate=ddlPriceBasis
							text="<br>Please select Price Basis Code." />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
					<td class="mt-h">Delivery Information</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Penyerahan :*</td>
					<td>
                        <%--<asp:dropdownlist id=ddlTerm width=100% AutoPostBack=false onselectedindexchanged=onChanged_TermOfDelivery CssClass="fontObject" runat=server/>--%>

                                 <asp:DropDownList id="ddlTerm" width=100%    runat=server>
										<asp:ListItem value="0" Selected>Pilih Penyerahan</asp:ListItem>
                                        <asp:ListItem value="1">FRANCO</asp:ListItem>
										<asp:ListItem value="2">LOCO</asp:ListItem>
										<asp:ListItem value="3">CIF Pelabuhan pembeli </asp:ListItem>										
										<asp:ListItem value="4">FOB Pangkal Balam</asp:ListItem>
										 
									</asp:DropDownList>

						<asp:RequiredFieldValidator 
							id=rfvTerm
							display=dynamic 
							runat=server
							ControlToValidate=ddlTerm
							text="<br>Please select Term Of Delivery." />
					</td>
					<td>&nbsp;</td>
				</tr>	
				
				<tr>
					<td height=25 valign=top>Loading Destination :*</td>
					<td><textarea rows=3 id=taConsignment cols=50 class="fontObject" runat=server></textarea></td>		
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25 valign=top>Time Of Delivery :*</td>
					<td><textarea rows=3 id=taDeliveryTime cols=50 class="fontObject" runat=server></textarea></td>		
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25 valign=top>Term Of Weighing :*</td>
					<td><textarea rows=3 id=taTermOfWeighing cols=50 class="fontObject" runat=server></textarea></td>		
					<td>&nbsp;</td>
				</tr>	
					
				<tr>
					<td height=25>Delivery Month :* </td>
					<td><asp:Textbox id=txtDelivMonth maxlength=128 width=25% CssClass="fontObject" runat=server/>
						<a href="javascript:PopCal('txtDelivMonth');">
						<asp:Image id="btnDelivMonth" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator 
							id=rfvDelivMonth
							display=dynamic 
							runat=server
							ControlToValidate=txtDelivMonth
							text="<br>Please enter Delivery Date." />
						<asp:Label id=lblDelivMonth text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblDelivMonthFmt display=dynamic forecolor=red visible=false runat="server"/>
					</td>
					<td>&nbsp;</td>
				</tr>
                <tr>
					<td height=25 valign=top>Remarks : </td>
					<td><textarea rows=6 id=taRemarks cols=50 class="fontObject" runat=server></textarea></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				
                <tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
                <tr>
					<td class="mt-h">Payment Information</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
                <tr id=trSeller runat=server>
					<td height=25>Seller :*</td>
					<td><asp:dropdownlist id=ddlSeller width=100% CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvSeller
							display=dynamic 
							runat=server
							ControlToValidate=ddlSeller
							text="Please select Seller." />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height="25"><asp:label id="lblBankCode1" runat="server"></asp:label></td>
					<td><asp:DropDownList id="ddlBankCode1" OnSelectedIndexChanged="onSelect_Bank" AutoPostBack=true CssClass="fontObject" runat="server" Width="100%"></asp:DropDownList></td>
					<td>&nbsp;</td>
					<td height="25"><asp:label id="lblBankAccNo1" runat="server"></asp:label></td>
					<td><asp:TextBox id="txtBankAccNo1" ReadOnly=true CssClass="fontObject" runat="server" MaxLength="20"></asp:TextBox></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Term of payment : </td>
					<td><textarea rows=3 id=taTermOfPayment cols=50 class="fontObject" runat=server></textarea></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
					<td height=25 valign=top>Seller Sign :*</td>
					<td><asp:Textbox id=txtUnderName Text="" maxlength=128 width=100% CssClass="fontObject" runat=server/></td>	
					<td>&nbsp;</td>
					<td height=25 valign=top>Under Seller Sign :*</td>
					<td><asp:Textbox id=txtUnderNamePosition Text="" maxlength=128 width=47% CssClass="fontObject" runat=server/></td>	
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25 valign=top>Buyer Sign :*</td>
					<td><asp:Textbox id=txtUnderPost text="" maxlength=128 width=100% CssClass="fontObject" runat=server/></td>		
					<td>&nbsp;</td>
					<td height=25 valign=top>Under Buyer Sign :*</td>
					<td><asp:Textbox id=txtUnderPostPosition text="" maxlength=128 width=47% CssClass="fontObject" runat=server/></td>		
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
                    <td colspan="3">
                    <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
                </tr>
                <tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=NewTBBtn OnClick="NewTBBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Contract Registration" runat="server"/>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=CloseBtn AlternateText=" Close "  imageurl="../../images/butt_Close.gif" onclick=Button_Click CommandArgument=Close runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=PrintBtn runat="server" ImageUrl="../../images/butt_print.gif" AlternateText="Print" CausesValidation="False" onclick="PrintBtn_Click"></asp:ImageButton>	
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				
				
				<tr>
					<td height="25"><asp:label id="lblBankCode2" visible=false runat="server"></asp:label></td>
					<td><asp:DropDownList id="ddlBankCode2" visible=false CssClass="fontObject" runat="server" Width="100%"></asp:DropDownList></td>
					<td>&nbsp;</td>
					<td height="25"><asp:label id="lblBankAccNo2" visible=false runat="server"></asp:label></td>
					<td>
						<asp:TextBox id="txtBankAccNo2" visible=false CssClass="fontObject" runat="server" MaxLength="20"></asp:TextBox></td>
					<td>&nbsp;</td>
				</tr>		
				<tr>
					<td height=25><asp:label id=lblAccount visible=false runat=server /> </td>
					<td><asp:dropdownlist id=ddlAccount visible=false width=100% autopostback=true onselectedindexchanged=onChanged_AccCode CssClass="fontObject" runat=server />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblBlock visible=false runat=server /> </td>
					<td><asp:dropdownlist id=ddlBlock visible=false width=100% CssClass="fontObject" runat=server />
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				
				<tr>
					<td width=30%>
						<asp:label id=lbltxtContractQty maxlength=128 visible=false width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td width=30%>
						<asp:label id=lbltxtExtraQty maxlength=128 visible=false width=100% runat=server/>
					</td>
				</tr>
				
				
				<Input Type=Hidden id=tbcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblActiveMatchExist visible=false text=0 runat=server />
                </table>
                </div>
            </td>
            </tr>
			</table>
						  <asp:TextBox ID="txtPPN" ReadOnly=true runat="server" Width="9%" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
            <asp:TextBox ID="txtCreditTerm" ReadOnly=true runat="server" Width="9%" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
            <asp:TextBox ID="txtPPNInit" ReadOnly=true runat="server" Width="9%" BackColor="Transparent" BorderStyle="None"></asp:TextBox>&nbsp;
            <asp:ScriptManager ID="ScriptManager1" runat="server">    </asp:ScriptManager>
		</form>
	</body>
</html>
