<%@ Page Language="vb" src="../../../include/FA_Setup_AssetMaster.aspx.vb" Inherits="FA_AssetMaster" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFASetup" src="../../menu/menu_FAsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Asset Master List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"/>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server"/>
			<asp:label id="blnUpdate" Visible="False" Runat="server"/>
			<asp:label id="curStatus" Visible="False" Runat="server"/>
			<asp:label id="sortcol" Visible="False" Runat="server"/>
			<asp:Label id="ErrorMessage" runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuFASetup id=menuFA runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong><asp:label id="lblTitle" runat="server" /> LIST</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="20%" height="26" valign=bottom>
									<asp:label id="lblDCCode" runat="server" /> :<BR>
									<asp:TextBox id=srchDCCode width=100% maxlength="8" runat="server"/>
								</td>
								<td width="42%" height="26" valign=bottom>
									<asp:label id="lblDescription" runat="server" /> :<BR>
									<asp:TextBox id=srchDesc width=100% maxlength="128" runat="server"/>
								</td>
								<td width="10%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="8%" height="26" valign=bottom align=right><asp:Button ID="Button1"  Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="EventData"
						                AutoGenerateColumns="false" width="100%" runat="server"
						                GridLines = none
						                Cellpadding = "2"
						                AllowPaging="True" 
						                Allowcustompaging="False"
						                Pagesize="15" 
						                OnPageIndexChanged="OnPageChanged"
						                Pagerstyle-Visible="False"
						                OnSortCommand="Sort_Grid" 
						                AllowSorting="True"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
						                <Columns>
							                <asp:HyperLinkColumn
									                DataNavigateUrlField="ItemCode"
									                DataNavigateUrlFormatString="FA_Setup_AssetMasterDetails.aspx?id={0}"
									                DataTextField="ItemCode"
									                DataTextFormatString="{0:c}"
									                SortExpression="ItemCode"/>

							                <asp:HyperLinkColumn
									                DataNavigateUrlField="ItemCode"
									                DataNavigateUrlFormatString="FA_Setup_AssetMasterDetails.aspx?id={0}"
									                DataTextField="Description"
									                DataTextFormatString="{0:c}"
									                SortExpression="Description"/>
							
							                <asp:TemplateColumn HeaderText="Last Update" SortExpression="itm.UpdateDate">
								                <ItemTemplate>
									                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Status" SortExpression="itm.Status">
								                <ItemTemplate>
									                <%# objIN.mtdGetStockItemStatus(Container.DataItem("Status")) %>
								                </ItemTemplate>
							                </asp:TemplateColumn>
							                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
								                <ItemTemplate>
									                <%# Container.DataItem("UserName") %>
								                </ItemTemplate>
							                </asp:TemplateColumn>
						                </Columns>
					                </asp:DataGrid><BR>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
							<td>
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td style="width: 100%">&nbsp;</td>
									<td><img height="18px" src="../../../images/btfirst.png" width="18px" class="button" /></td>
									<td><asp:ImageButton ID="btnPrev" runat="server" alternatetext="Previous" commandargument="prev" imageurl="../../../images/btprev.png" onClick="btnPrevNext_Click" /></td>
									<td><asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=ibNew OnClick="btnNewItm_Click" imageurl="../../images/butt_new.gif" AlternateText="New Fixed Asset Master" runat="server"/>
						        <asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print onClick="btnPreview_Click" runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
                        </tr>
					</table>
				</div>
				</td>
		        <table cellpadding="0" cellspacing="0" style="width: 20px">
			        <tr>
				        <td>&nbsp;</td>
			        </tr>
		        </table>
				</td>
			</tr>
		</table>


			</FORM>
		</body>
</html>
