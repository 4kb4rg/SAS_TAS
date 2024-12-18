<%@ Page Language="vb" codefile="../../../include/FA_trx_AssetDispDetails.aspx.vb" Inherits="FA_trx_AssetDispDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFATrx" src="../../menu/menu_FATrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
 
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html>
	<head>
		<title>FIXED ASSET - Asset Disposal Details</title>
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
					doc.txtDispValue.value = '';
				else
					doc.txtDispValue.value = round(c, 2);
			}
			function calNetF() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtAssetValueF.value);				
				var b = parseFloat(doc.txtAccumDeprValueF.value);
				var c = a - b;
				if (c == 'NaN')
					doc.txtDispValueF.value = '';
				else
					doc.txtDispValueF.value = round(c, 2);
			}
			-->
		</script>	
		
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
		
	</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
			    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
              <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server"></asp:label>
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
            <asp:Label id=hidAccMonth visible=False text="0" runat=server />
            <asp:Label id=hidAccYear visible=False text="0" runat=server />
            <asp:label id=hidFiskalSame visible=False text="0" runat=server />
			<table cellspacing="1" cellpadding="1" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6">
						<UserControl:MenuFATrx id=MenuFATrx runat="server" />
					</td>
				</tr>
				<tr>
					<td colspan="6" width=60%>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                              <strong>  <asp:label id="lblTitle" runat="server" /> DETAILS </strong></td>
                                <td class="font9Header"  style="text-align: right">
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
					<td width="20%" height=25><asp:label id=lblTxIDTag Runat="server"/> :*</td>
					<td width="25%"><asp:label id=lblTxID Runat="server"/>
						<asp:Label id=lblDupMsg visible=false forecolor=red text="<br>This code has been used. Please try again." display=dynamic runat=server/>
					</td>
					<td width="10%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="25%">&nbsp;</td>
					<td width="5%">&nbsp;</td>
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
					<td colspan="4">
                        <telerik:RadComboBox CssClass="fontObject" ID="RadAssetCode" runat="server" AllowCustomText="True" AutoPostBack=True 
                            EmptyMessage="Please Select Asset Code" Height="200" Width="100%" ExpandDelay="50"
                            Filter="Contains" Sort="Ascending" EnableVirtualScrolling="True" OnSelectedIndexChanged=Get_Asset_Details >
                            <CollapseAnimation Type="InQuart" />
                        </telerik:RadComboBox> 
						<asp:label id=lblAssetCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>
                        Nilai Perolehan :*</td>
					<td valign=center><asp:TextBox id="txtNP" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:label id=lblErrNP Text="Asset Value cannot be zero." Visible=False forecolor=red Runat="server" />
						<asp:RequiredFieldValidator 
							id="RequiredFieldValidator2" 
							runat="server"  
							ControlToValidate="txtNP" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="RegularExpressionValidator1" 
							ControlToValidate="txtNP"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="RangeValidator1"
							ControlToValidate="txtNP"
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
					<td height=25>Bill Party :*</td>
					<td> 
					<asp:TextBox id="txtBillPartyCode" runat="server" width="100%" maxlength="256" CssClass="font9Tahoma"/><asp:RequiredFieldValidator 
							id="RequiredFieldValidator1" 
							runat="server"  
							ControlToValidate="txtBillPartyCode" 
							text = "Field cannot be blank"
							display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>
                        Sales Value (DPP) :*</td>
					<td valign=center><asp:TextBox id="txtSalesValue" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:label id=lblSalesValueZeroErr Text="Asset Value cannot be zero." Visible=False forecolor=red Runat="server" />
						<asp:RequiredFieldValidator 
							id="rfvSalesValue" 
							runat="server"  
							ControlToValidate="txtSalesValue" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revSalesValue" 
							ControlToValidate="txtSalesValue"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvSalesValue"
							ControlToValidate="txtSalesValue"
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
					<td height=25>
                        Sales Account :*</td>
					<td>
                        <telerik:RadComboBox CssClass="fontObject" ID="radSalesAccCode" runat="server" AllowCustomText="True" AutoPostBack="True"
                            EmptyMessage="Please Select Sales Account" Height="200" Width="100%" ExpandDelay="50"
                            Filter="Contains" Sort="Ascending" EnableVirtualScrolling="True" OnSelectedIndexChanged="CheckSalesAccBlk">
                            <CollapseAnimation Type="InQuart" />
                        </telerik:RadComboBox>
                      
						<asp:label id="lblSalesAccCodeErr" Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr id="SalesBlkCode" visible = false runat=server>
					<td height=25>
                        Sales Cost Center :*</td>
					<td><asp:DropDownList id="ddlSalesBlkCode" Width=100% runat=server CssClass="font9Tahoma" />

						<asp:label id="lblSalesBlkCodeErr" Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>
                        Disposal Account :*</td>
					<td>
                        <telerik:RadComboBox CssClass="fontObject" ID="radDispAccCode" runat="server" AllowCustomText="True" AutoPostBack="True"
                            EmptyMessage="Please Select Disposal Account" Height="200" Width="100%" ExpandDelay="50"
                            Filter="Contains" Sort="Ascending" EnableVirtualScrolling="True" OnSelectedIndexChanged="CheckDispAccBlk">
                            <CollapseAnimation Type="InQuart" />
                        </telerik:RadComboBox>						
					 				
						<asp:label id="lblDispAccCodeErr" Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				
				<tr id="DispBlkCode" visible = false runat=server>
					<td height=25>
                        Disposal Cost Center :*</td>
					<td><asp:DropDownList id="ddlDispBlkCode" Width=100% runat=server CssClass="font9Tahoma"/>
						<asp:label id="lblDispBlkCodeErr" Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr runat=server>
					<td height=25>
                        PPN :</td>
					<td> <asp:CheckBox ID="chkPPN" runat="server" visible="true" Text=" Yes"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>
                        Disposed Qty :*</td>
					<td valign=center><asp:TextBox id="txtQty" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:label id="lblQtyZeroErr" Text="Qty cannot be zero." Visible=False forecolor=red Runat="server" />
						<asp:RequiredFieldValidator 
							id="rfvQty" 
							runat="server"  
							ControlToValidate="txtQty" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revQty" 
							ControlToValidate="txtQty"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvQty"
							ControlToValidate="txtQty"
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
					<td height=25>
                        Disposed Asset Value :*</td>
					<td valign=center><asp:TextBox id="txtAssetValue" OnKeyUp="javascript:calNet();" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
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
					<td height=25>
                        Disposed Asset Fiscal Value :*</td>
					<td valign=center><asp:TextBox id="txtAssetValueF" OnKeyUp="javascript:calNetF();" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:label id=lblAssetValueZeroFErr Text="Asset Value cannot be zero." Visible=False forecolor=red Runat="server" />
						<asp:RequiredFieldValidator 
							id="rfvAssetValueF" 
							runat="server"  
							ControlToValidate="txtAssetValueF" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revAssetValueF" 
							ControlToValidate="txtAssetValueF"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvAssetValueF"
							ControlToValidate="txtAssetValueF"
							MinimumValue="-99999999999999999999"
							MaximumValue="99999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is -99999999999999999999 and Maximum is 99999999999999999999"
							runat="server" display="dynamic"/>
					</td>
				</tr>
				<tr>
					<td height=25>
                        Disposed Depreciation Value :*</td>
					<td valign=center><asp:TextBox id="txtAccumDeprValue" OnKeyUp="javascript:calNet();" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
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
					<td height=25>
                        Disposed Depreciation Fiscal Value :*</td>
					<td valign=center><asp:TextBox id="txtAccumDeprValueF" OnKeyUp="javascript:calNetF();" runat="server" width=100% maxlength="20" CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator 
							id="rfvAccumDeprValueF" 
							runat="server"  
							ControlToValidate="txtAccumDeprValueF" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revAccumDeprValueF" 
							ControlToValidate="txtAccumDeprValueF"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvAccumDeprValueF"
							ControlToValidate="txtAccumDeprValueF"
							MinimumValue="-99999999999999999999"
							MaximumValue="99999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is -99999999999999999999 and Maximum is 99999999999999999999"
							runat="server" display="dynamic"/>
					</td>
				</tr>
				<tr>
					<td height=25>Disposed Value :</td>
					<td><asp:TextBox id=txtDispValue ReadOnly=True Runat="server" CssClass="font9Tahoma"/>
						<asp:label id=lblDispValueErr Text="Asset Value, Depreciation Value and Disposed Value must be with the same numeric sign." Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td height=25>Disposed Fiscal Value:</td>
					<td><asp:TextBox id=txtDispValueF ReadOnly=True Runat="server" CssClass="font9Tahoma"/>
						<asp:label id=lblDispValueFErr Text="Asset Fiscal Value, Depreciation Fiscal Value and Disposed Fiscal Value must be with the same numeric sign." Visible=False forecolor=red Runat="server" />
					</td>
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
					    <br />
					</td>
				</tr>
				<tr>
					<td colspan="6">
						&nbsp;</td>
				</tr>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
