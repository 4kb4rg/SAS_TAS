<%@ Page Language="vb" src="../../../include/GL_mthend_GCDist_Loc.aspx.vb" Inherits="GL_mthend_GCDist_Loc" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLMthEnd" src="../../menu/menu_GLMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>

<head>
	<title>General Charges Distribution</title>
       <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<script language="javascript">
		function calTotalAllocation() {
			var doc = document.frmMain;
			var a = parseFloat(doc.txtMature.value);
			var b = parseFloat(doc.txtImmature.value);
			document.getElementById('totalAllocation').innerHTML = a + b;
			if (document.getElementById('totalAllocation').innerHTML == 'NaN') {
				document.getElementById('totalAllocation').innerHTML = '';
			}
			else {
				document.getElementById('totalAllocation').innerHTML = round(document.getElementById('totalAllocation').innerHTML, 2) + '%';
			}
		}
	</script>		
</head>
<body onload="javascript:calTotalAllocation();" >
	<form id=frmMain runat=server>
    <div class="kontenlist">
		<table border=0 cellspacing=0 cellpading=2 width="100%" class="font9Tahoma" >
		<tr>
			<td colspan=5>
				<UserControl:MenuGLMthEnd id=MenuGLMthEnd runat="server" />
			</td>
		</tr>
		<tr>
			<td  colspan="5"><strong> GENERAL CHARGES DISTRIBUTION</strong></td>
		</tr>
		<tr>
			<td colspan=5><hr size="1" noshade></td>
		</tr>
		<tr>
			<td width=20% heigh=25>Accounting Period :*</td>
			<td width=30%>
				<asp:TextBox id=txtAccMonth width=10% maxlength=2 runat=server /> / <asp:TextBox id=txtAccYear width=20% maxlength=4 runat=server />
				<asp:Label id=lblErrAccPeriod visible=false forecolor=red text="<br>The given accounting period has been used, please try another accounting period." runat=server />
				<asp:Label id=lblErrAccCfg visible=false forecolor=red text="<br>The given accounting period has not been setup in period configuration. Please configure your period before do any general charges distribution." runat=server />
				<asp:RequiredFieldValidator id=rfvAccMonth display=dynamic runat=server 
					ErrorMessage="<br>Please enter accounting month. " 
					ControlToValidate=txtAccMonth />
				<asp:RequiredFieldValidator id=rfvAccYear display=dynamic runat=server 
					ErrorMessage="<br>Please enter accounting year. " 
					ControlToValidate=txtAccYear />
				<asp:RangeValidator id="rvAccMonth"
					ControlToValidate="txtAccMonth"
					MinimumValue="1"
					MaximumValue="24"
					Type="Integer"
					EnableClientScript="true"
					Text="Valid accounting month must be within 1 to 23"
					runat="server"/>
                <asp:RegularExpressionValidator id="revTxtAccYear" 
                     ControlToValidate="txtAccYear"
                     ValidationExpression="\d{4}"
                     Display="Dynamic"
                     ErrorMessage="<br>Accounting Year must be in 4 digits"
                     EnableClientScript="True" 
                     runat="server"/>
			</td>
			<td width=5%>&nbsp;</td>
			<td width=15%>Status :</td>
			<td width=30%><asp:Label id=lblStatus runat=server /></td>
		</tr>
		<tr>
			<td height=25>Mature Allocation :*</td>
			<td><asp:TextBox id=txtMature OnKeyUp="javascript:calTotalAllocation();" width=20% maxlength=6 Text="0.00" runat=server /> %
				<asp:RequiredFieldValidator id=rfvMature display=dynamic runat=server 
					ErrorMessage="<br>Please enter general charges allocation to mature block. " 
					ControlToValidate=txtMature />			
				<asp:RangeValidator id="rvMature"
					ControlToValidate="txtMature"
					MinimumValue="0"
					MaximumValue="100"
					Type="Double"
					EnableClientScript="true"
					Text="The value must be within 0% - 100%"
					runat="server"/>
			</td>
			<td>&nbsp;</td>
			<td>Date Created :</td>
			<td><asp:Label id=lblDateCreated runat=server /></td>
		</tr>
		<tr>
			<td height=25>Immature Allocation :*</td>
			<td><asp:TextBox id=txtImmature OnKeyUp="javascript:calTotalAllocation();" width=20% maxlength=6 Text="0.00" runat=server /> %
			<asp:RequiredFieldValidator id=rfvImmature display=dynamic runat=server 
					ErrorMessage="<br>Please enter general charges allocation to immature block. " 
					ControlToValidate=txtImmature />			
				<asp:RangeValidator id="rvImmature"
					ControlToValidate="txtImmature"
					MinimumValue="0"
					MaximumValue="100"
					Type="Double"
					EnableClientScript="true"
					Text="The value must be within 0% - 100%"
					runat="server"/>
			</td>
			<td>&nbsp;</td>
			<td height=25>Last Update :</td>
			<td><asp:Label id=lblLastUpdate runat=server /></td>
		</tr>
		<tr>
			<td>
				Total Allocation :
				<asp:label id=lblErrAllocation visible=false forecolor=red text="<br>Combination of mature and immature allocation must be exactly 100%" runat=server/>
			</td>
			<td>
				<span id=totalAllocation name=totalAllocation></span>
			</td>
			<td>&nbsp;</td>
			<td height=25>Updated By :</td>
			<td><asp:Label id=lblUpdateBy runat=server /></td>
		</tr>
		<tr>
			<td colspan=5>&nbsp;</td>
		</tr>
		<tr>
			<td colspan="5">
				<table id=tblSelection width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server class="font9Tahoma">
					<tr class="font9Tahoma">						
						<td>
							<table border="0" cellspacing="0" width="100%" cellpadding="2" class="font9Tahoma">
								<tr>
									<td colspan=2>
										Choose the <asp:Label id=lblLoc1 runat=server/> on which the general charges will be distributed.
	    							</td>
								</tr>
								<tr>
									<td colspan=2>
										<asp:DropDownList id=ddlLocation width=100% runat=server /> 
										<asp:Label id=lblErrLoc visible=false forecolor=red runat=server/>
	    							</td>
								</tr>
								<tr>
									<td colspan=2>
										<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext="Add Location" onclick=btnAdd_Click runat=server />
	    							</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr><td colspan=5>&nbsp;</td></tr>
		<tr>
			<td colspan=5>
				<asp:DataGrid id=dgResult
							BorderColor=black
							BorderWidth=0
							GridLines=both
							CellPadding=1
							CellSpacing=1
							width=100% 
							AutoGenerateColumns=false 
							OnDeleteCommand=DEDR_Delete 
							runat=server CssClass="font9Tahoma">
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	

						<Columns>
							<asp:BoundColumn DataField="ActLocCode" />
							<asp:BoundColumn DataField="Description" />
							<asp:TemplateColumn ItemStyle-HorizontalAlign=Right HeaderStyle-VerticalAlign=Bottom ItemStyle-VerticalAlign=Bottom>
								<ItemTemplate>
									<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
								</ItemTemplate>
							</asp:TemplateColumn>	
						</Columns>
				</asp:DataGrid>
			</td>
		</tr>		
		<tr>
			<td colspan=5>&nbsp;</td>
		</tr>
		<tr>
			<td colspan="5">
				<asp:Label id=lblGCSuccess visible=false forecolor=red text="General changes is successfully distributed.<br>" runat=server/>
				<asp:Label id=lblErrGCFail visible=false forecolor=red text="Distribution process has been terminated because there are some errors when distributing general charges.<br>" runat=server/>
				<asp:Label id=lblErrGCNoAllocation visible=false forecolor=red text="Distribution process has been terminated when distributing general charges. Kindly define the general charges allocation for mature / immature before try again.<br>" runat=server/>
				<asp:Label id=lblErrGCNoLocation visible=false forecolor=red text="Distribution process has been terminated when distributing general charges. Kindly define the location(s) to which the general charges will be distributed.<br>" runat=server/>

				<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" alternatetext=Save onclick=btnSave_Click runat=server/>
				<asp:ImageButton id=btnDistribute imageurl="../../images/butt_distribute.gif" alternatetext=Distribute onclick=btnDistribute_Click runat=server/>
				<asp:ImageButton id=btnBack CausesValidation=False imageurl="../../images/butt_back.gif" alternatetext=Back onclick=btnBack_Click runat=server />
			</td>
		</tr>
		</table>
		<asp:Label id="lblAccPeriod" visible="false" text="" runat="server" />
		<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
		<asp:Label id="lblLocCode" visible="false" text="" runat="server" />
		<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
		<asp:Label id="lblErrSelect" visible="false" text="Please select " runat="server" />
		<asp:Label id="lblHiddenStatus" visible="false" text=0 runat="server" />
        </div>
	</form>    
</body>

</html>
