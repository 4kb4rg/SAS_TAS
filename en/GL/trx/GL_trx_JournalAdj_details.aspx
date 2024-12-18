<%@ Page Language="vb" trace="False" src="../../../include/GL_Trx_JournalAdj_Details.aspx.vb" Inherits="GL_Trx_JournalAdj_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Journal Adjustment Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
			function calDRAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQty.value);
				var b = parseFloat(doc.txtPrice.value);
				doc.txtDRTotalAmount.value = a * b;
				if (doc.txtDRTotalAmount.value == 'NaN')
					doc.txtDRTotalAmount.value = '';
				else
					doc.txtDRTotalAmount.value = round(doc.txtDRTotalAmount.value, 2);
			}

			function calCRAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyCR.value);
				var b = parseFloat(doc.txtPriceCR.value);
				doc.txtCRTotalAmount.value = a * b;
				if (doc.txtCRTotalAmount.value == 'NaN')
					doc.txtCRTotalAmount.value = '';
				else
					doc.txtCRTotalAmount.value = round(doc.txtCRTotalAmount.value, 2);
			}
		</script>		
	    <style type="text/css">
            .style1
            {
                height: 19px;
            }
            .style2
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
	
	<body >
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

		<asp:label id="lblStsHid" Visible="False" Runat="server"/>
		<asp:label id="blnShortCut" Visible="False" Runat="server"/>
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id="blnUpdate" Visible="False" Runat="server"/>
		<asp:label id=lblTxLnID visible=false runat=server />
		<table border=0 width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma"> 
			<tr>
				<td colspan=6><UserControl:MenuGLTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>
                    <table cellpadding="0" cellspacing="0" class="style2">
                        <tr>
                            <td class="font9Tahoma">
                              <strong>  JOURNAL ADJUSTMENT DETAILS</strong></td>
                            <td class="font9Header"  style="text-align: right">
                                Status : <asp:Label id=Status runat=server />&nbsp;| Date Created : Date Created : 
                                | Last Update : <asp:Label id=UpdateDate runat=server />&nbsp;| Updated By : <asp:Label id=UpdateBy runat=server />&nbsp;| Print Date : <asp:Label id=lblPrintDate runat=server /></td>
                        </tr>
                    </table>
                     <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td colspan=6 class="style1">&nbsp;</td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False 
                        forecolor=Red runat=server style="text-align: right" />
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
			</tr>
			<tr>
				<td width="20%" height=25>Journal Adjustment ID :</td>
				<td width="40%"><asp:label id=lblJrnAdjId Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="20%">Accounting Period :</td>
				<td width="15%">
					<asp:DropDownList id=ddlAccMonth runat=server  CssClass="font9Tahoma"/> / 
					<asp:DropDownList id=ddlAccYear OnSelectedIndexChanged=OnIndexChage_ReloadAccPeriod AutoPostBack=True runat=server  CssClass="font9Tahoma"/>
				</td>
				<td width="8%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Description :*</td>
				<td><asp:Textbox id="txtDesc" Width=100% MaxLength=128 runat=server CssClass="font9Tahoma" />
					<asp:RequiredFieldValidator 
						id="validateDesc" 
						runat="server" 
						ErrorMessage="Please enter description." 
						EnableClientScript="True"
						ControlToValidate="txtDesc" 
						display="dynamic"/>
				</td>
				<td>&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="22%">&nbsp;</td>
				<td width="8%">&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Journal Type :</td>
				<td><asp:DropDownList id="lstJrnType"  Width=50% runat=server  CssClass="font9Tahoma"/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>Received From :</td>
				<td><asp:DropDownList id="ddlReceiveFrom"  CssClass="font9Tahoma" Width=50% runat=server>
						<asp:ListItem value="1">Head Quarter</asp:ListItem>
						<asp:ListItem value="2">Others</asp:ListItem>
					</asp:DropDownList>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>		
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Document Ref. No. :*</td>
				<td><asp:Textbox id="txtRefNo" Width=100% runat=server  CssClass="font9Tahoma"/>	
					<asp:RequiredFieldValidator 
						id="validateRef" 
						runat="server" 
						ErrorMessage="Please enter document reference number." 
						EnableClientScript="True"
						ControlToValidate="txtRefNo" 
						display="dynamic"/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>Document Ref. Date :* </td>
				<td><asp:TextBox id=txtDate Width=50% maxlength=10 runat=server  CssClass="font9Tahoma"/>
					<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="<br>Please enter document date." 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
					<asp:label id=lblDate Text ="<BR>Date entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					<asp:label id=lblFmt forecolor=red Visible = false Runat="server"/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>Document Amount :*</td>
				<td><asp:Textbox id="txtAmt" width=100% maxlength=22 runat=server  CssClass="font9Tahoma"/>	
					<asp:RequiredFieldValidator 
						id="validateAmt" 
						runat="server" 
						ErrorMessage="Please enter document amount" 
						EnableClientScript="True"
						ControlToValidate="txtAmt" 
						display="dynamic"/>
					<asp:RegularExpressionValidator 
						id="RegularExpressionValidatorAmt" 
						ControlToValidate="txtAmt"
						ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
						Display="Dynamic"
						text = "<BR>Maximum length 19 digits and 2 decimal points"
						runat="server"/>
					<asp:RangeValidator 
						id="RangetxtAmt"
						ControlToValidate="txtAmt"
						MinimumValue="0.01"
						MaximumValue="9999999999999999999.99"
						Type="double"
						EnableClientScript="True"
						Text="<BR>The value must be from 0.01!"
						runat="server" display="dynamic"/>
				</td>
				<td colspan=4>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
            </table>
            <table border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
            <tr>
            <td>
      
			<tr>
				<td colspan="6">
					<table id="tblAdd" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server" class="sub-add">
						<tr class="mb-c">
							<td height=25>Line Description :*</td>
							<td colspan=4><asp:TextBox id=txtDescLn Width=100% maxlength=128 runat=server  CssClass="font9Tahoma"/>	
											<asp:label id=lblDescErr text="Please enter description." Visible=False forecolor=red Runat="server" /></td>
						</tr>
						<tr class="mb-c">
							<td height=25><asp:label id="lblAccCodeTag" Runat="server"/></td>
							<td colspan=4><GG:AutoCompleteDropDownList id="lstAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged=OnChange_Reload runat=server  CssClass="font9Tahoma"/> 
									   	  <input type=button value=" ... " id="Find" onclick="javascript:PopCOA('frmMain', '', 'lstAccCode', 'False');" CausesValidation=False runat=server />  
										  <asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
							</td>
						</tr>						
						<tr class="mb-c">
							<td height=25><asp:label id=lblBlkTag Runat="server"/></td>
							<td colspan=4><asp:DropDownList id="lstBlock" Width=100% runat=server  CssClass="font9Tahoma"/>
										  <asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
						</tr>		
						<tr class="mb-c">
							<td width="15%" height=25>Quantity (DR) :</td>
							<td width="30%"><asp:Textbox id="txtQty" Width=50% maxlength=15 OnKeyUp="javascript:calDRAmount();" runat=server  CssClass="font9Tahoma"/>
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidatorQty" 
									ControlToValidate="txtQty"
									ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									Display="Dynamic"
									text = "<BR>Maximum length 9 digits and 5 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtQty"
									ControlToValidate="txtQty"
									MinimumValue="0"
									MaximumValue="999999999.99999"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be more than 1!"
									runat="server" display="dynamic"/>
								<asp:Label id=lblNoQty visible=false forecolor=red text="<BR>Please enter quantity." runat=server/>
								<asp:Label id=lblTwoQty visible=false forecolor=red text="<BR>Please enter either DR or CR quantity." runat=server/>
							</td>
							<td width="10%">&nbsp;</td>
							<td width="15%">Quantity (CR) :</td>
							<td width="30%"><asp:Textbox id="txtQtyCR" Width=50% maxlength=15 OnKeyUp="javascript:calCRAmount();" runat=server  CssClass="font9Tahoma"/>
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidatorQtyCR" 
									ControlToValidate="txtQtyCR"
									ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									Display="Dynamic"
									text = "<BR>Maximum length 9 digits and 5 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtQtyCR"
									ControlToValidate="txtQtyCR"
									MinimumValue="0"
									MaximumValue="999999999.99999"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value."
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr class="mb-c">
							<td>Unit Price (DR) :</td>
							<td><asp:Textbox id="txtPrice" OnKeyUp="javascript:calDRAmount();" Width=100%  maxlength=22 runat=server  CssClass="font9Tahoma"/> 
								 <asp:RegularExpressionValidator 
									id="RegularExpressionValidatorPrice" 
									ControlToValidate="txtPrice"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtPrice"
									ControlToValidate="txtPrice"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value."
									runat="server" display="dynamic"/>
							
								<asp:Label id=lblNoPrice visible=false forecolor=red text="<BR>Please enter unit price." runat=server/>
								<asp:Label id=lblTwoPrice visible=false forecolor=red text="<BR>Please enter either DR or CR unit price." runat=server/>
								<asp:Label id=lblDRorCR visible=false forecolor=red text="<BR>Please enter either DR quantity and unit price OR, CR quantity and unit price." runat=server/>
							</td>
							<td>&nbsp;</td>
							<td>Unit Price (CR) :</td>
							<td><asp:Textbox id="txtPriceCR" OnKeyUp="javascript:calCRAmount();" Width=100% maxlength=22 runat=server  CssClass="font9Tahoma"/> 
								 <asp:RegularExpressionValidator 
									id="RegularExpressionValidatorPriceCR" 
									ControlToValidate="txtPriceCR"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtPriceCR"
									ControlToValidate="txtPriceCR"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr class="mb-c">
							<td>Total Amount (DR) :</td>
							<td><asp:Textbox id="txtDRTotalAmount" Width=100% maxlength=22 runat=server  CssClass="font9Tahoma"/>
							<asp:RegularExpressionValidator 
									id="RegularExpressionValidatorAmtDR" 
									ControlToValidate="txtDRTotalAmount"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtAmtDR"
									ControlToValidate="txtDRTotalAmount"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/>
								
								<asp:Label id=lblTwoAmount visible=false forecolor=red text="<BR>Please enter either DR or CR total amount" runat=server/>
							</td>
							<td>&nbsp;</td>
							<td>Total Amount (CR) :</td>
							<td><asp:Textbox id="txtCRTotalAmount" Width=100% maxlength=22 runat=server  CssClass="font9Tahoma"/>
									<asp:RegularExpressionValidator 
									id="RegularExpressionValidatorAmtCR" 
									ControlToValidate="txtCRTotalAmount"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangetxtAmtCR"
									ControlToValidate="txtCRTotalAmount"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True"
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr class="mb-c">
							<td>&nbsp;</td>						
							<td Colspan=2>
								<asp:label id=lblerror text="<br>Number generated is too big!" Visible=False forecolor=red Runat="server" />
							</td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>						
						</tr>
						<tr class="mb-c">
							<td Colspan=3><asp:ImageButton  id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" /> 
										  <asp:ImageButton  id="btnUpdate" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnAdd_Click" Runat="server" />&nbsp;</td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>	
						</tr>
					</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="6"> 
					<asp:DataGrid id="dgResult"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"								
						AllowSorting="True" class="font9Tahoma">	
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />		
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
					<asp:TemplateColumn HeaderText="No.">
						<ItemStyle width="3%"/>
						<ItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</EditItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Line Description">
						<ItemStyle width="16%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Quantity">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="8%" HorizontalAlign="Right" />			
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Quantity"),5) %> 				
							<asp:label id="lblQtyTrx" text=<%# Container.DataItem("Quantity") %> visible=false runat="server" />							
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit Price">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="8%" HorizontalAlign="Right" />							
						<ItemTemplate>
							<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("UnitPrice"),0,True,False,False)) %> 
							<asp:label id="lblUnitCost" text=<%# Container.DataItem("UnitPrice") %> visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="10%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblAmount" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %>  runat="server" />
							<asp:label id="lblAccTx" runat="server" />
							<asp:label id="lblAmt" text=<%# Container.DataItem("Total")%> visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>										
					<asp:TemplateColumn>		
						<ItemStyle width="5%" HorizontalAlign="center" />							
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("JournalAdjLnId") %> Visible=False id="lblID" runat="server" />
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label text=<%# Container.DataItem("JournalAdjLnId") %> Visible=False id="lblID" runat="server" />
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					</Columns>										
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr class="font9Tahoma">
				<td colspan=4>&nbsp;</td>								
				<td height=25 align=right>Total Amount : <asp:label id="lblTotAmtFig" text="0" runat="server" /></td>						
				<td>&nbsp;</td>					
			</tr>
			<tr class="font9Tahoma">
				<td colspan=4>&nbsp;</td>								
				<td align=right>Control Amount : <asp:label id="lblCtrlAmtFig" runat="server" /></td>	
				<td>&nbsp;</td>					
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr class="font9Tahoma">
				<td align="left" colspan="6">
				    <asp:ImageButton id="btnNew"    UseSubmitBehavior="false" OnClick=btnNew_Click     imageurl="../../images/butt_new.gif"     AlternateText="New Journal Adjustment Entry" runat="server"/>
					<asp:ImageButton id="btnSave"	UseSubmitBehavior="false" onClick=btnSave_Click		ImageURL="../../images/butt_save.gif"	 Visible=False runat="server" />
					<asp:ImageButton id="btnPost"	UseSubmitBehavior="false" onClick=btnPost_Click		ImageURL="../../images/butt_post_journal.gif"	 Visible=False runat="server" />
					<asp:ImageButton id="btnDelete"	UseSubmitBehavior="false" onClick=btnDelete_Click		ImageURL="../../images/butt_delete.gif" CausesValidation =False AlternateText="Cancel" Visible=False runat="server" />
					<asp:ImageButton id="btnPrint"	UseSubmitBehavior="false" onClick=btnPrint_Click		ImageURL="../../images/butt_print.gif"	CausesValidation =False runat=server visible=false />
					<asp:ImageButton id="btnBack"	UseSubmitBehavior="false" onClick=btnBack_Click		ImageURL="../../images/butt_back.gif"	CausesValidation =False runat="server" />
				    <br />
				</td>
			</tr>
                  </td>
            </tr>		
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
