<%@ Page Language="vb" src="../../../include/HR_Setup_Evaluation.aspx.vb" Inherits="HR_Setup_Evaluation" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Evaluation List</title>
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


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>EVALUATION LIST</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td width="15%" height="26" valign=bottom>Evaluation Code :<BR><asp:TextBox id=srchEvalCode width=100% maxlength="8" runat="server"/></td>
								<td width="40%" height="26" valign=bottom>Description :<BR><asp:TextBox id=srchDesc width=100% maxlength="128" runat="server"/></td>
								<td width="15%" height="26" valign=bottom>Status :<BR><asp:DropDownList id="srchStatusList" width=100% runat=server /></td>
								<td width="20%" height="26" valign=bottom>Last Updated By :<BR><asp:TextBox id=srchUpdateBy width=100% maxlength="128" runat="server"/></td>
								<td width="10%" height="26" valign=bottom align=right><asp:Button ID="Button1" Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
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
					
					                <asp:TemplateColumn HeaderText="Evaluation Code" SortExpression="EvalCode" ItemStyle-Width="11%" HeaderStyle-VerticalAlign=Bottom>
						                <ItemTemplate>
							                <%# Container.DataItem("EvalCode") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="EvalCode" MaxLength="8" width=95%
								                Text='<%# trim(Container.DataItem("EvalCode")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								                ErrorMessage="Please Enter Evaluation Code"
								                ControlToValidate=EvalCode />
							                <asp:RegularExpressionValidator id=revCode 
								                ControlToValidate="EvalCode"
								                ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								                Display="Dynamic"
								                text="Alphanumeric without any space in between only."
								                runat="server"/>
							                <asp:label id="lblDupMsg" Text="Code already exist" Visible=false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Description" SortExpression="EVAL.Description" ItemStyle-Width="25%" HeaderStyle-VerticalAlign=Bottom>
						                <ItemTemplate>
							                <%# Container.DataItem("Description") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="Description" width=100% MaxLength="64"
								                Text='<%# trim(Container.DataItem("Description")) %>' runat="server"/>
							                <asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
								                ErrorMessage="Please Enter Evaluation Description"
								                ControlToValidate=Description />
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Bonus Rate" SortExpression="EVAL.IncRate" ItemStyle-Width="13%" HeaderStyle-VerticalAlign=Bottom >
						                <ItemTemplate>
							                <%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("IncRate"))%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="IncRate" width=100% MaxLength="22"
								                Text='<%# trim(Container.DataItem("IncRate")) %>' runat="server"/>  
							                <asp:RequiredFieldValidator id=validateIncRate display=Dynamic runat=server 
								                ErrorMessage="Please Enter Increment Rate."
								                ControlToValidate="IncRate"/>
							                <asp:CompareValidator id="cvValidateRate" display=dynamic runat="server" 
								                ControlToValidate="IncRate" Text="The value must whole number." 
								                Type="Double" Operator="DataTypeCheck"/>							
							                <asp:RangeValidator id="rvIncRate"
							                ControlToValidate="IncRate"
							                MinimumValue="0"
							                MaximumValue="9999999999999999999.99"
							                Type="Double"
							                display=dynamic 
							                EnableClientScript="True"
							                Text="Exceeded Value"
							                runat="server"/>							
							                <asp:Label id=lblRateErr forecolor=red runat=server/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Bonus Amount" SortExpression="EVAL.IncAmt" ItemStyle-Width="13%" HeaderStyle-VerticalAlign=Bottom>
						                <ItemTemplate>
							                <%# ObjGlobal.GetIDDecimalSeparator(Container.DataItem("IncAmt"))%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="IncAmt" width=100% MaxLength="22"
								                Text='<%# Container.DataItem("IncAmt") %>' runat="server"/> 
							                <asp:RequiredFieldValidator id=validateIncAmt display=Dynamic runat=server 
								                ErrorMessage="Please Enter Increment Amount."
								                ControlToValidate="IncAmt" />
							                <asp:CompareValidator id="cvValidateAmt" display=dynamic runat="server" 
								                ControlToValidate="IncAmt" Text="The value must whole number." 
								                Type="Double" Operator="DataTypeCheck"/>							
							                <asp:RangeValidator id="rvIncAmt"
							                ControlToValidate="IncAmt"
							                MinimumValue="0"
							                MaximumValue="9999999999999999999.99"
							                Type="Double"
							                EnableClientScript="True"
							                Text="Exceeded Value"
							                display=dynamic 
							                runat="server"/>							
							                <asp:Label id=lblAmtErr forecolor=red runat=server/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Last Update" SortExpression="EVAL.UpdateDate" ItemStyle-Width="8%" HeaderStyle-VerticalAlign=Bottom>
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
					                <asp:TemplateColumn HeaderText="Status" SortExpression="EVAL.Status" ItemStyle-Width="8%" HeaderStyle-VerticalAlign=Bottom>
						                <ItemTemplate>
							                <%# objHR.mtdGetFunctionStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							                <asp:TextBox id="Status" Readonly=TRUE Visible = False
								                Text='<%# Container.DataItem("Status")%>' runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName" ItemStyle-Width="12%" HeaderStyle-VerticalAlign=Bottom>
						                <ItemTemplate>
							                <%# Container.DataItem("UserName") %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="UserName" Readonly=TRUE size=8 
								                Text='<%# Session("SS_USERID") %>' Visible=False runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn ItemStyle-Width="10%" ItemStyle-HorizontalAlign=center>					
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
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
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
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Evaluation Code" runat="server"/>
					        	<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
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


			<asp:Label id=lblErrIncMessage visible=false text="Please enter either increment rate or, increment amount." runat=server/>
			<asp:Label id=lblErrRateMessage visible=false text="Increment Rate should not exceeded 100 percent." runat=server/>
			</Form>
		</body>
</html>
