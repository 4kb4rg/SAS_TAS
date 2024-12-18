<%@ Page Language="vb" trace=false src="../../../include/PU_trx_DADet.aspx.vb" Inherits="PU_DADet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">

 
</script>

<html>
	<head>
		<title>Dispatch Advice Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
       <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server />
			<asp:label id=lblTo visible=false text="To " runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelectListLoc visible=false text="Please select Purchase Requisition Ref. " runat="server"/>
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />			
			<asp:label id=lblErrOnHand visible=false text="Insufficient quantity on hand" runat=server />		
			<asp:label id=lblErrOnHold visible=false text="Insufficient quantity on hold" runat=server />		
			<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
			<asp:Label id=lblHidStatus visible=false runat=server />
			<input type=hidden id=DispAdvId runat=server />
			<input type=hidden id=hidItemType runat=server />

			<table border="0" cellspacing="1" cellpadding="2" width="100%"  class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPU id=menuPU runat="server" /></td>
				</tr>			
				<tr>
					<td  class="font9Tahoma" colspan="6"><strong>DISPATCH ADVICE DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td height="15" style="width: 20%">Dispatch Advice ID :</td>
					<td width="40%"><asp:label id=lblDispAdvId runat=server /></td>
					<td style="width: 129px">&nbsp;</td>
					<td style="width: 77px">Period :</td>
					<td style="width: 121px"><asp:Label id=lblAccPeriod runat=server />&nbsp;</td>
					<td width="5%">&nbsp;</td>					
				</tr>
				<tr>
					<td height="15" style="width: 190px">Dispatch Advice Date :*</td>
					<td><asp:TextBox id=txtDispAdvDate  CssClass="fontObject" width=30% maxlength=10 runat=server />
						<a href="javascript:PopCal('txtDispAdvDate');"><asp:Image id="btnSelDate" CssClass="font9Tahoma" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
				        <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
						<asp:RequiredFieldValidator 
							id="rfvDispAdvDate" 
							runat="server" 
							ErrorMessage="<br>Please key in Dispatch Advice Date" 
							ControlToValidate="txtDispAdvDate" 
							display="dynamic"/></td>
					<td style="width: 129px">&nbsp;</td>
					<td style="width: 77px">Status :</td>
					<td style="width: 121px"><asp:Label id=lblStatus runat=server /></td>
					<td width="5%">&nbsp;</td>		
				</tr>		
				<tr>
					<td height="15" style="width: 190px">Dispatch Advice Type :</td>
					<td><asp:Label id=lblDispAdvType visible=false runat=server />
						<asp:Label id=lblDocType runat=server /></td>
					<td style="width: 129px">&nbsp;</td>
					<td style="width: 77px">&nbsp;</td>			
				</tr>			
                <tr>
                    <td height="15" style="width: 190px">
                        Dispatch Category :</td>
                    <td>
                        <asp:DropDownList id=DDLDispCat CssClass="fontObject" runat=server  AutoPostBack="True" onSelectedIndexChanged="IndexChangeCategory"   width="36%" /></td>
                    <td style="width: 129px">
                    </td>
                    <td style="width: 77px">
                    Date Created :</td>
                    <td style="width: 121px">
                    <asp:Label id=lblCreateDate runat=server /></td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td height="15" style="width: 190px">
                        Transfer Via:
                    </td>
                    <td>
                        <asp:TextBox id="txtTransporter"  width="100%" maxlength="128" CssClass="fontObject" runat="server" Height="20px" /></td>
                    <td style="width: 129px">
                    </td>
                    <td style="width: 77px">
                    </td>
                    <td style="width: 121px">
                    </td>
                    <td width="5%">
                    </td>
                </tr>
				<tr>
					<td height="15" style="width: 190px">Dispatch Advice Issued :*</td>
					<td><asp:DropDownList id=ddlDAIssue width="100%" CssClass="fontObject" runat=server />
						<asp:Label id=lblDAIssue forecolor=red visible=false runat=server /></td>
					<td style="width: 129px">&nbsp;</td>
					<td style="width: 77px">Last Update :</td>
					<td style="width: 121px"><asp:Label id=lblUpdateDate runat=server /></td>
					<td width="5%">&nbsp;</td>							
				</tr>			
				<tr>
					<td height="15" style="width: 190px">To <asp:label id=lblLocation runat=server /> Code :*</td>
					<td><asp:DropDownList id=ddlToLocCode width="100%" CssClass="fontObject" runat=server AutoPostBack="true" onSelectedIndexChanged="DispLocAddressIndex" />
						<asp:Label id=lblToLocCode forecolor=red visible=false runat=server  /></td>
					<td style="width: 129px">&nbsp;</td>
					<td style="width: 77px">Updated By :</td>
					<td style="width: 121px"><asp:Label id=lblUpdateBy runat=server /></td>		
					<td width="5%">&nbsp;</td>						
				</tr>			
                <tr>
                    <td style="width: 190px; height: 19px" valign="top">
                        Address :</td>
                    <td style="height: 19px">
                        <asp:TextBox ID="txtAddress" CssClass="fontObject" runat="server" Font-Bold="True" ForeColor="Black" Height="50px"
                            MaxLength="128" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                    <td style="width: 129px; height: 19px">
                    </td>
                    <td style="width: 77px; height: 19px">
                    </td>
                    <td style="width: 121px; height: 19px">
                    </td>
                    <td style="height: 19px" width="5%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 190px; height: 19px;">
                        PIC/Shipping Name * :</td>
                    <td style="height: 19px">
                        <asp:TextBox ID="txtPIC" CssClass="fontObject" runat="server" Font-Bold="True" ForeColor="Black" Height="20px"
                            MaxLength="128" Width="100%"></asp:TextBox></td>
                    <td style="width: 129px; height: 19px;">
                    </td>
                    <td style="width: 77px; height: 19px;">
                    Print Date :</td>
                    <td style="width: 121px; height: 19px;">
                    <asp:Label id=lblPrintDate runat=server /></td>
                    <td width="5%" style="height: 19px">
                    </td>
                </tr>
                <tr>
                    <td height="15" style="width: 190px">
                        ETD
                        <asp:label id=lblETDDeskripsi runat=server />
                        :</td>
                    <td>                        
                        <asp:TextBox id=txtETDLoc width=30% maxlength=10 CssClass="fontObject" runat=server />
                        <a href="javascript:PopCal('txtETDLoc');"><asp:Image id="btnSelETDLoc" CssClass="fontObject" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        <asp:label id=lblETDLoc Text ="Date Entered should be in the format " forecolor=Red Visible = False Runat="server"/></td>
                    <td style="width: 129px">
                    </td>
                    <td style="width: 77px">
                    </td>
                    <td style="width: 121px">
                    </td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td height="15" style="width: 190px">
                        ETA&nbsp;
                        <asp:label id=lblETADeskripsi runat=server />
                        :</td>
                    <td>
                        <asp:TextBox id=txtETALoc width=30% maxlength=10 CssClass="fontObject" runat=server />
                        <a href="javascript:PopCal('txtETALoc');"><asp:Image id="btnSelETA" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        <asp:label id=lblETALoc Text ="Date Entered should be in the format " forecolor=Red Visible = False Runat="server"/></td>
                    <td style="width: 129px">
                    </td>
                    <td style="width: 77px">
                    </td>
                    <td style="width: 121px">
                    </td>
                    <td width="5%">
                    </td>
                </tr>
                <tr>
                    <td height="15" style="width: 190px">
                        ETA
                        <asp:TextBox id=txtETALocDeskripsi width="70%" maxlength=10 runat=server BackColor="Transparent" BorderStyle="Dotted" CssClass="fontObject" >Loa Buah</asp:TextBox></td>
                    <td>
                        <asp:TextBox id=txtETAToLoc width=30% maxlength=10 CssClass="font9Tahoma" runat=server />
                        <a href="javascript:PopCal('txtETAToLoc');"><asp:Image id="btnSelETAtoLoc" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        <asp:label id=lblETAToLoc Text ="Date Entered should be in the format " forecolor=Red Visible = False Runat="server"/></td>
                    <td style="width: 129px">
                    </td>
                    <td style="width: 77px">
                    </td>
                    <td style="width: 121px">
                    </td>
                    <td width="5%">
                    </td>
                </tr>
				<tr>
					<td height="15" style="width: 190px">Dispatch From :*</td>
				    <td><asp:DropDownList id="ddlInventoryBin" CssClass="fontObject" Width=100% runat=server/>
				        <asp:Label id=lblInventoryBin text="Please Select Inventory Bin" forecolor=red visible=false runat=server /></td>
					<td style="width: 129px">&nbsp;</td>
					<td style="width: 77px"></td>
					<td style="width: 121px"></td>				
					<td width="5%">&nbsp;</td>			
				</tr>
				<tr>
					<td height="15" style="width: 190px">Warehouse From :*</td>
					<td><asp:DropDownList id="lstStorage"  CssClass="fontObject" Width="100%" runat=server/>
						<asp:Label id=lblstoragemsg text="Please Select Storage" forecolor=red visible=false runat=server /></td>
				</tr>							
				<tr>
					<td style="width: 190px;"></td>
					<td>
					<asp:TextBox ID="txtSuppCode" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="15" Width="25%" Font-Bold="True" Height="21px" ReadOnly="True" Visible="False"></asp:TextBox>
                                                <asp:TextBox ID="txtSuppName" runat="server" Font-Bold="True" ForeColor="Black" MaxLength="10"
                                                    Width="65%" Height="21px" Visible="False"></asp:TextBox>                                                
                                                <asp:Button ID="BtnFindSup1" CssClass="button-small"  AutoPostBack="True" OnClick="BtnSup1Find_Click" runat="server" Text="Find" Font-Bold=true Height="23px" Visible="False" /><br />
						<asp:Label id=lblSuppCode text="Please Select Supplier Code" forecolor=Red visible=False runat=server /></td>
				    <td style="width: 129px;">&nbsp;</td>
					<td style="width: 77px;"></td>
					<td style="width: 121px;"></td>		
					<td width="5%">&nbsp;</td>				
				</tr>			
                <tr>
                    <td style="width: 190px">
                    </td>
                    <td colspan="2" valign="top">
