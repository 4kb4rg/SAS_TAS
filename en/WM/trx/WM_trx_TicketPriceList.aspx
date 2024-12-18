<%@ Page Language="vb" trace=false codefile="../../../include/WM_trx_TicketPriceList.aspx.vb" Inherits="WM_trx_TicketPriceList" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWMTrx" src="../../menu/menu_wmtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
    TagPrefix="igtab" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<html>
	<head>
		<title>Weighing Management - WeighBridge Price Ticket List</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	    <form id=frmMain class="main-modul-bg-app-list-pu"  runat=server >
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		        <tr>
                    <td style="width: 100%; height: 800px" valign="top">
			            <div class="kontenlist">
                            <asp:Label id=SortExpression visible=false runat=server />
                            <asp:Label id=SortCol visible=false runat=server />
                            <asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
                            <table border=0 cellspacing=1 cellpadding=1 width=100% class="font9Tahoma" >
                                <tr>
                                    <td colspan=6><UserControl:MenuWMTrx id=MenuWMTrx runat=server /></td>
                                </tr>
                                <tr>
                                    <td  colspan="3"><strong> WEIGHBRIDGE TICKET LIST</strong></td>
                                    <td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
                                </tr>
                                <tr>
                                    <td colspan=6><hr style="width :100%" />   </td>
                                </tr>
                                <tr>
                                    <td colspan=6 width=100% style="background-color:#FFCC00">
                                        <table width="100%" cellspacing="0" cellpadding="3" border="0" class="font9Tahoma" align="center">
                                            <tr class="mb-t">
                                                <td width="10%" valign=bottom>Location :<BR><asp:DropDownList id="srchlocation" width=100% runat=server class="font9Tahoma" /></td>	
                                                    <td width="10%" valign=bottom>Transaction :<BR><asp:DropDownList id="srchTransType" width=100% runat=server class="font9Tahoma" /></td>	                                    
                                                <td width="10%">Product :<BR><asp:dropdownlist id=srchProductList width=100% CssClass="fontObject" runat="server"/></td>																																							
                                                <td width="15%">Customer :<BR><telerik:RadComboBox   CssClass="fontObject" ID="radcmbCust"
                                                            Runat="server" AllowCustomText="True" 
                                                            EmptyMessage="Plese Select Customer " Height="200" Width="100%" 
                                                            ExpandDelay="50" Filter="Contains" Sort="Ascending" 
                                                            EnableVirtualScrolling="True">
                                                            <CollapseAnimation Type="InQuart" />
                                                        </telerik:RadComboBox>	
                                                </td>									                                    
                                                <td width="10%" height="26">Ticket No. :<BR><asp:TextBox id=srchTicketNo width=100% maxlength="20" CssClass="fontObject" runat="server" /></td>
                                                <td width="10%">Contract No. :<BR><asp:TextBox id=srchContractNo width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>
                                                <td width="10%">DO No. :<BR><asp:TextBox id=srchDeliveryNo width=100% maxlength="20" CssClass="fontObject" runat="server"/></td>
                                                <td width="12%" height="26">
                                                    From : <br /><telerik:RadDatePicker ID="dtDateFr" Runat="server" 
                                                    Culture="en-US"> 
                                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                                    <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
                                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                </telerik:RadDatePicker>									
                                                </td>			
                                                <td width="12%" height="26">
                                                    To:<br /><telerik:RadDatePicker ID="dtDateTo" Runat="server" 
                                                        Culture="en-US"> 
                                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                                        <DateInput DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MMM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px"></DateInput>
                                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                    </telerik:RadDatePicker>									
                                                </td>	                                					
                                                
                                                <td width="10%" align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click CssClass="button-small" runat="server"/></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                        
                            </table>
                    
                            <table border=0 cellspacing=1 cellpadding=1 width=100%>
                                <tr>
                                    <td style="height: 24px;" colspan="5">
                                        <table border="0" cellspacing="1" cellpadding="1" width="100%">
                                            <tr>
                                                <td colspan=6>	
                            
                                                    <asp:DataGrid id=dgTicketList
                                                        AutoGenerateColumns=false width=100% runat=server
                                                        GridLines=both 
                                                        Cellpadding=2 
                                                        Pagerstyle-Visible=False    
                                                        OnSortCommand=Sort_Grid  
                                                        AllowPaging=True 
                                                        Allowcustompaging=False 
                                                        Pagesize=15 
                                                        AllowSorting=True ShowFooter="True">                                        
                                                        <HeaderStyle CssClass="mr-h" />
                                                        <ItemStyle CssClass="mr-l" />
                                                        <AlternatingItemStyle CssClass="mr-r" />
                                                        
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Ticket No" >
                                                                <ItemTemplate>
                                                                    <%# Container.DataItem("TicketNo") %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="LEFT" Width="8%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Date" >
                                                                <ItemTemplate>
                                                                    <%# objGlobal.GetLongDate(Container.DataItem("OutDate")) %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="In" >
                                                                <ItemTemplate>
                                                                    <%# FormatDateTime(Container.DataItem("JamMasuk"),DateFormat.ShortTime)%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateColumn>        
                                                            <asp:TemplateColumn HeaderText="Out" >
                                                                <ItemTemplate>
                                                                    <%# FormatDateTime(Container.DataItem("JamKeluar"),DateFormat.ShortTime)%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateColumn>                                                                                   
                                                            <asp:TemplateColumn HeaderText="Product" >
                                                                <ItemTemplate>
                                                                    <%# Container.DataItem("ProductCode") %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="LEFT" Width="5%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Customer" >
                                                                <ItemTemplate>
                                                                    <%#Container.DataItem("RelasiName")%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="LEFT" Width="20%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Nomor SPB" >
                                                                <ItemTemplate>
                                                                    <%#Container.DataItem("SPBKirim")%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="LEFT" Width="7%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Vehicle" >
                                                                <ItemTemplate>
                                                                    <%#Container.DataItem("VehicleCode")%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="LEFT" Width="8%" />
                                                            </asp:TemplateColumn>                                            
                                                            <asp:TemplateColumn HeaderText="Contract No" >
                                                                <ItemTemplate>
                                                                    <%#Container.DataItem("ContractNo")%> <br>
                                                                    <%#Container.DataItem("DeliveryNoteNo")%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="LEFT" Width="8%" />
                                                            </asp:TemplateColumn>                                            
                                                            <asp:TemplateColumn HeaderText="First Weight" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>
                                                                    <%#FormatNumber(Container.DataItem("FirstWeight"), 0)%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Second Weight" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>
                                                                    <%#FormatNumber(Container.DataItem("SecondWeight"), 0)%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            </asp:TemplateColumn>                                          
                                                            <asp:TemplateColumn HeaderText="Gross" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>
                                                                    <%#FormatNumber(Container.DataItem("NetWeight1"), 0)%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                            </asp:TemplateColumn>
                                                                                                                                             
                                                            <asp:TemplateColumn HeaderText="Total Pot" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>
                                                                    <%#FormatNumber(Container.DataItem("SortaseWeight"), 0)%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                            </asp:TemplateColumn>                                              
                                                            <asp:TemplateColumn HeaderText="Netto" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>                                       
                                                                            <%#FormatNumber(Container.DataItem("NetWeightFinal"),0)%>									                             
                                                                            <asp:Label id=lblNetWeight visible=false text='<%# Container.DataItem("NetWeightFinal") %>'   runat=server/>
                                                                </ItemTemplate>
                                                                <FooterTemplate >
                                                                    <asp:Label ID=lblTotalNetWeight runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                            </asp:TemplateColumn>                                            

                                                            <asp:TemplateColumn HeaderText="Price" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>
                                                                    <%#FormatNumber(Container.DataItem("ProductPrice"), 0)%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                            </asp:TemplateColumn>   

                                                            <asp:TemplateColumn HeaderText="Nominal" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
                                                                <ItemTemplate>                                                                                                                                        
                                                                            <%# FormatNumber(Container.DataItem("TotalAmount"), 0)%>									                             
                                                                            <asp:Label id=lblAmount visible=false text='<%# Container.DataItem("TotalAmount") %>'   runat=server/>
                                                                </ItemTemplate>

                                                                <FooterTemplate >
                                                                    <asp:Label ID=lblTotalAmount runat=server />
                                                                    </FooterTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle Font-Size=Smaller BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />


                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" Width="7%" />
                                                            </asp:TemplateColumn>   
                                                            
                                                        </Columns>
                                                    </asp:DataGrid><BR>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                <asp:CheckBox id="cbExcelTicket" text=" Export To Excel" checked="false" Visible=false runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td align=right colspan="6">
                                                        <asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
                                                        <asp:DropDownList id="lstDropList" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" runat="server" />
                                                        <asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
                                                </td>
                                            </tr>                            
                                            <tr>
                                                <td colspan=5>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="100%" ColSpan=6>
                                                    <asp:ImageButton id=NewTicketBtn onClick=NewTicketBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Ticket" visible=True runat=server/>
                                                    <asp:ImageButton id=TicketPrintPrev imageurl="../../images/butt_print_preview.gif" AlternateText=Print onClick="btnTicketPrintPrev_Click" runat="server"/>
                                                </td>
                                            </tr>                                        
                                        </table>                             
                                    </td>
                                </tr>	
                            </table>    
                        </div>
                    </td>
                </tr>
            </table>  
            <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>	                                          
		</FORM>
	</body>
</html>
