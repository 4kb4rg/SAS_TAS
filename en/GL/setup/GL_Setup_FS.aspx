<%@ Page Language="vb" codefile="../../../include/GL_Setup_FS.aspx.vb" Inherits="GL_Setup_FS" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGL" src="../../menu/menu_GLSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html>
	<head>
		<title>Financial Statement Report Template Detail</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id="PrefHdl" runat="server" />
		<body>
		<form id=frmTmpl class="main-modul-bg-app-list-pu"  runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist"> 

			<asp:label id="lblErrMessage" visible=false text="Error while initiating component." runat=server />
			<asp:label id="lblAccount" visible=false runat=server />
			<asp:label id=lblTxLnID visible=false runat=server />
			
			<table border="0" cellspacing="1" cellpadding="2" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuGL id=MenuGL runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="3"> <strong>FINANCIAL STATEMENT REPORT TEMPLATE DETAIL</strong> </td>
				</tr>
				<tr>
					<td colspan="6">
                    <hr style="width :100%" />
                    </td>
				</tr>
				
				<tr>
					<td height="15" width = 20% >Report Code :*</td>
					<td width = "75%"  ><asp:textbox id="txtCode" width="30%" maxlength="32" runat="server" CssClass="fontObject" />
						<asp:RequiredFieldValidator id="rfvCode" runat="server" ErrorMessage="Required" ControlToValidate="txtCode"
							display="dynamic" />
					</td>
					<td width = "1%">&nbsp;</td>
					<td width = "1%">&nbsp;</td>
					<td width = "1%">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="15">Name :*</td>
					<td><asp:textbox id="txtName" width="80%" maxlength="32" runat="server"  CssClass="fontObject"/>
						<asp:RequiredFieldValidator id="rfvName" runat="server" ErrorMessage="Required" ControlToValidate="txtName"
							display="dynamic" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="15">Description :</td>
					<td><asp:textbox id="txtDescription" width="80%" maxlength="50" runat="server"  CssClass="font9Tahoma"/>
						
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="15">Foreign Description :</td>
					<td><asp:textbox id= "txtDescription3" width="80%" maxlength="50" runat="server"  CssClass="fontObject"/>
						
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				
				<tr>
					<td>Report Type :*</td>
					<td>
						<asp:DropDownList id="ddlReportType" width="30%" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_OnSelectedIndexChanged" CssClass="fontObject"/>
						<asp:Label id="lblErrReportType" visible="false" forecolor="red" text="<br>Please select one of Report Type." runat=server/>
    				</td>
				</tr>
				
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
			</table>
			
			
			<table border="0" cellspacing="1" cellpadding="2" width="100%">
			<tr>
				<td colspan="5">
					<table id="tblSelection" border="0" width="100%" cellspacing="0" cellpadding="2" runat="server">
						
						<tr style="height:15px" class="mb-c">
							<td style="width:20%; height: 15px;">
                                Seq :*</td>
							<td colspan="3" style="height: 15px">
                               <asp:Textbox id="txtDispSeq1"  Width="10%" maxlength="8"  text = "1" runat="server"  CssClass="fontObject"/>
                               <asp:RangeValidator id="rfvDispSeq1"
								ControlToValidate="txtDispSeq1"
								MinimumValue="0"
								MaximumValue="9999"
								Type="Integer"
								EnableClientScript="True"
								Text="Sequence must be in positive number."
								runat="server" 
								display="dynamic"/>	 
                                <asp:Label id="lblRowID" forecolor="Red" visible="False" runat="server" />
                             </td>
						</tr>
						<tr style="height:15px" class="mb-c" runat="server" id="trGroupID" visible="false">
							<td style="width:20%; height: 15px;">
                                Station :</td>
							<td colspan="3">
                                            <telerik:RadComboBox CssClass="fontObject" ID="radGroupID"
                                                runat="server" AllowCustomText="True" EmptyMessage="Please Select"
                                                Height="200" Width="70%" ExpandDelay="50" Filter="Contains" Sort="Ascending"
                                                EnableVirtualScrolling="True">
                                                <CollapseAnimation Type="InQuart" />
                                            </telerik:RadComboBox>

                                
                             </td>
						</tr>
						
						<tr style="height:15px" class="mb-c">
							<td style="width:20%; height: 15px;">
                                Description :*</td>
							<td colspan="3">
                                <asp:Textbox id="txtDescription1"  Width="70%" maxlength="128"  runat="server"  CssClass="fontObject" />
                             </td>
						</tr>
						
						<tr style="height:15px" class="mb-c">
							<td style="width:20%; height: 15px;">
                                Foreign Description :</td>
							<td colspan="3">
                                <asp:Textbox id="txtDescription2"  Width="70%" maxlength="128"  runat="server"  CssClass="fontObject"   />
                             </td>
						</tr>
							
						 <tr style="height:15px" class="mb-c">
				            <td >
                                Display Type :*</td>
				            <td colspan="3">
				               <asp:DropDownList id="ddlFSRowType1" AutoPostback="true" Width="10%" OnSelectedIndexChanged="ddlCheckType1" runat="server"  CssClass="fontObject">
				                 <asp:ListItem value="1" Selected = "True" >Entry</asp:ListItem>
						        <asp:ListItem value="2">Header</asp:ListItem>
						        <asp:ListItem value="3">Formula</asp:ListItem>
						        <asp:ListItem value="4">Sub Entry</asp:ListItem>
						         <asp:ListItem value="5">Sub Formula</asp:ListItem> 
				               </asp:DropDownList>
                                &nbsp; &nbsp;
                                <asp:Label id="lblRefNo1" text="Ref. No :* " runat="server" visible="true"/>
                                &nbsp; &nbsp;
                                <asp:Textbox id="txtRefNo1"  Width="7%" maxlength="22"  runat="server" visible="true"  CssClass="fontObject"/>
                                &nbsp; &nbsp;
                                <asp:Label id="lblRefNo2" text="Reverse Ref. No : " runat="server" visible="true"/>
                                &nbsp; &nbsp;
                                <asp:Textbox id="txtRefNo2"  Width="7%" maxlength="22"  runat="server" visible="true"  CssClass="fontObject"/>
                                &nbsp; &nbsp;
                                <asp:Label id="lblFormula1" text="Formula :* " runat="server" visible="false"/>
                                &nbsp; &nbsp;
                                <asp:Textbox id="txtFormula1"  Width="60%" maxlength="2000"  runat="server" visible="false"  CssClass="fontObject"/>
                                &nbsp; &nbsp;
                                <asp:CheckBox ID="chkBegBal" runat="server" visible="true" Text=" Is Beg. Balance"  CssClass="font9Tahoma"/>
                                </td>
                                
			            </tr>
						
								
						<tr id ="trStyle" style="height:15px" class="mb-c">
							<td style="width:20%; height: 15px;">
                            </td>
							<td colspan="3"> Space :    
                                <asp:DropDownList id="ddlSpace1" Width="5%"  runat="server"  CssClass="fontObject">
                                    <asp:ListItem value="0" Selected = "True" >0</asp:ListItem>
                                    <asp:ListItem value="1">1</asp:ListItem>
                                    <asp:ListItem value="2">2</asp:ListItem>
                                    <asp:ListItem value="3">3</asp:ListItem>
                                    <asp:ListItem value="4">4</asp:ListItem> 
                                    <asp:ListItem value="5">5</asp:ListItem> 
                                    <asp:ListItem value="6">6</asp:ListItem> 
                                    <asp:ListItem value="7">7</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                                <asp:CheckBox ID="chkUnderline" runat="server" visible="true" Text=" Underline"/>
                                &nbsp;
                                <asp:CheckBox ID="chkBold" runat="server" visible="true" Text=" Bold"/>
                                &nbsp;
                                Font : 
                                <asp:DropDownList id="ddlFont" Width="5%"  runat="server"  CssClass="fontObject">
                                    <asp:ListItem value="7">7</asp:ListItem>
                                    <asp:ListItem value="8">8</asp:ListItem>
                                    <asp:ListItem value="9">9</asp:ListItem>
                                    <asp:ListItem value="10" Selected = "True" >10</asp:ListItem>
                                    <asp:ListItem value="11">11</asp:ListItem> 
                                    <asp:ListItem value="12">12</asp:ListItem> 
                                    <asp:ListItem value="13">13</asp:ListItem> 
                                    <asp:ListItem value="14">14</asp:ListItem>
                                    <asp:ListItem value="14">15</asp:ListItem>
                                </asp:DropDownList>
				            </td>
						</tr>
						<tr id ="trEditor" style="height:15px" class="mb-c">
						    <td>Font Editor :*</td>
						    <td colspan="3">    
						        Size:&nbsp;
						        <asp:DropDownList id="ddlFSize" Width="5%"  runat="server"  CssClass="fontObject">
                                    <asp:ListItem value="7">7</asp:ListItem>
                                    <asp:ListItem value="8">8</asp:ListItem>
                                    <asp:ListItem value="9">9</asp:ListItem>
                                    <asp:ListItem value="10" Selected = "True" >10</asp:ListItem>
                                    <asp:ListItem value="11">11</asp:ListItem> 
                                    <asp:ListItem value="12">12</asp:ListItem> 
                                    <asp:ListItem value="13">13</asp:ListItem> 
                                    <asp:ListItem value="14">14</asp:ListItem>
                                    <asp:ListItem value="14">15</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                                Style:&nbsp;
                                <asp:DropDownList id="ddlFStyle" Width="10%"  runat="server"  CssClass="fontObject">
                                    <asp:ListItem value="0" Selected="True">Regular</asp:ListItem>
                                    <asp:ListItem value="2">Italic</asp:ListItem>
                                    <asp:ListItem value="1">Bold</asp:ListItem>
                                    <asp:ListItem value="3">Bold Italic</asp:ListItem>
                                </asp:DropDownList> 
                                &nbsp;
                                Effect:&nbsp;
                                <asp:DropDownList id="ddlFEffect" Width="10%"  runat="server"  CssClass="fontObject">
                                    <asp:ListItem value="0" Selected="True">N/A</asp:ListItem>
                                    <asp:ListItem value="1">Underline</asp:ListItem>
                                    <asp:ListItem value="2">Strikeout</asp:ListItem>
                                </asp:DropDownList> 
                                &nbsp;
                                Spacing:&nbsp;
                                <asp:DropDownList id="ddlFSpacing" Width="5%"  runat="server"  CssClass="fontObject">
                                    <asp:ListItem value="0" Selected="True">0</asp:ListItem>
                                    <asp:ListItem value="1">1</asp:ListItem>
                                    <asp:ListItem value="2">2</asp:ListItem>
                                    <asp:ListItem value="3">3</asp:ListItem>
                                    <asp:ListItem value="4">4</asp:ListItem> 
                                    <asp:ListItem value="5">5</asp:ListItem> 
                                    <asp:ListItem value="6">6</asp:ListItem> 
                                    <asp:ListItem value="7">7</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;
                                Negative sign for display:&nbsp;
                                <asp:DropDownList id="ddlFNegSign" Width="5%"  runat="server"  CssClass="fontObject">
                                    <asp:ListItem value="0">No</asp:ListItem>
                                    <asp:ListItem value="1" Selected="True">Yes</asp:ListItem>
                                </asp:DropDownList>
						    </td>
						</tr>
						
						<tr class="mb-c">
						    <td colspan="4">
							    <asp:Label id="lblReqField" text="Please Fill Required Field" forecolor="red" visible="false" runat="server" />
						     </td>
						</tr>	
						
						<tr class="mb-c">
							<td colspan="2"><asp:ImageButton AlternateText="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" /> &nbsp;
										  <asp:ImageButton AlternateText="Save" id="Update" visible=false ImageURL="../../images/butt_save.gif" OnClick="btnAdd_Click" Runat="server" />&nbsp;&nbsp;</td>
							<td>&nbsp;</td>						
							<td>&nbsp;</td>	
						</tr>			
									
					</table>
				</td>		
			</tr>
			</table>
			
			
			<table border=0 cellspacing=1 cellpadding=2 width=100%>
				<tr>
					<td colspan=6 align=left>					
					<asp:DataGrid id="TmplList"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						OnItemDataBound="DataGrid_ItemCreated"
                     
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
						OnDeleteCommand="DEDR_Delete" >
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />	
  					
					<Columns>
					<asp:TemplateColumn HeaderText="No.">
						<ItemStyle width="5%" />
						<ItemTemplate>
							<asp:label id="lblNo" runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Seq.">
						<ItemStyle Width="3%" />
						<ItemTemplate>
							<asp:Label id="lblDispSeq" text=<%# Container.DataItem("DispSeq") %> runat="server" />
							<asp:Label id="lblFBold" Visible="false" text=<%# Container.DataItem("FBold") %> runat="server" />
							<asp:Label id="lblSpace" Visible="false" text=<%# Container.DataItem("FSpace") %> runat="server" />
 
						</ItemTemplate>
					</asp:TemplateColumn>
						
					<asp:TemplateColumn HeaderText="Description">
						<ItemStyle Width="35%"/>
						<ItemTemplate>
							<asp:Label id=lblDescription Text=<%#Container.DataItem("DescriptionX")%> runat="server" />					
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText ="Display <br/> Type" >
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<%# objGLSetup.mtdGetFSRowType(Container.DataItem("RowType")) %>
							<asp:Label id="lblFSRowType" Text=<%#Container.DataItem("RowType")%> visible=false runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText ="Reference" >
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<%# Container.DataItem("Refno") %>
							<asp:Label id="lblRefNoRvs" Text=<%#Container.DataItem("RefNoRvs")%> visible=false runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
	
					<asp:TemplateColumn HeaderText="Formula" >
						<ItemStyle Width="20%" />
						<ItemTemplate>
							<%# Container.DataItem("Formula") %>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Beg <br/> Balance" >
						<ItemStyle Width="7%" />
						<ItemTemplate>
							<asp:CheckBox id="chkBegBalance1" Checked= <%#Container.DataItem("BegBalance")%>  runat="server" Enabled ="false"/>
						</ItemTemplate>
					</asp:TemplateColumn>

					<asp:TemplateColumn HeaderText="Station">
						<ItemStyle Width="10%"/>
						<ItemTemplate>
							<asp:Label id=lblDescriptiond Text=<%#Container.DataItem("StationName")%> runat="server" />					
						</ItemTemplate>
					</asp:TemplateColumn>
  					
					<asp:TemplateColumn HeaderText="Account">
						<ItemStyle Width="7%"/>
						<ItemTemplate>
							<asp:Button id=btnAccount text="view" CssClass="font9Tahoma" commandargument=<%#Container.DataItem("RowId")%> commandname=<%#Container.DataItem("Description")%> OnClick="onClick_Account" visible=false runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>
					
					<asp:TemplateColumn>		
						<ItemStyle width="5%" HorizontalAlign="center" />							
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("RowID") %> Visible=False id="lblRowId" runat="server" />
							<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" />
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label text=<%# Container.DataItem("RowID") %> Visible=False id="lblRowId" runat="server" />
							<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						</EditItemTemplate>
					</asp:TemplateColumn>
					
					</Columns>
					</asp:DataGrid>
					</td>
					</tr>
					
				<tr>
					<td align="left" ColSpan="6">
						<b><asp:label id=lblErrCalc runat="server"/></b>
					</td>
				</tr>
				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<TD colSpan="6">
						<asp:ImageButton ID="SaveBtn" CausesValidation="True" onclick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif"
							AlternateText="Save" Runat="server" />
						<asp:ImageButton ID="DeleteBtn" CausesValidation="False" Visible = "False" onclick="DeleteBtn_Click" ImageUrl="../../images/butt_delete.gif"
							AlternateText="Delete" Runat="server" />
						<asp:ImageButton ID="BackBtn" CausesValidation="False" onclick="BackBtn_Click" ImageUrl="../../images/butt_back.gif"
							AlternateText="Back" Runat="server" />
					&nbsp;</TD>
				</tr>
				
				
								
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
		</body>
</html>
