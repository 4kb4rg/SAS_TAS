<%@ Page Language="vb" src="../../../include/system_config_paramsetting.aspx.vb" Inherits="system_config_paramsetting"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Parameters Setting</title>		
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body leftmargin="10" topmargin="10" marginwidth="10" marginheight="10">
		<form id=frmMain enctype="multipart/form-data" class="main-modul-bg-app-list-pu" runat=server>
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 


		<table border="0" width="100%" cellspacing="0" cellpadding="2" class="font9Tahoma">
			<tr>
				<td colspan="5">
					<UserControl:MenuSYS id=MenuSYS runat="server" />
				</td>
			</tr>
			<tr>
				<td align=left class="font9Tahoma" colspan="5"><strong>PARAMETERS SETTING</strong> </td>
			</tr>
			<tr>
				<td colspan=5><hr style="width :100%" /></td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 1 : Application</td>
			</tr>
			<tr>
				<td valign=top width=5%>i</td>
				<td valign=top width=95% valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Which level to be used in COSTING ANALYSIS CONTROL</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rbBlock checked groupname=costlevel  autopostback=true oncheckedchanged=OnIndexChanged_ResetChargingLevel runat=server/> 
								<asp:RadioButton id=rbSubBlock groupname=costlevel  autopostback=true oncheckedchanged=OnIndexChanged_ResetChargingLevel runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>ii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Allow charging to <asp:label id="lblBlkTag1" runat="server" /> if <asp:label id="lblSubBlkTag1" runat="server" /> is used in COSTING ANALYSIS CONTROL :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbChargingToBlock text=" Yes" autopostback=true oncheckedchanged=OnIndexChanged_ResetChargingLevelDefault runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>iii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Which level of costing prefered to be defaulted if COSTING ANALYSIS CONTROL can be charged to <asp:label id="lblBlkTag2" runat="server" /> or <asp:label id="lblSubBlkTag2" runat="server" /> :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rbCLBlock checked groupname=chargelevel runat=server/> 
								<asp:RadioButton id=rbCLSubBlock groupname=chargelevel runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>iv</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Which level to be used in YIELD ANALYSIS CONTROL</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rbBlock_Yield checked autopostback=true oncheckedchanged=OnIndexChanged_AnalysisForProd groupname=yieldlevel runat=server/> 
								<asp:RadioButton id=rbSubBlock_Yield autopostback=true oncheckedchanged=OnIndexChanged_AnalysisForProd groupname=yieldlevel runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>v</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Automate inter-estate charging :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbInterEstateCharging text=" Yes" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td valign=top>vi</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Using 5 working Account Distribution (Workshop, Vehicle, General
							Charges, Medical and Housing) :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cb5Distribution text=" Yes" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 2 : Workshop</td>
			</tr>
			<tr>
				<td valign=top>i</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Internal Labour Charge Based On :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>								
								<asp:RadioButton id=rbIntLabCharge_Actual text="Actual" checked groupname=intLabCharge runat=server/> 
								<asp:RadioButton id=rbIntLabCharge_Budget text="Budget" groupname=intLabCharge runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>ii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Staff Labour Charge Based On :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>								
								<asp:RadioButton id=rbStfLabCharge_Actual text="Actual" checked groupname=stfLabCharge runat=server/> 
								<asp:RadioButton id=rbStfLabCharge_Budget text="Budget" groupname=stfLabCharge runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>iii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>External Party Labour Charge Based On :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>								
								<asp:RadioButton id=rbExtPtyLabCharge_Actual text="Actual" checked groupname=extptyLabCharge runat=server/> 
								<asp:RadioButton id=rbExtPtyLabCharge_Budget text="Budget" groupname=extptyLabCharge runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width=5%></td>
				<td width=100%><asp:label id="lblChooseActual" text="Either one of the above labour charges must be based on actual rate." Visible="False" ForeColor="Red" runat="server" /></td>
			</tr>
			<tr>
				<td valign=center>iv</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Run Workshop Cost Distribution Every 
							<asp:TextBox id=txtWSCostDistMth height=18px width=10% maxlength=2 runat=server />
							  &nbsp;Months
							<asp:RangeValidator display=dynamic id="numericWSCostDistMth"
								ControlToValidate="txtWSCostDistMth"
								MinimumValue="1"
								MaximumValue="99"
								Type="Integer"
								EnableClientScript="true"
								Text="The value must be from 1 to 99. "
								runat="server"/>
							</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>								
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>v</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Use Control Account for Workshop Expenses :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbUseCtrlAcct text=" Yes" runat=server/>								
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 3 : Nursery</td>
			</tr>
			<tr>
				<td valign=top>i</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Use one stage/two stage in nursery costing? :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rb1Stage text="1 Stage (Nursery)" checked groupname=nursery runat=server/> 
								<asp:RadioButton id=rb2Stage text="2 Stage (Pre & Main Nursery)" groupname=nursery runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 4 : Human Resource & Payroll</td>
			</tr>
			<tr>
				<td valign=top>i</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Employee CODE will be created</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rbAutoEmp text="Automatically" groupname=empcode runat=server/> 
								<asp:RadioButton id=rbManualEmp text="Manually" checked groupname=empcode runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>ii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Automate harvester piece rate/incentive from estate yield production :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbAutoIncentive text=" Yes" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>iii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Automation of Labour Overhead Distribution? :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbAutoLabourOverheadDist text=" Yes" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 5 : Production</td>
			</tr>
			<tr>
				<td valign=top>i</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Estate Yield for harvesting rate, total bunches and weight are updated automatically from Piece Rate Payment and Year of Planting Yield :<br>(Applicable if COSTING ANALYSIS and YIELD ANALYSIS are same level)</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbAutoYieldRate text=" Yes" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 6 : General Ledger</td>
			</tr>
			<tr>
				<td valign=top>i</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%><asp:label id="lblVeh1" runat="server" /> distribution method will be using running unit on :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rbVeh_MTD text=" Current Month " checked groupname=vehBase runat=server/> 
								<asp:RadioButton id=rbVeh_YTD text=" Year To Date " groupname=vehBase runat=server/> 
								<asp:RadioButton id=rbVeh_12 text=" Last 12 Months " groupname=vehBase runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>ii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%><asp:label id="lblVeh2" runat="server" /> Distribution Method will be distributed using :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rbVeh_Veh checked groupname=vehUse runat=server/> 
								<asp:RadioButton id=rbVeh_VehType groupname=vehUse runat=server/> 
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>iii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Auto reset accounts to zero balance in new accounting year :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbAutoResetPLAcc text=" Profit & Loss" runat=server/> <asp:CheckBox id=cbAutoResetBSAcc text=" Balance Sheet" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>iv</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1>
						<tr>
							<td valign=top width=40%>Auto transfer reset P&L accounts (iv) to retained earning account :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbAutoAccRetainEarn text=" Yes" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>v</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>General Charges will distribute :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:RadioButton id=rbGCDist_No autopostback=true oncheckedchanged=OnIndexChange_GCDist groupname=GCDist text="No Automate Distribution" runat=server/> <br>
								<asp:RadioButton id=rbGCDist_MthEnd autopostback=true oncheckedchanged=OnIndexChange_GCDist groupname=GCDist text="Automatically During Month End Process" runat=server /> <br>
								<asp:RadioButton id=rbGCDist_PreMth autopostback=true oncheckedchanged=OnIndexChange_GCDist groupname=GCDist text="Automatically On Preceding Month End Closed " runat=server /> <br>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>vi</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Proportioning general charges to mature and immature blocks prior distribution :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbProportionGC text=" Yes (define your proportion general charges at General Charges Distribution screen " runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td valign=top>vii</td>
				<td valign=top colspan=4>
					<table width=100% border=0 cellspacing=0 cellpadding=1 class="font9Tahoma">
						<tr>
							<td valign=top width=40%>Adjustment accounting period can be posted using Journal Adjustment :</td>
							<td width=5%>&nbsp;</td>
							<td valign=top width=55%>
								<asp:CheckBox id=cbAdjAccPeriod text=" Yes" runat=server/>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			<tr class="mr-h">
				<td class="normalbold" colspan="5">Step 7 : Save all parameters setting&nbsp;</td>
			</tr>
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=SaveBtn imageurl="../../images/butt_save.gif" alternatetext=Save onclick=SaveBtn_Click runat=server />
					<asp:Label id=lblCentreControl visible=false runat=server />
				</td>
			</tr>
		</table>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</Script>
</html>
