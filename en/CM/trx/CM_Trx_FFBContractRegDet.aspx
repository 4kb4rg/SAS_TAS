<%@ Page Language="vb" src="../../../include/CM_Trx_FFBContractRegDet.aspx.vb" Inherits="CM_Trx_FFBContractRegDet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FFB Contract Registration Details</title>
              <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
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
					<td class="font9Tahoma" colspan="6"><strong>FFB CONTRACT REGISTRATION DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td width=20% height=25>Contract No :*</td>
					<td width=30%>
						<asp:textbox id=txtContNo maxlength=128 width=100% Enabled=false runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Contract Date :*</td>
					<td><asp:Textbox id=txtContractDate maxlength=128 width=25% runat=server/>
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
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Contract Period :* </td>
				    <td><asp:Textbox id=txtPeriod1 maxlength=128 width=25% runat=server/>
				        <a href="javascript:PopCal('txtPeriod1');">
						<asp:Image id="btnPeriod1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator2
							display=dynamic 
							runat=server
							ControlToValidate=txtContractDate
							text="<br>Please enter Contract Date." />
						<asp:Label id=Label1 text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=Label2 display=dynamic forecolor=red visible=false runat="server"/> 
					&nbsp;To&nbsp;
					    <asp:Textbox id=txtPeriod2 maxlength=128 width=25% runat=server/>
				        <a href="javascript:PopCal('txtPeriod2');">
						<asp:Image id="btnPeriod2" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator3
							display=dynamic 
							runat=server
							ControlToValidate=txtContractDate
							text="<br>Please enter Contract Date." />
						<asp:Label id=Label3 text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=Label4 display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>					
				</tr>

				<tr><td height=25>Supplier :*</td>
					<td>
					<%--<asp:dropdownlist id=ddlSeller onSelectedIndexChanged=onSeller_Change AutoPostBack=true width=87% runat=server/>--%>
					 <asp:TextBox ID="txtSupCode" runat="server" MaxLength="15" Width="35%"></asp:TextBox>
					  <input type=button value=" ... " id="FindSpl" class="button-small"  onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');" CausesValidation=False runat=server />
 
					  <asp:Button ID="BtnSupDet" CssClass="button-small" runat="server" Text="Click For Get Data Bank" OnClick = "onSeller_Change" ToolTip="Click Here For Get Data Bank"/>&nbsp;
					 <asp:TextBox ID="txtSupName" CssClass="fontObject" runat="server" BackColor="Transparent" BorderStyle="None"
                        Font-Bold="True"   MaxLength="10" Width="99%"></asp:TextBox>
					 
						<asp:RequiredFieldValidator 
							id=rfvSeller
							display=dynamic 
							runat=server
							ControlToValidate=txtSupCode
							text="Please select Seller." />
					</td>
						
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Reference No. : </td>
				    <td><asp:Textbox id=txtBuyerNo maxlength=128 width=100% CssClass="fontObject" runat=server/>
				    </td>	
				</tr>
				<tr>
					<td height=25 valign=top>Additional Note : </td>
					<td><textarea rows=3 id=taAddNote cols=50 style='width:100%;' class="fontObject" runat=server></textarea></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td class="mt-h">Price Information</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Pricing Method : </td>
					<td ><asp:RadioButtonList id=rdPricingMtd repeatdirection=horizontal onSelectedIndexChanged=onChange_PricingMtd CssClass="font9Tahoma" autopostback=true runat=server>
							<asp:ListItem id=item1 value=1 text="Harga Papan" Selected  runat=server />
							<asp:ListItem id=item2 Value=2 text="Harga Disbun" runat=server />
							<asp:ListItem id=item3 Value=3 text="Harga Tetap" runat=server /> 
							<asp:ListItem id=itemRange Value=4 text="Harga Range" runat=server /> 
							<asp:ListItem id=itemRendement Value=5 text="Harga Rendement" runat=server /> 
						</asp:RadioButtonList>
					</td>
			    </tr>
			    <tr id=trPapan runat=server>
			        <td height=25></td>
			        <td>
                        <asp:CheckBox ID="chkPapan" enabled=false Text="checked if using price on same periode as beginning contract" runat="server" />
                    </td>
			    </tr>
				<tr id=trQty>
					<td height=25>Quantity (Kg) : </td>
					<td><asp:Textbox id=txtContractQty maxlength=128 width=50% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RegularExpressionValidator id="revContractQty" 
							ControlToValidate="txtContractQty"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvContractQty"
							ControlToValidate="txtContractQty"
							MinimumValue="0"
							MaximumValue="9999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
						<asp:label id=lblErrContractQty visible=false forecolor=red text="<br>Total of Contract Quantity and Extra Quantity should be equal or bigger than Matched Quantity." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Matched Quantity : </td>
					<td><asp:label id=lblMatchedQty maxlength=128 width=50% runat=server/></td>
				</tr>
				<tr id=tr1>
					<td height=25>Tolerance Limit (%) : </td>
					<td><asp:Textbox id=txtExtraQty maxlength=2 width=20% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RegularExpressionValidator id="revExtraQty" 
							ControlToValidate="txtExtraQty"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 2 decimal points."
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
						<asp:label id=lblErrExtraQty visible=false forecolor=red text="<br>Total of Extra Quantity and Extra Quantity should be equal or bigger than Matched Quantity." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Balance Quantity : </td>
					<td><asp:label id=lblBalQty maxlength=128 width=50% runat=server/></td>
				</tr>
				<tr id=trPrice>
					<td height=25>Price :</td>
					<td><asp:dropdownlist id=ddlCurrency width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtPrice maxlength=128 width=34% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvPrice
							display=dynamic 
							runat=server
							ControlToValidate=txtPrice
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="revPrice" 
							ControlToValidate="txtPrice"
							ValidationExpression="\d{1,17}\.\d{0,5}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 5 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvPrice"
							ControlToValidate="txtPrice"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
					</td>
					
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr id=trAddFee runat=server>
					<td height=25>Additional Fee(A) :</td>
					<td><asp:dropdownlist id=ddlCurrAddFee width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtAddFee maxlength=128 width=34% Text=0 style="text-align:right" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator1
							display=dynamic 
							runat=server
							ControlToValidate=txtAddFee
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator1" 
							ControlToValidate="txtAddFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator1"
							ControlToValidate="txtAddFee"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/> <br />									                       					
													                       					
					</td>										 									
					<td colspan=4>&nbsp;</td>
			    </tr>

				<tr id=trAddFee2 runat=server>
					<td height=25>Additional Fee(B) :</td>
					<td><asp:dropdownlist id=ddlCurrAddFee2 width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtAddFee2 maxlength=128 width=34% Text=0 style="text-align:right" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator5
							display=dynamic 
							runat=server
							ControlToValidate=txtAddFee2
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator4" 
							ControlToValidate="txtAddFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator4"
							ControlToValidate="txtAddFee2"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/> <br />									                       																		                       					
					</td>										 									
					<td colspan=4>&nbsp;</td>
			    </tr>

				<tr id=trAddFee3 runat=server>
					<td height=25>Additional Fee(C) :</td>
					<td><asp:dropdownlist id=ddlCurrAddFee3 width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtAddFee3 maxlength=128 width=34% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator6
							display=dynamic 
							runat=server
							ControlToValidate=txtAddFee3
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator5" 
							ControlToValidate="txtAddFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator5"
							ControlToValidate="txtAddFee3"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/> <br />									                       					
													                       					
					</td>										 									
					<td colspan=4>&nbsp;</td>
			    </tr>
				
				<tr id=trAddFeeS1 runat=server>
					<td height=25>Additional Fee(Grade S1) :</td>
					<td><asp:dropdownlist id=ddlCurrAddFeeS1 width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtAddFeeS1 maxlength=128 width=34% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator8
							display=dynamic 
							runat=server
							ControlToValidate=txtAddFeeS1
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator7" 
							ControlToValidate="txtAddFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator7"
							ControlToValidate="txtAddFeeS1"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/> <br />									                       								                       					
					</td>										 									
					<td colspan=4>&nbsp;</td>
			    </tr>
				<tr id=trAddFeeS2 runat=server>
					<td height=25>Additional Fee(Grade S2) :</td>
					<td><asp:dropdownlist id=ddlCurrAddFeeS2 width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtAddFeeS2 maxlength=128 width=34% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator9
							display=dynamic 
							runat=server
							ControlToValidate=txtAddFeeS2
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator8" 
							ControlToValidate="txtAddFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator8"
							ControlToValidate="txtAddFeeS2"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/> <br />									                       								                       					
					</td>										 									
					<td colspan=4>&nbsp;</td>
			    </tr>
				
				<tr id=trAddFeeS3 runat=server>
					<td height=25>Additional Fee(Grade S3) :</td>
					<td><asp:dropdownlist id=ddlCurrAddFeeS3 width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtAddFeeS3 maxlength=128 width=34% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator10
							display=dynamic 
							runat=server
							ControlToValidate=txtAddFeeS3
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator9" 
							ControlToValidate="txtAddFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator9"
							ControlToValidate="txtAddFeeS3"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/> <br />									                       								                       					
					</td>										 									
					<td colspan=4>&nbsp;</td>
			    </tr>
				<tr id=trAddFee4 runat=server>
					<td height=25>Brondolan Fee :</td>
					<td><asp:dropdownlist id=ddlCurrBrd width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtAddFeeBrd maxlength=128 width=34% Text=0 style="text-align:right" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator7
							display=dynamic 
							runat=server
							ControlToValidate=txtAddFeeBrd
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator6" 
							ControlToValidate="txtAddFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator6"
							ControlToValidate="txtAddFeeBrd"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/> <br />									                       					
													                       					
					</td>										 									
					<td colspan=4>&nbsp;</td>
			    </tr>
						    			    			    
				<tr id=trBonusFee visible=false runat=server>
					<td height=25>Bonus Fee :</td>
					<td><asp:dropdownlist id=ddlCurrBonusFee width=15% CssClass="fontObject" runat=server />
						<asp:Textbox id=txtBonusFee maxlength=128 width=34% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RequiredFieldValidator 
							id=RequiredFieldValidator4
							display=dynamic 
							runat=server
							ControlToValidate=txtBonusFee
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="RegularExpressionValidator2" 
							ControlToValidate="txtBonusFee"
							ValidationExpression="\d{1,17}\.\d{0,2}|\d{1,17}"
							Display="Dynamic"
							text = "<br>Maximum length 17 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator2"
							ControlToValidate="txtBonusFee"
							MinimumValue="0"
							MaximumValue="99999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				
	            <tr id=trBonus runat=server>
	                <td height="25">Bonus Fee :
		            </td>
	            </tr>
                <tr id=trBonus1 runat=server>
		            <td align="left" width="20%" style="height: 28px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Qty Level 1</td>
		            <td align="left" style="height: 28px;" colspan="3">
                        min
                        <asp:TextBox ID="txt_min1" runat="server" MaxLength="64" CssClass="fontObject" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max1" runat="server" MaxLength="64" CssClass="fontObject" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>&nbsp; fee
                        <asp:TextBox ID="txt_fee1" runat="server" MaxLength="6" CssClass="fontObject" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                        <asp:DropDownList id="ddl_opsi1" width=10% CssClass="fontObject" runat=server>
							<asp:ListItem value="1" Selected>IDR</asp:ListItem>
							<asp:ListItem value="2">%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		            <td style="height: 28px"></td>
	            </tr>
	            <tr id=trBonus2 runat=server>
		            <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Qty Level 2</td>
		            <td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_min2" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max2" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>&nbsp; fee
                        <asp:TextBox ID="txt_fee2" CssClass="fontObject" runat="server" MaxLength="6" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                    <asp:DropDownList id="ddl_opsi2" width=10% CssClass="fontObject" runat=server>
							<asp:ListItem value="1" Selected>IDR</asp:ListItem>
							<asp:ListItem value="2">%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		            <td></td>
	            </tr>
	            <tr id=trBonus3 runat=server>
		            <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Qty Level 3</td>
		            <td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_min3" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max3" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>&nbsp; fee
                        <asp:TextBox ID="txt_fee3" CssClass="fontObject" runat="server" MaxLength="6" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                        <asp:DropDownList id="ddl_opsi3" width=10% CssClass="fontObject" runat=server>
							<asp:ListItem value="1" Selected>IDR</asp:ListItem>
							<asp:ListItem value="2">%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		            <td></td>
	            </tr>
	            <tr id=trBonus4 runat=server>
		            <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Qty Level 4</td>
		            <td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_min4" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max4" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>&nbsp; fee
                        <asp:TextBox ID="txt_fee4" CssClass="fontObject" runat="server" MaxLength="6" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                        <asp:DropDownList id="ddl_opsi4" width=10% CssClass="fontObject" runat=server>
							<asp:ListItem value="1" Selected>IDR</asp:ListItem>
							<asp:ListItem value="2">%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		            <td></td>
	            </tr>
				<tr id=trBonus5 runat=server>
		            <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Qty Level 5</td>
		            <td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_min5" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_max5" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="15%"></asp:TextBox>&nbsp; fee
                        <asp:TextBox ID="txt_fee5" CssClass="fontObject" runat="server" MaxLength="6" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                        <asp:DropDownList id="ddl_opsi5" width=10% CssClass="fontObject" runat=server>
							<asp:ListItem value="1" Selected>IDR</asp:ListItem>
							<asp:ListItem value="2">%</asp:ListItem>
                        </asp:DropDownList>
                    </td>
		            <td></td>
	            </tr>
	            
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
	            <tr>
				    <td height=25>Deduction : </td>
					<td><asp:RadioButtonList id=rdDeductionMtd repeatdirection=horizontal onSelectedIndexChanged=onChange_DeductionMtd autopostback=true CssClass="font9Tahoma" runat=server>
							<asp:ListItem id=item6 value=1 text="Fix Rate" Selected runat=server />
							<asp:ListItem id=item7 Value=2 text="Max. Rate" runat=server />
							<asp:ListItem id=item8 Value=3 text="Progressive" runat=server /> 
						</asp:RadioButtonList>
					</td>
			    </tr>
	            <tr id=trDeduct runat=server>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rate (%) : </td>
					<td><asp:Textbox id=txtDeductionRate maxlength=5 width=20% Text=0 style="text-align:right" CssClass="fontObject" runat=server/>
						<asp:RegularExpressionValidator id="RegularExpressionValidator3" 
							ControlToValidate="txtDeductionRate"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="RangeValidator3"
							ControlToValidate="txtDeductionRate"
							MinimumValue="0"
							MaximumValue="9999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
					</td>
				</tr>
				<tr id=trDeduct1 visible=false runat=server>
		            <td align="left" width="20%" style="height: 28px">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deduction Level 1</td>
		            <td align="left" style="height: 28px;" colspan="3">
                        min
                        <asp:TextBox ID="txt_minDeduct1" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_maxDeduct1" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>&nbsp; rate
                        <asp:TextBox ID="txt_rate1" CssClass="fontObject" runat="server" MaxLength="6" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                    </td>
		            <td style="height: 28px"></td>
	            </tr>
	            <tr id=trDeduct2 visible=false runat=server>
		            <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deduction Level 2</td>
		            <td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_minDeduct2" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_maxDeduct2" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>&nbsp; rate
                        <asp:TextBox ID="txt_rate2" CssClass="fontObject" runat="server" MaxLength="6" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                    </td>
		            <td></td>
	            </tr>
	            <tr id=trDeduct3 visible=false runat=server>
		            <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deduction Level 3</td>
		            <td align="left" colspan="3">
                        min
                        <asp:TextBox ID="txt_minDeduct3"  CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                        max
                        <asp:TextBox ID="txt_maxDeduct3" CssClass="fontObject" runat="server" MaxLength="64" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>&nbsp; rate
                        <asp:TextBox ID="txt_rate3" CssClass="fontObject" runat="server" MaxLength="6" Text=0 onkeypress="javascript:return isNumberKey(event)"
                            Width="10%"></asp:TextBox>
                    </td>
		            <td></td>
	            </tr>
	            <tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
	            <tr>
    				<td>Ongkos Bongkar Charged :	</td>
    				<td><asp:CheckBox id="chkOB" checked=true runat=server /></td>
				</tr>
				<tr>
    				<td>Ongkos Lapangan Charged :	</td>
    				<td><asp:CheckBox id="chkOL"  checked=true runat=server /></td>
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
					<td height=25>Term Of Weighing :*</td>
					<td><asp:DropDownList id="ddlTermOfWeighing" width=100% CssClass="fontObject" runat=server>
							<asp:ListItem value="1" Selected>Mill Weighbridge</asp:ListItem>
							<asp:ListItem value="2">Supplier Weighbridge</asp:ListItem>										
						</asp:DropDownList>
					</td>
					<td>&nbsp;</td>
				</tr>	
				
                <tr>
					<td height=25 valign=top>Remarks : </td>
					<td><textarea rows=3 id=taRemarks cols=50 style='width:100%;' class="fontObject" runat=server></textarea></td>
					<td colspan=4>&nbsp;</td>
				</tr>
				
			    
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
                <tr>
					<td class="mt-h">Payment Information</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
                <tr>
				    <td height=25>Invoicing Method : </td>
					<td><asp:RadioButtonList id=rdInvoiceMtd repeatdirection=horizontal onSelectedIndexChanged=onChange_InvoiceMtd autopostback=true CssClass="font9Tahoma" runat=server>
							<asp:ListItem id=item4 value=1 text="Monthly" Selected runat=server />
							<asp:ListItem id=item5 Value=2 text="Weekday" runat=server />
						</asp:RadioButtonList>
					</td>
			    </tr>
			    <tr>
			        <td height=25></td>
			        <td>
                        <asp:CheckBox ID="chkSenin" Text="Monday" Enabled=false runat="server" />
                        <asp:CheckBox ID="chkSelasa" Text="Tuesday" Enabled=false runat="server" />
                        <asp:CheckBox ID="chkRabu" Text="Wednesday" Enabled=false runat="server" />
                        <asp:CheckBox ID="chkKamis" Text="Thursday" Enabled=false runat="server" />
                        <asp:CheckBox ID="chkJumat" Text="Friday" Enabled=false runat="server" />
                    </td>
			    </tr>
				<tr>
					<td height="25">Bank for DPP :</td>
					<td><asp:DropDownList id="ddlBankCode1" OnSelectedIndexChanged="onSelect_Bank1" AutoPostBack=true CssClass="fontObject" runat="server" Width="100%"></asp:DropDownList></td>
					<td>&nbsp;</td>
					<td height="25"></asp:label></td>
					<td><asp:TextBox id="txtBankAccNo1" Visible=false ReadOnly=true CssClass="fontObject" runat="server" MaxLength="20"></asp:TextBox></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Bank for PPN :</td>
					<td><asp:DropDownList id="ddlBankCode2" OnSelectedIndexChanged="onSelect_Bank2"  AutoPostBack=true  CssClass="fontObject" runat="server" Width="100%"></asp:DropDownList></td>
					<td>&nbsp;</td>
					<td height="25"></asp:label></td>
					<td><asp:TextBox id="txtBankAccNo2" Visible=false ReadOnly=true CssClass="fontObject" runat="server" MaxLength="20"></asp:TextBox></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Term of payment : </td>
					<td><textarea rows=3 id=taTermOfPayment cols=50 style='width:100%;' class="fontObject" runat=server></textarea></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				
				<tr>
					<td height=25 valign=top>Supplier Name :*</td>
					<td><asp:Textbox id=txtUnderName Text="" maxlength=128 width=100% CssClass="fontObject" runat=server/></td>	
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25 valign=top>Undersigned Post :*</td>
					<td><asp:Textbox id=txtUnderPost text="" maxlength=128 width=100% CssClass="fontObject" runat=server/></td>		
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
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton ID=ConfirmBtn UseSubmitBehavior="false" onclick=Button_Click ImageUrl="../../images/butt_confirm.gif" CommandArgument=Confirm Runat=server />
						<asp:ImageButton ID=EditBtn UseSubmitBehavior="false" onclick=Button_Click ImageUrl="../../images/butt_edit.gif" CommandArgument=Edit Runat=server />
						<asp:ImageButton id=PrintBtn runat="server" 
                            ImageUrl="../../images/butt_print.gif" AlternateText="Print" 
                            CausesValidation="False" onclick="PrintBtn_Click" Visible="False"></asp:ImageButton>	
						<asp:ImageButton id=CloseBtn AlternateText=" Close "  imageurl="../../images/butt_Close.gif" onclick=Button_Click CommandArgument=Close runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
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
		</form>
	</body>
</html>