<asp:DataGrid ID="dgLine" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="2" GridLines="None" 
                            OnItemCommand="OnSelectItem"
                             OnItemDataBound="dgLine_BindGrid" 
                             PagerStyle-Visible="False" Width="85%" PageSize="2" Visible="False" class="font9Tahoma">	

                            <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
                            <AlternatingItemStyle CssClass="mr-r" />
                            <Columns>                            
                                <asp:BoundColumn DataField="SupplierCode" HeaderText="Supplier Code" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Name" HeaderText="Name" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                                <asp:HyperLinkColumn DataTextField="SupplierCode" HeaderText="Supplier Code" SortExpression="SupplierCode">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="CreditTerm" HeaderText="Credit Term" SortExpression="CreditTerm">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="PPNInit" HeaderText="PPN" SortExpression="PPNInit">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                            <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Button ID="BtnSup1Close" OnClick=BtnSup1Close_Click runat="server" Text="X" Font-Bold=true />
                                    </HeaderTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Visible="False" />
                        </asp:DataGrid></td>
                    <td style="width: 77px">
                    </td>
                    <td style="width: 121px">
                    </td>
                    <td width="5%">
                    </td>
                </tr>
               </table>
         <table id="Table1" width="100%" class="font9Tahoma" cellspacing="0" cellpadding="1" border="0" align="center" runat=server >
                <tr>
               <td>

				<tr>
					<td colspan="6">
						<table id=tblLine class="sub-Add" border="0" width="100%" cellspacing="0" cellpadding="0" runat=server>
							<tr>						
								<td>
									<table id=tblDoc class="mb-c" border="0" width="100%" cellspacing="0" cellpadding="2" runat=server>
										<tr>
											<td width=20% style="height: 30px">Purchase Order ID :</td>
											<td width=80% style="height: 30px"><asp:DropDownList id=ddlPOId width="50%" CssClass="fontObject"  runat=server AutoPostBack="True" onSelectedIndexChanged="POIndexChanged"/></td>
										</tr>
										<tr>
											<td height="15">Item Code :*</td>
											<td><asp:DropDownList id=ddlItemCode width="50%"  CssClass="fontObject"   runat=server AutoPostBack="True" onSelectedIndexChanged="ItemIndexChanged"/>
												<asp:Label id=lblErrItemCode text="Please select Item Code" forecolor=red visible=false runat=server /></td>
										</tr>
									    <tr>
											<td><asp:Label id=lblSelectedItemCode visible=false runat=server /></td>
											<td><asp:Label id=lblSelectedGRId visible=false runat=server /></td>									    
									    </tr>
									</table>
									<table id="tblFACode" class="mb-c" border="0" width="100%" cellspacing="0" cellpadding="2" runat=server>
										<tr>
											<td height="20">Fixed Asset Code : </td>
											<td width=80%><asp:DropDownList id=ddlFACode  CssClass="fontObject"  width="50%" onselectedindexchanged=FAItemIndexChanged autopostback=true runat=server/>
														<asp:Label id=lblErrFACode forecolor="red" visible=false runat=server/></td>
										</tr>
									</table>
									<table id="tblDoc1" class="mb-c" border="0" width="100%" cellspacing="0" cellpadding="2" runat=server>
										<tr>
											<td height="15">Purchase Requisition Ref. No. :</td>
											<td width=80%><asp:TextBox id=txtPRRefId width=50% maxlength=20   CssClass="fontObject"  runat=server /></td>
										</tr>
										<tr>
											<td height="15">Purchase Requisition Ref. <asp:label id="lblPRLocation" runat="server" /> :</td>
											<td width=80%><asp:DropDownList id=ddlPRRefLocCode width="50%"  CssClass="fontObject"  runat=server AutoPostBack="True" onSelectedIndexChanged="LocIndexChanged" /></td>
										</tr>
										<tr>
											<td height="15">Quantity Outstanding :</td>
											<td width=80%><asp:Label id=lblIDQtyReceive width="50%"  CssClass="font9Tahoma"  runat=server /><asp:Label id=lblQtyReceive Visible=False width="50%" runat=server /></td>
										</tr>
										<tr>
											<td height="15">Quantity Dispatch :*</td>
											<td width=80%><asp:TextBox id=txtQty width="20%" maxlength=15 CssClass="fontObject" runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorQty" 
													ControlToValidate="txtQty"
													ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
													Display="Dynamic"
													text="Maximum length 9 digits and 5 decimal points"
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateQty" 
													runat="server" 
													ErrorMessage="Please Specify Quantity To Dispatch" 
													ControlToValidate="txtQty" 
													display="dynamic"/>
												<asp:label id=lblErrQty text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr style="visibility:hidden">
											<td height="15">Unit Cost :*</td>
											<td width=80%><asp:TextBox id=txtCost width="20%" maxlength=19 CssClass="fontObject" runat=server />
												<asp:RegularExpressionValidator id="RegularExpressionValidatorCost" 
													ControlToValidate="txtCost"
													ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
													Display="Dynamic"
													text="Maximum length 19 digits and 2 decimal points."
													runat="server"/>
												<asp:RequiredFieldValidator 
													id="validateCost" 
													runat="server" 
													ErrorMessage="Please Specify Cost" 
													ControlToValidate="txtCost" 
													display="dynamic"/>
												<asp:label id=lblErrCost text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>
									<table id=tblAcc class="mb-c" border="0" width="100%" cellspacing="0" cellpadding="2" runat=server>									
										<tr>
											<td width=20% height=25><asp:label id=lblAccount runat=server /> (CR) :* </td>
											<td width=80%>
                                                <asp:TextBox ID="txtAccCode"  CssClass="fontObject"  runat="server" AutoPostBack="True" MaxLength="15" Width="20%"></asp:TextBox>
                                                <input id="FindAcc" visible=true class="button-small" runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                                                    type="button" value=" ... " />
                                                <asp:Button ID="BtnGetData" class="button-small"  runat="server" Font-Bold="True" 
                                                    Text="Click Here" ToolTip="Click For Refresh COA " Width="65px" />
                                                <asp:TextBox ID="txtAccName"  CssClass="fontObject"  runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Font-Bold="True" ForeColor="White" MaxLength="10" 
                                                    Width="58%"></asp:TextBox>
                                                &nbsp;
												<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/></td>
										</tr>
										<tr id="RowChargeLevel" class="mb-c">
											<td height="15">Charge Level :* </td>
											<td><asp:DropDownList id="ddlChargeLevel" Width="50%" AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged CssClass="fontObject" runat=server /> </td>
										</tr>
										<tr id="RowPreBlk" class="mb-c">
											<td height="15"><asp:label id=lblPreBlkTag Runat="server"/> </td>
											<td><asp:DropDownList id="ddlPreBlock" Width="50%" CssClass="fontObject"  runat=server />
												<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
										</tr>
										<tr id="RowBlk" class="mb-c">
											<td height=25><asp:label id=lblBlock runat=server /> :</td>
											<td><asp:DropDownList id=ddlBlock width="50%" CssClass="fontObject"  runat=server/>
												<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/></td>
										</tr>
										<tr>
											<td height=25><asp:label id=lblVehicle runat=server /> :</td>
											<td><asp:Dropdownlist id=ddlVehCode width="50%" CssClass="fontObject"  runat=server/>
												<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/></td>
										</tr>
										<tr>
											<td height=25><asp:label id=lblVehExpense runat=server /> :</td>
											<td><asp:Dropdownlist id=ddlVehExpCode width="50%" CssClass="fontObject"  runat=server/>
												<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/></td>
										</tr>
										<tr>
										    <td valign="top">Additional Note :</td>
					                        <td><asp:TextBox id=txtAddNote width="98%" CssClass="fontObject"  runat=server Height="56px" TextMode="MultiLine" /></td>
										</tr>
										<tr>
											<td colspan=2><asp:Label id=lblErrQtyDisp visible=false forecolor=red text="Quantity Issue cannot be greater than quantity outstanding/onhand" runat=server /></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colspan=2 style="height: 25px"><asp:Imagebutton id="btnAdd" OnClick="btnAdd_Click" ImageURL="../../images/butt_add.gif" AlternateText=Add UseSubmitBehavior="false" Runat="server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>
						<asp:DataGrid id=dgDADet
							AutoGenerateColumns="False" width="100%" runat="server"
							GridLines=None
							Cellpadding="2"
							Pagerstyle-Visible="False"
                            OnItemDataBound="dgLine_BindGrid" 
							OnEditCommand="DEDR_Edit"
				            OnUpdateCommand="DEDR_Update"
							OnDeleteCommand="DEDR_Delete"
							OnCancelCommand="DEDR_Cancel"
							AllowSorting="True"  >	
