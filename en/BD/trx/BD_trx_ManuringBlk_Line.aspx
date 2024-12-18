<%@ Page Language="vb" trace="false" src="../../../include/BD_trx_ManuringBlk_Line.aspx.vb" Inherits="BD_ManuringBlk_Line" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Manuring Schedule</title>
		<script language="Javascript">
			
			function FComma(SS) {
				var T = "", S = String(SS), L = S.length - 1, C, j, P = S.indexOf(".") - 1;
				var D;
				if (P < 0) {
					P = L;
				}
				for (j = 0; j <= L; j++) {
					T += (C = S.charAt(j));
					if ((j < P) && ((P - j) % 3 == 0) && (C != "-")) {
						D = S.charAt(j+1) //look ahead for decimal ','
						if (D !=",")
						{
							T += ".";
						}
					}
				}
				return T;
			}

			function calQty() {
				var doc = document.frmMain;
				var r = parseFloat(doc.txtRates.value);
				var s = parseFloat(doc.hidSPH.value);
				var a = parseFloat(doc.hidPlantedArea.value);
				var varStr ; 
				//var label;
				//label = document.getElementById('lblErrMessage2');
				//label.style.visibility = 'hidden';
				
				doc.rsl.value = r * s * a / 1000
				if (doc.rsl.value == 'NaN') 
					doc.rsl.value = '';
				else
					doc.rsl.value = round(doc.rsl.value, 5);
					//fs 2.1 hym
					varStr=doc.rsl.value;
					varStr = varStr.replace(/\./, ',');
					varStr=FComma(varStr);
					document.getElementById(Qty.id).innerHTML = varStr;
					
			}
		</script>				
	</head>
	<body onload="javascript:calQty()">
	<Preference:PrefHdl id=PrefHdl runat="server" />
		<form runat="server" ID="frmMain">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblBlock visible=false runat=server />
			<asp:label id=lblSubBlk visible=false runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />
			<asp:Label id=lblBgtStatus Visible="False"  runat="server" />
			<table border=0 cellspacing="0" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuBDtrx id=menuBD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6" width=60%><asp:label id="lblTitle" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>				
				<tr>
					<td width=20% height=25><asp:label id="lblLocTag" runat="server"/> : </td>
					<td width=30% colspan=5><asp:label id="lblLocCode" runat="server"/></td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblBgtTag" text="Budgeting Period " runat="server"/> : </td>
					<td width=30% colspan=5><asp:label id="lblBgtPeriod" runat="server"/></td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblyrTag" text="Planting Year" runat="server"/> : </td>
					<td width=30% colspan=5><asp:label id="lblYearPlanted" runat="server"/></td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblBlkTag" runat="server"/> : </td>
					<td width=30% colspan=5><asp:label id="lblBlkCode" runat="server"/></td>
				</tr>
				<tr>
					<td width=20% height=25>Selected Period : </td>
					<td width=30% colspan=5><asp:label id="lblSelPer" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%></td>
					<td align="right" colspan="2" width=40%><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>
						<table id="tblAdd" border=0 width="100%" cellspacing="0" cellpadding="2" runat="server">
							<tr class="mb-c" >
								<td height=25 width=20%>Fertilizer : </td>
								<td width=40%><asp:dropdownlist width=50% id="lstFert"  runat="server"/>
									<asp:RequiredFieldValidator 
										id="validateCode" 
										runat="server" 
										ErrorMessage="Please select Fertilizer"
										ControlToValidate="lstFert" 
										display="dynamic"/>																		
								</td>
								<td width=40%>&nbsp;</td>
							</tr>
							<tr class="mb-c" >
								<td height=25 width=20%>Rate per palm : </td>
								<td width=40%><asp:textbox OnKeyUp="javascript:calQty();" id="txtRates" Width=50% maxlength=19 EnableViewState=False Runat="server" />
									<asp:RegularExpressionValidator id="RegularExpressionValidatorRates" 
										ControlToValidate="txtRates"
										ValidationExpression="\d{1,19}"
										Display="Dynamic"
										text = "Maximum length 19 digits and 0 decimal points"
										runat="server"/>
									<asp:RequiredFieldValidator 
										id="validateRates" 
										runat="server" 
										ErrorMessage="Please enter Rates" 
										ControlToValidate="txtRates" 
										display="dynamic"/>
									<asp:RangeValidator id="Range1"
										ControlToValidate="txtRates"
										MinimumValue="0"
										MaximumValue="9999999999999999999"
										Type="double"
										EnableClientScript="True"
										Text="The value is out of acceptable range !"
										runat="server" display="dynamic"/>
								</td>	
								<td width=40%>&nbsp;</td>
							</tr>
							<tr class="mb-c" >
								<td height=25 width=20%>Quantity : </td>
								<td width=40%><span id=Qty></span></td>
								<td width=40%>&nbsp;</td>
							</tr>
							<tr class="mb-c" >
								<td colspan=6><asp:ImageButton text="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" />
								<asp:label id=lblErrMessage2 visible=false forecolor=red text="Error in updating database (Quantity overflow) !" runat=server /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td ColSpan=6>&nbsp;</td>
				</tr>
				<tr>
					<table cellpadding=2 cellspacing=1 width=100% border=0>
						<TD colspan=6>					
							<asp:DataGrid id="dgManBlkLine"
								AutoGenerateColumns="false" width="100%" runat="server"
								GridLines = none
								OnEditCommand="DEDR_Edit"
			 					OnItemDataBound="DataGrid_ItemDataBound" 
								OnUpdateCommand="DEDR_Update"
								OnCancelCommand="DEDR_Cancel"
								OnDeleteCommand="DEDR_Delete"
	 							Cellpadding = "2"
								AllowSorting="false">
						    <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC" />
							<ItemStyle CssClass="mr-l" BackColor="#FEFEFE" />
							<AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE" />
								<Columns>
									<asp:TemplateColumn HeaderText="Item Code">
										<ItemStyle Width="20%" />
										<ItemTemplate>
											<asp:label text=<%# Container.DataItem("FertItemCode") %> id="FertItemCode" runat="server" />									
											<asp:label id="lblManuringBlkLnID" Text =<%# Trim(Container.DataItem("ManuringBlkLnID")) %> visible=false runat="server"/>
										</ItemTemplate>
									</asp:TemplateColumn>
			
									<asp:TemplateColumn HeaderText="Description">
										<ItemStyle Width="30%" />
										<ItemTemplate>
											<%# Container.DataItem("Description") %>
										</ItemTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="Rates">
										<ItemStyle Width="20%" />
										<ItemTemplate>
											<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("Rates"), 0, True, False, False)) %>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:TextBox id="txtRates" width=100% MaxLength="19"
												Text='<%# FormatNumber(Container.DataItem("Rates"),0, True, False, False) %>'
												runat="server"/>
											<asp:RequiredFieldValidator id=rfvRates display=dynamic runat=server 
													text="Please enter rates"
													ControlToValidate=txtRates />															
											<asp:RegularExpressionValidator id="RegExpValLabCost" 
												ControlToValidate="txtRates"
												ValidationExpression="\d{1,19}"
												Display="Dynamic"
												text = "Maximum length 19 digits and 0 decimal points"
												runat="server"/>
											<asp:RangeValidator id="RangeRates"
												ControlToValidate="txtRates"
												MinimumValue="0"
												MaximumValue="9999999999999999999"
												Type="double"
												EnableClientScript="True"
												Text="The value is out of range !"
												runat="server" display="dynamic"/>
										</EditItemTemplate>										
									</asp:TemplateColumn>
											
									<asp:TemplateColumn HeaderText="Quantity">
										<ItemStyle Width="20%" />
										<ItemTemplate>
											<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Qty"), 5, True, False, False),5) %>
										</ItemTemplate>
									</asp:TemplateColumn>
								
									<asp:TemplateColumn>								
										<ItemStyle Width="10%" />
										<ItemTemplate>
											<asp:LinkButton id="Edit" CommandName="Edit" CausesValidation=False Text="Edit" runat="server"/>
										</ItemTemplate>									
										<EditItemTemplate>									
											<asp:LinkButton id="Update" CommandName="Update" Text="Save" CausesValidation=False
												runat="server"/>
											<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False
												runat="server"/>
											<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False
												runat="server"/>										
										</EditItemTemplate>
									</asp:TemplateColumn>														
								</Columns>											
							</asp:DataGrid>
						</td>
					</table>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="mb-t">
								<td width="20%" height="26" valign=bottom><B>TOTAL</B></td>
								<td width="30%" height="26" valign=bottom>&nbsp;</td>
								<td width="20%" height="26" valign=bottom>&nbsp;</td>
								<td width="30%" height="26" valign=bottom>
									<B><asp:Label id=lblTotQty width=100% runat="server"/></B>
								</td>
							 </tr>
						</table><BR>
					</td>
				</tr>				
				<tr>
					<td ColSpan=6>&nbsp;</td>
				</tr>				
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=hidBlkCode runat=server />			
				<Input Type=Hidden id=hidSPH runat=server />			
				<Input Type=Hidden id=hidPlantedArea runat=server />			
				<Input Type=Hidden id=rsl value="" runat=server />
			</table>		
		</FORM>
	</body>
</html>
