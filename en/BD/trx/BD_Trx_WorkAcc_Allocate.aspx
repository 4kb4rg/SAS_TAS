<%@ Page Language="vb" trace="false" src="../../../include/BD_Trx_WorkAcc_Allocate.aspx.vb" Inherits="BD_WorkAcc_Allocate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBDtrx" src="../../menu/menu_BDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Working Account Allocation</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
<%--		<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuBDtrx id=MenuBDtrx runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
					<tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td class="mt-h" colspan="5">WORKING ACCOUNT ALLOCATION</td>
                        </table></td>
				    </tr>
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td colspan=6><hr size="1" noshade></td>
                        </table></td>
				    </tr>
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td height=25><asp:label id="lblWorkAccIDTag" Text="Working Account ID" runat="server" /> :</td>
					    <td><asp:Label id=lblWorkAccID runat=server/></td>
					    <td>&nbsp;</td>
					    <td><asp:label id="lblBgtTag" text="Budgeting Period" runat="server"/> :</td>
					    <td><asp:Label id=lblBgtPeriod runat=server /></td>
                        </table></td>
				    </tr>
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td height=25><asp:label id="lblWorkAccNameTag" Text="Working Account Name" runat="server" /> :</td>
					    <td><asp:Label id=lblWorkAccName runat=server/></td>
					    <td>&nbsp;</td>
					    <td>Status : </td>
					    <td><asp:Label id=lblStatus runat=server /></td>
                        </table></td>
				    </tr>
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td height=25 width=20%>Total :</td>
					    <td width=30%><asp:Label id=lblTotal runat=server /></td>
					    <td width=5%>&nbsp;</td>
					    <td width=20%>Date Created : </td>
					    <td width=25%><asp:Label id=lblDateCreated runat=server/></td>
                        </table></td>
				    </tr>
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td height=25>&nbsp;</td>
					    <td>&nbsp;</td>
					    <td>&nbsp;</td>
					    <td>Last Updated : </td>
					    <td><asp:Label id=lblLastUpdate runat=server /></td>
                        </table></td>
				    </tr>
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td height=25>&nbsp;</td>
					    <td>&nbsp;</td>
					    <td>&nbsp;</td>
					    <td>Updated By : </td>
					    <td><asp:Label id=lblUpdatedBy runat=server /></td>
                        </table></td>
				    </tr>
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td colspan="5">&nbsp;</td>
                        </table></td>
				    </tr>				
				    <tr>
                        <td class="font9Tahoma"><table cellpadding="4" cellspacing="0" style="width: 100%">
					    <td colspan="5"><asp:Label id=lblErrPcnt forecolor="red" visible=false Text="Total Percentage cannot exceed 100%." runat=server /></td>
                        </table></td>
				    </tr>
						 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr align="center">
                                    <td>
						            <asp:DataGrid id="dgWorkAccAlloc"
						                AutoGenerateColumns="false" width="100%" runat="server"
						                GridLines = none
						                Cellpadding = "2" 
						                OnEditCommand="DEDR_Edit"
						                OnUpdateCommand="DEDR_Update"
						                OnCancelCommand="DEDR_Cancel"
						                OnDeleteCommand="DEDR_Delete"
						                Pagerstyle-Visible="False"
						                OnSortCommand="Sort_Grid" 
						                AllowSorting="false"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>					
					                <Columns>
					
					                <asp:TemplateColumn HeaderText="Allocation Description" SortExpression="WAAllocDesc">
						                <ItemTemplate>
							                <%# Container.DataItem("WAAllocDesc") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:Label id="lblWAAllocID" Text='<%# Container.DataItem("WAAllocID") %>' Visible=False runat="server" />
							                <asp:TextBox id="txtAllocDesc" MaxLength="32" width=95%
									                Text='<%# trim(Container.DataItem("WAAllocDesc")) %>'
									                runat="server"/>
							                <BR>
							                <asp:RequiredFieldValidator id=validateAllocDesc display=dynamic runat=server 
								                text = "Please enter Allocation description"
								                ControlToValidate=txtAllocDesc />
						                </EditItemTemplate>
					                </asp:TemplateColumn>
	
					                <asp:TemplateColumn HeaderText="Percentage (%)" SortExpression="WAAllocPcnt">
						                <ItemTemplate>
							                <%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("WAAllocPcnt"), 0, True, False, False)) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="txtPcnt" width=100% MaxLength="25"
								                Text='<%# FormatNumber(Container.DataItem("WAAllocPcnt"), 0, True, False, False) %>'
								                runat="server"/>
							                <asp:RequiredFieldValidator id=validatePcnt display=dynamic runat=server 
									                text = "Please enter percentage"
									                ControlToValidate=txtPcnt />															
							                <asp:RegularExpressionValidator id="RegExpValPcnt" 
								                ControlToValidate="txtPcnt"
								                ValidationExpression="\d{1,3}"
								                Display="Dynamic"
								                text = "Maximum length 3 digits and 0 decimal points"
								                runat="server"/>
							                <asp:RangeValidator id="RangePcnt"
								                ControlToValidate="txtPcnt"
								                MinimumValue="0"
								                MaximumValue="100"
								                Type="double"
								                EnableClientScript="True"
								                Text="The value is out of range !"
								                runat="server" display="dynamic"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
																							
					                <asp:TemplateColumn>
						                <ItemTemplate>
							                <asp:LinkButton id="lbEdit" CommandName="Edit" Text="Edit" runat="server"/>
						                </ItemTemplate>
						
						                <EditItemTemplate>						
							                <asp:LinkButton id="lbUpdate" CommandName="Update" Text="Save" runat="server"/>
							                <asp:LinkButton id="lbDelete" CommandName="Delete" Text="Delete" runat="server"/>
							                <asp:LinkButton id="lbCancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>							
						                </EditItemTemplate>
					                </asp:TemplateColumn>

					                </Columns>
					                </asp:DataGrid>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
					        <td colspan="5">
						        <asp:ImageButton id=ibNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Allocation" runat="server"/>
						        <asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					        </td>
				        </tr>
				        <asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
                        <tr>
                            <td>
 					            &nbsp;
                                &nbsp;
                              
                            </td>
                        </tr>
					</table>
				</div>
				</td>
		        <table cellpadding="0" cellspacing="0" style="width: 20px">
			        <tr>
				        <td>&nbsp;</td>
			        </tr>
		        </table>
				</td>
			</tr>
		</table>



		</form>
	</body>
</html>
