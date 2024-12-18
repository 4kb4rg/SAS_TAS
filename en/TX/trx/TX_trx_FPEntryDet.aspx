<%@ Page Language="vb" src="../../../include/TX_trx_FPEntryDet.aspx.vb" Inherits="TX_trx_FPEntryDet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<head>
    <title>Tax Invoice Detail</title> 
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <Preference:PrefHdl id=PrefHdl runat="server" />
    <script language="javascript">
		function calPPNPrice() {
			var doc = document.frmMain;
			var a = parseFloat(doc.txtFPDPPAmount.value);
			var b = parseFloat(doc.txtFPAmount.value);		
			var c = (a * (10/100));		    
			var newnumber = new Number(c+'').toFixed(parseInt(0));
			
			doc.txtFPAmount.value = newnumber;
			if (doc.txtFPAmount.value == 'NaN')
				doc.txtFPAmount.value = '';
			else
				doc.txtFPAmount.value = doc.txtFPAmount.value;
		}
	</script>	
</head>	
<body onload="javascript:self.focus();document.frmMain.txtFromId.focus();" leftmargin="2" topmargin="2">
    <form id=frmMain runat="server" class="main-modul-bg-app-list-pu">
		<tr>
             <td style="width: 80%; height: 1000px" valign="top">
			    <div class="kontenlist">


		<table id=tblMain width=100% border=0 cellspacing="1" cellpadding="1"  runat=server class="font9Tahoma">
			<tr>
				<td colspan=2 class="mt-h">Tax Invoice Detail</td>
			</tr>
			<tr>
			    <td width=10% height=25>Supplier Code : </td>
				<td width=20%><asp:Textbox id=txtSplCode ReadOnly=true width=80% runat=server/>
				</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>Create Date : </td>
				<td width=20%><asp:Label id=lblCreateDate runat=server /></td>
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
				<td>Create ID : </td>
				<td><asp:Label id=lblCreatedBy runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>NPWP No. : </td>
				<td><asp:Textbox id=txtSplNPWP  ReadOnly=true width=80% runat=server/></td>
				<td>&nbsp;</td>
				<td>Update ID : </td>
				<td><asp:Label id=lblUpdatedBy runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
			    <td width=20%>NPWP Address :</td>
			    <td>
                   <textarea rows="4" id=txtSplAddress cols="61" readonly=readonly runat=server></textarea>
                </td>
			</tr>
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			
			<tr>
				<td width=20%>Doc ID : </td>
				<td width=50%><asp:TextBox id=txtDocId width=80% ReadOnly=true runat=server /></td>
			</tr>	
			<tr>
				<td width=20%>Description : </td>
				<td width=50%><textarea rows="3" id=txtDescr cols="61" readonly=readonly runat=server></textarea></td>
			</tr>	
			<tr>
				<td width=20%>Doc Amount : </td>
				<td width=50%><asp:TextBox id=txtAmount width=80% ReadOnly=true runat=server /></td>
				<td>&nbsp;</td>
				<td>Total DPP Amount : </td>
				<td align=right><asp:Label id=lblTtlFPDPPAmt runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>	
			<tr>
			    <td width=20%>Remark :</td>
			    <td width=50%><asp:TextBox id=txtRemark width=80% runat=server /></td>
			    <td>&nbsp;</td>
				<td>Total Tax Amount : </td>
				<td align=right><asp:Label id=lblTtlFPAmt runat=server /></td>
				<td width=5%>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
            </table>

            <table width="80%" id="tblDetail" class="sub-Add" runat="server" >
			<tr>
				<td colSpan="6">
                    <table id="tblSelection" cellSpacing="0" cellPadding="2" width="100%" border="0" runat=server>
                        <tr class="mb-c">
                            <td><asp:Label id=Label10 text="Tax Invoice :" Font-Bold=true runat=server /></td>
                            <td colSpan="5">&nbsp;</td>
                        </tr>
                        <tr class="mb-c">
                            <td height=25>Number : </td>
                            <td colSpan="5"><asp:TextBox ID=txtFPNo maxlength=32 width=30% Runat=server /> 
                            </td>
                        </tr>
                        <tr class="mb-c">
                            <td height=25>Date : </td>
                            <td colSpan="5"><asp:TextBox ID=txtFPDate maxlength=10 width=10% Runat=server /> 
                            <a href="javascript:PopCal('txtFPDate');"><asp:Image id="btnSelFakturPjkDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                            <asp:Label id=lblerrFPDate forecolor=red text="Date format " runat=server />
                            <asp:label id=lblFmtFPDate  forecolor=red Visible = false Runat="server"/> 
                            </td>
                        </tr>
                        <tr class="mb-c">
                            <td width="20%" height=25>DPP Amount: </td>
                            <td colSpan="5"><asp:TextBox ID=txtFPDPPAmount maxlength=10 width=30% text=0 OnKeyUp="javascript:calPPNPrice();" Runat=server /> 
                                <asp:RegularExpressionValidator id=RegularExpressionValidator1 												
                                    ControlToValidate="txtFPDPPAmount"												
                                    ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
                                    Display="Dynamic"
                                    text="Maximum length 21 digits and 2 decimal points."
                                    runat="server"/> 
                            </td>
                        </tr>
                        <tr class="mb-c">
                            <td width="20%" height=25>Tax Amount: </td>
                            <td colSpan="5"><asp:TextBox ID=txtFPAmount maxlength=10 width=30% text=0 Runat=server /> 
                                <asp:RegularExpressionValidator id=RegularExpressionValidator2 												
                                    ControlToValidate="txtFPAmount"												
                                    ValidationExpression="\d{1,19}\.\d{0,2}|\d{1,19}"
                                    Display="Dynamic"
                                    text="Maximum length 21 digits and 2 decimal points."
                                    runat="server"/> 
                            </td>
                        </tr>
                        <tr class="mb-c">
                            <td width="20%" height=25>Additional Note : </td>
                            <td colSpan="5"><asp:TextBox ID=txtAddNote maxlength=256 width=50% Enabled=true Runat=server /></td>
                        </tr>

                        <tr class="mb-c">
                            <td colSpan="6">&nbsp;</td>
                        </tr>	
                        <tr class="mb-c">
	                        <td height=25 colspan=2>
		                        <asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add CausesValidation=True onclick=AddBtn_Click UseSubmitBehavior="false" runat=server /> 									
	                        </td>
	                        <td>&nbsp;</td>
	                        <td>&nbsp;</td>
	                        <td>&nbsp;</td>
	                        <td>&nbsp;</td>
                        </tr>
                    </table>
				</td>
			</tr>
            </table>

            <table style="width: 100%" class="font9Tahoma">
			<tr>
				<td>&nbsp;</td>
			</tr>
			
			<tr>
			    <td>
				    <asp:DataGrid id=dgLineDet
					    AutoGenerateColumns="false" width="100%" runat="server"
					    GridLines=none
					    Cellpadding="2"
					    Pagerstyle-Visible="False"
					    OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
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
						   <asp:TemplateColumn HeaderText="Tax No." ItemStyle-Width="15%">
							    <ItemTemplate>
								    <asp:label text=<%# Container.DataItem("FPNo") %> id="lblFPNo" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Date" ItemStyle-Width="7%">
							    <ItemTemplate>
								    <asp:label text=<%# objGlobal.GetLongDate(Container.DataItem("FPDate")) %> id="lblIDFPDate" runat="server" />
									<asp:label text=<%# Container.DataItem("FPDate") %> id="lblFPDate" Visible=false runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="DPP Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							    <ItemTemplate> 
								    <ItemStyle />
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("FPDPPAmount"), 2), 2) %> id="lblIDFPDPPAmount" runat="server" />
									    <asp:label text=<%# Container.DataItem("FPDPPAmount") %> id="lblFPDPPAmount" Visible=false runat="server" />
								    </ItemStyle>
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn HeaderText="Tax Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
							    <ItemTemplate> 
								    <ItemStyle />
									    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("FPAmount"), 2), 2) %> id="lblIDFPAmount" runat="server" />
									    <asp:label text=<%# Container.DataItem("FPAmount") %> id="lblFPAmount" Visible=false runat="server" />
								    </ItemStyle>
							    </ItemTemplate>
						    </asp:TemplateColumn>	
						    <asp:TemplateColumn HeaderText="Notes" ItemStyle-Width="25%">
							    <ItemTemplate>
								    <asp:label text=<%# Container.DataItem("AdditionalNote") %> id="lblFPNote" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>
						    <asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>
								    <asp:Label id=lblTrxLnID Text='<%# Trim(Container.DataItem("TrxLnID")) %>' Visible=False runat=server />
								    <asp:Label id=lblStatus Text='<%# Trim(Container.DataItem("Status")) %>' Visible=False runat=server />
								    <asp:Label id=lblInitStatus Text='<%# Trim(Container.DataItem("InitStatus")) %>' Visible=False runat=server />
									<asp:LinkButton id=lbDelete CommandName="Delete" Text="Delete" CausesValidation=False runat=server />
									<asp:LinkButton id=lbEdit CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
									<asp:LinkButton id=lbCancel CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
								</ItemTemplate>
							</asp:TemplateColumn>		
				    </Columns>										
				    </asp:DataGrid>
			    </td>
		    </tr>
			
			<!-- End -->
			<tr><td>&nbsp;</td></tr>
			<tr>
				<td align=left>
				    <asp:ImageButton id=VerifiedBtn onClick="VerifiedBtn_Click" alternatetext="Set Verified" imageurl="../../images/butt_verified.gif" runat=server/> 
					<asp:ImageButton id=CancelBtn onClick="CancelBtn_Click" alternatetext="Cancel verification" imageurl="../../images/butt_cancel.gif" runat=server/>
					<input type=image src="../../images/butt_close.gif" alt=Close onclick="javascript:window.close();" width="58" height="20">
				</td>
			</tr>
			<tr>
				<td align=left>
                                            &nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
			</tr>
		</table>
		<asp:Label id=lblErrMessage visible=false ForeColor=red Text="Error while initiating component." runat=server />	
		<Input Type=Hidden id=hidOriDoc value=0 runat=server />	
		<Input Type=Hidden id=hidTrxID value="" runat=server />	
		<Input Type=Hidden id=hidTrxLnID value="" runat=server />	
		<Input Type=Hidden id=hidTtlDocAmount value=0 runat=server />
		<Input Type=Hidden id=hidTtlFPAmount value=0 runat=server />	
		<Input Type=Hidden id=hidFPAmount value=0 runat=server />


        <br />
        </div>
        </td>
        </tr>
    </form>
</body>
</html>
