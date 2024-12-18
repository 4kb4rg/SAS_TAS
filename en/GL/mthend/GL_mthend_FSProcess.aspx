<%@ Page Language="vb" codefile="../../../include/GL_mthend_FSProcess.aspx.vb" Inherits="GL_mthend_FSProcess" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLMthEnd" src="../../menu/menu_GLMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>
    
    
<html>
	<head>
		<title>Financial Statement Process</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain runat=server>

        <div class="kontenlist">
	 

		<input type=hidden id=hidCntLoc runat=server />
		<input type=hidden id=hidLoc1 runat=server />
		<input type=hidden id=hidLoc2 runat=server />
		<input type=hidden id=hidLoc3 runat=server />
		<input type=hidden id=hidLoc4 runat=server />
		<input type=hidden id=hidLoc5 runat=server />
		<input type=hidden id=hidLoc6 runat=server />
		<input type=hidden id=hidLoc7 runat=server />
		<input type=hidden id=hidLoc8 runat=server />
		<input type=hidden id=hidLoc9 runat=server />
		<input type=hidden id=hidMethod runat=server />
				
            
		<table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma">
			<tr>
				<td colspan=5 align=center><UserControl:MenuGLMthEnd id=MenuGLMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>
                  <strong>  FINANCIAL STATEMENT PROCESS</strong></td>
			</tr>
			<tr>
				<td colspan=5>
                         <hr style="width :100%" />
					</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red></font><p>
                        Process will generate financial statement on
                        selected periode:</td>
			</tr>
			<tr>
				<td colspan=5 style="height: 25px">&nbsp;</td>
			</tr>
			<tr>
				<td width=40% height=25>Accounting Period To Be Processed :</td> 
				<td width=30%>
					<asp:DropDownList id=ddlAccMonth runat=server/> / 
					<asp:DropDownList id=ddlAccYear OnSelectedIndexChanged=OnIndexChage_ReloadAccPeriod AutoPostBack=True runat=server />
				</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=10%>&nbsp;</td>
			</tr>
			<tr>
                <td height=25>Calculating Method :</td>
                <td width=5%>
                <asp:radiobutton GroupName="rbMethod" id="rbMethod1" text=" Direct Value (from TB Amount)" checked="true" runat="server" /></td>
                <td width=15%>&nbsp;</td>
				<td width=10%>&nbsp;</td>
            </tr>
            <tr runat=server visible=false>
                <td height=25>&nbsp;</td>
                <td width=5%>
                <asp:radiobutton GroupName="rbMethod" id="rbMethod2" text=" Indirect Value (recalculating)" Checked="false" runat="server" /></td>
                <td width=15%>&nbsp;</td>
				<td width=10%>&nbsp;</td>
            </tr>
			<tr>
				<td colspan=5 height=25>
                    <asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing financial statement." runat=server/>
				</td>
			</tr>
			<tr>
				<td colspan=5>
				  
                    <asp:Button Text="Process"  id="btnProceed" OnClick="btnProceed_Click" runat="server" class="button-small"/>
                    <asp:Button Text="Preview"  id="btnPreview" onclick="PreviewBtn_Click"   runat="server" class="button-small"/>
				</td>
			</tr>
 
			<tr>
				<td colspan=5>
                         <hr style="width :100%" />
					</td>
			</tr>
			
            <tr>
                <td style="height: 24px; " colspan="5">
                    <igtab:UltraWebTab ID="TABBK" Visible="false" ThreeDEffect="False" TabOrientation=TopLeft 
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor=black runat="server">
                        <DefaultTabStyle Height="22px">
                        </DefaultTabStyle>
                        <HoverTabStyle CssClass="ContentTabHover">
                        </HoverTabStyle>
                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                        <SelectedTabStyle CssClass="ContentTabSelected">
                        </SelectedTabStyle>
                        <Tabs>
                            
                            <%--COGS--%>
                            <igtab:Tab Key="COGS" Text="COGS" Tooltip="COGS">
                                <ContentPane>
                                    <table border="0" cellspacing="1" cellpadding="1" width="73%" class="font9Tahoma">
                                        <tr>
                                            <td colspan="3">
                                                <asp:RadioButton ID="optGroup" Text="Group Display Based on Template "  AutoPostBack="true" OnCheckedChanged="optGroup_CheckedChanged"   runat="server" />                       
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">                        
                                                <asp:RadioButton ID="optExpand" Text="Expand Display Based on Account "   AutoPostBack="true"  OnCheckedChanged="optExpand_CheckedChanged"   runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <div id="div6" style="height: 500px;width:1020;overflow: auto;">	

                                                    <asp:DataGrid ID="dgCOGS" runat="server" AutoGenerateColumns="False" CssClass="font9Tahoma" GridLines="Both"
                                                        CellPadding="2"   Width="100%">
                                                        <AlternatingItemStyle CssClass="mr-l" />
                                                        <ItemStyle CssClass="mr-l" />
                                                        <HeaderStyle CssClass="mr-h" />
                                                        <Columns>
                                                           <asp:TemplateColumn Visible="false" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>                                                                
                                                                    <asp:Label ID="lblrowid" Visible=false Text='<%# Container.DataItem("RowID") %>' runat="server" />                                                                    
                                                                    <asp:Label ID="lblFBold" Visible=false Text='<%# Container.DataItem("FBold") %>' runat="server" />
                                                                    <asp:Label ID="lblSpace" Visible=false Text='<%# Container.DataItem("FSpace") %>' runat="server" />
                                                                    <asp:Label ID="lblUnderLine" Visible=false Text='<%# Container.DataItem("FUnderline") %>' runat="server" />
                                                                    <asp:Label ID="lblFont" Visible=false Text='<%# Container.DataItem("FFont") %>' runat="server" />                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="DESCRIPTION" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="60%" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescription" Text='<%# Container.DataItem("Description") %>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>

                                                            <asp:TemplateColumn  HeaderText="REF. NO" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign=Right ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRefFNo"  runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn> 
 
                                                             <asp:TemplateColumn  HeaderText="Qty" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" Text='<%# FormatNumber(Container.DataItem("Qty"), 0)%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn> 

                                                            <asp:TemplateColumn  HeaderText="AMOUNT" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotBlnIniLrNew" Text='<%# FormatNumber(Container.DataItem("Amount"), 0)%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn> 

                                                            <asp:TemplateColumn  HeaderText="COST" HeaderStyle-Font-Bold=true HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotalcost" Text='<%# FormatNumber(Container.DataItem("Cost"), 0)%>' runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn> 

                                                        </Columns>
                                                    </asp:DataGrid>
                                                    
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
				                            <td colspan=5>&nbsp;</td>
			                            </tr>
			                           
			                            <tr>
				                            <td colspan=5>&nbsp;</td>
			                            </tr>
                                        <tr>
                                            <td height="25" style="width: 185px">
                                                
                                            <td style="width: 378px">
                                                &nbsp;</td>
                                            <td style="width: 29px">
                                                &nbsp;</td>
                                            <td style="width: 192px">
                                                &nbsp;</td>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </ContentPane>
                            </igtab:Tab>
                             
                                                       
                            
                        </Tabs>
                    </igtab:UltraWebTab>
                    
                </td>
            </tr>
			
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			
			
		</table>

        </div>
		</form>
	</body>
</html>
