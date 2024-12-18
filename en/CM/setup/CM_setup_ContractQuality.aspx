<%@ Page Language="vb" src="../../../include/CM_setup_ContractQuality.aspx.vb" Inherits="CM_setup_ContractQuality" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_cm_setup" src="../../menu/menu_cmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contract Quality Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<body>
		    <form id="frmCurr" class="main-modul-bg-app-list-pu" runat="server">
			<asp:label id="SortExpression" visible="False" runat="server"></asp:label>
			<asp:label id="blnUpdate" visible="false" runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="sortcol" visible="False" runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />
			
			<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:menu_cm_setup id=menu_cm_setup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3"><strong> CONTRACT QUALITY DETAILS</strong></td>
					<td align="right" colspan="3" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6 width=100%>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td valign=top colspan=3 height=25>
									<asp:Label id="Label1" Font-Bold="True" Font-Size="10pt" text="CPO" runat=server/>
								</td>
								<td width=10%>
									Status :
								</td>
								<td width=30%>
									<asp:Label id=lblStatus runat=server />
								</td>
							</tr>
							<tr class="mb-t">
								<td valign=top width=2% height=25>
								</td>								
								<td valign=top colspan=2>
									<asp:Label id="Label2" Font-Bold="True"  font-underline="true" text="LTC Biasa" runat=server/>									
								</td>
								<td valign=top>
									Date Created :
								</td>
								<td>
									<asp:Label id=lblDateCreated runat=server />									
								</td>
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									FFA :*
								</td>
								<td valign=top> 
									<asp:TextBox id="txtLTCBiasaFFA" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvLTCBiasaFFA display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter FFA."
										ControlToValidate=txtLTCBiasaFFA/>	
									<asp:RegularExpressionValidator id="revLTCBiasaFFA" 
										ControlToValidate="txtLTCBiasaFFA"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvLTCBiasaFFA"
										ControlToValidate="txtLTCBiasaFFA"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
								<td valign=top>
									Last Update :
								</td>
								<td valign=top>
									<asp:Label id=lblLastUpdate runat=server />
								</td>
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									M&I :*
								</td>
								<td valign=top>
									<asp:TextBox id="txtLTCBiasaMI" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvLTCBiasaMI display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter M&I."
										ControlToValidate=txtLTCBiasaMI/>	
									<asp:RegularExpressionValidator id="revLTCBiasaMI" 
										ControlToValidate="txtLTCBiasaMI"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvLTCBiasaMI"
										ControlToValidate="txtLTCBiasaMI"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>			
								<td valign=top>
									Update By :
								</td>
								<td valign=top>
									<asp:Label id=lblUpdatedBy runat=server />
								</td>
							</tr>
							
							<tr class="mb-t">
								<td valign=top width=2%>
								</td>								
								<td valign=top colspan=5>
									<asp:Label id="Label3" Font-Bold="True"  font-underline="true" text="LTC Forward" runat=server/>									
								</td>
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									FFA :*
								</td>
								<td valign=top colspan=3> 
									<asp:TextBox id="txtLTCForwardFFA" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvLTCForwardFFA display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter FFA."
										ControlToValidate=txtLTCForwardFFA/>	
									<asp:RegularExpressionValidator id="revLTCForwardFFA" 
										ControlToValidate="txtLTCForwardFFA"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvLTCForwardFFA"
										ControlToValidate="txtLTCForwardFFA"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									M&I :*
								</td>
								<td valign=top colspan=3>
									<asp:TextBox id="txtLTCForwardMI" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvLTCForwardMI display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter M&I."
										ControlToValidate=txtLTCForwardMI/>	
									<asp:RegularExpressionValidator id="revLTCForwardMI" 
										ControlToValidate="txtLTCForwardMI"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvLTCForwardMI"
										ControlToValidate="txtLTCForwardMI"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>			
							</tr>
							
							<tr class="mb-t">
								<td valign=top width=2%>
								</td>								
								<td valign=top colspan=5>
									<asp:Label id="Label4" Font-Bold="True"  font-underline="true" text="Spot Lokal" runat=server/>									
								</td>
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									FFA :*
								</td>
								<td valign=top colspan=3> 
									<asp:TextBox id="txtSpotLokalFFA" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvSpotLokalFFA display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter FFA."
										ControlToValidate=txtSpotLokalFFA/>	
									<asp:RegularExpressionValidator id="revSpotLokalFFA" 
										ControlToValidate="txtSpotLokalFFA"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvSpotLokalFFA"
										ControlToValidate="txtSpotLokalFFA"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									M&I :*
								</td>
								<td valign=top colspan=3>
									<asp:TextBox id="txtSpotLokalMI" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvSpotLokalMI display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter M&I."
										ControlToValidate=txtSpotLokalMI/>	
									<asp:RegularExpressionValidator id="revSpotLokalMI" 
										ControlToValidate="txtSpotLokalMI"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvSpotLokalMI"
										ControlToValidate="txtSpotLokalMI"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>						
								</td>			
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									DOBI :*
								</td>
								<td valign=top colspan=3>
									<asp:TextBox id="txtSpotLokalDOBI" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvSpotLokalDOBI display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter DOBI."
										ControlToValidate=txtSpotLokalDOBI/>	
									<asp:RegularExpressionValidator id="revSpotLokalDOBI" 
										ControlToValidate="txtSpotLokalDOBI"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="RangeValidator1"
										ControlToValidate="txtSpotLokalDOBI"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>						
								</td>			
							</tr>
							
							<tr class="mb-t">
								<td valign=top width=2%>
								</td>								
								<td valign=top colspan=5>
									<asp:Label id="Label5" Font-Bold="True"  font-underline="true" text="Spot Export" runat=server/>									
								</td>
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									FFA :*
								</td>
								<td valign=top colspan=3> 
									<asp:TextBox id="txtSpotExportFFA" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvSpotExportFFA display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter FFA."
										ControlToValidate=txtSpotExportFFA/>	
									<asp:RegularExpressionValidator id="revSpotExportFFA" 
										ControlToValidate="txtSpotExportFFA"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvSpotExportFFA"
										ControlToValidate="txtSpotExportFFA"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									M&I :*
								</td>
								<td valign=top colspan=3>
									<asp:TextBox id="txtSpotExportMI" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvSpotExportMI display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter M&I."
										ControlToValidate=txtSpotExportMI/>	
									<asp:RegularExpressionValidator id="revSpotExportMI" 
										ControlToValidate="txtSpotExportMI"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvSpotExportMI"
										ControlToValidate="txtSpotExportMI"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>						
								</td>			
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									DOBI :*
								</td>
								<td valign=top colspan=3>
									<asp:TextBox id="txtSpotExportDOBI" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvSpotExportDOBI display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter DOBI."
										ControlToValidate=txtSpotExportDOBI/>	
									<asp:RegularExpressionValidator id="revSpotExportDOBI" 
										ControlToValidate="txtSpotExportDOBI"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvSpotExportDOBI"
										ControlToValidate="txtSpotExportDOBI"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>						
								</td>			
							</tr>							<tr class="mb-t">
								<td valign=top colspan=5>
									<asp:Label id="Label6" Font-Bold="True" Font-Size="10pt" text="PK" runat=server/>
								</td>
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									Moisture :*
								</td>
								<td valign=top colspan=3> 
									<asp:TextBox id="txtMoisture" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvMoisture display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Moisture."
										ControlToValidate=txtMoisture/>	
									<asp:RegularExpressionValidator id="revMoisture" 
										ControlToValidate="txtMoisture"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvMoisture"
										ControlToValidate="txtMoisture"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>						
								</td>
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									Impurity :*
								</td>
								<td valign=top colspan=3>
									<asp:TextBox id="txtImpurity" maxlength="10" runat="server"/>
									<asp:RequiredFieldValidator id=rfvImpurity display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Impurity."
										ControlToValidate=txtImpurity/>	
									<asp:RegularExpressionValidator id="revImpurity" 
										ControlToValidate="txtImpurity"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvImpurity"
										ControlToValidate="txtImpurity"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>						
								</td>			
							</tr>
						</table>
					</td>
				</tr>			
				<tr>
					<td colspan=6><asp:label id=lblIsExist visible=false forecolor=red runat="server"/>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />						
					</td>
				</tr>			
				<Input Type=Hidden id=tbcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text=0 runat=server/>
			</table>
			</FORM>
		</body>
</html>
