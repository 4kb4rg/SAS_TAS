<%@ Page Language="vb" Trace="False" Src="../../../include/WS_Trx_Job_PartsIssueReturn.aspx.vb" Inherits="WS_TRX_JOB" %>
<%@ Register TagPrefix="UserControl" TagName="MenuWSTrx" Src="../../menu/menu_WSTrx.ascx" %>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" Src="../../include/preference/preference_handler.ascx" %>
<html>
	<head>
		<title>Parts Issue/Return Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" RunAt="Server" />
	</head>
	<body>
		<form ID="frmMain" RunAt="Server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:label ID="lblCode" Visible="False" Text=" Code" RunAt="Server" />
			<asp:label ID="lblSelect" Visible="False" Text="Select " RunAt="Server" />
			<asp:label ID="lblPleaseSelect" Visible="False" Text="Please select " RunAt="Server" />
			<asp:label ID="lblJobType" Visible="False" RunAt="Server" />
			<asp:Label ID="lblJobStatus" Visible="False" RunAt="Server" />
			<asp:Label ID="lblJobVehCode" Visible="False" RunAt="Server" />
			<table ID="tblMain" Border="0" Width="100%" CellSpacing="0" CellPadding="1" RunAt="Server" class="font9Tahoma">
				<tr>
					<td ColSpan="6"><UserControl:MenuWSTrx ID="menuWSTrx" RunAt="Server" /></td>
				</tr>
				<tr>
					<td Class="mt-h" ColSpan="6">PARTS ISSUE/RETURN DETAILS</td>
				</tr>
				<tr>
					<td ColSpan="6"><hr Size="1" NoShade></td>
				</tr>
				<tr>
					<td Width="20%" Height="25">Job ID :</td>
					<td Width="30%"><asp:Label ID="lblJobID" RunAt="Server"/>&nbsp;</td>
					<td Width="5%">&nbsp;</td>
					<td Width="15%">&nbsp;</td>
					<td Width="25%">&nbsp;</td>
					<td Width="5%">&nbsp;</td>
				</tr>
                </table>

                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
				<tr>
				    <td ColSpan="6">
				        <table ID="tblAdd" Border="0" Width="100%" CellSpacing="0" CellPadding="4" RunAt="Server" class="font9Tahoma">
				            <tr Class="mb-c">
					            <td Width="20%" Height="25">Transaction Type :*</td>
					            <td Width="80%">
					                <table Width="100%" CellSpacing="0" CellPadding="0" Border="0" class="font9Tahoma">
					                    <tr>
					                        <td Width="20%"> <asp:RadioButton ID="rbTransTypeIssue" GroupName="rbTransType" TextAlign="Right" Checked="True" AutoPostBack="True" OnCheckedChanged="rbTransType_OnCheckedChanged" RunAt="Server" /> </td>
					                        <td Width="20%"> <asp:RadioButton ID="rbTransTypeReturn" GroupName="rbTransType" TextAlign="Right" AutoPostBack="True" OnCheckedChanged="rbTransType_OnCheckedChanged" RunAt="Server" /> </td>
					                        <td Width="60%"></td>
					                    </tr>
					                </table>
					            </td>
				            </tr>
				            <tr Class="mb-c">
					            <td Height="25">Transaction Date :</td>
					            <td>
					                <table Width="100%" CellSpacing="0" CellPadding="0" Border="0" class="font9Tahoma">
							            <tr>
							                <td Width="20%"><asp:TextBox ID="txtTransDate" Width="100%" MaxLength="10" RunAt="Server" /></td>
						                    <td Width="80%">&nbsp;<a HRef="javascript:PopCal('txtTransDate');"><asp:Image ID="imgTransDate" ImageUrl="../../Images/calendar.gif" RunAt="Server" /></a></td>
						                </tr>
						            </table>
					                <asp:Label ID="lblTransDateErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
					            <td Height="25">Employee Code (Mechanic) :*</td>
					            <td>
					                <asp:DropDownList ID="ddlEmpCode" Width="100%" RunAt="Server" />
					                <asp:Label ID="lblEmpCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
					            <td Height="25"><asp:Label ID="lblWorkCodeTag" RunAt="Server" /> :*</td>
					            <td>
					                <asp:DropDownList ID="ddlWorkCode" Width="100%" RunAt="Server" />
					                <asp:Label ID="lblWorkCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
					            <td Height="25">Item Code :*</td>
                                <td>
					                <asp:DropDownList ID="ddlItemCode" Width="96%" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCode_OnSelectedIndexChanged" RunAt="Server" />
					                <input type=button value=" ... " id="FindWS" Visible=True onclick="javascript:findcode('frmMain','','','','','','','','','','','','ddlItemCode','','','','','','');" CausesValidation=False runat=server />
					                <asp:Label ID="lblItemCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					                <asp:Label ID="lblItemCode" Visible="False" RunAt="Server" />
					                <asp:Label ID="lblJobStockIssueID" Visible="False" RunAt="Server" />
					                <asp:Label ID="lblItemType" Visible="False" RunAt="Server" />
					            </td>
				            </tr>
				            <!-- Minamas CR-MNS0701010011; Preventive Maintenance 21 Sept 2006 PRM-->
				            <!-- start -->
							<tr Class="mb-c">
					            <td Height="25"><asp:label id="lblLineNo" runat="server" /></td>
                                <td>
					                <asp:DropDownList ID="ddlLineNo" Width="96%" AutoPostBack="True" RunAt="Server" />
					                <asp:Label ID="lblLineNoErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>	
				            <!-- end -->
				            <tr Class="mb-c" ID="trAccCode">
					            <td Height=25><asp:label ID="lblAccCodeTag" RunAt="Server" /> :*</td>
					            <td>
					                <table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							            <tr>
								            <td Width="99%"><asp:DropDownList ID="ddlAccCode" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlAccCode_OnSelectedIndexChanged" RunAt="Server" /></td>
						                    <td>&nbsp;<Input Type="Button" Value=" ... " ID="btnFindAccCode" OnClick="javascript:findcode('frmMain','','ddlAccCode','9','ddlBlkCode','','ddlVehCode','ddlVehExpCode','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" CausesValidation="False" RunAt="Server" /></td>
						                </tr>
						            </table>
						            <asp:Label ID="lblAccCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            					
							<tr Class="mb-c" ID="trChargeLevel" >
								<td Height="25">Charge Level :*</td>
								<td>
									<asp:DropDownList ID="ddlChargeLevel" Width="100%" AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged Runat="Server" /> 
								</td>
							</tr>
							<tr Class="mb-c" ID="trPreBlkCode" >
								<td Height="25"><asp:Label ID="lblPreBlkCodeTag" Runat="Server" /> :</td>
								<td>
									<asp:DropDownList ID="ddlPreBlkCode" Width="100%" Runat="Server" />
									<asp:Label ID="lblPreBlkCodeErr" Visible="False" ForeColor="Red" Runat="Server" />
								</td>
							</tr>
				            <tr Class="mb-c" ID="trBlkCode">
					            <td Height=25><asp:label ID="lblBlkCodeTag" RunAt="Server" /> :</td>
					            <td>
					                <asp:DropDownList ID="ddlBlkCode" Width="100%" RunAt="Server" />
					                <asp:Label ID="lblBlkCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>										
				            <tr Class="mb-c" ID="trVehCode">
					            <td Height=25><asp:label ID="lblVehCodeTag" RunAt="Server" /> :</td>
					            <td>
					                <asp:DropDownList ID="ddlVehCode" Width="100%" RunAt="Server" />
						            <asp:label ID="lblVehCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c" ID="trVehExpCode">
					            <td Height=25><asp:label ID="lblVehExpCodeTag" RunAt="Server" /> :</td>
					            <td>
					                <asp:DropDownList ID="ddlVehExpCode" Width="100%" RunAt="Server" />
						            <asp:label ID="lblVehExpCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c" ID="trVehStkExpCode">
					            <td Height=25><asp:label ID="lblVehStkExpCodeTag" RunAt="Server" /> (Stock) :</td>
					            <td>
					                <asp:DropDownList ID="ddlVehStkExpCode" Width="100%" RunAt="Server" />
						            <asp:label ID="lblVehStkExpCodeErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
					            <td Height="25"><asp:label ID="lblQuantityTag" RunAt="Server" /> :*</td>
					            <td>
					                <asp:textbox ID="txtQty" MaxLength="15" Width="20%" RunAt="Server" />
                                    <asp:RegularExpressionValidator ID="revQty" 
													                ControlToValidate="txtQty"
													                ValidationExpression="^\d{1,9}\.\d{1,5}|^\d{1,9}"
													                Display="Dynamic"
													                Text = "<br>Maximum length 9 digits and 5 decimal points"
													                RunAt="Server"/>
                                    <asp:Label ID="lblQtyErr" Visible="False" ForeColor="Red" RunAt="Server" />
					            </td>
				            </tr>
				            <tr Class="mb-c">
				                <td ColSpan="2" Align="Left"><asp:ImageButton Text="Add" ID="ibAdd" ImageURL="../../images/butt_add.gif" OnClick="ibAdd_OnClick" RunAt="Server" />
                                            </td>
				            </tr>
				        </table>
				    </td>
				</tr>
                </table>

                <table style="width: 100%" class="font9Tahoma">
				<tr>
				    <td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td ColSpan="6"> 
						<asp:DataGrid ID="dgJobStock"
							AutoGenerateColumns="False" 
							Width="100%" 
							RunAt="Server"
							GridLines = "None"
							Cellpadding = "2"
							Pagerstyle-Visible="False"
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
								<asp:TemplateColumn HeaderText="Job Stock ID">
									<ItemStyle Width="8%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("JobStockID")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Type">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# objWSTrx.mtdGetJobStockType(IIf(IsNumeric(Container.DataItem("TransType")), Container.DataItem("TransType"), 0)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Transaction Date">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# objGlobal.GetShortDate(Session("SS_DATEFMT"), Container.DataItem("TransDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Employee Code (Mechanic)">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("EmpCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Work Code">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("WorkCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item Code">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("ItemCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Account Code">
									<ItemStyle Width="8%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("AccCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Block Code">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("BlkCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Vehicle Code">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("VehCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Vehicle Expense Code">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("VehExpenseCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Expense Code">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("VehStkExpCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Quantity">
									<ItemStyle HorizontalAlign="Right" Width="6%" />
									<ItemTemplate>
										
										<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Qty"), 5), 5)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost">
									<ItemStyle HorizontalAlign="Right" Width="6%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("Cost"), 0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cost Amount">
									<ItemStyle HorizontalAlign="Right" Width="6%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("Amount"), 0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Price">
									<ItemStyle HorizontalAlign="Right" Width="6%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("Price"), 0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Price Amount">
									<ItemStyle HorizontalAlign="Right" Width="6%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("PriceAmount"), 0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
				    <td ColSpan="6">
				        <table Width="100%" CellSpacing="0" CellPadding="2" Border="0">
							<tr>
							    <td Width="70%">&nbsp;</td>
							    <td Width="30%"><hr></td>
							</tr>
					    </table>
				    </td>
				</tr>
				<tr>
				    <td ColSpan="6">
				        <table Width="100%" CellSpacing="0" CellPadding="2" Border="0">
							<tr>
							    <td Width="76%">&nbsp;</td>
							    <td Width="6%">Total Cost : </td>
							    <td Width="6%" Align="Right"><asp:Label ID="lblTotalCost" RunAt="Server" /></td>
							    <td Width="6%">Total Price : </td>
							    <td Width="6%" Align="Right"><asp:Label ID="lblTotalPrice" RunAt="Server" /></td>
							</tr>
					    </table>
				    </td>
				</tr>
				<tr>
					<td ColSpan="6"><asp:Label ID="lblActionResult" Visible="False" ForeColor="Red" RunAt="Server" />&nbsp;</td>
				</tr>
				<tr>
					<td align="left" ColSpan="6">
						<asp:ImageButton ID="ibBack" AlternateText="Back" OnClick="ibBack_OnClick" ImageURL="../../images/butt_back.gif" CausesValidation="False" RunAt="Server" />
					</td>
				</tr>
			</table>
			<input Type=Hidden ID="hidBlockCharge" Value="" RunAt="Server" />
			<input Type=Hidden ID="hidChargeLocCode" Value="" RunAt="Server" />
			<asp:label id=lblSubBlkCode visible=false runat=server/>
            
          <br />
        </div>
        </td>
        </tr>
        </table>


	
		</form>
	</body>
</html>
