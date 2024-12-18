<%@ Page Language="vb" src="../../../include/IN_Trx_ItemToMachine_details.aspx.vb" Inherits="IN_ItemToMachineDetail" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_intrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Assign Item To Machine Details</title>
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                width: 418px;
            }
        </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="Please select " runat="server" />
			<asp:label id=lblSelect visible=false text="Select " runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuINTrx id=MenuINTrx runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6">
                        <table cellspacing="1" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong>ASSIGN ITEM TO MACHINE ENTRY DETAILS</strong> </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />
                                    | Last Updated : <asp:Label id=lblUpdatedBy runat=server />| Updated By : <asp:Label id=lblLastUpdate runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id=lblBlkTag Runat="server"/> :*</td>
					<td width=30%><asp:DropDownList id="ddlBlock" CssClass="font9Tahoma" Width=100% AutoPostBack=True OnSelectedIndexChanged=onSelect_Block runat=server />
										<asp:label id=lblErrBlock Visible=False forecolor=red Runat="server" /></td>
					<td>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblSubBlkTag Runat="server"/> :*</td>
					<td><asp:DropDownList id="ddlSubBlock" CssClass="font9Tahoma" Width=100% AutoPostBack=True OnSelectedIndexChanged=onSelect_SubBlock runat=server />
										<asp:label id=lblErrSubBlock Visible=False forecolor=red Runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
                <table width="100%" class="font9Tahoma" cellspacing="0" cellpadding="2" border="0" align="center" runat=server>
                <tr>
                <td>
				<tr class=mb-t>
					<td>
						<table id="tblSelection" width="100%" class="font9Tahoma" cellspacing="0" cellpadding="4" border="0" align="center" runat=server>
							<tr>						
								<td>
									<table border=0 cellpadding=2 cellspacing=0 width=100% class="font9Tahoma">
										<tr>
											<td width="20%" height=25>Item Code :*</td>
											<td width="80%"><asp:DropDownList id="ddlItem" CssClass="font9Tahoma" Width="70%" AutoPostBack=True OnSelectedIndexChanged=onSelect_Item runat=server />
                                                    <input type=button value=" ... " id="FindIN"  onclick="javascript:PopItem('frmMain', '', 'ddlItem', 'True');" CausesValidation=False runat=server />                        
													<asp:label id=lblItemCodeErr text="<br>Please select one Item" Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										
										<tr>
											<td valign=top height=25 width=20%>Installation Date :*</td>
											<td valign=top width=80% colspan=5><asp:TextBox id=txtDate CssClass="font9Tahoma" Width=25% maxlength=10 runat=server />
															<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
															<asp:Label id=lblErrDate text="Please enter Installation Date." visible=false forecolor=red runat=server />
															<asp:Label id=lblErrDateFmt visible=false forecolor=red runat=server/>
															<asp:Label id=lblErrDateFmtMsg visible=false text="<br>Date format should be in " runat=server/>
											</td>
										</tr>
										<tr>
											<td width="20%" height=25>Line Number :*</td>
											<td width="80%"><asp:DropDownList id="ddlLineNo" CssClass="font9Tahoma" Width=15% runat=server>
															<asp:ListItem value="1">1</asp:ListItem>
															<asp:ListItem value="2">2</asp:ListItem>
															<asp:ListItem value="3">3</asp:ListItem>
															<asp:ListItem value="4">4</asp:ListItem>
															<asp:ListItem value="5">5</asp:ListItem>
															<asp:ListItem value="6">6</asp:ListItem>
															<asp:ListItem value="7">7</asp:ListItem>
															<asp:ListItem value="8">8</asp:ListItem>
															<asp:ListItem value="9">9</asp:ListItem>
															<asp:ListItem value="10">10</asp:ListItem>
															<asp:ListItem value="11">11</asp:ListItem>
															<asp:ListItem value="12">12</asp:ListItem>
															<asp:ListItem value="13">13</asp:ListItem>
															<asp:ListItem value="14">14</asp:ListItem>
															<asp:ListItem value="15">15</asp:ListItem>
															<asp:ListItem value="16">16</asp:ListItem>
															<asp:ListItem value="17">17</asp:ListItem>
															<asp:ListItem value="18">18</asp:ListItem>
															<asp:ListItem value="19">19</asp:ListItem>
															<asp:ListItem value="20">20</asp:ListItem>
															</asp:DropDownList>
															<asp:label id=lblLineNoErr text="<br>Please select one Line Number" Visible=False forecolor=red Runat="server" />
											</td>
										</tr>
										<tr>
											<td width="20%" height=25>Line Description :</td>
											<td width="80%"><asp:TextBox id=txtLineDesc CssClass="font9Tahoma" Width=75% runat=server EnableViewState=True />															
											</td>
										</tr>
										<tr>
											<td width="20%" height=25>Annual Part Quantity :</td>
											<td width="80%"><asp:TextBox id=txtPartQty CssClass="font9Tahoma" Width=25% Text=0 runat=server EnableViewState=True />															
											</td>
										</tr>
										<tr>
											<td width="20%" height=25>Mechanic Hour :</td>
											<td width="80%"><asp:TextBox id=txtMechHour CssClass="font9Tahoma" Width=25% Text=0 runat=server EnableViewState=True />															
											</td>
										</tr>
                                        <tr>
                                            <td height="25" width="20%">
                                                Life Time</td>
                                            <td width="80%">
                                                <asp:TextBox id=TxtLifeTime CssClass="font9Tahoma" Width=25% Text=0 runat=server EnableViewState=True /></td>
                                        </tr>
										<tr class="mb-c">											
											<td colspan=6 height=25>
												<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
												&nbsp;<asp:Button ID="Issue10" runat="server" class="button-small" Text="Add" />