<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>
							
							<Columns>
								<asp:BoundColumn Visible=False DataField="DispAdvLnId" />
								<asp:BoundColumn Visible=False DataField="GoodsRcvLnId" />
								<asp:BoundColumn Visible=False DataField="ItemCode" />
								<asp:BoundColumn Visible=False DataField="QtyDisp" />

								<asp:TemplateColumn HeaderText="No">
									<ItemStyle Width="8%"/> 																								
									<ItemTemplate>
										<asp:Label  id="lblNoUrut" runat="server" />
										
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="PO ID">
									<ItemStyle Width="8%"/> 																								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("POID") %> id="lblPOId" runat="server" />
										<asp:Label Text=<%# Container.DataItem("DispAdvLnID") %> id="lblDispAdvLnId" visible=false runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item <br> Add. Note">
									<ItemStyle Width="18%"/> 																								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblItem" runat="server" /><br>
										<asp:Label Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
										<asp:TextBox id="lstAddNote" Visible=false Text='<%# trim(Container.DataItem("AdditionalNote")) %>' runat="server"/>	
									</ItemTemplate>
								</asp:TemplateColumn>						
								<asp:TemplateColumn HeaderText="PR Ref. No <br>PR Ref. Loc">
									<ItemStyle Width="8%"/> 																								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRID") %> id="lblPRRefNo" runat="server" />
										<asp:Label Text=<%# Container.DataItem("PRLocCode") %> id="lblPRLocCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/> 																																
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/> 																								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("BlkCode") %> id="lblBlkCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/> 																								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="10%"/> 																								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("VehExpenseCode") %> id="lblVehExpCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>																						
								<asp:TemplateColumn HeaderText="UOM">
									<ItemStyle Width="5%"/> 																								
									<ItemTemplate> 
										<asp:Label Text=<%# Container.DataItem("PurchaseUOM") %> id="lblUOMCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>							
								<asp:TemplateColumn HeaderText="Quantity Dispatch">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width=5% HorizontalAlign="Right" /> 
									<ItemTemplate>										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("QtyDisp"), 5), 5) %> id="lblQtyDisp" runat="server" />							
									</ItemTemplate>
								</asp:TemplateColumn>							
								<asp:TemplateColumn HeaderText="Unit Cost" Visible="False">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width=5% HorizontalAlign="Right" /> 
									<ItemTemplate>										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Cost"), 2), 2) %> id="lblCost" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>							
								<asp:TemplateColumn HeaderText="Amount" Visible="False">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width=5% HorizontalAlign="Right" /> 
									<ItemTemplate>										
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 2)  %> id="lblAmount" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>										
								<asp:TemplateColumn>		
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width=4% HorizontalAlign="Right" /> 								
									<ItemTemplate>
										<asp:LinkButton id="Delete" CommandName="Delete" CausesValidation=False Text="Delete" runat="server"/>
										<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
									    <asp:LinkButton id="Update" CommandName="Update" Text="Update" CausesValidation=False  runat="server"/>
									    <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>					
							</Columns>										

