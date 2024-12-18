<%@ Page Language="vb" src="../../../include/PR_setup_RouteDet.aspx.vb" Inherits="PR_Setup_RouteDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Route Code Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
<tr>
	<td style="width: 100%; height: 1500px" valign="top">
	<div class="kontenlist">
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblDefault" visible="false" text="Default" runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=routecode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6"> <strong><asp:label id="lblTitle" runat="server" /> DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height="25"><asp:label id="lblTitle1" runat="server"/> Code :* </td>
					<td width="30%">
						<!--
						<asp:Textbox id="txtADCode" width="50%" maxlength="8" runat="server"/>
						-->
						<asp:Textbox id="txtRouteCode" width="50%" maxlength="8" runat="server"/>
						<asp:Label id=lblErrDupRouteCode visible=false forecolor=red text="<br>Route code in used, please try other Route Code." runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Route Code"
							ControlToValidate=txtRouteCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtRouteCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblDesc" runat="server" /> :* </td>
					<td><asp:Textbox id=txtDesc maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Description"
							ControlToValidate=txtDesc />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<comment>Modified By BHL</comment>
					<td height=25><asp:label id="lblVehType" runat="server" /> :*</td>
					<td><asp:dropdownlist id=ddlVehType width=100% runat=server/>
						<asp:Label id=lblErrVehType visible=false forecolor=red text="<br>Please select one Vehicle Type." runat=server/>
					</td>
					<comment>End Modified</comment>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Distance :* </td>
					<td><asp:Textbox id=txtDistance maxlength=15 width=50% runat=server/> KM
						<asp:RequiredFieldValidator id=rvfDistance display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Distance (KM)."
							ControlToValidate=txtDistance />
						<asp:CompareValidator id="cvDistance" display=dynamic runat="server" 
							ControlToValidate="txtDistance" Text="<br>Distance must be a double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
								id=revDistance
								ControlToValidate=txtDistance
								ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
								Display="Dynamic"
								text = "<br>Maximum length 9 digits and 5 decimals. "
								runat="server"/></td>					
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Load Volume Basis :*</td>
					<td><asp:Textbox id=txtLoadBasis maxlength=15 width=50% runat=server/> KG
						<asp:RequiredFieldValidator id=rvfLoadBasis display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Load Volume Basis."
							ControlToValidate=txtLoadBasis />
						<asp:CompareValidator id="cvLoadBasis" display=dynamic runat="server" 
							ControlToValidate="txtLoadBasis" Text="<br>Load Volume Basis must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revLoadBasis
							ControlToValidate=txtLoadBasis
							ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
							Display="Dynamic"
							text = "<br>Maximum length 9 digits and 5 decimals. "
							runat="server"/></td>
					<td colspan=4>&nbsp;</td>					
				</tr>
				<tr>
					<td height=25 valign=top>Basis Incentive :*</td>
					<td><asp:Textbox id=txtBasisInc maxlength=15 width=50% runat=server/> Rp
						<asp:RequiredFieldValidator id=rvfBasisInc display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Baisis Incentive."
							ControlToValidate=txtBasisInc />
						<asp:CompareValidator id="cvBasisInc" display=dynamic runat="server" 
							ControlToValidate="txtBasisInc" Text="<br>Load Basis Incentive must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revBasisInc
							ControlToValidate=txtBasisInc
							ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
							Display="Dynamic"
							text = "<br>Maximum length 9 digits and 5 decimals. "
							runat="server"/></td>
					<td colspan=4>&nbsp;</td>					
				</tr>
				<tr>
					<td height=25 valign=top><asp:label id="lblTitle2" runat="server" /> Rate :*</td>
					<td><asp:Textbox id=txtRouteRate maxlength=10 width=50% runat=server/> Rp / KG
						<asp:RequiredFieldValidator id=rvfRouteRate display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Route Rate."
							ControlToValidate=txtRouteRate />
						<asp:CompareValidator id="cvRouteRate" display=dynamic runat="server" 
							ControlToValidate="txtRouteRate" Text="<br>Route Rate must be an integer. " 
							Type="double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revRouteRate
							ControlToValidate=txtRouteRate
							ValidationExpression="\d{1,9}\.\d{1,2}|\d{1,9}"
							Display="Dynamic"
							text = "<br>Maximum length 9 digits and 2 decimals. "
							runat="server"/></td>
					<td colspan=4>&nbsp;</td>					
				</tr>
				<tr>
					<td height=25>Dump Type :*</td>
					<td><asp:dropdownlist id=ddlDumpType width=100% runat=server/>
						<asp:Label id=lblErrDumpType visible=false forecolor=red text="<br>Please select one Dump Type." runat=server/>
					</td>
					<td colspan=4>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
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