&nbsp;<asp:Label id=lblErrDupl visible=false forecolor=red text="Item Code already exists." runat=server/>
												<asp:Label id=lblErrExceeding visible=false forecolor=red text="Exceeding Block Total Area." runat=server/>
											</td>
										</tr>									
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<asp:DataGrid id=dgLineDet
							AutoGenerateColumns=False width="100%" runat=server
							GridLines=None
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
								<asp:TemplateColumn HeaderText="Item Code">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ItemCode") %>  id=ItemCode runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="10%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Description">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("ItemDescr") %>  id="ItemDescr" runat="server" />
									</ItemTemplate>
                                    <ItemStyle Width="25%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Line No.">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("LinesNo") %>  id="LinesNo" runat="server" />
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Line Description">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("LinesDesc") %>  id="LinesDesc" runat="server" />
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Installation Date">
									<ItemTemplate>
										<asp:label text=<%# objGlobal.GetLongDate(Trim(Container.DataItem("InstallDate"))) %> id="lblInstallDate" runat="server" />	
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Lifetime">
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("Lifespan")) %> id="lblLifespan" runat="server" />  
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Annual Part Quatity">
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("PartQty")) %> id="lblPartQty" runat="server" />  
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Mechanic Hour">
									<ItemTemplate>
										<asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("MechanicHour")) %> id="lblMechHour" runat="server" />  
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Replacement Schedule by Date" Visible="False">
									<ItemTemplate>
										<asp:label text=<%# objGlobal.GetLongDate(Trim(Container.DataItem("ReplaceDate"))) %> id="lblReplDate" runat="server" />	
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Estimation Date">
									<ItemTemplate>
										<asp:label text=<%# objGlobal.GetLongDate(Container.DataItem("EstimationDate")) %> id="lblReplHM" runat="server" />	
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server CausesValidation=False />
									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
								</asp:TemplateColumn>	
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
					</td>
				</tr>
				
				<tr>
					<td>&nbsp;</td>				
				</tr>									
				<tr>
					<td>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText="  Delete  " imageurl="../../images/butt_delete.gif" onclick="DeleteBtn_Click" runat="server" />
						<asp:ImageButton id=UnDelBtn AlternateText="  Undelete  " imageurl="../../images/butt_undelete.gif" onclick=UnDeleteBtn_Click runat=server  visible=false/>
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=tbcode runat=server />
				<Input type=hidden id=hidBlockCharge value="" runat=server/>
				<Input type=hidden id=hidChargeLocCode value="" runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:label id=lblCloseExist visible=false text="no" runat=server/>
				<asp:label id=lblLifespan visible=false text=0 runat=server/>	
				<asp:label id=lblActHourMeter visible=false text=0 runat=server/>	
                </td>
                </tr>

				<tr>
					<td>
						&nbsp;<asp:Button ID="Issue3" runat="server" class="button-small" Text="Save" />
                        &nbsp;
                        <asp:Button ID="Issue7" runat="server" class="button-small" Text="Delete" />
                        &nbsp;<asp:Button ID="Issue12" runat="server" class="button-small" Text="UnDelete" />
                        &nbsp;&nbsp;&nbsp;<asp:Button ID="Issue9" runat="server" class="button-small" Text="Back" />
					</td>
				</tr>
				
			</table>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
