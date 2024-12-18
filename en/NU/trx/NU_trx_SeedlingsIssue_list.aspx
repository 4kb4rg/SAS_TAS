<%@ Page Language="vb" src="../../../include/NU_trx_SeedlingsIssue_list.aspx.vb" Inherits="NU_trx_SeedlingsIssueList" %>
<%@ Register TagPrefix="UserControl" TagName="MenuNUTrx" src="../../menu/menu_NUtrx.ascx"%>
<%@ Register TagPrefix="Preference" TagName="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>NURSERY - Seedlings Issue List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl ID="PrefHdl" Runat="Server" />
	</head>
	<body>
	    <form ID="frmTrx" class="main-modul-bg-app-list-pu" Runat="Server" >
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma">
			    <div class="kontenlist">  

			<asp:Label ID="lblErrMessage" Visible="False" Text="Error while initiating component." Runat="Server" />
			<asp:Label ID="lblOrderBy" Visible="False" Runat="Server" />
			<asp:Label ID="lblOrderDir" Visible="False" Runat="Server" />
			<asp:Label ID="lblCode" Visible="False" Text=" Code" Runat="Server" />
			<table Border="0" CellSpacing="1" CellPadding="1" Width="100%" class="font9Tahoma">
				<tr>
					<td ColSpan=6><UserControl:MenuNUTrx ID="MenuNUTrx" Runat="Server" /></td>
				</tr>
				<tr>
					<td  ColSpan=3><strong> SEEDLINGS ISSUE LIST</strong></td>
					<td ColSpan="3" Align="Right"><asp:Label ID="lblTracker" Runat="Server" /></td>
				</tr>
				<tr>
					<td ColSpan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr style="background-color:#FFCC00" >>
					<td ColSpan="6" Width="100%" Class="mb-c">
						<table Width="100%" CellSpacing="0" CellPadding="3" Border="0" Align="Center" class="font9Tahoma">
							<tr>
								<td Width="20%" Height="26" VAlign="Bottom">Issue ID :<br><asp:TextBox ID="txtSearchIssueID" Width="100%" MaxLength="20" Runat="Server" /></td>
								<td Width="20%" Height="26" VAlign="Bottom">Document Ref. No. :<br><asp:TextBox ID="txtSearchDocRefNo" Width="100%" MaxLength="32" Runat="Server" /></td>
								<td Width="15%" Height="26" VAlign="Bottom"><asp:Label ID="lblNurseryBlockTag" Runat="Server" /> :<br><asp:TextBox ID="txtSearchBlkCode" Width="100%" MaxLength="8" Runat="Server" /></td>
								<td Width="15%" Height="26" VAlign="Bottom">Status :<br><asp:DropDownList ID="ddlSearchStatus" Width="100%" Runat="Server" /></td>
								<td Width="20%" Height="26" VAlign="Bottom">Last Updated By :<br><asp:TextBox ID="txtSearchUpdateBy" Width="100%" MaxLength="128" Runat="Server" /></td>
								<td Width="10%" Height="26" VAlign="Bottom" Align="Right"><asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_OnClick" Runat="Server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td ColSpan="6">
						<asp:DataGrid ID="dgTxList"
							AutoGenerateColumns="False"
							Width="100%"
							GridLines="None"
							CellPadding="2"
							AllowPaging="True"
							AllowCustomPaging="False"
							AllowSorting="True"
							PageSize="15"
							Pagerstyle-Visible="False"
							OnItemDataBound="dgTxList_OnItemDataBound"
							OnPageIndexChanged="dgTxList_OnPageIndexChanged"
							OnDeleteCommand="dgTxList_OnDeleteCommand"
							OnSortCommand="dgTxList_OnSortCommand"
							Runat="Server" CssClass="font9Tahoma" >
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
								<asp:HyperLinkColumn
									HeaderText="Issue ID"
									SortExpression="SI.IssueID"
									DataNavigateUrlField="IssueID"
									DataNavigateUrlFormatString="NU_trx_SeedlingsIssue_details.aspx?ID={0}"
									DataTextField="IssueID"
									DataTextFormatString="{0:c}"
									ItemStyle-Width= "10%" />
								<asp:TemplateColumn HeaderText="Document Ref. No." SortExpression="SI.DocRefNo">
								    <ItemStyle Width="15%" />
									<ItemTemplate>
										<%# Container.DataItem("DocRefNo") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Issue Date" SortExpression="SI.IssueDate">
								    <ItemStyle Width="10%" />
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("IssueDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nursery Block" SortExpression="SI.BlkCode">
								    <ItemStyle Width="10%" />
									<ItemTemplate>
										<%# Container.DataItem("BlkCode") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Period" SortExpression="SI.AccYear, SI.AccMonth">
								    <ItemStyle Width="10%" />
									<ItemTemplate>
										<%# Container.DataItem("AccMonth").Trim & "/" & Container.DataItem("AccYear").Trim %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Last Update" SortExpression="SI.UpdateDate">
								    <ItemStyle Width="10%" />
									<ItemTemplate>
										<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status" SortExpression="SI.Status">
								    <ItemStyle Width="10%" />
									<ItemTemplate>
										<%# objNUTrx.mtdGetSeedlingsIssueStatus(Container.DataItem("Status")) %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Updated By" SortExpression="SI.UpdateID">
								    <ItemStyle Width="15%" />
									<ItemTemplate>
										<%# Container.DataItem("UserName") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="" >
									<ItemStyle Width="5%" HorizontalAlign="Right" />
									<ItemTemplate>
										<asp:LinkButton ID="lbDelete" CommandName="Delete" Text="Delete" Runat="Server" />
										<asp:Label ID="lblIssueID" Text='<%# Container.DataItem("IssueID") %>' Visible="False" Runat="Server" />
										<asp:Label ID="lblStatusCode" Text='<%# Container.DataItem("Status") %>' Visible="False" Runat="Server" />
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
						<br><asp:Label ID="lblActionResult" Text="Insufficient Quantity to Perform Operation!" Visible="False" ForeColor="Red" Runat="Server" />
					</td>
				</tr>
				<tr>
					<td Align="Right" ColSpan="6">
						<asp:ImageButton ID="btnPrev" ImageURL="../../images/icn_prev.gif" AlternateText="Previous" CommandArgument="prev" OnClick="ibPrevNext_Click" Runat="Server" />
						<asp:DropDownList ID="ddlPage" AutoPostBack="True" OnSelectedIndexChanged="ddlPage_OnSelectedIndexChanged" Runat="Server" />
			         	<asp:Imagebutton ID="btnNext" ImageURL="../../images/icn_next.gif" AlternateText="Next" CommandArgument="next" OnClick="ibPrevNext_Click" Runat="Server" />
					</td>
				</tr>
				<tr>
					<td Align="Left" Width="100%" ColSpan="6">
						<asp:ImageButton ID="ibNew" OnClick="ibNew_OnClick" ImageURL="../../images/butt_new.gif" AlternateText="New" Runat="Server" />
						<asp:ImageButton ID="ibPrint" ImageURL="../../images/butt_print.gif" AlternateText="Print" Visible="False" Runat="Server" />
					    <br />
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