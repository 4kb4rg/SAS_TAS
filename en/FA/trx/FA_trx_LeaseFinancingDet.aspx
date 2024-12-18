<%@ Page Language="vb" trace="false" src="../../../include/FA_trx_LeaseFinancingDet.aspx.vb" Inherits="FA_trx_LeaseFinancingDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_fatrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Lease/Financing Payment Schedule Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    </head>
	<script language="javascript">	
	        function calTtlAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtAmount.value);
				var b = parseFloat(doc.txtDiscount.value);				
				var c = parseFloat(doc.txtDPAmount.value);	
				var d = parseFloat(doc.txtInsurance.value);	
				doc.txtTotalAmount.value = a-b-c+d;
				if (doc.txtTotalAmount.value == 'NaN')
					doc.txtTotalAmount.value = '';
				else
					doc.txtTotalAmount.value = round(doc.txtTotalAmount.value, 2);
			}			
	</script>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
                  <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=trxid text="" runat=server />
			<Input Type=Hidden id=trxlnid text="" runat=server />
			<Input Type=Hidden id=hidAmount text="0" runat=server />
			<Input Type=Hidden id=hidDPAmount text="0" runat=server />
			<Input Type=Hidden id=hidDiscount text="0" runat=server />
			<Input Type=Hidden id=hidInsurance text="0" runat=server />
			<Input Type=Hidden id=hidTotalAmount text="0" runat=server />
			<Input Type=Hidden id=hidM_Amount text="0" runat=server />
			<Input Type=Hidden id=hidRate text="0" runat=server />
			<Input Type=Hidden id=hidDescr text="" runat=server />
			
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6">
                        <table cellpadding="0" cellspacing="0" class="font9Tahoma" style="width: 100%" >
                            <tr>
                                <td class="font9Tahoma">
                                <strong>   LEASE/FINANCING PAYMENT SCHEDULE DETAILS</strong></td>
                                <td class="font9Header"  style="text-align: right">
                                    Period : <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
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
				    <td height=25 width="20%">Transaction ID :</td>
				    <td width="30%"><asp:Label id=lblTrxID runat=server /></td>
				    <td width="5%">&nbsp;</td>
				    <td width="15%">&nbsp;</td>
				    <td width="25%">&nbsp;</td>
				    <td width="5%"></td>
			    </tr>
				<tr>
					<td height=25>Type</td>
					<td width=30%><asp:DropDownList id=ddlTrxType width=100% runat=server CssClass="font9Tahoma" >
								        <asp:ListItem value="0" Selected>All</asp:ListItem>
						                <asp:ListItem value="1">Lease</asp:ListItem>
						                <asp:ListItem value="2">Financing</asp:ListItem>
					                </asp:DropDownList>
					<asp:Label id=lblErrTrxType visible=false forecolor=red text=" Please select Type." runat=server />                
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Transaction Date : </td>
					<td><asp:TextBox ID=txtTransDate maxlength=10 width=50% Runat=server CssClass="font9Tahoma" />
					    <a href="javascript:PopCal('txtTransDate');"><asp:Image id="Image1" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					    <asp:Label id=lblDateTrans forecolor=red Visible = false text="Date format " runat=server />
					    <asp:label id=lblFmtTrans  forecolor=red Visible = false Runat="server"/> 
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Lessor : </td>
					<td><asp:TextBox ID=txtLessor maxlength=256 width=100% Runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description : </td>
					<td><asp:DropDownList width=100% id=ddlAccCode runat=server CssClass="font9Tahoma" />
					    <asp:Label id=lblErrAccCode forecolor=red visible=false text="Please select Account Code"  runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Additional Note : </td>
					<td><asp:Textbox id=txtDescription maxlength=256 width=100% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Reference No. : </td>
					<td><asp:Textbox id=txtRefNo maxlength=256 width=100% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
				    <td height=25>Amount : </td>
					<td><asp:Textbox id=txtAmount width=50% Text="0" style="text-align:right" OnKeyUp="javascript:calTtlAmount();" runat=server CssClass="font9Tahoma"/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator4 												
                            ControlToValidate="txtAmount"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" 
                            display="dynamic" 
                            ControlToValidate="txtAmount"
                            Text="Please enter amount" />
                        <asp:Label id=lblErrAmount visible=false forecolor=red text=" Please enter amount." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Total Harga Barang :</td>
					<td><asp:Textbox id=lblAmount Width=40% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true Enabled=false runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Discount : </td>
					<td><asp:Textbox id=txtDiscount width=50% Text="0" style="text-align:right" OnKeyUp="javascript:calTtlAmount();" runat=server CssClass="font9Tahoma"/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator5 												
                            ControlToValidate="txtDiscount"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" 
                            display="dynamic" 
                            ControlToValidate="txtDiscount"
                            Text="Please enter discount" />
                        <asp:Label id=lblErrDiscount visible=false forecolor=red text=" Please enter discount." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Discount :</td>
					<td><asp:Textbox id=lblDiscount Width=40% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true Enabled=false runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Down Payment : </td>
					<td><asp:Textbox id=txtDPAmount width=50% Text="0" style="text-align:right" OnKeyUp="javascript:calTtlAmount();" runat=server CssClass="font9Tahoma"/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator6 												
                            ControlToValidate="txtDPAmount"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" 
                            display="dynamic" 
                            ControlToValidate="txtDPAmount"
                            Text="Please enter down payment" />
                        <asp:Label id=lblErrDPAmount visible=false forecolor=red text=" Please enter down payment." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Uang Muka :</td>
					<td><asp:Textbox id=lblDPAmount Width=40% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true Enabled=false runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Insurance : </td>
					<td><asp:Textbox id=txtInsurance width=50% Text="0" style="text-align:right" OnKeyUp="javascript:calTtlAmount();" runat=server CssClass="font9Tahoma"/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator7 												
                            ControlToValidate="txtInsurance"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" 
                            display="dynamic" 
                            ControlToValidate="txtInsurance"
                            Text="Please enter insurance" />
                        <asp:Label id=lblErrInsurance visible=false forecolor=red text=" Please enter insurance." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Asuransi :</td>
					<td><asp:Textbox id=lblInsurance Width=40% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true Enabled=false runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Outstanding Amount : </td>
					<td><asp:Textbox id=txtTotalAmount width=50% Text="0" style="text-align:right" ReadOnly=true runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>Total Hutang :</td>
					<td><asp:Textbox id=lblTotalAmount Width=40% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true Enabled=false runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td height=25>Monthly Payment : </td>
					<td><asp:Textbox id=txtMonthlyAmount width=50% Text="0" style="text-align:right" runat=server CssClass="font9Tahoma"/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator1 												
                            ControlToValidate="txtMonthlyAmount"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                            display="dynamic" 
                            ControlToValidate="txtMonthlyAmount"
                            Text="Please enter monthly payment" />
                        <asp:Label id=lblErrMonthlyAmount visible=false forecolor=red text=" Please enter monthly payment." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Angsuran :</td>
					<td><asp:Textbox id=lblMonthlyAmount Width=40% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true Enabled=false runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Installment Date : </td>
					<td><asp:TextBox ID=txtInstallDate maxlength=10 width=50% Runat=server CssClass="font9Tahoma"/>
					    <a href="javascript:PopCal('txtInstallDate');"><asp:Image id="btnSelTransDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					    <asp:Label id=lblDateInstall forecolor=red Visible = false text="Date format " runat=server />
					    <asp:label id=lblFmtInstall  forecolor=red Visible = false Runat="server"/> 
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Duration : </td>
					<td><asp:DropDownList id=ddlDuration width=30% runat=server CssClass="font9Tahoma"> 
				            <asp:ListItem value="1">1</asp:ListItem>
                            <asp:ListItem value="2">2</asp:ListItem>
                            <asp:ListItem value="3">3</asp:ListItem>
                            <asp:ListItem value="4">4</asp:ListItem>
                            <asp:ListItem value="5">5</asp:ListItem>
                            <asp:ListItem value="6">6</asp:ListItem>
                            <asp:ListItem value="7">7</asp:ListItem>
                            <asp:ListItem value="8">8</asp:ListItem>
                            <asp:ListItem value="9">9</asp:ListItem>
                            <asp:ListItem value="10">10</asp:ListItem>
					    </asp:DropDownList>&nbsp;Tahun
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
					<td class="mt-h">Interest</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>From Leasing : </td>
					<td><asp:Textbox id=txtRateLease width=30% Text="0" style="text-align:right" runat=server CssClass="font9Tahoma"/>
					    <asp:label id=lblPercent2 text="%" Runat="server" />
                        <asp:RegularExpressionValidator id=RegularExpressionValidator3 												
                            ControlToValidate="txtRateLease"												
                            ValidationExpression="\d{1,2}\.\d{0,12}|\d{1,2}"
                            Display="Dynamic"
                            text="Maximum length 2 digits and 12 decimal points."
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" 
                            display="dynamic" 
                            ControlToValidate="txtRateLease"
                            Text="Please enter rate" />
                        <asp:Label id=lblErrRate2 visible=false forecolor=red text=" Please enter rate." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>For Calculating : </td>
					<td><asp:Textbox id=txtRate width=30% Text="0" style="text-align:right" runat=server CssClass="font9Tahoma"/>
					    <asp:label id=lblPercent1 text="%" Runat="server" />
                        <asp:RegularExpressionValidator id=RegularExpressionValidator2 												
                            ControlToValidate="txtRate"												
                            ValidationExpression="\d{1,2}\.\d{0,12}|\d{1,2}"
                            Display="Dynamic"
                            text="Maximum length 2 digits and 12 decimal points."
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" 
                            display="dynamic" 
                            ControlToValidate="txtRate"
                            Text="Please enter rate" />
                        <asp:Label id=lblErrRate visible=false forecolor=red text=" Please enter rate." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Rounding : </td>
					<td><asp:DropDownList id=ddlRounding width=30% runat=server CssClass="font9Tahoma" >
			                <asp:ListItem value="1">Yes</asp:ListItem>
			                <asp:ListItem value="2">No</asp:ListItem>
		                </asp:DropDownList></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>COA Interest : </td>
					<td><asp:DropDownList width=100% id=ddlRateCOA runat=server CssClass="font9Tahoma"  /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
				    <td height=25>Remark : </td>
					<td><asp:Textbox id=txtRemark width=100% runat=server CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
					<td colspan="6">
					    <asp:ImageButton id=NewBtn AlternateText="  New  " imageurl="../../images/butt_new.gif" onclick=NewBtn_Click CommandArgument=New runat=server />
					    <asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=ConfirmBtn AlternateText="Confirm" imageurl="../../images/butt_confirm.gif" onclick=Button_Click CommandArgument=Confirm runat=server />
						<asp:ImageButton id=CancelBtn AlternateText="Cancel" imageurl="../../images/butt_cancel.gif" onclick=Button_Click CommandArgument=Cancel runat=server />
						<asp:ImageButton ID=DownloadBtn UseSubmitBehavior="false" onclick=DownloadBtn_Click CausesValidation=false ImageUrl="../../images/butt_download.gif" AlternateText=Print Runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td colSpan="6">
					    <asp:DataGrid id=dgLineDet
						    AutoGenerateColumns="false" width="100%" runat="server"
						    GridLines=Both
						    Cellpadding="2"
						    Pagerstyle-Visible="False"
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
						        <asp:TemplateColumn HeaderText="NO" ItemStyle-Width="3%">
								    <ItemTemplate>
									   <asp:Label Text=<%# Container.DataItem("RowID") %> id="lblRowID" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="PERIODE" ItemStyle-Width="5%">
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# objGlobal.GetLongDate(Container.DataItem("PayDate")) %> id="lblPayDate" runat="server" />
									</ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" POKOK" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_Amount"), 2), 2) %> id="lblM_Amount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" BUNGA" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_Rate"), 2), 2) %> id="lblM_Rate" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText=" TOTAL ANGSURAN" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_TotalAmount"), 2), 2) %> id="lblM_TotalAmount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" SALDO HUTANG" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-Font-Bold=true ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_BalanceAmount"), 2), 2) %> id="lblM_BalanceAmount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>						
                                <asp:TemplateColumn HeaderText=" POKOK" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("H_Amount"), 2), 2) %> id="lblH_Amount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>		    								
							    <asp:TemplateColumn HeaderText=" SALDO HUTANG" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-Font-Bold=true ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("H_BalanceAmount"), 2), 2) %> id="lblH_BalanceAmount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>		    								
							    <asp:TemplateColumn HeaderText=" POKOK" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("I_Amount"), 2), 2) %> id="lblI_Amount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>		    								
							    <asp:TemplateColumn HeaderText=" SALDO HUTANG" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-Font-Bold=true ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("I_BalanceAmount"), 2), 2) %> id="lblI_BalanceAmount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>		    								
					    </Columns>										
					    </asp:DataGrid>
				    </td>
			    </tr>
    			
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				
			</table>
                                    </td>
            </tr>
            </table>
		</form>
	</body>
</html>
