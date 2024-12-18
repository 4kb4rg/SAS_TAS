<%@ Page Language="vb" src="../../../include/PU_RFQ_Detail.aspx.vb" Inherits="PU_RFQ_Detail" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">

</script>

<html>
	<head>
		<title>Purchase Requisition Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
   <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<form id=frmPurReqDet class="main-modul-bg-app-list-pu" runat=server>
		<table border="0" width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /><asp:Label id=PRType Visible="False" runat=server /><asp:label id=lblStatus visible=false runat=server /><asp:label id=lblPrintDate visible=false runat=server /><asp:label id="SortExpression" Visible="False" Runat="server" /><input type=hidden id=hidPQID runat=server />
			<tr>
				<td colspan="6"><UserControl:MenuINTrx id=menuIN runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6" style="height: 21px">
                    RFQ</td>
			</tr>
			<tr>
				<td colspan=6><hr style="width :100%" /> </td>
			</tr>			
			<tr>
				<td height="25" style="width: 255px">
                    RFQ &nbsp;ID :</td>
				<td style="width: 1168px"><asp:label id=lblPurReqID Runat="server"/></td>
				<td style="width: 7px">&nbsp;</td>
				<td style="width: 155px"> </td>
				<td style="width: 255px"></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td style="width: 255px; height: 14px;" valign="middle">Transaction Date :*dddd</td>
				<td style="width: 1168px; height: 14px;" valign="middle"><asp:TextBox id=txtDate Width="20%" maxlength=10 runat=server />
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
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/></td>
				<td style="height: 14px; width: 7px;"></td>
				<td style="width: 155px; height: 14px;"></td>
				<td style="width: 255px; height: 14px;"></td>
				<td style="height: 14px"></td>
			</tr>
			<tr>
				<td style="width: 255px; height: 15px;">
                    Quotation Deadline &nbsp;:*</td>
				<td style="width: 1168px; height: 15px;" valign="middle"><asp:TextBox id=txtDateDL Width="20%" maxlength=10 runat=server />
				<a href="javascript:PopCal('txtDateDL');">
                    <asp:Image id="btnSelDateDL" runat="server" ImageUrl="../../Images/calendar.gif"/></a> <asp:RequiredFieldValidator 
						id="RequiredFieldValidator1" 
						runat="server" 
						ErrorMessage="<br>Please specify reference date!" 
						EnableClientScript="True"
						ControlToValidate="txtDateDL" 
						display="dynamic"/><asp:label id=Label1 Text="Date Entered should be in the format" forecolor=red Visible=false Runat="server"/><asp:label id=lblFmtDL  forecolor=red Visible = false Runat="server"/></td>
				<td style="height: 15px; width: 7px;">&nbsp;</td>
			    <td style="width: 155px; height: 15px;"> </td>
				<td style="width: 255px; height: 15px;"></td>
				<td style="height: 15px">&nbsp;</td>
			</tr>			
			<tr>
				<td style="width: 255px; height: 20px;" valign="middle">
                                            Purchase Requtition Number :</td>
				<td style="width: 1168px; height: 20px;" valign="middle">
                                            <asp:TextBox ID="txtPRID_Plmph" runat="server" AutoPostBack="False" MaxLength="64"
                                                Width="70%"></asp:TextBox>
                                            <input id="BtnViewPR" runat="server" causesvalidation="False" onclick="javascript:PopPR_RPH('frmMain', '', 'txtItemCode','TxtItemName','txtCost1','txtQtyOrder1','txtPRID_Plmph','txtTtlCost1','ddlPRRefLocCode','txtAddNote','txtPRRefId','txtPurchUOM','False');"
                                                type="button" value=" ... " />
                    <asp:ImageButton id="ImgGen" UseSubmitBehavior="false" ImageURL="../../Images/butt_generate.gif" onClick="btnGenerate_Click" CausesValidation="false"  AlternateText="ImgGenerate" runat="server" /></td>
				<td style="height: 20px; width: 7px;">&nbsp;</td>	
				<td style="width: 155px; height: 20px;"></td>
				<td style="width: 255px; height: 20px;"></td>	
				<td style="height: 20px">&nbsp;</td>	
			</tr>			
                <tr>
                    <td colspan="6" style="height: 21px">
                    </td>
                </tr>
			<tr>
				<td colspan=6 style="height: 21px"></td>				
			</tr>											
			<tr>
				<td colspan="6" style="height: 75px" valign="top" class="font9Tahoma"> 
					<table id="PRLnTable" border="0" width="100%" cellspacing="0" cellpadding="2" runat="server">
						<tr>
							<td colspan="3" width=100%>
                                <asp:DataGrid ID="dgPRLn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CellPadding="2" GridLines="None" OnCancelCommand="DEDR_Cancel" OnDeleteCommand="DEDR_Delete"
                                    OnEditCommand="DEDR_Edit" OnUpdateCommand="DEDR_Update" PagerStyle-Visible="False" class="font9Tahoma"
                                    Width="95%">
                                    <PagerStyle Visible="False" />
                                    <AlternatingItemStyle CssClass="mr-r" />
                                    <ItemStyle CssClass="mr-l" />
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
                                         <asp:TemplateColumn HeaderText="RFQLNID">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblRfqLNID" runat="server" Text='<%# Container.DataItem("RfqLnId") %>'></asp:Label>&nbsp;                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="8%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Item &lt;br&gt; PR Note">
                                            <ItemTemplate>
                                                <asp:Label ID="ItemCode" runat="server" Text='<%# Container.DataItem("ItemCode") %>'></asp:Label>                                                
                                                <br />
                                                <asp:Label ID="lblAddNote" runat="server" Text='<%# Container.DataItem("PRNote") %>'></asp:Label>&nbsp;
                                                <asp:Label ID="lblPrLNID" runat="server" Text='<%# Container.DataItem("PRLnID") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Item Description  &lt;br&gt; Other Name">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblItemdesc" runat="server" Text='<%# Container.DataItem("Description") %>'></asp:Label>&nbsp;                                                
                                              <br />  <asp:Label ID="lblOtheName" runat="server" Text='<%# Container.DataItem("OtherName") %>'></asp:Label>&nbsp;                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Stock UOM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOMCode" runat="server" Text='<%# Container.DataItem("PurchaseUom") %>'></asp:Label>                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Quantity/Cost Requested">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblQtyReqDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),2) %>'></asp:Label>                                                                                               
                                                <asp:Label ID="lblUnitCost" runat="server" Text='<%# Container.DataItem("UnitCost") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblUnitCostDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("UnitCost"),2) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Additional Note">
                                         <ItemTemplate>   
                                                <asp:Label ID="lblAdditionNote" runat="server" Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label><br />
                                                <asp:TextBox id=txtaddNote Width="100%" Visible=false maxlength=10 runat=server />
                                         </ItemTemplate>
                                            <EditItemTemplate>
                                                &nbsp;
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="Update" runat="server" CausesValidation="False" CommandName="Update"
                                                    Text="Update"></asp:LinkButton>
                                                <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete"></asp:LinkButton>
                                                <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="mr-h" />
                                </asp:DataGrid></td>	
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="25" style="width: 255px"><asp:label id=Label3 Runat="server" Visible="False">Remark :</asp:label></td>	
				<td colspan="5">
				<asp:textbox id="txtRemarks" width="86%" maxlength="256" runat="server" Height="48px" TextMode="MultiLine" Visible="False" /></td>
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
				<td align="left" colspan="6">
					<asp:ImageButton id="Save" UseSubmitBehavior="false" ImageURL="../../images/butt_save.gif" onClick="btnSave_Click" CausesValidation="false"  AlternateText="Save" runat="server" />
					<asp:ImageButton id="Confirm" UseSubmitBehavior="false" ImageURL="../../images/butt_confirm.gif" CausesValidation="false" onClick="btnConfirm_click" AlternateText="Confirm" runat="server" />
					<asp:ImageButton id="Cancel" UseSubmitBehavior="false" ImageURL="../../images/butt_cancel.gif" CausesValidation="false" onClick="btnCancel_click"  AlternateText="Cancel" visible=false runat="server" />
					<asp:ImageButton id="Print" UseSubmitBehavior="false" ImageURL="../../images/butt_print.gif" AlternateText="Print" CausesValidation="false" onClick="btnPreview_Click" runat="server" />
					<asp:ImageButton id="PRDelete" UseSubmitBehavior="false" ImageURL="../../images/butt_delete.gif" CausesValidation="false" onClick="btnPRDelete_Click" AlternateText="Delete" runat="server" />
					<asp:ImageButton id="Undelete" UseSubmitBehavior="false" ImageURL="../../images/butt_undelete.gif" CausesValidation="false" onClick="btnPRUnDelete_Click" AlternateText="Undelete" runat="server" />&nbsp;
					<asp:ImageButton id="Back" UseSubmitBehavior="false" ImageURL="../../images/butt_back.gif" onClick="btnBack_Click" AlternateText="Back" CausesValidation=False runat="server" />
				    <br />
				</td>
			</tr>		
		</table>
            &nbsp;&nbsp;
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />	
			<asp:label id=lblLocCode visible=false runat=server />
            &nbsp;
		</form>
	</body>
</html>