<PagerStyle Visible="False"></PagerStyle>
						</asp:DataGrid>
					</td>	
				</tr>
				<tr>
					<td colspan=3></td>
					<td colspan=2 height=25>&nbsp;</td>
					<td width="5%">&nbsp;</td>					
				</tr>
				<tr style="visibility:hidden">
					<td colspan=3>&nbsp;</td>
					<td height=25 style="width: 77px">Total Amount</td>
					<td align=right style="width: 121px"><asp:Label ID=lblTotalAmount Runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="15" style="width: 190px">Remarks :</td>	
					<td colspan="5"><asp:TextBox id="txtRemark" width="98%" maxlength="128" CssClass="fontObject" runat="server" /></td>
				</tr>
				<tr>
					<td height="15" style="width: 190px">&nbsp;</td>
					<td>&nbsp;</td>
					<td style="width: 129px">&nbsp;</td>
					<td style="width: 77px">&nbsp;</td>
					<td style="width: 121px">&nbsp;</td>
				</tr>
				<!-- PU Bugs START -->
				<tr>
					<td colspan='6'>
						<asp:label id=lblErrOutStandingQty forecolor=red visible=false text="Insufficient quantity on hand for PO ID: " runat=server />
						<asp:label id=lblErrPOID forecolor=red visible=false runat=server /><br>
						<asp:label id=lblOutStandingQtyMsg forecolor=red visible=false text="Quantity Outstanding: " runat=server />
						<asp:label id=lblOutStandingQty forecolor=red visible=false runat=server />
						<asp:label id=lblPBBKB forecolor=red visible=false runat=server />
					</td>
				</tr>
				<!-- PU Bugs END -->	
				<tr>
					<td align="left" colspan="6">
					    <asp:ImageButton id="btnNew" UseSubmitBehavior="false" onClick="btnNew_Click" imageurl="../../images/butt_new.gif" AlternateText="New" runat=server/>
						<asp:ImageButton id="btnSave" UseSubmitBehavior="false" onClick="btnSave_Click" ImageUrl="../../images/butt_save.gif" AlternateText=Save CausesValidation=False runat="server" />
						<asp:ImageButton id="btnConfirm" UseSubmitBehavior="false" onClick="btnConfirm_Click" ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm CausesValidation=False runat="server" />
						<asp:ImageButton id="btnPrint" UseSubmitBehavior="false" onClick="btnPreview_Click" ImageUrl="../../images/butt_print.gif" AlternateText=Print CausesValidation=False runat="server" />
						<asp:ImageButton id="btnDelete" UseSubmitBehavior="false" onClick="btnDelete_Click" ImageUrl="../../images/butt_delete.gif" AlternateText=Delete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnUndelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnEdited" UseSubmitBehavior="false" onClick="btnEdited_Click" ImageUrl="../../images/butt_edit.gif" AlternateText=Edit CausesValidation=False runat="server" />
						<asp:ImageButton id="btnCancel" UseSubmitBehavior="false" onClick="btnCancel_Click" ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancel CausesValidation=False runat="server" />
						<asp:ImageButton id="btnBack" UseSubmitBehavior="false" onClick="btnBack_Click" ImageUrl="../../images/butt_back.gif" AlternateText=Back CausesValidation=False runat="server" />
					</td>
				</tr>		
				
				
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr id=TrLink runat=server >
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
				    <td style="width: 190px">&nbsp;</td>								
				    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
				    <td style="width: 129px">&nbsp;</td>	
				    <td align=right style="width: 77px"><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
				    <td style="width: 121px">&nbsp;</td>		
				    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
			    </tr>
        </td>
        </tr>
	    </table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<asp:label id=lblCurrentPeriod visible=false runat=server />
                </div>
            </td>
            </tr>
        </table>
		</form>
	</body>
</html>
