<%@ Page Language="vb" src="../../../include/HR_Setup_SalScheme.aspx.vb" Inherits="HR_Setup_SalScheme" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Salary Scheme List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form runat="server" class="main-modul-bg-app-list-pu">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />

			
                <igtab:UltraWebTab ID="UltraWebTab1" AutoPostBack=True  runat="server" Height="100%" Width="100%" ThreeDEffect="False" Font-Names="Tahoma" Font-Size="8pt" Font-Bold="True">
                 <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                <DefaultTabStyle ForeColor="Black" Height="24px"></DefaultTabStyle>
                <HoverTabStyle ForeColor="Green" ></HoverTabStyle>
                <SelectedTabStyle ForeColor="White"></SelectedTabStyle>
                    <Tabs>
                    <igtab:Tab Text="Kode Perusahaan">

                    </igtab:Tab>
                    
                    <igtab:Tab Text="Kode Unit Kerja">

                    </igtab:Tab>
                    
                    <igtab:Tab Text="Kode Lokasi">

                    </igtab:Tab>
                    
                    <igtab:Tab Text="Kode Status Karyawan">
                     <ContentTemplate>
                         <table border="0" cellspacing="1" cellpadding="1" width="100%">
				<tr>
					<td colspan="6"><UserControl:MenuHRSetup id=MenuHRSetup runat="server" /></td>
				</tr>
				<tr>
                    <td><table width="100%" cellspacing="0" cellpadding="3" border="0">
					<comment>Modified By BHL</comment>
					<td class="mt-h" colspan="4" width="60%">EMPLOYEE CATEGORY LIST</td>
					<td align="right" colspan="2" width="40%"><asp:label id="lblTracker" runat="server"/></td>
                    </table></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="mb-c">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center">
							<tr class="font9Tahoma" style="background-color:#FFCC00">
								<td width="25%" height="26" valign=bottom>Employee Category Code :<BR><asp:TextBox id=srchSalSchemeCode width=100% maxlength="8" runat="server"/></td>
								<td width="30%" height="26" valign=bottom>Description :<BR><asp:TextBox id=srchDesc width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
                    <td><table width="100%" cellspacing="0" cellpadding="3" border="0">

					<TD colspan = 7 >					
					<asp:DataGrid id="EventData"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnEditCommand="DEDR_Edit"
						OnUpdateCommand="DEDR_Update"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete"
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
					<asp:TemplateColumn HeaderText="Employee Category Code" SortExpression="SalSchemeCode">
						<ItemTemplate>
							<%# Container.DataItem("SalSchemeCode") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="SalSchemeCode" MaxLength="8" width=80%
								Text='<%# trim(Container.DataItem("SalSchemeCode")) %>' runat="server"/><BR>
							<asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								ErrorMessage="Please Enter Employee Category Code"
								ControlToValidate=SalSchemeCode />
							<asp:RegularExpressionValidator id=revCode 
								ControlToValidate="SalSchemeCode"
								ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								Display="Dynamic"
								text="Alphanumeric without any space in between only."
								runat="server"/>
							<asp:label id="lblDupMsg" Text="Code already exist" Visible=false forecolor=red Runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>	
					<asp:TemplateColumn HeaderText="Description" SortExpression="Description">
						<ItemTemplate>
							<%# Container.DataItem("Description") %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox id="Description" width=100% MaxLength="64"
								Text='<%# trim(Container.DataItem("Description")) %>' runat="server"/>
							<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								ErrorMessage="Please Enter Salary Scheme Description"
								ControlToValidate=Description />
						</EditItemTemplate>
					</asp:TemplateColumn>										
					<asp:TemplateColumn HeaderText="Category Type" SortExpression="CategoryTypeInd">						
						<ItemTemplate>
							<%# objHR.mtdGetCategoryType(trim(Container.DataItem("CategoryTypeInd"))) %>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:DropDownList id="ddlType" width=100% AutoPostBack=true runat=server>
							</asp:DropDownList>
							<asp:Label id=lblErrType visible=false forecolor=red runat=server/>	
							<asp:label id=lblTypeCode visible=false text='<%# trim(Container.DataItem("CategoryTypeInd")) %>' runat=server/>						
						</EditItemTemplate>						
					</asp:TemplateColumn>					

					<asp:TemplateColumn HeaderText="Position" SortExpression="PositionInd">						
						<ItemTemplate>
							<%# objHR.mtdGetPosition(trim(Container.DataItem("PositionInd"))) %>
						</ItemTemplate>
						<EditItemTemplate>													
							<asp:DropDownList id="ddlPosition" width=100% AutoPostBack=true runat=server>
							</asp:DropDownList>
							<asp:Label id=lblErrPosition visible=false forecolor=red runat=server/>	
							<asp:label id=lblPositionCode visible=false text='<%# trim(Container.DataItem("PositionInd")) %>' runat=server/>						
						</EditItemTemplate>						
					</asp:TemplateColumn>					

					<asp:TemplateColumn HeaderText="Last Update" SortExpression="SAL.UpdateDate">
						<ItemTemplate>
							<%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UpdateDate" Readonly=TRUE size=8 
								Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>' runat="server"/>
							<asp:TextBox id="CreateDate" Visible=False
								Text='<%# Container.DataItem("CreateDate") %>' runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status" SortExpression="SAL.Status">
						<ItemTemplate>
							<%# objHR.mtdGetFunctionStatus(Container.DataItem("Status")) %>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							<asp:TextBox id="Status" Visible=False
								Text='<%# Container.DataItem("Status")%>' runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						<ItemTemplate>
							<%# Container.DataItem("UserName") %>
						</ItemTemplate>
						<EditItemTemplate >
							<asp:TextBox id="UserName" Readonly=TRUE size=8 
								Text='<%# Session("SS_USERID") %>' Visible=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>					
					<asp:TemplateColumn>					
						<ItemTemplate>
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					</Columns>
					</asp:DataGrid><BR>
					</td>
                     </table></td>
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
						<asp:Label id=lblErrValidation  visible=false Text="Category Type and Position already saved in database." forecolor=red runat=server/>
					</td>	
				</tr>
				<tr>
					<td align="left" ColSpan=6>
						<asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Employee Category" runat="server"/>
						<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
					</td>
				</tr>
			</table>
	                 </ContentTemplate>		
                    </igtab:Tab>
                    
                    </Tabs>
                </igtab:UltraWebTab>
			</Form>
		</body>
</html>
