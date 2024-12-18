<%@ Page Language="vb" trace=false src="../../../include/popUpFindINPR.aspx.vb" Inherits="popUpFindINPR" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>

<script runat="server">

Protected Sub txtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

End Sub
</script>

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

<body onload="javascript:self.focus();onload_setfocus();" leftmargin="2" topmargin="2">
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

		<table id="tblMain" width="100%" class="font9Tahoma" runat="server">
			<tr>
				<td width="20%" class="font9Tahoma"><strong>FIND ITEM</strong> </td>
				<td align="right" width="60%"><asp:label id="lblTracker" runat="server"/></td>
			</tr>
            <tr>
                <td style="height: 27px" width="20%">
                    PR Status</td>
                <td style="height: 27px" width="80%"><asp:DropDownList ID="DDLPrStatus" runat="server" Width="20%" AutoPostBack="True">
                </asp:DropDownList></td>
            </tr>
			
			<tr>
				<td width="20%" style="height: 27px">
                    PR No/Name/Code : </td>
				<td width="80%" style="height: 27px">
					<asp:TextBox id="txtItemCode" width="50%" maxlength="128" runat="server" OnTextChanged="txtItemCode_TextChanged" />&nbsp;
					<asp:ImageButton id="ibConfirm" alternatetext="ALT + C" imageurl="../../images/butt_confirm.gif" runat="server" AccessKey="C"  /> 
				    <input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20"></td>
			</tr>
			<tr>
				<td align="left" ColSpan="2" style="text-align: right">
					<asp:Label id="SortCol" Visible="False" Runat="server" />&nbsp;
                    <asp:Label id="LblDesc" Runat="server" ForeColor="OrangeRed" />
                    <asp:Label id="txtSaldo" Runat="server" ForeColor="OrangeRed" />
                    <asp:Label id="txtCost" Runat="server" ForeColor="OrangeRed" />
                    <asp:Label id="txtTotalCost" Runat="server" ForeColor="OrangeRed" />&nbsp;
                    <asp:Label id="txtPrID" Runat="server" ForeColor="OrangeRed" />
                    <asp:Label id="txtPrLoc" Runat="server" ForeColor="OrangeRed" />
                    <asp:Label id="txtAddNote" Runat="server" ForeColor="OrangeRed" />
					<asp:Label id="txtPRLnID" Runat="server" ForeColor="OrangeRed" />
					<asp:Label id="txtPurchaseUOM" Runat="server" ForeColor="OrangeRed" />
                    <asp:ImageButton id="btnPrev" runat="server" 
                        imageurl="../../../images/btprev.png" alternatetext="Previous" 
                        commandargument="prev" onClick="btnPrevNext_Click" /><asp:DropDownList id="lstDropList" runat="server"
						AutoPostBack="True" 
						onSelectedIndexChanged="PagingIndexChanged" /><asp:Imagebutton id="btnNext" 
                        runat="server"  imageurl="../../../images/btnext.png" alternatetext="Next" 
                        commandargument="next" onClick="btnPrevNext_Click" /></td>
			</tr>
			</table>
			<table id="Table1" width="100%" class="font9Tahoma" runat="server">
			<tr>
					<td colspan="2" style="height: 429px" valign="top">					
						<asp:DataGrid id="dgLine" runat="server"
							AutoGenerateColumns="False" width="100%" 
							GridLines="None" 
							Cellpadding="2" 
							AllowPaging="True" 
							OnPageIndexChanged="OnPageChanged" 
							OnItemCommand="OnSelectItem"
							Pagerstyle-Visible="False" 
							OnSortCommand="Sort_Grid" PageSize="15" class="font9Tahoma">								
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
								<asp:BoundColumn Visible="False" HeaderText="Item Code" DataField="ItemCode" />
								<asp:BoundColumn Visible="False" HeaderText="Description" DataField="Description" />
                                <asp:BoundColumn DataField="PRID" HeaderText="PR ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Qty" DataField="PRQTYAPP" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Unit Cost" DataField="Amount" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TotalCost" HeaderText="Total Cost" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRLOCCODE" HeaderText="PRLocCode" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AdditionalNote" HeaderText="AddNote" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PRLnID" HeaderText="PRLnID" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PurchaseUOM" HeaderText="PurchaseUOM" Visible="False"></asp:BoundColumn>
								
                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle Width="5%" />
                                </asp:ButtonColumn>
								<asp:HyperLinkColumn HeaderText="PR ID" 
								    SortExpression="PrID"
									DataTextField="PrID" >
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:HyperLinkColumn>
								<asp:HyperLinkColumn HeaderText="Item Code" 
								    SortExpression="ItemCode"
									DataTextField="ItemCode" >
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:HyperLinkColumn>
								<asp:HyperLinkColumn HeaderText="Description"
									SortExpression="Description" 
									DataTextField="Description" >
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>                                                               
                                <asp:HyperLinkColumn HeaderText="Qty" DataTextField="PRQTYAPP" DataTextFormatString="{0:#,##0.00}"></asp:HyperLinkColumn>
								<asp:HyperLinkColumn HeaderText="Purchase UOM" DataTextField="PurchaseUOM" DataTextFormatString="{0:#,##0.00}"></asp:HyperLinkColumn>
                                <asp:HyperLinkColumn HeaderText="Unit Cost" DataTextField="Amount" DataTextFormatString="{0:#,##0.00}"></asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="TotalCost" HeaderText="Total Cost" DataTextFormatString="{0:#,##0.00}"></asp:HyperLinkColumn>                                
                                <asp:HyperLinkColumn HeaderText="PO ID"
									SortExpression="Description" 
									DataTextField="POID" >
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>  
							    <asp:HyperLinkColumn HeaderText="Additional Note"
									SortExpression="Description" 
									DataTextField="AdditionalNote" >
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>  
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
					</td>
				</tr>
                <tr>
                    <td colspan="2" style="height: 21px">
                    </td>
                </tr>
		</table>
		 <asp:Label id="lblErrMessage" visible=false Text="Error while initiating component." runat=server />
		<asp:label id="SortExpression" Visible="False" Runat="server" />&nbsp;<br />
        <br />
            </div>
            </td>
            </tr>
            </table>
    </form>
</body>
</html>
