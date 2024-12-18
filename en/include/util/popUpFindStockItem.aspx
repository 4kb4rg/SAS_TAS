<%@ Page Language="vb" trace=false src="../../../include/PopUpFindStockItem.aspx.vb" Inherits="PopUpFindStockItem" %>
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
				<td width="20%" class="font9Tahoma"><strong>FIND ITEM</strong> </td>
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
				<td align="right" colspan="2">
					<asp:ImageButton id="btnPrev" runat="server" imageurl="../../images/icn_prev.gif" alternatetext="Previous" commandargument="prev" onClick="btnPrevNext_Click" />
					<asp:DropDownList id="lstDropList" runat="server"
						AutoPostBack="True" 
						onSelectedIndexChanged="PagingIndexChanged" />
		         	<asp:Imagebutton id="btnNext" runat="server"  imageurl="../../images/icn_next.gif" alternatetext="Next" commandargument="next" onClick="btnPrevNext_Click" />
				</td>
			</tr>
			<tr>
				<td align="left" ColSpan="2">
					<asp:Label id="SortCol" Visible="False" Runat="server" />
				</td>
			</tr>
			</table>
			<table id="Table1" width="100%" runat="server">
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
								<asp:BoundColumn Visible="False" HeaderText="Item Code" DataField="ItemCode" />
								<asp:BoundColumn Visible="False" HeaderText="Description" DataField="Description" />
								<asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-Width="10%" HeaderStyle-Width="10%"/>
								<asp:HyperLinkColumn HeaderText="Item Code" ItemStyle-Width="30%" HeaderStyle-Width="30%" 
								    SortExpression="ItemCode"
									DataTextField="ItemCode" />
								<asp:HyperLinkColumn HeaderText="Description" ItemStyle-Width="60%" HeaderStyle-Width="60%"
									SortExpression="Description" 
									DataTextField="Description" />
							</Columns>
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
