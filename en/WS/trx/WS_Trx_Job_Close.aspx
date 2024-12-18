<%@ Page Language="vb" trace=False src="../../../include/WS_Trx_Job_Close.aspx.vb" Inherits="WS_JobClose" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWSTrx" src="../../menu/menu_WStrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
    <title>Workshop Job Detailsq</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl id=PrefHdl runat="server" />
</head>

<body>
    <form ID="frmMain" RunAt="Server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


		    <a ID="hlDummy" Style="display:none;">Hyperlink</a>
		    <asp:label ID="lblCode" Visible="False" Text=" Code" RunAt="Server" />
		    <asp:label ID="lblRegCode" Visible="False" Text=" Registration" RunAt="Server" />
			<table ID="tblMain" Border="0" Width="100%" CellSpacing="0" CellPadding="1" RunAt="Server" class="font9Tahoma">
				<tr>
					<td ColSpan="6"><UserControl:MenuWSTrx ID="menuWSTrx" RunAt="Server" /></td>
				</tr>
				<tr>
					<td Class="mt-h" ColSpan="6">WORKSHOP JOB DETAILS (JOB CLOSE)</td>
				</tr>
				<tr>
					<td ColSpan="6"><hr Size="1" NoShade></td>
				</tr>
				<tr>
					<td Width="20%" Height="25">Job ID :</td>
					<td Width="30%"><asp:Label ID="lblJobID" RunAt="Server"/>&nbsp;</td>
					<td Width="5%">&nbsp;</td>
					<td Width="15%">Period :</td>
					<td Width="25%"><asp:Label ID="lblPeriod" RunAt="Server" />&nbsp;</td>
					<td Width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Description :</td>
					<td>					    
					    <asp:Label ID="lblDescription" RunAt="Server"/>					    
					</td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td>
					    <asp:Label ID="lblStatusText" RunAt="Server" />
					    <asp:Label ID="lblStatus" Visible="False" RunAt="Server" />&nbsp;
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Document Ref No :</td>
					<td>					    
					    <asp:Label ID="lblDocRefNo" RunAt="Server"/>					    
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label ID="lblCreateDate" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Document Ref Date :</td>
					<td>
						<asp:Label ID="lblDocRefDate" RunAt="Server"/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label ID="lblUpdateDate" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Job Start Date :</td>
					<td>
						<asp:Label ID="lblJobStartDate" RunAt="Server"/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label ID="lblUserName" RunAt="Server" />&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">Job End Date :</td>
					<td>
						<asp:Label ID="lblJobEndDate" RunAt="Server"/>					    
					</td>					
				</tr>
				<tr>
					<td Height="25">Labour Hourly Rate :</td>
					<td>
						<asp:Label ID="lblChrgRate" RunAt="Server"/>
					</td>					
				</tr>
				<tr>
					<td Height="25">
						<asp:label ID="lblAccCodeTag" Visible="False" RunAt="Server" />
						<asp:label ID="lblEmpCodeTag" Visible="False" Text="Employee Code :" RunAt="Server" />
						<asp:label ID="lblBillPartyCodeTag" Visible="False" Text="Bill Party Code :" RunAt="Server" />
					</td>
					<td>
						<asp:Label ID="lblAccCode" Visible="False" RunAt="Server"/>
						<asp:Label ID="lblEmpCode" Visible="False" RunAt="Server"/>
						<asp:Label ID="lblBillPartyCode" Visible="False" RunAt="Server"/>
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td Height="25">
						<asp:label ID="lblBlkCodeTag" Visible="False" RunAt="Server" />
						<asp:label ID="lblVehRegCodeTag" Visible="False" Text="" RunAt="Server" />
					</td>
					<td>
						<asp:Label ID="lblBlkCode" Visible="False" RunAt="Server"/>
						<asp:Label ID="lblVehRegCode" Visible="False" RunAt="Server"/>
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td Height="25"><asp:label ID="lblVehCodeTag" RunAt="Server" /></td>
					<td>
						<asp:Label ID="lblVehCode" RunAt="Server"/>
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr>
					<td Height="25"><asp:label ID="lblVehExpCodeTag" RunAt="Server" /></td>
					<td>
						<asp:Label ID="lblExpCode" RunAt="Server"/>
					</td>
					<td ColSpan="4">&nbsp;</td>
				</tr>
				<tr>
				    <td ColSpan="6">&nbsp;</td>
				</tr>
				
				<tr ID="trJobStock">
					<td ColSpan="6"> 
						<asp:DataGrid ID="dgJobStock"
							AutoGenerateColumns="False" 
							Width="100%" 
							RunAt="Server"
							GridLines = "None"
							Cellpadding = "2"
							Pagerstyle-Visible="False"							
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
								<asp:TemplateColumn HeaderText="Job Stock ID">
									<ItemStyle Width="6%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("JobStockID")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item Code">
									<ItemStyle Width="12%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("ItemCode")) + " (" + Trim(Container.DataItem("ItemDesc")) + ")" %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Work Code">
									<ItemStyle Width="4%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("WorkCode")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Quantity">
									<ItemStyle  Width="2%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("Qty") - Container.DataItem("QtyReturn")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost" HeaderStyle-HorizontalAlign=Right>
									<ItemStyle HorizontalAlign="Right" Width="6%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("Cost"), 0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cost Amount" HeaderStyle-HorizontalAlign=Right>
									<ItemStyle HorizontalAlign="Right" Width="8%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("Amount") - Container.DataItem("AmountReturn"),0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Price" HeaderStyle-HorizontalAlign=Right>
									<ItemStyle HorizontalAlign="Right" Width="6%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("Price"), 0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Price Amount" HeaderStyle-HorizontalAlign=Right>
									<ItemStyle HorizontalAlign="Right" Width="8%" />
									<ItemTemplate>
										<%# objGlobal.GetIDDecimalSeparator(FormatNumber(RoundNumber(Container.DataItem("PriceAmount") - Container.DataItem("PriceAmountReturn"),0), 0, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>								
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
				    <td ColSpan="6">
				        <table Width="100%" CellSpacing="0" CellPadding="2" Border="0" class="font9Tahoma">
							<tr>
							    <td Width="39%">&nbsp;</td>
							    <td Width="10%"><hr></td>
							    <td Width="17%">&nbsp;</td>
							    <td Width="10%"><hr></td>
							    <td Width="15%">&nbsp;</td>
							    <td Width="15%"><hr></td>
							</tr>
					    </table>
				    </td>
				</tr>				
				<tr>
					<td ColSpan="6">
						<table Width="100%" CellSpacing="0" CellPadding="2" Border="0" class="font9Tahoma">
							<tr>
							   <td Width="37%">Total :</td>
							    <td Width="10%" Align="Right"><asp:Label ID="lblTotalQty" RunAt="Server"/></td>
							    <td Width="17%">&nbsp;</td>
							    <td Width="10%" Align="Right"><asp:Label ID="lblTotalCostAmt" RunAt="Server"/></td>
							    <td Width="20%">&nbsp;</td>
							    <td Width="15%" Align="Right"><asp:Label ID="lblTotalPriceAmt" RunAt="Server"/></td>
							</tr>
					    </table>
					</td>
				</tr>
				<tr>
				    <td ColSpan="6">
				        <table Width="100%" CellSpacing="0" CellPadding="2" Border="0">
							<tr>
							    <td Width="39%">&nbsp;</td>
							    <td Width="10%"><hr></td>
							    <td Width="17%">&nbsp;</td>
							    <td Width="10%"><hr></td>
							    <td Width="15%">&nbsp;</td>
							    <td Width="15%"><hr></td>
							</tr>
					    </table>
				    </td>
				</tr>
				<tr ID="trMechHour">
					<td ColSpan="6"> 
						<asp:DataGrid ID="dgMechHour"
							AutoGenerateColumns="False" 
							Width="100%" 
							RunAt="Server"
							GridLines = "None"
							Cellpadding = "2"
							Pagerstyle-Visible="False"							
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
								<asp:TemplateColumn HeaderText="Working Date">
									<ItemStyle Width="7%" />
									<ItemTemplate>
										<%# Trim(FormatDate(Container.DataItem("WorkingDate"),2)) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Employee Code (Mechanic)">
									<ItemStyle Width="20%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("EmpCode")) + " (" + Trim(Container.DataItem("EmpName")) +  ")"%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Work Code">
									<ItemStyle Width="15%" />
									<ItemTemplate>
										<%# Trim(Container.DataItem("WorkCode")) + " (" + Trim(Container.DataItem("WorkDesc")) +  ")"%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Time Spent (Hours : Minutes)" HeaderStyle-HorizontalAlign=Center>
									<ItemStyle Width="7%" HorizontalAlign="Center"/>
									<ItemTemplate>
										<%# Trim(Format$(CInt(Container.DataItem("HourSpent")), "00")) + ":" + Trim(Container.DataItem("MinuteSpent")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Amount" HeaderStyle-HorizontalAlign=Right>
									<ItemStyle Width="12%" HorizontalAlign="Right" />
									<ItemTemplate>
										<%# Trim(FormatNumber(RoundNumber(Container.DataItem("OriginalAmount"), 2),  2, True, False, False)) %>
									</ItemTemplate>
								</asp:TemplateColumn>								
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
				    <td ColSpan="6">
				        <table Width="100%" CellSpacing="0" CellPadding="2" Border="0">
							<tr>
							    <td Width="45%">&nbsp;</td>
							    <td Width="21%">&nbsp;</td>
							    <td Width="18%"><hr></td>
							    <td Width="25%"><hr></td>
							</tr>
					    </table>
				    </td>
				</tr>				
				<tr>
					<td ColSpan="6">
						<table Width="100%" CellSpacing="0" CellPadding="2" Border="0" class="font9Tahoma">
							<tr>
							   <td Width="34%">Total Labour Charges :</td>
							    <td Width="20%"></td>
							    <td Width="10%">&nbsp;</td>
							    <td Width="20%" Align="Center"><asp:Label ID="lblTotalMercTime" RunAt="Server"/></td>
							    <td Width="15%" Align="Right"><asp:Label ID="lblTotalMechAmt" RunAt="Server"/></td>
							</tr>
					    </table>
					</td>
				</tr>
				<tr>
				    <td ColSpan="6">
				        <table Width="100%" CellSpacing="0" CellPadding="2" Border="0" class="font9Tahoma">
							<tr>
							    <td Width="45%">&nbsp;</td>
							    <td Width="21%">&nbsp;</td>
							    <td Width="18%"><hr></td>
							    <td Width="25%"><hr></td>
							</tr>
					    </table>
				    </td>
				</tr>
				<tr>
				    <td ColSpan="6">&nbsp;</td>
				</tr>
				<tr>
				    <td ColSpan="6">Remark : <asp:Label ID="lblJobRemarks" RunAt="Server"/></td>				    
				</tr>
				<tr>
				    <td ColSpan="6">
								<asp:label id=lblBPErr text="Unable to generate Debit Note, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
								
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan="6">
						<asp:ImageButton ID="ibClose" AlternateText="Back" OnClick="ibClose_OnClick" ImageURL="../../images/butt_close.gif" CausesValidation="False" RunAt="Server" />
						<asp:ImageButton ID="ibBack" AlternateText="Back" OnClick="ibBack_OnClick" ImageURL="../../images/butt_back.gif" CausesValidation="False" RunAt="Server" />
					</td>
				</tr>				
				<tr>
					<td align="left" ColSpan="6">
                                            &nbsp;</td>
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
