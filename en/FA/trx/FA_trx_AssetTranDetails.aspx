<%@ Page Language="vb" Src="~/include/FA_trx_AssetTranDetails.aspx .vb" Inherits="FA_trx_AssetTranDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFATrx" src="../../menu/menu_FATrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FIXED ASSET - Asset Transfer Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="Javascript">
			<!--
			function calNet() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtAssetValue.value);				
				var b = parseFloat(doc.txtAccumDeprValue.value);
				var c = a - b;
				if (c == 'NaN')
					doc.txtTranValue.value = '';
				else
					doc.txtTranValue.value = round(c, 0);
			}
			-->
		</script>		
	    </head>
	<body>
		<form id="frmMain"  class="main-modul-bg-app-list-pu" runat="server">
          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server"></asp:label>
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
            <asp:label id=hidAccMonth visible=False text="0" runat=server />
            <asp:label id=hidAccYear visible=False text="0" runat=server />
			<table cellspacing="1" cellpadding="1" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6">
						<UserControl:MenuFATrx id=MenuFATrx runat="server" />
					</td>
				</tr>
				<tr>
					<td   colspan="6" width=60%>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="font9Tahoma">
                                    <asp:label id="lblTitle" runat="server" /> DETAILS</td>
                                <td class="font9Header">
                                    Period :&nbsp; <asp:Label id="lblAccPeriod" runat="server"/>&nbsp;| Status : <asp:Label id="lblStatus" runat="server"/>&nbsp;| Date Created : <asp:Label id="lblCreateDate" runat="server"/>&nbsp;| Last Update : <asp:Label id="lblLastUpdate" runat="server"/>&nbsp;| Update By : <asp:Label id="lblUpdateBy" runat="server"/>
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
						<asp:Label id=lblDupMsg visible=false forecolor=red text="<br>This code has been used. Please try again." display=dynamic runat=server/>
					</td>
					<td width="10%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblRefNoTag text="Reference No" Runat="server"/> :*</td>
					<td valign=center><asp:TextBox id="txtRefNo" runat="server" width=100% maxlength="32" CssClass="font9Tahoma"/>
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
					<td>&nbsp;</td>
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
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAssetCodeTag Runat="server"/> :*</td>
					<td colspan="4"><asp:DropDownList id="ddlAssetCode" Width=87% OnSelectedIndexChanged=Get_Asset_Details AutoPostBack=True runat=server CssClass="font9Tahoma"/>
					<input type=button value=" ... " id="Find" onclick="javascript:PopFA('frmMain', '', 'ddlAssetCode', 'True');" CausesValidation=False runat=server />  
						<asp:label id=lblAssetCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Transfer To:*</td>
					<td><asp:DropDownList id="ddlTranTo"  Width=100% runat=server CssClass="font9Tahoma"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				
				
				<tr>
					<td height=25>Transfer Qty :*</td>
					<td valign=center><asp:TextBox id="txtQty" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator 
							id="RequiredFieldValidator1" 
							runat="server"  
							ControlToValidate="txtQty" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="RegularExpressionValidator1" 
							ControlToValidate="txtQty"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="RangeValidator1"
							ControlToValidate="txtQty"
							MinimumValue="0.01"
							MaximumValue="99999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is 0.01 and Maximum is 99999999999999999999"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				
				
				<tr>
					<td height=25><asp:label id=lblAssetValueTag text="Transfer Asset Value" Runat="server"/> :*</td>
					<td valign=center><asp:TextBox id="txtAssetValue" ReadOnly=True OnKeyUp="javascript:calNet();" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:label id=lblAssetValueZeroErr Text="Asset Value cannot be zero." Visible=False forecolor=red Runat="server" />
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
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvAssetValue"
							ControlToValidate="txtAssetValue"
							MinimumValue="-99999999999999999999"
							MaximumValue="99999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is -99999999999999999999 and Maximum is 99999999999999999999"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAccumDeprValueTag text="Transfer Depreciation Value" Runat="server"/> :*</td>
					<td valign=center><asp:TextBox id="txtAccumDeprValue" ReadOnly=True OnKeyUp="javascript:calNet();" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator 
							id="rfvAccumDeprValue" 
							runat="server"  
							ControlToValidate="txtAccumDeprValue" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revAccumDeprValue" 
							ControlToValidate="txtAccumDeprValue"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvAccumDeprValue"
							ControlToValidate="txtAccumDeprValue"
							MinimumValue="-99999999999999999999"
							MaximumValue="99999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is -99999999999999999999 and Maximum is 99999999999999999999"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblTranValueTag text="Transfer Value" Runat="server"/> :</td>
					<td><asp:TextBox id=txtTranValue ReadOnly=True Runat="server" CssClass="font9Tahoma"/>
						<asp:label id=lblTranValueErr Text="Asset Value, Depreciation Value and Write Off Value must be with the same numeric sign." Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblRemarkTag text="Remarks" Runat="server"/> :</td>
					<td colspan=4 valign=center><asp:TextBox id="txtRemark" runat="server" width=100% maxlength="256" CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><asp:label id=lblDeleteErr text="Insufficient value in the asset to perform operation or no permission is set for this asset!" Visible=False forecolor=red Runat="server" />
					</td>				
				</tr>
				<tr>
				   <td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnConfirm imageurl="../../images/butt_confirm.gif" AlternateText="  Confirm  " onclick=btnConfirm_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" visible=true CausesValidation=false AlternateText=" Delete " onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="  Back  " onclick=btnBack_Click runat=server />
					    <asp:ImageButton id=btnNew imageurl="../../images/butt_new.gif" CausesValidation=False AlternateText="  New  " onclick=btnNew_Click runat=server />
					    <br />
					</td>
				</tr>
				<tr>
				   <td colspan="6">** Please Input Date First Before Select Asset **</td>
				</tr>
			</table>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
