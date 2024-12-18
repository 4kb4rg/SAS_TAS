<%@ Page Language="vb" Src="../../../include/WS_WorkshopItem_Details.aspx.vb" Inherits="WS_WorkshopItemDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
    <title>Workshop ITEM DETAILS</title>
     <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl id=PrefHdl runat="server" />
</head>
<body>
    <form id="frmSysSetting" runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


  		<asp:Label id=lblCode visible=false text=" Code" runat=server />
  		<asp:Label id=lblChargeTo visible=false text="Charge to " runat=server />
		<asp:Label id="DiffAverageCost" runat="server" Visible="False"/>
        <asp:Label id="blnUpdate" runat="server" Visible="False"/>
        <table cellpadding="2" width="100%" border="0" class="font9Tahoma">
 			<tr>
				<td colspan="6">
					<UserControl:MenuWS id=menuWS runat="server" />
				</td>
			</tr>
            <tr>
                <td class="mt-h" colspan="6"><asp:label id="lblHead" Runat="server" text="WORKSHOP ITEM DETAILS"/></td>
            </tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
            <tr>
                <td><asp:label id="Label1" Runat="server" text="Workshop Item Code "/> :*</td>
                <td><asp:DropDownList id="lstItemCode" width=100% runat="server" AutoPostback=True OnSelectedIndexChanged=LoadItemMaster />
                    <asp:TextBox id="StckItemCode" runat="server" Visible=False maxlength="20" width=100% />
                    <asp:RequiredFieldValidator 
						id="validateCode" 
						runat="server" 
						ErrorMessage="Please Enter Workshop Item Code" 
						ControlToValidate="lstItemCode" 
						display="dynamic"/>
					<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/>
                </td>
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
                <td><asp:DropDownList id="lstProdType" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Last Update :</td>
                <td><asp:Label id="UpdateDate" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label3" Runat="server" /> *:</td>
                <td><asp:DropDownList id="lstProdCat" runat="server" width=100% /></td>
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
                <td><asp:DropDownList id="lstProdBrand" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Initial Cost :</td>
                <td><asp:Label id="InitialCost" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label6" Runat="server" /> *:</td>
                <td><asp:DropDownList id="lstProdModel" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Highest Cost :</td>
                <td><asp:Label id="HighestCost" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label7" Runat="server" /> :</td>
                <td><asp:DropDownList id="lstProdMat" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Lowest Cost :</td>
                <td><asp:Label id="LowestCost" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label8" Runat="server"/> :</td>
                <td><asp:DropDownList id="lstStockAnalysis" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Average Cost :</td>
                <td><asp:Label id="AvrgCost" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:label id="Label9" Runat="server" /> :</td>
                <td><asp:DropDownList id="lstVehExpense" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Latest Cost :</td>
                <!--td><asp:Label id="LatestCost" runat="server" /></td-->
            </tr>
            <tr>
                <td><asp:label id="Label10" Text ="Inventory Account" Runat="server" /> :*</td>
                <td>
					<asp:DropDownList id="lstAccCode" runat="server" width=100% />
					<asp:Label id=lblErrAccCode Visible=False Forecolor=Red Text="<BR>Please select one code" Runat=Server/>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:label id="Label21" Runat="server" text="Final"/> :</td>
				<td><asp:Label id="LatestCostFinal" runat="server"/></td>                
            </tr>
            <tr>
                <td><u><b>Unit of Measurement</b></u></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
				<td><asp:label id="Label22" Runat="server" text="Second"/> :</td>
				<td><asp:Label id="LatestCostSecond" runat="server"/></td>                
            </tr>
            <tr>
                <td width=15%><asp:label id="Label12" Runat="server" text="Purchasing "/> :*</td>
                <td width=30%><asp:DropDownList id="PurchaseUOM" runat="server" width=100% /></td>
                <td width=5%>&nbsp;</td>
                <td width=5%>&nbsp;</td>
                <td><asp:label id="Label23" Runat="server" text="Initial"/> :</td>
				<td><asp:Label id="LatestCostInitial" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label13" Runat="server" text="Inventory "/> :*</td>
                <td><asp:DropDownList id="InventoryUOM" runat="server" width=100% /></td>
                <td>&nbsp;</td>
                <td colspan="2">Finance Account number :</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><u><b>Inventory Level</b></u></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>Purchase A/C No. :</td>
                <td><asp:TextBox id="purchaseACNo" runat="server" maxlength="32" width=100% /></td>
            </tr>
            <tr>
                <td><asp:label id="Label15" Runat="server" text="Quantity on Hand "/> :</td>
                <td><asp:Label id="HandQty" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td width=15%>Issue A/C No :</td>
                <td width=25%><asp:TextBox id="IssueACNo" runat="server" maxlength="32" width=100% /></td>
            </tr>	 
            </tr>
            <tr>
                <td><asp:label id="Label16" Runat="server" text="Quantity on Hold "/> :</td>
                <td><asp:Label id="HoldQty" runat="server"/></td>
                <td>&nbsp;</td>
                <td colspan="2">Selling Price :*</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:label id="Label17" Runat="server" text="Quantity on Order"/> :</td>
                <td><asp:Label id="OrderQty" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:RadioButton id="Fixed_Price" runat="server" TextAlign="Right" Text="Fixed :" GroupName="SellPrice" Checked="True"/></td>
                <td><asp:TextBox id="FixedPrice" runat="server" maxlength="19" width=100% />
						
						<asp:RegularExpressionValidator id="RegularExpressionValidator2" 
							ControlToValidate="FixedPrice"
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 0 decimal points"
							runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label18" Runat="server" text="Reorder Level "/> :</td>
                <td><asp:TextBox id="ReorderLvl" runat="server" maxlength="15" width=50% />
                    <asp:RegularExpressionValidator id="RegularExpressionReorderLvl" 
							ControlToValidate="ReorderLvl"
							ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
							Display="Dynamic"
							text = "Maximum length 9 digits and 5 decimal points"
							runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:RadioButton id="Latest_Price" runat="server" TextAlign="Right" Text="% of Latest Cost :" GroupName="SellPrice"/></td>
                <td><asp:TextBox id="LatestPrice" runat="server" maxlength="3" width=50% />
					
					<asp:CompareValidator Display="Dynamic" id="ChckLatestPrice" runat="server" 
						ControlToValidate="LatestPrice" Text="Maximum length 3 digits and 0 decimal points" 
						Type="integer" Operator="DataTypeCheck"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label19" Runat="server" text="Reorder Quantity "/> :</td>
                <td><asp:TextBox id="ReorderQty" runat="server" maxlength="15" width=50% />
                    <asp:RegularExpressionValidator id="RegularExpressionReorderQty" 
							ControlToValidate="ReorderQty"
							ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
							Display="Dynamic"
							text = "Maximum length 9 digits and 5 decimal points"
							runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:RadioButton id="Avrg_Price" runat="server" TextAlign="Right" Text="% of Average Cost :" GroupName="SellPrice"/></td>
                <td><asp:TextBox id="AvrgPrice" runat="server" maxlength="3" width=50% ></asp:TextBox>
				
					<asp:CompareValidator Display="Dynamic" id="ChckAvrgPrice" runat="server" ControlToValidate="AvrgPrice" 
						Text="Maximum length 3 digits and 0 decimal points" Type="integer" Operator="DataTypeCheck"/></td>	
            </tr>
            <tr>
                <td><asp:label id="Label20" Runat="server" text="Remark"/> :</td>
                <td colspan="5"><asp:TextBox id="Remark" runat="server" maxlength="64" width=100% /></td>
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
                <td><asp:label id="LblFuelMeter" Runat="server" text="Fuel Meter Reading "/> :</td>
                <td><asp:TextBox id="txtFuelMeter" runat="server" maxlength="21" width=50%/> 
					<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
						ControlToValidate="txtFuelMeter"
						ValidationExpression="^[-]?\d{1,15}\.\d{1,5}|^[-]?\d{1,15}"
						Display="Dynamic"
						text = "Maximum length 15 digits and 5 decimal points"
						runat="server"/>
					<asp:RangeValidator id="Range1"
						ControlToValidate="txtFuelMeter"
						MinimumValue="0"
						MaximumValue="999999999999999"
						Type="double"
						EnableClientScript="True"
						Text="The value must be from 0 !"
						runat="server" display="dynamic"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
				<td colspan=6>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
                    <asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" causesvalidation=false onclick="btnDelete_Click" runat="server" AlternateText="Delete"/>
                    <asp:ImageButton id="SynchronizeData" imageurl="../../images/butt_synchronize_data.gif" onclick="btnSynchronizeData_Click" runat="server" AlternateText="Synchronize Data"/>
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
