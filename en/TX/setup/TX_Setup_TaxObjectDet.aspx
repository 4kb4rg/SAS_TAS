<%@ Page Language="vb" trace="false" src="../../../include/TX_Setup_TaxObjectDet.aspx.vb" Inherits="TX_Setup_TaxObjectDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Tax Object Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=trxid text="" runat=server />
			<Input Type=Hidden id=trxlnid text="" runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">TAX OBJECT DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblAccCodeTag" Runat="server"/> </td>
					<td width=30%><asp:DropDownList id=ddlAccCode width=90% runat=server/>
					    <input type=button value=" ... " id="Find" onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode', 'True');" CausesValidation=False runat=server />
						<asp:label id=lblAccCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description : </td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Expired : </td>
					<td><asp:CheckBox id="chkExpired" Text="  No expire date" checked=false OnCheckedChanged=change_expired AutoPostBack=true runat=server /></td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 align=left valign=top></td>
					<td><asp:TextBox ID=txtTransDate maxlength=10 width=25% Runat=server />
					    <a href="javascript:PopCal('txtTransDate');"><asp:Image id="btnSelTransDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					    <asp:Label id=lblErrTransDate forecolor=red Visible = false text="Date format " runat=server />
					    <asp:label id=lblFmtTransDate  forecolor=red Visible = false Runat="server"/> 
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
                </table>

                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
			    <tr>
				    <td colSpan="6">
					    <!--<table width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0">
						    <tr>						
							    <td>-->
								    <table id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server>
                                    <tr class="mb-c">
                                        <td width="20%" height=25>Tax Object :* </td>
                                        <td colSpan="5"><asp:TextBox ID=txtTaxObject maxlength=256 width=90% Runat=server />
                                                        <asp:label id=lblerrTaxObject Visible=False Text="Tax object cannot be empty" forecolor=red Runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td width="20%" height=25>Rate (with NPWP) :* </td>
                                        <td colSpan="5"><asp:TextBox ID=txtWRate maxlength=5 width=10% text=0 Runat=server /> 
                                            <asp:label id=lblWPercent text="%" Runat="server" />
                                            <asp:RegularExpressionValidator id=RegularExpressionValidator1 												
                                                ControlToValidate="txtWRate"												
                                                ValidationExpression="\d{1,2}\.\d{0,2}|\d{1,2}"
                                                Display="Dynamic"
                                                text="Maximum length 2 digits and 2 decimal points."
                                                runat="server"/>
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                                                display="dynamic" 
                                                ControlToValidate="txtWRate"
                                                Text="Please enter rate" />
                                            <asp:Label id=lblErrWRate visible=false forecolor=red text=" Please enter tax rate." runat=server />
                                        </td>
                                    </tr>
                                    <tr class="mb-c">
                                        <td width="20%" height=25>Rate (without NPWP) :* </td>
                                        <td colSpan="5"><asp:TextBox ID=txtWORate maxlength=5 width=10% text=0 Runat=server /> 
                                            <asp:label id=lblWOPercent text="%" Runat="server" />
                                            <asp:RegularExpressionValidator id=RegularExpressionValidator3 												
                                                ControlToValidate="txtWORate"												
                                                ValidationExpression="\d{1,2}\.\d{0,2}|\d{1,2}"
                                                Display="Dynamic"
                                                text="Maximum length 2 digits and 2 decimal points."
                                                runat="server"/>
                                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" 
                                                display="dynamic" 
                                                ControlToValidate="txtWORate"
                                                Text="Please enter rate" />
                                            <asp:Label id=lblErrWORate visible=false forecolor=red text=" Please enter tax rate." runat=server />
                                        </td> 
                                    </tr>
    			                        
                                    <tr class="mb-c">
                                        <td width="20%" height=25>Additional Note : </td>
                                        <td colSpan="5"><asp:TextBox ID=txtAddNote maxlength=256 width=90% Enabled=true Runat=server /></td>
                                    </tr>
                                    
                                    <tr class="mb-c">
                                        <td colSpan="6">&nbsp;</td>
                                    </tr>	
                                    <tr class="mb-c">
	                                    <td height=25 colspan=2>
		                                    <asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add CausesValidation=True onclick=AddBtn_Click UseSubmitBehavior="false" runat=server /> 									
		                                    <asp:label id=lblErrRate Visible=False Text="Both rate cannot be empty or zero value" forecolor=red Runat="server" />
	                                    </td>
	                                    <td>&nbsp;</td>
	                                    <td>&nbsp;</td>
	                                    <td>&nbsp;</td>
	                                    <td>&nbsp;</td>
                                    </tr>
                                    <tr class="mb-c">
	                                    <td height=25 colspan=2>
	                                    </td>
	                                    <td>&nbsp;</td>
	                                    <td>&nbsp;</td>
	                                    <td>&nbsp;</td>
	                                    <td>&nbsp;</td>
                                    </tr>
                                </table>
							    <!--</td>
						    </tr>
					    </table>-->
				    </td>
			    </tr>
                </table>

                <table style="width: 100%" class="font9Tahoma">
			    <tr>
				    <td>
					    <asp:DataGrid id=dgLineDet
						    AutoGenerateColumns="false" width="100%" runat="server"
						    GridLines=none
						    Cellpadding="2"
						    Pagerstyle-Visible="False"
						    OnEditCommand="DEDR_Edit"
						    OnCancelCommand="DEDR_Cancel"
						    OnDeleteCommand="DEDR_Delete"
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
							    <asp:TemplateColumn HeaderText="Tax Object" ItemStyle-Width="15%">
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# Container.DataItem("TaxObject") %> id="lblTaxObject" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    
							    <asp:TemplateColumn HeaderText=" Rate <br>With NPWP" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("WRate"), 2), 2) %> id="lblIDWRate" runat="server" />
										    <asp:Label Text=<%# FormatNumber(Container.DataItem("WRate"), 2) %> id="lblWRate" visible = False runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>		
							    <asp:TemplateColumn HeaderText=" Rate <br>Without NPWP" HeaderStyle-HorizontalAlign=Right ItemStyle-Width="8%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("WORate"), 2), 2) %> id="lblIDWORate" runat="server" />
										    <asp:Label Text=<%# FormatNumber(Container.DataItem("WORate"), 2) %> id="lblWORate" visible = False runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>		
							    
							    <asp:TemplateColumn HeaderText="Additional Note" ItemStyle-Width="35%">
								    <ItemTemplate>
									    <asp:Label visible=true Text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
    														
							    <asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate>
								        <asp:label text= '<%# Container.DataItem("TrxLnID") %>' Visible=False id="lblTrxLnID" runat="server" />
									    <asp:LinkButton id=lbDelete CommandName="Delete" Text="Delete" CausesValidation=False runat=server />
									    <asp:LinkButton id=lbEdit CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
								        <asp:LinkButton id=lbCancel CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
								    </ItemTemplate>
							    </asp:TemplateColumn>	
					    </Columns>										
					    </asp:DataGrid>
				    </td>
			    </tr>
    			
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<tr>
					<td>
                                            &nbsp;</td>
				</tr>
			</table>


        <br />
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
