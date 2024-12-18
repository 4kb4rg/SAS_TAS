<%@ Page Language="vb" trace=false src="../../../include/GL_Setup_BlockDet.aspx.vb" Inherits="GL_Setup_BlockDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Block Details</title>
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="Javascript">
			function calTotalStd() {
				var doc = document.frmMain;
				var ta = parseFloat(doc.txtTotalArea.value);
				var spa = parseFloat(doc.txtStdPerArea.value);
				doc.txtTotalStand.value = ta * spa;
				if (doc.txtTotalStand.value == 'NaN' || doc.txtTotalStand.value == 'Infinity') 
					doc.txtTotalStand.value = '';
				else
					doc.txtTotalStand.value = Math.round(doc.txtTotalStand.value);
			}
			
			function calStdPerArea() {
				var doc = document.frmMain;
				var ta = parseFloat(doc.txtTotalArea.value);
				var ts = parseFloat(doc.txtTotalStand.value);
				doc.txtStdPerArea.value = ts / ta;
				if (doc.txtStdPerArea.value == 'NaN' || doc.txtStdPerArea.value == 'Infinity') 
					doc.txtStdPerArea.value = '';
				else
					doc.txtStdPerArea.value = round(doc.txtStdPerArea.value, 5);
			}
			
			function calQuotaInc() {
				var doc = document.frmMain;
				var quota = parseFloat(doc.txtQuota.value);
				if (quota == 0)
					doc.txtQuotaIncRate.value = 0;
			}
			
		</script>			
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"   runat="server">
                      <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:Label id=lblPleaseEnter visible=false text="Please enter " runat=server />
			<asp:Label id=lblPlsSelectUOM visible=false text="Please select UOM for " runat=server />
			<asp:Label id=lblPlsSelectEither visible=false text="Please select either " runat=server />
			<asp:Label id=lblOnly visible=false text=" only." runat=server />
			<asp:Label id=lblOr visible=false text=" OR " runat=server />
			<asp:Label id=lblPlsSelect visible=false text="Please select " runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong> <asp:label id="lblTitle" runat="server" /> DETAILS </strong> </td>
                                <td class="font9Header"  style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />&nbsp;| Created at <asp:Label id=lblCreateLoc runat=server/> : <asp:Label id=lblCreateLocCode runat=server />
                                </td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblBlkCode" runat="server" /> :* </td>
					<td width=30%>
						<asp:Textbox id=txtBlkCode width=50% maxlength=8 runat=server  CssClass="font9Tahoma"/>
						<input type="hidden" runat="server" id="hidRecStatus">
						<input type="hidden" runat="server" id="hidOriBlkCode">
						<asp:RequiredFieldValidator id=rfvBlkCode display=Dynamic runat=server
							ControlToValidate=txtBlkCode />
						<!--<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtBlkCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." display=dynamic runat=server/>-->
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblBlkDesc" runat="server" /> :*</td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server  CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=rfvBlkDesc display=Dynamic runat=server 
							ControlToValidate=txtDescription /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblBlkGrp" runat="server" /> :*</td>
					<td><asp:dropdownlist id=ddlBlkGrp width=100% runat=server  CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=rfvBlkGrp display=Dynamic runat=server ControlToValidate=ddlBlkGrp />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblDateOfPlanting" runat="server" /> : </td>
					<td><asp:textbox id=txtPlantDate maxlength=10 width=50% runat=server  CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtPlantDate');"><asp:Image id="btnSelPlantDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrPlantDate text="<br>Please enter date of planting." display=dynamic forecolor=red visible=false runat="server"/>
						<asp:Label id=lblPlantDate text="<br>Date entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblPlantDateFmt display=dynamic forecolor=red visible=false runat="server"/> </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblBlkType" runat="server" /> Type : </td>
					<td><asp:RadioButton id=rbTypeInMatureField text=" Immature Field" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype runat=server /> 
						<asp:RadioButton id=rbTypeMatureField text=" Mature Field" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype checked=true runat=server /> 
						<asp:RadioButton id=rbTypeOff text=" Office" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype runat=server /> 
						<asp:RadioButton id=rbTypeNursery text=" Nursery" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype runat=server /> 
						<asp:RadioButton id=rbTypeMill text=" Mill" OnCheckedChanged=OnTypeChanged AutoPostBack=True visible=false groupname=blocktype runat=server /> 
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="TRStartArea" runat=server >
					<td height=25><asp:label id="lblStartArea" runat="server" /></td>
					<td><asp:textbox id=txtStartArea text=0 maxlength=21 width=50% runat=server  CssClass="font9Tahoma"/>
						<asp:CompareValidator id="cvStartArea" display=dynamic runat="server" 
							ControlToValidate="txtStartArea" 
							Text="The value must be whole number or with decimal." 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator 
							id="revStartArea" 
							ControlToValidate="txtStartArea"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<BR>Maximum length 15 digits and 5 decimal points"
							runat="server" />														
					</td>
					<td>&nbsp;</td>
					<td><asp:label id="lblBlock" runat="server" /></td>
					<td><asp:dropdownlist id=ddlTransferBlk width=100% runat=server  CssClass="font9Tahoma"/>
						<asp:Label id=lblErrTransferBlk forecolor=red visible=false runat="server"/> 												
					</td>				
				</tr>
				<tr id="TRTotalArea" runat=server>
					<td height=25><asp:label id="lblTotalArea" runat="server" /></td>
					<td><asp:textbox id=txtTotalArea OnKeyUp="javascript:calStdPerArea();" text=0 maxlength=21 width=50% runat=server  CssClass="font9Tahoma"/>
						<asp:CompareValidator id="cvTotalArea" display=dynamic runat="server" 
							ControlToValidate="txtTotalArea" 
							Text="The value must be whole number or with decimal." 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator 
							id="revTotalArea" 
							ControlToValidate="txtTotalArea"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<BR>Maximum length 15 digits and 5 decimal points"
							runat="server" />																					
						<asp:Label id=lblErrTotalSize ForeColor=Red Visible=False RunAt=Server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:label id="lblTransDate" runat="server" /></td>
					<td><asp:textbox id=txtTransferDate maxlength=10 width=50% runat=server  CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtTransferDate');"><asp:Image id="btnSelTransferDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblTransferDate text="<br>Date entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblTransferDateFmt display=dynamic forecolor=red visible=false runat="server"/>
						<asp:Label id=lblErrTransferDate text="Transfer Date must be after Initial Charge Date" forecolor=red visible=false runat="server"/>
						<asp:Label id=lblErrTransferBlkDate text="Please enter Transfer Date" forecolor=red visible=false runat="server"/> 												
					</td> 				
				</tr>
				<tr id="TRBunchRatio" runat=server valign=top>
					<td height=25><asp:label id="lblBunchRatio" runat="server" /></td>
					<td><asp:textbox id=txtBunchRatio text=1 width=50% maxlength=3 runat=server  CssClass="font9Tahoma"/>
						<asp:RegularExpressionValidator 
								id=revBunchRatio
								ControlToValidate=txtBunchRatio
								ValidationExpression="\d{1,3}\.\d{1,2}|\d{1,3}"
								Display="Dynamic"
								text = "<br>Maximum length 2 digits and 1 decimal points. "
								runat="server"/>
						<asp:RangeValidator
								id=rgBunchRatio
								display=dynamic
								text="<br>The value must be in between 0.1 and 9.9."
								controltovalidate=txtBunchRatio
								minimumvalue=0.1
								maximumvalue=9.9
								type=double
								runat=server/></td>	
					<td>&nbsp;</td>	
					<td><asp:label id=lblQuotaInc runat=server /></td>
					<td><asp:textbox id=txtQuotaIncRate text=0 width=50% maxlength=21 runat=server  CssClass="font9Tahoma"/>
						<asp:RegularExpressionValidator 
							id="revQuotaIncRate" 
							ControlToValidate="txtQuotaIncRate"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>
				</tr>
				<tr id="TRDailyQuota" runat=server valign=top >
					<td height=25><asp:label id="lblDailyQuota" runat="server" /></td>
					<td><asp:textbox id=txtQuota text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server  CssClass="font9Tahoma"/>
						<asp:RegularExpressionValidator 
							id=revQuota
							ControlToValidate=txtQuota
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
					<td>&nbsp;</td>	
					<td height=25><asp:label id="lblMeasuredBy" runat="server" /></td>
					<td><asp:RadioButton id=rbByHour text=" Hour" groupname=QuotaMethod checked=true runat=server /> 
						<asp:RadioButton id=rbByVolume text=" Volume" groupname=QuotaMethod runat=server /> 
					</td>
				</tr>

				<tr valign=top>
					<td height=25><asp:label visible="false" id="lblStdRunHour" runat="server" /></td>
					<td><asp:textbox visible="false" id=txtStdRunHour text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server  CssClass="font9Tahoma"/>
						<asp:RegularExpressionValidator visible="false" 
							id=revStdRunHour
							ControlToValidate=txtStdRunHour
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
				</tr>
				
				<tr valign=top >
					<td height=25><asp:label id="lblStationCap" visible="false" runat="server" /></td>
					<td><asp:textbox visible="false" id=txtStationCap text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server  CssClass="font9Tahoma"/>
						<asp:RegularExpressionValidator visible="false" 
							id=revStationCap
							ControlToValidate=txtStationCap
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
				</tr>
				<tr valign="top">
					<td height="25"><asp:label id="lblProcessCtrl" runat="server" /></td>
					<td colspan="4">
						<asp:RadioButton id="rbProcessCtrlYes" runat="server" Text="Yes" GroupName="ProcessCtrl"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:RadioButton id="rbProcessCtrlNo" runat="server" Text="No" GroupName="ProcessCtrl" Checked="True"></asp:RadioButton></td>
				</tr>				
				<tr>
					<td height=25 colspan=5><u>Unit of Measurement</u></td>
				</tr>
				<tr>
					<td colspan=5 width=100%>
						<table width=100% border=0 class=mb-c cellspacing=0 cellpadding=4>
						<tr>
							<td>
								<table border=0 width=100% cellspacing=0 cellpadding=2>			
								<tr>
									<td width=20% height=25><asp:label id="lblArea" runat="server" /> :*</td>
									<td width=30%>
										<asp:dropdownlist id=ddlAreaUOM width=100% runat=server  CssClass="font9Tahoma"/>
										<asp:RequiredFieldValidator id=rfvAreaUOM display=Dynamic runat=server ControlToValidate=ddlAreaUOM />
									</td>
									<td width=5%>&nbsp;</td>
									<td width=20%><asp:label id="lblYield" runat="server" /> :*</td>
									<td width=25%>
										<asp:dropdownlist id=ddlYieldUOM width=100% runat=server  CssClass="font9Tahoma" />
										<asp:RequiredFieldValidator id=rfvYieldUOM display=Dynamic runat=server ControlToValidate=ddlYieldUOM />
									</td>
								</tr>
								<tr>
									<td height=25 width=20%><asp:label id="lblAreaAvg" runat="server" /> :*</td>
									<td width=30%>
										<asp:dropdownlist id=ddlAreaAvgUOM width=100% runat=server  CssClass="font9Tahoma"/>
										<asp:RequiredFieldValidator id=rfvAreaAvgUOM display=Dynamic runat=server ControlToValidate=ddlAreaAvgUOM />
									</td>
									<td width=5%>&nbsp;</td>
									<td width=20%><asp:label id="lblYieldAvg" runat="server" /> :*</td>
									<td width=25%>
										<asp:dropdownlist id=ddlYieldAvgUOM width=100% runat=server  CssClass="font9Tahoma"/>
										<asp:RequiredFieldValidator id=rfvYieldAvgUOM display=Dynamic runat=server ControlToValidate=ddlYieldAvgUOM />
									</td>
								</tr>
								</table>
							</td>
						</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr id="TRMaterialPlant" runat=server >
					<td height=25><asp:label id="lblMaterialPlant" runat="server" /></td>
					<td><asp:textbox id=txtPlantMaterial maxlength=8 width=50% runat=server  CssClass="font9Tahoma"/></td>
					<td>&nbsp;</td>
					<td><asp:label id="lblHarvestStartDate" runat="server" /></td>
					<td><asp:textbox id=txtHarvestStartDate maxlength=10 width=50% runat=server  CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtHarvestStartDate');"><asp:Image id="btnSelHarvStartDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblHarvStartDate text="<br>Date entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblHarvStartDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
				</tr>
				<tr id="TRStdPerArea" runat=server > 
					<td height=25><asp:label id="lblStdPerArea" runat="server" /></td>
					<td><asp:textbox id=txtStdPerArea OnKeyUp="javascript:calTotalStd();" maxlength=21 width=50% runat=server  CssClass="font9Tahoma"/>
						<asp:CompareValidator id="cvStdPerArea" display=dynamic runat="server" 
							ControlToValidate="txtStdPerArea" 
							Text="The value must be whole number or with decimal." 
							Type="Double" Operator="DataTypeCheck" />
						<asp:RegularExpressionValidator 
							id="revStdPerArea" 
							ControlToValidate="txtStdPerArea"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<BR>Maximum length 15 digits and 5 decimal points"
							runat="server" />							
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:label id="lblinitialchgdate" runat="server" /></td>
					<td><asp:textbox id=txtInitialDate maxlength=10 width=50% runat=server  CssClass="font9Tahoma"/>
						<a href="javascript:PopCal('txtInitialDate');"><asp:Image id="btnSelInitialDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblInitialDate text="<br>Date entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblInitialDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
				</tr>
				<tr id="TRTotalStand" runat=server>
					<td height=25><asp:label id="lblTotalStand" runat="server" /></td>
					<td><asp:textbox id=txtTotalStand OnKeyUp="javascript:calStdPerArea();" maxlength=21 width=50% runat=server  CssClass="font9Tahoma"/>
						<asp:CompareValidator id="cvTtlStand" display=dynamic runat="server" 
							ControlToValidate="txtTotalStand" 
							Text="The value must be whole number." 
							Type="integer" Operator="DataTypeCheck" />
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				
				<tr id="TRGroupType" runat=server>
					<td height=25><asp:label id="lblGroupType" runat="server" /></td>
					<td><asp:RadioButton id=rbPlantedArea text=" Areal Diusahakan" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype checked=true runat=server /><br> 
						<asp:RadioButton id=rbUnplantedArea text=" Areal Tidak Ditanam" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype runat=server /><br> 
						<asp:RadioButton id=rbLandClearing text=" Land Clearing" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype runat=server /><br> 
						<asp:RadioButton id=rbExtension text=" Areal Mungkin Bisa Ditanam" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype runat=server /> 
					</td>
					<td>&nbsp;</td>
					<td><asp:label id="lblBlkMgrRpt" runat="server" /></td>
					<td>
						<asp:dropdownlist id=ddlBlkTypeInRpt width=100% runat=server  CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=rfvBlkTypeInRpt display=Dynamic ErrorMessage='Please select Block Type in Managerial Report.' runat=server ControlToValidate=ddlBlkTypeInRpt />
					</td>
				</tr>
				
				<tr>
					<td colspan="5">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD height=25 width=20%><asp:label id="lblAccGrp" runat="server" /> :*</TD>
											<TD width=30%><asp:DropDownList id=ddlAccGrp width=100% runat=server  CssClass="font9Tahoma"/></TD>
											<TD width=5% align=center> OR </td>
											<TD width=15%><asp:label id="lblAccount" runat="server" /> :*</td>
											<TD width=30%><asp:DropDownList id=ddlAccount width=100% runat=server  CssClass="font9Tahoma"/></TD>
										</TR>
										<TR class="mb-c">
											<TD height=25 colspan=5>
												<asp:Label id=lblErrSelectBoth visible=false forecolor=red display=dynamic runat=server/>
												<asp:Label id=lblErrNotSelect visible=false forecolor=red display=dynamic runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD colspan=5 height=25>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
											&nbsp;</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				
				<tr>
					<td colspan="5">
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_Delete 
							Pagerstyle-Visible=False
							AllowSorting="True" class="font9Tahoma">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
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
								<asp:TemplateColumn ItemStyle-Width="30%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="60%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Center>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
								
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>			
				<tr>
					<td><asp:Label id=lblLocType display=dynamic forecolor=red visible=false runat="server"/></td> 
				</tr>	
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<asp:Label id=lblErrLicSize visible=false forecolor=red text="<br>Access denied for hectarage license." runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblHidCostLevel visible=false runat=server />
				<Input Type=Hidden id=tbcode runat=server />
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
