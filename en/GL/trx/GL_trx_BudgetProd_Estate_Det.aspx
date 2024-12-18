<%@ Page Language="vb" Src="../../../include/GL_trx_BudgetProd_Estate_Det.aspx.vb" Inherits="GL_trx_BudgetProd_Estate_Det" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>General Ledger - Budget Details</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
        
                 <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 


			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server"/>
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table cellspacing="0" class="font9Tahoma" cellpadding="0" width="100%" border="0" id="TABLE1"">
 			 
				<tr>
					<td class="mt-h" colspan="7">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong> BUDGET PRODUKSI ESTATE DETAILS</strong> </td>
                                <td class="font9Header" style="text-align: right">
                        Create Date&nbsp; :
                        <asp:Label id="lblCreateDate" runat="server"/>&nbsp;|
                        Create By&nbsp; :
                        <asp:Label id="lblUpdateBy" runat="server"/></td>
                            </tr>
                        </table>
                    </td>
				</tr>				
				<tr>
					<td colspan="7" >&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="2" style="height: 25px">
                        Periode :</td>
					<td colspan="2" style="height: 25px">
                        &nbsp;<asp:TextBox runat="server" ID="txtYearBudget" MaxLength="4" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox>
							<asp:RequiredFieldValidator id="reqYear" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key Budget Year" 
							ControlToValidate="txtYearBudget" /></td>
					<td colspan="2" style="height:25px">
                        &nbsp;</td>
					<td style="height: 25px; width:15%"></td>
				</tr>
				
				<tr>
					<td  colspan="2" style="height:25px">Tahun Tanam :</td>
					<td  colspan="2" style="height:25px">&nbsp;<asp:DropDownList ID="ddlGroupCOA" runat="server" OnSelectedIndexChanged="ddlGroupCOA_OnSelectedIndexChanged" AutoPostBack="true" Width="90%">
                        </asp:DropDownList></td>
					<td colspan="2" style="height: 25px">
                        &nbsp;</td>
					<td style="height: 25px"></td>
				</tr>
				
				<tr>
					<td  colspan="2" style="height:25px">Blok :</td>
					<td  colspan="2" style="height:25px">&nbsp;<asp:DropDownList ID="ddlSubBlok" runat="server" AutoPostBack="false" Width="90%">
                        </asp:DropDownList></td>
					<td colspan="2" style="height: 25px"></td>
					<td style="height: 25px"></td>
				</tr>
				
				
				<tr>
					<td  colspan="2" style="height:25px">Komoditi :</td>
					<td  colspan="2" style="height:25px">&nbsp;<asp:DropDownList ID="ddlKomoditi" runat="server" AutoPostBack="false" Width="90%">
					        <asp:ListItem Value="" Text="-"></asp:ListItem>
                            <asp:ListItem Value="SWT" Text="Sawit"></asp:ListItem>
                            <asp:ListItem Value="KRT" Text="Karet"></asp:ListItem>
                        </asp:DropDownList>
					<asp:ImageButton id="btnAdd" imageurl="../../images/butt_add.gif" AlternateText="  Add  " onclick="btnAdd_Click" runat="server" />
					&nbsp;</td>
					<td colspan="2" style="height: 25px">
					</td>
					<td style="height: 25px"></td>
					
				</tr>
				
				<tr>
				    <td colspan="7" style="height: 2px">&nbsp;<asp:Label id="lblValueError" visible="false" forecolor="red" runat="server"/></td>
				</tr>
				
                <tr>
					<td colspan="7" style="height:25px">&nbsp;</td>
				</tr>

				
				 <tr valign="top">
                    <td colspan="7" style="height:600px" class="mb-c" >
					<div id="divgd" style="width:100%;height=100%;overflow: auto;">
					<asp:DataGrid ID="dgBudget" 
                    AutoGenerateColumns="False" runat="Server" 
					OnItemCreated="dgBudget_ItemCreated"
					OnItemDataBound="dgBudget_BindGrid" 
					OnDeleteCommand="dgBudget_Delete"  
                    GridLines="Both" CellPadding="0" 
					width=150% class="font9Tahoma"
                    >
                    <AlternatingItemStyle CssClass="mr-r" />
                    <ItemStyle CssClass="mr-l" />
                    <HeaderStyle CssClass="mr-h" />

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
                            <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                            <asp:HiddenField ID="dgBudget_hid_trxid" Value='<%# Container.DataItem("trxid") %>' runat="Server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation="False"  runat="server" />
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Blok">
                        <ItemTemplate>
                            <asp:Label ID="dgBudget_blok" Width="60px" Text='<%# Container.DataItem("CodeSubBlok") %>' runat="server" />
                        </ItemTemplate>
       
                    </asp:TemplateColumn>
                                                        
                    <asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B01" Width="100%" Text='<%# Container.DataItem("B01T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
		                 
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R01" Width="100%" Text='<%# Container.DataItem("R01T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                                                        
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B02" Width="100%" Text='<%# Container.DataItem("B02T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R02" Width="100%" Text='<%# Container.DataItem("R02T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B03" Width="100%" Text='<%# Container.DataItem("B03T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R03" Width="100%" Text='<%# Container.DataItem("R03T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B04" Width="100%" Text='<%# Container.DataItem("B04T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R04" Width="100%" Text='<%# Container.DataItem("R04T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B05" Width="100%" Text='<%# Container.DataItem("B05T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R05" Width="100%" Text='<%# Container.DataItem("R05T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B06" Width="100%" Text='<%# Container.DataItem("B06T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R06" Width="100%" Text='<%# Container.DataItem("R06T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B07" Width="100%" Text='<%# Container.DataItem("B07T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R07" Width="100%" Text='<%# Container.DataItem("R07T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B08" Width="100%" Text='<%# Container.DataItem("B08T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R08" Width="100%" Text='<%# Container.DataItem("R08T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B09" Width="100%" Text='<%# Container.DataItem("B09T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R09" Width="100%" Text='<%# Container.DataItem("R09T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B10" Width="100%" Text='<%# Container.DataItem("B10T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R10" Width="100%" Text='<%# Container.DataItem("R10T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B11" Width="100%" Text='<%# Container.DataItem("B11T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R11" Width="100%" Text='<%# Container.DataItem("R11T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>
					
					<asp:TemplateColumn HeaderText="Bud">
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_B12" Width="100%" Text='<%# Container.DataItem("B12T")%>' style="text-align:right;" 
                             onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>                                                                      
                    <asp:TemplateColumn HeaderText="Rel" >
                        <ItemTemplate>
                            <asp:TextBox ID="dgBudget_R12" Width="100%" Text='<%# Container.DataItem("R12T")%>' style="text-align:right;" 
                            onkeypress="javascript:return isNumberKey(event)" runat="server" />
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>
					
					
                    
					</Columns>
					</asp:DataGrid></div>
					</td>
				</tr>
                
				<tr>
					<td colspan="7" style="height:25px">&nbsp;</td>
				</tr>
								
				<tr>
					<td colspan="7">
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />&nbsp;
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="False" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />
					</td>
				</tr>
			</table>
            </div>
            </td>
            </tr>
            </table>
         </form>
	</body>
</html>
