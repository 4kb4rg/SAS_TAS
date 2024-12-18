<%@ Page Language="vb" trace="False" codefile="../../../include/IN_Trx_StockIssue_Details.aspx.vb" Inherits="IN_IssueDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Stock Issue Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
 <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 221px;
            }
            .style5
            {
                font-size: 9pt;
                font-family: Tahoma;
                width: 299px;
            }
        </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
                
        <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id=lblTxLnID visible=false runat=server />
		<asp:label id=lblOldQty visible=false runat=server />
		<asp:label id=lblOldItemCode visible=false runat=server />
		
        		
		<table border=0 width="100%" cellspacing="0" cellpading="1" class="font9Tahoma">
			<tr>
				<td colspan=6><UserControl:MenuINTrx EnableViewState=False id=menuIN runat="server" />
                     <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="style5">
                             <strong><asp:label id="lblStkName" runat="server"/></strong></td>
                            <td class="font9Header">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
			</tr>			
			<tr>
				<td class="font9Header" colspan=6 style="height: 16px">Period : <asp:Label id=lblAccPeriod runat=server />| Status : <asp:Label id=Status runat=server /> | Date Created : <asp:Label id=CreateDate runat=server />| Last Update : <asp:Label id=UpdateDate runat=server />| 
                                Update By : <asp:Label id=UpdateBy runat=server />| Print Date : <asp:Label id=lblPrintDate runat=server />| Debit Note ID :<asp:Label id=lblDNNoteID  runat=server />
                            </td>
			</tr>
			<tr>
				<td height=25 class="style1"><asp:label id="lblStkID" runat="server"/> :</td>
				<td width="40%"><asp:label id=lblStckTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="20%">&nbsp;</td>
				<td width="5%"><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>
			</tr>
			<tr>
				<td class="style1">
					<asp:label id=lblBillTo text="Bill To :*" Visible=False Runat="server"/>
					<asp:label id=lblBPartyTag Visible=False Runat="server"/>
					<asp:label id=lblLocationTag Text="Charge To :*" Visible=False Runat="server"/>
				</td>
				<td>
					<asp:DropDownList id="lstBillTo" CssClass="font9Tahoma" Width=100% Visible=False runat=server /><br />
					<asp:DropDownList id="lstBillParty" CssClass="font9Tahoma" Width=100% Visible=False runat=server />
					<asp:DropDownList id="ddlLocation" CssClass="font9Tahoma" Width=100% Visible=False AutoPostBack=True OnSelectedIndexChanged=ddlLocation_OnSelectedIndexChanged runat=server /> 
					<asp:label id=lblBillPartyErr Visible=False forecolor=red Runat="server" />
					<asp:label id=lblLocationErr Visible=False forecolor=red Runat="server" />
				</td>			
				<td></td>
				<td></td>
				<td></td>
				<td></td>
			</tr>			
			<tr>
				<td height=25 class="style1"><asp:label id=lblChargeMarkUp visible=False text="Charge :" runat=server /></td>
				<td><asp:CheckBox id=chkMarkUp visible=false Checked =False Runat="server"/></td>
				<td>&nbsp;</td>
			    <td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>			
			
			<tr>
				<td class="style1">Issue Date :*</td>
				<td><asp:TextBox id=txtDate Width="30%" CssClass="font9Tahoma" maxlength=10 runat=server />
					<a href="javascript:PopCal('txtDate');">
					<asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="Please specify reference date!" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
					<asp:label id=lblDate Text="Date Entered should be in the format" forecolor=red Visible=false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td></td>	
				<td></td>
				<td></td>	
				<td></td>
			</tr>
			
			<tr>
				<td class="style1"><asp:label visible=false text="Stock Issue Type :" runat=server /></td>
				<td><asp:label id=lblStkIssType visible=false Runat="server"/></td>
				<td></td>
				<td></td>
				<td></td>				
				<td></td>
			</tr>
			<tr>
				<td height=25 class="style1">Issue From :*</td>
				<td><asp:DropDownList id="ddlInventoryBin" CssClass="font9Tahoma" Width=100% runat=server AutoPostBack=false OnSelectedIndexChanged="ddlInventoryBin_OnSelectedIndexChanged"/>
				    <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 class="style1">Warehouse From :*</td>
				<td><asp:DropDownList id="lstStorage"  CssClass="fontObject" Width="100%" runat=server/>
				    <asp:Label id=lblstoragemsg text="Please Select Storage" forecolor=red visible=false runat=server /></td>
            </tr>

         </table>

        <table border=0 width="100%" cellspacing="0" cellpading="1">
        <tr>
      
				<td colspan=6>
				<table id="tblAdd"  class="sub-Add" border=0 width="100%" cellspacing="0" cellpadding="2" runat="server">
					<tr class="mb-c">
						<td style="width: 249px;">Item Code :*</td>
						<td width="80%">
                            <asp:DropDownList id="lstItem" CssClass="font9Tahoma" Width="92%" 
                                AutoPostBack=false OnSelectedIndexChanged=ValidateBlkBatch runat=server 
                                EnableViewState=True />
                            <br />
                            <asp:TextBox ID="txtItemCode" CssClass="font9Tahoma" runat="server" AutoPostBack="False"  MaxLength="15" 
                                Width="25%"></asp:TextBox>&nbsp;<asp:TextBox ID="TxtItemName" 
                                CssClass="font9Tahoma" runat="server" Font-Bold="True" MaxLength="10" 
                                Width="64%"></asp:TextBox>
