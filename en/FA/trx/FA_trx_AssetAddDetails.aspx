<%@ Page Language="vb" Src="../../../include/FA_trx_AssetAddDetails.aspx.vb" Inherits="FA_trx_AssetAddDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFATrx" src="../../menu/menu_FATrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FIXED ASSET - Asset Additional Details</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		
			function setValue() {
				var doc = document.frmMain;
				if (doc.hidQty.Value != 0) 
				 {
				    doc.txtAssetValue.value = (doc.hidAmount.value / doc.hidQty.value) * doc.txtQty.value;
				 }
				
			}
            
		</script>		
		
		
		
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                font-size: 9pt;
                font-family: Tahoma;
                width: 304px;
            }
            </style>
		
		
		
	</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
                  <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server"></asp:label>
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblPleaseSelectOne visible=false text="Please select one " runat=server />
			<asp:Label id=lblVehicleOption visible=false text=false runat=server/>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
            <asp:Label ID="hidAccMonth" runat="server" Text="0" Visible="False"></asp:Label><asp:Label
                ID="hidAccYear" runat="server" Text="0" Visible="False"></asp:Label>
                <table cellspacing="1" cellpadding="1" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6">
						<UserControl:MenuFATrx id=MenuFATrx runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6" width=60%>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="style2">
                               <strong> <asp:label id="lblTitle" runat="server" /> DETAILS </strong></td>
                                <td  class="font9Header" style="text-align: right">
                                    Period : <asp:Label id="lblAccPeriod" runat="server"/>&nbsp;| Status : <asp:Label id="lblStatus" runat="server"/>&nbsp;| Date Created : <asp:Label id="lblCreateDate" runat="server"/>&nbsp;| Last Update : <asp:Label id="lblLastUpdate" runat="server"/>&nbsp;| Update By :&nbsp; <asp:Label id="lblUpdateBy" runat="server"/>
                                </td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>&nbsp;</td>
					<td colspan="2" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width="25%" height=25><asp:label id=lblTxIDTag Runat="server"/> :*</td>
					<td width="30%"><asp:label id=lblTxID Runat="server"/>
						<asp:Label id=lblDupMsg visible=false forecolor=red text="<br>This code has been used. Please try again."  runat=server/>
					</td>
					<td width="10%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblRefNoTag text="Reference No" Runat="server"/> :*</td>
					<td valign=middle><asp:TextBox id="txtRefNo" runat="server" width=100% maxlength="32" CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator 
							id="rfvRefNo" 
							runat="server"  
							ControlToValidate="txtRefNo" 
							text = "Field cannot be blank"
							display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblRefDateTag text="Reference Date" Runat="server"/> :*</td>
					<td>
						<asp:TextBox id="txtRefDate" runat="server" width=60% maxlength="10" CssClass="font9Tahoma"/>                       
						<a href="javascript:PopCal('txtRefDate');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:RequiredFieldValidator 
							id="rfvRefDate" 
							runat="server"  
							ControlToValidate="txtRefDate" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:label id=lblRefDateErr Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
						<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAssetCodeTag Runat="server"/> :*</td>
					<td colspan="3"><asp:DropDownList id="ddlAssetCode" Width=90% runat=server AutoPostBack=True OnSelectedIndexChanged=Get_Asset_Details CssClass="font9Tahoma"/>
					<input type=button value=" ... " id="Find" onclick="javascript:PopFA('frmMain', '', 'ddlAssetCode', 'True');" CausesValidation=False runat=server />  
						<asp:label id=lblAssetCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
				
				<tr id="trAsset" runat="server">
					<td style="height: 25px">Asset Issue:</td>
					<td style="height: 25px" colspan="3"><asp:DropDownList id="ddlAsset" CssClass="font9Tahoma" Width="90%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectAsset"/>
					</td>
					<td>Qty : <asp:TextBox id="txtQty" Runat="server" width="50%" maxlength="20" onkeyup="javascript:setValue()" CssClass="font9Tahoma"/></td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
				
				
				<tr>
					<td height=25><asp:label id=lblAssetValueTag text="Asset Additional Value" Runat="server"/> :*</td>
					<td valign=middle><asp:TextBox id="txtAssetValue"  runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:label id=lblAssetValueZeroErr Text="Asset Additional Value cannot be zero." Visible=False forecolor=red Runat="server" />
						<asp:RequiredFieldValidator 
							id="rfvAssetValue" 
							runat="server"  
							ControlToValidate="txtAssetValue" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revAssetValue" 
							ControlToValidate="txtAssetValue"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvAssetValue"
							ControlToValidate="txtAssetValue"
							MinimumValue="-9999999999999999999"
							MaximumValue="9999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is -9999999999999999999 and Maximum is 9999999999999999999"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
					<tr>
						<td height=25><asp:label id="lblAccount" runat="server" />  :* </td>
						<td>
						<asp:DropDownList width=87% id=ddlAccount onselectedindexchanged=CheckAccBlk autopostback=true runat=server CssClass="font9Tahoma"/>
							<input type=button value=" ... " id="Button1" onclick="javascript:PopCOA('frmMain', '', 'ddlAccount', 'True');" CausesValidation=False runat=server />  										
							<asp:Label id=lblErrAccount visible=false forecolor=red runat=server/>
						</td>
						 <td>&nbsp;</td>
						<td>&nbsp;</td>
					    <td>&nbsp;</td>
					    <td style="width: 37px">&nbsp;</td>
					</tr>
						
					<tr id="RowBlk" visible = false runat=server>
						<td height=25>Cost Center :*</td>
						<td><asp:DropDownList id=ddlBlock width=100% runat=server CssClass="font9Tahoma"/>
							<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/></td>
					    <td></td>
						<td></td>
						<td></td>
					</tr>
					
					
				<tr>
					<td height=25><asp:label id=lblRemarkTag text="Remarks" Runat="server"/> :</td>
					<td colspan=4 valign=middle><asp:TextBox id="txtRemark" runat="server" width=100% maxlength="256" CssClass="font9Tahoma"/></td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id=lblNetValueTag  Runat="server"/> :</td>
					<td><asp:label id="lblNetValue" Runat="server"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td style="width: 37px">&nbsp;</td>
				</tr>
				
	
				<tr>
					<td colspan=6><asp:label id=lblDeleteErr text="Insufficient value in the asset to perform operation or no permission is set for this asset!!" Visible=False forecolor=red Runat="server" />
					</td>				
				</tr>
				<tr>
					<td colspan=6 height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnConfirm imageurl="../../images/butt_confirm.gif" AlternateText="  Confirm  " onclick=btnConfirm_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" visible=true CausesValidation=false AlternateText=" Delete " onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="  Back  " onclick=btnBack_Click runat=server />
					    <br />
					</td>
				</tr>
			</table>
				<Input type=hidden id=hidBlockCharge value="" runat=server/>
				<Input type=hidden id=hidChargeLocCode value="" runat=server/>
				<Input type=hidden id=hidQty value="0" runat=server/>
				<Input type=hidden id=hidAmount value="0" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
