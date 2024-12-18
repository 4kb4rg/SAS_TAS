<%@ Page Language="vb" src="../../../include/GL_Setup_ActivityDet.aspx.vb" Inherits="GL_Setup_ActivityDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_GLsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Activity Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        
		<script language="Javascript">
			function calQuotaInc() {
				var doc = document.frmMain;
				var quota = parseFloat(doc.txtQuota.value);
				if (quota == 0)
					doc.txtQuotaIncRate.value = 0;
			}
		</script>	
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
             <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<Input Type=Hidden id=tbcode runat=server />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma" >
                                  <strong> <asp:label id="lblTitle" runat="server" /> DETAILS</strong> </td>
                                <td class="font9Header"  style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25><asp:label id="lblActCode" runat="server" /> :* </td>
					<td width=30%>
						<asp:Textbox id=txtActCode width=50% runat=server CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server
								ControlToValidate=txtActCode />	
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtActCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>														
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>This code has been used. Please try again." runat=server/>
						<asp:Label id=lblErrLen visible=false forecolor=red runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblActDesc" runat="server" /> :*</td>
					<td><asp:Textbox id=txtDescription maxlength=128 width=100% runat=server CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server
							ControlToValidate=txtDescription />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblActGrp" runat="server" /> :*</td>
					<td><asp:DropDownList id=ddlActGrpCode width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrActCode visible=false forecolor=red display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Unit of Measurement :*</td>
					<td>
						<asp:DropDownList id=ddlUOM width=100% runat=server CssClass="font9Tahoma"/>
						<asp:Label id=lblErrUOM visible=false forecolor=red text="Please select Unit of Measurement." display=dynamic runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Labour Overhead : </td>
					<td>
						<asp:checkbox id=cbLabourOverhead text=" Yes" runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:label id=lblQuotaInc runat=server /> Rate :</td>
					<td>
						<asp:textbox id=txtQuotaIncRate text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server CssClass="font9Tahoma"/>
						<asp:RegularExpressionValidator 
							id=revQuotaIncRate
							ControlToValidate=txtQuotaIncRate
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 0 decimal points"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Daily Quota : </td>
					<td>
						<asp:textbox id=txtQuota text=0 width=50% maxlength=21 OnKeyUp="javascript:calQuotaInc();" runat=server CssClass="font9Tahoma"/>
						<asp:RegularExpressionValidator 
							id=revQuota
							ControlToValidate=txtQuota
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points. "
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>Quota is measured by : </td>
					<td>
						<asp:radiobutton id=rbByHour text=" Hour" checked=true groupname=QuotaMethod runat=server />
						<comment>START BY ALIM - QA Minamas Phase 1 </comment>	
						<asp:radiobutton id=rbByVolume text=" Volume" groupname=QuotaMethod runat=server />	
					</td>
					<td>&nbsp;</td
				</tr>
				<tr>
					<td colspan="6">
						<TABLE id="tblSelection" width="100%" class="mb-c" cellspacing="0" cellpadding="6" border="0" align="center" runat=server>
							<tr>						
								<td>
									<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
										<TR class="mb-c">
											<TD width=20% height=25><asp:label id="lblAccClass" runat="server" /> :</TD>
											<TD width=80%>
												<asp:DropDownList id=ddlAccCls width=100% runat=server CssClass="font9Tahoma"/>
												<asp:Label id=lblErrAccCls visible=false forecolor=red runat=server/>
											</TD>
										</TR>
										<TR class="mb-c">
											<TD vAlign="top" colspan=2 height=25><asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />&nbsp;<asp:Button 
                                    ID="Issue10" class="button-small" 
                        runat="server" Text="Add"  />
					                        </TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</TABLE>
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
							AllowSorting="True" class="font9Tahoma">
							
							<HeaderStyle CssClass="mr-h"/>
							<ItemStyle CssClass="mr-l"/>
							<AlternatingItemStyle CssClass="mr-r"/>
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
								<asp:TemplateColumn ItemStyle-Width="35%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("AccClsCode") %> id="lblCode" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn ItemStyle-Width="55%">
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=Right>
									<ItemTemplate>
										<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
			</table>
                            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
