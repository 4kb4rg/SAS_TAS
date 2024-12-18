<%@ Page Language="vb"  %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<html>
	<head>
		<title>Employee Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server>
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<table border=0 cellspacing="1" width="100%">
				<tr>
					<td class="mt-h" colspan="5">BENEFICIARY DETAILS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Pension No :*</td>
					<td width=25%>
						<asp:TextBox id=txtPensionNo width=100% runat=server />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status :</td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td width=20% height=25>Pension Date :*</td>
					<td width=25%>
						<asp:TextBox id=txtPensionDate width=50% runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td height=25>Pension Type :*</td>
					<td><asp:DropDownList id=ddlPensionType width=100% runat=server/>
						<asp:Label id=Label5 visible=false forecolor=red text="<br>Please select one Pension Type." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>Reason :*</td>
					<td><asp:TextBox id=txtReason width=100% maxlength=64 runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>
				</tr>
				
				
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>ACTIVE BENEFICIARY</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>MONTHLY BENEFITS</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
				    <td width=20% height=25>Name :*</td>
					<td width=25%><asp:TextBox id=txtBenName maxlength=64 width=100% runat=server />
                                    <asp:RequiredFieldValidator id=RequiredFieldValidator7 display=dynamic runat=server 
                                    ErrorMessage="Please enter Beneficiary Name." 
                                    ControlToValidate=txtBenName />	
                    </td>	
				    <td>&nbsp;</td>
					<td>Defined Benefit :</td>
					<td><asp:Label id=lblDefinedBenefit runat=server /></td>
				</tr>
				<tr>
				    <td height=25>Status :*</td>
                    <td><asp:DropDownList id=ddlBenStatus width=100% runat=server/>
                        <asp:Label id=Label8 visible=false forecolor=red text="<br>Please select one Beneficiary Status." runat=server/>
                    </td>
				    <td>&nbsp;</td>
					<td>Total Allowance :</td>
					<td><asp:Label id=lblTotAllowance runat=server /></td>
				</tr>
				<tr>
				    <td height=25>Date of Birth :*</td>
                    <td><asp:TextBox id=txtBenDOB width=50% runat=server />
                    </td>
				    <td>&nbsp;</td>
					<td>Total Deduction :</td>
					<td><asp:Label id=lblTotDeduction runat=server /></td>
				</tr>
				<tr>
				    <td height=25>Age :</td>
					<td><asp:TextBox id=txtBenAge width=20% runat=server /></td>
					<td>&nbsp;</td>
					<td>Receipt Benefit :</td>
					<td><asp:Label id=lblReceiptBenefit runat=server /></td>
				</tr>
				<tr>
				    <td height=25 valign=top>Residential Address :</td>
                    <td valign=top>
                        <textarea rows="6" id=txtBenAddress width=100% cols="37" value="" runat=server></textarea>
                        <asp:Label id=Label10 visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
                    </td>
				</tr>
				<tr>
				    <td height=25>Residential Tel No :</td>
                    <td><asp:TextBox id=txtBenTelp width=100% maxlength=15 runat=server />
                            <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
                            ControlToValidate="txtBenTelp"
                            ValidationExpression="[\d\-\(\)]{1,15}"
                            Display="dynamic"
                            ErrorMessage="Phone number must be in numeric digits"
                            EnableClientScript="True" 
                            runat="server"/>
                    </td>
				</tr>
				
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				
				<tr>
				    <td colspan=6>	
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
                                <igtab:Tab Text=" Member Detail ">
                                    <ContentTemplate>
                                        <table border=0 cellspacing=0 cellpadding=2 width=100%> 
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table4" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
						                                <tr>						
							                                <td>
								                                <table border=0 cellpadding=2 cellspacing=0 width=100%>
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
					                                                    <td width=20%></td>
					                                                    <td width=25%></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td width=20% height=25>Employee Code :*</td>
					                                                    <td>
						                                                    <asp:DropDownList id=ddlEmpCode width=100% visible=false runat=server />
						                                                    <asp:Textbox id=txtEmpCode width=100% maxlength=25 runat=server />
						                                                    <asp:RequiredFieldValidator id=revEmpCode1 display=dynamic runat=server 
							                                                    ErrorMessage="<br>Please enter Employee Code." 
							                                                    ControlToValidate=txtEmpCode />			
						                                                    <asp:Label id=lblErrEmpCode visible=false forecolor=red text="<br>Please select/enter one Employee Code." runat=server/>
						                                                    <asp:Label id=lblErrDupEmpCode visible=false forecolor=red text="<br>This employee code has been used. Please try another employee code." runat=server/>
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Unit :*</td>
					                                                    <td><asp:DropDownList id=ddlUnit width=100% runat=server/>
						                                                    <asp:Label id=lblErrUnit visible=false forecolor=red text="<br>Please select one Unit Code." runat=server/>
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                   
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Name :*</td>
				                                                        <td><asp:TextBox id=txtEmpName width=100% maxlength=64 runat=server />
						                                                        <asp:RequiredFieldValidator id=validateEmpName display=dynamic runat=server 
							                                                    ErrorMessage="<br>Please enter Employee Name." 
							                                                    ControlToValidate=txtEmpName />			
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Gender :*</td>
					                                                    <td><asp:DropDownList id=ddlSex width=100% runat=server/>
						                                                    <asp:Label id=lblErrSex visible=false forecolor=red text="<br>Please select one Gender." runat=server/>
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Place of Birth :</td>
					                                                    <td><asp:TextBox id=txtPOB width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Date of Birth :*</td>
					                                                    <td><asp:TextBox id=txtDOB width=50% runat=server /></td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Age :</td>
					                                                    <td><asp:TextBox id=txtAge width=20% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Start Working Date :*</td>
					                                                    <td><asp:TextBox id=txtWorkDate width=50% runat=server />
						                                                    <a href="javascript:PopCal('txtWorkDate');"><asp:Image id="Image1" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						                                                    <asp:Label id=Label1 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						                                                    <asp:RequiredFieldValidator id=RequiredFieldValidator1 display=dynamic runat=server 
							                                                    ErrorMessage="<br>Please enter Start Working Date." 
							                                                    ControlToValidate=txtWorkDate />			
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Working Period :</td>
					                                                    <td><asp:TextBox id=txtWorkPeriod width=20% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Membership Date :*</td>
					                                                    <td><asp:TextBox id=txtMemberDate width=50% runat=server />
						                                                    <a href="javascript:PopCal('txtMemberDate');"><asp:Image id="Image2" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						                                                    <asp:Label id=Label2 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						                                                    <asp:RequiredFieldValidator id=RequiredFieldValidator2 display=dynamic runat=server 
							                                                    ErrorMessage="<br>Please enter Membership Date." 
							                                                    ControlToValidate=txtMemberDate />			
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                    <td height=25>Membership Period :</td>
					                                                    <td><asp:TextBox id=txtMemberPeriod width=20% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Promotion Date :</td>
					                                                    <td><asp:TextBox id=txtProDate width=50% runat=server />
						                                                    <a href="javascript:PopCal('txtProDate');"><asp:Image id="Image3" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						                                                    <asp:Label id=Label3 visible=False forecolor=red text="<br>Invalid date format." runat=server />
						                                                    <asp:RequiredFieldValidator id=RequiredFieldValidator3 display=dynamic runat=server 
							                                                    ErrorMessage="<br>Please enter Membership Date." 
							                                                    ControlToValidate=txtProDate />			
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Level/Golongan :*</td>
					                                                    <td><asp:DropDownList id=ddlGol width=100% runat=server/>
						                                                    <asp:Label id=lblErrGol visible=false forecolor=red text="<br>Please select one Level/Golongan." runat=server/>
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                    <td>PhDP :*</td>
					                                                    <td><asp:TextBox id=txtPhdp width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Employee Contribution :</td>
					                                                    <td><asp:TextBox id=txtNewEmpeCon width=25% runat=server /> %</td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Employee Contribution Amount :</td>
					                                                    <td><asp:TextBox id=txtNewEmpeConAmount width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Employer Contribution :</td>
					                                                    <td><asp:TextBox id=txtNewEmprCon width=25% runat=server /> %</td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Employer Contribution Amount :</td>
					                                                    <td><asp:TextBox id=txtNewEmprConAmount width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td>&nbsp;</td>
					                                                    <td>&nbsp;</td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Total Contribution Amount :</td>
					                                                    <td><asp:TextBox id=txtTotConAmount width=100% runat=server /></td>
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
                                
                                <igtab:Tab Text=" Beneficiary Changes ">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table6" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
                                                        <tr>						
                                                            <td>
                                                                <table border=0 cellpadding=2 cellspacing=0 width=100%>
                                                                    <tr>
					                                                    <td width=20% height=25>NEW BENIFICIARY</td>
					                                                    <td>&nbsp;</td>
				                                                    </tr>
				                                                    <tr>
                                                                        <td height=25 width=20%>Name :*</td>
                                                                        <td width=25%><asp:DropDownList id=ddlBenNameNew width=100% runat=server/>
                                                                                      <asp:Label id=Label18 visible=false forecolor=red text="<br>Please select one Beneficiary Name." runat=server/>
                                                                        </td>	
                                                                        <td width=5%>&nbsp;</td>
					                                                    <td width=20%></td>
					                                                    <td width=25%></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Status :*</td>
                                                                        <td><asp:DropDownList id=ddlBenStatusNew width=100% runat=server/>
                                                                            <asp:Label id=Label9 visible=false forecolor=red text="<br>Please select one Beneficiary Status." runat=server/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Date of Birth :*</td>
                                                                        <td><asp:TextBox id=txtBenDOBNew width=50% runat=server />
                                                                            <a href="javascript:PopCal('txtBenDOBNew');"><asp:Image id="Image6" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
                                                                            <asp:Label id=Label14 visible=False forecolor=red text="<br>Date format should be in " runat=server />
                                                                            <asp:RequiredFieldValidator id=RequiredFieldValidator9 display=dynamic runat=server 
                                                                                ErrorMessage="<br>Please enter Date Of Birth." 
                                                                                ControlToValidate=txtBenDOBNew />			
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
				                                                        <td height=25>Age :</td>
					                                                    <td><asp:TextBox id=txtBenAgeNew width=20% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25 valign=top>Residential Address :</td>
                                                                        <td valign=top>
                                                                            <textarea rows="6" id=txtBenAddressNew width=100% cols="37" value="" runat=server></textarea>
                                                                            <asp:Label id=Label17 visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
                                                                        </td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Residential Tel No :</td>
                                                                        <td><asp:TextBox id=txtBenTelpNew width=100% maxlength=15 runat=server />
                                                                                <asp:RegularExpressionValidator id="RegularExpressionValidator2" 
                                                                                ControlToValidate="txtBenTelpNew"
                                                                                ValidationExpression="[\d\-\(\)]{1,15}"
                                                                                Display="dynamic"
                                                                                ErrorMessage="Phone number must be in numeric digits"
                                                                                EnableClientScript="True" 
                                                                                runat="server"/>
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
                                                                        <td width=35%><asp:TextBox id=txtMemberName maxlength=64 width=100% runat=server />
                                                                            <asp:RequiredFieldValidator id=validateFamMemberName display=dynamic runat=server 
                                                                                ErrorMessage="Please enter Member Name." 
                                                                                ControlToValidate=txtMemberName />	
                                                                        </td>	
                                                                        <td width=5%>&nbsp;</td>
                                                                        <td width=15%></td>
                                                                        <td width=25%></td>	
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
                                                                        <td><asp:TextBox id=txtDOBFam width=40% runat=server />
                                                                            <a href="javascript:PopCal('txtDOBFam');"><asp:Image id="Image4" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
                                                                            <asp:Label id=Label4 visible=False forecolor=red text="<br>Date format should be in " runat=server />
                                                                            <asp:RequiredFieldValidator id=RequiredFieldValidator4 display=dynamic runat=server 
                                                                                ErrorMessage="<br>Please enter Date Of Birth." 
                                                                                ControlToValidate=txtDOBFam />			
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
						                                AllowSorting="True">
                                						
						                                <HeaderStyle CssClass="mr-h"/>
						                                <ItemStyle CssClass="mr-l"/>
						                                <AlternatingItemStyle CssClass="mr-r"/>
						                                <Columns>						
						                                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Name" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("FamMemberName") %> id="lblFamMemberName" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
				                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Relationship" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("Relationship") %> id="lblRelationship" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
				                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Gender" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("Gender") %> id="lblGender" runat="server" />
					                                            </ItemTemplate>
				                                            </asp:TemplateColumn>	
							                                <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Date Of Birth" >
					                                            <ItemTemplate>
						                                            <asp:Label Text=<%# Container.DataItem("DOB") %> id="lblDOB" runat="server" />
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
				                                                       <td width=35%><asp:TextBox id=txtSKNo maxlength=64 width=100% runat=server />
					                                                        <asp:RequiredFieldValidator id=RequiredFieldValidator5 display=dynamic runat=server 
						                                                        ErrorMessage="Please enter Member Name." 
						                                                        ControlToValidate=txtSKNo />	
				                                                       </td>	
			                                                           <td width=5%>&nbsp;</td>
                                                                       <td width=15%></td>
                                                                       <td width=25%></td>	
			                                                       </tr>
				                                                   <tr>
					                                                   <td height=25>Date :*</td>
					                                                   <td><asp:TextBox id=txtSKDate width=40% runat=server />
						                                                    <a href="javascript:PopCal('txtSKDate');"><asp:Image id="Image5" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
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
				                                                            <asp:ImageButton id=btnAddSK imageurl="../../images/butt_add.gif" alternatetext=Add  runat=server />
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
				                                <asp:DataGrid id="DgSK"
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
					                                                    <tdheight=25>CONTRIBUTIONS</td>
					                                                    <td>&nbsp;</td>
				                                                    </tr>
							                                        <tr>
					                                                    <td width=20% height=25>Total Employee Contribution Amount :</td>
					                                                    <td width=35% align=right><asp:Label id=lblTotEmpeConAmount runat=server /></td>
					                                                    <td width=5%>&nbsp;</td>
					                                                    <td width=15%></td>
					                                                    <td width=25%></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Total Employer Contribution Amount :</td>
					                                                    <td align=right><asp:Label id=lblTotEmprConAmount runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Total Contribution Amount :</td>
					                                                    <td align=right><asp:Label id=lblTotConAmount runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td colspan=5>&nbsp;</td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>BENEFITS BALANCE</td>
					                                                    <td>&nbsp;</td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Total Receipt Benefit until last month :</td>
					                                                    <td align=right><asp:Label id=lblBeginBalance runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Total Receipt Benefit until this month :</td>
					                                                    <td align=right><asp:Label id=lblEndBalance runat=server /></td>
				                                                    </tr>
                                                                    <tr>
					                                                    <td colspan=5>&nbsp;</td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td width=20% height=25>ALLOWANCES & DEDUCTIONS</td>
					                                                    <td>&nbsp;</td>
				                                                    </tr>
				                                                    <tr>
                                                                        <td height=25>Transaction Code :</td>
                                                                        <td><asp:DropDownList id=ddlADCode width=100% runat=server/>
                                                                            <asp:Label id=Label15 visible=false forecolor=red text="<br>Please select one Allowance & Deduction Code." runat=server/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Transaction Type :</td>
                                                                        <td><asp:DropDownList id=ddlADType width=100% runat=server/>
                                                                            <asp:Label id=Label16 visible=false forecolor=red text="<br>Please select one Allowance & Deduction Type." runat=server/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
					                                                    <td height=25>No. of Months :</td>
					                                                    <td colspan=5><asp:TextBox id=txtDuration width=15% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
                                                                        <td height=25>Effective Period :</td>
                                                                        <td><asp:DropDownList id=ddlPeriod width=35% runat=server/>
                                                                            <asp:Label id=Label6 visible=false forecolor=red text="<br>Please select one Period." runat=server/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
					                                                    <td height=25>Amount :</td>
					                                                    <td><asp:TextBox id=txtADAmount width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr class="mb-c">											
			                                                            <td>
				                                                            <asp:ImageButton id=btnAddAD imageurl="../../images/butt_add.gif" alternatetext=Add  runat=server />
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
				                                <asp:DataGrid id="DgAD"
					                                AutoGenerateColumns="false" width="100%" runat="server"
					                                GridLines = none
					                                Cellpadding = "2"
					                                Pagerstyle-Visible="False"
					                                AllowSorting="True">
					                                <HeaderStyle CssClass="mr-h" />							
					                                <ItemStyle CssClass="mr-l" />
					                                <AlternatingItemStyle CssClass="mr-r" />						
				                                <Columns>					
			                                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Allowance & Deduction" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("ADCode") %> id="lblADCode" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>	
	                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Type" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("TransType") %> id="lblTransType" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>	
	                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="No. of Months" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("Duration") %> id="lblDuration" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>
	                                            <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Effective Period" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("Period") %> id="lblPeriod" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>
	                                             <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Amount" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("Amount") %> id="lblAmount" runat="server" />
		                                            </ItemTemplate>
	                                            </asp:TemplateColumn>
	                                             <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Status" >
		                                            <ItemTemplate>
			                                            <asp:Label Text=<%# Container.DataItem("Status") %> id="lblStatus" runat="server" />
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
                                
                                <igtab:Tab Text=" Benefits Processing ">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table7" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
					                                    <tr>						
						                                    <td>
							                                    <table border=0 cellpadding=2 cellspacing=0 width=100%>
				                                                    <tr>
                                                                        <td height=25 width=20%>Transaction Type :*</td>
                                                                        <td width=25%><asp:DropDownList id=ddlTransType width=100% runat=server/>
                                                                                      <asp:Label id=Label21 visible=false forecolor=red text="<br>Please select one Transaction Type." runat=server/>
                                                                        </td>	
                                                                        <td width=5%>&nbsp;</td>
					                                                    <td width=20%></td>
					                                                    <td width=25%></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Activation Date :*</td>
                                                                        <td><asp:TextBox id=txtBenActiveDateNew width=50% runat=server />
                                                                            <a href="javascript:PopCal('txtBenActiveDateNew');"><asp:Image id="Image7" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
                                                                            <asp:Label id=Label12 visible=False forecolor=red text="<br>Date format should be in " runat=server />
                                                                            <asp:RequiredFieldValidator id=RequiredFieldValidator10 display=dynamic runat=server 
                                                                                ErrorMessage="<br>Please enter Date Of Birth." 
                                                                                ControlToValidate=txtBenActiveDateNew />			
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td>Pension No. :*</td>
					                                                    <td><asp:TextBox id=txtPensionNoNew width=100% runat=server /></td>
                                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Pension Type :*</td>
					                                                    <td><asp:DropDownList id=ddlPensionTypeNew width=100% runat=server/>
						                                                    <asp:Label id=Label19 visible=false forecolor=red text="<br>Please select one Pension Type." runat=server/>
					                                                    </td>
					                                                    <td>&nbsp;</td>
					                                                    <td>Reason :</td>
					                                                    <td><asp:TextBox id=txtReasonNew width=100% runat=server /></td>
					                                                </tr>
					                                                <tr>
				                                                        <td height=25>Payment Status :*</td>
					                                                    <td><asp:DropDownList id=ddlPayStatusNew width=100% runat=server/>
						                                                    <asp:Label id=Label20 visible=false forecolor=red text="<br>Please select one Payment Status." runat=server/>
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td>&nbsp;</td>
					                                                    <td height=25>
					                                                        <input type=button width=100% value=" evaluation " id="FindSpl" onclick="javascript:PopSupplier('frmMain', '', 'ddlSupplier', 'True');" CausesValidation=False runat=server />
					                                                    </td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Formula :</td>
					                                                    <td colspan=5><asp:TextBox id=txtFormula width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Increament :</td>
					                                                    <td colspan=5><asp:TextBox id=txtIncreament width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Benefit :</td>
					                                                    <td colspan=5><asp:TextBox id=txtBenefit width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
				                                                        <td height=25>Said :</td>
					                                                    <td colspan=5><asp:Label id=lblSaid runat=server /></td>
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
                            
                                <igtab:Tab Text=" Payment Location ">
                                    <ContentTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr class=mb-t>
                                                <td colspan=6>
                                                    <table id="Table5" width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0" align="left" runat=server>
					                                    <tr>						
						                                    <td>
							                                    <table border=0 cellpadding=2 cellspacing=0 width=100%>
				                                                    <tr>
                                                                        <td width=20% height=25>Payment Type :*</td>
                                                                        <td width=35%><asp:DropDownList id=ddlPayType width=100% runat=server/>
                                                                            <asp:Label id=Label11 visible=false forecolor=red text="<br>Please select one Payment Type." runat=server/>
                                                                        </td>
                                                                         <td width=5%>&nbsp;</td>
                                                                        <td width=15%></td>
                                                                        <td width=25%></td>	
                                                                    </tr>
                                                                    <tr>
                                                                        <td height=25>Location/Unit/Post Office/Bank :*</td>
                                                                        <td><asp:DropDownList id=ddlPayLoc width=100% runat=server/>
                                                                            <asp:Label id=Label13 visible=false forecolor=red text="<br>Please select one Payment Location." runat=server/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
					                                                    <td height=25>Bank Account No :</td>
					                                                    <td><asp:TextBox id=txtBankAccNo width=100% runat=server /></td>
				                                                    </tr>
				                                                    <tr>
					                                                    <td height=25>Bank Account Name  :</td>
					                                                    <td><asp:TextBox id=txtBankAccName width=100% runat=server /></td>
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
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Remark :</td>
					<td colspan="5"><asp:TextBox id=txtRemark width=100% runat=server /></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 colspan="5">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save"  runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText="Delete"  runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="UnDelete"  runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
			</table>
			<input type=hidden id=EmpCode value='' runat=server />
			<input type=hidden id=hidEmpName value='' runat=server />
			<input type=hidden id=hidStatus value=0 runat=server/>
			<asp:Label id=lblRedirect visible=false runat=server/>
		</form>    
	</body>
</html>
