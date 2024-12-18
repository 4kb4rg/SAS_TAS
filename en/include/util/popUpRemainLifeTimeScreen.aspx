<%@ Page Language="vb" trace=false src="../../../include/popUpRemainLifeTimeScreen.aspx.vb" Inherits="popUpRemainLifeTimeScreen" %>
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
    <form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
   		 <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1000px" valign="top">
			    <div class="kontenlist"> 

		<table id="tblMain" width="100%" class="font9Tahoma" runat="server">
			<tr>
				<td width="20%" class="mt-h"></td>
				<td align="right" width="60%"><asp:label id="lblTracker" runat="server"/></td>
			</tr>
			<tr>
				<td colspan="2"><hr size="1" noshade>
                    <asp:Label ID="lblHead" runat="server" Font-Size="Large" Font-Bold="True"></asp:Label></td>
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
				<td align="left" ColSpan="2" style="height: 21px">
					<asp:Label id="SortCol" Visible="False" Runat="server" />
				</td>
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
							Pagesize="5" 
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
                                <asp:BoundColumn HeaderText="Machine Code/Desc" DataField="SubBlkCode"></asp:BoundColumn>
								<asp:BoundColumn HeaderText="Item Code" DataField="ItemCode" />
								<asp:BoundColumn HeaderText="Description" DataField="SparePartName" />
                                <asp:BoundColumn HeaderText="Line Number/Description" DataField="LinesNo"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="Life Time" DataField="RemainLifeSpan"></asp:BoundColumn>
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
					</td>
				</tr>
                <tr>
                    <td colspan="2">
                        <asp:ImageButton ID="btnPrint" runat="server" AlternateText="Print" CausesValidation="False"
                            ImageUrl="../../images/butt_print.gif" OnClick="btnPreview_Click" UseSubmitBehavior="false" /></td>
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
