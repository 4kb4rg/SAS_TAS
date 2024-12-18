<%@ Page Language="vb" trace="false" codefile="../../../include/CB_trx_ReceiptDet.aspx.vb" Inherits="cb_trx_ReceiptDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCB" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Cash And Bank - Receipt Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		    function lostFocusAmount() {
			    var doc = document.frmMain;
			    var x = doc.txtAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtAmount.value = toCurrency(round(dbAmt, 0));
	        }
	        
	        function lostFocusDPP() {
			    var doc = document.frmMain;
			    var x = doc.txtDPPAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtDPPAmount.value = toCurrency(round(dbAmt, 0));
	        }
	        
		    function toCurrency(num) {
              num = num.toString().replace(/\$|\,/g, '')
              if (isNaN(num)) num = "0";
              sign = (num == (num = Math.abs(num)));
              num = Math.floor(num * 100 + 0.50000000001);
              cents = num % 100;
              num = Math.floor(num / 100).toString();
              if (cents < 10) cents = '0' + cents;

              for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3))
              }

              return (((sign) ? '' : '-') + num + '.' + cents)
            }
		</script>
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            

.button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

		<TABLE id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
        <tr>
        <td>

			<tr>
				<td colspan="6"><UserControl:MenuCB id=MenuCB runat="server" /></td>
			</tr>
			<tr>
				<td  colspan="6">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                              <strong> RECEIPT DETAILS</strong> </td>
                            <td class="font9Header" style="text-align: right">
                                &nbsp;</td>
                        </tr>
                    </table>
                     <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 width="20%">Receipt ID :</td>
				<td width="40%"><asp:Label id=lblReceiptID runat=server /></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period :</td>
				<td width="20%"> <asp:Label id=lblAccPeriod runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
			    <td height=25>Transaction Date :</td>
			    <td><asp:TextBox id=txtDateCreated CssClass="fontObject" width=25% maxlength="10" runat="server"/>
					<a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:RequiredFieldValidator	id="RequiredFieldValidator1" runat="server"  ControlToValidate="txtDateCreated" text = "Please enter Date Created" display="dynamic"/>
					<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Status : </td>
				<td> <asp:Label id=lblStatus runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblBillPartyTag" runat="server" /> :*</td>
				<td>
					
					<telerik:RadComboBox   CssClass="fontObject" ID="radcmbCust"  autopostback=true
						OnSelectedIndexChanged="onSelect_BillParty"
						Runat="server" AllowCustomText="True" 
						EmptyMessage="Plese Select Customer " Height="200" Width="100%" 
						ExpandDelay="50" Filter="Contains" Sort="Ascending" 
						EnableVirtualScrolling="True">
						<CollapseAnimation Type="InQuart" />
					</telerik:RadComboBox>	

					<asp:Label id=lblErrBillParty visible=false forecolor=red runat=server/>
				</td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td> <asp:Label id=lblDateCreated runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Receipt Type :*</td>
				<td><asp:DropDownList width=100% id=ddlRecType CssClass="fontObject"   autopostback=true runat=server onSelectedIndexChanged=onSelect_RecType>
						<asp:ListItem value="0">Cheque</asp:ListItem>
						<asp:ListItem value="1">Cash</asp:ListItem>
						<asp:ListItem value="2">Other Party</asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>&nbsp;</td>
				<td>Last Update : </td>
				<td> <asp:Label ID=lblLastUpdate runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			
			<tr>
				<td height=25>Bank Code :</td>
				<td><asp:DropDownList width=100% id=ddlBank CssClass="fontObject"  autopostback=true onSelectedIndexChanged=onSelect_Bank runat=server />
					<asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/>
					<asp:RequiredFieldValidator 
							id=rfvBankCode
							display=dynamic 
							runat=server
							ControlToValidate=radcmbCust
							text="<br>Please enter Bank Code." />
					</td>
				<td>&nbsp;</td>
				<td>Print Date :</td>
				<td> <asp:Label ID=lblPrintDate runat=server />
                                </td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Cheque No. :</td>
				<td><asp:Textbox id=txtChequeNo CssClass="fontObject"  width=100% maxlength=32 runat=server/></td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td> <asp:Label ID=lblUpdatedBy runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
                <td style="height: 25px"> <asp:Label id=lblSKBStartDate runat=server Visible="False" />&nbsp; </td>
				<td style="height: 25px"> <asp:Label id=LblIsSKBActive runat=server Visible="False" />
                            </td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
        </td>
        </tr>
        </TABLE>
        <TABLE id="TABLE1"  cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma" runat=server>
            <tr>
            <td>
			<tr>
				<td colSpan="6">
					<!--<table width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center">
						<tr>						
							<td>-->
								<TABLE id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server class="sub-Add">
								    <tr class="mb-c">
					                    <td colspan=6><asp:Label id=Label10 text="Please select one of these option:" Font-Bold=true runat=server /></td>
					                </tr>
									<tr class="mb-c">
										<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1. Invoice ID :</td>
										<td colspan=5><asp:DropDownList id=ddlInvoice CssClass="fontObject"  autopostback=true onSelectedIndexChanged=onSelect_InvRcv width=95% runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. Debit Note ID :</td>
										<td colspan=5><asp:DropDownList id=ddlDebitNote CssClass="fontObject"  autopostback=true onSelectedIndexChanged=onSelect_DN width=95% runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. Credit Note ID :</td>
										<td colspan=5><asp:Dropdownlist id=ddlCreditNote CssClass="fontObject"  autopostback=true onSelectedIndexChanged=onSelect_CN width=95% runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4. Debtor Journal ID :</td>
										<td colspan=5><asp:Dropdownlist id=ddlDebtorJrn CssClass="fontObject"  autopostback=true onSelectedIndexChanged=onSelect_DJ width=95% runat=server/></td>
									</tr>	
									<tr class="mb-c">
							            <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5. Other :</td>
							            <td colSpan="5">
                                            <asp:TextBox ID="txtAccCodeOth" runat="server" AutoPostBack="True" 
                                                CssClass="fontObject" MaxLength="15" OnTextChanged="onSelect_StrAccCodeOther" 
                                                Width="22%"></asp:TextBox>
