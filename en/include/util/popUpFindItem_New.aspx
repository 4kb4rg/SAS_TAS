<%@ Page Language="vb" trace=false src="../../../include/popUpFindItem_New.aspx.vb" Inherits="popUpFindItem_New" %>
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

<body onload="javascript:self.focus();onload_setfocus();" leftmargin="2" topmargin="2">
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

		<table id="tblMain" width="100%" class="font9Tahoma" runat="server">
			<tr>
				<td width="20%" class="mt-h">FIND ITEM</td>
				<td align="right" width="60%"><asp:label id="lblTracker" runat="server"/></td>
			</tr>
			<tr>
				<td colspan="2"><hr style="width:100%">
                    </td>
			</tr>
			
			<tr>
				<td width="20%">Name/Code : </td>
				<td width="80%">
					<asp:TextBox id="txtItemCode" width="75%" maxlength="128" runat="server" /></td>
			</tr>
			

			<tr>
				<td style="height: 21px">&nbsp;</td>
				<td style="height: 21px">&nbsp;</td>
			</tr>
			
			<tr>
				<td width="20%"></td>
				<td width="80%">
					<asp:ImageButton id="ibConfirm" alternatetext="ALT + C" imageurl="../../images/butt_confirm.gif" runat="server" AccessKey="C"  /> 
				    <input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
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
					<asp:Label id="SortCol" Visible="False" Runat="server" />&nbsp;
                    <asp:Label id="LblDesc" Runat="server" ForeColor="OrangeRed" />
                    <asp:Label id="lblSaldo" Runat="server" ForeColor="OrangeRed" />
                    <asp:Label id="lblCost" Runat="server" ForeColor="OrangeRed" /></td>
			</tr>
			</table>
			<table id="Table1" width="100%" class="font9Tahoma" runat="server">
			<tr>
					<td colspan="2">					
						<asp:DataGrid id="dgLine" runat="server"
							AutoGenerateColumns="False" width="100%" 
							GridLines="None" 
							Cellpadding="2" 
							AllowPaging="True" 
							OnPageIndexChanged="OnPageChanged" 
							OnItemCommand="OnSelectItem"
							Pagerstyle-Visible="False" 
							OnSortCommand="Sort_Grid" class="font9Tahoma">								
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
								<asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
								<asp:HyperLinkColumn HeaderText="Item Code" 
								    SortExpression="ItemCode"
									DataTextField="ItemCode" >
                                    <HeaderStyle Width="15%" />
                                    <ItemStyle Width="15%" />
                                </asp:HyperLinkColumn>
								<asp:HyperLinkColumn HeaderText="Description"
									SortExpression="Description" 
									DataTextField="Description" >
                                    <HeaderStyle Width="35%" />
                                    <ItemStyle Width="35%" />
                                </asp:HyperLinkColumn>
                                <asp:BoundColumn DataField="Saldo" HeaderText="Saldo"></asp:BoundColumn>
                                <asp:BoundColumn DataField="UnitCost" HeaderText="Unit Cost"></asp:BoundColumn>
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
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
