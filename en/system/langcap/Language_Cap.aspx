<%@ Page Language="vb"  src="../../../include/system_lang_langcap.aspx.vb" Inherits="system_lang_langcap"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuSYS" src="../../menu/menu_sys.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
<head>
<title>PENAMAAN ISTILAH</title>
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<Preference:PrefHdl id=PrefHdl runat="server" />
</head>
<body>
	<form id="frmBussTem" class="main-modul-bg-app-list-pu" runat="server" >
    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 2000px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<table border="0" cellspacing="0" cellpadding="2" width=100% class="font9Tahoma">
			<tr>
				<td colspan="2">
					<UserControl:MenuSYS id=MenuSYS runat="server" />
				</td>
			</tr>
			<tr>
				<td class="font9Tahoma" colspan="2"><strong>PENAMAAN ISTILAH</strong> </td>
			</tr>	
			<tr>
				<td colspan=2><hr style="width:100%">
                        </td>
			</tr>
			<tr>
				<td width=15%>Bahasa :</td>
				<td width=85%> 
					<asp:dropdownlist width=20% id="ddlLanguage" AutoPostBack="True" onSelectedIndexChanged="IndexChanged" runat="server">
					</asp:dropdownlist>
				</td>
			</tr>
			<!-- Millware Phase 1 2.1 Languange Caption PRM 17 Jul 2006 -->				
			<tr>
				<td colspan="2">
					<asp:DataGrid Runat=server ID=dgBussTerm
						Width="100%"
						GridLines="None"
						CellPading="2"
						AutoGenerateColumns="False" class="font9Tahoma">
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
						   <asp:BoundColumn HeaderText="Istilah Sistem" DataField="SystemTerm"/>
							<asp:TemplateColumn HeaderText="Istilah Kebun" ItemStyle-Width="25%" HeaderStyle-Width="35%">
								<ItemTemplate>
									<asp:TextBox id=txtBussTerm runat="server"
										Text='<%# DataBinder.Eval(Container.DataItem, "BusinessTerm") %>'
										Width=100% MaxLength=64 />
									<asp:Label id=lbTermCode runat="server"
										Text='<%# DataBinder.Eval(Container.DataItem, "TermCode") %>'
										visible=false />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Istilah Pabrik" ItemStyle-Width="25%" HeaderStyle-Width="30%">
								<ItemTemplate>
									<asp:TextBox id=txtBussTermMW runat="server"
										Text='<%# DataBinder.Eval(Container.DataItem, "BusinessTermMW") %>'
										Width=100% MaxLength=64 />
								</ItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
		</table>
		</br>
	    <asp:ImageButton id=btnUpdate imageurl="../../images/butt_save.gif" runat="server" AlternateText="  Save  " onClick="btnUpdate_click" />
                       </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
