<%@ Page Language="vb" trace=false src="../../../include/PopUpFindTrx.aspx.vb" Inherits="PopUpFindTrx" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>

<html>
<head>
    <title>G2 - Find</title> 
        <link href="../css/gopalms.css" rel="stylesheet" type="text/css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
		
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();onload_setfocus();" leftmargin="2" topmargin="2" class="main-modul-bg-app-list-pu">
    <form id="frmMain" runat="server">
                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma"  >
		    <tr>
             <td style="width: 100%; height: 1000px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 
		<table id="tblMain" width="100%" class="font9Tahoma"  runat="server">
			<tr>
				<td width="20%" class="font9Tahoma"><strong> FIND TRANSACTION</strong></td>
				<td align="right" width="60%"><asp:label id="lblTracker" runat="server"/></td>
			</tr>
			<tr>
				<td colspan="2"><hr style="width:100%">
                    </td>
			</tr>
			
			<tr>
				<td width="20%">PO ID/Supplier : </td>
				<td width="80%">
					<asp:TextBox id="txtItemCode" width="75%" maxlength="128" runat="server" /></td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td width="20%"></td>
				<td width="80%">
					<asp:ImageButton id="ibConfirm" alternatetext="ALT + C" imageurl="../../images/butt_confirm.gif" runat="server" AccessKey="C"  /> 
				    <input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
				</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			</table>
			<table id="Table1" width="100%" class="font9Tahoma"  runat="server">
			<tr>
					<td colspan="2">					
						<asp:DataGrid id="dgLine" runat="server"
							AutoGenerateColumns="false" width="100%" 
							GridLines="none" 
							Cellpadding="2" 
							AllowPaging="True" 
							Allowcustompaging="False" 
							Pagesize="5" 
							OnPageIndexChanged="OnPageChanged" 
							OnItemCommand="OnSelectItem"
							Pagerstyle-Visible="False" 
							OnSortCommand="Sort_Grid"  
							AllowSorting="False" class="font9Tahoma">								
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
								<asp:BoundColumn Visible="False" HeaderText="POID" DataField="POID" />
								<asp:BoundColumn Visible="False" HeaderText="Supplier" DataField="SupplierCode" />
								<asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-Width="10%" HeaderStyle-Width="10%"/>
								<asp:HyperLinkColumn HeaderText="Supplier Code" ItemStyle-Width="15%" HeaderStyle-Width="15%" 
								    SortExpression=""
									DataTextField="" />
								<asp:HyperLinkColumn HeaderText="Name" ItemStyle-Width="40%" HeaderStyle-Width="40%"
									SortExpression="SupplierCode" 
									DataTextField="SupplierCode" />
								<asp:TemplateColumn HeaderText="Bank Code" HeaderStyle-Width="15%" SortExpression="BankCode">
								    <HeaderStyle HorizontalAlign="Right" /> 
								    <ItemStyle Width="15%"  HorizontalAlign="Center" />	
									<ItemTemplate>
										<%#Container.DataItem("BankCode")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Bank Acc. No"  HeaderStyle-Width="20%" SortExpression="BankAccNo">
								    <HeaderStyle HorizontalAlign="Right" /> 
								    <ItemStyle Width="20%"  HorizontalAlign="Right" />	
									<ItemTemplate>
										<%#Container.DataItem("BankAccNo")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td align="right" colspan="2">
						<asp:ImageButton id="btnPrev" runat="server" 
                            imageurl="../../../images/btprev.png" alternatetext="Previous" 
                            commandargument="prev" onClick="btnPrevNext_Click" />
						<asp:DropDownList id="lstDropList" runat="server"
							AutoPostBack="True" 
							onSelectedIndexChanged="PagingIndexChanged" />
			         	<asp:Imagebutton id="btnNext" runat="server"  
                            imageurl="../../../images/btnext.png" alternatetext="Next" 
                            commandargument="next" onClick="btnPrevNext_Click" />
					</td>
				</tr>
				<tr>
					<td align="left" ColSpan="2">
						<asp:Label id="SortCol" Visible="False" Runat="server" />
					</td>
				</tr>
		</table>
		 <asp:Label id="lblErrMessage" visible=false Text="Error while initiating component." runat=server />
		<asp:label id="SortExpression" Visible="False" Runat="server" />
           </div>
            </td>
            </tr>
            </table>
    </form>
</body>
</html>
