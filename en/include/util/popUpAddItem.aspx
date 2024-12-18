<%@ Page Language="vb" trace=false src="../../../include/PopUpAddItem.aspx.vb" Inherits="PopUpAddItem" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>

<head>
    <title>Green Golden - Add Item</title> 
          <link href="../css/gopalms.css" rel="stylesheet" type="text/css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body leftmargin="2" topmargin="2">
    <form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

        <asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<table id=tblMain width=100%  class="font9Tahoma" runat=server>
			<tr>
				<td class="font9TahomaADD ITEM</td>
				<td></td>
			</tr>
			<tr>
				<td colspan=2><hr size="1" noshade></td>
			</tr>
			<tr id=trItm class="mb-c">
				<td width=15%><asp:Label id="lblItemCode" runat="server" />&nbsp;Code : </td>
				<td>
					<asp:DropDownList id=ddlItemCode width=100% runat=server/> 
					<asp:Label id=lblErrItemCode visible=false forecolor=red runat=server/>
				</td>
			</tr>
            <tr id=trQty class="mb-c">
				<td width=25%>Quantity : </td>
				<td width=60%>
					<asp:TextBox id=txtQuantity width=30% maxlength=20 Text="0" onkeypress="javascript:keypress()" runat=server/>
					<asp:Label id=lblErrQuantity visible=false forecolor=red runat=server/>
				</td>
			</tr>
			<tr id=trAddNote class="mb-c">
				<td width=25%>Additional Note : </td>
				<td width=60%>
					<asp:TextBox id=txtAddNote width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
				</td>
			</tr>
			<tr>											
				<td>
					<asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add onclick=btnAdd_Click runat=server />
				</td>
			</tr>		
			<tr>
				<td colspan="6">
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
						OnDeleteCommand=DEDR_Delete 
						Pagerstyle-Visible=False
						AllowSorting="True" class="font9Tahoma">
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
							<asp:TemplateColumn ItemStyle-Width="40%" HeaderText="Item Code">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("ItemCodeDesc") %>  id=lblItemCodeDesc runat="server" />
									<asp:Label Text=<%# Container.DataItem("ItemCode") %>  id=lblItemCode visible=false runat="server" />										
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Quantity">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Quantity") %>  id="lblQuantity" runat="server" />									
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="UOM">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("UOMCodeDesc") %>  id="UOMCode" runat="server" />										
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="15%" HeaderText="Additional Note">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AdditionalNote") %>  id="lblAddNote" runat="server" />										
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
								    <asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server CausesValidation=False />
								</ItemTemplate>
							</asp:TemplateColumn>	
						</Columns>
					</asp:DataGrid>
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
