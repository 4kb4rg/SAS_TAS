<%@ Page Language="vb" Src="../../../include/IN_StockMaster_Request_Detail.aspx.vb" Inherits="IN_StockMaster_Request_Detail" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINSetup" src="../../menu/menu_INsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>STOCK MASTER REQUEST</title>
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
    <form id="frmSysSetting" class="main-modul-bg-app-list-pu" runat="server">
         <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
        <asp:Label id="ErrorMessage" runat="server"/>
		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
        <asp:Label id="blnUpdate" runat="server" Visible="False"/>
        <table cellpadding="2" width="100%" border="0" class="font9Tahoma">
 			<tr>
				<td colspan="6"><UserControl:MenuINSetup id=menuIN runat="server" /></td>
			</tr>
            <tr>
                <td colspan="6">
                    <table cellspacing="1" class="style1">
                        <tr>
                            <td class="font9Tahoma"><strong>
                    REQUEST STOCK MASTER &nbsp;DETAILS</strong></td>
                            <td  class="font9Header" style="text-align: right">
                                Status : <asp:Label id="Status" runat="server"/>&nbsp;| &nbsp;Date Created : <asp:Label id="CreateDate" runat="server"/>&nbsp;| 
                                Last Update : <asp:Label id="UpdateDate" runat="server"/>&nbsp;| Updated By : <asp:Label id="UpdateBy" runat="server"/></td>
                        </tr>
                    </table>
                     <hr style="width :100%" />
                </td>
            </tr>
			<tr>
				<td colspan=6 style="height: 38px"><asp:label id="lblTitle" runat="server" Visible="False" />&nbsp;</td>
			</tr>
            <tr>
                <td style="width: 258px">
                    <asp:label id="Label1" Runat="server" >No. Document</asp:label></td>
                <td colspan="4">
                    <asp:label id="LblNoDoc" Runat="server" >No.Document</asp:label></td>
                <td style="width: 418px">
                </td>
            </tr>
            <tr>
                <td style="width: 258px; height: 7px;"><asp:label id="lblStockItemCode" Runat="server" /> :*</td>
                <td colspan=4 style="height: 7px"><asp:TextBox id="StckItemCode" CssClass="font9Tahoma"  runat="server" width="36%" maxlength="20" Enabled="False" ReadOnly="True"/>&nbsp;
					<!--asp:RegularExpressionValidator id=revCode 
						ControlToValidate="StckItemCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/-->
					<asp:label id="lblDupMsg"  Text="Code already exist" Visible = false forecolor=red Runat="server"/></td>
				<td style="width: 418px; height: 7px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 258px">
                    Deskripsi/Stock Name :*</td>
                <td colspan=4><asp:TextBox id="Desc" CssClass="font9Tahoma"  runat="server" maxlength="128" width=95% Height="21px"  />
                    <asp:RequiredFieldValidator id="validateDesc" runat="server" 
						display="dynamic" 
						ControlToValidate="Desc"/></td>
                <td style="width: 418px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 258px"><asp:label id="lblProdType" Runat="server" /> *:</td>
                <td colspan=4><asp:DropDownList id="lstProdType" CssClass="font9Tahoma"  width="36%" runat="server" />
                <asp:Label id=lblErrProdType visible=false Text="Please select Product Type." forecolor=red runat=server />
                </td>
                <td style="width: 418px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 258px" ><asp:label id="lblProdCat" Runat="server" /> *:</td>
                <td colspan=4><asp:DropDownList id="lstProdCat" CssClass="font9Tahoma"  width="36%" runat="server" />
                <asp:Label id=lblErrProdCat visible=false Text="Please select Product Category." forecolor=red runat=server />
                </td>
                <td style="width: 418px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 258px; height: 51px;"><asp:label id="Label4" Runat="server" text="Fuel Item Type "/> :</td>
                <td style="height: 51px"><asp:RadioButton id="Fuel_No" runat="server" TextAlign="Right" Text="Not Applicable" GroupName="FuelItm" Checked="True"/><br>
                    <asp:RadioButton id="Fuel_Yes" runat="server" TextAlign="Right" Text="Fuel" GroupName="FuelItm"/><br>
                    <asp:RadioButton id="Fuel_Lub" runat="server" TextAlign="Right" Text="Lubricant" GroupName="FuelItm"/></td>
            </tr>
			<!-- Millware 2.9 PRM 19 Jul 2006 -->
            <tr>
                <td style="width: 258px"><asp:label id="Label5" Runat="server" text="Item Type "/> :</td>
                <td><asp:RadioButton id="Itm_Stock" runat="server" TextAlign="Right" Text="Stock" OnCheckedChanged="ItmType_CheckedChanged" AutoPostBack="True" GroupName="ItmType" Checked="True"/><br>
                    <asp:RadioButton id="Itm_Workshop" runat="server" TextAlign="Right" Text="Workshop" OnCheckedChanged="ItmType_CheckedChanged" AutoPostBack="True" GroupName="ItmType"/><td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td style="width: 418px">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 258px"><asp:label id="lblProdBrand" Runat="server" /> :</td>
                <td colspan=4><asp:DropDownList id="lstProdBrand" CssClass="font9Tahoma"  width="36%" runat="server" /></td>
            </tr>
            <tr>
                <td style="width: 258px"><asp:label id="lblProdModel" Runat="server" /> *:</td>
                <td colspan=4><asp:DropDownList id="lstProdModel" CssClass="font9Tahoma" width="36%" runat="server" />
                <asp:Label id=lblErrProdModel visible=false Text="Please select Product Model." forecolor=red runat=server />
                </td>
            </tr>
            <tr>
                <td style="width: 258px"><asp:label id="lblProdMat" Runat="server" /> :</td>
                <td colspan=4><asp:DropDownList id="lstProdMat" CssClass="font9Tahoma"  width="36%" runat="server" /></td>
            </tr>
            <tr>
                <td style="width: 258px"><u><b>Unit of Measurement</b></u></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
               
            </tr>
      

              <tr>
                <td style="width: 258px"><asp:label id="Label13" Runat="server" text="Inventory "/> :*</td>
                <td colspan=4><asp:DropDownList id="InventoryUOM" CssClass="font9Tahoma"  width=95% runat="server" />
                    <asp:Label id="lblErrInventoryUOM" forecolor=red text="<BR>Please select unit of measurement." visible=false runat=server/></td>
            </tr>
            <tr>
                <td style="width: 258px" valign="top">
                    Additional Note :</td>
                <td colspan="4">
                    <asp:TextBox id="Remark" CssClass="font9Tahoma"  runat="server" maxlength="256" width=95% TextMode="MultiLine" Height="56px" /></td>
            </tr>
 
            
            <tr>
                <td colspan="6" style="height: 23px">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:ImageButton ID="btnNew" runat="server" AlternateText="New" ImageUrl="../../images/butt_new.gif"
                        OnClick="btnNew_Click" UseSubmitBehavior="false" />
                    <asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
                    <asp:ImageButton id="Delete" CausesValidation=False imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete"/>                    
                    <asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
                    <br />
                </td>
            </tr>

        </table>
                    </div>
            </td>
        </tr>
           </table   
    </form>
</body>
</html>