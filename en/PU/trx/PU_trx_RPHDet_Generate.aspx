<%@ Page Language="vb" src="../../../include/PU_trx_RPHDet_Generate.aspx.vb" Inherits="PU_trx_RPHDet_Generate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINTrx" src="../../menu/menu_INtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>


<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Purchase Requisition Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<form id=frmPurReqDet class="main-modul-bg-app-list-pu"  runat=server>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>


        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  

		<input type=hidden id=hidPPN1 value=0 runat=server/>	
		<input type=hidden id=hidPPN2 value=0 runat=server/>	
		<input type=hidden id=hidPPN3 value=0 runat=server/>	
		<table border="0" width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /><asp:Label id=PRType Visible="False" runat=server /><asp:label id=lblStatus visible=false runat=server /><asp:label id=lblPrintDate visible=false runat=server /><asp:label id="SortExpression" Visible="False" Runat="server" /><input type=hidden id=hidPQID runat=server />
			<tr>
				<td colspan="6"><UserControl:MenuINTrx id=menuIN runat="server" /></td>
			</tr>
			<tr>
				<td class="font9Tahoma" colspan="6" style="height: 21px">
                  <strong>  DTH DETAIL GENERATE </strong></td>
			</tr>
			<tr>
				<td colspan=6><hr style="width :100%" /></td>
			</tr>			
			<tr>
				<td height="25" style="width: 255px">
                    DTH&nbsp;ID :</td>
				<td style="width: 1168px"><asp:label id=lblPurReqID Runat="server"/></td>
				<td style="width: 7px">&nbsp;</td>
				<td style="width: 155px"> </td>
				<td style="width: 255px"></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td style="width: 255px; height: 20px;" valign="middle">
                                            Purchase Requtition Number :</td>
				<td style="width: 1168px; height: 20px;" valign="middle">
                                            <asp:TextBox ID="txtPRID_Plmph" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="64"
                                                Width="70%"></asp:TextBox>&nbsp;
                </td>
				<td style="height: 20px; width: 7px;">&nbsp;</td>	
				<td style="width: 155px; height: 20px;"></td>
				<td style="width: 255px; height: 20px;"></td>	
				<td style="height: 20px">&nbsp;</td>	
			</tr>			
            <tr>
                <td style="width: 255px; height: 20px" valign="middle">
                    Supplier 1 :</td>
                <td style="width: 1168px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtSupCode1" CssClass="fontObject"  runat="server" AutoPostBack="False" MaxLength="15" Width="20%"></asp:TextBox>
                    <input id="Find" class="button-small" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmPurReqDet','','txtSupCode1','txtSupName1','txtCreditTerm','ddlPPN1','txtPPNInit1', 'False');"
                        type="button" value=" ... " />
                    <asp:TextBox ID="txtSupName1"  CssClass="fontObject"  runat="server" BackColor="Transparent" BorderStyle="None"
                        Font-Bold="True"  MaxLength="10" Width="60%"></asp:TextBox></td>
                <td style="width: 7px; height: 20px">
                </td>
                <td style="width: 155px; height: 20px">
                </td>
                <td style="width: 255px; height: 20px">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 255px; height: 20px" valign="middle">
                    PPN :</td>
                <td style="width: 1168px; height: 20px" valign="middle"><asp:DropDownList ID="ddlPPN1" CssClass="fontObject"  runat="server" Width="9%" Enabled="False">
                </asp:DropDownList></td>
                <td style="width: 7px; height: 20px">
                </td>
                <td style="width: 155px; height: 20px">
                </td>
                <td style="width: 255px; height: 20px">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 255px; height: 20px" valign="middle">
                    Supplier 2 :</td>
                <td style="width: 1168px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtSupCode2" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="15" Width="20%"></asp:TextBox>
                    <input id="Button1" class="button-small" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmPurReqDet','','txtSupCode2','txtSupName2','txtCreditTerm','ddlPPN2','txtPPNInit2', 'False');"
                        type="button" value=" ... " />
                    <asp:TextBox ID="txtSupName2" CssClass="fontObject" runat="server" BackColor="Transparent" BorderStyle="None"
                        Font-Bold="True"  MaxLength="10" Width="60%"></asp:TextBox></td>
                <td style="width: 7px; height: 20px">
                </td>
                <td style="width: 155px; height: 20px">
                </td>
                <td style="width: 255px; height: 20px">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 255px; height: 20px" valign="middle">
                    PPN : </td>
                <td style="width: 1168px; height: 20px" valign="middle"><asp:DropDownList ID="ddlPPN2" CssClass="fontObject" runat="server" Width="9%" Enabled="False">
                </asp:DropDownList></td>
                <td style="width: 7px; height: 20px">
                </td>
                <td style="width: 155px; height: 20px">
                </td>
                <td style="width: 255px; height: 20px">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 255px; height: 20px" valign="middle">
                    Supplier 3 :</td>
                <td style="width: 1168px; height: 20px" valign="middle">
                    <asp:TextBox ID="txtSupCode3" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="15" Width="20%"></asp:TextBox>
                    <input id="Button2" class="button-small" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmPurReqDet','','txtSupCode3','txtSupName3','txtCreditTerm','ddlPPN3','txtPPNInit3', 'False');"
                        type="button" value=" ... " />
                    <asp:TextBox ID="txtSupName3" CssClass="fontObject" runat="server" BackColor="Transparent" BorderStyle="None"
                        Font-Bold="True" MaxLength="10" Width="60%"></asp:TextBox></td>
                <td style="width: 7px; height: 20px">
                </td>
                <td style="width: 155px; height: 20px">
                </td>
                <td style="width: 255px; height: 20px">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 255px; height: 20px" valign="middle">
                    PPN :</td>
                <td style="width: 1168px; height: 20px" valign="middle">
                    <asp:DropDownList ID="ddlPPN3" CssClass="fontObject" runat="server" Width="9%" Enabled="False">
                    </asp:DropDownList></td>
                <td style="width: 7px; height: 20px">
                </td>
                <td style="width: 155px; height: 20px">
                </td>
                <td style="width: 255px; height: 20px">
                </td>
                <td style="height: 20px">
                </td>
            </tr>
			<tr>
				<td colspan=6 style="height: 21px; text-align: right;">
				    <asp:ImageButton id="ImageButton1" UseSubmitBehavior="false" ImageURL="../../images/butt_save.gif" onClick="btnSave_Click" CausesValidation="false"  AlternateText="Save" runat="server" />
                    <asp:ImageButton id="ImageButton2" UseSubmitBehavior="false" ImageURL="../../images/butt_back.gif" onClick="btnBack_Click" AlternateText="Back" CausesValidation=False runat="server" /></td>				
			</tr>											
			<tr>
				<td colspan="6" style="height: 75px" valign="top"> 
					<table id="PRLnTable" class="sub-Add" border="0" width="100%" cellspacing="0" cellpadding="2" runat="server">
						<tr>
							<td colspan="3" style="width: 100%">
							 <div id="divgd" style="width:100%;height:400px;overflow: auto;">
                                <asp:DataGrid ID="dgPRLn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CellPadding="2" OnDeleteCommand="DEDR_Delete"
                                    OnEditCommand="DEDR_Edit" PagerStyle-Visible="False"
                                    Width="100%" BorderColor="#33CCCC" BorderStyle="None" GridLines="Vertical" class="font9Tahoma">
                                    <PagerStyle Visible="False" />
                                    <AlternatingItemStyle CssClass="mr-r" />
                                    <ItemStyle CssClass="mr-l" />
                            <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
 
                                    <Columns>
                                         <asp:TemplateColumn HeaderText="NO">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>&nbsp;     
                                                <asp:Label ID="lblPrLNID" runat="server" Text='<%# Container.DataItem("PrlnID") %>' Visible=false></asp:Label>&nbsp;                                                
                                                <br />
                                                <asp:Label ID="lblPrLocCode" runat="server" Text='<%# Container.DataItem("PRLoccode") %>'></asp:Label>&nbsp;                                                                                  
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateColumn>
                                         
                                        <asp:TemplateColumn HeaderText="Item &lt;br /&gt; PR Note">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Container.DataItem("ItemCode") %>'></asp:Label>                                                
                                                <br />                                                
                                                <asp:Label ID="lblItemDesc" runat="server" Text='<%# Container.DataItem("Description") %>'></asp:Label>
                                                <br />   
                                                <asp:TextBox ID="txtAddNote" CssClass="fontObject" runat="server" TextMode="MultiLine" Height="35px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Stock UOM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOMCode" runat="server" Text='<%# Container.DataItem("PurchaseUom") %>'></asp:Label>                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qty Requested">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblQtyReqDisplay" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyAPP"),2) %>'></asp:Label>
                                                <asp:Label ID="lblUnitCost" runat="server" Text='<%# Container.DataItem("Cost") %>'
                                                    Visible="false"></asp:Label>                                                
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="SUPPLIER 1 &lt;br /&gt;  NOTE ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupName1" runat="server"></asp:Label><br />
                                                <asp:TextBox ID="txtSupNote1" CssClass="fontObject" runat="server" Height="35px" TextMode="MultiLine" Width="100%"></asp:TextBox><br />
                                                <asp:Label ID="lblPPN1" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblSupCode1" runat="server" Visible="False"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="10%"/>
                                        </asp:TemplateColumn>                                        
                                        <asp:TemplateColumn HeaderText="Qty Order">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQtyOrder1" CssClass="fontObject" style="text-align:right"  runat="server" Width="100%"></asp:TextBox><br />
                                                <asp:Label ID="lblErrItemQty1" runat="server" ForeColor="Red" Text="Please Type Numeric Value"
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%"/>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCost1" style="text-align:right" CssClass="fontObject" runat="server"  Width="100%"></asp:TextBox>                    
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="SUPPLIER 2 &lt;br /&gt;  NOTE ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupName2" runat="server"></asp:Label>
                                                &nbsp;
                                                <br />
                                                <asp:TextBox ID="txtSupNote2" CssClass="fontObject" runat="server" Height="35px" TextMode="MultiLine" Width="100%"></asp:TextBox><br />
                                                <asp:Label ID="lblSupCode2" runat="server"></asp:Label>
                                                <asp:Label ID="lblPPN2" runat="server" Visible="False"></asp:Label>                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />                                            
                                        </asp:TemplateColumn>                                        
                                        <asp:TemplateColumn HeaderText="Qty Order">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQtyOrder2" CssClass="fontObject" style="text-align:right"  runat="server"  Width="100%"></asp:TextBox>
                                                <asp:Label ID="lblErrItemQty2" style="text-align:right"  runat="server" ForeColor="Red" Text="Qty More than Qty PR"
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%"/>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCost2" style="text-align:right" CssClass="fontObject"  runat="server"  Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateColumn>  
                                        <asp:TemplateColumn HeaderText="SUPPLIER 3 &lt;br /&gt;  NOTE ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupName3" runat="server"></asp:Label>&nbsp;<br />
                                                <asp:TextBox ID="txtSupNote3" runat="server" Height="35px" CssClass="fontObject" TextMode="MultiLine" Width="97%"></asp:TextBox><br />
                                                <asp:Label ID="lblSupCode3" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblPPN3" runat="server" Visible="False"></asp:Label>                                                
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                            
                                        </asp:TemplateColumn>                                        
                                        <asp:TemplateColumn HeaderText="Qty Order">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQtyOrder3" style="text-align:right" CssClass="fontObject" runat="server"  Width="100%" ></asp:TextBox><br />
                                                <asp:Label ID="lblErrItemQty3" runat="server" ForeColor="Red" Text="Qty More than Qty PR"
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%"/>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cost">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCost3" style="text-align:right" CssClass="fontObject"  runat="server"  Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%"/>
                                        </asp:TemplateColumn>                                                                                
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <HeaderStyle CssClass="mr-h" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:DataGrid>
              
                             </div>   
                              </td>	
						</tr>
					</table>
                    <asp:TextBox ID="txtPPNInit1" runat="server" AutoPostBack="False" BackColor="Transparent"
                       ForeColor="Transparent"   BorderStyle="None" Height="8px"   MaxLength="15" Width="2%"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txtPPNInit2" runat="server" AutoPostBack="False" BackColor="Transparent"
                        ForeColor="Transparent"  BorderStyle="None" Height="8px" MaxLength="15" Width="2%"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txtPPNInit3" runat="server" AutoPostBack="False" BackColor="Transparent"
                       ForeColor="Transparent"   BorderStyle="None" Height="8px" MaxLength="15" Width="2%"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txtPPN" runat="server" AutoPostBack="False" BackColor="Transparent"
                        BorderStyle="None" Height="8px" MaxLength="15" Width="2%"></asp:TextBox>
                    <asp:TextBox ID="txtCreditTerm" runat="server" AutoPostBack="False" BackColor="Transparent"
                       ForeColor="Transparent" BorderStyle="None" Height="8px" MaxLength="15" Width="2%"></asp:TextBox></td>
			</tr>
			<tr>
			    <td colspan=2 height="25">
			        <asp:Label id=lblErrGR visible=True Text="" forecolor=red runat=server />
			    </td>
			</tr>
			<tr>
				<td c&nbsp;</td>				
			</tr>
			<tr>
				<td align="left" colspan="6">
					<asp:ImageButton id="Save" UseSubmitBehavior="false" ImageURL="../../images/butt_save.gif" onClick="btnSave_Click" CausesValidation="false"  AlternateText="Save" runat="server" />
					&nbsp;
					<asp:ImageButton id="Back" UseSubmitBehavior="false" ImageURL="../../images/butt_back.gif" onClick="btnBack_Click" AlternateText="Back" CausesValidation=False runat="server" />
				    <br />
				</td>
			</tr>		
		</table>
            &nbsp;<asp:label id=lblPleaseSelect visible=False text="Please select " runat=server 
                        ForeColor="Transparent" />
			<asp:label id=lblPleaseSelectOne visible=False text="Please select one " runat=server 
                        ForeColor="Transparent" />	
			<asp:label id=lblLocCode visible=false runat=server />
            &nbsp;&nbsp;
 
            <asp:TextBox ID="txtPurchUOM" runat="server" AutoPostBack="False" BackColor="Transparent"
                BorderStyle="None" MaxLength="64" Width="3%" ForeColor="Transparent"></asp:TextBox>
            <asp:TextBox ID="txtPRRefLocCode" runat="server" AutoPostBack="False" BackColor="Transparent"
                BorderStyle="None" MaxLength="64" Width="3%" ForeColor="Transparent"></asp:TextBox>
            </div>
            </td>
            </tr>
            </table>

                  <br />

                                 <telerik:RadInputManager  ID="RadInputManager1" runat="server">
                                     <telerik:NumericTextBoxSetting BehaviorID="NumericQty1" EmptyMessage="please type Cost">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtQtyOrder1" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>
                   
                                     <telerik:NumericTextBoxSetting BehaviorID="NumericCost1" EmptyMessage="please type Cost">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtCost1" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>
  
                                     <telerik:NumericTextBoxSetting BehaviorID="NumericQty2"  >
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtQtyOrder2" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>
        
                                     <telerik:NumericTextBoxSetting BehaviorID="NumericCost2" EmptyMessage="please type Cost">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtCost2" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>
             
                                     <telerik:NumericTextBoxSetting BehaviorID="NumericQty3" >
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtQtyOrder3" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>

                                     <telerik:NumericTextBoxSetting BehaviorID="NumericCost3" EmptyMessage="please type Cost">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtCost3" />
                                        </TargetControls>
                                    </telerik:NumericTextBoxSetting>


                                </telerik:RadInputManager>
		</form>
	</body>
</html>
