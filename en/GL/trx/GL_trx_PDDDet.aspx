<%@ Page Language="vb" trace="False" src="../../../include/GL_trx_PDDDet.aspx.vb" Inherits="GL_trx_PDDDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<html>
	<head>
		<title>Journal Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	
	<body >
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
		<asp:label id=lblErrMessage visible=false Text="Error while initiating component." forecolor=red runat=server />
		<asp:label id="issueType" Visible="False" Runat="server" />
		<asp:label id="lblStsHid" Visible="False" Runat="server"/>
		<asp:label id="blnShortCut" Visible="False" Runat="server"/>
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id="blnUpdate" Visible="False" Runat="server"/>
		<asp:label id=lblTxLnID runat=server Visible="False" />

		<table border=0 width="100%" cellspacing="0" cellpadding="2" class="font9Tahoma">
			<tr>
				<td colspan=6><UserControl:MenuGLTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>
                    <table cellpadding="0" cellspacing="2" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                              <strong> PERMINTAAN DROPING DANA </strong> </td>
                            <td class="font9Header" style="text-align: right">
                                Period : <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=Status runat=server />&nbsp;| Date Created : <asp:Label id=CreateDate runat=server />&nbsp;| Last Update : <asp:Label id=UpdateDate runat=server />&nbsp;| Updated By : <asp:Label id=UpdateBy runat=server />&nbsp;| Print Date : <asp:Label id=lblPrintDate visible="false" runat=server />&nbsp;| <asp:Label ID="lblSKBStartDate" runat="server" Visible="False"></asp:Label>&nbsp;: <asp:Label ID="LblIsSKBActive" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td colspan=6><hr size="1" noshade></td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td style="width: 198px">&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>				
			</tr>		
			<tr>
				<td width="20%" height=25>PDD ID :</td>
				<td width="40%"><asp:label id=lblTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%"></td>
				<td style="width: 198px">&nbsp;</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>PDD Date&nbsp; :*</td>
                <td>
                    <asp:TextBox id=txtDate Width="20%" maxlength=10 runat=server />
					<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/><br />
					<asp:label id=lblDate Text ="<BR>Date entered should be in the format " forecolor=Red Visible = False Runat="server" Height="16px" Width="224px"/> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="<br>Please enter document date" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
                </td>
				<td>&nbsp;</td>
				<td></td>
				<td style="width: 198px">&nbsp;</td>
				<td>&nbsp;</td>
			</tr>			
            <tr>
			    <td width="20%" height=25>Period :</td>
				<td width="40%">
				<asp:DropDownList id="lstMonth" Width="20%" runat=server></asp:DropDownList>
				<asp:DropDownList id="lstYear" Width="20%" runat=server></asp:DropDownList>
                <asp:ImageButton ID="BtnGetPDOList" runat="server" AlternateText="Get List PDO"
                        CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/icn_next.gif"
                        OnClick="BtnGetPDOList_Click" UseSubmitBehavior="false" />
				</td>
				<td width="5%">&nbsp;</td>
				<td width="15%"></td>
				<td style="width: 198px"></td>
				<td width="5%">&nbsp;</td>
            </tr>
            <tr>
			    <td width="20%" height=25>PDO ID :</td>
				<td width="40%"><asp:Label id=lblPDO visible=False runat=server /> <asp:DropDownList id="lstPDO" Width="41%"  runat=server>
                </asp:DropDownList>
				<asp:ImageButton ID="btnAddAllItem" runat="server" AlternateText="Get PDO"
                        CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/icn_next.gif"
                        OnClick="BtnAddAllItem_Click" UseSubmitBehavior="false" />
				</td>
				<td width="5%">&nbsp;</td>
				<td width="15%"></td>
				<td style="width: 198px"></td>
				<td width="5%">&nbsp;</td>
            </tr>
			<%--
			<tr>
			    <td width="20%" height=25>&nbsp;</td>
				<td width="40%">&nbsp;</td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Approved By :</td>
				<td style="width: 198px"><asp:Label id=lblapv runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>			
            <tr>
				<td width="20%" height=25>&nbsp;</td>
				<td width="40%">&nbsp;</td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Next Approved :</td>
				<td style="width: 198px"><asp:Label id=lblnextapv runat=server /></td>
				<td width="5%">&nbsp;</td>
            </tr>
			--%>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="6"> 
								<asp:DataGrid ID="dgDetail" runat="server" CellPadding="2" GridLines="Both" width="100%" AutoGenerateColumns=false ShowFooter=True 
							    OnItemDataBound=KeepRunningSum> 
								<AlternatingItemStyle CssClass="mr-r" />
                                <ItemStyle CssClass="mr-l" />
                                <HeaderStyle CssClass="mr-h" />
								 <Columns>
								<asp:TemplateColumn HeaderText="Keterangan" >
												<ItemTemplate>
													<asp:Label id=lblnamalst text='<%# Container.DataItem("Description")%>' runat=server/>
												</ItemTemplate>
								</asp:TemplateColumn>
											
								<asp:TemplateColumn HeaderText="QtyPDO" ItemStyle-HorizontalAlign=Right>
												<ItemTemplate>
													<asp:Label id=txtQty text='<%# Container.DataItem("Qty")%>'   runat=server/>
												</ItemTemplate>
								</asp:TemplateColumn>
											
								<asp:TemplateColumn HeaderText="Permintaan" ItemStyle-HorizontalAlign=Right>
												<ItemTemplate>
													<asp:Label id=txAmountPDO text='<%# FormatNumber(Container.DataItem("AmountPDO"), 0)%>'   runat=server/>
												</ItemTemplate>
											
								    <FooterTemplate >
									    <asp:Label ID=lbTotalPermintaan runat=server />
									</FooterTemplate>
									<FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />	
								</asp:TemplateColumn>	
								
								<asp:TemplateColumn HeaderText="Pemakaian" ItemStyle-HorizontalAlign=Right>
												<ItemTemplate>
													<asp:Label id=txAmountPDD text='<%# FormatNumber(Container.DataItem("AmountPDD"), 0)%>'  runat=server/>
												</ItemTemplate>
								    <FooterTemplate >
									    <asp:Label ID=lbTotalPemakaian runat=server />
									</FooterTemplate>
									<FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />			
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Selisih" ItemStyle-HorizontalAlign=Right>
												<ItemTemplate>
													<asp:Label id=txSelisihPDD text='<%# FormatNumber(Container.DataItem("Selisih"), 0)%>'  runat=server/>
												</ItemTemplate>
								    <FooterTemplate >
									    <asp:Label ID=lbTotalSelisih runat=server />
									</FooterTemplate>
									<FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />			
							    </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="% Selisih" ItemStyle-HorizontalAlign=Right>
												<ItemTemplate>
													<asp:Label id=txPsnPDD text='<%# FormatNumber(Container.DataItem("SelisihPsn"), 2)%>'  runat=server/>
												</ItemTemplate>
								</asp:TemplateColumn>
											
								</Columns>
								 </asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=6>
				<asp:DataGrid ID="dgsaldo" runat="server" CellPadding="2" GridLines="Both" width="40%" AutoGenerateColumns=false 
							    > 
								<AlternatingItemStyle CssClass="mr-r" />
                                <ItemStyle CssClass="mr-l" />
                                <HeaderStyle CssClass="mr-h" />
								 <Columns>
								<asp:TemplateColumn HeaderText="Keterangan" >
												<ItemTemplate>
													<asp:Label id=lblnamasd text='<%# Container.DataItem("Description")%>' runat=server/>
												</ItemTemplate>
								</asp:TemplateColumn>
											
								<asp:TemplateColumn HeaderText="Value" ItemStyle-HorizontalAlign=Right>
												<ItemTemplate>
													<asp:Label id=lblvalsd text='<%# FormatNumber(Container.DataItem("value"),0) %>'  runat=server/>
												</ItemTemplate>
								</asp:TemplateColumn>
				</Columns>
				</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colspan=6 style="height: 21px">&nbsp;</td>
			</tr>
			<tr>
				<td ColSpan="6">
					Penjelasan : <BR>
					<asp:Textbox id="txtDesc" Width=100% MaxLength=128 runat=server Height="56px" TextMode="MultiLine" />
				</td>
			</tr>
			<tr>
				<td align="left" colspan="6">
				    <asp:ImageButton id="NewBtn"  UseSubmitBehavior="false" onClick=NewBtn_Click     imageurl="../../images/butt_new.gif"     CausesValidation=False  AlternateText="New"     runat=server/>
					<asp:ImageButton id="Save"    UseSubmitBehavior="false" onClick=btnSave_Click    ImageURL="../../images/butt_save.gif"                            AlternateText="Save"    runat="server" />
					<asp:ImageButton id="Print"   UseSubmitBehavior="false" onClick=btnPrint_Click   ImageURL="../../images/butt_print.gif"   CausesValidation=False  AlternateText="Print"   runat="server" />
					<asp:ImageButton id="Back"    UseSubmitBehavior="false" onClick=btnBack_Click    ImageURL="../../images/butt_back.gif"    CausesValidation=False  AlternateText="Back"    runat="server" />
				</td>
			</tr>		
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
			    <td>&nbsp;</td>								
			    <td height=25 align=right>
                    &nbsp;</td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td style="width: 198px">&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>
		</table>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
			<input type="hidden" id="isNew" value="" runat="server" />
		</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
