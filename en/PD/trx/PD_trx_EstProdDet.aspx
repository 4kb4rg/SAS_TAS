<%@ Page Language="vb" src="../../../include/PD_trx_EstProdDet.aspx.vb" Inherits="PD_trx_EstProdDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Oil Palm Yield Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="Javascript">			
			function calweight() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtBunches.value);
				var b = parseFloat(doc.txtWeight.value);
				var c = parseFloat(doc.txtDedWeight.value);
				doc.rsl.value = (b - c) / a;
				if (doc.rsl.value == 'NaN' || doc.rsl.value == 'Infinity') 
					doc.rsl.value = '';
				else
					doc.rsl.value = DisplayIDDecimalSeparator(doc.rsl.value, 1);
					document.getElementById(txtabw.id).innerHTML = doc.rsl.value;
					doc.rsl.value = DisplayIDDecimalSeparator(b - c, 1);
				    document.getElementById(spanWeight.id).innerHTML = doc.rsl.value;
				    			
			}
			
			
		</script>
	    <style type="text/css">
            .style1
            {
                height: 32px;
            }
        </style>
	</head>
	<body onload="javascript:calweight();">
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select one " runat="server" />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=eyid runat=server />
			<Input Type=Hidden id=rsl value="" runat=server />		
			<Input Type=Hidden id=inTotalBunches runat=server />	
			<Input Type=Hidden id=inBacklogBunche runat=server />
			<Input Type=Hidden id=inActualProductionWeight runat=server />
			<Input Type=Hidden id=inEstateProductionWeight runat=server />	
			<Input Type=Hidden id=inHarvesterOutput runat=server />
			<Input Type=Hidden id=inManday runat=server />						
			<asp:label id="SortExpression" Visible="False" Runat="server"/>
			<asp:label id="sortcol" Visible="False" Runat="server"/>
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPDTrx id=MenuPDTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">OIL PALM YIELD DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td height=25 width = 20%>Oil Palm Yield ID : </td>
					<td width = 55%><asp:Label id=lblEstateYieldID runat=server/></td>
					<td width = 0%>&nbsp;</td>
					<td width = 15%>Period : </td>
					<td width = 15%><asp:Label id=lblPeriod runat=server /></td>
					<td width = 0%>&nbsp;</td>
				</tr>
				<tr runat = server visible=false>
					<td height=25><asp:label id="lblAccount" runat="server" /> :* (DR)</td>
					<td><asp:DropDownList id=ddlAccCode  width=80% runat=server/> 
						<input type="button" id=btnFind3 value=" ... "  runat=server/>
						<asp:Label id=lblErrAcc visible=false forecolor=red text="<br>Please select one account code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Reference Date :* </td>
					<td >
						<asp:Textbox id=txtDate width=30% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator id=rfvDate display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Date."
								ControlToValidate=txtDate />
						<asp:Label id=lblErrDate forecolor=red runat=server/>
						<asp:Label id=lblErrDateMsg visible=false text="<br>Date Format should be in " runat=server/>
					</td>
					<td nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Reference No :* </td>
					<td><asp:Textbox id=txtRefNo width=50% maxlength=20 runat=server/>
						<asp:RequiredFieldValidator id=rfvRefNo display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Reference No."
								ControlToValidate=txtRefNo />
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25><asp:label id="lblBlock" runat="server" /> :* </td>
					<td><asp:DropDownList id=ddlBlkCode width=85% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','ddlBlkCode','','','','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrBlk visible=false forecolor=red text="<br>Please select one block code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr runat=server visible =false>
					<td height=25>Harvesting Rate :*</td>
					<td><asp:Textbox id=txtHarvRate Text = "1"  runat=server/>
						
					</td>
					<td>&nbsp;</td>
					<td>Total Bunches : </td>
					<td><asp:Label id=lblTotalBunche runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible =false>
					<td height=25>No. of Round :*</td>
					<td><asp:Textbox id=txtRoundNo  Text = "1"  runat=server/>
					
					</td>
					<td>&nbsp;</td>
					<td>Backlog Bunches : </td>
					<td><asp:Label id=lblBacklogBunche runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible =false>
					<td height=25>Harvester ID :*</td>
					<td>
						<asp:DropDownList id=ddlEmployee width=80% runat=server/> 
						<input type="button" id=btnFind2 value=" ... "  runat=server/>
						<asp:Label id=lblErrEmployee visible=false forecolor=red text="<br>Please select one employee code ." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td nowrap>Estate Production Weight : </td>
					<td><asp:Label id=lblEstateProductionWeight runat=server/><span id="EstateProductionWeightUnit"></span></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>No of Bunches :*</td>
					<td><asp:Textbox OnKeyUp="javascript:calweight();" Text = "0" id=txtBunches maxlength=21 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rfvBunches display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter No of Bunches."
								ControlToValidate=txtBunches />												
						<asp:CompareValidator id="cvBunches" display=dynamic runat="server" 
							ControlToValidate="txtBunches" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revBunches 
							ControlToValidate="txtBunches"
							ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
							Display="Dynamic"
							text = "Maximum length 9 digits and 5 decimal points. "
							runat="server"/>
						<asp:RangeValidator id="rvBunches"
							ControlToValidate="txtBunches"
							MinimumValue="0.00001"
							MaximumValue="999999999999999.99999"
							Type="Double"
							EnableClientScript="True"
							Text="Must be greater than zero."
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td runat=server visible=false><asp:Label id=lblActualProductionWeight runat=server/><span id=ActualProductionWeightUnit></span></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Weight :* </td>
					<td><asp:Textbox OnKeyUp="javascript:calweight();" id=txtWeight Text = "0" maxlength=21 width=40% runat=server/>&nbsp;&nbsp; KG
					<asp:RequiredFieldValidator id=rfvAWeight display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Weight."
							ControlToValidate=txtWeight />												
					<asp:CompareValidator id="cvAWeight" display=dynamic runat="server" 
						ControlToValidate="txtWeight" Text="<br>The value must whole number or with decimal. " 
						Type="Double" Operator="DataTypeCheck"/>
					<asp:RegularExpressionValidator id=revAWeight 
						ControlToValidate="txtWeight"
						ValidationExpression="\d{1,21}\.\d{1,5}|\d{1,21}"
						Display="Dynamic"
						text = "Maximum length 21 digits and 5 decimal points. "
						runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Deduction Weight :* </td>
					<td><asp:Textbox Text = "0" OnKeyUp="javascript:calweight()" id=txtDedWeight maxlength=21 width=40% runat=server/>&nbsp;&nbsp; KG
					<asp:RequiredFieldValidator id=rfvDedWeight display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Deduction Weight."
							ControlToValidate=txtDedWeight />												
					<asp:CompareValidator id="cvDedWeight" display=dynamic runat="server" 
						ControlToValidate="txtDedWeight" Text="<br>The value must whole number or with decimal. " 
						Type="Double" Operator="DataTypeCheck"/>
					<asp:RegularExpressionValidator id=revDedWeight 
						ControlToValidate="txtDedWeight"
						ValidationExpression="\d{1,21}\.\d{1,5}|\d{1,21}"
						Display="Dynamic"
						text = "Maximum length 21 digits and 5 decimal points. "
						runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Net Weight :</td>
					<td><asp:Label id=spanWeight width=40% runat="server" /> &nbsp;&nbsp; KG </td>
					<td>&nbsp;</td>
					<td height=25></td>
					<td runat=server visible=false><asp:Label id=amount runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Average Bunch Weight : </td>
					<td><asp:Label  id=txtabw  width=40% runat=server />&nbsp;&nbsp; KG
					
					</td>
					<td>&nbsp;</td>
					<td height=25></td>
					<td runat=server visible=false><asp:Label id=lblHarvestIntv runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible=false>
					<td height=25>Bunches Quota :*</td>
					<td><asp:Textbox  id=txtBunchesQuota runat=server/>
						
					</td>
					<td>&nbsp;</td>
					<td height=25 nowrap>No of Harvester Mandays : </td>
					<td><asp:Label id=lblNoHarvester runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible=false>
					<td height=25>Bunches Over Quota :*</td>
					<td><asp:Textbox  id=txtBunchesOverQuota runat=server/>
					</td>
					<td>&nbsp;</td>
					<td height=25>Total No Of Harvester : </td>
					<td><asp:Label id=lblTotHarvester runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible=false>
					<td height=25 nowrap>Total Bunches Delivered To Mill :*</td>
					<td><asp:Textbox  id=txtBunchesDeliverToMill  runat=server/>
					</td>
					<td>&nbsp;</td>
					<td height=25></td>
					<td><asp:Label id=lblHaMdAboveQuota runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible=false>
					<td height=25>Estimated ABW :*</td>
					<td><asp:Textbox id=txtEstimatedABW  runat=server/>
					</td>
					<td>&nbsp;</td>
					<td height=25></td>
					<td><asp:Label id=lblHaMdLessQuota runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible=false>
					<td height=25>Harvester Output : </td>
					<td><asp:Label id=lblHarvesterOutput runat=server/></td>
					<td>&nbsp;</td>
					<td height=25></td>
					<td><asp:Label id=lblHaMdTotal runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible=false>
					<td height=25>Actual Harvested Area :*</td>
					<td><asp:Textbox id=txtActualHA maxlength=21 width=100% runat=server/>
						
					</td>
					<td>&nbsp;</td>
					<td height=25></td>
					<td><asp:Label id=lblAverageBacklog runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr runat=server visible=false>
					<td height=25>Round Period : </td>
					<td><asp:label id=lblRoundPeriod runat=server/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	

				<tr>
					<td class="style1" colspan="6" class="font9Tahoma"><br>ACTUAL CROP DELIVERY</td>
				</tr>	
				<tr>
					<td colspan="6">
					<asp:ImageButton id=ibAdd imageurl="../../images/butt_add.gif" OnClick="DEDR_Add" AlternateText="Add" runat="server"/>
					</td>
				</tr>	
				</table>			
                            
                            
                <table style="width: 100%" class="font9Tahoma">            								
				<tr>
					<TD >					
					<asp:DataGrid id="EventData"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"						
						AllowPaging="false" 
						Allowcustompaging="False"
						Pagerstyle-Visible="False"
						OnSortCommand="Sort_Grid" 
						AllowSorting="True"
                            class="font9Tahoma">	
							 
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
		
						<asp:TemplateColumn HeaderText="MillCode" SortExpression="Mill" ItemStyle-Width="15%">
							<ItemTemplate>
								<%# Container.DataItem("Mill") %>
								<asp:label id=lblMillCode visible=false text='<%# Container.DataItem("Mill") %>' runat=server/>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList id="ddlMillCode" width=100% runat=server>
								</asp:DropDownList>
								
								<asp:Label id=lblErrMillCode visible=false text ="Please select Mill Code" forecolor=red runat=server/>	
							</EditItemTemplate>
						</asp:TemplateColumn>		
														
					<asp:TemplateColumn HeaderText="Total Weight Delivered Crop" SortExpression="Total">
						<ItemStyle Width="20%" />
						<ItemTemplate>
							<%# objGlobal.DisplayQuantityFormat(Container.DataItem("Total")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtTotalBunch" width=100% MaxLength="21"
								Text='<%# trim(Container.DataItem("Total")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateTotalBunch display=Dynamic runat=server 
									ControlToValidate=txtTotalBunch />
							<asp:RegularExpressionValidator id="revTotalBunch" 
								ControlToValidate="txtTotalBunch"
								ValidationExpression="\d{1,21}\.\d{1,5}|\d{1,21}"
								Display="Dynamic"
								text = "Maximum length 21 digits and 5 decimal places"
								runat="server"/>							
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Total Weight Deduction" SortExpression="TotalDed">
						<ItemStyle Width="20%" />
						<ItemTemplate>
							<%# objGlobal.DisplayQuantityFormat(Container.DataItem("TotalDed")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="txtTotalDed" width=100% MaxLength="21"
								Text='<%# trim(Container.DataItem("TotalDed")) %>'
								runat="server"/>
							<asp:RequiredFieldValidator id=validateTotalDed display=Dynamic runat=server 
									ControlToValidate=txtTotalDed />
							<asp:RegularExpressionValidator id="revTotalDed" 
								ControlToValidate="txtTotalDed"
								ValidationExpression="\d{1,21}\.\d{1,5}|\d{1,21}"
								Display="Dynamic"
								text = "Maximum length 21 digits and 5 decimal places"
								runat="server"/>							
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					
					<asp:TemplateColumn  ItemStyle-Width=0 visible=false>
						<ItemTemplate>
						<asp:Label id=lblEstateYieldLNID runat=server
							Text='<%# Container.DataItem("EstateYieldLNID") %>' />
						</ItemTemplate>			
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>					
						<ItemTemplate>
						<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete"
							runat="server"/>&nbsp;&nbsp;
						<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit"
							runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
						<asp:LinkButton id="Update" CommandName="Update" Text="Save"
							runat="server"/>&nbsp;&nbsp;
						<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False
							runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid>
		 
				</tr>											
				<tr>
					<td ><asp:label id=lblExceedBunchDelivered visible=false text = "The Total Weight Delivered/Deduction Crop cannot exceed Total Weight Delivered/Deduction To Mill" forecolor=red runat=server/></td>
				</tr>	
				</tr>			
				<tr>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=btnNew AlternateText=" New " CausesValidation=False imageurl="../../images/butt_new.gif" onclick=Button_Click CommandArgument=New runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				
				<tr>
					<td>
                                            &nbsp;</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>


        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
