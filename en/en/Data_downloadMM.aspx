<%@ Page Language="vb" src="../include/Data_downloadMM.aspx.vb" Inherits="Data_downloadMM" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>


<html>
	<head>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <title>Upload Reference File</title>
	</head>
	<body>
		<table border="0" width="100%" cellpadding="1" cellspacing="0">
			<tr>
				<td width="100%">
					<form id="frmMain" runat=server>
					<table id="tblDownload" border="0" cellpadding="0" cellspacing="0" width="100%" runat=server>
						<tr>
							<td class="mt-h" width="100%" colspan="4">DOWNLOAD TRANSACTION FILE</td>
						</tr>
						<tr>
							<td colspan=5><hr size="1" noshade></td>
						</tr>
						<tr>
							<td width="100%" colspan="5">Transaction Data</td>
						</tr>
						<tr>
							<td width="100%" colspan="5">&nbsp;</td>
						</tr>
						<TR>
							<TD width="100%" colSpan="5">Steps:</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="5">1.&nbsp; select data to export.</TD>
						</TR>
						<TR>
							<TD width="100%" colSpan="5">2.&nbsp; Click "Generate" button to generate the file.</TD>
						</TR>
						<tr>
							<td colspan="5">&nbsp;</td>
						</tr>
		
						<tr>
						    <td>&nbsp;Data:</td>
						    <td colspan="5" style="width: 244px">
                                &nbsp;<asp:DropDownList width="100%" id=ddlTable runat=server>
								        <asp:ListItem value="0" Selected=True>Historical Item Price</asp:ListItem>
								        </asp:DropDownList></td>
					         <td>&nbsp;</td>
					         <td>&nbsp;</td>
						</tr>
						<tr>
							<td colspan="5">&nbsp;</td>
						</tr>
						
				        <tr>
				            <td style="width: 15%">&nbsp;Date</td>
					        <td colspan="4">
                                &nbsp;<asp:Textbox id="txtDate" width="100px" maxlength=10 runat=server/>
						        <a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="images/calendar.gif"/></a>&nbsp;
                                To:&nbsp; &nbsp;<asp:Textbox id="txtDateTo" width="100px" maxlength=10 runat=server/>
						        <a href="javascript:PopCal('txtDateTo');"><asp:Image id="btnDateTo" runat="server" ImageUrl="images/calendar.gif"/></a>
					        </td>		
				        </tr>	
				        
				        
				        <tr>
							<td colspan="5">&nbsp;</td>
						</tr>
						<tr>
						    <td>&nbsp;File Type:</td>
						    <td colspan="5" style="width: 244px">
                                &nbsp;<asp:DropDownList width="50%" id=ddlFType runat=server>
								        <asp:ListItem value="0" Selected=True>DBase File</asp:ListItem>
								        <asp:ListItem value="1" >XML File</asp:ListItem>
								        </asp:DropDownList></td>
					         <td>&nbsp;</td>
					         <td>&nbsp;</td>
						</tr>
						
						 <tr>
							<td colspan="5"><hr size="1" noshade></td>
						</tr>
						
						<tr>
							<td colspan="5">
				<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Tbl_hist_itm" runat=server>	
				<tr>
					<td>Supplier :</td>
					<td><asp:textbox id="txtSupplier" width="50%" maxlength=20 runat="server" /> 
					    <input type=button value=" ... " id="Find" onclick="javascript:PopSupplier('frmMain', '', 'txtSupplier', 'False');" CausesValidation=False runat=server /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>								
				</tr>
				<tr>
					<td>Document No From :</td>
					<td><asp:textbox id="txtDocNoFrom" width="50%" maxlength=25 runat="server" /> (blank for all)</td>
					<td>To :</td>
					<td><asp:textbox id="txtDocNoTo" width="50%" maxlength=25 runat="server" /> (blank for all)</td>
				</tr>			
				<tr>
					<td>
                        Product Type Code : </td>
					<td><asp:textbox id="txtProdType" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        Product Brand Code : </td>
					<td><asp:textbox id="txtProdBrand" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td>
                        Product Model Code : </td>
					<td><asp:textbox id="txtProdModel" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>								
				<tr>
					<td>
                        Product Category Code : </td>
					<td><asp:textbox id="txtProdCat" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        Product Material Code : </td>
					<td><asp:textbox id="txtProdMaterial" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>			
				<tr>
					<td>
                        Analisis Stock Code : </td>
					<td><asp:textbox id="txtStkAna" maxlength=8 width="50%" runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>															
				<tr>
					<td>Item Code :</td>
					<td><asp:textbox id="txtItemCode" width="50%" maxlength=20 runat="server" /> 
					    <input type=button value=" ... " id="Find2" onclick="javascript:PopItem('frmMain', '', 'txtItemCode', 'False');" CausesValidation=False runat=server /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Item Description :</td>
					<td><asp:textbox id="txtItemDesc" width="50%" maxlength=20 runat="server" /> (blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Item Notes :</td>
					<td><asp:textbox id="txtAddNote" width="50%" runat="server" /> (SPK Purposes, blank for all)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
                        PO Type :</td>
					<td><asp:DropDownList id="lstPOType" width="50%" runat="server" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>									
				</tr>			
				<tr>
					<td width=15%>PO Status :</td>
					<td width=35%><asp:DropDownList id="lstStatus" size="1" width="50%" runat="server" /></td>
					<td width=15%>&nbsp;</td>
					<td width=35%>&nbsp;</td>
				</tr>			
				<tr>
					<td width=25%>Historical By : </td>
					<td colspan=2><asp:RadioButton id="rbItem" text="Item" checked="true" GroupName="rbSupp" runat="server" />
						<asp:RadioButton id="rbSupp" text="Supplier" GroupName="rbSupp" runat="server" />
					</td>			
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=4><asp:Label id="lblLocation" visible="false" runat="server" /></td>
				</tr>
						
			</table>
							</td>
						</tr>
						
						<tr>
							<td colspan="5"><asp:ImageButton id=btnGenerate onclick=btnGenerate_Click imageurl="images/butt_generate.gif" alternatetext="Generate" runat=server /></td>
						</tr>
					</table>
					
					<asp:Label id="lblErrMesage" visible="false" Text="Error while initiating component." runat="server" />
					<asp:Label id="lblDownloadfile" visible="true" runat="server" />
                        <br />
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowCustomPaging="True" AllowPaging="True"
                            AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
                             PagerStyle-Visible="False" PageSize="15" Width="100%">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Visible="False" />
                            <AlternatingItemStyle BackColor="White" CssClass="mr-r" />
                            <ItemStyle BackColor="#E3EAEB" CssClass="mr-l" />
                            <HeaderStyle BackColor="#1C5E55" CssClass="mr-h" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid></form>
				</td>
			</tr>
		</table>
	</body>
</html>