&nbsp;<input id="btnFind2" runat="server" class="Button-Small" 
                                                onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCodeOth', 'txtAccNameOth', 'False');" 
                                                type="button" value=" ... " />
                                            <asp:Button ID="BtnGetDataOth" runat="server" CssClass="button-small" 
                                                Font-Bold="True" OnClick="onSelect_Other" Text="Click Here" 
                                                ToolTip="Click For Refresh COA " Width="70px" />
&nbsp;<asp:TextBox ID="txtAccNameOth" runat="server" BackColor="Transparent" BorderStyle="None" CssClass="fontObject" 
                                                Font-Bold="True" MaxLength="10" Width="55%"></asp:TextBox>
&nbsp;</td>
						            </tr>
						            <tr class="mb-c">
							            <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6. Contract No. 
                                            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (Advance Receipt) :</td>
							            <td colSpan="5"><asp:DropDownList width="95%" id=ddlContract CssClass="fontObject"  runat="server" autopostback=true onSelectedIndexChanged=onSelect_Contract />
							                <asp:Label id=lblErrNoSelectDoc visible=false text="Please select either Invoice, Debit Note or Credit Note." forecolor=red runat=server />
											<asp:Label id=lblErrManySelectDoc visible=false text="Please select ONLY one document." forecolor=red runat=server />					
											<asp:Label id=lblErrValidPPNHRate forecolor=red visible=false text="Please select invoice with the same PPN and PPH 23/26 Rate." runat=server/>
							            </td>
						            </tr>
									<tr class="mb-c">
					                    <td colspan=6>&nbsp;</td>
					                </tr>
									<tr class="mb-c">
										<td height=25><asp:label id="lblAccCodeTag" runat="server" /> (DR) :* </td>
										<td colspan=5>
                                            <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" 
                                                CssClass="fontObject" MaxLength="15" OnTextChanged="onSelect_StrAccCode" 
                                                Width="22%"></asp:TextBox>
&nbsp;<input id="btnFind1" runat="server" class="button-small" 
                                                onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');" 
                                                type="button" value=" ... " />
                                            <asp:Button ID="BtnGetData" runat="server" CssClass="button-small" 
                                                Font-Bold="True" OnClick="onSelect_Account" Text="Click Here" 
                                                ToolTip="Click For Refresh COA " Width="70px" />
&nbsp;<asp:TextBox ID="txtAccName" runat="server" BackColor="Transparent" BorderStyle="None" CssClass="fontObject" 
                                                Font-Bold="True" MaxLength="10" Width="55%"></asp:TextBox>
