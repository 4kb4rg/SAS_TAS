<%@ Page Language="vb" trace="false" src="../../../include/cb_Trx_FundingDet.aspx.vb" Inherits="cb_Trx_FundingDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_cbtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
    <title>Bank Loan Details</title>
     <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
    </head>
	<%--<head>
		<title>Bank Loan Details</title>
       
	</head>--%>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=trxid text="" runat=server />
			<Input Type=Hidden id=trxlnid text="" runat=server />
			<Input Type=Hidden id=hidAccCode text="" runat=server />
			<Input Type=Hidden id=hidAmount text="0" runat=server />
			<Input Type=Hidden id=hidM_Amount text="0" runat=server />
			<Input Type=Hidden id=hidRate text="0" runat=server />
			<Input Type=Hidden id=hidStatEdit text="0" runat=server />
			<Input Type=Hidden id=hidStatEditLn text="0" runat=server />
			<Input Type=Hidden id=hidRowID text="0" runat=server />
			<Input Type=Hidden id=hidDescr text="" runat=server />
			
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                   <strong> BANK LOAN DETAILS </strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Period : <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;|Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;|Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By :<asp:Label id=lblUpdatedBy runat=server />
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
					<td height=25>Facility</td>
					<td width=30%><asp:DropDownList id=ddlTrxType width=100% runat=server>
								        <asp:ListItem value="0" Selected>All</asp:ListItem>
						                <asp:ListItem value="1">PRK</asp:ListItem>
						                <asp:ListItem value="2">Term Loan</asp:ListItem>
					                </asp:DropDownList>
					<asp:Label id=lblErrTrxType visible=false forecolor=red text=" Please select Type." runat=server />                
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Drawndown Date : </td>
					<td><asp:TextBox ID=txtTransDate maxlength=10 width=25% Runat=server />
					    <a href="javascript:PopCal('txtTransDate');"><asp:Image id="Image1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					    <asp:Label id=lblDateTrans forecolor=red Visible = false text="Date format " runat=server />
					    <asp:label id=lblFmtTrans  forecolor=red Visible = false Runat="server"/> 
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Bank : </TD>
				    <td><asp:DropDownList width=100% id=ddlAccCode runat=server />
					    <asp:Label id=lblErrAccCode forecolor=red visible=false text="Please select Bank Code"  runat=server/></td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>
					<td>&nbsp;</td>		
				    <td>&nbsp;</td>
			    </tr>
			    <tr>
				    <td height=25>Loan No. : </td>
					<td><asp:Textbox id=txtLoanNo width=100% runat=server/>
					    <asp:Label id=lblErrLoanNo visible=false forecolor=red text=" Please enter Loan No." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description : </td>
					<td><asp:Textbox id=txtDescription maxlength=256 width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Notes : </td>
					<td><asp:Textbox id=txtOtherID maxlength=256 width=100% runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
				    <td height=25>Amount : </td>
					<td><asp:Textbox id=txtAmount width=50% Text="0" style="text-align:right" runat=server/>
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
					<td>Amount :</td>
					<td><asp:Textbox id=lblAmount Width=50% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				 <tr>
				    <td height=25>Principal Repayment : </td>
					<td><asp:Textbox id=txtMonthlyAmount width=50% Text="0" style="text-align:right" runat=server/>
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
					<td><asp:Textbox id=lblMonthlyAmount Width=50% style="text-align:right" BorderStyle=None BackColor=transparent ForeColor=white ReadOnly=true runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Installment Date : </td>
					<td><asp:TextBox ID=txtInstallDate maxlength=10 width=25% Runat=server />
					    <a href="javascript:PopCal('txtInstallDate');"><asp:Image id="btnSelTransDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					    <asp:Label id=lblDateInstall forecolor=red Visible = false text="Date format " runat=server />
					    <asp:label id=lblFmtInstall  forecolor=red Visible = false Runat="server"/> 
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Periodic : </td>
					<td><asp:DropDownList id=ddlPeriodic width=50% runat=server>
				            <asp:ListItem value="1">Every 1 Month</asp:ListItem>
                            <asp:ListItem value="3">Every 3 Month</asp:ListItem>
                            <asp:ListItem value="6">Every 6 Month</asp:ListItem>
                            <asp:ListItem value="12">Every 12 Month</asp:ListItem>
					    </asp:DropDownList>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Interest : </td>
					<td><asp:Textbox id=txtRate width=25% Text="0" style="text-align:right" runat=server/>
					    <asp:label id=lblPercent text="%" Runat="server" />
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
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Duration : </td>
					<td><asp:DropDownList id=ddlDuration width=25% runat=server>
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
				    <td height=25>Total days in year : </td>
					<td><asp:Textbox id=txtTotDays width=25% Text="360" runat=server/>&nbsp;Days
					    <asp:Label id=lblErrTotDays visible=false forecolor=red text=" <br>Please enter total days in year." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Maturity Date : </td>
					<td><asp:TextBox ID=txtMaturityDate maxlength=10 width=25% Enabled=false Runat=server />
					    <a href="javascript:PopCal('txtMaturityDate');"><asp:Image id="Image2" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					    <asp:Label id=lblDateMaturity forecolor=red Visible = false text="Date format " runat=server />
					    <asp:label id=lblFmtMaturity  forecolor=red Visible = false Runat="server"/> 
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Remark : </td>
					<td><asp:Textbox id=txtRemark width=100% runat=server/></td>
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
					</td>
				</tr>
				<tr>
				    <td colspan=6>
 					            &nbsp;</td>
			    </tr>
			    <tr>
				    <td colSpan="6">
					    <asp:DataGrid id=dgLineDet
						    AutoGenerateColumns="false" width="100%" runat="server"
						    GridLines=Horizontal
						    Cellpadding="2"
						    OnEditCommand="DEDR_Edit"
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
						        <asp:TemplateColumn HeaderText="NO." ItemStyle-Width="2%">
								    <ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("RowID") %> id="lblRowID" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="PERIODE" ItemStyle-Width="5%">
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# objGlobal.GetLongDate(Container.DataItem("PayDate")) %> id="lblPayDate" runat="server" />
									    <asp:Label Text=<%# Container.DataItem("PayDate") %> id="lblIDPayDate" Visible=false runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="DAY OF DAYS" ItemStyle-Width="5%">
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# Container.DataItem("Days") %> id="lblDays" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" INTEREST PAYMENT" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_Rate"), 2), 2) %> id="lblM_Rate" runat="server" />
										    <asp:Label Text=<%# Container.DataItem("M_Rate") %> id="lblIDM_Rate" Visible=false runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" PRINCIPAL REPAYMENT" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_Amount"), 2), 2) %> id="lblM_Amount" runat="server" />
										    <asp:Label Text=<%# Container.DataItem("M_Amount") %> id="lblIDM_Amount" Visible=false runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText=" PRINCIPAL BALANCE" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-Font-Bold=true ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_BalanceAmount"), 2), 2) %> id="lblM_BalanceAmount" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" ACCRUED" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-Font-Bold=true ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_Accrued"), 2), 2) %> id="lblM_Accrued" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="INTEREST (%)" HeaderStyle-HorizontalAlign=center ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Center>
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("M_Interest"), 2), 2) %> id="lblM_Interest" runat="server" />
									    <asp:Label Text=<%# Container.DataItem("M_Interest") %> id="lblIDM_Interest" Visible=false runat="server" />
									    <asp:Label Text=<%# Container.DataItem("M_Periodic") %> id="lblIDM_Periodic" Visible=false runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>	
							    <asp:TemplateColumn HeaderText="INTEREST DAYS" HeaderStyle-HorizontalAlign=center ItemStyle-Width="3%" ItemStyle-HorizontalAlign=Center>
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# Container.DataItem("TotDays") %> id="lblTotDays" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>		
							    <asp:TemplateColumn>		
						        <ItemStyle width="3%" HorizontalAlign="center" />							
						            <ItemTemplate>
							            <asp:LinkButton id="lbEdit" CommandName="Edit" Text="Edit" CausesValidation =False Visible=true runat="server" />
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
                                                    </div>
            </td>
        </tr>
        </table>
		</form>
	</body>
</html>
