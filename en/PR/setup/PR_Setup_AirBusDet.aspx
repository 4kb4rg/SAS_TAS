<%@ Page Language="vb" src="../../../include/PR_setup_AirBusdet.aspx.vb" Inherits="PR_setup_AirBusdet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Air Fare/Bus Ticket Details</title>
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
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=AirBuscode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6"><strong> <asp:Label id=lblTitle runat=server />DETAILS</strong></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height="25"><asp:Label id=lblTitle1 runat=server />Code :* </td>
					<td width="30%">
						<asp:Textbox id="txtAirBusCode" width="50%" maxlength="8" runat="server"/>
						<asp:Label id=lblErrDupAirBusCode visible=false forecolor=red text="<br>Ticket code in used, please try other Ticket Code." runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Air Fare/Bus Ticket Code"
							ControlToValidate=txtAirBusCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtAirBusCode"
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
					<td height=25>Description :* </td>
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
					<td height=25><asp:Label id="lblPOH" runat="server" /> *:</td>
					<td><asp:DropDownList id=ddlPOHCode width=100% runat=server/>
						<asp:Label id=lblErrPOHCode visible=false forecolor=red text="Please select one " runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:Label id="lblNearLoc" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlNearLocation width=100% runat=server/>
						<asp:Label id=lblErrNearLocation visible=false forecolor=red text="Please select one " runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By: </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:Label id="lblLoc" runat="server" /> *:</td>
					<td><asp:DropDownList id=ddlLocation width=100% runat=server/>
						<asp:Label id=lblErrLocation visible=false forecolor=red text="Please select one " runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Allowance and Deduction Code :*</td>
					<td><asp:DropDownList id=ddlADCode  width=85% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlADCode','1',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrADCode visible=false forecolor=red text="<br>Please select one Allowance And Deduction Code." runat=server/>
						
					</td>
				</tr>

				<tr >
					<td colspan=6>
						<table id="tblSelection" width="100%" class="sub-add" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
						<tr>						
							<td>
								<table border=0 cellpadding=2 cellspacing=0 width=100%  class="font9Tahoma">				
									<tr>
										<td width="15%" height="26">Type :*<BR>
										<td valign=top width=20%>
											<asp:DropDownList id="ddlType" width=100% runat=server onSelectedIndexChanged=onSelect_ChangeType>
												<asp:ListItem value="0" selected>Select Type</asp:ListItem>
												<asp:ListItem value="1">Air Fare</asp:ListItem>
												<asp:ListItem value="2">Bus Ticket</asp:ListItem>
											</asp:DropDownList>
											<asp:Label id=lblErrType visible=false forecolor=red text="<br>Please enter Type." runat=server/>							
										</td>
										</td>
																			
										<td>&nbsp;</td>
										<td>Amount :* </td>
										<td><asp:Textbox id=txtAmount runat=server /></td>
											<asp:Label id=lblErrAmount visible=false forecolor=red text="<br>Please enter Amount." runat=server/>							
										<td>&nbsp;</td>
									</tr>				
									<tr>
										<td  width=15% height="26">Category :*<BR>
										<td valign=top width=20%>
											<asp:DropDownList id="ddlCategory" width=100% runat=server>
												
											</asp:DropDownList>
											<asp:Label id=lblErrCategory visible=false forecolor=red text="<br>Please enter Category." runat=server/>							
										</td>
										</td>										
									</tr>

									<tr class="mb-c">
										<TD vAlign="top" colspan=6 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" onclick=btnAdd_Click alternatetext=Add runat=server />
										<asp:Label id=lblErrFill visible=false forecolor=red text="<br>Please select 2 destination only." runat=server/>
										<asp:Label id=lblErrInsert visible=false forecolor=red text="<br>Air Fare/Bus Ticket Details must be input first." runat=server/>
										</TD>
									</tr>
								</table>
							</td>
						</tr>									
						</table>
					</td>
				</tr>
				<tr><td colspan=5><asp:Label id=lblErrDelete forecolor=red visible=false text="Unable to delete location because it has been associated in system configuration." runat=server />
								<asp:Label id=lblErrExists visible=false forecolor=red text="<br>Selected AD Code has been used for POH and Location above. <br>Please select other POH or Location or AD Code. " runat=server/>
								<asp:Label id=lblErrDouble visible=false forecolor=red text="<br>Selected Type or Category already exists for Ticket Code above. <br>Please select other Type or Category. " runat=server/>
				</td></tr>
				<tr>
					<td colspan="6">		    						
						<asp:DataGrid id=dgAirBusTicket
							AutoGenerateColumns=false width="100%" runat=server
							GridLines=none
							Cellpadding=2
							OnDeleteCommand=DEDR_DeleteAirBus
							Pagerstyle-Visible=False
							AllowSorting="True" CssClass="font9Tahoma" >
                        <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>						
								<asp:TemplateColumn HeaderText="Point Of Hired">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("POHCode") %> id="lbldgPOHCode" visible=TRUE runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Location">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Location") %> id="lbldgLocation" visible=TRUE runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Type") %> id="lbldgType" visible=TRUE runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Category">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Category")%> id="lbldgCategory" visible=TRUE runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Amount">
									<ItemTemplate>
										<asp:Label Text=<%# objGlobalHdl.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Amount"),0) %> runat="server" />
										<asp:Label Text=<%# Container.DataItem("Amount") %> id="lbldgAmount" visible=FALSE runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("TicketCode") %> id="lbldgTicketCode" visible=False runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>								
								
								<asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server CausesValidation=False />
									</ItemTemplate>
								</asp:TemplateColumn>										
							</Columns>
						</asp:DataGrid>		
					</td>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>				
				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				
				<tr>
					<td ColSpan="6">
					<table border=0 cellspacing=0 cellpadding=2 width=100%>
						<tr>
							<td colspan=5 width=60%>&nbsp;</td>
							<td colspan=2 align=right><hr size="1" noshade width=100%></td>
						</tr>
						<tr>
							<td colspan=5 width=60%>&nbsp;</td>											
							<td align=right>Total Amount : <asp:Label ID=lblTotalAmount Runat=server /></td>							
						</tr>						
					</table>
					</td>
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
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
         	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
