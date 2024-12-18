<%@ Page Language="vb" src="../../../include/NU_trx_SeedlingsIssue_details.aspx.vb" Inherits="NU_trx_SeedlingsIssueDetails" %>
<%@ Register TagPrefix="UserControl" TagName="MenuNUTrx" src="../../menu/menu_NUtrx.ascx"%>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>NURSERY - Seedlings Issue Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" Runat="Server" />
	</head>
	<script Language="javascript">
	    function calAmount(i) {
			var doc = document.frmMain;
			var a = parseFloat(doc.txtQty.value);
			var b = parseFloat(doc.txtCost.value);
			var c = parseFloat(doc.txtAmount.value);

			if ((i == '1') || (i == '2')) {
				if ((doc.txtQty.value != '') && (doc.txtCost.value != ''))
					doc.txtAmount.value = round(a * b, 2);
				else
					doc.txtAmount.value = '';
			}
			
			if (i == '3') {
				if ((doc.txtQty.value != '') && (doc.txtAmount.value != ''))
					doc.txtCost.value = round(c / a, 5);
				else
					doc.txtCost.value = '';
			}

			if (doc.txtQty.value == 'NaN')
				doc.txtQty.value = '';

			if (doc.txtCost.value == 'NaN')
				doc.txtCost.value = '';
				
			if (doc.txtAmount.value == 'NaN')
				doc.txtAmount.value = '';
		}
	</script>
	<body>
		<form ID="frmMain" Runat="Server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label ID="lblCode" Visible="False" Text=" Code" Runat="Server" />
			<asp:Label ID="lblSelect" Visible="False" Text="Select " Runat="Server" />
			<asp:Label ID="lblPleaseSelect" Visible="False" Text="Please select " Runat="Server" />
			<table Border="0" Width="100%" CellSpacing="0" CellPadding="1" Runat="Server" class="font9Tahoma">
				<tr>
					<td ColSpan="6"><UserControl:MenuNUTrx ID="menuNUTrx" EnableViewState="False" Runat="Server" /></td>
				</tr>
				<tr>
					<td Class="mt-h" ColSpan="6">SEEDLINGS ISSUE DETAILS</td>
				</tr>
				<tr>
					<td ColSpan="6"><hr Size="1" NoShade></td>
				</tr>
				<tr>
					<td Width="20%" Height="25">Issue ID :</td>
					<td Width="30%"><asp:Label ID="lblIssueID" Runat="Server" /></td>
					<td Width="5%">&nbsp;</td>
					<td Width="15%">Period :</td>
					<td Width="25%"><asp:Label ID="lblPeriod" Runat="Server" /></td>
					<td Width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Document Ref. No. :*</td>
					<td><asp:TextBox ID="txtDocRefNo" Width="100%" MaxLength="32" Runat="Server" />
					    <asp:Label ID="lblDocRefNoErr" Visible="False" Text="Doc Ref No cannot be blank!" ForeColor="Red" Runat="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label ID="lblStatus" Runat="Server" /><asp:Label ID="lblStatusCode" Visible="False" Runat="Server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Issue Date :*</td>
					<td><asp:TextBox ID="txtIssueDate" Width="50%" MaxLength="10" Runat="Server" />
					    <a href="javascript:PopCal('txtIssueDate');"><asp:Image ID="imgIssueDate" ImageUrl="../../Images/calendar.gif" Runat="Server" /></a> 
					    <asp:Label ID="lblIssueDateErr" Visible="False" ForeColor="Red" Runat="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label ID="lblCreateDate" Runat="Server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25"><asp:Label ID="lblNurseryBlockTag" Runat="Server" /> :*</td>
					<td><asp:DropDownList ID="ddlNurseryBlock" Width="100%" AutoPostBack=True OnSelectedIndexChanged=ddlNurseryBlock_OnSelectedIndexChanged Runat="Server" />
					    <asp:Label ID="lblNurseryBlockErr" Visible="False" ForeColor="Red" Runat="Server" />
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label ID="lblUpdateDate" Runat="Server" /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        Remarks:</td>
					<td><asp:TextBox ID="txtRemark" Width="100%" MaxLength="512" Runat="Server" /></td>
					<td></td>
					<td>Updated By :</td>
					<td>&nbsp;<asp:Label ID="lblUpdateID" Runat="Server" /></td>
				</tr>
                </table>

                 <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
				<tr>
					<td ColSpan="6">
						<table ID="tblAdd" Border=0 Width="100%" CellSpacing="0" CellPadding="4" Runat="Server">
							<tr class="mb-c">
								<td width="20%" height=25>Item Code :*</td>
								<td width="80%" ColSpan="4"><asp:DropDownList id="lstItem" Width=90% AutoPostBack=True OnSelectedIndexChanged=lstItem_OnSelectedIndexChanged runat=server EnableViewState=True />
										<input type=button value=" ... " id="Find"  onclick="javascript:findcode('frmMain','','','','','','','','','1','lstItem','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation=False runat=server />
										<asp:label id=lblItemCodeErr text="<br>Please select one Nursery Item" Visible=False forecolor=red Runat="server" />
								</td>
							</tr>
							<tr Class="mb-c">
								<td Width="20%" Height="25">Description :</td>
								<td Width="80%" ColSpan="4"><asp:TextBox ID="txtDescription" Width="100%" Runat="Server" /></td>
							</tr>
							<tr Class="mb-c" >
								<td Width="20%" Height="25"><asp:Label ID="lblBatchNoTag" Runat="Server" /> :*</td>
								<td Width="80%" ColSpan="4"><asp:DropDownList ID="ddlBatchNo" Width="100%" Runat="Server" />
									<asp:Label ID="lblBatchNoErr" Visible="False" ForeColor="Red" Runat="Server" /></td>
							</tr>
							<tr ID=trChargeTo Class="mb-c">
							    <td Width="20%" Height="25">Charge To :*</td>
							    <td Width="80%">
							        <asp:DropDownList ID="ddlChargeTo" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeTo_OnSelectedIndexChanged runat=server /> 
            					    <asp:label ID=lblChargeToErr Visible=False ForeColor=red Runat="server" />
			    		        </td>
						    </tr>
							<tr Class="mb-c" >
								<td Width="20%" Height="25"><asp:Label ID="lblAccCodeTag" Runat="Server" /> :*</td>
								<td Width="80%" ColSpan="4"><asp:DropDownList ID="ddlAccCode" Width="90%" AutoPostBack="True" OnSelectedIndexChanged="ddlAccCode_OnSelectedIndexChanged" Runat="Server" />
												<input Type=button Value=" ... " ID="btnFindAccCode" OnClick="javascript:findcode('frmMain','','ddlAccCode','9',(hidBlockCharge.value==''? 'ddlBlkCode': 'ddlPreBlkCode'),'','ddlVehCode','ddlVehExpCode','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation="False" Runat="Server" />
												<br><asp:Label ID="lblAccCodeErr" Visible="False" ForeColor="Red" Runat="Server" /></td>
							</tr>
							<tr ID="trChargeLevel" Class="mb-c" >
								<td Width="20%" Height="25">Charge Level :*</td>
								<td Width="80%" ColSpan="4"><asp:DropDownList ID="ddlChargeLevel" Width="100%" AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged Runat="Server" /> </td>
							</tr>
							<tr ID="trPreBlkCode" Class="mb-c" >
								<td Width="20%" Height="25"><asp:Label ID="lblPreBlkCodeTag" Runat="Server" /> :</td>
								<td Width="80%" ColSpan="4"><asp:DropDownList ID="ddlPreBlkCode" Width="100%" Runat="Server" />
									<asp:Label ID="lblPreBlkCodeErr" Visible="False" ForeColor="Red" Runat="Server" /></td>
							</tr>
							<tr ID="trBlkCode" Class="mb-c" >
								<td Width="20%" Height="25"><asp:Label ID="lblBlkCodeTag" Runat="Server" /> :</td>
								<td Width="80%" ColSpan="4"><asp:DropDownList ID="ddlBlkCode" Width="100%" Runat="Server" />
									<asp:Label ID="lblBlkCodeErr" Visible="False" ForeColor="Red" Runat="Server" /></td>
							</tr>
							<tr Class="mb-c" >
								<td Width="20%" Height="25"><asp:Label ID="lblVehCodeTag" Runat="Server" /> :</td>
								<td Width="80%" ColSpan="4"><asp:DropDownList ID="ddlVehCode" Width="100%" Runat="Server" />
									<asp:Label ID="lblVehCodeErr" Visible="False" ForeColor="Red" Runat="Server" /></td>
							</tr>
							<tr Class="mb-c" >
								<td Width="20%" Height="25"><asp:Label ID="lblVehExpCodeTag" Runat="Server" /> :</td>
								<td Width="80%" ColSpan="4"><asp:DropDownList ID="ddlVehExpCode" Width="100%" Runat="Server" />
									<asp:Label ID="lblVehExpCodeErr" Visible="False" ForeColor="Red" Runat="Server" /></td>
							</tr>					
							<tr Class="mb-c">
							    <td Width="20%" Height="25">Quantity - Seedlings :*</td>
							    <td Width="80%">
								    <asp:TextBox ID="txtQty" Width="50%" OnKeyUp="javascript:calAmount('1');" MaxLength="20" Runat="Server" />
								    <asp:RequiredFieldValidator ID="rfvQty"
								        ControlToValidate="txtQty"
									    Display="Dynamic"
									    Text = "Quantity - Seedlings cannot be blank"
									    Runat="Server" />
								    <asp:RegularExpressionValidator ID="revQty" 
									    ControlToValidate="txtQty"
									    ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									    Display="Dynamic"
									    Text="Maximum length 9 digits and 5 decimal points"
									    Runat="Server" />
								    <asp:RangeValidator ID="rvQty"
									    ControlToValidate="txtQty"
									    MinimumValue="0.00001"
									    MaximumValue="999999999999999"
									    Type="Double"
									    EnableClientScript="True"
									    Text="The value is out of acceptable range!"
									    Display="Dynamic"
									    Runat="Server" />
									    <asp:Label ID="lblQtyErr" Text="<br>Quantity - Seedlings cannot be blank!" Visible="False" ForeColor="Red" Runat="Server" />
							    </td>
						    </tr>
						    <tr Class="mb-c">
							    <td Height="25">Unit Cost :*</td>
							    <td>
								    <asp:TextBox ID="txtCost" Width="50%" OnKeyUp="javascript:calAmount('2');" MaxLength="20" Runat="Server" />
								
							    </td>
						    </tr>
						    <tr Class="mb-c">
							    <td Height="25">Amount :*</td>
							    <td>
								    <asp:TextBox ID="txtAmount" Width=50% OnKeyUp="javascript:calAmount('3');" MaxLength=20 Runat="Server" />														
							    </td>
						    </tr>
							<tr Class="mb-c">
								<td ColSpan=5><asp:ImageButton Text="Add" ID="ibAdd" ImageURL="../../images/butt_add.gif" OnClick="ibAdd_OnClick" Runat="Server" />
                                            </td>
							</tr>
						</table>
					</td>
				</tr>
                </table>

                <table style="width: 100%" class="font9Tahoma">
				<tr>
					<td ColSpan=3>&nbsp;</td>
				</tr>
				<tr ID="trDataGrid1">
					<td ColSpan=3> 
						<asp:DataGrid ID="dgLines"
							OnItemDataBound="dgLines_OnItemDataBound"
							AutoGenerateColumns="False"
							Width="100%"
							GridLines="None"
							Cellpadding="2"
							Pagerstyle-Visible="False"
							OnDeleteCommand="dgLines_OnDeleteCommand"
							AllowSorting="True"
							Runat="Server"
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
								<asp:TemplateColumn HeaderText="Nursery Item">
									<ItemStyle Width="20%"/>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ItemCode") %> ID="lblDGItemCode" Runat="Server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="8%"/>
									<ItemTemplate>
										<%# Container.DataItem("BatchNo") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Charge To">
						            <ItemStyle Width="8%"/> 																								
						            <ItemTemplate>
							            <%# Container.DataItem("ChargeLocCode") %>
						            </ItemTemplate>
					            </asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="8%"/>
									<ItemTemplate>
										<%# Container.DataItem("AccCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="8%"/>
									<ItemTemplate>
										<%# Container.DataItem("BlkCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="8%"/>
									<ItemTemplate>
										<%# Container.DataItem("VehCode") %> 
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle Width="8%"/>
									<ItemTemplate>
										<%# Container.DataItem("VehExpCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Quantity">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="8%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Qty"),5) %> id="lblIDQty" runat="server" />							
										<asp:label text=<%# Container.DataItem("Qty") %> id="lblQty" visible = "false" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>										
								<asp:TemplateColumn HeaderText="Cost">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="8%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Cost"), 0)) %> id="lblIDCost" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("Cost"), 2) %> id="lblCost" visible = False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Amount">
									<HeaderStyle HorizontalAlign="Right" />
									<ItemStyle Width="8%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Amount"), 0)) %> id="lblIDAmount" runat="server" />
										<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2) %> id="lblAmount" visible = False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemStyle HorizontalAlign="Right" Width="5%"/>
									<ItemTemplate>
										<asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" CausesValidation="False" Runat="Server" />
										<asp:Label Text=<%# Trim(Container.DataItem("IssueLnID")) %> ID="lblIssueLNID" Visible="False" Runat="Server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr ID="trDataGrid2">
					<td>&nbsp;</td>
					<td ColSpan=2 Height=25><hr Size="1" NoShade></td>
				</tr>
				<tr ID="trDataGrid3">
					<td>&nbsp;</td>
					<td Align="Right">Total :</td>
					<td Align="Right"><asp:Label ID="lblTotalAmount" Runat="Server" /><asp:Label ID="lblDummySpace" Text=" " Style="width:18%;" Runat="Server" /></td>
				</tr>
				<tr>
					<td ColSpan=3><asp:Label ID="lblActionResult" Visible="False" ForeColor="Red" Runat="Server" />&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan=3><comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
								<asp:label id=lblBPErr text="Unable to generate Debit Note, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								<comment>Minamas FS 2.2 - Loo 28/09/2005 - START</comment>
					</td>
				</tr>
				<tr>
					<td Align="Left" ColSpan="3">
						<asp:ImageButton ID="ibSave"     AlternateText="Save"    OnClick="ibSave_OnClick"    ImageURL="../../images/butt_save.gif"    CausesValidation="False" Runat="Server" />
						<asp:ImageButton ID="ibConfirm"  AlternateText="Confirm" OnClick="ibConfirm_OnClick" ImageURL="../../images/butt_confirm.gif" CausesValidation="False" Runat="Server" />
						<asp:ImageButton ID="ibDelete"   AlternateText="Delete"  OnClick="ibDelete_OnClick"  ImageURL="../../images/butt_delete.gif"  CausesValidation="False" Runat="Server" />
						<asp:ImageButton ID="ibNew"      AlternateText="New"     OnClick="ibNew_OnClick"     ImageURL="../../images/butt_new.gif"     CausesValidation="False" Runat="Server" />
						<asp:ImageButton ID="ibBack"     AlternateText="Back"    OnClick="ibBack_OnClick"    ImageURL="../../images/butt_back.gif"    CausesValidation="False" Runat="Server" />
					</td>
				</tr>
				<tr>
					<td Align="Left" ColSpan="3">
                                            &nbsp;</td>
				</tr>
			</table>
			<Input type=hidden ID="hidBlockCharge" value="" Runat="Server" />
			<Input type=hidden ID="hidChargeLocCode" value="" Runat="Server" />


        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
