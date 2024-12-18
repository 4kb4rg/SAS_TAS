<%@ Page Language="vb" trace="false" src="../../../include/IN_Trx_FuelIssue_Details.aspx.vb" Inherits="IN_FuelIssueDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Fuel Issue Details</title>		
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	    <style type="text/css">
            .style1
            {
                height: 18px;
            }
        </style>
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


   		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id="lblStatusHid" Visible="False" Runat="server" />
		<asp:label id="AccountCode" Visible="False" Text= "Account Code" Runat="server" />
		<!--<asp:label id="BillParty" Visible="False" Text= "Bill Party" Runat="server" />-->
		<asp:label id="EmployeeCode" Visible="False" Text= "Employee Code" Runat="server" />
		<asp:label id="issueType" Visible="False" Runat="server" />
		<asp:label id="lblCode" visible=false text=" Code" runat=server />
		<asp:label id="lblPleaseSelect" visible=false text="Please select " runat=server />			
		<asp:label id="lblSelect" visible=false text="Select " runat=server />
		<asp:label id=lblTxLnID visible=false runat=server />
		<asp:label id=lblOldQty visible=false runat=server />			
		<asp:label id=lblOldItemCode visible=false runat=server />
		
		<table border=0 width="100%" cellspacing="0" cellpading="1"  class="font9Tahoma">
			<tr>
				<td colspan=6><UserControl:MenuINTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>FUEL ISSUE DETAILS</td>
			</tr>
			<tr>
				<td colspan=6><hr style="width :100%" />   </td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>				
			</tr>		
			<tr>
				<td width="20%" height=25>Fuel Issue ID :</td>
				<td width="30%"><asp:label id=lblFuelTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Status :</td>
				<td width="25%"><asp:Label id=Status runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>
					<asp:label id=lblBillTo text="Bill To :*" Visible=False Runat="server"/>
					<asp:label id=lblBPartyTag Visible=False Runat="server"/>
					<asp:label id=lblLocationTag Text="Charge To :*" Visible=False Runat="server"/>
				</td>
				<td>
					<asp:DropDownList id="lstBillTo" Width=100% Visible=False runat=server />
					<asp:DropDownList id="lstBillParty" Width=100% Visible=False runat=server />
					<asp:DropDownList id="ddlLocation" Width=100% Visible=False AutoPostBack=True OnSelectedIndexChanged=ddlLocation_OnSelectedIndexChanged runat=server /> 
					<asp:label id=lblBillPartyErr Visible=False forecolor=red Runat="server" />
					<asp:label id=lblLocationErr Visible=False forecolor=red Runat="server" />
				</td>
				<td>&nbsp;</td>
				<td>Date Created :</td>
				<td><asp:Label id=CreateDate runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25><asp:label id=lblChargeMarkUp visible=False text="Charge :" runat=server /></td>
				<td><asp:CheckBox id=chkMarkUp visible=false Checked =False Runat="server"/></asp:CheckBox></td>
				<td>&nbsp;</td>
				<td>Last Update :</td>
				<td><asp:Label id=UpdateDate runat=server /></td>		
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>Updated By :</td>
				<td><asp:Label id=UpdateBy runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblPDateTag Text="Print Date :" visible=false runat=Server /></td>
				<td><asp:Label id=lblPrintDate  visisble=false runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblDNIDTag visible=False Text="Debit Note ID :" runat=Server /></td>
				<td><asp:Label id=lblDNNoteID  runat=server /></td>				
				<td>&nbsp;</td>
			</tr>
            </table>

             <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
			<tr>
				<td colspan=6>
				<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="2" runat="server"  class="font9Tahoma">
					<tr class="mb-c">
						<td width="20%" height=25>Item Code :*</td>
						<td width="80%"><asp:DropDownList id="lstItem" Width=90% runat=server EnableViewState=True />
										<input type=button value=" ... " id="Find"  onclick="javascript:findcode('frmMain','','','','','','','','','5','lstItem','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
										<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowEmp" class="mb-c">
						<td width="20%" height=25><asp:label id=lblEmpTag text="Employee Code (DR) :*" Runat="server"/></td>
						<td width="80%"><asp:DropDownList id="lstEmpID" Width=90% runat=server />
										<input type=button value=" ... " id="FindEmp" onclick="javascript:findcode('frmMain','','','','','','','','lstEmpID','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
										<asp:label id=lblEmpCodeErr text="<br>Please select one Employee Code" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowAcc" class="mb-c">
						<td width="20%" height=25><asp:label id="lblAccTag" Runat="server"/></td>						
						<td width="80%"><asp:DropDownList id="lstAccCode" Width=90% AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse runat=server />
										<input type=button value=" ... " id="FindAcc" onclick="javascript:findcode('frmMain','','lstAccCode','9',(hidBlockCharge.value==''? 'lstBlock': 'ddlPreBlock'),'','lstVehCode','lstVehExp','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
										<asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowChargeLevel" class="mb-c">
						<td width="20%" height="25">Charge Level :* </td>
						<td width="80%"><asp:DropDownList id="lstChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=lstChargeLevel_OnSelectedIndexChanged runat=server /> </td>
					</tr>
					<tr id="RowPreBlk" class="mb-c">
						<td width="20%" height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
						<td width="80%"><asp:DropDownList id="lstPreBlock" Width=100% runat=server />
							<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowBlk" class="mb-c">
						<td height=25><asp:label id=lblBlkTag Runat="server"/></td>
						<td><asp:DropDownList id="lstBlock" Width=100% runat=server />
							<asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowVeh" class="mb-c">
						<td height=25><asp:label id="lblVehTag" Runat="server"/> </td>
						<td><asp:DropDownList id="lstVehCode" Width=100% runat=server />
							<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowVehExp" class="mb-c">
						<td height=25><asp:label id="lblVehExpTag" Runat="server"/></td>
						<td><asp:DropDownList id="lstVehExp" Width=100% runat=server />
							<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr class="mb-c">
						<td height=25>Latest Meter Reading :</td>
						<td><asp:textbox id="txtMeter" Width=50% maxlength=15 EnableViewState=False Runat="server" />
						     <asp:RegularExpressionValidator id="RegularExpressionValidatorMeter" 
								ControlToValidate="txtMeter"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<BR>Maximum length 9 digits and 5 decimal points"
								runat="server"/>
							<asp:RangeValidator id="RangeMeter"
								ControlToValidate="txtMeter"
								MinimumValue="0"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="<BR>The value is out of acceptable range!"
								runat="server" display="dynamic"/></td>
					</tr>
					<tr class="mb-c">
						<td height=25>&nbsp;</td>
						<td>&nbsp;OR</td>
					</tr>
					<tr class="mb-c">
						<td height=25>Quantity Issued :</td>
						<td><asp:textbox id="txtQty" Width=50% maxlength=14 EnableViewState=False Runat="server" />
			                <asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
								ControlToValidate="txtQty"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<BR>Maximum length 9 digits and 5 decimal points"
								runat="server"/>
							<asp:RangeValidator id="Range1"
								ControlToValidate="txtQty"
								MinimumValue="0"
								MaximumValue="999999999999999"
								Type="double"
								EnableClientScript="True"
								Text="<BR>The value is out of acceptable range!"
								runat="server" display="dynamic"/></td>
					</tr>
					<tr class="mb-c">
						<td height=25>&nbsp;</td>
						<td>
							<asp:label id=lblerror text="Number generated is too big! " Visible=False forecolor=red Runat="server" />
							<asp:label id=lblStock text="Not enough quantity in hand!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lbleither text="Please key in either Meter Reading OR Quantity Issue." Visible=False forecolor=red Runat="server" />
							<asp:label id=lblFuelMeter text="New meter reading cannot be smaller than current reading!" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton text="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" />&nbsp;
						 <asp:ImageButton text="Save" id="Update" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnUpdate_Click" Runat="server" /></td>
					</tr>
					<tr class="mb-c">
						<td colspan=2>
                                            &nbsp;</td>
					</tr>
				</table>
				</td>		
			</tr>
            </table>


            <table style="width: 100%" class="font9Tahoma">
			<tr>
				<td colspan=3>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=3><asp:label id=lblConfirmErr text="<BR>Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:label id=lblUnDel text="Insufficient quantity in Inventory to perform operation!" Visible=False forecolor=red Runat="server" />
				</td>				
			</tr>
			<tr>
				<td colspan=3> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						OnItemCreated="DataGrid_ItemCreated" 
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
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
						<asp:TemplateColumn HeaderText="Item">
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" />
								( <%# Container.DataItem("Description") %> )					
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn >
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("AccCode") %> Visible=True id="AccCode" runat="server" />
								<asp:label text=<%# Container.DataItem("PsEmpCode") %> Visible=True id="PsEmpCode" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>				
						<asp:TemplateColumn>
							<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemTemplate>
							<asp:label text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemTemplate>
							<asp:label text=<%# Container.DataItem("VehExpCode") %> id="lblVehExpCode" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Quantity Issued">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle HorizontalAlign="Right" />			
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrxDisplay" runat="server" />	
								<asp:label text=<%# Container.DataItem("Qty") %> id="lblQtyTrx" Visible = False runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Unit Cost">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Cost")) %> id="lblUnitCost" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Cost Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Amount")) %> id="lblAmount" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>		
						<asp:TemplateColumn HeaderText="Unit Price">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Price")) %> id="lblUnitPrice" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Price Amount">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("PriceAmount")) %> id="lblPriceAmount" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>	
							
						<asp:TemplateColumn HeaderText="To Charge" Visible = False>
							<ItemTemplate>
							<asp:label text=<%# Container.DataItem("ToCharge") %> id="lblToCharge" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:TemplateColumn>		
							<ItemStyle HorizontalAlign="Right" />							
							<ItemTemplate>
								<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />														
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
								<asp:label text=<%# Container.DataItem("FuelIssueLNID") %> Visible=False id="lblID" runat="server" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
							</EditItemTemplate>								
						</asp:TemplateColumn>
						</Columns>										
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=2>&nbsp;</td>
				<td height=25><hr style="width :100%" />   </td>
			</tr>				
			<tr>
				<td colspan=2>&nbsp;</td>
				<td>
					<table border=0 width="100%" cellspacing="0" cellpadding="1" runat="server" class="font9Tahoma">
						<tr>		
							<td width="20%" height="25">&nbsp;</td>
							<td width="15%" align=right>Total Cost :</td>
							<td width="15%" align=center><asp:label id="lblTotAmtFig" runat="server" /></td>
							<td width="20%">&nbsp;</td>
							<td width="15%" align=right>Total Price :</td>
							<td width="15%" align=center><asp:label id="lblTotPriceFig" runat="server" /></td>					
						</tr>
					</table>
				</td>						
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>				
			</tr>
			<tr>
				<td>SIS Ref. Remark :</td>	
				<td colspan="2"><asp:textbox id=txtRefSIS width="20%" wrap=true maxlength="10" runat="server" /></td>
			</tr>
			<tr>
				<td>SIS Date Remark :</td>	
				<td colspan="2"><asp:textbox id=txtRefDate width="20%" wrap=true maxlength="10" runat="server" /></td>
			</tr>
			<tr>
				<td>Remarks :</td>	
				<td colspan=2><asp:textbox id="txtRemarks" width="100%" maxlength="256" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=3 class="style1"></td>				
			</tr>
			<tr>
				<td colspan="3">
					<asp:checkboxlist id="cblDisplayCost" runat="server" class="font9Tahoma">
						<asp:listitem id=option1 value="Display Unit Price in Stock Issue Slip." selected runat="server"/>
					</asp:checkboxlist>
				</td>
			</tr>
			<tr>
				<td colspan=3><comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
								<asp:label id=lblBPErr text="Unable to generate Debit Note, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								<comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
				</td>				
			</tr>
			<tr>
				<td align="left" colspan="3">
					<asp:ImageButton id="Save"      AlternateText="Save"       onClick="btnSave_Click"      ImageURL="../../images/butt_save.gif"      CausesValidation=False               runat="server" />
					<asp:ImageButton id="Confirm"   AlternateText="Confirm"    onClick="btnConfirm_Click"   ImageURL="../../images/butt_confirm.gif"   CausesValidation=False               runat="server" />
					<asp:ImageButton id="Cancel"    AlternateText="Cancel"     onClick="btnCancel_Click"    ImageURL="../../images/butt_Cancel.gif"    CausesValidation=False Visible=False runat="server" />
					<asp:ImageButton id="Print"     AlternateText="Print"      onClick="btnPrint_Click"     ImageURL="../../images/butt_print.gif"     CausesValidation=False Visible=False runat="server" />
					<asp:ImageButton id="PRDelete"  AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False               runat="server" />
					<asp:ImageButton id="DebitNote" AlternateText="Debit Note" onClick="btnDebitNote_Click" ImageURL="../../images/butt_DebitNote.gif" CausesValidation=False Visible=False runat="server" />
					<asp:ImageButton id="btnNew"    AlternateText="New"        onclick="btnNew_Click"       ImageURL="../../images/butt_new.gif"       CausesValidation=False               runat="server" />
					<asp:ImageButton id="Back"      AlternateText="Back"       onClick="btnBack_Click"      ImageURL="../../images/butt_back.gif"      CausesValidation=False               runat="server" />
				</td>
			</tr>		
			<tr>
				<td align="left" colspan="3">
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
