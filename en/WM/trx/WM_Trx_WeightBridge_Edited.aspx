<%@ Page Language="vb" src="../../../include/WM_Trx_WeightBridge_Edited.aspx.vb" Inherits="WM_Trx_WeightBridge_Edited" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="../../menu/menu_WMtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>


<html>

		    <script language="javascript">
		        function setovrhour()
                    {
                    var doc = document.frmMain;
                    
                    var s = doc.txtStartTm.value;
                    var e = doc.txtEndTm.value;
            //        
                    if ((s.length==5) && (e.length==5))
                    {
                    var s1 = s.substring(2,3);
                    var e1 = e.substring(2,3);
                    
                       if ((s1==":")&&(e1==":"))
                        {
                            var s2 = parseFloat(s.substring(0,2))
                            var ms2 = parseFloat(s.substring(3,5))
                            var e2 = parseFloat(e.substring(0,2))
                            var me2 = parseFloat(e.substring(3,5))
                        
                            if (e2 < s2)
                            {
                                           
                                var a = (24-s2)+e2;
                                var b = ((60+me2)-ms2)/60;
                                doc.TxtQty.value = (a+b).toFixed(2);
                            }
                            else
                            {
                                 var a = (e2 - s2)-1;
                                 var b = ((60+me2)-ms2)/60;
                                 doc.TxtQty.value = (a+b).toFixed(2);
                            }
                            //setOvertimePsn()                
                        }
                        else
                        {
                            exit;
                        }
                    }
                    else
                    {
                    exit;
                    }      
                    }
            </script>
	<head>
		<title>Weighing Management - WeighBridge Ticket List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		    <form runat="server" ID="Form1">
		         <table border="0" cellspacing="1" cellpadding="1" width="100%">
            		<tr>
					<td class="mt-h" colspan="3" style="height: 21px">WEIGHTBRIDGE TICKET&nbsp;EDIT</td>					
				    </tr>
				    <tr>
				    <td colspan=6 style="height: 11px"></td>
				    </tr>
                </table>
	
			<table border="0" cellspacing="1" cellpadding="1" width="99%">
			
				<tr>
					<td colspan="6"><UserControl:MenuWMTrx id=MenuWMTrx runat="server" /></td>
				</tr>
				<tr>
					<td colspan=6 width=100% style="background-color:#FFCC00">
						<table width="95%" cellspacing="0" cellpadding="3" border="0" class="font9Tahoma" align="center">
							<tr class="mb-t">
								<td valign=bottom width=15% style="height: 39px">Ticket No :<BR><asp:TextBox id="srchTicketNo" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=15% style="height: 39px">Contract No. :<BR><asp:TextBox id="srchContractNo" width=100% maxlength="32" runat="server"/></td>
								<td valign=bottom width=15% style="height: 39px">DO No. :<BR><asp:TextBox id="srchDONo" width=100% maxlength="32" runat="server"/></td>
                                <td valign=bottom width=15% style="height: 39px">Customer :<asp:TextBox id="srchCust" width=100% maxlength="32" runat="server"/></td>
                                <td valign=bottom width="10%">Product :<BR><asp:dropdownlist id=srchProductList width=100% runat="server"/></td>	
								<td valign="bottom" width=5% style="height: 39px">
                                    Periode :<asp:DropDownList id="lstAccMonth" width=100% runat=server>
										<asp:ListItem value="1">1</asp:ListItem>
										<asp:ListItem value="2">2</asp:ListItem>										
										<asp:ListItem value="3">3</asp:ListItem>
										<asp:ListItem value="4">4</asp:ListItem>
										<asp:ListItem value="5">5</asp:ListItem>
										<asp:ListItem value="6">6</asp:ListItem>
										<asp:ListItem value="7">7</asp:ListItem>
										<asp:ListItem value="8">8</asp:ListItem>
										<asp:ListItem value="9">9</asp:ListItem>
										<asp:ListItem value="10">10</asp:ListItem>
										<asp:ListItem value="11">11</asp:ListItem>
										<asp:ListItem value="12">12</asp:ListItem>
									</asp:DropDownList></td>
								<td valign=bottom width=8% style="height: 39px"><asp:DropDownList id="lstAccYear" width=100% runat=server></asp:DropDownList>
								<td valign=bottom width=10% align=right style="height: 39px">
								    <asp:Button Text="Search" OnClick=srchBtn_Click runat="server" CssClass="button-small" ID="BtnSearch"/></td>							   
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="5">
					    <div id="divgd" style="height:520px;width:1100;overflow:auto;">   
	                        <asp:DataGrid ID="dgPRListing" runat="server" AllowSorting="True" AutoGenerateColumns="False" GridLines="Both" 
	                        OnItemDataBound="dgPRListing_BindGrid" CellPadding="2" 
							OnEditCommand="DEDR_Edit"
							OnCancelCommand="DEDR_Cancel"
							OnUpdateCommand="DEDR_Update"
							
                            Width="160%">
                            <AlternatingItemStyle CssClass="mr-r" />
                            <ItemStyle CssClass="mr-l" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Ticket No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTicketNo" runat="server" Text='<%# Container.DataItem("TicketNo") %>'></asp:Label>                                        
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndate" runat="server" Text='<%# ObjGlobal.GetLongDate(Container.DataItem("InDate")) %>'
                                            Visible="True"></asp:Label>
  									</ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Product">
								<ItemTemplate>
								      <asp:Label ID="lblProductCode" Text='<%# objWMTrx.mtdGetWeighBridgeTicketProduct(Container.DataItem("ProductCode")) %>' runat="server" />
								</ItemTemplate>
								<ItemStyle HorizontalAlign="LEFT" Width="5%" />
							    </asp:TemplateColumn>	
                                <asp:TemplateColumn HeaderText="Customer">
                                    <ItemTemplate>                                                                                
                                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Container.DataItem("Name") %>'></asp:Label><br />
                                        <asp:Label ID="lblContractNo" runat="server" Text='<%# Container.DataItem("ContractNo") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />                                  
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Transporter">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTransporter" runat="server" Text='<%# Container.DataItem("TransporterName") %>'></asp:Label> <br />
                                        <asp:Label ID="lblDONo" runat="server" Text='<%# Container.DataItem("DeliveryNoteNo") %>'></asp:Label>                                      
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Vehicle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVehicle" runat="server" Text='<%# Container.DataItem("VehicleCode") %>'></asp:Label> <br />
                                    </ItemTemplate>
                                    <ItemStyle Width="4%" />
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Bruto">
                                    <ItemTemplate>                                        
                                        <asp:Label ID="lblSWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("SecondWeight"),0) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tarra">
                                    <ItemTemplate>                                        
                                           <asp:Label ID="lblFWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("FirstWeight"),0) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Netto" Visible=True>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNetWeight" runat="server" Text='<%# FormatNumber(Container.DataItem("NetWeight"),0) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateReceived" runat="server" Text='<%# ObjGlobal.GetLongDate(Container.DataItem("DateReceived")) %>'
                                            Visible="True"></asp:Label> 
                                        <asp:TextBox ID="TxtDateReceived" Visible=false runat="server" Text='<%# Container.DataItem("DateReceived") %>' ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHourReceived" runat="server" Text='<%# format(Container.DataItem("HourReceived"),"HH:mm") %>'
                                            Visible="True"></asp:Label> 
                                        <asp:TextBox ID="TxtHourReceived" MaxLength="5" onkeyup="setovrhour()" Visible=false runat="server" Text='<%# Container.DataItem("HourReceived") %>' >
                                        <MaskSettings Mask="HH:mm" IncludeLiterals="All" ShowHints="true" />
                                        </asp:TextBox><br />
                                        <asp:Label ID="lblErrHour" Visible="false" ForeColor="red" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bruto" Visible=True>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyerFirst" runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerFirstWeight"),0) %>' ></asp:Label>
                                        <asp:TextBox ID="TxtBuyerFirst" Visible=false runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerFirstWeight"),0) %>' ></asp:TextBox>
                                    </ItemTemplate>
                                       <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Tarra" Visible=True>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyerSecond" runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerSecondWeight"),0) %>'></asp:Label>
                                        <asp:TextBox ID="TxtBuyerSecond" Visible=false runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerSecondWeight"),0) %>' ></asp:TextBox>
                                    </ItemTemplate>
                                       <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Netto" Visible=True>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBuyerNet" runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerNetWeight"),0) %>' ></asp:Label>
                                        <asp:TextBox ID="TxtBuyerNet" Visible=false runat="server" Text='<%# FormatNumber(Container.DataItem("BuyerNetWeight"),0) %>' ></asp:TextBox>
                                    </ItemTemplate>
                                       <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                                            
                                <asp:TemplateColumn HeaderText="Difference">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSelisih" runat="server" Text='<%# FormatNumber(Container.DataItem("Selisih"),0) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                                                                                    
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Approved" runat="server" CausesValidation="False" CommandName="Approved"
                                            Text="Approved" Visible="False"></asp:LinkButton>
                                        <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="Edit"></asp:LinkButton>
                                        <asp:LinkButton ID="Update" runat="server" CausesValidation="False" CommandName="Update"
                                            Text="Update" Visible=false></asp:LinkButton>
                        
                                        <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="Cancel" Visible=false></asp:LinkButton>&nbsp;
                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="OA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOA" runat="server" Text='<%# FormatNumber(Container.DataItem("Price"),2) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# FormatNumber(Container.DataItem("Amount"),2) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="5%" />
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblType" runat="server" Text='<%# Container.DataItem("ClaimDescr") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Min">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMin" runat="server" Text='<%# FormatNumber(Container.DataItem("ClaimKGMin"),0) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Max">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMax" runat="server" Text='<%# FormatNumber(Container.DataItem("ClaimKGMax"),0) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Tonase">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimed" runat="server" Text='<%# FormatNumber(Container.DataItem("ClaimKG"),0) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Rp/Kg">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCtrPrice" runat="server" Text='<%# FormatNumber(Container.DataItem("ContractPrice"),2) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaimAmount" runat="server" Text='<%# FormatNumber(Container.DataItem("ClaimAmount"),0) %>' ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" Width="4%" />
                                </asp:TemplateColumn>
                                
                                
                            </Columns>
                            <HeaderStyle CssClass="mr-h" />
                        </asp:DataGrid>
                    </div>
                    </td>
				</tr>
				<tr>
					<td align=right colspan="6">
						<asp:label id="lblTracker" runat="server"/>
						<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" Visible="False" />
						<asp:DropDownList id="lstDropList" Visible=false AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
			         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" Visible="False" />
					</td>
				</tr>
                <tr>
                    <td colspan=5>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                    <asp:CheckBox id="cbDetailByDO" text=" Detail by DO" checked="false" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                    <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" Visible=false runat="server" /></td>
                </tr>
                <tr>
                    <td colspan=5>&nbsp;</td>
                </tr>
				<tr>
					<td align="left" width="80%" ColSpan=6>
					    <asp:ImageButton ID="ImgRefresh" OnClick="Btnrefresh_onClick" runat="server" AlternateText="+ Refresh list"  ImageUrl="../../images/butt_refresh.gif" Visible=False />					    
						<asp:ImageButton id=Stock UseSubmitBehavior="false" imageurl="../../images/butt_new_stockPR.gif" AlternateText="New Stock/WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=DC UseSubmitBehavior="false" imageurl="../../images/butt_new_directchargepr.gif" AlternateText="New Direct Charge PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=WS UseSubmitBehavior="false" imageurl="../../images/butt_new_workshoppr.gif" AlternateText="New WorkShop PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" /> 
						<asp:ImageButton id=FA UseSubmitBehavior="false" imageurl="../../images/butt_new_fixedassetpr.gif" AlternateText="New Fixed Asset PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />
						<asp:ImageButton id=NU UseSubmitBehavior="false" imageurl="../../images/butt_new_NURserypr.gif" AlternateText="New Nursery PR" OnClick="btnNewStPR_Click" runat="server" Visible="False" />						
						<asp:ImageButton id=ibPrint UseSubmitBehavior="false" imageurl="../../images/butt_print_preview.gif" AlternateText=Print onClick="btnPreview_Click" runat="server" Visible="False" />
                        <table>
                            <tr>
                                <td style="width: 59px">
                                </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                </td>
                            </tr>                            
                            <tr>
                                <td style="width: 59px">
                                    <asp:Label ID="lblSearch" runat="server" Font-Bold="True" ForeColor="Yellow" Visible="False"></asp:Label></td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="srchPRTypeList" width=100% runat=server Visible="False" /></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="srchPRLevelList" width=100% runat=server Visible="False" /></td>
                            </tr>
                            <tr>
                                <td style="width: 59px">
                                    </td>
                                <td style="width: 15px">
                                </td>
                                <td style="width: 100px">
                                    <asp:DropDownList id="srchStatusList" width=100% runat=server Visible="False" /></td>
                            </tr>
                        </table>
					</td>									
				</tr>
				<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label><asp:label id="sortcol" Visible="False" Runat="server"></asp:label><asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /></table>

			</form>
		</body>
</html>
