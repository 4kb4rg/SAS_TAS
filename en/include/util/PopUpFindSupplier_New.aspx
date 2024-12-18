<%@ Page Language="vb" trace=false src="../../../include/PopUpFindSupplier_New.aspx.vb" Inherits="PopUpFindSupplier_New" %>
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
             <td style="width: 100%; height: 400px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 
		<table id="tblMain" width="100%" class="font9Tahoma" runat="server">
			<tr>
				<td width="20%" class="font12Tahoma"><strong> FIND SUPPLIER</strong></td>
				<td align="right" width="60%"><asp:label id="lblTracker" runat="server"/></td>
			</tr>
			<tr>
				<td colspan="2"><hr style="width:100%">
                    </td>
			</tr>
			
			<tr>
				<td width="20%"><strong>Name/Code : </strong> </td>
				<td width="80%">
					<asp:TextBox id="txtItemCode" width="75%" maxlength="128" runat="server" /></td>
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
					<asp:Label id="SortCol" Visible="False" Runat="server" />&nbsp;
                    <asp:TextBox id="txtSupname" width="5%" maxlength="128" runat="server" Visible="False" />
                    <asp:TextBox id="txtCreditTerm" width="5%" maxlength="128" runat="server" Visible="False" />
					<asp:DropDownList id="ddlPPN" width="5%" runat="server" Visible="False" />
					<asp:TextBox id="txtPPNInit" width="5%" maxlength="128" runat="server" Visible="False" />
				&nbsp;
					<asp:ImageButton id="btnPrev" runat="server" 
                        imageurl="../../../images/btprev.png" alternatetext="Previous" 
                        commandargument="prev" onClick="btnPrevNext_Click" Height="18px" />
					<asp:DropDownList id="lstDropList" runat="server"
						AutoPostBack="True" 
						onSelectedIndexChanged="PagingIndexChanged" />
		         	<asp:Imagebutton id="btnNext" runat="server"  
                        imageurl="../../../images/btnext.png" alternatetext="Next" 
                        commandargument="next" onClick="btnPrevNext_Click" />
				</td>
			</tr>
			</table>
			
			<table id="Table1" width="100%" runat="server">
			<tr>
					<td colspan="2">					
						<asp:DataGrid id="dgLine" runat="server"
							AutoGenerateColumns="False" width="100%" 
							GridLines="None" 
							Cellpadding="2" 
							AllowPaging="True" 
							OnPageIndexChanged="OnPageChanged" 
							OnItemCommand="OnSelectItem"
                            OnItemDataBound="dgLine_BindGrid" 
							Pagerstyle-Visible="False" 
							OnSortCommand="Sort_Grid" class="font9Tahoma" PageSize="15">								
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
								<asp:BoundColumn Visible="False" HeaderText="Supplier Code" DataField="SupplierCode" />
								<asp:BoundColumn Visible="False" HeaderText="Name" DataField="Name" />
                                <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>

								<asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
								<asp:HyperLinkColumn HeaderText="Supplier Code" 
								    SortExpression="SupplierCode"
									DataTextField="SupplierCode" >
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
								<asp:HyperLinkColumn HeaderText="Name"
									SortExpression="Name" 
									DataTextField="Name" >
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn HeaderText="Credit Term"
									SortExpression="CreditTerm" 
									DataTextField="CreditTerm" >
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
								<asp:HyperLinkColumn HeaderText="PPN"
									SortExpression="PPNInit" 
									DataTextField="PPNInit" >
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
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