&nbsp;<asp:RequiredFieldValidator id=rfvAccCode display=dynamic runat=server 
												ControlToValidate=txtAccCode />		</td>
									</tr>
									
									
									<tr id="RowChargeLevel" class="mb-c">
										<td height="25">Charge Level :* </td>
										<td colspan=5><asp:DropDownList id="ddlChargeLevel" CssClass="fontObject"  Width=95% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server /> </td>
									</tr>
									<tr id="RowPreBlk" class="mb-c">
										<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
										<td colspan=5><asp:DropDownList id="ddlPreBlock" CssClass="fontObject"  Width=95% runat=server />
										  <asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
									</tr>
									<tr id="RowBlk" class="mb-c">
										<td height=25><asp:label id=lblBlkTag Runat="server"/></td>
										<td colspan=5><asp:DropDownList id="lstBlock" CssClass="fontObject"  Width=95% runat=server />
										  <asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
									</tr>		
									<tr class="mb-c">
										<td height=25><asp:label id="lblVehTag" Runat="server"/></td>
										<td colspan=5><asp:DropDownList id="lstVehCode" Width=95%  CssClass="fontObject" runat=server />
										  <asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id="lblVehExpTag" Runat="server"/></td>
										<td colspan=5><asp:DropDownList id="lstVehExp"  CssClass="fontObject"  Width=95% runat=server />
										  <asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
									</tr>
									
		                            <tr class="mb-c">
									    <td height=25>Currency Code :</td>
				                        <td colSpan="5"><asp:DropDownList id=ddlCurrency  CssClass="fontObject"  width=95% runat=server/></td>	
									</tr>
									<tr class="mb-c">
									    <td height=25>Exchange Rate :</td>
				                        <td colSpan="5"><asp:TextBox id=txtExRate  CssClass="fontObject"  text="1" width=20% maxlength=20 runat=server /></td>				
				                    </tr>
				                    <tr id="RowTax" visible=false class="mb-c">
							            <td height=25>Tax Object :</td>
							            <td colSpan="5"><asp:DropDownList id="lstTaxObject"  CssClass="fontObject"  Width=95% AutoPostBack=True OnSelectedIndexChanged=lstTaxObject_OnSelectedIndexChanged runat=server />
									              <asp:label id=lblTaxObjectErr Visible=False forecolor=red Runat="server" />
						                </td>
						            </tr>
						            <tr id="RowTaxAmt" visible=false class="mb-c">
						                <td height=25 width="20%">DPP Amount : </td>
						                <td width="30%"><asp:Textbox id="txtDPPAmount"  CssClass="fontObject"  text=0 style="text-align:right"  Width=20% maxlength=22 OnKeyUp="javascript:calTaxPrice();" OnBlur="javascript:lostFocusDPP();" runat=server />
								            <asp:Label id=lblErrAmountDPP visible=false forecolor=red text="<BR>Please enter DPP amount" runat=server/>
						                </td>
						                <td width="5%"></td>
							            <td width="15%"></td>	
							            <td width="10%"></td>
							            <td width="30%"></td>
						            </tr>
									<tr class="mb-c">
										<td height=25 width=20%>Amount :*</td>
										<td colspan=5 width=80%>
											<asp:Textbox id=txtAmount text=0  CssClass="fontObject" Font-Bold="true"   width=20% maxlength=22 style="text-align:right" runat=server/>
											<asp:RequiredFieldValidator id=rfvAmount display=dynamic runat=server 
												ErrorMessage="Please enter Amount" 
												ControlToValidate=txtAmount />	
											
											<asp:Label id=lblErrAmount visible=false text="Amount is invalid." forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">	
									    <td height=25 width=20%></td>
										<td colspan=5 width=80%><asp:radiobuttonlist id=rblReceiptTerm TextAlign="Right" RepeatColumns="3" RepeatLayout="Flow" runat=server /></td>
									</tr>				
									<tr class="mb-c">
										<td height=25 width=20%><asp:Label id=lblPPN text='PPN :' runat=server/></td>
										<td width=20%><asp:CheckBox ID=cbPPN text=" Yes" Enabled=false   Runat=server /></td>
										<td width=10%><asp:Label id=lblPPH text='PPh 23/26 Rate :' visible=false runat=server/></td>
										<td width=20%><asp:Textbox id=txtPPHRate  CssClass="fontObject"  width=50% maxlength=5 visible=false  runat=server/>
										<asp:Label id=lblPercen text='%' visible=false  runat=server/>
										<asp:CompareValidator visible=false id="cvPPHRate" display=dynamic runat="server" 
												ControlToValidate="txtPPHRate" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>
										<asp:RegularExpressionValidator id=revPPHRate 
												ControlToValidate="txtPPHRate"
												ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
												Display="Dynamic"
												text = "<br>Maximum length 2 digits and 2 decimal points. "
												runat="server"/>
										</td>
										<td width=10%>&nbsp;</td>	
										<td width=20%>&nbsp;</td>	
									</tr>
									<tr class="mb-c">
					                    <td height=25 valign=top>Additional Note :</td>
                                        <td colSpan="5"><textarea rows=4 id=txtAddNote cols=100 style='width:95%;'  CssClass="fontObject" runat=server></textarea></td>
					                </tr>
									<tr class="mb-c">
										<td height=25 colspan=6>
											<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add onclick=AddBtn_Click runat=server /> 
										&nbsp;</td>
									</tr>
								</TABLE>
							<!--</td>
						</tr>
					</table>-->
				</td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>			
			<tr>
				<td colSpan="6">
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
                        OnItemDataBound="dgLine_BindGrid" 
						OnDeleteCommand=DEDR_Delete 
						Pagerstyle-Visible=False
						AllowSorting="True"  >	
						
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
          
						<Columns>						
							<asp:TemplateColumn HeaderText="Document ID" ItemStyle-Width=10%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("DocID") %> id="lblDocID" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Document Type" ItemStyle-Width=10%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("DocType") %> id="lblDocType" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width=10%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="17%" HeaderText="COA Descr">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccDescr") %> id="lblAccDescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Quantity" Visible = False>
								<HeaderStyle HorizontalAlign="Right" />			
								<ItemStyle width="8%" HorizontalAlign="Right" />			
								<ItemTemplate>
									<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Quantity"),5) %> 				
								    <asp:label id="lblQtyTrx" text= '<%# Container.DataItem("Quantity") %>' visible=false runat="server" />							
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" ItemStyle-Width=1% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("NetAmountToDisplay"), 2), 0) %> id="lblViewNetAmount" visible = False  runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("NetAmount"), 2) %> id="lblNetAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" ItemStyle-Width=1% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPNAmountToDisplay"), 2), 0) %> id="lblViewPPNAmount" visible = False  runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %> id="lblPPNAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="" ItemStyle-Width=1% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PPHAmountToDisplay"), 2), 0) %> id="lblViewPPHAmount" visible = False runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("PPHAmount"), 2) %> id="lblPPHAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="17%" HeaderText="Notes">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblReceiptTermDescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width=12% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountToDisplay"), 2), 0) %> id="lblViewAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-HorizontalAlign=Right ItemStyle-Width=5%>
								<ItemTemplate>
								    <asp:label text= '<%# Container.DataItem("BlkCode") %>' id="lblBlkCode" Visible=false runat="server" />
								    <asp:label text= '<%# Container.DataItem("VehCode") %>' id="lblVehCode" Visible=false runat="server" />
								    <asp:label text= '<%# Container.DataItem("VehExpCode") %>' id="lblVehExpCode" Visible=false runat="server" />
									<asp:label id=lblLnID visible="false" text=<%# Container.DataItem("ReceiptLnID")%> runat="server" />
									<asp:label id=lblAmt visible="false" text=<%# Container.DataItem("Amount")%> runat="server" />
									<asp:label id=lblDocTypeVal visible="false" text=<%# Container.DataItem("DocType")%> runat="server" />
									<asp:LinkButton id=lbDelete CausesValidation=False CommandName=Delete Text=Delete runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>	
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr style="width :100%" /></td>
				<td>&nbsp;</td>
			</tr>		
			<tr>
				<td colspan=3>&nbsp;</td>
				<td height=25>Total Amount : </td>
				
				<td Align=right><asp:Label ID=lblCurrency CssClass="font9Tahoma" Runat=server />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID=lblViewTotalAmount CssClass="font9Tahoma" Runat=server />&nbsp;</td>
				<td Align=right><asp:Label ID=lblTotalAmount CssClass="font9Tahoma" Visible = False Runat=server />&nbsp;</td>
				
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Remarks:</td>
				<td colSpan="5"><asp:TextBox ID=txtRemark CssClass="font9Tahoma"  maxlength=256 width=100% Runat=server /></td>
			</tr>
			<tr>
				<td colSpan="6">&nbsp;</td>
			</tr>			
			<tr>
				<td colSpan="6">
					<asp:Label id=lblErrTotal forecolor=red visible=false CssClass="font9Tahoma" text="Total amount cannot be zero.<br>" runat=server/>
                    <asp:Label id=lblErrAction forecolor=red CssClass="font9Tahoma" visible=false Text="" runat=server />
					<br />
						        <asp:ImageButton id=NewBtn onClick=NewBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Receipt" runat=server/>
					<asp:ImageButton ID=SaveBtn CausesValidation=False onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=ConfirmBtn CausesValidation=False onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
                    <asp:ImageButton ID=EditBtn UseSubmitBehavior="false" onclick=EditBtn_Click   ImageUrl="../../images/butt_edit.gif" AlternateText=Edit CausesValidation=False runat="server" />
					<asp:ImageButton ID=PrintBtn UseSubmitBehavior="false" onclick=btnPreview_Click CausesValidation=false ImageUrl="../../images/butt_print.gif" AlternateText=Print Runat=server />
					<asp:ImageButton ID=DeleteBtn CausesValidation=False onclick=DeleteBtn_Click ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />			
					<asp:ImageButton ID=BackBtn CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=hidReceiptID value="" runat=server />
					<Input type=hidden id=hidStatus value="" runat=server />
					<Input type=hidden id=hidTotalAmt value="" runat=server />
				</td>
			</tr>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:Label id=lblAccount visible=false runat=server />
			<asp:Label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:Label id=lblSelect visible=false text="Select " runat=server />
			<asp:Label id=lblPPHRateHidden visible=false runat=server />
			<asp:Label id=lblPPNHidden visible=false runat=server />
			<asp:Label id=lblhidstatus visible=false runat=server />
			
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr id=TrLink runat=server>
				<td colspan=5>
					<asp:LinkButton id=lbViewJournal CssClass="font9Tahoma" text="View Journal Predictions" causesvalidation=false runat=server /> 
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgViewJournal
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false" >	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
  
						<Columns>
							<asp:TemplateColumn HeaderText="COA Code">
							    <ItemStyle Width="20%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("ActCode") %> id="lblCOACode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Description">
							     <ItemStyle Width="40%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Description") %> id="lblCOADescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Debet">
							    <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
							    <ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountDB"), 2), 2) %> id="lblAmountDB" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>									
						    <asp:TemplateColumn HeaderText="Credit">
						        <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
								<ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountCR"), 2), 2) %> id="lblAmountCR" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>		
						    <asp:TemplateColumn>		
								<ItemStyle HorizontalAlign="Right" /> 									
								<ItemTemplate>
									
								</ItemTemplate>
							</asp:TemplateColumn>							
						</Columns>
					</asp:DataGrid>
                    <br />

             <telerik:RadInputManager  ID="RadInputManager1" runat="server">
                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type">
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtDPPAmount" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             
            </telerik:RadInputManager>
	 
             <telerik:RadInputManager  ID="RadInputManager2" runat="server">
                     <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type">
                        <TargetControls>
                            <telerik:TargetInput ControlID="txtAmount" />
                        </TargetControls>
                    </telerik:NumericTextBoxSetting>             
            </telerik:RadInputManager>

				</td>
			</tr>	
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
			    <td>&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal CssClass="font9Tahoma" Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>
        </td>
        </tr>
		</TABLE>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=HidARCurrency value="IDR" runat=server/>
		    <Input type=hidden id=hidARExRate value="1" runat=server/>
		    <Input type=hidden id=HidARAmount value="1" runat=server/>
		    
		    <Input type=hidden id=hidTaxObjectRate value=0 runat=server />
		    <Input type=hidden id=hidPPNInit value=0 runat=server />
		    <Input type=hidden id=hidCOATax value=0 runat=server />
		    <Input type=hidden id=hidFindPOPPH23 value=0 runat=server />
		    <Input type=hidden id=hidPOPPH23 value=0 runat=server />
		    <Input type=hidden id=hidTaxStatus value=1 runat=server />
		    <Input type=hidden id=hidHadCOATax value=0 runat=server />
		    <Input type=hidden id=hidDocID value="" runat=server />
		    <Input type=hidden id=hidFindPOPPH21 value=0 runat=server />
		    <Input type=hidden id=hidPOPPH21 value=0 runat=server />
	 
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
