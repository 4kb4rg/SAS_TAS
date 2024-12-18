<%@ Page Language="vb" src="../../../include/HR_trx_MandoranDet_Estate.aspx.vb" Inherits="HR_trx_MandoranDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>KeMandoran Details</title>
  <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">

            <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
            <tr>
            <td style="width: 100%; height: 1500px" valign="top">
            <div class="kontenlist"> 
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblList" visible="false" text="Select " runat="server" />
			
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma" > 
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="font9Tahoma" colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                   <strong> <asp:label id="lblTitle" runat="server" />DETAIL KEMANDORAN</strong></td>
                                <td  class="font9Header" style="text-align: right">
                                    Tgl buat : <asp:Label id=lblDateCreated runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Diupdate : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
										
				<tr>
					<td align="left" style="height: 26px; width: 108px;">
                        Kode Mandor</td>
					<td align="left" style="height: 26px; width: 413px;">
                        <asp:label id="lblidM" runat="server" /><asp:label id="lblidD" runat="server" /></td>
					<td style="width: 79px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>								
				<tr>
					<td align="left" style="height: 26px; width: 108px;">
                        Divisi</td>
					<td align="left" style="height: 26px; width: 413px;">
						<GG:AutoCompleteDropDownList id=ddlEmpDiv width="100%" class="font9Tahoma"  runat=server OnSelectedIndexChanged="ddlEmpDiv_OnSelectedIndexChanged" AutoPostBack=true/>
						<asp:label id="lbldivisi" runat="server" Visible=false />
						<asp:Label id=validddlEmpDiv forecolor=red visible=false runat=server />
						<GG:AutoCompleteDropDownList id=ddlMdrtype width="100%" visible=False class="font9Tahoma"  runat=server/>	
						<asp:label id="lblMdrType" visible=False runat="server" />
					</td>
					<td style="width: 79px; height: 26px;">&nbsp;</td>
					<td width=20% style="height: 26px">&nbsp;</td>
					<td width=25% style="height: 26px">&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 26px; width: 108px;">
                        Mandor
                    </td>
					<td align="left" style="height: 26px; width: 413px;">
					<GG:AutoCompleteDropDownList id=ddlMdrCode width="100%" class="font9Tahoma"  runat=server OnSelectedIndexChanged="ddlEmpMdr_OnSelectedIndexChanged" AutoPostBack=true/>
					<asp:label id="lblMdrCode" runat="server" Visible=false/>
					<asp:Label id=validddlMdrCode forecolor=red visible=false runat=server />
					
					<GG:AutoCompleteDropDownList id=ddlKcsCode width="100%" Visible=false class="font9Tahoma"  runat=server/>
					<asp:label id="lblkcsCode" runat="server" Visible=false/>
					<asp:Label id=validddlKcsCode forecolor=red visible=false runat=server />
					<GG:AutoCompleteDropDownList id=ddlTrnCode width="100%" Visible=false class="font9Tahoma"  runat=server/>
					<asp:label id="lbltrnCode" runat="server" Visible=false/>
					</td>
					<td style="width: 79px; height: 26px;">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="height: 26px; width: 108px;">
                    </td>
					<td align="left" style="height: 26px; width: 413px;">
					</td>
					<td style="width: 79px ;height: 26px;">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
					<td style="height: 26px">&nbsp;</td>
				</tr>
				
				<tr>
					<td align="left" style="height: 26px; width: 108px;">
                    </td>
					<td align="left" style="height: 26px; width: 413px;">
					</td>
					<td style="width: 79px ;height: 26px;">&nbsp;</td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>
				
				
				<tr>
					<td align="left" style="height: 26px; width: 108px;">
                        Nama Karyawan :  
                    </td>
					<td align="left" style="height: 26px; width: 413px;">
					<GG:AutoCompleteDropDownList id=ddlEmpCode width="100%" class="font9Tahoma"  runat=server/>&nbsp;
					</td>
					<td style="width: 79px ;height: 26px;">&nbsp;</td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>

				<tr>
					<td align="left" style="height: 26px; width: 108px;">
                        Periode :  
                    </td>
					<td align="left" style="height: 26px; width: 413px;">
					<asp:DropDownList ID="ddlEmpMonth" runat="server" Width="25%" OnSelectedIndexChanged="ddlEmpMonth_OnSelectedIndexChanged" class="font9Tahoma"  AutoPostBack=true>
                                        <asp:ListItem Value="01">January</asp:ListItem>
                                        <asp:ListItem Value="02">February</asp:ListItem>
                                        <asp:ListItem Value="03">March</asp:ListItem>
                                        <asp:ListItem Value="04">April</asp:ListItem>
                                        <asp:ListItem Value="05">May</asp:ListItem>
                                        <asp:ListItem Value="06">June</asp:ListItem>
                                        <asp:ListItem Value="07">July</asp:ListItem>
                                        <asp:ListItem Value="08">August</asp:ListItem>
                                        <asp:ListItem Value="09">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList><asp:DropDownList id="ddlyear" width="20%" runat=server OnSelectedIndexChanged="ddlEmpMonth_OnSelectedIndexChanged" AutoPostBack=true></asp:DropDownList></td>
					<td style="width: 79px ;height: 26px;">&nbsp;</td>
					<td style="height: 26px"></td>
					<td style="height: 26px"></td>
				</tr>


				<td colspan="5" style="height: 23px">&nbsp;<asp:Label id=validddlEmpCode forecolor=red visible=false runat=server /></td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Add  " imageurl="../../images/butt_add.gif" onclick=SaveBtn_Click CommandArgument=Save runat=server />
						&nbsp;
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
						&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="Copy Dari Periode :"  OnClick=Copybtn_Click />&nbsp;
                        <asp:DropDownList ID="ddlbeforemonth" class="font9Tahoma"  runat="server" Width="100px">
                            <asp:ListItem Value="01">January</asp:ListItem>
                            <asp:ListItem Value="02">February</asp:ListItem>
                            <asp:ListItem Value="03">March</asp:ListItem>
                            <asp:ListItem Value="04">April</asp:ListItem>
                            <asp:ListItem Value="05">May</asp:ListItem>
                            <asp:ListItem Value="06">June</asp:ListItem>
                            <asp:ListItem Value="07">July</asp:ListItem>
                            <asp:ListItem Value="08">August</asp:ListItem>
                            <asp:ListItem Value="09">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList><asp:DropDownList id="ddlbeforeyear" width="70px" runat=server>
                        </asp:DropDownList></td>
				</tr>
				
				<tr>
					<td colspan=6>
                        <asp:DataGrid ID="dgLine" runat="server" 
                             AllowPaging="True" 
                             AllowSorting="True" 
                             AutoGenerateColumns="False"
                             CellPadding="2" 
                             GridLines="None" 
                             PagerStyle-Visible="False" 
                             PageSize="15" 
                             Width="100%" OnDeleteCommand="dgLine_Delete" OnItemDataBound="dgLine_Bind" CssClass="font9Tahoma">
                            <PagerStyle Visible="False" />
                            <AlternatingItemStyle CssClass="mr-r" />
                            <ItemStyle CssClass="mr-l" />
                            <HeaderStyle CssClass="mr-h" />
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
                                <asp:TemplateColumn HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbID" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="NIK" SortExpression="CodeEmp">
                                    <ItemTemplate>
                                        <asp:Label ID="CodeEmp" runat="server" Text='<%#Container.DataItem("CodeEmp")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Nama" SortExpression="EmpName">
                                    <ItemTemplate>
                                        <asp:Label ID="EmpName" runat="server" Text='<%#Container.DataItem("EmpName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                             
                                <asp:TemplateColumn HeaderText="Tgl Update" SortExpression="UpdateDate">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <%#objGlobal.GetLongDate(Container.DataItem("UpdateDate"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                             
                                                            
                                <asp:TemplateColumn HeaderText="Diupdate" SortExpression="UserName">
                                    <ItemTemplate>
                                        <%# Container.DataItem("UserName") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                         <asp:LinkButton ID="Delete" CommandName="Delete" Text="Delete" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                                        
                             </Columns>
                        </asp:DataGrid></td>
				</tr>
				
				<Input Type=Hidden id=BlokCode runat=server />
				<asp:Label id=lblNoRecord visible=false text="Details not found." runat=server/>
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
			<Input type=hidden id=idMDR value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
