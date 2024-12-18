<%@ Page Language="vb" src="../../../include/PR_trx_BKMPopEmp_Estate.aspx.vb" Inherits="PR_DailyAttdDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Attendance Details</title>
			  <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
 	</head>
	<body onunload="window.opener.location=window.opener.location;" onload="javascript:self.focus();">
		<Preference:PrefHdl id=PrefHdl runat="server" />
		
		
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="lblRedirect" visible="false" runat="server" />
			<asp:Label id="lblReback" visible="false" runat="server" />
			<table id="tblMain" width="100%" runat="server" class="font9Tahoma">
			<tr>
				<td width="20%" class="mt-h">FIND EMPLOYEE</td>
				<td align="right" width="60%"><asp:label id="lblTracker" runat="server"/></td>
			</tr>
			<tr>
				<td colspan="2"><hr style="width :100%" /> </td>
			</tr>
			
			<tr>
				<td width="20%">
                    Divisi</td>
				<td width="80%">
                    <asp:DropDownList ID="ddldivisicode" runat="server" Width="75%">
                    </asp:DropDownList></td>
			</tr>
			

			<tr>
				<td>Name/Code : </td>
				<td>
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
			
		<table id="Table1" width="100%" runat="server" class="font9Tahoma">
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
								<asp:BoundColumn Visible="False" HeaderText="Employee Code" DataField="EmpCode" />
								<asp:BoundColumn Visible="False" HeaderText="Name" DataField="EmpName" />
								<asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-Width="10%" HeaderStyle-Width="10%"/>
								<asp:HyperLinkColumn HeaderText="Employee Code" ItemStyle-Width="15%" HeaderStyle-Width="15%" 
								    SortExpression="EmpCode"
									DataTextField="EmpCode" />
								<asp:HyperLinkColumn HeaderText="Name" ItemStyle-Width="40%" HeaderStyle-Width="40%"
									SortExpression="EmpName" 
									DataTextField="EmpName" />
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
