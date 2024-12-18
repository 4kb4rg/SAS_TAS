<%@ Page Language="vb" src="../../../include/ap_trx_CJDet.aspx.vb" Inherits="ap_trx_CJDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Creditor Journal Details</title>
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function fnBindAccMonth(ddlAccMonth, ddlAccYear) {
				var doc = document.frmMain;
				var intAccYear = parseFloat(ddlAccYear.options[ddlAccYear.selectedIndex].value);
				var arrPeriod, arrData;
				var intMaxPeriod, intYear, intMonth;
                var intSelected, i;
                
                if (intAccYear != 'NaN') {
                    if (intAccYear >= 1901 && intAccYear <= 2029) {
                        intSelected = ddlAccMonth.selectedIndex;
				        arrPeriod = doc.hidPeriodData.value.split(";");
				        intMaxPeriod = 12;
				        
                        for (i = 0; i < arrPeriod.length; i++) {
                            arrData = arrPeriod[i].split("/");
                            intMonth = parseFloat(arrData[0]);
                            intYear = parseFloat(arrData[1]);
                            if (intMonth != 'NaN' && intYear != 'NaN') {
                                if (intAccYear == intYear) {
                                    intMaxPeriod = intMonth;
                                    break;
                                }
                            }
                        }
                        
                        if (ddlAccMonth.options.length < intMaxPeriod) {
                            for(i = ddlAccMonth.options.length + 1; i <= intMaxPeriod; i++) {
                                var objOption = document.createElement("OPTION");
                                objOption.text = i;
                                objOption.value = i;
                                ddlAccMonth.options.add(objOption);
                            }
                        }
                        else if(ddlAccMonth.options.length > intMaxPeriod) {
                            for(i = ddlAccMonth.options.length - 1; i >= intMaxPeriod; i--) {
                                ddlAccMonth.options.remove(i);
                            }
                            
                            if (ddlAccMonth.selectedIndex == -1) {
                                ddlAccMonth.selectedIndex = 0;
                            }
                        }
                    }
                }
			}
		</script>
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

		<table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
			<tr>
				<td colspan="6">
					<UserControl:MenuAP id=MenuAP runat="server" />
				</td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                              <strong>  CREDITOR JOURNAL DETAILS</strong></td>
                            <td class="font9Header"  style="text-align: right">
                                Period : <asp:Label id=lblAccPeriod runat=server />| Status :<asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />
                                | Last Update : <asp:Label ID=lblLastUpdate runat=server />| Updated By :<asp:Label ID=lblUpdatedBy runat=server />
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td height=25 width="20%">&nbsp;</td>
				<td width="40%">&nbsp;</td>
				<td width="5%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="20%">&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 width="20%">Creditor Journal ID :</td>
				<td width="40%"><asp:Label id=lblCJID runat=server /></td>
				<td width="5%"></td>
				<td width="15%">&nbsp;</td>
				<td width="20%">&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Transaction Date : </td>
				<td><asp:TextBox id=txtCJRefDate width=25% maxlength=10 CssClass="fontObject" runat=server />
					<a href="javascript:PopCal('txtCJRefDate');"><asp:Image id="btnSelDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrTransDate forecolor=red text="Date format " runat=server />
					<asp:label id=lblFmtTransDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			<tr>
				<td height=25>Supplier Code :*</td>
				<td>
                    <asp:TextBox ID="txtSupCode" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="15" Width="50%"></asp:TextBox>
                    <input id="Find" class="button-small" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                        type="button" value=" ... " />&nbsp;<asp:ImageButton ID="btnGet" runat="server" AlternateText="Get Data"
                            CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/icn_next.gif"
                          CssClass="button-small"   OnClick="GetSupplierBtn_Click" ToolTip="Click For Get Data" />
                    <asp:TextBox ID="txtSupName"
                                runat="server" BackColor="Transparent" BorderStyle="None" Font-Bold="True"
                                CssClass="fontObject" MaxLength="10" Width="99%"></asp:TextBox><asp:Label id=lblErrSupplier forecolor=Red text="Please select Supplier Code" runat=server/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Journal Type :*</td>
				<td><asp:RadioButton id=rbVoid text=" Void" autopostback=true oncheckedchanged=onCheck_Change groupname="journaltype" runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td><asp:RadioButton id=rbWriteOff text=" Write Off" autopostback=true oncheckedchanged=oncheck_change groupname="journaltype" runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>
					<asp:RadioButton id=rdAdjust text=" Adjustment" autopostback=true oncheckedchanged=oncheck_change groupname="journaltype" runat=server/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height="25">&nbsp;</td>
				<td>
					<asp:RadioButton id="rdAllocation" text=" Allocation" autopostback="true" oncheckedchanged="oncheck_change" groupname="journaltype" runat="server"></asp:RadioButton>
					<asp:Label id="lblNoJrnType" visible="false" forecolor="red" text="<br>Please select creditor Journal Type." runat="server"></asp:Label>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr><td colspan="6">&nbsp;</td></tr>
			<tr>
				<td colSpan="6">
					<!--<table width="100%" class="mb-c" cellspacing="0" cellpadding="0" border="0">
						<tr>						
							<td>-->
								<table id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server class="sub-Add">
									<tr class="mb-c">
										<td height=25>Accounting Period :</td>
										<td colSpan="5"><asp:DropDownList id=ddlAccMonth CssClass="fontObject"  size=1 width=10% runat=server ></asp:DropDownList>&nbsp;
											<asp:DropDownList id=ddlAccYear CssClass="fontObject"  size=1 width=10% onclick="fnBindAccMonth(document.frmMain.ddlAccMonth, this);" runat=server />&nbsp;
											<asp:ImageButton id=ibRefresh ImageAlign=AbsMiddle CausesValidation=False onclick=ibRefresh_Click ImageUrl="../../images/butt_refresh.gif" AlternateText=Refresh Runat=server />
										</td>
									</tr>
									<tr class="mb-c">
					                    <td colspan=6><asp:Label id=Label10 text="Please select one of these option:" Font-Bold=true runat=server /></td>
					                </tr>
									<tr class="mb-c">
										<td height=25 width=20%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1. <asp:label id=lblInvoiceRcvIDTag runat=server/> :</td>
										<td colSpan="5"><asp:DropDownList id=ddlInvoiceRcv CssClass="fontObject"  width=95% autopostback=true onSelectedIndexChanged=onSelect_InvPPNH runat=server /></td>
									</tr>
									<tr class="mb-c">
										<td width="20%" height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. Debit Note ID :</td>
										<td colSpan="5"><asp:DropDownList CssClass="fontObject"  width=95% id=ddlDebitNote runat=server /></td>
									</tr>
									<tr class="mb-c">
										<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. Credit Note ID :</td>
										<td colSpan="5"><asp:DropDownList CssClass="fontObject"  width=95% id=ddlCreditNote runat=server /></td>
									</tr>
									<tr class="mb-c">
										<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4. Payment ID :</td>
										<td colSpan="5"><asp:DropDownList CssClass="fontObject"  width=95% id=ddlPayment autopostback=true onSelectedIndexChanged=onSelect_PayPPNH runat=server /></td>
									</tr>
									<tr class="mb-c">
										<td height="25">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5. Creditor Journal ID :</td>
										<td colSpan="5"><asp:DropDownList CssClass="fontObject"  width="95%" id="ddlCreditorJournal" runat="server"></asp:DropDownList></td>
									</tr>
									<tr class="mb-c">
										<td height="25">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6. Goods Receive ID :</td>
										<td colSpan="5"><asp:DropDownList CssClass="fontObject"  width="95%" id="ddlGoodsRcv" runat="server"></asp:DropDownList></td>
									</tr>
									<tr class="mb-c">
										<td height=25></td>
										<td colSpan="5"><asp:Label id=lblErrNoSelectDoc forecolor=red visible=false text="Please select one document." runat=server/>
											<asp:Label id=lblErrManySelectDoc forecolor=red visible=false text="Please select ONLY one document." runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td style="height: 33px"><asp:label id=lblAccount runat=server /> :*</td>
										<td colSpan="5" style="height: 33px" valign="top">
                                            <asp:TextBox ID="txtAccCode" CssClass="fontObject"  runat="server" AutoPostBack="True" MaxLength="15" OnTextChanged="onSelect_StrAccCode"
                                                Width="22%"></asp:TextBox>
                                            <input id="btnFind1" class="button-small" runat="server" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                                                type="button" value=" ... " />&nbsp;<asp:Button ID="BtnGetData" 
                                                runat="server" class="button-small" Font-Bold="True"
                                                    OnClick="onSelect_Account" Text="Click Here" 
                                                ToolTip="Click For Refresh COA " Width="68px" />
                                            <asp:TextBox ID="txtAccName"  CssClass="fontObject"  runat="server" BackColor="Transparent" BorderStyle="None"
                                                Font-Bold="True" ForeColor="White" MaxLength="10" Width="64%"></asp:TextBox><asp:Label id=lblErrAccount visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr id="RowChargeLevel" class="mb-c">
										<td height="25">Charge Level :* </td>
										<td colSpan="5"><asp:DropDownList id="ddlChargeLevel" Width=95% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged  CssClass="fontObject" runat=server /> </td>
									</tr>
									<tr id="RowPreBlk" class="mb-c">
										<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
										<td colSpan="5"><asp:DropDownList id="ddlPreBlock"  Width=95%  CssClass="fontObject" runat=server />
											<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
									</tr>
									<tr id="RowBlk" class="mb-c">
										<td height=25><asp:label id=lblBlock runat=server /> :</td>
										<td colSpan="5"><asp:DropDownList id=ddlBlock    width=95%  CssClass="fontObject" runat=server/>
											<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id=lblVehicle runat=server /> :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlVehCode    width=95%  CssClass="fontObject" runat=server/>
											<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id=lblVehExpense runat=server /> :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlVehExpCode   width=95%  CssClass="fontObject"  runat=server/>
											<asp:Label id=lblErrVehicleExp visible=false forecolor=red runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25 width="20%">Amount :*</td>
										<td width="30%"><asp:TextBox id=txtAmount   width=60% maxlength=22  CssClass="fontObject" runat=server />
											<asp:RegularExpressionValidator id=ValidateAmount 
												ControlToValidate="txtAmount"
												ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 19 digits and 2 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrReqAmount visible=false forecolor=red text="Please enter amount." runat=server />
										</td>
										<TD width="5%"><asp:Label id=lblPPN text='PPN :' runat=server/></td>
										<TD width="15%"><asp:CheckBox ID=cbPPN Enabled=False text=" Yes" Runat=server />
										</TD>	
										<TD width="10%"><asp:Label id=lblPPH text='PPh 23/26 Rate :' runat=server/></td>
										<TD width="30%"><asp:Textbox id=txtPPHRate    Enabled=False width=50% maxlength=5  CssClass="fontObject" runat=server/>
										<asp:Label id=lblPercen text='%' runat=server/>
										</td>
									</tr>
									<tr class="mb-c">
										<td height=25 colspan=6>
											<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add onclick=AddBtn_Click UseSubmitBehavior="false" runat=server /> 
										&nbsp;</td>
									</tr>
								</table>
							<!--</td>
						</tr>
					</table>-->
				</td>
			</tr>
			<tr>
				<td colSpan="6">
					<asp:Label id=lblErrConfirmNotFulFil visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrConfirmNotFulFilText visible=false text="Unable to process one Creditor Journal line. This may caused by invalid document state, or document did not exist, or duplicate document id, or document outstanding amount exceeded total amount. Please verify the document " runat=server/>
				</td>
			</tr>
			<tr>
				<td colSpan="6">
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
						OnDeleteCommand=DEDR_Delete 
						Pagerstyle-Visible=False
						AllowSorting="True" class="font9Tahoma">
						
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
                                                      <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
						<Columns>						
							<asp:TemplateColumn HeaderText="Document ID">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("DocId") %> id="lblDocId" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Document Type">
								<ItemTemplate>
									<asp:Label Text=<%# objAPTrx.mtdGetPaymentDocType(Container.DataItem("DocType")) %> id="lblDocType" runat="server" />
									<asp:label text=<%#Container.DataItem("DocType")%> id="lblEnumDocType" visible=false runat=server/>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("VehExpenseCode") %> id="lblVehExpenseCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Doc. Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("NetAmount"), 2) %> id="lblViewNetAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("NetAmount"), 2) %> id="lblNetAmount" visible = False runat="server" />
																		
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Curr" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("CurrencyCode") %> id="lblCurrencyCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Rate" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("ExchangeRate"), 2)) %> id="lblIDPPHAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("ExchangeRate"), 2) %> id="lblExchangeRate" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2) %> id="lblViewAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" visible = False runat="server" />
									
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("InvoiceType") %> id="lblInvoiceType" visible=False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:label id=jrnlnid visible="false" text=<%# Container.DataItem("CreditJrnLnId")%> runat="server" />
									<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>	
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td vAlign="top">Remarks :</td>
				<td colSpan="5" vAlign="top"><asp:TextBox ID=txtRemark CssClass="font9Tahoma"  maxlength=256 width=100% Runat=server /></td>
			</tr>
			<tr>
				<td colSpan="6">&nbsp;</td>
			</tr>			
			<tr>
				<td colSpan="6">
					<asp:ImageButton ID=SaveBtn UseSubmitBehavior="false" onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=RefreshBtn UseSubmitBehavior="false" CausesValidation=False onclick=RefreshBtn_Click ImageUrl="../../images/butt_refresh.gif" AlternateText=Refresh Runat=server />
					<asp:ImageButton ID=ConfirmBtn UseSubmitBehavior="false" onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=PrintBtn UseSubmitBehavior="false" onclick=PrintBtn_Click ImageUrl="../../images/butt_print.gif" AlternateText=Print visible=false Runat=server />
					<asp:ImageButton ID=DeleteBtn UseSubmitBehavior="false" onclick=DeleteBtn_Click CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					<asp:ImageButton ID=UnDeleteBtn UseSubmitBehavior="false" onclick=UnDeleteBtn_Click ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete Runat=server />
					<asp:ImageButton ID=BackBtn UseSubmitBehavior="false" CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=cjid value="" runat=server />
				</td>
			</tr>
			
			
				
		    <tr>
			    <td colspan=5>&nbsp;&nbsp;&nbsp;&nbsp;</td>
		    </tr>
			
			
				
		    <tr>
			    <td colspan=5>&nbsp;</td>
		    </tr>
		    <tr id=TrLink runat=server>
			    <td colspan=5>
				    <asp:LinkButton id=lbViewJournal text="View Journal Predictions" causesvalidation=false runat=server /> 
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
					    AllowSorting="false" class="font9Tahoma">
					    <HeaderStyle CssClass="mr-h"/>
					    <ItemStyle CssClass="mr-l"/>
					    <AlternatingItemStyle CssClass="mr-r"/>
                                                      <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
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
			    </td>
		    </tr>	
		    <tr>
			    <td colspan=5>&nbsp;</td>
		    </tr>
		    <tr>
		        <td>&nbsp;</td>								
		        <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
		        <td>&nbsp;</td>	
		        <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
		        <td>&nbsp;</td>		
		        <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
	        </tr>
    	    
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /><asp:label id=lblCode visible=false text=" Code" runat=server /><asp:label id=lblID visible=false text=" ID" runat=server /><asp:label id=lblSelect visible=false text="Select " runat=server /><asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server /><asp:label id=lblInvoiceRcvTag visible=false runat=server/><asp:Label id=lblStatusHidden visible=false runat=server /><asp:Label id=lblJrnType visible=false runat=server /><asp:Label id=lblVehicleOption visible=false text=false runat=server/><asp:Label id=lblPPHRateHidden visible=false runat=server /><asp:Label id=lblPPNHidden visible=false runat=server /><asp:Label id=lblPPNAmtHidden visible=false runat=server /><asp:Label id=lblPPHAmtHidden visible=false runat=server /><asp:Label id=lblNetAmtHidden visible=false runat=server /><asp:Label id=lblInvTypeHidden visible=false runat=server /><asp:Label id=lblCurrencyHidden visible=false runat=server /><asp:Label id=lblRateHidden visible=false runat=server /></table>
		    <Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidPeriodData value="" runat=server/>
            <br />
            <asp:TextBox ID="txtPPN" CssClass="font9Tahoma"  runat="server" BackColor="Transparent" ForeColor="Transparent" BorderStyle="None"
                Width="9%"></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server" BackColor="Transparent"
                    BorderStyle="None" Width="9%"></asp:TextBox><asp:TextBox ID="txtPPNInit" ForeColor="Transparent" runat="server"
                        BackColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
