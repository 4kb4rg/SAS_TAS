<%@ Page Language="vb" src="../../../include/TX_trx_TaxVerificationDet.aspx.vb" Inherits="TX_trx_TaxVerificationDet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<head>
    <title>Print Documents</title> 
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();document.frmMain.txtFromId.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">



		<table id=tblMain width=100% border=0 cellspacing="1" cellpadding="1"  runat=server class="font9Tahoma">
			<tr>
				<td colspan=2 class="mt-h">Tax Verification Detail</td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>
			<tr>
			    <td width=15% height=25>Supplier Code : </td>
				<td width=20%><asp:Textbox id=txtSplCode ReadOnly=true width=80% runat=server/>
				</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>Tax Status : </td>
				<td width=20%><asp:Label id=lblTaxStatus runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Supplier Name : </td>
				<td><asp:Textbox id=txtFromTo  ReadOnly=true width=80% runat=server/></td>
				<td>&nbsp;</td>
				<td>Date Update : </td>
				<td><asp:Label id=lblLastUpdate runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>NPWP Name : </td>
				<td><asp:Textbox id=txtSplName  ReadOnly=true width=80% runat=server/></td>
				<td>&nbsp;</td>
				<td>Update ID : </td>
				<td><asp:Label id=lblUpdatedBy runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>NPWP No. : </td>
				<td><asp:Textbox id=txtSplNPWP  ReadOnly=true width=80% runat=server/></td>
			</tr>
			<tr>
			    <td width=20%>NPWP Address :</td>
			    <td rowspan="5">
                   <textarea rows="4" id=txtSplAddress cols="50" readonly=readonly runat=server></textarea>
                </td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			
			<tr>
				<td width=20%>Doc ID : </td>
				<td width=50%><asp:TextBox id=txtDocId width=80% ReadOnly=true runat=server /></td>
			</tr>	
			<tr>
				<td width=20%>Printed ID : </td>
				<td width=50%><asp:TextBox id=txtTrxID width=80% ReadOnly=true runat=server /></td>
			</tr>
			<tr>
			    <td width=20%>Tax Object Group :</td>
			    <td width=50% valign=top>
                    <asp:DropDownList id="ddlTaxObjectGrp" width=80% Enabled=false runat=server>
                    </asp:DropDownList>
                </td>
			</tr>	
			<tr>
			    <td width=20%>KPP Location :</td>
			    <td width=50% valign=top>
                    <asp:DropDownList id="ddlKPP" width=80% runat=server>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
				<td>Total DPP Amount : </td>
				<td align=right><asp:Label id=lblTtlDPPAmt runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
			    <td width=20%>Remark :</td>
			    <td width=50%><asp:TextBox id=txtRemark width=80% runat=server /></td>
			    <td>&nbsp;</td>
				<td>Total Tax Amount : </td>
				<td align=right><asp:Label id=lblTtlTaxAmt runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			
			<tr>
			    <td colSpan="5">
				    <asp:DataGrid id=dgLineDet
					    AutoGenerateColumns="false" width="100%" runat="server"
					    GridLines=none
					    Cellpadding="2"
					    Pagerstyle-Visible="False"
					    AllowSorting="True"
                            class="font9Tahoma">	
							 
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
						   <asp:TemplateColumn HeaderText="Doc ID" ItemStyle-Width="15%">
							    <ItemTemplate>
								    <asp:Label visible=true Text=<%# Container.DataItem("DocID") %> id="lblDocID" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Description">
							    <ItemTemplate>
								    <asp:Label visible=true Text=<%# Container.DataItem("Description") %> id="lblDescription" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Tax Object">
							    <ItemTemplate> 
								    <ItemStyle />
									    <asp:Label Text=<%# Container.DataItem("TaxObject") %> id="lblTaxObject" runat="server" />
								    </ItemStyle>
							    </ItemTemplate>
						    </asp:TemplateColumn>	
						    <asp:TemplateColumn HeaderText="DPP Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							    <ItemTemplate> 
								    <ItemStyle />
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("DPPAmount"), 2), 2) %> id="lblIDDPP" runat="server" />
								    </ItemStyle>
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Rate" ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							    <ItemTemplate> 
								    <ItemStyle />
									    <asp:Label Text=<%#objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rate"), 2), 2) & "%"%> id="lblRate" runat="server" />
								    </ItemStyle>
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Tax Amount" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							    <ItemTemplate> 
								    <ItemStyle />
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TaxAmount"), 2), 2) %> id="lblIDTaxAmount" runat="server" />
								    </ItemStyle>
							    </ItemTemplate>
						    </asp:TemplateColumn>	
				    </Columns>										
				    </asp:DataGrid>
			    </td>
		    </tr>
			
			<!-- End -->
			<tr><td colspan=2>&nbsp;</td></tr>
			<tr>
				<td colspan=2 align=left>
					<asp:ImageButton id=VerifiedBtn onClick="VerifiedBtn_Click" alternatetext="Set Verified" imageurl="../../images/butt_verified.gif" runat=server/> 
					<asp:ImageButton id=CancelBtn onClick="CancelBtn_Click" alternatetext="Cancel verification" imageurl="../../images/butt_cancel.gif" runat=server/> 
					<input type=image src="../../images/butt_close.gif" alt=Close onclick="javascript:window.close();" width="58" height="20">
				</td>
			</tr>
			<tr>
				<td colspan=2 align=left>
                                            &nbsp;</td>
			</tr>
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
		</table>
		<asp:Label id=lblErrMessage visible=false ForeColor=red Text="Error while initiating component." runat=server />	
		<Input Type=Hidden id=hidOriDoc value=0 runat=server />	

        <br />
        </div>
        </td>
        </tr>
        </table>


    </form>
</body>
</html>