&nbsp;<input type=button value=" ... " id="Find" class="button-small" onclick="javascript:PopItem_New('frmMain', '', 'txtItemCode','TxtItemName','txtCost', 'False');" CausesValidation=False runat=server />&nbsp;
                            <br />
										<asp:label id=lblItemCodeErr text="Please select one Item" Visible=False forecolor=Red Runat="server" />
						</td>
					</tr>
					<tr id="RowEmp"  >
						<td style="height: 51px; width: 249px;">Employee Code (DR) :*</td>
						<td width="80%" style="height: 51px"><asp:DropDownList id="lstEmpID" CssClass="font9Tahoma"  Width=90% runat=server />
										<input type=button value=" ... " id="FindEmp" onclick="javascript:findcode('frmMain','','','','','','','','lstEmpID','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
										<asp:label id=lblEmpCodeErr text="Please select one Employee Code" Visible=False forecolor=Red Runat="server" />
						</td>
					</tr>
					<tr id="RowAcc" class="mb-c" >
						<td style="height: 27px; width: 249px;" ><asp:label id="lblAccTag" Runat="server"/></td>
						<td width="80%" style="height: 27px">
							<telerik:RadComboBox   CssClass="fontObject" ID="radcmbCOA"  AutoPostBack="true"
								OnSelectedIndexChanged=SelectedCOA_OnSelectedIndexChanged
								Runat="server" AllowCustomText="True" 
								EmptyMessage="Please Select COA" Height="200" Width="95%" 
								ExpandDelay="50" Filter="Contains" Sort="Ascending" 
								EnableVirtualScrolling="True">
								<CollapseAnimation Type="InQuart" />
							</telerik:RadComboBox>							
										<asp:label id=lblAccCodeErr text="Please select one Account Code" Visible=False forecolor=Red Runat="server" />
						</td>
					</tr>
					<tr id="RowChargeLevel" class="mb-c">
						<td height="25" style="width: 249px">Charge Level :* </td>
						<td width="80%"><asp:DropDownList id="lstChargeLevel" CssClass="font9Tahoma" 
                                Width="95%" AutoPostBack=True 
                                OnSelectedIndexChanged=lstChargeLevel_OnSelectedIndexChanged runat=server /> <asp:label id=LblChargeLevel text="<br>Please select one Charge Level" Visible=False forecolor=Red Runat="server" /></td>
					</tr>
					<tr id="RowPreBlk" class="mb-c">
						<td height="25" style="width: 249px"><asp:label id=lblPreBlkTag Runat="server"/> </td>
						<td width="80%"><asp:DropDownList id="lstPreBlock" CssClass="font9Tahoma" 
                                Width="95%" AutoPostBack=False OnSelectedIndexChanged=BindPreBlkBatchNo 
                                runat=server />
							<asp:label id=lblPreBlockErr text="<br>Please select one Block Code" Visible=False forecolor=red Runat="server" /></td>
					</tr>
					<tr id="RowBlk" class="mb-c" >
						<td height=25 style="width: 249px"><asp:label id=lblBlkTag Runat="server"/></td>
						<td width="80%">
								<telerik:RadComboBox   CssClass="fontObject" ID="lstBlock"  AutoPostBack="true"
									OnSelectedIndexChanged=BindBlkBatchNo
									Runat="server" AllowCustomText="True" 
									EmptyMessage="Please Select Machine/Block Code" Height="200" Width="95%" 
									ExpandDelay="50" Filter="Contains" Sort="Ascending" 
									EnableVirtualScrolling="True">
									<CollapseAnimation Type="InQuart" />
								</telerik:RadComboBox>	

						<asp:label id=lblBlockErr text="<br>Please select one Block Code" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowBatchNo" class="mb-c">
						<td Height="25" style="width: 249px"><asp:Label ID="lblBatchNoTag" Runat="Server" /> :*</td>
						<td width="80%"><asp:DropDownList id="lstBatchNo" CssClass="font9Tahoma" 
                                Width="95%" runat=server />
                            TxtItemCode.Attributes.Add("readonly", "readonly")
							<asp:label id=lblBatchNoErr text="<br>Please select one Block Code" Visible=False forecolor=red Runat="server" /></td>
					</tr>			
					<tr id="RowVeh" class="mb-c" >
						<td height=25 style="width: 249px"><asp:label id="lblVehTag" Runat="server"/> </td>
						<td width="80%">
							<telerik:RadComboBox   CssClass="fontObject" ID="lstVehCode" 								
								Runat="server" AllowCustomText="True" 
								EmptyMessage="Please Select Vehicle" Height="200" Width="95%" 
								ExpandDelay="50" Filter="Contains" Sort="Ascending" 
								EnableVirtualScrolling="True">
								<CollapseAnimation Type="InQuart" />
							</telerik:RadComboBox>				
							<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr id="RowVehExp" class="mb-c" >
						<td height=25 style="width: 249px"><asp:label id="lblVehExpTag" Runat="server"/> </td>
						<td width="80%"><asp:DropDownList id="lstVehExp" CssClass="font9Tahoma" Width="95%" 
                                runat=server />
										<asp:label id=lblVehExpCodeErr Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
						<td height="25" style="width: 249px">Quantity to Issue :*</td>
						<td width="80%">							
							<telerik:RadNumericTextBox ID="txtQty"   CssClass="font9Tahoma"    Runat="server" LabelWidth="64px">     <NumberFormat ZeroPattern="n"></NumberFormat>
							<EnabledStyle HorizontalAlign="Right" />
							</telerik:RadNumericTextBox>							
							<asp:label id=lblErrNum text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
							<asp:label id=lblErrStock text="Not Enough Stock in Hand!" Visible=False forecolor=red Runat="server" />													
							<EnabledStyle HorizontalAlign="Right" />
							</telerik:RadNumericTextBox>									
						</td>
					</tr>
					<tr id="RowCost" class="mb-c" style="visibility:hidden">
						<td height="25" style="width: 249px">Unit Cost :*</td>
						<td><asp:TextBox id=txtCost CssClass="font9Tahoma" width="25%" maxlength=21 runat=server />
						
							<asp:label id=lblErrCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
						</td>
					</tr>
					<tr class="mb-c">
					    <td valign=top style="width: 249px">Additional Note :</td>
                        <td><textarea rows=6 id=txtAddNote runat=server style="width: 416px"></textarea></td>
					</tr>
					<tr class="mb-c">
						<td colspan=2><asp:ImageButton text="btnAdd" id="btnAdd" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" />&nbsp;
						 <asp:ImageButton text="Save" id="btnUpdate" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnUpdate_Click" UseSubmitBehavior="false" Runat="server" /></td>
					</tr>
				</table>
				</td>		
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr>
				<td colspan=6><asp:label id=lblConfirmErr text="<BR>Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" />
							  <asp:label id=lblUnDel text="<BR>Insufficient stock in Inventory to perform operation!" Visible=False forecolor=red Runat="server" /></td>
			</tr>
            		
			<tr>
				<td colspan=6> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="False" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = None
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
						AllowSorting="True" class="font9Tahoma">	
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
					<asp:TemplateColumn HeaderText="Item &lt;br&gt; Additional Note">
							<ItemTemplate>
							<asp:label text=<%# Container.DataItem("StockIssueLnID") %> Visible=false id="LnID" runat="server" />
							<asp:label text=<%# Container.DataItem("ItemCode") %> id="ItemCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("Description") %> id="Description" runat="server" />
							
                                <br />
							<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>							
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AccCode") %> Visible=True id="AccCode" runat="server" />
							<asp:label text=<%# Container.DataItem("PsEmpCode") %> Visible=True id="PsEmpCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("AccDescr") %> Visible=True id="AccDescr" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("BlkDesc") %> Visible=True id="BlkDescr" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("BatchNo") %> id="lblBatchNo" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>	
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" /><br />
							<asp:label text=<%# Container.DataItem("VehName") %> id="lblVehName" runat="server" />
							<asp:label text=<%# Container.DataItem("VehExpCode") %> id="lblVehExpCode" runat="server" />
							
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Quantity Issued">
						<ItemStyle HorizontalAlign="Right" />			
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemTemplate>
							<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblQtyTrxDisplay" runat="server" />							
							<asp:label text=<%# Container.DataItem("Qty") %> id="lblQtyTrx" visible = "false" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Unit Cost" Visible="False">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />							
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2)%> id="lblIDUnitCost" runat="server" />
							<asp:label text=<%# Container.DataItem("Cost")%> id="lblUnitCost" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Cost Amount" Visible="False">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2)%> id="lblIDAmount" runat="server" />
							<asp:label text=<%# Container.DataItem("Amount")%> id="lblAmount" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>		
					<asp:TemplateColumn HeaderText="Unit Price" Visible=False>
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />							
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Price"), 2), 2) %> id="lblIDUnitPrice" Visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("Price")%> id="lblUnitPrice" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Price Amount" Visible=False>
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("PriceAmount"), 2), 2) %> id="lblIDPriceAmount" Visible=false runat="server" />
							<asp:label text=<%# Container.DataItem("PriceAmount")%> id="lblPriceAmount" visible=false runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>		
					<asp:TemplateColumn HeaderText="To Charge" Visible = False>
						<ItemTemplate>
						<asp:label text=<%# Container.DataItem("ToCharge") %> id="lblToCharge" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>		
						<ItemStyle HorizontalAlign="Right" Width="5%" />							
						<ItemTemplate>
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" visible=false CausesValidation =False runat="server" />												
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" visible=false CausesValidation =False runat="server" />												
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					</Columns>										
                        <PagerStyle Visible="False" />
					</asp:DataGrid>
				</td>
			</tr>					
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=3 height=25><hr style="width : 100%" /></td>
			</tr>				
			<tr style="visibility:hidden">
				<td colspan=3>&nbsp;</td>
				<td colspan=3>
					<table border=0 width="100%" cellspacing="0" cellpadding="1" runat="server" id="Table1" class="font9Tahoma">
						<tr>
							<td width="20%" height="25">&nbsp;</td>
							<td width="15%" align=right><asp:label id="lblTotCost" Runat="server"/></td>
							<td width="15%" align=center><asp:label id="lblTotAmtFig" runat="server" /></td>
							<td width="20%">&nbsp;</td>
							<td width="15%" align=right>Total Price : </td>
							<td width="15%" align=center><asp:label id="lblTotPriceFig" runat="server" /></td>					
						</tr>
					</table>
				</td>						
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr  class="font9Tahoma">
				<td height=25 style="width: 225px" >SIS Reference Remark :</td>	
				<td colspan="5"><asp:textbox id=txtRefSIS CssClass="font9Tahoma" width="50%" wrap=true maxlength="50" runat="server" /></td>
			</tr>
			<tr  class="font9Tahoma"> 
				<td height=25 style="width: 225px" class="font9Tahoma">SIS Date Remark :</td>	
				<td colspan="5"><asp:textbox id=txtRefDate CssClass="font9Tahoma" width="50%" wrap=true maxlength="10" runat="server" /></td>
			</tr>
			<tr  class="font9Tahoma">
				<td style="width: 225px; height: 25px;">General Remarks :</td>	
				<td colspan="5" style="height: 25px"><asp:textbox id="txtRemarks" 
                        CssClass="font9Tahoma" width="95%" wrap=true maxlength="256" runat="server" /></td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>				
			</tr>
			<tr  class="font9Tahoma">
				<td colspan="6" >
					<asp:checkboxlist id="cblDisplayCost" class="font9Tahoma" runat="server">
						<asp:listitem id=option1 value="Display Unit Price in Stock Issue Slip." selected runat="server"/>
					</asp:checkboxlist>
				</td>
			</tr>
			<tr>
				<td colspan=6  class="font9Tahoma"><comment></comment>
								<asp:label id=lblBPErr text="Unable to generate Debit Note, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								<comment></comment>
				</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
				    <asp:ImageButton id="btnNew"       UseSubmitBehavior="false" AlternateText="New"        onclick="btnNew_Click"       ImageURL="../../images/butt_new.gif"       CausesValidation=False runat="server" />
					<asp:ImageButton id="btnSave"      UseSubmitBehavior="false" AlternateText="Save"       onClick="btnSave_Click"      ImageURL="../../images/butt_save.gif"      CausesValidation=False runat="server" />
					<asp:ImageButton id="btnConfirm"   UseSubmitBehavior="false" AlternateText="Confirm"    onClick="btnConfirm_Click"   ImageURL="../../images/butt_confirm.gif"   CausesValidation=False runat="server" />
					<asp:ImageButton id="btnCancel"	   UseSubmitBehavior="false" AlternateText="Cancel"     onClick="btnCancel_Click"    ImageURL="../../images/butt_Cancel.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="btnPrint"     UseSubmitBehavior="false" AlternateText="Print"      onClick="btnPreview_Click"   ImageURL="../../images/butt_print.gif"     CausesValidation=False runat="server" />
					<asp:ImageButton id="btnDelete"    UseSubmitBehavior="false" AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False runat="server" />
					<asp:ImageButton id="btnDebitNote" UseSubmitBehavior="false" AlternateText="Debit Note" onClick="btnDebitNote_Click" ImageURL="../../images/butt_debitnote.gif" CausesValidation=False runat="server" />
					<asp:ImageButton id="btnBack"      UseSubmitBehavior="false" AlternateText="Back"       onClick="btnBack_Click"      ImageURL="../../images/butt_back.gif"      CausesValidation=False runat="server" />
				</td>
			</tr>		
			<tr>
				<td colspan=5>
                    &nbsp;</td>
			</tr>
							
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr id=TrLink  class="font9Tahoma" runat=server style="visibility:hidden">
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
			    <td style="width: 225px">&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>
		    
			<input type=hidden id=hidPQID runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="AccountCode" Visible="False" Text= "Account Code" Runat="server"></asp:label>
			<asp:label id="BillParty" Visible="False" Text= "Bill Party" Runat="server"></asp:label>
			<asp:label id="EmployeeCode" Visible="False" Text= "Employee Code" Runat="server"></asp:label>
			<asp:label id="issueType" Visible="false" Runat="server"></asp:label>
			<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />			
			<asp:label id="lblStatusHid" Visible="False" Runat="server"></asp:label>
			<asp:Label id="lblBlockHidden" visible=false runat=server />
			<asp:Label id="lblBatchHidden" visible=false runat=server />
 
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
            <asp:TextBox ID="txtunit" CssClass="font9Tahoma" runat="server"  AutoPostBack="True" BackColor="Transparent"
                BorderColor="Transparent" MaxLength="15" Width="25%"></asp:TextBox>
            </td>
            </tr>
            </table>

            </div>
            </td>
            </tr>
            </table>
			 <asp:ScriptManager ID="ScriptManager1" runat="server">
			</asp:ScriptManager>			
		</form>
	</body>
</html>
