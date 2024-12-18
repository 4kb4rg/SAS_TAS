<%@ Page Language="vb" src="../../../include/RC_trx_DADet.aspx.vb" Inherits="RC_trx_DADet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuRC" src="../../menu/menu_rctrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Reconciliation Dispatch Advice Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function calAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyDisp.value);
				var b = parseFloat(doc.txtCost.value);
				doc.txtAmount.value = a * b;
				if (doc.txtAmount.value == 'NaN')
					doc.txtAmount.value = '';
			}
		</script>		
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



		<TABLE id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
			<tr>
				<td colspan="5">
					<UserControl:MenuRC id=MenuRC runat="server" />
				</td>
			</tr>
			<tr>
				<td class="mt-h" colspan="5">RECONCILIATION DISPATCH ADVICE DETAILS</td>
			</tr>
			<tr>
				<td colspan=5><hr size="1" noshade></td>
			</tr>
			<TR>
				<TD vAlign="top" height=25 width="20%">Dispatch Advice ID :</TD>
				<TD vAlign="top" width="30%"><asp:Label id=lblDispatchAdviceID runat=server /></TD>
				<TD width="2%"></TD>
				<TD vAlign="top" width="20%">Account Period:</TD>
				<TD vAlign="top" width="28%"><asp:Label id=lblAccPeriod runat=server /></TD>
			</TR>
			<TR>
				<TD vAlign="top" height=25>Dispatch Advice Ref No :</TD>
				<TD vAlign="top"><asp:Textbox id=txtDispatchAdviceRefNo width=100% maxlength=32 runat=server/></TD>
				<TD></TD>
				<TD vAlign="top" width="15%">Status:</TD>
				<TD vAlign="top" width="33%"><asp:Label id=lblStatus runat=server /></TD>
			</TR>
			<TR>
				<TD vAlign="top" height=25>Dispatch Advice Ref Date :</TD>
				<TD vAlign="top">
					<asp:Textbox id=txtDispatchAdviceRefDate width=50% maxlength=10 runat=server/> 
					<a href="javascript:PopCal('txtDispatchAdviceRefDate');"><asp:Image id="btnSelPlantDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrRefDate visible=false forecolor=red text="Date Format " runat=server/>
				</TD>
				<TD></TD>
				<TD vAlign="top">Date Created:</TD>
				<TD vAlign="top"><asp:Label id=lblDateCreated runat=server /></TD>
			</TR>
			<TR>
				<TD vAlign="top" height=25>To <asp:label id=lblLocation runat=server /> :*</TD>
				<TD vAlign="top">
					<asp:DropDownList id=ddlLocation width=100% runat=server/>
					<asp:Label id=lblErrLocation visible=false forecolor=red runat=server/>
				</TD>
				<TD></TD>
				<TD vAlign="top">Last Update:</TD>
				<TD vAlign="top"><asp:Label ID=lblLastUpdate runat=server /></TD>
			</TR>
			<TR>
				<TD vAlign="top" height=25><asp:Label id=lblDocType Text="Dispatch Advice Type : " runat=server/></TD>
				<TD vAlign="top"><asp:Label id=lblDocTypeValue runat=server/></TD>
				<TD></TD>
				<TD vAlign="top">Print Date:</TD>
				<TD vAlign="top"><asp:Label ID=lblPrintDate runat=server /></TD>
			</TR>
			<TR>
				<TD vAlign="top" height=25></TD>
				<TD vAlign="top"></TD>
				<TD></TD>
				<TD vAlign="top">Updated By:</TD>
				<TD vAlign="top"><asp:Label ID=lblUpdatedBy runat=server /></TD>
			</TR>
            </table>

            <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
			<TR>
				<TD colSpan="5" vAlign="top">
					<table width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" class="font9Tahoma">
						<tr>						
							<td>
								<TABLE id="tblSelection" cellSpacing="0" cellPadding="1" width="100%" border="0" runat=server class="font9Tahoma">
									<TR class="mb-c">
										<TD vAlign="top" height=25><asp:label id=lblStockItem runat=server /> code :*</TD>
										<TD vAlign="top" colspan=5>
											<asp:DropDownList id=ddlItemCode width=90% runat=server/> 
											<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','1','ddlItemCode','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
											<asp:Label id=lblErrItemCode visible=false forecolor=red text="<br>Select either stock item or direct charge." runat=server/>
										</TD>
									</TR>
									<TR class="mb-c">
										<TD vAlign="top" height=25><asp:label id=lblDirectChgItem runat=server /> code :*</TD>
										<TD vAlign="top" colspan=5>
											<asp:DropDownList id=ddlDirectCode width=90% runat=server/> 
											<input type="button" id=btnFind2 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','ddlDirectCode','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
											<asp:Label id=lblErrDirectCode visible=false forecolor=red runat=server/>
										</TD>
									</TR>
									<TR class="mb-c">
										<TD vAlign="top" height=25>Purchase Request ID :</TD>
										<TD vAlign="top" colspan=5>
											<asp:Textbox id=txtPRID width=30% maxlength=20 runat=server/>
											<asp:Label id=lblErrPR visible=false forecolor=red runat=server/>
										</TD>
									</TR>
									<tr class="mb-c">
										<td height=25 width=20%>Quantity Dispatch :*</td>
										<td width=20%>
											<asp:Textbox id=txtQtyDisp OnKeyUp="javascript:calAmount();" width=100% maxlength=21 runat=server/>
											<asp:CompareValidator id="cvTotalUnits" display=dynamic runat="server" 
												ControlToValidate="txtQtyDisp" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>
											<asp:RegularExpressionValidator id=revTotalUnits 
												ControlToValidate="txtQtyDisp"
												ValidationExpression="^(\-|)\d{1,15}(\.\d{1,5}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 15 digits and 5 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrTotalUnits visible=false forecolor=red text="<br>Please enter quantity dispatch." runat=server/>
										</td>
										<td height=25 width=10% align=center> X Cost *</td>
										<td width=20%>
											<asp:Textbox id=txtCost width=100% OnKeyUp="javascript:calAmount();" maxlength=21 runat=server/>
											<asp:CompareValidator id="cvRate" display=dynamic runat="server" 
												ControlToValidate="txtCost" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>
											<asp:RegularExpressionValidator id=revRate 
												ControlToValidate="txtCost"
												ValidationExpression="^(\-|)\d{1,15}(\.\d{1,5}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 15 digits and 5 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrRate visible=false forecolor=red text="<br>Please enter cost." runat=server/>
										</td>
										<td height=25 width=10% align=center> = Amount *</td>
										<td width=20%>
											<asp:Textbox id=txtAmount width=100% maxlength=21 runat=server/>
											<asp:CompareValidator id="cvAmount" display=dynamic runat="server" 
												ControlToValidate="txtAmount" Text="<br>The value must whole number or with decimal. " 
												Type="Double" Operator="DataTypeCheck"/>
											<asp:RegularExpressionValidator id=revAmount 
												ControlToValidate="txtAmount"
												ValidationExpression="^(\-|)\d{1,15}(\.\d{1,5}|\.|)$"
												Display="Dynamic"
												text = "Maximum length 15 digits and 5 decimal points. "
												runat="server"/>
											<asp:Label id=lblErrAmount visible=false forecolor=red text="<br>Please enter the Amount." runat=server/>
										</td>
									</tr>
									<TR class="mb-c">
										<TD vAlign="top" height=25 colspan=2>
											<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add onclick=AddBtn_Click runat=server /> 
										</TD>
									</TR>
								</TABLE>
							</td>
						</tr>
					</table>
				</TD>
			</TR>
            </table>

            <table style="width: 100%" class="font9Tahoma">
			<TR>
				<TD colSpan="5" vAlign="top">
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
						OnDeleteCommand=DEDR_Delete 
						Pagerstyle-Visible=False
						AllowSorting="True"
                            class="font9Tahoma">	
							 
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
							<asp:TemplateColumn HeaderText="Item Code">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("ItemCode") %> id="lblItemCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Purchase Request ID">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("PRID") %> id="lblPRID" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Quantity Dispatched" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# FormatNumber(Container.DataItem("QtyDisp"), 2) %> id="lblQtyDisp" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Cost" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Cost"), 2) %> id="lblCost" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
									<asp:label id=dalnid visible="false" text=<%# Container.DataItem("DispAdvLnID")%> runat="server" />
									<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>	
						</Columns>
					</asp:DataGrid>
				</TD>
			</TR>
			<TR>
				<TD></TD>
				<TD></TD>
				<TD></TD>
				<TD colspan=2 height=25><hr size="1" noshade></TD>
			</TR>
			<TR>
				<TD></TD>
				<TD></TD>
				<TD></TD>
				<TD vAlign="top" height=25>Total Amount :</TD>
				<TD vAlign="top" Align=right><asp:Label ID=lblTotalAmount Runat=server />&nbsp;</TD>
			</TR>
			<TR>
				<TD colSpan="5">&nbsp;</TD>
			</TR>			
			<TR>
				<TD colSpan="5">
					<asp:Label id=lblErrTotal forecolor=red visible=false text="Zero amount cannot be dispatched.<br>" runat=server/>
					<asp:ImageButton ID=SaveBtn onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=ConfirmBtn onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=PrintBtn onClick=btnPreview_Click ImageUrl="../../images/butt_print.gif" AlternateText=Print Runat=server />
					<asp:ImageButton ID=CancelBtn onclick=CancelBtn_Click ImageUrl="../../images/butt_cancel.gif" Text=" Cancel " Runat=server />
					<asp:ImageButton ID=DeleteBtn CausesValidation=False onclick=DeleteBtn_Click ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					<asp:ImageButton ID=BackBtn CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=daid value="" runat=server />
				</TD>
			</TR>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:Label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:Label id=lblSelect visible=false text="Select " runat=server />
			<asp:Label id=lblSelectEither visible=false text="Please select either " runat=server />
			<asp:Label id=lblOr visible=false text=" or " runat=server />
			<asp:Label id=lblStatusHidden visible=false runat=server />
			<asp:Label id=lblDocTypeHidden visible=false runat=server />
			<asp:Label id=lblReferer visible=false text="" runat=server/>
			<TR>
				<TD colSpan="5">
                                            &nbsp;</TD>
			</TR>
			</TABLE>
			<Input type=hidden id=hidBlockCharge value="BC" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="CLC" runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
