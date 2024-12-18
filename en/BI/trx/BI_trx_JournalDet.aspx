<%@ Page Language="vb" trace=false src="../../../include/BI_trx_JournalDet.aspx.vb" Inherits="BI_trx_JournalDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBI" src="../../menu/menu_bitrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Accounts Receivables - Debtor Journal Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu"  runat=server>

         <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

		<table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
        <tr>
        <td>

			<tr>
				<td colspan="6">
					<UserControl:MenuBI id=MenuBI runat="server" />
				</td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                              <strong> DEBTOR JOURNAL DETAILS </strong> </td>
                            <td class="font9Header"  style="text-align: right">
                                Period : | <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label ID=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label ID=lblUpdatedBy runat=server />
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 width="20%">Debtor Journal ID :</td>
				<td width="40%"><asp:Label id=lblDebtorJrnID runat=server /></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="20%">&nbsp;</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Transaction Date : </td>
				<td><asp:TextBox id=txtDateCreated width=25% maxlength=10 runat=server CssClass="font9Tahoma"/>
					<a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblDate forecolor=red text="Date format " runat=server />
					<asp:label id=lblFmtDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblBillPartyTag" runat="server" /> :*</td>
				<td><asp:DropDownList id=ddlBillParty width=100% AutoPostBack=true OnSelectedIndexChanged=onSelect_Change runat=server CssClass="font9Tahoma"/>
					<asp:RequiredFieldValidator id=rfvBillParty display=dynamic runat=server
						ControlToValidate=ddlBillParty /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblJrnTypeTag" text="Journal Type :*" runat="server" /></td>
				<td><asp:radiobutton id=rbVoid text=" Void" autopostback=true oncheckedchanged=onCheck_Change groupname="journaltype" runat=server/></td>
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
				<td><asp:RadioButton id=rbAdjust text=" Adjustment" autopostback=true oncheckedchanged=oncheck_change groupname="journaltype" runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td><asp:RadioButton id=rbAlloc text=" Allocation" autopostback=true oncheckedchanged=oncheck_change groupname="journaltype" runat=server/>
					<asp:Label id=lblNoJrnType visible=false forecolor=red text="<br>Please select journal type." runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>

                </td>
                </tr>

        </table>
            <table  cellSpacing="0" cellPadding="4" width="100%" border="0" runat=server>
            <tr>
            <td>

			<tr>
				<td colSpan="6">
					<!--<table width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center">
						<tr>						
							<td>-->
								<table id="tblSelection" cellSpacing="0" cellPadding="4" width="100%" border="0" runat=server class="sub-Add">
									<tr class="mb-c">
										<td height=25 width=20%>Accounting Period :</td>
										<td colSpan="5"><asp:dropdownlist id=ddlAccMonth size=1 width=10% runat=server CssClass="font9Tahoma">
												<asp:ListItem text="1" value="1" />
												<asp:ListItem text="2" value="2" />
												<asp:ListItem text="3" value="3" />
												<asp:ListItem text="4" value="4" />
												<asp:ListItem text="5" value="5" />
												<asp:ListItem text="6" value="6" />
												<asp:ListItem text="7" value="7" />
												<asp:ListItem text="8" value="8" />
												<asp:ListItem text="9" value="9" />
												<asp:ListItem text="10" value="10" />
												<asp:ListItem text="11" value="11" />
												<asp:ListItem text="12" value="12" />											
											</asp:DropDownList>&nbsp;
											<asp:dropdownlist id=ddlAccYear size=1 width=10% runat=server CssClass="font9Tahoma"/>&nbsp;
											<asp:ImageButton ID=RefreshBtn ImageAlign=AbsMiddle CausesValidation=False onclick=RefreshBtn_Click ImageUrl="../../images/butt_refresh.gif" AlternateText=Refresh Runat=server />											
										</td>
									</tr>
									<tr class="mb-c">
					                    <td colspan=6><asp:Label id=Label10 text="Please select one of these option:" Font-Bold=true runat=server /></td>
					                </tr>
									<tr class="mb-c">
										<td height=25 width=20%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1. Invoice ID :</td>
										<td colSpan="5"><asp:DropDownList id=ddlInvoice width=95% autopostback=true onSelectedIndexChanged=onSelect_InvPPNH runat=server CssClass="font9Tahoma"/></td>
									</tr>
									<tr class="mb-c">
										<td height=25 width=20%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. Debit Note ID :</td>
										<td colSpan="5"><asp:DropDownList id=ddlDebitNote width=95% runat=server CssClass="font9Tahoma"/></td>
									</tr>
									<tr class="mb-c">
										<td height=25 width=20%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3. Credit Note ID :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlCreditNote width=95% runat=server CssClass="font9Tahoma"/></td>
									</tr>
									<tr class="mb-c">
										<td height=25 width=20%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4. Receipt ID :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlReceipt width=95% autopostback=true onSelectedIndexChanged=onSelect_RecPPNH runat=server CssClass="font9Tahoma"/>
											<asp:Label id=lblErrReceipt visible=false text="You must select a Receipt." forecolor=red runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25 width=20%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;5. Debtor Journal ID :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlDebtorJrn width=95% runat=server CssClass="font9Tahoma"/></td>
									</tr>
									<tr class="mb-c">
										<td height=25 width=20%>&nbsp;</td>
										<td colSpan="5"><asp:Label id=lblErrNoSelectDoc forecolor=red visible=false text="Please select one document." runat=server/>										
											<asp:Label id=lblErrManySelectDoc visible=false text="Please select ONLY one document." forecolor=red runat=server /></td>
									</tr>									
									<tr class="mb-c">
										<td height=25 width=20%><asp:label id="lblAccCodeTag" runat="server" /> :*</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlAccount width=95% onselectedindexchanged=onSelect_Account autopostback=true runat=server CssClass="font9Tahoma"/>
											<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','ddlAccount','6',(hidBlockCharge.value==''? 'ddlBlock': 'ddlPreBlock'),'','','','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
											<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/></td>
									</tr>
									<tr id="RowChargeLevel" class="mb-c">
										<td height="25" width=20%>Charge Level :* </td>
										<td colspan=5><asp:DropDownList id="ddlChargeLevel" Width=95% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server CssClass="font9Tahoma"/> </td>
									</tr>
									<tr id="RowPreBlk" class="mb-c">
										<td height="25"  width=20%><asp:label id=lblPreBlkTag Runat="server"/> </td>
										<td colspan=5><asp:DropDownList id="ddlPreBlock" Width=95% runat=server CssClass="font9Tahoma"/>
											<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
									</tr>
									<tr id="RowBlk" class="mb-c">
										<td height=25 width=20%><asp:label id="lblBlkCodeTag" runat="server" /> :</td>
										<td colSpan="5"><asp:Dropdownlist id=ddlBlock width=95% runat=server CssClass="font9Tahoma"/>
											<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25 width=20%>Amount :*</td>
										<td width=30%>
											<asp:Textbox id=txtAmount width=70% maxlength=22 runat=server CssClass="font9Tahoma"/>
											<asp:CompareValidator id="cvAmount" display=dynamic runat="server" 
												ControlToValidate="txtAmount" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>
											<asp:RegularExpressionValidator id=revAmount 
												ControlToValidate="txtAmount"
												ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 19 digits and 2 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrAmount visible=false text="Amount is invalid." forecolor=red runat=server/>
											<asp:Label id=lblErrAmountZero visible=false text="Amount cannot be zero." forecolor=red runat=server/>
											<asp:Label id=lblErrReqAmount visible=false text="Please enter Amount." forecolor=red runat=server/>
										</td>
										<TD width="5%"><asp:Label id=lblPPN text='PPN :' runat=server/></td>
										<TD width="15%"><asp:CheckBox ID=cbPPN Enabled=False text=" Yes" Runat=server />
										</TD>	
										<TD width="10%"><asp:Label id=lblPPH text='PPh 23/26 Rate :' runat=server/></td>
										<TD width="30%"><asp:Textbox id=txtPPHRate Enabled=False width=50% maxlength=5 runat=server CssClass="font9Tahoma"/>
										<asp:Label id=lblPercen text='%' runat=server/>
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
			<tr class ="font9Tahoma">
				<td colSpan="6">
				    <asp:Label id=lblErrAction forecolor=red visible=false Text="" runat=server />
					<asp:Label id=lblErrConfirmNotFulFil visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrConfirmNotFulFilText visible=false text="Unable to process one Debtor Journal line. This may caused by invalid document state, or document did not exist, or duplicate document id, or document outstanding amount exceeded total amount. Please verify the document " runat=server/>
					<asp:Label id=lblErrNoDocID text="Document not found!" visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrDupDoc text="Duplicate document found!" visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrNoRec text="Transaction document not found!" visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrExceed text="Amount entered exceeded Total Amount in transaction!" visible=false forecolor=red runat=server/>
					<asp:Label id=lblErrTotal forecolor=red visible=false text="Total amount cannot be zero." runat=server/>
					<asp:Label id=lblErrConfirmAllocType forecolor=red visible=false Text="Cannot confirm Debtor Journal if Total Amount is not equal to zero." runat=server /><br>
					<asp:Label id=lblErrConfirmTotalAmt forecolor=red visible=false Text="Cannot confirm Debtor Journal if Total Amount is less than zero." runat=server />
				</td>
			</tr>								
			<tr>
				<td colSpan="6">
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
						Pagerstyle-Visible=False
						AllowSorting="True" class="font9Tahoma"
                        OnItemDataBound="dgLine_BindGrid"
						OnDeleteCommand="dgLineDet_OnDeleteCommand">
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
							<asp:TemplateColumn HeaderText="Document ID" ItemStyle-Width=12%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("DocID") %> id="lblDocID" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Document Type" ItemStyle-Width=12%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("DocType") %> id="lblDocType" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width=12%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width=12%>
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Amount" ItemStyle-Width=12% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("NetAmount"), 0)) %> id="lblViewNetAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("NetAmount"), 2) %> id="lblNetAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="PPN" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("PPNAmount"), 0)) %> id="lblViewPPNAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("PPNAmount"), 2) %> id="lblPPNAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="PPH 23/26" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<ItemStyle />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("PPHAmount"), 0)) %> id="lblViewPPHAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("PPHAmount"), 2) %> id="lblPPHAmount" visible = False runat="server" />
									</ItemStyle>
								</ItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn HeaderText="Total Amount" ItemStyle-Width=12% HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Amount"), 0)) %> id="lblViewAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" visible = False runat="server" />
									<asp:label id=lblLnID visible="false" text=<%# Container.DataItem("DebtorJrnLnId")%> runat="server" />
									<asp:label id=lblAmt visible="false" text=<%# Container.DataItem("Amount")%> runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
                            <asp:TemplateColumn>
								<ItemStyle HorizontalAlign="right" Width="5%"/>
								<ItemTemplate>
									<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=3 height=25><hr style="width :100%" /></td>
			</tr>		
			<tr class="font9Tahoma">
				<td colspan=3>&nbsp;</td>
				<td height=25>Total Amount : </td>
				<td colspan=2 Align=right>
				<asp:Label ID=lblViewTotalAmount Runat=server CssClass="font9Tahoma"/>
				<asp:Label ID=lblTotalAmount Visible = False Runat=server CssClass="font9Tahoma"/>&nbsp;</td>
			</tr>						
			<tr>
				<td height=25>Remarks:</td>
				<td colSpan="5"><asp:TextBox ID=txtRemark maxlength=256 width=100% Runat=server CssClass="font9Tahoma"/></td>
			</tr>
			<tr>
				<td colSpan="6">
				</td>	
			</tr>
			<tr>
				<td colSpan="6">
					<asp:ImageButton ID=SaveBtn CausesValidation=true onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 					
					<asp:ImageButton ID=ConfirmBtn CausesValidation=False onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=DeleteBtn CausesValidation=False onclick=DeleteBtn_Click ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />			
					<asp:ImageButton ID=BackBtn CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=hidDebtorJrnID value="" runat=server />
					<Input type=hidden id=hidStatus value="" runat=server />
				    <br />
				</td>
			</tr>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:Label id=lblAccount visible=false runat=server />
			<asp:Label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:Label id=lblSelect visible=false text="Select " runat=server />
			<asp:Label id=lblJrnType visible=false runat=server />
			<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
			<asp:Label id=lblPPHRateHidden visible=false runat=server />
			<asp:Label id=lblPPNHidden visible=false runat=server />
			<asp:Label id=lblPPNAmtHidden visible=false runat=server />
			<asp:Label id=lblPPHAmtHidden visible=false runat=server />
			<asp:Label id=lblNetAmtHidden visible=false runat=server />
			
			
				
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
            
                    </td>
            </tr>
		</TABLE>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
            </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
