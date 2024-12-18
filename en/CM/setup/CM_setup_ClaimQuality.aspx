<%@ Page Language="vb" src="../../../include/CM_setup_ClaimQuality.aspx.vb" Inherits="CM_setup_ClaimQuality" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_cm_setup" src="../../menu/menu_cmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Claim Quality Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server"/>--%>
		<body>
		    <form id="frmCurr" runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SortExpression" visible="False" runat="server"></asp:label>
			<asp:label id="blnUpdate" visible="false" runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="sortcol" visible="False" runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />

<table border="0" cellspacing="1" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3"></td>
					<td align="right" colspan="3" ><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100%>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td valign=top height=30 colspan=2>
									<asp:Label id="Label1" Font-Bold="True" Font-Size="10pt" text="CPO" runat=server/>
								</td>								
								<td width=10% align=center>
									From
								</td>					
								<td width=10% align=center>
									To
								</td>		
								<td align=center>
									Condition
								</td>
								<td width=10%>
								</td>		
								<td width=10%>
									Status :
								</td>
								<td width=30%>
									<asp:Label id=lblStatus runat=server />
								</td>
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									FFA(%) :*
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtFFAFrom" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvFFAFrom display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter FFA."
										ControlToValidate=txtFFAFrom/>	
									<asp:RegularExpressionValidator id="revFFAFrom" 
										ControlToValidate="txtFFAFrom"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvFFAFrom"
										ControlToValidate="txtFFAFrom"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>	
						
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtFFATo" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvFFATo display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter FFA."
										ControlToValidate=txtFFATo/>	
									<asp:RegularExpressionValidator id="revFFATo" 
										ControlToValidate="txtFFATo"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvFFATo"
										ControlToValidate="txtFFATo"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>		
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtFFACondition" maxlength="20" runat="server"/>
								</td>
								<td >
								</td>		
								<td valign=top>
									Date Created :
								</td>
								<td>
									<asp:Label id=lblDateCreated runat=server />									
								</td>
							</tr>
							<tr>
								<td colspan=2>
								</td>		
								<td colspan=6>									
									<asp:CompareValidator id="cvFFAFrom" runat="server" 
										Display="Dynamic" Operator="GreaterThanEqual" 
										Type="double" ControlToCompare="txtFFAFrom" 
										ErrorMessage="From must be smaller then To." 
										ControlToValidate="txtFFATo">
									</asp:CompareValidator>
								</td>
								
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									M&I(%) :*
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtMIFrom" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvMIFrom display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter M&I."
										ControlToValidate=txtMIFrom/>	
									<asp:RegularExpressionValidator id="revMIFrom" 
										ControlToValidate="txtMIFrom"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvMIFrom"
										ControlToValidate="txtMIFrom"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>			
								<td valign=top align=center> 
									<asp:TextBox id="txtMITo" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvMITo display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter M&I."
										ControlToValidate=txtMITo/>	
									<asp:RegularExpressionValidator id="revMITo" 
										ControlToValidate="txtMITo"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvMITo"
										ControlToValidate="txtMITo"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>			
								<td valign=top align=center> 
									<asp:TextBox id="txtMICondition" maxlength="20" runat="server"/>
								</td>			
								<td >
								</td>		
								<td valign=top>
									Last Update :
								</td>
								<td valign=top>
									<asp:Label id=lblLastUpdate runat=server />
								</td>
							</tr>
							<tr>
								<td colspan=2>
								</td>		
								<td colspan=6>									
									<asp:CompareValidator id="cvMIFrom" runat="server" 
										Display="Dynamic" Operator="GreaterThanEqual" 
										Type="double" ControlToCompare="txtMIFrom" 
										ErrorMessage="From must be smaller then To." 
										ControlToValidate="txtMITo">
									</asp:CompareValidator>
								</td>
								
							</tr>
							<tr class="mb-t">
								<td valign=top>
								</td>				
								<td valign=top>
									DOBI(%) :*
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtDOBIFrom" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvDobiFrom display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Dobi."
										ControlToValidate=txtDobiFrom/>	
									<asp:RegularExpressionValidator id="revDobiFrom" 
										ControlToValidate="txtDobiFrom"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvDobiFrom"
										ControlToValidate="txtDobiFrom"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtDOBITo" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvDobiTo display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Dobi."
										ControlToValidate=txtDobiTo/>	
									<asp:RegularExpressionValidator id="revDobiTo" 
										ControlToValidate="txtDobiTo"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvDobiTo"
										ControlToValidate="txtDobiTo"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtDOBICondition" maxlength="20" runat="server"/>
								</td>
								<td >
								</td>		
								<td valign=top>
									Update By :
								</td>
								<td valign=top>
									<asp:Label id=lblUpdatedBy runat=server />
								</td>
							</tr>		
							<tr>
								<td colspan=2>
								</td>		
								<td colspan=6>									
									<asp:CompareValidator id="cvDobiFrom" runat="server" 
										Display="Dynamic" Operator="GreaterThanEqual" 
										Type="double" ControlToCompare="txtDobiFrom" 
										ErrorMessage="From must be smaller then To." 
										ControlToValidate="txtDobiTo">
									</asp:CompareValidator>
								</td>
								
							</tr>	
							<tr class="mb-t">
								<td valign=top colspan=7>
									<asp:Label id="Label2" Font-Bold="True" Font-Size="10pt" text="PK" runat=server/>
								</td>
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									Moist (%) :*
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtPKMIFrom" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvPKMIFrom display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Moist."
										ControlToValidate=txtPKMIFrom/>	
									<asp:RegularExpressionValidator id="revPKMIFrom" 
										ControlToValidate="txtPKMIFrom"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvPKMIFrom"
										ControlToValidate="txtPKMIFrom"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtPKMITo" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id=rfvPKMITo display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Moist."
										ControlToValidate=txtPKMITo/>	
									<asp:RegularExpressionValidator id="revPKMITo" 
										ControlToValidate="txtPKMITo"
										ValidationExpression="^(\+|-)?\d{1,3}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 3 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvPKMITo"
										ControlToValidate="txtPKMITo"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtPKMICondition" maxlength="20" runat="server"/>
								</td>
							</tr>
							<tr>
								<td colspan=2>
								</td>		
								<td colspan=6>									
									<asp:CompareValidator id="cvPKMIFrom" runat="server" 
										Display="Dynamic" Operator="GreaterThanEqual" 
										Type="double" ControlToCompare="txtPKMIFrom" 
										ErrorMessage="From must be smaller then To." 
										ControlToValidate="txtPKMITo">
									</asp:CompareValidator>
								</td>								
							</tr>
							<tr class="mb-t" >
								<td valign=top>
								</td>				
								<td valign=top>
									Impurity (%) :*
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtPKImpurityFrom" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id="rfvPKImpurityFrom" display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Impurity."
										ControlToValidate=txtPKImpurityFrom/>	
									<asp:RegularExpressionValidator id="revPKImpurityFrom" 
										ControlToValidate="txtPKImpurityFrom"
										ValidationExpression="^(\+|-)?\d{1,2}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 2 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvPKImpurityFrom"
										ControlToValidate="txtPKImpurityFrom"
										MinimumValue="0"
										MaximumValue="99.99"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 99.99."
										runat="server" display="dynamic"/>							
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtPKImpurityTo" maxlength="20" runat="server" width=40%/>
									<asp:RequiredFieldValidator id="rfvPKImpurityTo" display=Dynamic runat=server 
										ErrorMessage="<br>Please Enter Impurity."
										ControlToValidate=txtPKImpurityTo/>	
									<asp:RegularExpressionValidator id="revPKImpurityTo" 
										ControlToValidate="txtPKImpurityTo"
										ValidationExpression="^(\+|-)?\d{1,2}(\.\d{1,2})?$" 
										Display="Dynamic"
										text = "<br>Maximum length 2 digits and 2 decimal points."
										runat="server"/>
									<asp:RangeValidator 
										id="rvPKImpurityTo"
										ControlToValidate="txtPKImpurityTo"
										MinimumValue="0"
										MaximumValue="100"
										Type="double"
										EnableClientScript="True"
										Text="<br>The value must in between 0 and 100."
										runat="server" display="dynamic"/>							
								</td>
								<td valign=top align=center> 
									<asp:TextBox id="txtPKImpurityCondition" maxlength="20" runat="server"/>
								</td>
							</tr>
							<tr>
								<td colspan=2>
								</td>		
								<td colspan=6>									
									<asp:CompareValidator id="cvPKImpurityFrom" runat="server" 
										Display="Dynamic" Operator="GreaterThanEqual" 
										Type="double" ControlToCompare="txtPKImpurityFrom" 
										ErrorMessage="From must be smaller then To." 
										ControlToValidate="txtPKImpurityTo">
									</asp:CompareValidator>
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
