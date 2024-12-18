<%@ Page Language="vb" Src="../../../include/CT_CanteenItemMaster_Details.aspx.vb" Inherits="CT_MasterDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuCTSetup" src="../../menu/menu_CTsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
    <title>CANTEEN MASTER DETAILS</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl id=PrefHdl runat="server" />
</head>
<body>
    <form id="frmSysSetting" runat="server" class="main-modul-bg-app-list-pu">
        <asp:Label id="ErrorMessage" runat="server"/>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblChargeTo visible=false text="Charge to " runat=server />
        <asp:Label id="DiffAverageCost" runat="server" Visible="False"/>
        <asp:Label id="blnUpdate" runat="server" Visible="False"/>

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



        <table cellpadding="2" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuCTSetup id=menuIN runat="server" /></td>
				</tr>
               <tr>
                    <td class="mt-h" colspan="6"><asp:label id="lblHead" Runat="server" text="CANTEEN MASTER DETAILS"/></td>
                </tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
                <tr>
                    <td><asp:label id="Label1" Runat="server" text="Canteen Master Code"/> :*</td>
                    <td><asp:TextBox id="StckItemCode" runat="server" width=100% maxlength="20"/>
                        <asp:RequiredFieldValidator 
							id="validateCode" 
							runat="server" 
							ErrorMessage="Please Enter Canteen Master Code" 
							ControlToValidate="StckItemCode" 
							display="dynamic"/>
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="StckItemCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,20}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>								
						<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/></td>
                    <td>&nbsp;</td>
                    <td colspan="2">Status :</td>
                    <td><asp:Label id="Status" runat="server"/></td>
                </tr>
                <tr>
                    <td>Description :*</td>
                    <td><asp:TextBox id="Desc" runat="server" maxlength="64" width=100% />
                        <asp:RequiredFieldValidator id="validateDesc" runat="server" 
							ErrorMessage="Please Enter Description" 
							display="dynamic" 
							ControlToValidate="Desc"/></td>
                    <td>&nbsp;</td>
                    <td colspan="2">Date Created :</td>
                    <td><asp:Label id="CreateDate" runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label2" Runat="server" /> *:</td>
                    <td><asp:DropDownList id="lstProdType" width=100% runat="server" />
                    <asp:Label id=lblErrProdType visible=false Text="Please select Product Type." forecolor=red runat=server />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2">Last Update :</td>
                    <td><asp:Label id="UpdateDate" runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label3" Runat="server" /> *:</td>
                    <td><asp:DropDownList id="lstProdCat" width=100% runat="server" />
                    <asp:Label id=lblErrProdCat visible=false Text="Please select Product Category." forecolor=red runat=server />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2">Updated By :</td>
                    <td><asp:Label id="UpdateBy" runat="server"/></td>
                </tr>
                <tr>
                    <td valign=top><asp:label id="Label4" Runat="server" text="Fuel Item Type "/> :</td>
                    <td><asp:RadioButton id="Fuel_No" runat="server" TextAlign="Right" Text="Not Applicable" GroupName="FuelItm" Checked="True"/><br>
                        <asp:RadioButton id="Fuel_Yes" runat="server" TextAlign="Right" Text="Fuel" GroupName="FuelItm"/><br>
                        <asp:RadioButton id="Fuel_Lub" runat="server" TextAlign="Right" Text="Lubricant" GroupName="FuelItm"/></td>
                    <td>&nbsp;</td>
                    <td colspan="2"><u><b>Financial Options</b></u></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:label id="Label5" Runat="server" /> :</td>
                    <td><asp:DropDownList id="lstProdBrand" width=100% runat="server" /> </td>
                    <td>&nbsp;</td>
                    <td colspan="2">Initial Cost :</td>
                    <td><asp:Label id="InitialCost" runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label6" Runat="server" /> *:</td>
                    <td><asp:DropDownList id="lstProdModel" width=100% runat="server" />
                    <asp:Label id=lblErrProdModel visible=false Text="Please select Product Model." forecolor=red runat=server />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2">Highest Cost :</td>
                    <td><asp:Label id="HighestCost" runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label7" Runat="server" /> :</td>
                    <td><asp:DropDownList id="lstProdMat" width=100% runat="server" /></td>
                    <td>&nbsp;</td>
                    <td colspan="2">Lowest Cost :</td>
                    <td><asp:Label id="LowestCost" runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label8" Runat="server" /> :</td>
                    <td><asp:DropDownList id="lstStockAnalysis" width=100% runat="server" /></td>
                    <td>&nbsp;</td>
                    <td colspan="2">Average Cost :</td>
                    <td><asp:Label id="AvrgCost" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:label id="Label9" Runat="server" /> :</td>
                    <td><asp:DropDownList id="lstVehExpense" width=100% runat="server" /></td>
                    <td>&nbsp;</td>
                    <td colspan="2">Latest Cost :</td>
                    <td><asp:Label id="LatestCost" runat="server" /></td>
                </tr>
                <tr>
                    <td><asp:label id="Label10" Runat="server" /> :</td>
                    <td><asp:DropDownList id="lstAccCode" width=100% runat="server" />
                    <asp:RequiredFieldValidator id="validateAccCode" runat="server" 
						display="dynamic" 
						ControlToValidate="lstAccCode"
						Text="<br>Please select account code."/>  
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2">Finance Account number :</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><u><b>Unit of Measurement</b></u></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>Purchase A/C No. :</td>
                    <td><asp:TextBox id="purchaseACNo" runat="server" maxlength="32" width=100%/></td>
                </tr>
                <tr>
                    <td width=15%><asp:label id="Label12" Runat="server" text="Purchasing"/> :*</td>
                    <td width=30%><asp:DropDownList id="PurchaseUOM" width=100% runat="server" /></td>
                    <td width=5%>&nbsp;</td>
                    <td width=5%>&nbsp;</td>
                    <td width=15%>Issue A/C No :</td>
                    <td width=25%><asp:TextBox id="IssueACNo" runat="server" maxlength="32" width=100%/>
                    </td>
                </tr>
                <tr>
                    <td><asp:label id="Label13" Runat="server" text="Inventory"/> :*</td>
                    <td><asp:DropDownList id="InventoryUOM" width=100% runat="server" /></td>
                    <td>&nbsp;</td>
                    <td colspan="2">Selling Price :*</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><u><b>Inventory Level</b></u></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:RadioButton id="Fixed_Price" runat="server" TextAlign="Right" Text="Fixed :" GroupName="SellPrice" Checked="True"/></td>
                    <td><asp:TextBox id="FixedPrice" runat="server" maxlength="19" width=100%/>
                        <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
								ControlToValidate="FixedPrice"
								ValidationExpression="\d{1,19}"
								Display="Dynamic"
								text = "Maximum length 19 digits and 0 decimal points"
								runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label15" Runat="server" text="Quantity on Hand"/> :</td>
                    <td><asp:Label id="HandQty" runat="server"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:RadioButton id="Latest_Price" runat="server" TextAlign="Right" Text="% of Latest Cost :" GroupName="SellPrice"/></td>
                    <td><asp:TextBox id="LatestPrice" runat="server" maxlength="9" width=50%/>
						<asp:RegularExpressionValidator id="RegularExpressionValidator2" 
								ControlToValidate="LatestPrice"
								ValidationExpression="\d{1,3}\.\d{1,5}|\d{1,3}"
								Display="Dynamic"
								text = "Maximum length 3 digits and 5 decimal points"
								runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label16" Runat="server" text="Quantity on Hold"/> :</td>
                    <td><asp:Label id="HoldQty" runat="server"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:RadioButton id="Avrg_Price" runat="server" TextAlign="Right" Text="% of Average Cost :" GroupName="SellPrice"/></td>
                    <td><asp:TextBox id="AvrgPrice" runat="server" maxlength="9" width=50%></asp:TextBox>
                        <asp:RegularExpressionValidator id="RegularExpressionValidator3" 
								ControlToValidate="AvrgPrice"
								ValidationExpression="\d{1,3}\.\d{1,5}|\d{1,3}"
								Display="Dynamic"
								text = "Maximum length 3 digits and 5 decimal points"
								runat="server"/></td>
                </tr>
                <tr>
                    <td><asp:label id="Label17" Runat="server" text="Quantity on Order"/> :</td>
                    <td><asp:Label id="OrderQty" runat="server"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp; </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:label id="Label18" Runat="server" text="Reorder Level"/> :</td>
                    <td><asp:TextBox id="ReorderLvl" runat="server" maxlength="15" width=50%/>
                        <asp:RegularExpressionValidator id="RegularExpressionReorderLvl" 
								ControlToValidate="ReorderLvl"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "Maximum length 9 digits and 5 decimal points"
								runat="server"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp; </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:label id="Label19" Runat="server" text="Reorder Quantity "/> :</td>
                    <td><asp:TextBox id="ReorderQty" runat="server" maxlength="15" width=50%/>
                        <asp:RegularExpressionValidator id="RegularExpressionReorderQty" 
								ControlToValidate="ReorderQty"
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "Maximum length 9 digits and 5 decimal points"
								runat="server"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp; </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:label id="Label20" Runat="server" text="Remark"/> :</td>
                    <td colspan="5"><asp:TextBox id="Remark" runat="server" maxlength="64" width=100%/> </td>
                </tr>
                <tr>
                    <td><asp:label id="LabelBin" Runat="server" text="Item Bin "/> :</td>
                    <td><asp:TextBox id="txtBin" runat="server" maxlength="20" width=50%/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
                        <asp:ImageButton id="Delete" CausesValidation=False imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete"/>
                        <asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                                            &nbsp;</td>
                </tr>
        </table>

        <br />
        </div>
        </td>
        </tr>
        </table>


    </form>
</body>
</html>
