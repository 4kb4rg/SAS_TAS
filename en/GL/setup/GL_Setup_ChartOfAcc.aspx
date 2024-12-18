<%@ Page Language="vb" src="../../../include/GL_Setup_ChartOfAcc.aspx.vb" Inherits="GL_Setup_ChartOfAcc" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Chart of Account List</title>
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmChartOfAcc class="main-modul-bg-app-list-pu" runat="server">
    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
    		<asp:label id=lblCode visible=false text=" Code" runat=server />
    		<asp:label id=lblType visible=false text=" Type" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border="0" cellspacing="1" cellpadding=1 width="100%"  class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">

				<tr>
					<td colspan="3"><strong><asp:label id="lblTitle" runat="server" /> LIST</strong> </td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan=6 widtha=100% style="background-color:#FFCC00">
                 
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td width="20%" height="26"><asp:label id="lblAccCode" runat="server" /> :<BR>
									<asp:TextBox id=srchAccCode width=100% runat="server" />
								</td>
								<td width="35%" height="26"><asp:label id="lblDescription" runat="server" /> :<BR>
									<asp:TextBox id=srchDescription width=100% maxlength="128" runat="server" />
								</td>
								<td width="15%" height="26">COA Level :<BR>
									<asp:DropDownList id="ddlCOALevel" width=100% runat=server>
										<asp:ListItem value="0" selected>All</asp:ListItem>
										<asp:ListItem value="1">General</asp:ListItem>
										<asp:ListItem value="2">Detail</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="15%" height="26">Status :<BR>
									<asp:DropDownList id="srchStatus" width=100% runat=server>
										<asp:ListItem value="0">All</asp:ListItem>
										<asp:ListItem value="1" selected>Active</asp:ListItem>
										<asp:ListItem value="2">Deleted</asp:ListItem>
									</asp:DropDownList>
								</td>
								<td width="20%" height="26" align=left>Last Updated By :<BR><asp:TextBox id=txtLastUpdate width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click class="button-small" runat="server"/></td>
							</tr>
						</table>
              
					</td>
				</tr>
				<tr>
					<td colspan=6>					
						<asp:DataGrid id=dgLine runat=server
							AutoGenerateColumns=false width=100% 
							GridLines=none 
							Cellpadding=2 
							AllowPaging=True 
							Allowcustompaging=False 
							Pagesize=15 
							OnPageIndexChanged=OnPageChanged 
							Pagerstyle-Visible=False 
							OnDeleteCommand=DEDR_Delete 
							OnSortCommand=Sort_Grid
                            OnItemDataBound="dgLine_BindGrid"
							AllowSorting=True  >
								<HeaderStyle CssClass="mr-h"/>
<ItemStyle CssClass="mr-l"/>
<AlternatingItemStyle CssClass="mr-r"/>
							<Columns>
								<asp:HyperLinkColumn ItemStyle-Width="15%"
                                
									SortExpression="acc.AccCode" 
									DataNavigateUrlField="AccCode" 
									DataNavigateUrlFormatString="GL_Setup_ChartOfAccDet.aspx?tbcode={0}" 
									DataTextField="AccCode"/>
									
								<asp:HyperLinkColumn ItemStyle-Width="35%" 
									SortExpression="acc.Description" 
									DataNavigateUrlField="AccCode" 
									DataNavigateUrlFormatString="GL_Setup_ChartOfAccDet.aspx?tbcode={0}" 
									DataTextField="Description" />
								
								<asp:TemplateColumn HeaderText="COA General" ItemStyle-Width="10%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("COAGeneral") %> id="lblCOAGeneral" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="COA Level" ItemStyle-Width="10%">
									<ItemTemplate>                                      
										<asp:Label Text=<%# Container.DataItem("COALevel") %> id="lblCOALevel" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Last Update" ItemStyle-Width="10%" SortExpression="acc.UpdateDate">
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Status" ItemStyle-Width="10%" SortExpression="acc.Status">
									<ItemTemplate>
										<%# objGLSetup.mtdGetAccStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
									
								<asp:TemplateColumn HeaderText="Updated By" ItemStyle-Width="15%" SortExpression="usr.UserName">
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>										
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-Width="15%" ItemStyle-HorizontalAlign=center>
									<ItemTemplate>
										<asp:Label id=lblLnId Visible=False text=<%# Container.DataItem("AccCode")%> Runat="server" />
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid><BR>
					</td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:ImageButton id="btnPrev" runat="server" 
                            imageurl="../../../images/btprev.png" alternatetext="Previous" 
                            commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  
                            imageurl="../../../images/btnext.png" alternatetext="Next" 
                            commandargument="next" onClick="btnPrevNext_Click" />
			         	<asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
			         	<asp:Label id=lblPageCount visible=false text=1 runat=server/>
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=NewTBBtn onClick=NewTBBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Chart of Account" runat=server/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onclick="btnPreview_Click" runat="server"/>
						<asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" />
						<asp:Label id=SortCol Visible=False Runat="server" />
					    <br />
					</td>
				</tr>
                </table>
                </div>
                </td>
                </tr>
			</table>
		</FORM>
	</body>
</html>
