<%@ Page Language="vb" Src="../../../include/FA_trx_AssetDeprDetails.aspx.vb" Inherits="FA_trx_AssetDeprDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFATrx" src="../../menu/menu_FATrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FIXED ASSET - Asset Depreciation Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		
			function setDeprValueF() {
				var doc = document.frmMain;
				doc.txtDeprValueF.value = doc.txtDeprValue.value;
			}
          
		</script>			
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style3
            {
                font-size: 9pt;
                font-family: Tahoma;
                width: 239px;
            }
            .style4
            {
                height: 8px;
            }
        </style>
	</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server"></asp:label>
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
            <asp:Label ID="hidAccMonth" runat="server" Text="0" Visible="False"></asp:Label>
            <asp:Label ID="hidAccYear" runat="server" Text="0" Visible="False"></asp:Label>
            <asp:Label ID="hidFiscalSame" runat="server" Text="0" Visible="False"></asp:Label>
            <table cellspacing="1" cellpadding="1" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6" class="style4">
						<UserControl:MenuFATrx id=MenuFATrx runat="server" />
					</td>
				</tr>
				<tr>
					<td   colspan="6" width=60%>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="style3">
                                  <strong> <asp:label id="lblTitle" runat="server" /> DETAILS </strong> </td>
                                <td class="font9Header"  style="text-align: right">
                                    Period :&nbsp; <asp:Label id="lblAccPeriod" runat="server"/>&nbsp;| Status : <asp:Label id="lblStatus" runat="server"/>&nbsp;| Date Created : <asp:Label id="lblCreateDate" runat="server"/>&nbsp;| Last Update : <asp:Label id="lblLastUpdate" runat="server"/>&nbsp;| Update By : <asp:Label id="lblUpdateBy" runat="server"/>
                                </td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td   colspan="4" width=60%>&nbsp;</td>
					<td colspan="2" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width="25%" height=25><asp:label id=lblTxIDTag Runat="server"/> :*</td>
					<td width="50%"><asp:label id=lblTxID Runat="server"/>
						<asp:Label id=lblDupMsg visible=false forecolor=red text="<br>This code has been used. Please try again." Display=dynamic runat=server/>
					</td>
					<td width="5%">&nbsp;</td>
					<td width="10%">&nbsp;</td>
					<td width="10%">&nbsp;</td>
					
				</tr>
				<tr>
					<td height=25><asp:label id=lblRefNoTag text="Reference No" Runat="server"/> :*</td>
					<td valign=center><asp:TextBox id="txtRefNo" runat="server" width=100% maxlength="32"/>
						<asp:RequiredFieldValidator 
							id="rfvRefNo" 
							runat="server"  
							ControlToValidate="txtRefNo" 
							text = "Field cannot be blank"
							Display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblRefDateTag text="Reference Date" Runat="server"/> :*</td>
					<td>
						<asp:TextBox id="txtRefDate" runat="server" width=50% maxlength="10"/>                       
						<a href="javascript:PopCal('txtRefDate');">
						<asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:RequiredFieldValidator 
							id="rfvRefDate" 
							runat="server"  
							ControlToValidate="txtRefDate" 
							text = "Field cannot be blank"
							Display="dynamic"/>
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
					<td><asp:DropDownList id="ddlAssetCode" Width=100% AutoPostBack=True OnSelectedIndexChanged=Get_Asset_Details  runat=server />
						<asp:label id=lblAssetCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Depresiasi Komersial:*</td>
					<td valign=center><asp:TextBox id="txtDeprValue" runat="server" width=50% maxlength="20" onkeyup="javascript:setDeprValueF()"/>
						<asp:label id=lblDeprValueZeroErr Text="Value cannot be zero." Visible=False forecolor=red Runat="server" />
						<asp:RequiredFieldValidator 
							id="rfvDeprValue" 
							runat="server"  
							ControlToValidate="txtDeprValue" 
							text = "Field cannot be blank"
							Display="dynamic"/>
						<asp:RegularExpressionValidator id="revDeprValue" 
							ControlToValidate="txtDeprValue"
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 0 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvDeprValue"
							ControlToValidate="txtDeprValue"
							MinimumValue="-9999999999999999999"
							MaximumValue="9999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is -99999999999999999999 and Maximum is 99999999999999999999"
							runat="server" Display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr id="DeprFiskalRow" runat="server" >
					<td height=25>Depresiasi Fiskal:*</td>
					<td valign=center><asp:TextBox id="txtDeprValueF" runat="server" width=50% maxlength="20"/>
						<asp:RequiredFieldValidator 
							id="rfvDeprValueF" 
							runat="server"  
							ControlToValidate="txtDeprValueF" 
							text = "Field cannot be blank"
							Display="dynamic"/>
						<asp:RegularExpressionValidator id="revDeprValueF" 
							ControlToValidate="txtDeprValueF"
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 0 decimal points"
							runat="server"/>
						<asp:RangeValidator id="rvDeprValueF"
							ControlToValidate="txtDeprValueF"
							MinimumValue="-9999999999999999999"
							MaximumValue="9999999999999999999"
							Type="Double"
							EnableClientScript="True"
							Text="The value is out of range. Minimum is -99999999999999999999 and Maximum is 99999999999999999999"
							runat="server" Display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>
                        &nbsp;</td>
					<td>&nbsp;
                        </td>
					<td>&nbsp;</td>
				</tr>
				
				
				<tr>
					<td height=25><asp:label id=lblRemarkTag text="Remarks" Runat="server"/> :</td>
					<td colspan=4 valign=center><asp:TextBox id="txtRemark" runat="server" width=100% maxlength="256"/></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id=lblNetValueTag  Runat="server"/> :</td>
					<td><asp:label id="lblNetValue" Runat="server"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id=lblFinalValueTag  Runat="server"/> :</td>
					<td><asp:label id="lblFinalValue" Runat="server"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				
				<tr>
					<td colspan=6><asp:label id=lblDeleteErr text="Insufficient value in the asset to perform operation or no permission is set for this asset!!" Visible=False forecolor=red Runat="server" />
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
