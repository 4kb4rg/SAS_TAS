<%@ Page Language="vb" src="../../../include/DP_trx_MemberDet.aspx.vb" Inherits="DP_trx_MemberDet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<html>
	<head>
		<title>Employee Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<table border=0 cellspacing="1" width="100%" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="5">MEMBER DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Member Code :*</td>
					<td width=25%>
						<asp:Textbox id=txtMemCode width=100% maxlength=25 runat=server />
						<asp:RequiredFieldValidator id=revMemCode1 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Member Code." 
							ControlToValidate=txtEmpCode />			
						<asp:Label id=lblErrMemCode visible=false forecolor=red text="<br>Please select/enter one Employee Code." runat=server/>
						<asp:Label id=lblErrDupMemCode visible=false forecolor=red text="<br>This employee code has been used. Please try another employee code." runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status :</td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td width=20% height=25>Employee Code :*</td>
					<td width=25%>
						<asp:Textbox id=txtEmpCode width=100% maxlength=25 runat=server />
						<asp:RequiredFieldValidator id=revEmpCode1 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Employee Code." 
							ControlToValidate=txtEmpCode />			
						<asp:Label id=lblErrEmpCode visible=false forecolor=red text="<br>Please select/enter one Employee Code." runat=server/>
						<asp:Label id=lblErrDupEmpCode visible=false forecolor=red text="<br>This employee code has been used. Please try another employee code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td height=25>Unit :*</td>
					<td><asp:DropDownList id=ddlUnit width=100% runat=server/>
						<asp:Label id=lblErrUnit visible=false forecolor=red text="<br>Please select one Unit Code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>Name :*</td>
					<td><asp:TextBox id=txtMemName width=100% maxlength=64 runat=server />
						<asp:RequiredFieldValidator id=validateMemName display=dynamic runat=server 
							ErrorMessage="<br>Please enter Employee Name." 
							ControlToValidate=txtMemName />			
					</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>
				</tr>
				<tr>
					<td height=25>Gender :*</td>
					<td><asp:DropDownList id=ddlSex width=100% runat=server/>
						<asp:Label id=lblErrSex visible=false forecolor=red text="<br>Please select one Gender." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Old Member Code :</td>
					<td><asp:Label id=lblOldMemberCode runat=server /></td>
				</tr>
				<tr>
				    <td height=25>Place of Birth :</td>
					<td><asp:TextBox id=txtPOB width=100% runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>Date of Birth :*</td>
					<td><asp:TextBox id=txtDOB width=50% runat=server />
						<a href="javascript:PopCal('txtDOB');"><asp:Image id="btnSelDOB" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrDOB visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=validateDOB display=dynamic runat=server 
							ErrorMessage="<br>Please enter Date Of Birth." 
							ControlToValidate=txtDOB />			
					</td>
					<td>&nbsp;</td>
					<td height=25>Age :</td>
					<td><asp:TextBox id=txtAge width=20% runat=server /></td>
				</tr>
				
				<tr>
					<td height=25>Start Working Date :*</td>
					<td><asp:TextBox id=txtWorkDate width=50% runat=server />
						<a href="javascript:PopCal('txtWorkDate');"><asp:Image id="btnSelWorkDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=Label1 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=RequiredFieldValidator1 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Start Working Date." 
							ControlToValidate=txtWorkDate />			
					</td>
					<td>&nbsp;</td>
					<td height=25>Working Period :</td>
					<td><asp:TextBox id=txtWorkPeriod width=20% runat=server /></td>
				</tr>
				<tr>
					<td height=25>Membership Date :*</td>
					<td><asp:TextBox id=txtMemDate width=50% runat=server />
						<a href="javascript:PopCal('txtMemDate');"><asp:Image id="btnSelMemDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=Label2 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=RequiredFieldValidator2 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Membership Date." 
							ControlToValidate=txtMemDate />			
					</td>
					<td>&nbsp;</td>
					<td height=25>Membership Period :</td>
					<td><asp:TextBox id=txtMemPeriod width=20% runat=server /></td>
				</tr>
				<tr>
					<td height=25>Promotion Date :</td>
					<td><asp:TextBox id=txtPromDate width=50% runat=server />
						<a href="javascript:PopCal('txtPromDate');"><asp:Image id="btnSelPromDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=Label3 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator id=RequiredFieldValidator3 display=dynamic runat=server 
							ErrorMessage="<br>Please enter Membership Date." 
							ControlToValidate=txtPromDate />			
					</td>
				</tr>
				<tr>
					<td height=25>Level/Golongan :*</td>
					<td><asp:DropDownList id=ddlGol width=100% runat=server/>
						<asp:Label id=lblErrGol visible=false forecolor=red text="<br>Please select one Level/Golongan." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>PhDP :*</td>
					<td><asp:Label id=lblPhDP runat=server /></td>
				</tr>
				<tr>
					<td height=25>Employee Contribution :</td>
					<td><asp:Label id=lblEmpeCo runat=server /> %</td>
					<td>&nbsp;</td>
					<td>Employee Contribution Amount :</td>
					<td><asp:Label id=lblEmpeCoAmount runat=server /></td>
				</tr>
				<tr>
					<td height=25>Employer Contribution :</td>
					<td><asp:Label id=lblEmprCo runat=server /> %</td>
					<td>&nbsp;</td>
					<td>Employer Contribution Amount :</td>
					<td><asp:Label id=lblEmprCoAmount runat=server /></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Total Contribution Amount :</td>
					<td><asp:Label id=lblTotCoAmount runat=server /></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				</table>

                <table style="width: 100%" class="font9Tahoma">
				<tr>
				    <td colspan=2>	
				        <igtab:UltraWebTab ID="UltraWebTab1" AutoPostBack=True  runat="server" Height="100%" Width="100%" BorderColor="#949C9C" ThreeDEffect="False">
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
                                <igtab:Tab Text=" Personnal ">
                                    <ContentTemplate>
                                        <table border=0 cellspacing=0 cellpadding=2 width=100%> 
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
						                                <tr>						
							                                <td>
								                                <table border=0 cellpadding=2 cellspacing=0 width=100%>
									                                <tr>
					                                                    <td width=20% height=25>Marital Status :*</td>
					                                                    <td width=25%><asp:DropDownList id=ddlMarital width=100% runat=server/>
						                                                    <asp:Label id=lblErrMarital visible=false forecolor=red text="<br>Please select one Marital Status." runat=server/>
					                                                    </td>
					                                                    <td width=5%>&nbsp;</td>
					                                                    <td width=20%>Religion :</td>
					                                                    <td width=25%><asp:DropDownList id=ddlReligion width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25 width=20%>Tax Status :*</td>
					                                                    <td><asp:DropDownList id=ddltaxstatus width=100% runat=server />
						                                                    <asp:Label id=lblErrTaxStatus visible=false forecolor=red text="<br>Please select one Tax Status." runat=server/>
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Race :</td>
					                                                    <td><asp:DropDownList id=ddlRace width=100% runat=server />
						                                                    <asp:Label id=lblErrRace visible=false forecolor=red text="<br>Please select one Race." runat=server/>
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25 width=20%>NPWP :</td>
					                                                    <td><asp:TextBox id=txtNPWP width=100% runat=server /></td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Nationality :</td>
					                                                    <td><asp:DropDownList id=ddlNation width=100% runat=server />
						                                                    <asp:Label id=lblErrNation visible=false forecolor=red text="<br>Please select one Nationality." runat=server/>
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25 width=20%>I.C Type :</td>
					                                                    <td><asp:DropDownList id=ddlICType width=100% runat=server /></td>
					                                                    <td>&nbsp;</td>
				                                                        <td valign=top>I.C Number :</td>
					                                                    <td valign=top><asp:TextBox id=txtICNo width=100% maxlength=18 runat=server />
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25 width=20% valign=top>Residential Address :</td>
					                                                    <td valign=top>
						                                                    <textarea rows="6" id=txtResAddress width=100% cols="37" value="" runat=server></textarea>
						                                                    <asp:Label id=lblErrResAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                    <td valign=top>IC Address :</td>
					                                                    <td valign=top>
						                                                    <textarea rows="6" id=txtICAddress width=100% cols="37" runat=server></textarea>
						                                                    <asp:Label id=lblErrICAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25 width=20%>Residential Tel No :</td>
					                                                    <td><asp:TextBox id=txtResTel width=100% maxlength=15 runat=server />
							                                                    <asp:RegularExpressionValidator id="revResNo" 
							                                                    ControlToValidate="txtResTel"
							                                                    ValidationExpression="[\d\-\(\)]{1,15}"
							                                                    Display="dynamic"
							                                                    ErrorMessage="Phone number must be in numeric digits"
							                                                    EnableClientScript="True" 
							                                                    runat="server"/>
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Mobile Phone No :</td>
					                                                    <td><asp:TextBox id=txtMobileTel width=100% maxlength=15 runat=server />
							                                                    <asp:RegularExpressionValidator id="revMobileNo" 
							                                                    ControlToValidate="txtMobileTel"
							                                                    ValidationExpression="[\d\-\(\)]{1,15}"
							                                                    Display="dynamic"
							                                                    ErrorMessage="Phone number must be in numeric digits"
							                                                    EnableClientScript="True" 
							                                                    runat="server"/>
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25 valign=top>Postal Address :</td>
					                                                    <td valign=top>
						                                                    <textarea rows="6" id=txtPostAddress width=75% cols="37" runat=server></textarea>
						                                                    <asp:Label id=lblErrPostAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					                                                    </td>
				                                                    </tr>					
								                                </table>
							                                </td>
						                                </tr>
					                                </table>
				                                </td>
                                            </tr>
				                        </table>
				                    </ContentTemplate>
                                    <SelectedStyle Font-Bold="True" Font-Names="Tahoma">
                                    </SelectedStyle>
                                    <Style Font-Names="Tahoma"></Style>
				                </igtab:Tab>
				                
				                <igtab:Tab Text=" Family Member ">
                                    <ContentTemplate>
                                        <table border=0 cellspacing=0 cellpadding=2 width=100%> 
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table1" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
						                                <tr>						
							                                <td>
								                                <table border=0 cellpadding=2 cellspacing=0 width=100%>
                                                                    <tr>
                                                                        <td height=25 width=20%>Member Name :*</td>
                                                                        <td width=100% colspan=5><asp:TextBox id=txtFamName maxlength=64 width=100% runat=server />
                                                                            <asp:RequiredFieldValidator id=validateFamName display=dynamic runat=server 
                                                                                ErrorMessage="Please enter Family Member Name." 
                                                                                ControlToValidate=txtFamName />	
                                                                        </td>	
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Relationship :*</td>
                                                                        <td><asp:DropDownList id=ddlRelationship width=100% runat=server/>
                                                                            <asp:Label id=lblErrRelationship visible=false forecolor=red text="<br>Please select one Relationship." runat=server/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Gender :*</td>
                                                                        <td><asp:DropDownList id=ddlGender width=100% runat=server/>
                                                                            <asp:Label id=lblErrGender visible=false forecolor=red text="<br>Please select one Gender." runat=server/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Date of Birth :*</td>
                                                                        <td><asp:TextBox id=txtFamDOB width=15% runat=server />
                                                                            <a href="javascript:PopCal('txtFamDOB');"><asp:Image id="btnSelFamDOB" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
                                                                            <asp:Label id=Label4 visible=False forecolor=red text="<br>Date format should be in " runat=server />
                                                                            <asp:RequiredFieldValidator id=RequiredFieldValidator4 display=dynamic runat=server 
                                                                                ErrorMessage="<br>Please enter Date Of Birth." 
                                                                                ControlToValidate=txtFamDOB />			
                                                                        </td>
                                                                    </tr>
			                                                        <tr class="mb-c">											
				                                                        <td>
					                                                        <asp:ImageButton id=btnAddItem imageurl="../../images/butt_add.gif" alternatetext=Add  runat=server />
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
					                                <asp:DataGrid id=DgFamily
						                                AutoGenerateColumns=false width="100%" runat=server
						                                GridLines=none
						                                Cellpadding=2
						                                Pagerstyle-Visible=False
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
						                                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Name" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("war_1") %> id="lblFamMemberName" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
				                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Relationship" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("sta_1") %> id="lblRelationship" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
				                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Gender" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("sex_1") %> id="lblGender" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
							                                <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Date Of Birth" >
					                                            <ItemTemplate>
						                                            <%#objGlobal.GetLongDate(Container.DataItem("lah_1"))%>
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
                                
                                <igtab:Tab Text=" Surat Keputusan ">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table2" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
					                                    <tr>						
						                                    <td>
							                                    <table border=0 cellpadding=2 cellspacing=0 width=100%>
						                                            <tr>
				                                                        <td height=25 width=20%>Number :*</td>
				                                                        <td width=100% colspan=5><asp:TextBox id=txtSKNo maxlength=64 width=100% runat=server />
					                                                        <asp:RequiredFieldValidator id=RequiredFieldValidator5 display=dynamic runat=server 
						                                                        ErrorMessage="Please enter Member Name." 
						                                                        ControlToValidate=txtSKNo />	
				                                                        </td>	
			                                                        </tr>
				                                                   <tr>
					                                                    <td height=25>Date :*</td>
					                                                   <td><asp:TextBox id=txtSKDate width=15% runat=server />
						                                                    <a href="javascript:PopCal('txtSKDate');"><asp:Image id="btnSelSKDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						                                                    <asp:Label id=Label7 visible=False forecolor=red text="<br>Date format should be in " runat=server />
						                                                    <asp:RequiredFieldValidator id=RequiredFieldValidator6 display=dynamic runat=server 
							                                                    ErrorMessage="<br>Please enter Date Of Birth." 
							                                                    ControlToValidate=txtSKDate />			
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Notes :</td>
					                                                    <td><asp:TextBox id=txtNotes width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>File :</td>
					                                                    <td><asp:TextBox id=txtFile width=100% runat=server /></td>
				                                                    </tr>
		                                                            <tr class="mb-c">											
			                                                            <td>
				                                                            <asp:ImageButton id=btnAddAttd imageurl="../../images/butt_add.gif" alternatetext=Add  runat=server />
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
				                                <asp:DataGrid id="DgLine2"
					                                AutoGenerateColumns="false" width="100%" runat="server"
					                                GridLines = none
					                                Cellpadding = "2"
					                                Pagerstyle-Visible="False"
					                                AllowSorting="True">
					                                <HeaderStyle CssClass="mr-h" />							
					                                <ItemStyle CssClass="mr-l" />
					                                <AlternatingItemStyle CssClass="mr-r" />						
				                                <Columns>					
			                                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="No." >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("SKNo") %> id="lblSKNo" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>	
	                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Date" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("SKDate") %> id="lblSKDate" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>	
	                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Remark" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("Remark") %> id="lblRemark" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>	
				                                <asp:TemplateColumn ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
					                                <ItemTemplate>
					                                    <asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server CausesValidation=False />
					                                </ItemTemplate>
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
                                
                                 <igtab:Tab Text=" Amount Balance ">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table3" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
					                                    <tr>						
						                                    <td>
							                                    <table border=0 cellpadding=2 cellspacing=0 width=100%>
				                                                    <tr>
					                                                    <td height=25>CONTRIBUTIONS</td>
					                                                    <td>&nbsp;</td>
				                                                    </tr>
							                                        <tr>
					                                                    <td width=20% height=25>Total Employee Contribution Amount :</td>
					                                                    <td width=15% align=right><asp:Label id=lblTotEmpeConAmount runat=server /></td>
					                                                    <td width=5%>&nbsp;</td>
					                                                    <td width=25%></td>
					                                                    <td width=35%></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Total Employer Contribution Amount :</td>
					                                                    <td align=right><asp:Label id=lblTotEmprConAmount runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Total Contribution Amount :</td>
					                                                    <td align=right><asp:Label id=lblTotConAmount runat=server /></td>
				                                                    </tr>
		                                                        </table>
		                                                    </td>
		                                                </tr>
	                                                </table>
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
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Remark :</td>
					<td><asp:TextBox id=txtRemark width=100% runat=server /></td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 colspan="2">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save"  runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText="Delete"  runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="UnDelete"  runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" runat=server />
					</td>
				</tr>
				<tr>
					<td height=25 colspan="2">
                                            &nbsp;</td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>
			</table>
			<input type=hidden id=MemCode value='' runat=server />
			<input type=hidden id=hidMemName value='' runat=server />
			<input type=hidden id=hidStatus value=0 runat=server/>
			<asp:Label id=lblRedirect visible=false runat=server/>


        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>    
	</body>
</html>
