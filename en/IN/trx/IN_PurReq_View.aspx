<%@ Page Language="vb" src="../../../include/IN_PurReq_View.aspx.vb" Inherits="IN_PurReq_View" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Purchase Requisition Details</title>		
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmPurReqDet runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



		<table border="0" width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /><asp:Label id=PRType Visible="False" runat=server /><asp:label id=lblStatus visible=false runat=server /><asp:label id=lblPrintDate visible=false runat=server /><asp:label id="SortExpression" Visible="False" Runat="server" /><input type=hidden id=hidPQID runat=server />
			<tr>
				<td colspan="6"><UserControl:MenuINTrx id=menuIN runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">PURCHASE REQUISITION DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr style="width :100%" /> </td>
			</tr>			
			<tr>
				<td width="20%" height="25">Purchase Requisition ID :</td>
				<td width="40%"><asp:label id=lblPurReqID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period : </td>
				<td width="20%"><asp:Label id=lblAccPeriod runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Transaction Date :*</td>
				<td><asp:TextBox id=txtDate Width=50% maxlength=10 runat=server />
					<a href="javascript:PopCal('txtDate');">
					<asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="<br>Please specify reference date!" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
					<asp:label id=lblDate Text="Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>Status : </td>
				<td><asp:Label id=Status runat=server /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td width="20%" height="25">Purchase Requisition Level :*</td>
				<td width=30%><asp:DropDownList id="ddlPRLevel" width=100%  runat=server />
							  <asp:Label id=lblErrPRLevel visible=false forecolor=red text="<br>Please select PR Level." runat=server/>
				</td>
				<td>&nbsp;</td>
			    <td>Date Created : </td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td width="20%" height="25">Dept Code :*</td>
				<td width=30%><asp:DropDownList id="ddlDeptCode" width=100% runat=server />
							  <asp:Label id=lblErrDeptCode visible=false forecolor=red text="<br>Please select Dept Code." runat=server/>
				</td>
				<td>&nbsp;</td>	
				<td>Last Update :</td>
				<td><asp:Label id=UpdateDate runat=server /></td>	
				<td>&nbsp;</td>	
			</tr>			
			<tr>
				<tr><td height="25">Centralized :</td>
				<td><asp:CheckBox id="chkCentralized" Text="  No" checked=false AutoPostBack=true OnCheckedChanged=Centralized_Type runat=server /></td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>			
				<td>&nbsp;</td>
				<td>&nbsp;</td>		
			</tr>
            </table>

            <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
			<tr>
				<td colspan="6">
					<table id=tblLine width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" runat=server visible="false" class="font9Tahoma">
						<tr>						
							<td width=20% height="25">Item Code :*</td>
							<td width=80%>
                                <asp:TextBox id=txtItemCode Width="25%" maxlength=15 AutoPostBack=True runat=server />
                                <input type=button value=" ... " id="FindIN" Visible=False onclick="javascript:PopItem_New('frmPurReqDet', '', 'txtItemCode','TxtItemName','UnitCost', 'False');" CausesValidation=False runat=server /><input type=button value=" ... " id="FindDC" Visible=False onclick="javascript:PopItem_New('frmPurReqDet', '', 'txtItemCode', 'False');" CausesValidation=False runat=server /><input type=button value=" ... " id="FindWS" Visible=False onclick="javascript:PopItem_New('frmPurReqDet', '', 'txtItemCode', 'False');" CausesValidation=False runat=server /><input type=button value=" ... " id="FindNU" Visible=False onclick="javascript:PopItem_New('frmPurReqDet', '', 'txtItemCode', 'False');" CausesValidation=False runat=server />
                                <asp:TextBox ID="TxtItemName" runat="server" BackColor="Transparent" BorderStyle="None"
                                    ForeColor="White" MaxLength="10" Width="60%"></asp:TextBox><asp:RequiredFieldValidator 
												id="validateQty" 
												runat="server" 
												ErrorMessage="<br>Please select one Item" 
												ControlToValidate="txtItemCode" 
												display="dynamic"/></td>
						</tr>
						<tr>
							<td height="25">Quantity Request :*</td>
							<td>
								<asp:textbox id="QtyReq" width=35% maxlength=15 Runat="server" />
								<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
									ControlToValidate="QtyReq"
									ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									Display="Dynamic"
									text = "<br>Maximum length 9 digits and 5 decimal points"
									runat="server"/>
								<asp:RequiredFieldValidator 
									id="validateQtyReq" 
									runat="server" 
									ErrorMessage="<br>Please specify quantity to request" 
									ControlToValidate="QtyReq" 
									display="dynamic"/>
								<asp:RangeValidator id="RangeQtyReq"
									ControlToValidate="QtyReq"
									MinimumValue="0"
									MaximumValue="999999999999999"
									Type="double"
									EnableClientScript="True"
									Text="<br>The value is out of acceptable range!"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tr>
							<td height="25">Unit Cost :</td>
							<td>
								<asp:textbox id="UnitCost" width=35% maxlength=19 Runat="server" />
								<asp:RegularExpressionValidator id="RegularExpressionValidatorUnitCost" 
									ControlToValidate="UnitCost"
									ValidationExpression="\d{1,15}\.\d{0,2}|\d{1,15}"
									Display="Dynamic"
									text="<br>Maximum length 15 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator id="RangeUnitCost"
									ControlToValidate="UnitCost"
									MinimumValue="0"
									MaximumValue="999999999999999"
									Type="double"
									EnableClientScript="True"
									Text="<br>The value is out of acceptable range!"
									runat="server" display="dynamic"/>
							</td>
						</tr>
						<tAdditional Note :</td>
	                        <td><textarea rows=6 id=txtAddNote cols=50 runat=server></textarea></td>
						</tr>
						<tr>
							<td colspan=2><asp:ImageButton text="Add" id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" />
                                            </td>
						</tr>
					</table>
				</td>
			</tr>
            </table>

            <table style="width: 100%" class="font9Tahoma">
			<tr>
				<td colspan=5>&nbsp;</td>				
			</tr>											
			<tr>
				<td colspan="5"> 
					<table id="PRLnTable" border="0" width="100%" cellspacing="0" cellpadding="2" runat="server">
						<tr>
							<td colspan="3" width=100%>
								<asp:DataGrid id="dgPRLn"
									AutoGenerateColumns="false" width="100%" runat="server"
									GridLines = none
									Cellpadding = "2"
									Pagerstyle-Visible="False"
									OnEditCommand="DEDR_Edit"
						            OnUpdateCommand="DEDR_Update"
									OnDeleteCommand="DEDR_Delete"
									OnCancelCommand="DEDR_Cancel"									
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
								<asp:BoundColumn Visible=False DataField="PRLnId" />
								<asp:TemplateColumn HeaderText="Item <br> Additional Note">
									<ItemStyle Width="20%"/> 																								
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
										(<%# Container.DataItem("ItemDesc") %>)	<br />
										<asp:label text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
										<asp:TextBox id="lstAddNote" Visible=false Text='<%# trim(Container.DataItem("AdditionalNote")) %>'
										        runat="server"/>	
										<asp:label text=<%# Container.DataItem("PRLnID") %> Visible=false id="LnID" runat="server" />		    	
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Stock UOM">
									<ItemStyle Width="5%"/> 																								
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("UOMCode") %> id="lblUOMCode" runat="server" />			
										<asp:label text=<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("Status")) %> visible="false" id="hidStatus" runat="server" />			
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right HeaderText="Quantity/Cost Requested">
									<ItemStyle Width="10%"/> 																								
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("QtyReq") %> id="lblQtyReq" visible="false" runat="server" />							
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyReq"),2) %> id="lblQtyReqDisplay" runat="server" />
										<asp:TextBox id="lstQtyReq" MaxLength="8" width=90% Visible=false Text='<%# trim(Container.DataItem("QtyReq")) %>'
										        runat="server"/>
										<asp:RegularExpressionValidator id="rvQtyReq" 
									        ControlToValidate="lstQtyReq"
									        ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									        Display="Dynamic"
									        text = "<br>Maximum length 9 digits and 5 decimal points"
									        runat="server"/>
								        <asp:RequiredFieldValidator 
									        id="vQtyReq" 
									        runat="server" 
									        ErrorMessage="<br>Please specify quantity to request" 
									        ControlToValidate="lstQtyReq" 
									        display="dynamic"/>
								        <asp:RangeValidator id="rQtyReq"
									        ControlToValidate="lstQtyReq"
									        MinimumValue="0"
									        MaximumValue="999999999999999"
									        Type="double"
									        EnableClientScript="True"
									        Text="<br>The value is out of acceptable range!"
									        runat="server" display="dynamic"/><br />
									    <asp:label text=<%# Container.DataItem("Cost") %> id="lblUnitCost" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Cost"),2) %> id="lblUnitCostDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right HeaderText="Quantity Approved">
									<ItemStyle Width="10%"/> 																								
									<ItemTemplate>
										<asp:label text=<%# Container.DataItem("QtyApp") %> id="lblQtyApp" visible="false" runat="server" />							
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyApp"),2) %> id="lblQtyAppDisplay" runat="server" />
										<asp:TextBox id="lstQtyApp" MaxLength="8" width=90% Visible=false Text='<%# trim(Container.DataItem("QtyApp")) %>'
										        runat="server"/>
										<asp:RegularExpressionValidator id="rvQtyApp" 
									        ControlToValidate="lstQtyApp"
									        ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									        Display="Dynamic"
									        text = "<br>Maximum length 9 digits and 5 decimal points"
									        runat="server"/>
								        <asp:RequiredFieldValidator 
									        id="vQtyApp" 
									        runat="server" 
									        ErrorMessage="<br>Please specify quantity to approved" 
									        ControlToValidate="lstQtyApp" 
									        display="dynamic"/>
								        <asp:RangeValidator id="rQtyApp"
									        ControlToValidate="lstQtyApp"
									        MinimumValue="0"
									        MaximumValue="999999999999999"
									        Type="double"
									        EnableClientScript="True"
									        Text="<br>The value is out of acceptable range!"
									        runat="server" display="dynamic"/><br />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right HeaderText="Quantity/Price <br> on Last Order">
									<ItemStyle Width="10%"/> 																								
									<ItemTemplate>
									    <!--
										<asp:label text=<%# Container.DataItem("QtyRcv") %> id="lblQtyRcv" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyRcv"),5) %> id="lblQtyRcvDisplay" runat="server" />
										-->
										<asp:label text=<%# Container.DataItem("QtyOrderLast") %> id="lblQtyOrderLast" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOrderLast"),2) %> id="lblQtyOrderLastDisplay" runat="server" /><br>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("CostLast"),2) %> id="lblCostOrderLastDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Left ItemStyle-HorizontalAlign=Left HeaderText="POID <br> PO Date">
									<ItemStyle Width="15%"/> 																								
									<ItemTemplate>
									    <!--
										<asp:label text=<%# Container.DataItem("QtyOutstanding") %> id="lblQtyOutstanding" visible="false" runat="server" />
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOutstanding"),5) %> id="lblQtyOutstandingDisplay" runat="server" />
										-->
										<asp:label text=<%# Container.DataItem("POIDLast") %> id="lblPOIDLast" runat="server" />
										<asp:label text=<%# Container.DataItem("PODate") %> id="lblPODate" visible="false" runat="server" /><br />
										<asp:label text=<%# ObjGlobal.GetLongDate(Container.DataItem("PODate")) %> id="lblPODateDisplay" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right HeaderText="Status">
									<ItemStyle Width="8%"/> 																								
									<ItemTemplate>
									    <asp:label id=lblStatusDescln text='<%# objIN.mtdGetPurReqLnStatus(Container.DataItem("StatusLn")) %>' runat=server/>
									    <asp:label id=lblStatusln visible=false text='<%# Container.DataItem("StatusLn") %>' runat=server/>
							            <asp:DropDownList id="lstStatusLn" visible=false size=1 width="95%" runat="server"/>
							            <BR>
							            <asp:RequiredFieldValidator id=validateStatusln display=dynamic runat=server 
									            ControlToValidate=lstStatusLn />
						            </ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right HeaderText="App. By <br> Next App. By">
									<ItemStyle Width="8%"/> 																								
									<ItemTemplate>
									    <asp:label id=lblApprovedBy text='<%# Container.DataItem("ApprovedBy") %>' runat=server/>
									    <asp:label id=lblhidApprovedBy visible=false text='<%# Container.DataItem("indApprovedBy") %>' runat=server/><br />
									    <asp:label id=lblNextApprovedBy text='<%# Container.DataItem("NextApprovedBy") %>' runat=server/>
						            </ItemTemplate>
								</asp:TemplateColumn>		
								<asp:TemplateColumn>
									<ItemStyle Width="5%" HorizontalAlign="Right"/> 																
									<ItemTemplate>
									    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
									    <asp:LinkButton id="Update" CommandName="Update" Text="Update" CausesValidation=False  runat="server"/>
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False  runat="server"/>
										<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								</Columns>										
								</asp:DataGrid>
							</td>	
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan=2>&nbsp;</td>
				<td colspan=2 height=25><hr style="width :100%" />   </td>
				<td width="5%">&nbsp;</td>					
			</tr>
			<tr>
				<td colspan=2>&nbsp;</td>
				<TD height=25>Total Amount : Amount :</TD>
				<TD Align=right><asp:Label ID=lblTotAmtFigDisplay Runat=server /><asp:Label ID=lblTotAmtFig Runat=server Visible=False />&nbsp;</TD>
				<td>&nbsp;</td>
			</TR>
			<tr>
				<td height="25">Remarks :</td>	
				<td colspan="4"><asp:textbox id="txtRemarks" width=100% maxlength="256" runat="server" /></td>
			</tr>
			<tr>
			    <td colspan=2 height="25">
			        <asp:Label id=lblErrGR visible=True Text="" forecolor=red runat=server />
			    </td>
			</tr>
			<tr>
				<td c&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="5">
					<asp:ImageButton id="Save" UseSubmitBehavior="false" ImageURL="../../images/butt_save.gif" onClick="btnSave_Click" CausesValidation="false"  AlternateText="Save" runat="server" Visible="False" />
					<asp:ImageButton id="Confirm" UseSubmitBehavior="false" ImageURL="../../images/butt_confirm.gif" CausesValidation="false" onClick="btnConfirm_click" AlternateText="Confirm" runat="server" Visible="False" />
					<asp:ImageButton id="Cancel" UseSubmitBehavior="false" ImageURL="../../images/butt_cancel.gif" CausesValidation="false" onClick="btnCancel_click"  AlternateText="Cancel" visible=False runat="server" />
					<asp:ImageButton id="Print" UseSubmitBehavior="false" ImageURL="../../images/butt_print.gif" AlternateText="Print" CausesValidation="false" onClick="btnPreview_Click" runat="server" Visible="False" />
					<asp:ImageButton id="PRDelete" UseSubmitBehavior="false" ImageURL="../../images/butt_delete.gif" CausesValidation="false" onClick="btnPRDelete_Click" AlternateText="Delete" runat="server" Visible="False" />
					<asp:ImageButton id="Undelete" UseSubmitBehavior="false" ImageURL="../../images/butt_undelete.gif" CausesValidation="false" onClick="btnPRUnDelete_Click" AlternateText="Undelete" runat="server" Visible="False" />
					<asp:ImageButton id="btnAddendum" UseSubmitBehavior="false" onClick="btnAddendum_Click" ImageUrl="../../images/butt_gen_addendum.gif" AlternateText="Generate Addendum" CausesValidation=False runat="server" Visible="False" />
					<asp:ImageButton id="Back" UseSubmitBehavior="false" ImageURL="../../images/butt_back.gif" onClick="btnBack_Click" AlternateText="Back" CausesValidation=False runat="server" />
				</td>
			</tr>		
			<tr>
				<td align="left" colspan="5">
                                            &nbsp;</td>
			</tr>		
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>


        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
