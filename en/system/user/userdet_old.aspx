<%@ Page Language="vb" Trace=False src="../../../include/system_user_userdet.aspx.vb" Inherits="system_user_userdet"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<html>
	<head>
		<title>User Details</title>
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Script language="JavaScript">
		function CAS() {
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if (((e.name != 'cbSysAll') && (e.type=='checkbox')) && ((e.name != 'cbAdminCompany') && (e.type=='checkbox')) && ((e.name != 'cbAdminLocation') && (e.type=='checkbox')) && ((e.name != 'cbAdminUOM') && (e.type=='checkbox')) && ((e.name != 'cbAdminDT') && (e.type=='checkbox')) && ((e.name != 'cbAdmAll') && (e.type=='checkbox')))
					e.checked = document.frmMain.cbSysAll.checked;
			}
		}

		function CCAS() {
			var TotalBoxes = 0;
			var TotalOn = 0;
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if (((e.name != 'cbSysAll') && (e.type=='checkbox')) && ((e.name != 'cbAdminCompany') && (e.type=='checkbox')) && ((e.name != 'cbAdminLocation') && (e.type=='checkbox')) && ((e.name != 'cbAdminUOM') && (e.type=='checkbox')) && ((e.name != 'cbAdminDT') && (e.type=='checkbox')) && ((e.name != 'cbAdmAll') && (e.type=='checkbox')))
				{
					TotalBoxes++;
					if (e.checked) {
						TotalOn++;
					}
				}
			}
			if (TotalBoxes==TotalOn)
				{document.frmMain.cbSysAll.checked=true;}
			else
				{document.frmMain.cbSysAll.checked=false;}
		}

		function CAA() {
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if (((e.name != 'cbAdmAll') && (e.type=='checkbox')) && ((e.name != 'cbSysUser') && (e.type=='checkbox')) && ((e.name != 'cbSysCfg') && (e.type=='checkbox')) && ((e.name != 'cbSysLangCap') && (e.type=='checkbox')) && ((e.name != 'cbSysAll') && (e.type=='checkbox')))
					e.checked = document.frmMain.cbAdmAll.checked;
			}
		}

		function CCAA() {
			var TotalBoxes = 0;
			var TotalOn = 0;
			for (var i=0;i<document.frmMain.elements.length;i++) {
				var e = document.frmMain.elements[i];
				if (((e.name != 'cbAdmAll') && (e.type=='checkbox')) && ((e.name != 'cbSysUser') && (e.type=='checkbox')) && ((e.name != 'cbSysCfg') && (e.type=='checkbox')) && ((e.name != 'cbSysLangCap') && (e.type=='checkbox')) && ((e.name != 'cbSysAll') && (e.type=='checkbox')))
				{
					TotalBoxes++;
					if (e.checked) {
						TotalOn++;
					}
				}
			}
			if (TotalBoxes==TotalOn)
				{document.frmMain.cbAdmAll.checked=true;}
			else
				{document.frmMain.cbAdmAll.checked=false;}
		}
		</Script>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
			<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
             <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 800px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

			<table border=0 cellspacing="0" cellpadding=2 width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6">
						<UserControl:MenuSYS id=MenuSYS runat="server" />
					</td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan="3"><strong> APPLICATION USER DETAILS</strong></td>
					<td colspan=2 width=20%>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan="6"><hr style="width :100%" /></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr class="mr-h">
					<td class="NormalBold" colspan="3">Step 1 : User Information</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">User ID* :</td>
					<td>
						<asp:textbox id=txtUserId width=40% maxlength=8 CssClass="font9Tahoma" runat=server />
						<asp:Label id=lblErrUserId Text="User ID is being used." forecolor=red runat=server />
						<asp:RequiredFieldValidator id=validatetxtUserId display=dynamic runat=server 
							ErrorMessage="Please enter User ID. " 
							ControlToValidate=txtUserId />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtUserId"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
					</td>
					<td colspan="2">Status :</td>
					<td><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td colspan="2">Password* :</td>
					<td><asp:TextBox textmode=password id=txtPassword width=40% maxlength=8  CssClass="font9Tahoma"  runat=server /> </td>
					<td colspan="2">Date Created :</td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td colspan="2">Confirm Password*&nbsp; :</td>
					<td>
						<asp:TextBox textmode=password id=txtConfirmPwd width=40% maxlength=8  CssClass="font9Tahoma"  runat=server /> 
						<asp:Label id=lblPassword Text="Please re-enter your password." forecolor=red runat=server />
					</td>
					<td colspan="2">Last Updated :</td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td colspan="2">Name* :</td>
					<td>
						<asp:TextBox id=txtName width=80% maxlength=64  CssClass="font9Tahoma"  runat=server />
						<asp:RequiredFieldValidator id=validatetxtName display=dynamic runat=server 
							ErrorMessage="Please enter user name. " 
							ControlToValidate=txtName />
					</td>
					<td colspan="2">Updated By :</td>
					<td>
						<asp:Label id=lblUpdatedBy runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="2">Email Address :</td>
					<td>
						<asp:TextBox id=txtEmail width=80% maxlength=64  CssClass="font9Tahoma"  runat=server /><br>
						<asp:RegularExpressionValidator id="validateTxtEmail2" 
							ControlToValidate="txtEmail"
							ValidationExpression=".*@.*\..*"
							Display="Dynamic"
							ErrorMessage="Please enter a valid email."
							EnableClientScript="True" 
							runat="server"/>
					</td>
					<td colspan="2">Employee ID :</td>
					<td>
						<asp:DropDownList id=ddlEmpID width=80% CssClass="font9Tahoma" runat=server /> 
						<input type="button" id=btnFind value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','ddlEmpID','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
					</td>
				</tr>
				<tr>
					<td colspan="2">
                        User Level :</td>
					<td width=30%><asp:DropDownList id=ddlLevel width=80%  CssClass="font9Tahoma"  runat=server >
                        <asp:ListItem Selected="True" Value="0">User/Krani</asp:ListItem>
                        <asp:ListItem Value="1">Asisten</asp:ListItem>
                        <asp:ListItem Value="2">KTU</asp:ListItem>
                        <asp:ListItem Value="3">Askep/Manager</asp:ListItem>
                        <asp:ListItem Value="4">RC/CorEM</asp:ListItem>
                        <asp:ListItem Value="5">GM</asp:ListItem>
                        <asp:ListItem Value="6">DirOps</asp:ListItem>
                    </asp:DropDownList></td>
					<td width=10%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2">
                        Department :</td>
					<td width=30%><asp:DropDownList id=ddlDeptCode width=80%  CssClass="font9Tahoma" runat=server >
                    </asp:DropDownList></td>
					<td width=10%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr>
					<td width=10%>&nbsp;</td>
					<td width=10%>&nbsp;</td>
					<td width=30%>&nbsp;</td>
					<td width=10%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>
				<tr class="mr-h">
					<td class="NormalBold" colspan="3">Step 2 : System Access Right</td>
					<td colspan="2">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">Note : To grant system access rights, click on respective checkbox. 
						Administration access rights are automatically inherited by the user who has system access rights. 
						<BR />User should not have system and application access rights at the same time</td>
				</tr>
				<tr>
					<td colspan=6>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" class="font9Tahoma" >
							<tr>
								<td>&nbsp;</td>
								<td class="NormalBold" colspan="2"><asp:CheckBox id=cbSysAll text=" All" onclick="javascript:CAS();" textalign=right runat=server /> 
									<asp:Label id=lblADAR forecolor=red Text="User currently has location access rights." runat=server /></td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td><asp:CheckBox id=cbSysCfg text=" System Configuration, Parameters Setting" onclick="javascript:CCAS();" textalign=right runat=server /></td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td width=5%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
								<td width=70%><asp:CheckBox id=cbSysUser text=" Application User" onclick="javascript:CCAS();" textalign=right runat=server /></td>
								<td width=5%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
								<td width=5%>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td><asp:CheckBox id=cbSysLangCap text=" Language Caption" onclick="javascript:CCAS();" textalign=right runat=server /></td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" class="font9Tahoma">
						<tr class="mr-h">
							<td class="NormalBold" colspan="3">Step 3 : Administration Access Rights</td>
							<td align="center">&nbsp;</td>
							<td align="center">&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td colspan="7">Note : To grant administration access rights, click on respective checkbox.</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td class="NormalBold" colspan="2"><asp:CheckBox id=cbAdmAll text=" All" onclick="javascript:CAA();" textalign=right runat=server /> 
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td width=5%>&nbsp;</td>
							<td width=5%>&nbsp;</td>
							<td width=70%><asp:CheckBox id=cbAdminCompany onclick="javascript:CCAA();" textalign=right runat=server /></td>
							<td width=5%>&nbsp;</td>
							<td width=5%>&nbsp;</td>
							<td width=5%>&nbsp;</td>
							<td width=5%>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td><asp:CheckBox id=cbAdminLocation onclick="javascript:CCAA();" textalign=right runat=server /></td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td><asp:CheckBox id=cbAdminNearestLocation onclick="javascript:CCAA();" textalign=right runat=server /></td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td><asp:CheckBox id=cbAdminUOM text=" Unit of Measurement" onclick="javascript:CCAA();" textalign=right runat=server /></td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td><asp:CheckBox id=cbAdminDT text=" Download/Upload company, location, UOM reference files" onclick="javascript:CCAA();" textalign=right runat=server /></td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						<tr>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td><asp:CheckBox id="cbAdminBackup" text=" Database Backup and Restoration" onclick="javascript:CCAA();" textalign="right" runat="server"></asp:CheckBox></td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						</table>
					</td>
				</tr>				
				<tr>
					<td colspan=6>
                       
                    </td>
				</tr>
				<tr class="mr-h">
					<td class="NormalBold" colspan="3">Step 4 : Application Access Rights</td>
					<td align="center">&nbsp;</td>
					<td align="center">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="NormalBold" colspan="3">
						<asp:ImageButton id=NewLocBtn imageurl="../../images/butt_add.gif" onClick=NewLocBtn_Click AlternateText="Add New Location" runat=server /> 
						<asp:Label id=lblNewLoc text="User currently has administration access rights." forecolor=red runat=server />
						copy Access Rights from : <asp:DropDownList id=ddluseraccessref CssClass="font9Tahoma" width=50% runat=server /> &nbsp;
						<asp:ImageButton id=CopyBtn imageurl="../../images/butt_copy.gif" onClick=CopyBtn_Click AlternateText="Copy Access Rights" runat=server /> 
					</td>
					<td align="center"> &nbsp;</td>
					<td align="center">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>

                
				<tr>
					<td width="21%" colspan="6">
                
                                        <igtab:UltraWebTab ID="TABBK" runat="server" 
                                            __designer:errorcontrol="Unable to locate license assembly." 
                                            Font-Names="Tahoma" Font-Size="8pt" ForeColor="black" SelectedTab="0" 
                                            TabOrientation="TopLeft" ThreeDEffect="False">
                                            <DefaultTabStyle Height="22px">
                                            </DefaultTabStyle>
                                            <HoverTabStyle CssClass="ContentTabHover">
                                            </HoverTabStyle>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" 
                                                HoverImage="../../images/thumbs/ig_tab_winXP2.gif" LeftSideWidth="6" 
                                                NormalImage="../../images/thumbs/ig_tab_winXP3.gif" RightSideWidth="6" 
                                                SelectedImage="../../images/thumbs/ig_tab_winXP1.gif" />
                                            <SelectedTabStyle CssClass="ContentTabSelected">
                                            </SelectedTabStyle>
                                            <Tabs>
                                                <igtab:Tab Key="StockReceive" Text="LOCATION ACSESS" 
                                                    Tooltip="LOCATION">
                                                    <ContentPane>
                                                     <table border="0" cellpadding="1" cellspacing="1" width="99%">
                                                            <tr>
                                                                <td colspan="6">
                                                                    <div ID="div1" style="height:260px;width:1100;overflow:auto;">
						                                                <asp:DataGrid id=ActiveLocation 
									BorderColor=black
									BorderWidth=0
									GridLines=both
									CellPadding=1
									CellSpacing=1
									width=100% 
									AutoGenerateColumns=false 
									OnItemCommand=OnItemCommand_Process 
									OnDeleteCommand=DEDR_Delete 
									runat=server class="font9Tahoma">
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
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:LinkButton CommandName=UserLocDetails id=UserLoc Text='<%# Container.DataItem("LocCode") %>' runat=server />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Description" />

									<asp:TemplateColumn HeaderText="Access Expiry Date">
										<ItemTemplate>
											<asp:Label text='<%# objGlobal.GetLongDate(Container.DataItem("LocCode")) %>' id=lblExpiryDate runat=server />
										</ItemTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:LinkButton text=Delete CommandName=Delete id=lbDelete runat=server />
											<asp:label id=lblLocCode text='<%# Container.DataItem("LocCode") %>' visible=false runat=server />
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
						</asp:DataGrid>
						                        </div>
                                            </td>
                                        </tr>
                                </table>
                                </ContentPane>
                            </igtab:Tab>


					 

                        <igtab:Tab Key="rptsetup" Text="SETUP REPORT" 
                            Tooltip="PLEASE SETUP REPORT">                                                     
                                <ContentPane>
                                <div ID="div2" style="height:260px;width:1100;overflow:auto;">
                                <asp:CheckBox ID="chkMenurptAll" Text="CHECK ALL" OnCheckedChanged="CheckBoxMenuAll_CheckedChanged" AutoPostBack=true runat="server" />
                                        <asp:DataGrid id=dgTxSHReport 
									BorderColor=black
									BorderWidth=0
									GridLines=None
									CellPadding=1
									CellSpacing=1
									width=100% 
									AutoGenerateColumns=false 
									runat=server class="font9Tahoma">
						                 <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="White" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="White" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
								<Columns>
                          
										<asp:TemplateColumn >
                                        <HeaderTemplate>
                                            
                                        </HeaderTemplate>
										<ItemTemplate>                                        
											  <asp:CheckBox ID="chkMenu" AutoPostBack=true OnCheckedChanged="CheckBoxMenu_CheckedChanged" runat="server" />
										</ItemTemplate>
                                        <ItemStyle Width="1%" />
									    </asp:TemplateColumn>

										<asp:TemplateColumn  HeaderText="Menu ID" Visible=false>
										<ItemTemplate>                                        
												<asp:label id=lblrptid text='<%# Container.DataItem("ReportID") %>' visible=TRUE runat=server />
										</ItemTemplate>
                                        <ItemStyle Width="5%" />
									    </asp:TemplateColumn>
										<asp:TemplateColumn  HeaderText="Description">
										<ItemTemplate>
                                                                                                
												<asp:label id=lblrptName text='<%# Container.DataItem("RptName") %>' visible=False runat=server />
                                                <asp:label id=lblrptDesc  visible=TRUE runat=server />
										</ItemTemplate>
                                        <ItemStyle Width="15%" />
									    </asp:TemplateColumn>


										<asp:TemplateColumn HeaderText="Module">
										<ItemTemplate>                                        
												<asp:label id=lblrptModule text='<%# Container.DataItem("Module") %>' visible=TRUE runat=server />
										</ItemTemplate>
                                        <ItemStyle Width="5%" />
									    </asp:TemplateColumn>

										<asp:TemplateColumn Visible=false>
										<ItemTemplate>                                        
												<asp:label id=lblrptLevel text='<%# Container.DataItem("RptLevel") %>' visible=TRUE runat=server />
										</ItemTemplate>
                                        <ItemStyle Width="5%" />
									    </asp:TemplateColumn>


										<asp:TemplateColumn Visible=false>
										<ItemTemplate>                                        
												<asp:label id=lblrptType text='<%# Container.DataItem("ReportType") %>' visible=TRUE runat=server />
										</ItemTemplate>
                                        <ItemStyle Width="5%" />
									    </asp:TemplateColumn>

										<asp:TemplateColumn Visible=False>
										<ItemTemplate>                                        
												<asp:label id=lblRptCheck text='<%# Container.DataItem("RptCheck") %>' visible=TRUE runat=server />
										</ItemTemplate>
                                        <ItemStyle Width="5%" />
									    </asp:TemplateColumn>

								</Columns>
						</asp:DataGrid>
                                </div>
                                </ContentPane>
                      
                        </igtab:Tab>

                        </Tabs>
                        </igtab:UltraWebTab>
                                    
                
            
                                                                                                                    
                                       
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width="21%" colspan="6">
						<asp:ImageButton id=SaveBtn imageurl="../../images/butt_save.gif" AlternateText=Save onClick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText=Delete onClick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn imageurl="../../images/butt_undelete.gif" CausesValidation=False AlternateText=Undelete onClick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText=Back onClick=BackBtn_Click runat=server />
					</td>
				</tr>
				<input type=hidden id=userid runat=server />
				<input type=hidden id=hidUserLoc runat=server />
				<asp:Label id=lblErrMesage visible=false Text="Error while initiating component." runat=server />
				<asp:label id=lblCode visible=false text=" Code" runat=server />
				<asp:label id=lblDownload visible=false text=" Download/Upload " runat=server />
				<asp:label id=lblUOMfile visible=false text=", UOM reference files" runat=server />
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
