<%@ Page Language="vb" src="../../../include/PR_trx_WPDet.aspx.vb" Inherits="PR_trx_WPDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
    
<html>
	<head>
		<title>Allowance and Deduction Entry Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">

                             <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblErrSelect visible=false text="Please select " runat="server" />
			<asp:label id=lblSelect visible=false text="Select " runat="server" />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:Label id=lblErrGangEmployee visible=false forecolor=red text="<br>Please select Gang or Employee Code." runat=server/>
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td   colspan="6"><strong> WORK PERFORMANCE ENTRY DETAILS</strong></td>
				</tr>
				<tr>
					<td colspan=6>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td height=25>Work Performance Code :* 
					<td width=30%><asp:Textbox id=txtWPTrxID width=100% maxlength=18 runat=server/>
						<asp:Label id=lblErrDupWPCode visible=false forecolor=red text="WP code already exist, please try other WP Code." runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Work Performance Code"
							ControlToValidate=txtWPTrxID />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtWPTrxID"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description :</td>
					<td><asp:Textbox id=txtDesc width=100% maxlength=128 runat=server/></td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign=top height=25>Date :*</td>
					<td valign=top width=30%>
						<asp:Textbox id=txtWPDate width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtWPDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif"/></a>
						<asp:ImageButton id=RefreshBtn AlternateText="Refresh" imageurl="../../images/butt_refresh.gif" onclick=onClick_Refresh runat=server />
						<asp:Label id=lblErrWPDate text="Please enter Date of Work Performance." visible=false forecolor=red runat=server />
						<asp:Label id=lblErrWPDateFmt visible=false forecolor=red runat=server/>
						<asp:Label id=lblErrWPDateFmtMsg visible=false text="<br>Date format should be in " runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Gang Code :*</td>
					<td><asp:DropDownList id=ddlGang width=100% OnSelectedIndexChanged=onSelect_Gang AutoPostBack=true runat=server/> 						
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Employee Code  :*</td>
					<td>
						<asp:DropDownList id=ddlEmployee width=88% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmployee','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>						
					</td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
					<td colspan=2></td>
					<td width="5%">&nbsp;</td>					
				</tr>
				<tr>
				    <td colspan=6>	
				        <igtab:UltraWebTab ID="UltraWebTab1" OnTabClick="Onselect_TabChanged" AutoPostBack=True  runat="server" Height="100%" Width="100%" BorderColor="#949C9C" ThreeDEffect="False">
				        <DisabledTabStyle BackColor="Silver">
                        </DisabledTabStyle>
                        <DefaultTabStyle Height="29px">
                        </DefaultTabStyle>
				        <HoverTabStyle CssClass="ContentTabHover" ></HoverTabStyle>
                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                            FillStyle="LeftMergedWithCenter"></RoundedImage>
                        <SelectedTabStyle CssClass="ContentTabSelected">
                        </SelectedTabStyle>   
                            <Tabs>
                                <igtab:Tab Text=" Activity ">
                                    <ContentTemplate>
                                        <table border=0 cellspacing=0 cellpadding=2 width=100%> 
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
						                                <tr>						
							                                <td>
								                                <table border=0 cellpadding=2 cellspacing=0 width=100%>
									                                <tr>
										                                <td valign=top height=25 width=10%>Activity :*</td>
						                                                <td valign=top width=100% colspan=5>
												                                <asp:Dropdownlist id=ddlAccCode OnSelectedIndexChanged=onSelect_Account width=85% AutoPostBack=true runat=server/>
												                                <asp:Label id=lblErrAccCode visible=false forecolor=red text="Please select one Activity." runat=server/>
										                                </td>
									                                </tr>
									                                <tr id="RowChargeLevel" runat="server">
				                                                        <td valign=top height=25 width=10%>Charge Level :*</td>
				                                                        <td valign=top width=100% colspan=5>
					                                                        <asp:DropDownList id="ddlChargeLevel" autopostback=true OnSelectedIndexChanged=onSelect_ChangeLevel  Width=50%  runat=server /> 
				                                                        </td>
			                                                        </tr>
			                                                        <tr id="RowPreBlk" runat="server">
									                                    <td valign=top height=25 width=20%><asp:label id=lblPreBlock Runat="server"/> :*</td>
									                                    <td valign=top width=100% colspan=5><asp:DropDownList id="ddlPreBlock" autopostback=true OnSelectedIndexChanged=onSelect_Block  Width=85% runat=server />
										                                    <asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" />
										                                </td>
								                                    </tr>			
								                                    <tr id="RowBlk" runat="server">
									                                    <td valign=top height=25 width=20%><asp:label id=lblBlock runat=server /> :*</td>
									                                    <td valign=top width=100% colspan=5><asp:DropDownList id=ddlBlock autopostback=true OnSelectedIndexChanged=onSelect_Block  width=85% runat=server/>
										                                                <asp:Label id=lblErrBlock visible=false forecolor=red runat=server/>
									                                    </td>
								                                    </tr>
									                                <tr>
										                                <td valign=top height=25 width=10%>To Date Work Productivity :</td>
										                                <td valign=top width=100% colspan=5><asp:Label id=lblToDateWP runat=server/></td>
									                                </tr>
									                                <tr>
										                                <td valign=top height=25 width=10%>UOM :*</td>
										                                <td valign=top width=100% colspan=5><asp:DropDownList id=ddlUOMCode Width=50% runat=server/>
										                                              <asp:Label id=lblErrUOMCode visible=false forecolor=red runat=server/>
										                                </td>
									                                </tr>
									                                <tr valign=top>
										                                <td valign=top height=25 width=20%>Work Productivity :*</td>
										                                <td valign=top width=20% colspan=5>
											                                <asp:Textbox id=txtWorkProductivity width=10% maxlength=9 runat=server/>
											                                <asp:Label id=lblErrWorkProductivity visible=false forecolor=red text="Please enter Work Productivity." runat=server/>
											                                <asp:RegularExpressionValidator id="rvWorkProductivity"
												                                ControlToValidate="txtWorkProductivity"
												                                ValidationExpression="\d{1,5}\.\d{0,4}|\d{1,5}"
												                                Display="Dynamic"
												                                Text="Maximum length 9 digits."
												                                runat="server"/>
										                                </td>
									                                </tr>										
									                                <tr class="mb-c">											
										                                <td colspan=6 height=25>
											                                <asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
											                                <asp:Label id=lblErrDupl visible=false forecolor=red text="Activity & Block already exists." runat=server/>
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
					                            <td colspan="6">
						                            <asp:DataGrid id=dgLineDet
							                            AutoGenerateColumns=false width="100%" runat=server
							                            GridLines=none
							                            Cellpadding=2
							                            OnDeleteCommand=DEDR_Delete 
							                            Pagerstyle-Visible=False
							                            AllowSorting="True">
                            							
							                            <HeaderStyle CssClass="mr-h"/>
							                            <ItemStyle CssClass="mr-l"/>
							                            <AlternatingItemStyle CssClass="mr-r"/>
							                            <Columns>						
								                            <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Activity">
									                            <ItemTemplate>
										                            <asp:Label Text=<%# Container.DataItem("AccCodeDesc") %>  id=lblAccCodeDesc runat="server" />
										                            <asp:Label Text=<%# Container.DataItem("AccCode") %>  id=lblAccCode visible=false runat="server" />										
									                            </ItemTemplate>
								                            </asp:TemplateColumn>
								                            <asp:TemplateColumn ItemStyle-Width="20%">
									                            <ItemTemplate>
										                            <asp:Label Text=<%# Container.DataItem("SubBlkCode") %>  Visible=True id=lblSubBlkCode runat="server" />
									                            </ItemTemplate>
								                            </asp:TemplateColumn>
								                            <asp:TemplateColumn ItemStyle-Width="10%" HeaderText="UOM">
									                            <ItemTemplate>
										                            <asp:Label Text=<%# Container.DataItem("UOMCode") %>  id="UOMCode" runat="server" />										
									                            </ItemTemplate>
								                            </asp:TemplateColumn>
								                            <asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Work Productivity" ItemStyle-HorizontalAlign=Center HeaderStyle-HorizontalAlign=Center>
									                            <ItemTemplate>
										                            <asp:Label Text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("WorkProductivity"),4) %> id="lblWorkProductivity" runat="server" />  <br>
									                            </ItemTemplate>
								                            </asp:TemplateColumn>
								                            <asp:TemplateColumn ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
									                            <ItemTemplate>
									                                <asp:LinkButton id=lbAddItem CommandName=AddItem Text="Add/Remove Item" runat=server CausesValidation=False />
										                            <asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server CausesValidation=False />
									                            </ItemTemplate>
								                            </asp:TemplateColumn>	
							                            </Columns>
						                            </asp:DataGrid>
					                            </td>
				                            </tr>
				                            <!--
                        				    <table border=0 cellspacing=0 cellpadding=2 width=100%> 
                                                <tr class=mb-t>
				                                    <tr>
					                                    <td height=25 >&nbsp;</td>
					                                    <td height=25 >&nbsp;</td>
					                                    <td height=25 width=60%><hr size="1" noshade></td>
					                                    <td height=25>&nbsp;</td>					
				                                    </tr>	
				                                    <tr>
					                                    <td height=25 >&nbsp;</td>			
					                                    <td height=25 width=60% align=right>Total Work Productivity : </td>
					                                    <td height=25 colspan=3 align=center><asp:label id="lblTotalWP" runat="server" /></td>
					                                    <td>&nbsp;</td>
				                                    </tr>
				                                </tr>
				                            </table>
				                            -->
				                        </table>
				                    </ContentTemplate>
                                    <SelectedStyle Font-Bold="True" Font-Names="Tahoma">
                                    </SelectedStyle>
                                    <Style Font-Names="Tahoma"></Style>
				                </igtab:Tab>
				                
				                <igtab:Tab Text=" Item used ">
                                    <ContentTemplate>
                                        <table border=0 cellspacing=0 cellpadding=2 width=100%> 
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table1" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
						                                <tr>						
							                                <td>
								                                <table border=0 cellpadding=2 cellspacing=0 width=100%>
								                                    <tr>
										                                <td valign=top height=25 width=10%>Activity :*</td>
						                                                <td valign=top width=100% colspan=5>
												                                <asp:Dropdownlist id=ddlActItem OnSelectedIndexChanged=onSelect_ActItemAttd width=85% AutoPostBack=true runat=server/>
												                                <asp:Label id=lblActivity visible=false forecolor=red text="Please select one Activity." runat=server/>
										                                </td>
									                                </tr>
									                                <tr id="Tr1" runat="server">
				                                                        <td valign=top height=25 width=10%>Charge Level :*</td>
				                                                        <td valign=top width=100% colspan=5>
					                                                        <asp:DropDownList id="ddlChargeLevelItem" autopostback=true OnSelectedIndexChanged=onSelect_ChangeLevel  Width=50%  runat=server /> 
				                                                        </td>
			                                                        </tr>
			                                                         <tr id="RowPreBlkItem" runat="server">
									                                    <td valign=top height=25 width=20%><asp:label id=lblBlockItem Runat="server"/> :*</td>
									                                    <td valign=top width=100% colspan=5><asp:DropDownList id="ddlBlockItem" Width=85% runat=server />
										                                    <asp:label id=lblErrBlockItem Visible=False forecolor=red Runat="server" />
										                                </td>
								                                    </tr>			
								                                    <tr id="RowBlkItem" runat="server">
									                                    <td valign=top height=25 width=20%><asp:label id=lblSubBlockItem Runat="server"/> :*</td>
									                                    <td valign=top width=100% colspan=5><asp:DropDownList id="ddlSubBlockItem" Width=85% runat=server />
										                                    <asp:label id=lblErrSubBlockItem Visible=False forecolor=red Runat="server" />
										                                </td>
								                                    </tr>
			                                                        <tr>
				                                                        <td valign=top height=25 width=10%><asp:Label id="lblItemCode" runat="server" />&nbsp;Code : </td>
				                                                        <td valign=top width=100% colspan=5>
					                                                        <asp:DropDownList id=ddlItemCode width=85% runat=server/> 
					                                                        <asp:Label id=lblErrItemCode visible=false forecolor=red text="Please select one Item." runat=server/>
				                                                        </td>
			                                                        </tr>
                                                                    <tr>
				                                                        <td valign=top height=25 width=10%>Quantity : </td>
				                                                        <td valign=top width=100% colspan=5>
					                                                        <asp:TextBox id=txtQuantity width=15% maxlength=20 Text="0" onkeypress="javascript:keypress()" runat=server/>
					                                                        <asp:Label id=lblErrQuantity visible=false forecolor=red text="Quantity cannot be empty or zero value." runat=server/>
				                                                        </td>
			                                                        </tr>
			                                                        <tr>
				                                                        <td valign=top height=25 width=20%>Additional Note : </td>
				                                                        <td valign=top width=100% colspan=5>
					                                                        <asp:TextBox id=txtAddNote width=85% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
				                                                        </td>
			                                                        </tr>
			                                                        <tr class="mb-c">											
				                                                        <td>
					                                                        <asp:ImageButton id=btnAddItem imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAddItem_Click runat=server />
					                                                        <asp:Label id=lblErrDuplItem visible=false forecolor=red text="Itemcode for selected Activity & Block already exists." runat=server/>
				                                                        </td>
			                                                        </tr>	
			                                                    </table>
			                                                </td>
			                                            </tr>
    	                                            </table>
			                                    </td>	
			                                </tr>
			                                <tr>
				                                <td colspan="6">
					                                <asp:DataGrid id=DgLineItem
						                                AutoGenerateColumns=false width="100%" runat=server
						                                GridLines=none
						                                Cellpadding=2
						                                OnDeleteCommand=DEDR_Delete 
						                                Pagerstyle-Visible=False
						                                AllowSorting="True">
                                						
						                                <HeaderStyle CssClass="mr-h"/>
						                                <ItemStyle CssClass="mr-l"/>
						                                <AlternatingItemStyle CssClass="mr-r"/>
						                                <Columns>						
						                                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Activity" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("AccDesc") %> id="lblAccountCode" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
				                                            <asp:TemplateColumn ItemStyle-Width="20%" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("SubBlkCode") %> id="lblSubBlkCode" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
							                                <asp:TemplateColumn ItemStyle-Width="30%" HeaderText="Item Code <br> Additional Note">
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("ItemCodeDesc") %>  id=lblItemCodeDesc runat="server" />
									                                <asp:Label Text=<%# Container.DataItem("ItemCode") %>  id=lblItemCode visible=false runat="server" />										
									                                <br>
									                                <asp:Label Text=<%# Container.DataItem("AdditionalNote") %>  id="lblAddNote" runat="server" />										
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Quantity">
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("Quantity") %>  id="lblQuantity" runat="server" />									
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn ItemStyle-Width="10%" HeaderText="UOM">
								                                <ItemTemplate>
									                                <asp:Label Text=<%# Container.DataItem("UOMCodeDesc") %>  id="UOMCode" runat="server" />										
								                                </ItemTemplate>
							                                </asp:TemplateColumn>
							                                <asp:TemplateColumn ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								                                <ItemTemplate>
								                                    <asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server CausesValidation=False />
								                                </ItemTemplate>
							                                </asp:TemplateColumn>	
						                                </Columns>
					                                </asp:DataGrid>
				                                </td>
			                                </tr>
		                                </table>
                                    </ContentTemplate>
                                    <SelectedStyle Font-Bold="True" Font-Names="Tahoma">
                                    </SelectedStyle>
                                    <Style Font-Names="Tahoma"></Style>
                                </igtab:Tab>
                                
                                <igtab:Tab Text=" Attendance ">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table2" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
					                                    <tr>						
						                                    <td>
							                                    <table border=0 cellpadding=2 cellspacing=0 width=100%>
							                                        <tr>
									                                    <td valign=top height=25 width=10%>Activity :*</td>
					                                                    <td valign=top width=100% colspan=5>
											                                    <asp:Dropdownlist id=ddlActAttd OnSelectedIndexChanged=onSelect_ActItemAttd width=85% AutoPostBack=true runat=server/>
											                                    <asp:Label id=lblActAttd visible=false forecolor=red text="Please select one Activity." runat=server/>
									                                    </td>
								                                    </tr>
								                                    <tr id="Tr4" runat="server">
				                                                        <td valign=top height=25 width=10%>Charge Level :*</td>
				                                                        <td valign=top width=100% colspan=5>
					                                                        <asp:DropDownList id="ddlChargeLevelAttd" autopostback=true OnSelectedIndexChanged=onSelect_ChangeLevel Width=50%  runat=server /> 
				                                                        </td>
			                                                        </tr>
			                                                         <tr id="RowPreBlkAttd" runat="server">
									                                    <td valign=top height=25 width=20%><asp:label id=lblBlockAttd Runat="server"/> :*</td>
									                                    <td valign=top width=100% colspan=5><asp:DropDownList id="ddlBlockAttd" Width=85% runat=server />
										                                    <asp:label id=lblErrBlockAttd Visible=False forecolor=red Runat="server" />
										                                </td>
								                                    </tr>			
								                                    <tr id="RowBlkAttd" runat="server">
									                                     <td valign=top height=25 width=20%><asp:label id=lblSubBlockAttd Runat="server"/> :*</td>
								                                        <td valign=top width=100% colspan=5><asp:DropDownList id="ddlSubBlockAttd" Width=85% runat=server />
									                                        <asp:label id=lblErrSubBlockAttd Visible=False forecolor=red Runat="server" />
									                                    </td>
								                                    </tr>
								                                    <tr>
									                                     <td valign=top height=25 width=20%><asp:label id=lblVehicle Runat="server"/> :</td>
								                                         <td valign=top width=100% colspan=5><asp:DropDownList id="ddlVehicle" Width=85% runat=server />
									                                        <asp:label id=lblErrVehicle Visible=False forecolor=red Runat="server" />
									                                     </td>
								                                    </tr>
								                                    <tr>
									                                     <td valign=top height=25 width=20%><asp:label id=lblVehExpense Runat="server"/> :</td>
								                                         <td valign=top width=100% colspan=5><asp:DropDownList id="ddlVehExpense" Width=85% runat=server />
									                                        <asp:label id=lblErrVehExpense Visible=False forecolor=red Runat="server" />
									                                     </td>
								                                    </tr>
		                                                            <tr>
			                                                            <td valign=top height=25 width=10%>Employee Code : </td>
			                                                            <td valign=top width=100% colspan=5>
				                                                            <asp:DropDownList id=ddlEmpAttd width=85% runat=server/> 
				                                                            <asp:Label id=lblErrEmpAttd visible=false forecolor=red runat=server/>
			                                                            </td>
		                                                            </tr>
		                                                            <tr>
			                                                            <td valign=top height=25 width=20%>OT Hours : </td>
			                                                            <td valign=top width=100% colspan=5>
				                                                            <asp:TextBox id=txtOTHours width=10% maxlength=20 runat=server/>
				                                                            <asp:Label id=lblOTHours visible=false forecolor=red runat=server/>
			                                                            </td>
		                                                            </tr>
		                                                            <tr>
			                                                            <td valign=top height=25 width=20%>Premi : </td>
			                                                            <td valign=top width=100% colspan=5>
				                                                            <asp:TextBox id=txtPremi width=10% maxlength=20 runat=server/>
				                                                            <asp:Label id=lblPremi visible=false forecolor=red runat=server/>
			                                                            </td>
		                                                            </tr>
		                                                            <tr class="mb-c">											
			                                                            <td>
				                                                            <asp:ImageButton id=btnAddAttd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAddAttd_Click runat=server />
			                                                            </td>
		                                                            </tr>	
		                                                        </table>
		                                                    </td>
		                                                </tr>
	                                                </table>
		                                        </td>	
		                                    </tr>
                                            <tr>
				                                <TD colspan = 6 >					
				                                <asp:DataGrid id="DgLineAttendance"
					                                AutoGenerateColumns="false" width="100%" runat="server"
					                                GridLines = none
					                                Cellpadding = "2"
					                                OnEditCommand="DEDR_EditAttd"
					                                OnUpdateCommand="DEDR_UpdateAttd"
					                                OnCancelCommand="DEDR_CancelAttd"
					                                OnDeleteCommand="DEDR_DeleteAttd"
					                                Pagerstyle-Visible="False"
					                                AllowSorting="True">
					                                <HeaderStyle CssClass="mr-h" />							
					                                <ItemStyle CssClass="mr-l" />
					                                <AlternatingItemStyle CssClass="mr-r" />						
				                                <Columns>					
				                                <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Activity" >
					                                <ItemTemplate>
						                                <asp:Label Text=<%# Container.DataItem("AccDesc") %> id="lblAccountCode" runat="server" />
						                                <asp:Label Text=<%# Container.DataItem("AccCode") %>  id=lblAccCode visible=false runat="server" />										
					                                </ItemTemplate>
				                                </asp:TemplateColumn>	
				                                <asp:TemplateColumn ItemStyle-Width="20%" >
					                                <ItemTemplate>
						                                <asp:Label Text=<%# Container.DataItem("SubBlkCode") %> id="lblSubBlkCode" runat="server" />
					                                </ItemTemplate>
				                                </asp:TemplateColumn>		
				                                <asp:TemplateColumn HeaderText="Employee" SortExpression="EmpCode">
					                                <ItemTemplate>
						                                <asp:Label Text=<%# Container.DataItem("EmpDesc") %> id="lblEmpCode" runat="server" />
					                                </ItemTemplate>
				                                </asp:TemplateColumn>			
				                                <asp:TemplateColumn HeaderText="Working Hours" >
					                                <ItemTemplate>
						                                <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Hours"), 2, True, False, False),2) %> id="lblHours" runat="server" /> 
					                                </ItemTemplate>
				                                </asp:TemplateColumn>
				                                <asp:TemplateColumn ItemStyle-HorizontalAlign=center>
					                                <ItemTemplate>
						                                <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" visible=false runat="server"/>
						                                <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
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
			                            </tr>
                                    </table>
                                    </ContentTemplate>
                                    <SelectedStyle Font-Bold="True" Font-Names="Tahoma">
                                    </SelectedStyle>
                                    <Style Font-Names="Tahoma"></Style>
                                </igtab:Tab>
                            </Tabs>
                            <RoundedImage FillStyle="LeftMergedWithCenter" LeftSideWidth="7" RightSideWidth="6" ShiftOfImages="2" />
                            <DefaultTabStyle BackColor="Transparent" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" Height="22px">
                                <Padding Top="2px" Left="2px" Right="2px" />
                                <Margin Left="2px" Right="2px" />
                            </DefaultTabStyle>
                            <SelectedTabStyle Font-Bold="True" Font-Names="Tahoma">
                                <Padding Bottom="2px" Left="2px" />
                                <Margin Left="2px" />
                            </SelectedTabStyle>
                        </igtab:UltraWebTab>
                    </td>
                </tr>
                
				<tr>
					<td colspan=6>&nbsp;</td>				
				</tr>									
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />											
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					&nbsp;</td>
				</tr>
				<Input Type=Hidden id=WPTrxID runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:label id=lblCloseExist visible=false text="no" runat=server/>
				<asp:label id=lblTotArea visible=false text=0 runat=server/>	
				<Input type=hidden id=hidBlockCharge value="" runat=server/>
			    <Input type=hidden id=hidChargeLocCode value="" runat=server/>
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
