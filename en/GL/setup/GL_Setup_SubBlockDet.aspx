<%@ Page Language="vb" trace="false" src="../../../include/GL_Setup_SubBlockDet.aspx.vb" Inherits="GL_Setup_SubBlockDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Sub Block Details</title>
               <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="Javascript">
			function checkMill(){
				var doc = document.frmMain;
				var ch = parseFloat(doc.lblCheckMill.Value);
				if (doc.lblCheckMill.value == '1') 
					{				
						calTotalStd();
						calStdPerArea();
					}					
			}
			function calTotalStd() {
				var doc = document.frmMain;
				var ta = parseFloat(doc.txtTotalArea.value);
				var spa = parseFloat(doc.txtStdPerArea.value);
				doc.txtTotalStand.value = ta * spa;
				if (doc.txtTotalStand.value == 'NaN' || doc.txtTotalStand.value == 'Infinity') 
					doc.txtTotalStand.value = '';
				else
					doc.txtTotalStand.value = Math.round(doc.txtTotalStand.value, 0);
			}
			
			function calStdPerArea() {
				var doc = document.frmMain;
				var ta = parseFloat(doc.txtTotalArea.value);
				var ts = parseFloat(doc.txtTotalStand.value);
				doc.txtStdPerArea.value = ts / ta;
				if (doc.txtStdPerArea.value == 'NaN' || doc.txtStdPerArea.value == 'Infinity') 
					doc.txtStdPerArea.value = '';
				else
					doc.txtStdPerArea.value = Math.round(doc.txtStdPerArea.value, 0);
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
	<body onload="javascript:checkMill();">
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
                        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 800px" valign="top">
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
					<td  colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td  class="font9Tahoma">
                                  <strong> <asp:label id="lblTitle" runat="server" /> DETAILS </strong> </td>
                                <td class="font9Header"  style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
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
					<td width=20% height=25><asp:label id ="lblSubBlkCode" runat="server" /> :* </td>
					<td width=30%>
						<asp:Textbox id=txtSubBlkCode width=50% maxlength=8 runat=server CssClass="fontObject"/>
						<input type="hidden" runat="server" id="hidRecStatus">
						<input type="hidden" runat="server" id="hidOriSubBlkCode">
						<asp:RequiredFieldValidator id=rfvSubBlkCode display=Dynamic runat=server 
							ControlToValidate=txtSubBlkCode />
						
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." display=dynamic runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblSubBlkDesc" runat="server" /> :*</td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server CssClass="fontObject"/>
						<asp:RequiredFieldValidator id=rfvSubBlkDesc display=Dynamic runat=server 
							ControlToValidate=txtDescription />	</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblBlock" runat="server" /> :*</td>
					<td><asp:dropdownlist id=ddlBlock width=100% runat=server CssClass="fontObject"/>
						<asp:RequiredFieldValidator id=rfvBlk display=Dynamic runat=server 
							ControlToValidate=ddlBlock />	</td>
					
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblDateOfPlanting" runat="server" /> : </td>
					<td><asp:textbox id=txtPlantDate maxlength=10 width=50% runat=server CssClass="fontObject"/>
						<a href="javascript:PopCal('txtPlantDate');"><asp:Image id="btnSelPlantDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblErrPlantDate text="<br>Please enter date of planting." display=dynamic forecolor=red visible=false runat="server"/>  <!--#PL-RH050725052-->						
						<asp:Label id=lblPlantDate text="<br>Date Entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblPlantDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblSubBlkType" runat="server" /> Type : </td>
					<td><asp:RadioButton id=rbTypeInMatureField text=" Immature Field" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype runat=server /> 
						<asp:RadioButton id=rbTypeMatureField text=" Mature Field" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype checked=true runat=server /> 
						<asp:RadioButton id=rbTypeOff text=" Office" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype runat=server /> 
						<asp:RadioButton id=rbTypeNursery text=" Nursery" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype runat=server /> 
						<asp:RadioButton id=rbTypeMill text=" Mill" OnCheckedChanged=OnTypeChanged AutoPostBack=True groupname=blocktype runat=server /> 
					</td>
					<td>&nbsp;</td>
					<td><asp:label id="lblBlock2" runat="server" /></td>
					<td><asp:dropdownlist id=ddlTransferSubBlk width=100% runat=server CssClass="fontObject"/>
						<asp:Label id=lblErrTransferSubBlk forecolor=red visible=false runat="server"/>					
					</td>
				</tr>				
				<tr id=TRStartArea runat=server >
					<td height=25><asp:label id="lblStartArea" runat="server" /></td>
					<td><asp:textbox id=txtStartArea text=0 maxlength=21 width=50% runat=server CssClass="fontObject"/>
						<asp:CompareValidator id="cvStartArea" display=dynamic runat="server" 
							ControlToValidate="txtStartArea" Text="The value must be whole number or with decimal." 
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
					<td height=25><asp:label id="lblTransDate" runat="server" /></td>
					<td><asp:textbox id=txtTransferDate maxlength=10 width=50% runat=server CssClass="fontObject"/>
						<a href="javascript:PopCal('txtTransferDate');"><asp:Image id="btnSelTransferDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblTransferDate text="<br>Date Entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblTransferDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblErrTransferDate text="Transfer Date must be after Initial Charge Date" forecolor=red visible=false runat="server"/>						
						<asp:Label id=lblErrTransferSubBlkDate text="Please enter Transfer Date" forecolor=red visible=false runat="server"/> 												
					</td>
				</tr>
				<tr id="TRTotalArea" runat="server" >
					<td height=25><asp:label id="lblTotalArea" runat="server" /></td>
					<td><asp:textbox id=txtTotalArea OnKeyUp="javascript:checkMill();" text=0 maxlength=21 width=50% runat=server CssClass="fontObject"/>
						<asp:CompareValidator id="cvTotalArea" display=dynamic runat="server" 
							ControlToValidate="txtTotalArea" Text="The value must be whole number or with decimal." 
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
					<td height=25><asp:label id="lblBunchRatio" runat="server" /></td>
					<td>
						<asp:textbox id=txtBunchRatio text=1 width=50% maxlength=3 runat=server CssClass="fontObject"/>
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
								runat=server/>
					</td>				
				</tr>
				<tr id="TRDailyQuota" valign=top runat="server" >
					<td height=25><asp:label id="lblDailyQuota" runat="server" /></td>
					<td><asp:textbox id=txtQuota text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server CssClass="fontObject"/>
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
				<tr id="TRQuotaInc" valign=top runat="server" >
					<td><asp:label id=lblQuotaInc runat=server /></td>
					<td><asp:textbox id=txtQuotaIncRate text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revQuotaIncRate" 
							ControlToValidate="txtQuotaIncRate"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr id="TREstimatedBJR" valign=top runat="server" >
					<td><asp:label id=lblEstimatedBJR runat=server /></td>
					<td><asp:textbox id=txtEstimatedBJR text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revEstimatedBJR" 
							ControlToValidate="txtEstimatedBJR"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>
					<td colspan=3>&nbsp;</td>				
				</tr>
				<tr id="TRSpacer" valign=top runat="server">
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr id="TRFert" valign=top runat="server">
					<td><b>Fertilizer</b></td>
					<td colspan=4>&nbsp;</td>			
				</tr>
				<tr id="TRFert1" valign=top runat="server">
					<td>Urea :</td>
					<td><asp:textbox id=txtUrea text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revUrea" 
							ControlToValidate="txtUrea"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>
					<td>&nbsp;</td>
					<td>RP :</td>
					<td><asp:textbox id=txtRp text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revRp" 
							ControlToValidate="txtRp"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>			
				</tr>
				<tr id="TRFert2" valign=top runat="server">
					<td>KCL/MOP :</td>
					<td><asp:textbox id=txtKclMop text=0 width=50% maxlength=21 runat=server CssClass="fontObject" />
						<asp:RegularExpressionValidator 
							id="revKclMop" 
							ControlToValidate="txtKclMop"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>
					<td>&nbsp;</td>
					<td>Kieserit :</td>
					<td><asp:textbox id=txtKliserit text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revKliserit" 
							ControlToValidate="txtKliserit"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>			
				</tr>
				<tr id="TRFert3" valign=top runat="server">
					<td>Dolomit :</td>
					<td><asp:textbox id=txtDolomit text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revDolomit" 
							ControlToValidate="txtDolomit"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>
					<td>&nbsp;</td>
					<td>HGFB :</td>
					<td><asp:textbox id=txtHGFB text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revHGFB" 
							ControlToValidate="txtHGFB"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>					
				</tr>
				<tr id="TRFert4" valign=top runat="server">
					<td>Mills Effluence :</td>
					<td><asp:textbox id=txtMillEff text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revMillEff" 
							ControlToValidate="txtMillEff"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>
					<td>&nbsp;</td>
					<td>JJ. Kosong :</td>
					<td><asp:textbox id=txtJJ text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id="revJJ" 
							ControlToValidate="txtJJ"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points"
							runat="server" />	
					</td>				
				</tr>
				<tr valign=top>
					<td height=25><asp:label id="lblStdRunHour" runat="server" /></td>
					<td><asp:textbox id=txtStdRunHour text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server CssClass="fontObject"/>
						<asp:RegularExpressionValidator 
							id=revStdRunHour
							ControlToValidate=txtStdRunHour
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
				</tr>
				
				
				<tr valign=top>
					<td height=25><asp:label id="lblMachineCap" runat="server" /></td>
					<td><asp:textbox id=txtMachineCap text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server CssClass="fontObject" />
						<asp:RegularExpressionValidator 
							id=revMachineCap
							ControlToValidate=txtMachineCap
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
					<td>&nbsp;</td>	
					<td height=25><asp:label id="lblActHourMeter" runat="server" /></td>
					<td><asp:textbox id=txtActHourMeter text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
					</td>
				</tr>
				<tr valign="top">
					<td height="25"><asp:label id="lblProcessCtrl" runat="server" /></td>
					<td>
						<asp:RadioButton id="rbProcessCtrlYes" runat="server" Text="Yes" GroupName="ProcessCtrl"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:RadioButton id="rbProcessCtrlNo" runat="server" Text="No" GroupName="ProcessCtrl" Checked="True"></asp:RadioButton></td>
					<td>&nbsp;</td>	
					<td height=25><asp:label id="lblExpHourMeter" runat="server" /></td>
					<td><asp:textbox id=txtExpHourMeter text=0 width=50% maxlength=21 runat=server CssClass="fontObject"/>
					<asp:RegularExpressionValidator 
							id=revExpHourMeter
							ControlToValidate=txtExpHourMeter
							ValidationExpression="\d{1,7}"
							Display="Dynamic"
							text = "<br>Maximum length 7 digits. "
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td colspan=5 height=25 ><u>Unit of Measurement</u></td>
				</tr>
				<tr>
					<td colspan=5 width=100%>
						<table width=100% border=0 class=mb-c cellspacing=0 cellpadding=4  class="font9Tahoma">
							<tr>
								<td>
									<table border=0 width=100% cellspacing=0 cellpadding=2  class="font9Tahoma">			
										<tr>
											<td width=20% height=25><asp:label id="lblArea" runat="server" /> :*</td>
											<td width=30%>
												<asp:dropdownlist id=ddlAreaUOM width=100% runat=server CssClass="fontObject"/>
												<asp:RequiredFieldValidator id=rfvAreaUOM display=Dynamic runat=server ControlToValidate=ddlAreaUOM />
											</td>
											<td width=5%>&nbsp;</td>
											<td width=20%><asp:label id="lblYield" runat="server" /> :*</td>
											<td width=25%>
												<asp:dropdownlist id=ddlYieldUOM width=100% runat=server CssClass="fontObject"/>
												<asp:RequiredFieldValidator id=rfvYieldUOM display=Dynamic runat=server ControlToValidate=ddlYieldUOM />
											</td>
										</tr>
										<tr>
											<td width=20% height=25><asp:label id="lblAreaAvg" runat="server" /> :*</td>
											<td width=30%>
												<asp:dropdownlist id=ddlAreaAvgUOM width=100% runat=server CssClass="fontObject"/>
												<asp:RequiredFieldValidator id=rfvAreaAvgUOM display=Dynamic runat=server ControlToValidate=ddlAreaAvgUOM />
											</td>
											<td width=5%>&nbsp;</td>
											<td width=20%><asp:label id="lblYieldAvg" runat="server" /> :*</td>
											<td width=25%>
												<asp:dropdownlist id=ddlYieldAvgUOM width=100% runat=server CssClass="fontObject"/>
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
					<td height=25 colspan=5>&nbsp;</td>
				</tr>
				<tr id="TRMaterialPlant" runat="server">
					<td height=25><asp:label id="lblMaterialPlant" runat="server" /></td>
					<td><asp:textbox id=txtPlantMaterial maxlength=8 width=50% runat=server CssClass="fontObject"/></td>
					<td>&nbsp;</td>
					<td><asp:label id="lblHarvestStartDate" runat="server" /></td>
					<td><asp:textbox id=txtHarvestStartDate maxlength=10 width=50% runat=server CssClass="fontObject"/>
						<a href="javascript:PopCal('txtHarvestStartDate');"><asp:Image id="btnSelHarvStartDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblHarvStartDate text="<br>Date Entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblHarvStartDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="TRStdPerArea" runat="server" >
					<td height=25><asp:label id="lblStdPerArea" runat="server" /></td>
					<td><asp:textbox id=txtStdPerArea OnKeyUp="javascript:checkMill();" maxlength=21 width=50% runat=server CssClass="fontObject"/>
						<asp:CompareValidator id="cvStdPerArea" display=dynamic runat="server" 
							ControlToValidate="txtStdPerArea" 
							Text="The value must be whole number or with decimal." 
							Type="Double" Operator="DataTypeCheck"/>
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
					<td><asp:textbox id=txtInitialDate maxlength=10 width=50% runat=server CssClass="fontObject"/>
						<a href="javascript:PopCal('txtInitialDate');"><asp:Image id="btnSelInitialDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:Label id=lblInitialDate text="<br>Date Entered should be in the format " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblInitialDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="TRTotalStand" runat="server" >
					<td height=25><asp:label id="lblTotalStand" runat="server" /></td>
					<td><asp:textbox id=txtTotalStand OnKeyUp="javascript:checkMill();" maxlength=21 width=50% runat=server CssClass="fontObject"/>
						<asp:CompareValidator id="cvTtlStand" display=dynamic runat="server" 
							ControlToValidate="txtTotalStand" Text="The value must be whole number or with decimal." 
							Type="Double" Operator="DataTypeCheck"/>
					</td>
					<td colspan=4>&nbsp;</td>
				</tr>
				<tr id="TRGroupType" runat="server" >
					<td height=25><asp:label id="lblGroupType" runat="server" /></td>
					<td><asp:RadioButton id=rbPlantedArea text=" Areal Diusahakan" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype checked=true runat=server /><br> 
						<asp:RadioButton id=rbUnplantedArea text=" Areal Tidak Ditanam" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype runat=server /><br> 
						<asp:RadioButton id=rbLandClearing text=" Land Clearing" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype runat=server /><br> 
						<asp:RadioButton id=rbExtension text=" Areal Mungkin Bisa Ditanam" OnCheckedChanged=OnGrpTypeChanged AutoPostBack=True groupname=grptype runat=server /> 
					</td>
					<td>&nbsp;</td>
					<td><asp:label id="lblSubBlkMgrRpt" runat="server" /></td>
					<td>
						<asp:dropdownlist id=ddlBlkTypeInRpt width=100% runat=server CssClass="fontObject"/>
						<asp:RequiredFieldValidator id=rfvBlkTypeInRpt display=Dynamic ErrorMessage='Please select Block Type in Managerial Report.' runat=server ControlToValidate=ddlBlkTypeInRpt />
					</td>
				</tr>
				<tr>
					<td colspan=5 width=100%>
					<table width=100% border=0 class="mb-c" cellspacing=0 cellpadding=4>
							<tr>
								<td>
									<table border=0 width=100% cellspacing=0 cellpadding=2  class="font9Tahoma">			
										<tr>
											<td width=20% height=25><asp:label id="lblkomoditi" Text = "Komoditi" runat="server" /> :</td>
											<td width=30%>
												<asp:dropdownlist id=ddl1 width=100% runat=server CssClass="fontObject">
												<asp:ListItem Value="" Text="-"></asp:ListItem>
												<asp:ListItem Value="SWT" Text="Sawit" Selected></asp:ListItem>
												<asp:ListItem Value="KRT" Text="Karet"></asp:ListItem>
												<asp:ListItem Value="JTI" Text="Jati/Jabon"></asp:ListItem>
												</asp:dropdownlist>
											</td>
											<td width=5%>&nbsp;</td>
											<td width=20%><asp:label id="lblbibit" Text = "Jenis Bibit" runat="server" /> :</td>
											<td width=25%><asp:textbox id=ddl2 width=50%  runat=server CssClass="fontObject"/></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height=25 colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					&nbsp;</td>
				</tr>
				<tr id="TRHistoriBlk" runat="server">
					<td colspan=5 width=100%>
						<table width=100% border=0 cellspacing=0 cellpadding=4>
							<tr>
								<td>
									<table border=0 width=100% cellspacing=0 cellpadding=2>			
										<tr>
											<td width=20% ><asp:label  id="lblhis" Text = "Histori Blok" runat="server" /> :</td>
											<td width=30% ><asp:label  id="lblprd" Text = "Periode" runat="server" /> :	
											<asp:textbox id=txtprd width=25% maxlength=4 runat=server CssClass="fontObject"/>&nbsp;
											<asp:Button text="Search" id=SrcBtn onclick=SrcBtnKlik runat=server />&nbsp;
											</td>
											<td width=5%>
											</td>
											<td width=20%></td>
											<td width=25%>
											<asp:Button text="Copy dari Periode :" id=CopyBtn onclick=CopyBtnKlik runat=server />&nbsp;
											<asp:textbox id=txtprd2 width=20% maxlength=4 runat=server CssClass="fontObject"/>
											</td>
										</tr>
										<tr>
											<td colspan="5">
											<asp:DataGrid id=dghist
											AutoGenerateColumns=false width="100%" runat=server
											GridLines=none
											Cellpadding=2
											Pagerstyle-Visible=False
											AllowSorting="True"   
											OnEditCommand="dghist_Edit"
											OnUpdateCommand="dghist_Update"
											OnCancelCommand="dghist_Cancel" >
											<HeaderStyle CssClass="mr-h"/>
											<ItemStyle CssClass="mr-l"/>
											<AlternatingItemStyle CssClass="mr-r"/>
               	
											<Columns>						
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Blok">
											<ItemTemplate>
											<%# Container.DataItem("SubBlkCode") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id1" runat="server"  CssClass="fontObject" MaxLength="8" Enabled=false Text='<%# trim(Container.DataItem("SubBlkCode")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
								
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Bulan">
											<ItemTemplate>
											<%# Container.DataItem("AccMonth") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id2" runat="server" CssClass="fontObject"  MaxLength="2" Enabled=false Text='<%# trim(Container.DataItem("AccMonth")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>

											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Tahun">
											<ItemTemplate>
											<%# Container.DataItem("AccYear") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id3" runat="server" CssClass="fontObject" MaxLength="4" Enabled=false Text='<%# trim(Container.DataItem("AccYear")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Luas(HA)">
											<ItemTemplate>
											<%# Container.DataItem("TotalArea") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id4" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("TotalArea")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Tot.Pokok">
											<ItemTemplate>
											<%# Container.DataItem("TotalStand") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id5" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("TotalStand")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Pokok.Prod">
											<ItemTemplate>
											<%# Container.DataItem("ProdStand") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id5b" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("ProdStand")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Pokok/HA">
											<ItemTemplate>
											    <%# Container.DataItem("StdPerArea") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id6" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("StdPerArea")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="30%" HeaderText="Blok-Transfer">
											<ItemTemplate>
											    <%# Container.DataItem("TransferBlok") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:DropDownList ID="id7ddl" CssClass="fontObject" runat="server"  Width="100%"/>
								            <asp:label ID="id7" runat="server" Text='<%# trim(Container.DataItem("TransferBlok")) %>' visible=false Width="100%" />
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Bangunan">
											<ItemTemplate>
											    <%# Container.DataItem("ANT1") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="idT1" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("ANT1")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											<ItemStyle BackColor="Lavender" />
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Jln/Jembatan">
											<ItemTemplate>
											    <%# Container.DataItem("ANT2") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="idT2" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("ANT2")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											<ItemStyle BackColor="Lavender" />
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Parit/Rawa">
											<ItemTemplate>
											    <%# Container.DataItem("ANT3") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="idT3" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("ANT3")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											<ItemStyle BackColor="Lavender" />
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Enclave">
											<ItemTemplate>
											    <%# Container.DataItem("ANT4") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="idT4" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("ANT4")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											<ItemStyle BackColor="Lavender" />
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Lainnya">
											<ItemTemplate>
											    <%# Container.DataItem("ANT5") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="idT5" CssClass="fontObject" runat="server" Text='<%# trim(Container.DataItem("ANT5")) %>'
											Width="100%"></asp:TextBox>
											</EditItemTemplate>
											<ItemStyle BackColor="Lavender" />
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="SubType">
											<ItemTemplate>
											    <%# Container.DataItem("subtype") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:DropDownList ID="ddlapl" width="100%" runat="server" CssClass="fontObject">
											<asp:ListItem Value="" Text=""></asp:ListItem>
											<asp:ListItem Value="4310" Text="Nursery"></asp:ListItem>
											<asp:ListItem Value="2210" Text="LC New Planting"></asp:ListItem>
											<asp:ListItem Value="2211" Text="LC Re-Planting"></asp:ListItem>
											<asp:ListItem Value="2120" Text="TBM 0"></asp:ListItem>
											<asp:ListItem Value="2121" Text="TBM 1"></asp:ListItem>
											<asp:ListItem Value="2122" Text="TBM 2"></asp:ListItem>
											<asp:ListItem Value="2123" Text="TBM 3"></asp:ListItem>
											<asp:ListItem Value="1110" Text="TM"></asp:ListItem>
											<asp:ListItem Value="3330" Text="Non Tanam"></asp:ListItem>											
											</asp:DropDownList>		
											<asp:label ID="lblapl" runat="server" Text='<%# trim(Container.DataItem("subtype")) %>' visible=false Width="100%" />
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="5%" HeaderText="Active">
											<ItemTemplate>
												<%# Container.DataItem("Active") %>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:TextBox ID="id8" CssClass="fontObject" runat="server" MaxLength="2" Enabled=false Text='<%# trim(Container.DataItem("Active")) %>'
											Visible="False" Width="10%"></asp:TextBox>
											</EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="Tgl.Update">
											<ItemTemplate>
												<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
											</ItemTemplate>
											<EditItemTemplate>
                                            <asp:TextBox ID="id9"  CssClass="fontObject" runat="server" ReadOnly="TRUE" size="8" Text='<%# objGlobal.GetLongDate(Now()) %>'
												Visible="False"></asp:TextBox>
                                            </EditItemTemplate>
											</asp:TemplateColumn>
											
											<asp:TemplateColumn ItemStyle-Width="10%" HeaderText="DiUpdate">
											<ItemTemplate>
												<%# Container.DataItem("UpdateId") %>
											</ItemTemplate>
											
											<EditItemTemplate>
												<asp:TextBox ID="id10"  CssClass="fontObject" runat="server" ReadOnly="TRUE" size="8" Text='<%# Session("SS_USERID") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id11" CssClass="fontObject" runat="server" size="8" Text='<%# Container.DataItem("bjr") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id12" CssClass="fontObject" runat="server" size="8" Text='<%# Container.DataItem("basis") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id13" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("ovrbasis") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id14" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("ovrbasis2") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id15" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("Prod1Kg") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id16" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("Prod1Rp") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id17" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("Prod2Kg") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id18" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("Prod2Rp") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id19" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("PokokRp") %>' Visible="False"></asp:TextBox>
												<asp:TextBox ID="id20" CssClass="fontObject" runat="server"  size="8" Text='<%# Container.DataItem("BasisHA") %>' Visible="False"></asp:TextBox>
											
											</EditItemTemplate>
											</asp:TemplateColumn>
										
											<asp:TemplateColumn ItemStyle-Width=12%>
											<ItemTemplate >
											<asp:LinkButton ID="Edit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
											</ItemTemplate>
											<EditItemTemplate>
											<asp:LinkButton ID="Update" runat="server" CommandName="Update" Text="Save"></asp:LinkButton>
											<asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"  Text="Cancel"></asp:LinkButton>
											</EditItemTemplate>
											</asp:TemplateColumn>	
											</Columns>
											</asp:DataGrid>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="5">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<td width=20% height=25><asp:label id="lblAccGrp" runat="server" /> :*</TD>
											<td width=30%><asp:DropDownList id=ddlAccGrp width=100% runat=server CssClass="fontObject" /></TD>
											<td width=5% align=center> OR </td>
											<td width=15%><asp:label id="lblAccount" runat="server" /> :*</td>
											<td width=30%><asp:DropDownList id=ddlAccount width=100% runat=server CssClass="fontObject"/></TD>
										</TR>
										<TR class="mb-c">
											<TD height=25 colspan=6>
												<asp:Label id=lblErrSelectBoth visible=false forecolor=red text="<br>Please select either Account Group Code OR Account Code only." display=dynamic runat=server/>
												<asp:Label id=lblErrNotSelect visible=false forecolor=red text="<br>Please select Account Group Code OR Account Code." display=dynamic runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD colspan=6 height=25>
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
							AllowSorting="True" >
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
            
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

								<asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
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
				
				<Input Type=Hidden id=tbcode runat=server />
				<Input Type=Hidden id=lblCheckMill runat=server />
				<asp:Label id=lblHidCostLevel visible=false runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblErrLicSize visible=false forecolor=red text="<br>Access denied for hectarage license." runat=server/>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
