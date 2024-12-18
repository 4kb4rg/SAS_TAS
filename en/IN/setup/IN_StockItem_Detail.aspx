<%@ Page Language="vb" Src="../../../include/IN_Setup_StockItem_Details.aspx.vb" Inherits="IN_StockDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINSetup" src="../../menu/menu_INsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
    <title>STOCK ITEM DETAILS</title>
    <Preference:PrefHdl id=PrefHdl runat="server" />
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        </style>
</head>
<body>
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist"> 
        <asp:Label id="ErrorMessage" runat="server"/>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
        <asp:Label id="blnUpdate" runat="server" Visible="False"/>
        <table cellpadding="2" width="100%" border="0" class="font9Tahoma">
 			<tr>
				<td colspan="6"><UserControl:MenuINSetup id=menuIN runat="server" /></td>
			</tr>
            <tr>
                <td class="mt-h" colspan="6">
                    <table cellspacing="1" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                               <strong> <asp:label id="lblTitle" Runat="server" />  DETAILS </strong></td>
                            <td  class="font9Header"  style="text-align: right">
                                Status : <asp:Label id="Status" runat="server"/>&nbsp;| Date Created : <asp:Label id="CreateDate" runat="server"/>&nbsp;| 
                                Last Update : <asp:Label id="UpdateDate" runat="server"/>&nbsp;| Updated By : <asp:Label id="UpdateBy" runat="server"/>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
			<tr>
				<td colspan=6>
                    <hr style="width :100%" />
                </td>
			</tr>
            <tr>
                <td><asp:label id="lblStockItemCode" Runat="server" /> :*</td>
                <td colspan=4><GG:AutoCompleteDropDownList id="lstItemCode" width=85% runat="server"  CssClass="fontObject"  AutoPostback=True OnSelectedIndexChanged=LoadItemMaster />
                <input type=button value=" ... " id="Find" onclick="javascript:PopStockItem('frmMain', '', 'lstItemCode', 'True');" CausesValidation=False runat=server />

					<asp:RequiredFieldValidator id="validatelstItemCode" runat="server" 
						display="dynamic" 
						ControlToValidate="lstItemCode"
						Text="<br>Please select stock item code." />&nbsp;

                    <asp:TextBox id="StckItemCode" runat="server" Visible=False width=100% maxlength="20"  CssClass="fontObject" />
                    <asp:RequiredFieldValidator 
						id="validateCode" 
						runat="server"  
						ControlToValidate="StckItemCode" 
						display="dynamic"/>
					<asp:label id="lblDupMsg" Text="Code already exist" Visible=false forecolor=red Runat="server"/></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:label id="lblDescription" runat="server" /> :*</td>
                <td colspan=4><asp:TextBox id="Desc" runat="server" maxlength="256" width=95%  CssClass="fontObject" />
                    <asp:RequiredFieldValidator id="validateDesc" runat="server" 
						display="dynamic" 
						ControlToValidate="Desc"/></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:label id="lblProdType" Runat="server" /> *:</td>
                <td colspan=4><asp:DropDownList id="lstProdType" width=95% runat="server"  CssClass="fontObject"  /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:label id="lblProdCat" Runat="server" /> *:</td>
                <td colspan=4><asp:DropDownList id="lstProdCat" width=95% runat="server"  CssClass="fontObject" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Fertilizer :</td>
                <td><asp:CheckBox id="chkFertInd" width=100% CssClass="fontObject" runat="server" /></td>
                <td>&nbsp;</td>
                <td colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign=top><asp:label id="Label4" Runat="server" text="Fuel Item Type "/> :</td>
                <td><asp:RadioButton id="Fuel_No" runat="server" TextAlign="Right" Text="Not Applicable" GroupName="FuelItm" Checked="True"/><br>
                    <asp:RadioButton id="Fuel_Yes" runat="server" TextAlign="Right" Text="Fuel" GroupName="FuelItm"/><br>
                    <asp:RadioButton id="Fuel_Lub" runat="server" TextAlign="Right" Text="Lubricant" GroupName="FuelItm"/></td>
            </tr>
            <!-- Millware 2.9 PRM 19 Jul 2006 -->
            <tr>
                <td><asp:label id="Label5" Runat="server" text="Item Type "/> :</td>
                <td><asp:RadioButton id="Itm_Stock" runat="server" TextAlign="Right" Text="Stock" OnCheckedChanged="ItmType_CheckedChanged" CssClass="fontObject"  AutoPostBack="True" GroupName="ItmType" Checked="True"/><br>
                    <asp:RadioButton id="Itm_Workshop" runat="server" TextAlign="Right" Text="Workshop" OnCheckedChanged="ItmType_CheckedChanged" AutoPostBack="True" CssClass="fontObject" GroupName="ItmType"/><br>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="1"><u><b>Financial Options</b></u></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:label id="lblProdBrand" Runat="server" /> :</td>
                <td colspan=4><asp:DropDownList id="lstProdBrand" width=95% runat="server"  CssClass="fontObject" /></td>
                <td>&nbsp;Initial Cost : <asp:Label id="InitialCost" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="lblProdModel" Runat="server" /> *:</td>
                <td colspan=4><asp:DropDownList id="lstProdModel" width=95% runat="server" CssClass="fontObject" /></td>
                <td>&nbsp;Highest Cost : <asp:Label id="HighestCost" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="lblProdMat" Runat="server" /> :</td>
                <td colspan=4><asp:DropDownList id="lstProdMat" width=95% runat="server"  CssClass="fontObject" /></td>
                <td>&nbsp;Lowest Cost : <asp:Label id="LowestCost" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="lblStockAna" Runat="server" /> :</td>
                <td colspan=4><asp:DropDownList id="lstStockAnalysis" width=95% runat="server"  CssClass="fontObject" /></td>
                <td>&nbsp;Average Cost : <asp:Label id="AvrgCost" runat="server"/></td>
            </tr>
            <tr>
                <td width=16%><asp:label id="lblVehExpCode" Runat="server" /> :</td>
                <td colspan=4><asp:DropDownList id="lstVehExpense" width=95% runat="server"  CssClass="fontObject" /></td>
                <td>&nbsp;Difference Average Cost : <asp:Label id="DiffAverageCost" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="lblAccount" Runat="server" text="Inventory Account" />:*</td>
                <td colspan=4><asp:DropDownList id="lstAccCode" width=95% runat="server"  CssClass="fontObject"  />
                    <asp:RequiredFieldValidator id="validateAccCode" runat="server" 
						display="dynamic" 
						ControlToValidate="lstAccCode"
						Text="<br>Please select account code." /></td>
                <td>&nbsp;<u><b>Latest Cost :</b></u></td>
                <!-- td><asp:Label id="LatestCost" runat="server"/></td -->
               
            </tr>
            <tr>
                <td><u><b>Unit of Measurement</b></u></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;<asp:label id="Label21" Runat="server" text="Final"/> : <asp:Label id="LatestCostFinal" runat="server"/></td>
            </tr>
            <tr>
                <td width=15%><asp:label id="Label12" Runat="server" text="Purchasing "/> :*</td>
                <td colspan=4><asp:DropDownList id="PurchaseUOM" width=95% runat="server"  CssClass="fontObject"  /></td>
                <td>&nbsp;<asp:label id="Label22" Runat="server" text="Second"/> : <asp:Label id="LatestCostSecond" runat="server"/></td>
            </tr>
            <tr>
                <td><asp:label id="Label13" Runat="server" text="Inventory "/> :*</td>
                <td colspan=4><asp:DropDownList id="InventoryUOM" width=95% runat="server"  CssClass="fontObject"  /></td>
                <td>&nbsp;<asp:label id="Label23" Runat="server" text="Initial"/> : <asp:Label id="LatestCostInitial" runat="server"/></td>
            </tr>
            <tr>
                <td><u><b>Inventory Level</b></u></td>
				<td>&nbsp;</td>
                <td>&nbsp;</td>
				<td>&nbsp;</td>
				<td colspan="2" height=25><u><b>Finance Account number :</b></u></td>
                <td>&nbsp;</td>
				
            </tr>
            <tr>
				<td><asp:label id="Label15" Runat="server" text="Quantity on Hand "/> :</td>
				<td><asp:Label id="HandQty" runat="server"/></td>
				<td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>Purchase A/C No. :</td>
                <td><asp:TextBox id="purchaseACNo" runat="server" maxlength="32" width=100%  CssClass="fontObject" /></td>
            </tr>
            <tr>
                <td><asp:label id="Label16" Runat="server" text="Quantity on Hold "/> :</td>
                <td><asp:Label id="HoldQty" runat="server"/></td>
				<td>&nbsp;</td>
                <td>&nbsp;</td>
				<td width=15%>Issue A/C No :</td>
                <td width=25%><asp:TextBox id="IssueACNo" runat="server" maxlength="32" width=100%  CssClass="fontObject" /></td>
               
                
            </tr>
            <tr>
                <td><asp:label id="Label17" Runat="server" text="Quantity on Order"/> :</td>
                <td><asp:Label id="OrderQty" runat="server"/></td>
				<td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="2" height=25><u><b>Selling Price :*</b></u></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:label id="Label18" Runat="server" text="Reorder Level "/> :</td>
                <td><asp:TextBox id="ReorderLvl" runat="server" maxlength="15" width=50%  CssClass="fontObject" />
                    <asp:RegularExpressionValidator id="RegularExpressionReorderLvl" 
						ControlToValidate="ReorderLvl"
						ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
						Display="Dynamic"
						text = "Maximum length 9 digits and 5 decimal points"
						runat="server"/></td>
				<td>&nbsp;</td>
                <td>&nbsp;</td>
                
             	<td><asp:RadioButton id="Fixed_Price" runat="server" TextAlign="Right" Text="Fixed :" GroupName="SellPrice" CssClass="fontObject" Checked="True"/></td>
                <td><asp:TextBox id="FixedPrice" runat="server" text=0 maxlength="19" width=100%  CssClass="font9Tahoma" />
                    <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
						ControlToValidate="FixedPrice"
						ValidationExpression="\d{1,19}"
						Display="Dynamic"
						text = "Maximum length 19 digits and 0 decimal points"
						runat="server"/></td>						
                
            </tr>
            <tr>
                <td><asp:label id="Label19" Runat="server" text="Reorder Quantity "/> :</td>
                <td><asp:TextBox id="ReorderQty" runat="server" maxlength="15" width=50%  CssClass="fontObject" />
                    <asp:RegularExpressionValidator id="RegularExpressionReorderQty" 
						ControlToValidate="ReorderQty"
						ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
						Display="Dynamic"
						text = "Maximum length 9 digits and 5 decimal points"
						runat="server"/></td>
				<td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:RadioButton id="Latest_Price" runat="server" TextAlign="Right"  CssClass="fontObject" Text="% of Latest Cost :" GroupName="SellPrice"/></td>
				<td><asp:TextBox id="LatestPrice" runat="server" text=0 maxlength="9" width=100%  CssClass="fontObject" />
					<asp:RegularExpressionValidator id="RegularExpressionLatestPrice" 
						ControlToValidate="LatestPrice"
						ValidationExpression="\d{1,3}\.\d{1,5}|\d{1,3}"
						Display="Dynamic"
						text = "Maximum length 3 digits and 5 decimal points"
						runat="server"/></td>						
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan=2><asp:RadioButton id="Avrg_Price" runat="server" CssClass="fontObject" TextAlign="Right" Text="% of Average Cost :" GroupName="SellPrice"/> <asp:TextBox id="AvrgPrice" runat="server" maxlength="9" text=0 width=62%  CssClass="font9Tahoma" ></asp:TextBox>
					<asp:RegularExpressionValidator id="RegularExpressionAvrgPrice" 
						ControlToValidate="AvrgPrice"
						ValidationExpression="\d{1,3}\.\d{1,5}|\d{1,3}"
						Display="Dynamic"
						text = "Maximum length 3 digits and 5 decimal points"
						runat="server"/></td>
            </tr>

            <!-- Millware 2.9 PRM 19 Jul 2006 -->
            <tr>
				<td><asp:label id="LabelLifespan" visible=false Runat="server" text="Lifespan :*"/></td>
                <td><asp:TextBox id="txtLifespan" enabled=true visible=false runat="server" maxlength="6" text=0 width=50%  CssClass="font9Tahoma" /> 
					<asp:label id="LabelHour" visible=false Runat="server" text="  Hours"/></td>

            </tr>
            <tr>
                <td><asp:label id="Label20" Runat="server" text="Remark"/> :</td>
                <td colspan="5"><asp:TextBox id="Remark" runat="server" maxlength="64" width=100%  CssClass="fontObject" /></td>
            </tr>
            <tr>
                <td><asp:label id="LabelBin" Runat="server" text="Item Bin "/> :</td>
                <td><asp:TextBox id="txtBin" runat="server" maxlength="20" width=50%  CssClass="fontObject"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:label id="LblFuelMeter" Runat="server" text="Fuel Meter Reading "/> :</td>
                <td><asp:TextBox id="txtFuelMeter" runat="server" maxlength="15" width=50%  CssClass="fontObject" /> 
					<asp:RegularExpressionValidator id="RegularExpressionValidatorQtyReq" 
						ControlToValidate="txtFuelMeter"
						ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
						Display="Dynamic"
						text = "Maximum length 9 digits and 5 decimal points"
						runat="server"/>
					<asp:RangeValidator id="Range1"
						ControlToValidate="txtFuelMeter"
						MinimumValue="0"
						MaximumValue="999999999999999"
						Type="double"
						EnableClientScript="True"
						Text="The value must be from 1 !"
						runat="server" display="dynamic"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td><u><b>Inventory Bin Level</b></u></td>
				<td>&nbsp;</td>
                <td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;
                <td>&nbsp;</td>
				
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin1" Runat="server" text="HO/PWK "/> :</td>
                <td><asp:Label id="QtyBin1" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin2" Runat="server" text="Central "/> :</td>
                <td><asp:Label id="QtyBin2" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin3" Runat="server" text="Other "/> :</td>
                 <td><asp:Label id="QtyBin3" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin4" Runat="server" text="Bin I "/> :</td>
                 <td><asp:Label id="QtyBin4" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin5" Runat="server" text="Bin II "/> :</td>
                 <td><asp:Label id="QtyBin5" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin6" Runat="server" text="Bin III"/> :</td>
                 <td><asp:Label id="QtyBin6" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin7" Runat="server" text="Bin IV "/> :</td>
                <td><asp:Label id="QtyBin7" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin8" Runat="server" text="Bin V "/> :</td>
                <td><asp:Label id="QtyBin8" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin9" Runat="server" text="Bin VI "/> :</td>
                <td><asp:Label id="QtyBin9" runat="server"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td height=25><asp:label id="lblBin10" Runat="server" text="Bin VII "/> :</td>
                <td><asp:Label id="QtyBin10" runat="server"/></td>
                <td>&nbsp;</td>
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
                    <asp:ImageButton id="Delete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete"/>
                    <asp:ImageButton id="SynchronizeData" imageurl="../../images/butt_synchronize_data.gif" onclick="btnSynchronizeData_Click" Visible=false runat="server" AlternateText="Synchronize Data"/>
                    <asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
                    <br />
                </td>
            </tr>
        </table>
                 </div>
            </td>
        </tr>
        </table>
    </form>
 
</body>
</html>
