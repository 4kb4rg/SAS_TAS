<%@ Page Language="vb" src="../../../include/GL_dayend_Process.aspx.vb" Inherits="GL_dayend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLMthEnd" src="../../menu/menu_GLMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Monthly Closing - Trial</title>
	    <style type="text/css">

.font9 {
	font-size: 9pt;
}
a {
	text-decoration:none;
}


hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
}
        </style>
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	<body>
		<form id=frmMain runat=server   onsubmit="ShowLoading()">
        <div class="kontenlist">
		<table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma" >
			<tr>
				<td colspan=5 align=center><UserControl:MenuGLMthEnd id=MenuGLMthEnd runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan=5>
                  <strong>  MONTHLY CLOSING TRIAL PROCESS</strong></td>
			</tr>
			<tr>
				<td colspan=5>
                         <hr style="width :100%" />
					</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
					<font color=red></font><p>
                        Process will generate jurnal on
                        selected periode:</td>
			</tr>
			<tr>
				<td colspan=5 style="height: 25px">&nbsp;</td>
			</tr>
			<tr>
				<td width=40% height=25>Accounting Period To Be Processed :</td> 
				<td width=30%>
					<asp:DropDownList id=ddlAccMonth runat=server/> / 
					<asp:DropDownList id=ddlAccYear OnSelectedIndexChanged=OnIndexChage_ReloadAccPeriod AutoPostBack=True runat=server />
				</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=10%>&nbsp;</td>
			</tr>
			<tr>
                <td colspan="3">
                <asp:CheckBox id="chkValidate" text=" Check confirmed/posted status ? " checked="false" runat="server" /></td>
            </tr>
			<tr>
				<td colspan=5 height=25>
                    <asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
				</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_process.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				&nbsp;</td>
			</tr>
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgViewActive
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false" CssClass="font9Tahoma">	
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
									<asp:TemplateColumn HeaderText="Transaction ID">
										<ItemStyle Width="40%" /> 
										<ItemTemplate>
											<asp:Label Text=<%# Container.DataItem("TrxID") %> id="lblTrxID" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Modul" > 
										<ItemTemplate >
											<asp:Label Text=<%# Container.DataItem("Balance") %> id="lblMdl" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>	
									<asp:TemplateColumn HeaderText="Status" > 
										<ItemTemplate >
											<asp:Label Text=<%# Container.DataItem("Status") %> id="lblMdl" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Sub Modul">
										<HeaderStyle HorizontalAlign="Right" /> 
										<ItemStyle HorizontalAlign="Right" Width="10%" />
										 <ItemStyle Width="40%" /> 
										<ItemTemplate>
											<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescr" runat="server" />
										</ItemTemplate>
									</asp:TemplateColumn>								
								</Columns>
					</asp:DataGrid>
				</td>
			</tr>	
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgViewJournal
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false" CssClass="font9Tahoma">	
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
							<asp:TemplateColumn HeaderText="Transaction ID">
							    <ItemStyle Width="20%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("TrxID") %> id="lblTrxID" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Description">
							     <ItemStyle Width="40%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="UnBalance Amount">
							    <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="30%" /> 
							    <ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Balance"), 2), 2) %> runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>									
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>	
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
			    <td>&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalBalance visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td>&nbsp;</td>
			    <td align=right><asp:label id="lblBalance" text="0" visible=false  runat="server" /></td>						
		    </tr>
		</table>
        </div>
		</form>
	</body>
	<script type="text/javascript">  
        function ShowLoading(e) {
            var div = document.createElement('div');
            var img = document.createElement('img');
            img.src = '/../en/images/load2.gif';
            div.innerHTML = "<br />";
            div.style.cssText = 'position: fixed; top: 0%; left: 0%; z-index: auto; width: 100%; height: 100%; text-align: center; background-color:rgba(255, 255, 255, 0.8);';
            div.appendChild(img);
            document.body.appendChild(div);
            return true;
            // These 2 lines cancel form submission, so only use if needed.  
            //window.event.cancelBubble = true;  
            //e.stopPropagation();  
        }
      </script>
	</html>
