<%@ Page Language="vb" src="../../../include/HR_setup_BilyetGiro.aspx.vb" Inherits="HR_setup_BilyetGiro" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_glsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
    
<html>
	<head>
		<title>Cheque & Bilyet Giro</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	</head>
	<body>
		<form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">  
		            <table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma" >
                        		
    		<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<asp:Label id=SortCol Visible=False Runat="server" />
			<table border="0" cellspacing="1" cellpadding=1 width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
                    <td><table width="100%">
                    <td><strong>CHEQUE & BILYET GIRO</strong>   
					<td colspan="3" align=right><asp:label id="lblTracker" Visible=false runat="server"/></td>
                    </table></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
                
                <tr>
                    <td colspan=6 width=100% class="font9Tahoma">
                        <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                            SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" runat="server">
                            <DefaultTabStyle ForeColor="black" Height="22px">
                            </DefaultTabStyle>
                            <HoverTabStyle CssClass="ContentTabHover">
                            </HoverTabStyle>
                            <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                                NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                                FillStyle="LeftMergedWithCenter"></RoundedImage>
                            <SelectedTabStyle CssClass="ContentTabSelected">
                            </SelectedTabStyle>
                            <Tabs>
                               <%--Generate Sequence--%>
                                <igtab:Tab Key="TAB2" Text="Generate Sequence" Tooltip="Generate Sequence Number">
                                    <ContentPane>
                                        <table border="0" cellspacing="1" cellpadding="1" width="99%">
                                            <tr>
                                                <td colspan="5">
                                                    <div id="div2" style="height: 350px;width:1020;">		
                                                        <table id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font12Tahoma" runat=server>
                                                            <tr>
				                                                <td colspan=5 height=25>
					                                                <font color=red>Important notes :</font><p>
					                                                - Click REFRESH for review all cheque/bilyet giro number that had created.<br>
					                                                - Click GENERATE to process sequence cheque/bilyet giro number on temporary.<br>
					                                                - Click POST to post sequence cheque/bilyet giro number had created on temporary.<br>
					                                            </td>
			                                                </tr>
			                                                <tr>
			                                                    <td colspan=5 height=25>&nbsp;</td>
		                                                    </tr>
                                                            <tr>
                                                                <td width="15%" height=25>Period : </td>
                                                                <td colSpan="5">
                                                                    <asp:DropDownList id="ddlMonth" width=10% runat=server>
                                                                        <asp:ListItem value="0" Selected>All</asp:ListItem>
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
                                                                    </asp:DropDownList>
                                                                    &nbsp;
                                                                    <asp:DropDownList id=ddlYear width="10%" maxlength="4" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" height=25>Effective Date : </td>
                                                                <td colSpan="5"><asp:TextBox id=txtEffDate width=10% maxlength="10" runat="server"/>
					                                                <asp:RequiredFieldValidator	id="rfvtEffDate" runat="server"  ControlToValidate="txtEffDate" text = "Please enter effective date" display="dynamic"/>
					                                                <asp:label id=lblDate Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					                                                <asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/> 
				                                                </td>
                                                            </tr>
                                                            <tr>
				                                                <td height=25 width="20%">Bank </td>
                                                                <td><asp:DropDownList width="50%" id=ddlBank runat=server />
                                                                    <asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/>&nbsp;</td>
			                                                </tr>
			                                                <tr>
                                                                <td width="15%" height=25>Type : </td>
                                                                <td colSpan="5">
                                                                    <asp:DropDownList id="ddlDocType" width=21% runat=server>
                                                                        <asp:ListItem value="0">Cheque</asp:ListItem>
                                                                        <asp:ListItem value="3">Bilyet Giro</asp:ListItem>										
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
			                                                <tr>
                                                                <td width="15%" height=25>Prefix : </td>
                                                                <td colSpan="5"><asp:TextBox id=txtPrefix width=10% runat=server />
                                                                                <asp:Label id=lblErrPrefix visible=false forecolor=red text="Prefix cannot be empty." runat=server />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="15%" height=25>Sequence From : </td>
                                                                <td colSpan="5"><asp:TextBox id=txtNoFrom width=10% maxlength=6 runat=server />
																        		-
																                <asp:TextBox id=txtNoTo width=10% maxlength=6 runat=server />
                                                                                <asp:Label id=lblErrSequence visible=false forecolor=red runat=server />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td height=25 colspan=2><asp:Label id=lblErrMessageNo visible=false Text="Error while initiating component." ForeColor=red runat=server /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colSpan="5">&nbsp;</td>
                                                            </tr>	
                                                            <tr>
                                                                <td height=25 colspan=5>
                                                                    <asp:ImageButton id=RefreshNoBtn ToolTip="Refresh" imageurl="../../images/butt_refresh.gif" AlternateText="Refresh" OnClick="RefreshNoBtn_Click" runat="server" />
                                                                    <asp:ImageButton id=GenerateNoBtn ToolTip="Generate Number" imageurl="../../images/butt_generate.gif" alternatetext=Add CausesValidation=True onclick=GenerateNoBtn_Click UseSubmitBehavior="false" runat=server /> 					<asp:ImageButton ID=PostingNoBtn ToolTip="Posting Number" UseSubmitBehavior="false" AlternateText="Posting" onclick="PostingNoBtn_Click"  ImageUrl="../../images/butt_post.gif"  CausesValidation=False Runat=server />
																	<asp:ImageButton id=RollBackNoBtn ToolTip="Rollback Number" onclick=RollBackNoBtn_Click imageurl="../../images/butt_rollback.gif" alternatetext="Rollback" runat=server />
                                                                </td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                            </tr>
                                                            
                                                        </table>
                                                    </div>                                                    
                                                    <div id="div3" style="height: 300px;width:1000;overflow: auto;">
                                                        <asp:DataGrid id=dgLineGen
	                                                        AutoGenerateColumns="false" width="100%" runat="server"
	                                                        GridLines=none
	                                                        Cellpadding="2"
	                                                        Pagerstyle-Visible="False"
	                                                        AllowSorting="True">
	                                                        <HeaderStyle CssClass="mr-h"/>
	                                                        <ItemStyle CssClass="mr-l"/>
	                                                        <AlternatingItemStyle CssClass="mr-r"/>
	                                                        <Columns>	
	                                                            <asp:TemplateColumn HeaderText="Period" ItemStyle-Width="3%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# trim(Container.DataItem("AccMonth"))+"/"+trim(Container.DataItem("AccYear")) %> id="lblFPDate" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Effective Date" ItemStyle-Width="5%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# objGlobal.GetLongDate(Container.DataItem("EffDate")) %> id="lblEffDate" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Bank" ItemStyle-Width="10%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# Container.DataItem("BankDescr") %> id="lblBank" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Type" ItemStyle-Width="5%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# Container.DataItem("DocDescr") %> id="lblDocType" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                        <asp:TemplateColumn HeaderText="Cheque/Giro No" ItemStyle-Width="7%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# Container.DataItem("DocNo") %> id="lblDocNo" runat="server" /><br />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>
		                                                       	                         
		                                                       	<asp:TemplateColumn HeaderText="Doc ID" ItemStyle-Width="7%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# Container.DataItem("DocID") %> id="lblDocID" runat="server" /><br />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>     
		                                                        <asp:TemplateColumn HeaderText="Doc Date" ItemStyle-Width="7%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# objGlobal.GetLongDate(Container.DataItem("DocDate")) %> id="lblDocDate" runat="server" />
				                                                    </ItemTemplate>
		                                                        </asp:TemplateColumn>     
		                                                        <asp:TemplateColumn HeaderText="Cheque/Giro Date" ItemStyle-Width="7%" >
			                                                        <ItemTemplate>
				                                                        <asp:Label visible=true Text=<%# objGlobal.GetLongDate(Container.DataItem("GiroDate")) %> id="lblGiroDate" runat="server" />
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
                                        </table>
                                    </ContentPane>
                                </igtab:Tab>                    
                            </Tabs>
                        </igtab:UltraWebTab>
                    </td>
                </tr>
                
                
			</table>
			<asp:Label id=lblErrMesage visible=false ForeColor=red Text="Error while initiating component." runat=server />		
			<Input Type=Hidden id=intRec value=0 runat=server />
			<Input Type=Hidden id=hidTaxStatus value=0 runat=server />
		</FORM>
	</body>
</html>
