<%@ Page Language="vb" trace=false Src="../../../include/WM_trx_FFBAssessDet.aspx.vb" Inherits="WM_FFBAssess_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="../../menu/menu_WMtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Weighing Management - FFB Assessment Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="Javascript">
			function calcGradedBunch() {
				var doc = document.frmMain;
				var varripe = parseFloat(doc.txtRipeBunches.value);
				var varover = parseFloat(doc.txtOverRipeBunches.value);
				var varunder = parseFloat(doc.txtUnderRipeBunches.value);
				var varunripe = parseFloat(doc.txtUnripeBunches.value);
				var varempty = parseFloat(doc.txtEmptyBunches.value);
				var varrotten = parseFloat(doc.txtRottenBunches.value);
				var varpoor = parseFloat(doc.txtPoorBunches.value);
				var varsmall = parseFloat(doc.txtSmallBunches.value);
				var varlongstalk = parseFloat(doc.txtLongStalkBunches.value);
				
				var varcontamination = parseFloat(doc.txtContamination.value);
				var varothers = parseFloat(doc.txtOthers.value);
				
				var vargrabunch = varripe + varover + varunder + varunripe + varempty + varrotten + varpoor + varsmall + varlongstalk + varcontamination + varothers;
								
				doc.hidGradedBunch.value = varripe + varover + varunder + varunripe + varempty + varrotten + varpoor + varsmall + varlongstalk + varcontamination + varothers;
				doc.hidRipe.value = varripe / vargrabunch * 100;
				doc.hidOverripe.value = varover / vargrabunch * 100;
				doc.hidUnderripe.value = varunder / vargrabunch * 100;
				doc.hidUnripe.value = varunripe / vargrabunch * 100;
				doc.hidEmpty.value = varempty / vargrabunch * 100;
				doc.hidRotten.value = varrotten / vargrabunch * 100;
				doc.hidPoor.value = varpoor / vargrabunch * 100;
				doc.hidSmall.value = varsmall / vargrabunch * 100;
				doc.hidLongStalk.value = varlongstalk / vargrabunch * 100;
			
				doc.hidContamination.value = varcontamination / vargrabunch * 100;
				doc.hidOthers.value = varothers / vargrabunch * 100;
				
				//calculate bunches graded
				if (doc.hidGradedBunch.value == 'NaN') 
					doc.hidGradedBunch.value = '';
				document.getElementById(bg.id).innerHTML = doc.hidGradedBunch.value;								
				
				//calculate ripe bunches
				if (doc.hidRipe.value == 'NaN') 
					doc.hidRipe.value = '';
				else
					doc.hidRipe.value = round(doc.hidRipe.value, 2);
				document.getElementById(ripe.id).innerHTML = doc.hidRipe.value;																
				
				//calculate overripe bunches
				if (doc.hidOverripe.value == 'NaN') 
					doc.hidOverripe.value = '';
				else
					doc.hidOverripe.value = round(doc.hidOverripe.value, 2);
				document.getElementById(overripe.id).innerHTML = doc.hidOverripe.value;																

				//calculate underripe bunches
				if (doc.hidUnderripe.value == 'NaN') 
					doc.hidUnderripe.value = '';
				else
					doc.hidUnderripe.value = round(doc.hidUnderripe.value, 2);
				document.getElementById(underripe.id).innerHTML = doc.hidUnderripe.value;																

				//calculate unripe bunches
				if (doc.hidUnripe.value == 'NaN') 
					doc.hidUnripe.value = '';
				else
					doc.hidUnripe.value = round(doc.hidUnripe.value, 2);
				document.getElementById(unripe.id).innerHTML = doc.hidUnripe.value;																

				//calculate empty bunches
				if (doc.hidEmpty.value == 'NaN') 
					doc.hidEmpty.value = '';
				else
					doc.hidEmpty.value = round(doc.hidEmpty.value, 2);
				document.getElementById(empty.id).innerHTML = doc.hidEmpty.value;																

				//calculate rotten bunches
				if (doc.hidRotten.value == 'NaN') 
					doc.hidRotten.value = '';
				else
					doc.hidRotten.value = round(doc.hidRotten.value, 2);
				document.getElementById(rotten.id).innerHTML = doc.hidRotten.value;																

				//calculate poor bunches
				if (doc.hidPoor.value == 'NaN') 
					doc.hidPoor.value = '';
				else
					doc.hidPoor.value = round(doc.hidPoor.value, 2);
				document.getElementById(poor.id).innerHTML = doc.hidPoor.value;																

				//calculate small bunches
				if (doc.hidSmall.value == 'NaN') 
					doc.hidSmall.value = '';
				else
					doc.hidSmall.value = round(doc.hidSmall.value, 2);
				document.getElementById(small.id).innerHTML = doc.hidSmall.value;																

				//calculate longstalk bunches
				if (doc.hidLongStalk.value == 'NaN') 
					doc.hidLongStalk.value = '';
				else
					doc.hidLongStalk.value = round(doc.hidLongStalk.value, 2);
				document.getElementById(longstalk.id).innerHTML = doc.hidLongStalk.value;	
				
				//calculate Contamination - SMN
				if (doc.hidContamination.value == 'NaN') 
					doc.hidContamination.value = '';
				else
					doc.hidContamination.value = round(doc.hidContamination.value, 2);
				document.getElementById(contamination.id).innerHTML = doc.hidContamination.value;	
																			
				//calculate Others - SMN
				if (doc.hidOthers.value == 'NaN') 
					doc.hidOthers.value = '';
				else
					doc.hidOthers.value = round(doc.hidOthers.value, 2);
				document.getElementById(others.id).innerHTML = doc.hidOthers.value;	
				
				
			}
			
		</script>		
	</head>
	<body onload="javascript:calcGradedBunch();">	
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<form id="frmMain" runat="server"  class="main-modul-bg-app-list-pu">

            <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<Input Type=Hidden id=hidTicketNo runat=server />
			<Input Type=Hidden id=hidGradedBunch value="" runat=server />
			<Input Type=Hidden id=hidRipe value="" runat=server />
			<Input Type=Hidden id=hidOverripe runat=server />
			<Input Type=Hidden id=hidUnderripe runat=server />
			<Input Type=Hidden id=hidUnripe runat=server />
			<Input Type=Hidden id=hidEmpty runat=server />
			<Input Type=Hidden id=hidRotten runat=server />
			<Input Type=Hidden id=hidPoor runat=server />
			<Input Type=Hidden id=hidSmall runat=server />
			<Input Type=Hidden id=hidLongStalk runat=server />
			
			<Input Type=Hidden id=hidContamination runat=server />
			<Input Type=Hidden id=hidOthers runat=server />
									
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<table cellpadding="2" cellspacing=0 width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuWMTrx id=MenuWMTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4">FFB ASSESSMENT DETAILS</td>
					<td colspan="2" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" />   </td>
				</tr>
				<tr>
					<td height=25>Ticket No :*</td>
					<td colspan=2><asp:dropdownlist id="lstTicketNo" runat="server" width=50% OnSelectedIndexChanged=Date_Insp autopostback=true size=1/>                       
						<asp:RequiredFieldValidator id="rfvTicketNo" runat="server"  
							ErrorMessage="Please Select Ticket No." 														
							ControlToValidate="lstTicketNo" 
							display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Period : </td>
					<td><asp:Label id="lblPeriod" runat="server"/></td>
				</tr>
				<tr>
					<td height=25>Date Inspected :*</td>
					<td colspan=2><asp:TextBox id="txtDateInsp" runat="server" width=50% maxlength="10"/>
						<a href="javascript:PopCal('txtDateInsp');">
						<asp:Image id="btnSelInspDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>					
						<asp:Label id=lblErrDateInsp forecolor=red runat=server/>
						<asp:Label id=lblErrDateInspMsg visible=false text="<br>Date Format should be in " runat=server/>						
						<asp:RequiredFieldValidator id="rfvDateInsp" runat="server"  
							ErrorMessage="<br>Date Inspected cannot be blank." 														
							ControlToValidate="txtDateInsp" 
							display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id="lblStatus" runat="server"/></td>
				</tr>
				<tr>
					<td height=25>Ripe Bunches :*</td>
					<td><asp:textbox id="txtRipeBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="18"/>
						<asp:RequiredFieldValidator id="rvfRipeBunches" runat="server"  
							ErrorMessage="Ripe Bunches cannot be blank." 														
							ControlToValidate="txtRipeBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvRipeBunches"
							ControlToValidate="txtRipeBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>		
						<asp:RegularExpressionValidator id="revRipeBunch" 
							ControlToValidate="txtRipeBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/> 													                                             					
					</td>
					<td><span id=ripe></span>%</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Overripe Bunches :*</td>
					<td><asp:textbox id="txtOverRipeBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfOverripeBunches" runat="server"  
							ErrorMessage="Overripe Bunches cannot be blank." 														
							ControlToValidate="txtOverRipeBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvOverRipeBunches"
							ControlToValidate="txtOverRipeBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>									
						<asp:RegularExpressionValidator id="revOverRipeBunches" 
							ControlToValidate="txtOverRipeBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>      
					</td>
					<td><span id=overripe></span>%</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
				</tr>
				<tr>
					<td height=25>Underripe Bunches :*</td>
					<td><asp:textbox id="txtUnderRipeBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfUnderripeBunches" runat="server"  
							ErrorMessage="Underripe Bunches cannot be blank." 														
							ControlToValidate="txtUnderRipeBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvUnderRipeBunches"
							ControlToValidate="txtUnderRipeBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>	
						<asp:RegularExpressionValidator id="revUnderRipeBunches" 
							ControlToValidate="txtUnderRipeBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>      														                                                         					
					</td>
					<td><span id=underripe></span>%</td>
					<td>&nbsp;</td>
					<td>Update By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
				</tr>
				<tr>
					<td height=25>Unripe Bunches :*</td>
					<td><asp:textbox id="txtUnripeBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfUnripeBunches" runat="server"  
							ErrorMessage="Unripe Bunches cannot be blank." 														
							ControlToValidate="txtUnripeBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvUnRipeBunches"
							ControlToValidate="txtUnripeBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>	
						<asp:RegularExpressionValidator id="revUnRipeBunches" 
							ControlToValidate="txtUnripeBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>
					</td>
					<td><span id=unripe></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Empty Bunches :*</td>
					<td><asp:textbox id="txtEmptyBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfEmptyBunches" runat="server"  
							ErrorMessage="Empty Bunches cannot be blank." 														
							ControlToValidate="txtEmptyBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvEmptyBunches"
							ControlToValidate="txtEmptyBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>								                                                         													                                            													                                                          					
						<asp:RegularExpressionValidator id="revEmptyBunches" 
							ControlToValidate="txtEmptyBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>							
					</td>
					<td><span id=empty></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Rotten Bunches :*</td>
					<td><asp:textbox id="txtRottenBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfRottenBunches" runat="server"  
							ErrorMessage="Rotten Bunches cannot be blank." 														
							ControlToValidate="txtRottenBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvRottenBunches"
							ControlToValidate="txtRottenBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>
						<asp:RegularExpressionValidator id="revRottenBunches" 
							ControlToValidate="txtRottenBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>														
					</td>
					<td><span id=rotten></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Poor Bunches :*</td>
					<td><asp:textbox id="txtPoorBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfPoorBunches" runat="server"  
							ErrorMessage="Poor Bunches cannot be blank." 														
							ControlToValidate="txtPoorBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvPoorBunches"
							ControlToValidate="txtPoorBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>								                                                         													                                            													                                                          													                                                         													                                                         					
						<asp:RegularExpressionValidator id="revPoorBunches" 
							ControlToValidate="txtPoorBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>
					</td>
					<td><span id=poor></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Small Bunches :*</td>
					<td><asp:textbox id="txtSmallBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfSmallBunches" runat="server"  
							ErrorMessage="Small Bunches cannot be blank." 														
							ControlToValidate="txtSmallBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvSmallBunches"
							ControlToValidate="txtSmallBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>								                                                         													                                            													                                                          													                                                         													                                                         													                                                           					
						<asp:RegularExpressionValidator id="revSmallBunches" 
							ControlToValidate="txtSmallBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>							
					</td>
					<td><span id=small></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Long Stalk Bunches :*</td>
					<td><asp:textbox id="txtLongStalkBunches" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfLongStalkBunches" runat="server"  
							ErrorMessage="Long Stalk Bunches cannot be blank." 														
							ControlToValidate="txtLongStalkBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvLongStalkBunches"
							ControlToValidate="txtLongStalkBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>								                                                         													                                            													                                                          													                                                         													                                                         													                                                           													                                                  					
						<asp:RegularExpressionValidator id="revLongStalkBunches" 
							ControlToValidate="txtLongStalkBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>
					</td>
					<td><span id=longstalk></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Contamination :</td>
					<td><asp:textbox id="txtContamination" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						
						<asp:RangeValidator id="rvContamination"
							ControlToValidate="txtContamination"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>								                                                         													                                            													                                                          													                                                         													                                                         													                                                           													                                                  					
						<asp:RegularExpressionValidator id="revContamination" 
							ControlToValidate="txtContamination"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>
					</td>
					<td><span id=contamination></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				
				<tr>
					<td height=25>Bunches Graded :</td>
					<td><span id=bg />
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Percentage Graded :*</td>
					<td><asp:textbox id="txtGradedPct" text=0 width=100% runat="server" maxlength="5"/>
						<asp:RequiredFieldValidator id="rvfPctGraded" runat="server"  
							ErrorMessage="Percentage Graded cannot be blank." 														
							ControlToValidate="txtGradedPct" 
							display="dynamic"/>
						<asp:RangeValidator id="rvGradedPct"
							ControlToValidate="txtGradedPct"
							MinimumValue="0"
							MaximumValue="100"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 100!"
							runat="server" display="dynamic"/>								                                                         													                                            													                                                          													                                                         													                                                         													                                                           													                                                  												                                                           					
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td width=20% height=25>Ungradable Bunches :*</td>
					<td width=15%><asp:TextBox id="txtUngBunches" text=0 width=100% runat="server" maxlength="10"/>
						<asp:RequiredFieldValidator id="rvfUngBunches" runat="server"  
							ErrorMessage="Ungradable Bunches cannot be blank." 														
							ControlToValidate="txtUngBunches" 
							display="dynamic"/>
						<asp:RangeValidator id="rvUngBunches"
							ControlToValidate="txtUngBunches"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>
						<asp:RegularExpressionValidator id="revUngBunches" 
							ControlToValidate="txtUngBunches"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>													                                                         													                                            													                                                          													                                                         													                                                         													                                                           													                                                  												                                                           													
					</td>
					<td width=15%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=30%>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Others :</td>
					<td><asp:textbox id="txtOthers" OnKeyUp="javascript:calcGradedBunch();" text=0 width=100% runat="server" maxlength="10"/>
						
						<asp:RangeValidator id="rvOthers"
							ControlToValidate="txtOthers"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="The value must be from 0 to 999999999999999!"
							runat="server" display="dynamic"/>								                                                         													                                            													                                                          													                                                         													                                                         													                                                           													                                                  					
						<asp:RegularExpressionValidator id="revOthers" 
							ControlToValidate="txtContamination"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 5 decimal points"
							runat="server"/>
					</td>
					<td><span id=others></span>%</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				
				<tr>
					<td height=25>Remarks :</td>
					<td colspan=5><asp:textbox id="txtRemarks" runat="server" maxlength=256 width=100%/></td>
				</tr>
				<tr>
					<td colspan="6" height=25>&nbsp;</td>					
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="btnDelete" imageurl="../../images/butt_delete.gif" onclick="btnDelete_Click" runat="server" AlternateText="Delete"/>
						<asp:ImageButton id="btnUnDelete" imageurl="../../images/butt_undelete.gif" onclick=btnUnDelete_Click runat=server AlternateText="Undelete" visible=false/>
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:Button ID="AddBtn3" runat="server" class="button-small" 
                            onclick="AddBtn3_Click" Text="save" />
                        <asp:Button ID="AddBtn4" runat="server" class="button-small" Text="delete" />
                        <asp:Button ID="AddBtn5" runat="server" class="button-small" Text="undelete" />
                        <asp:Button ID="AddBtn6" runat="server" class="button-small" Text="back" />
					</td>
				</tr>
			</table>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
