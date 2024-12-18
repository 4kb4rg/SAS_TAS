<%@ Page Language="vb" src="../../../include/cb_trx_RekonsileDet.aspx.vb" Inherits="cb_trx_RekonsileDet" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Bank Reconciliation Details</title>		
		<Preference:PrefHdl id="PrefHdl" runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	
	
	
	<body >
		<form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">
	        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  	 
		<input type="hidden" id="inrid" value="" runat="server" />
		
		    <table id="tblHeader" cellspacing="0" cellpadding="2" width="100%" border="0" class="font9Tahoma">
		   
			<tr>
             <asp:Label id="lblStatusHidden" visible="false" text="0" runat="server" />
				<td > &nbsp;</td>
			</tr>
			<tr style="height:25px">
				<td class="font9Tahoma" style="width:20%; height: 25px;">
                   <strong>BANK RECONCILIATION DETAILS</strong> </td>
				<td  class="font9Header"  style="height: 25px; text-align: right;" colspan="4">
                    Last Update : <asp:Label ID="lblLastUpdate" runat="server" />&nbsp;|Date Created : <asp:Label id="lblDateCreated" runat="server" />&nbsp;| Update By : <asp:Label id="lblUpdatedBy" runat="server" /></td>
			</tr>
			<tr style="height:25px">
				<td style="height: 25px;" colspan="5">
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr style="height:25px">
				<td style="width:20%; height: 25px;">
                    Rekonsile ID: Rekonsile ID:</td>
				<td style="width:40%; height: 25px;">
                 <asp:Label id="lblRekonsileID" runat="server" /></td>
				<td style="width:5%; height: 25px;">&nbsp;</td>
				<td style="width:15%; height: 25px;">&nbsp;</td>
				<td style="width:20%; height: 25px;">&nbsp;</td>
			</tr>
			<tr style="height:25px" >
				<td style="width:20%; height: 25px;">
                    Periode*:</td>
				<td style="width:40%; height: 25px;">
                     <asp:DropDownList id="lstAccMonth" CssClass="font9Tahoma" width=20% runat=server>
							<asp:ListItem value="1">1</asp:ListItem>
							<asp:ListItem value="2">2</asp:ListItem>										
							<asp:ListItem value="3">3</asp:ListItem>
							<asp:ListItem value="4">4</asp:ListItem>
							<asp:ListItem value="5">5</asp:ListItem>
							<asp:ListItem value="6">6</asp:ListItem>
							<asp:ListItem value="7">7</asp:ListItem>
							<asp:ListItem value="8">8</asp:ListItem>
							<asp:ListItem value="9">9</asp:ListItem>
							<asp:ListItem value="10">10</asp:ListItem>
							<asp:ListItem value="11">11</asp:ListItem>
							<asp:ListItem value="12">12</asp:ListItem>
					</asp:DropDownList>
                    <asp:DropDownList id="lstAccYear" CssClass="font9Tahoma" width=30% runat=server>
									</asp:DropDownList></td>
				<td></td>
				<td style="width:15%; height: 25px;">&nbsp;</td>
				<td style="width:20%; height: 25px;">&nbsp;</td>
			</tr>
			<tr style="height:25px">
				<td >
                    Bank Code*:</td>
				<TD><asp:DropDownList width=100% id=ddlBank CssClass="font9Tahoma" autopostback=false  runat=server />
					<asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/></TD>
				<td>&nbsp;</td>
				<td >&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			
		
			<tr style="height:25px">
				<td style="height: 25px" >
                    From Date*:</td>
				<td style="height: 25px">
                    <asp:TextBox ID="txtFromDate" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="25%"></asp:TextBox>
                    <a href="javascript:PopCal('txtFromDate');"><asp:Image id="btnSelDateFrom" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        <asp:Label
                            ID="lblErrFromDate" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label></td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px" >&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
			</tr>
			<tr style="height:25px">
				<td >
                    To Date*:</td>
				<td>
                    <asp:TextBox ID="txtToDate" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="25%"></asp:TextBox>
                    <a href="javascript:PopCal('txtToDate');"><asp:Image id="btnSelDateTo" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
                        <asp:Label
                            ID="lblErrToDate" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			
			<tr>
				<td style="height: 23px">
                    Penambahan:</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>				
			</tr>
		
		    <tr>
				<td style="height: 23px">
                    Tanggal:</td>
				<td style="height: 23px" colspan="3">Keterangan:</td>
				
				<td style="height: 23px">Jumlah:</td>				
			</tr>
		
		    <tr>
				<td style="height: 23px"><asp:TextBox ID="txtAddDate1" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
                    <a href="javascript:PopCal('txtAddDate1');"><asp:Image id="btnAddDate1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label ID="lblErrAddDate1" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label>
				</td>
				<td style="height: 23px" colspan="3"> 
				<asp:TextBox ID="txtAddDescr1" CssClass="font9Tahoma" runat="server" MaxLength="128" Width="100%"></asp:TextBox>
				  </td>
				
				<td style="height: 23px">
				<asp:TextBox ID="txtAddAmount1" CssClass="font9Tahoma" Text = "0" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
				</td>				
			</tr>
			
			<tr>
				<td style="height: 23px"><asp:TextBox ID="txtAddDate2" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
                    <a href="javascript:PopCal('txtAddDate2');"><asp:Image id="btnAddDate2" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label ID="lblErrAddDate2" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label>
				</td>
				<td style="height: 23px" colspan="3"> 
				<asp:TextBox ID="txtAddDescr2" CssClass="font9Tahoma" runat="server" MaxLength="128" Width="100%"></asp:TextBox>
				  </td>
				
				<td style="height: 23px">
				<asp:TextBox ID="txtAddAmount2" Text = "0" CssClass="font9Tahoma"  runat="server" MaxLength="20" Width="100%"></asp:TextBox>
				</td>				
			</tr>
			
			<tr>
				<td style="height: 23px"><asp:TextBox ID="txtAddDate3" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
                    <a href="javascript:PopCal('txtAddDate3');"><asp:Image id="btnAddDate3" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label ID="lblErrAddDate3" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label>
				</td>
				<td style="height: 23px" colspan="3"> 
				<asp:TextBox ID="txtAddDescr3" CssClass="font9Tahoma" runat="server" MaxLength="128" Width="100%"></asp:TextBox>
				  </td>
				
				<td style="height: 23px">
				<asp:TextBox ID="txtAddAmount3" Text = "0" CssClass="font9Tahoma" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
				</td>				
			</tr>
			
			
		    
			<tr>
				<td style="height: 23px">
                    Pengurangan:</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>				
			</tr>
		
		    
		
		    <tr>
				<td style="height: 23px"><asp:TextBox ID="txtDedDate1" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
                    <a href="javascript:PopCal('txtDedDate1');"><asp:Image id="btnDedDate1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label ID="lblErrDedDate1" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label>
				</td>
				<td style="height: 23px" colspan="3"> 
				<asp:TextBox ID="txtDedDescr1" runat="server" CssClass="font9Tahoma" MaxLength="128" Width="100%"></asp:TextBox>
				  </td>
				
				<td style="height: 23px">
				<asp:TextBox ID="txtDedAmount1" Text = "0" CssClass="font9Tahoma" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
				</td>				
			</tr>
			
			<tr>
				<td style="height: 23px"><asp:TextBox ID="txtDedDate2" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
                    <a href="javascript:PopCal('txtDedDate2');"><asp:Image id="btnDedDate2" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label ID="lblErrDedDate2" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label>
				</td>
				<td style="height: 23px" colspan="3"> 
				<asp:TextBox ID="txtDedDescr2" runat="server" CssClass="font9Tahoma" MaxLength="128" Width="100%"></asp:TextBox>
				  </td>
				
				<td style="height: 23px">
				<asp:TextBox ID="txtDedAmount2" Text = "0" CssClass="font9Tahoma" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
				</td>				
			</tr>
			
			<tr>
				<td style="height: 23px"><asp:TextBox ID="txtDedDate3" CssClass="font9Tahoma" runat="server" MaxLength="10" Width="70%"></asp:TextBox>
                    <a href="javascript:PopCal('txtDedDate3');"><asp:Image id="btnDedDate3" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
				    <asp:Label ID="lblErrDedDate3" runat="server" ForeColor="red" Text="Please Fill Date With Correct format"></asp:Label>
				</td>
				<td style="height: 23px" colspan="3"> 
				<asp:TextBox ID="txtDedDescr3" runat="server" CssClass="font9Tahoma" MaxLength="128" Width="100%"></asp:TextBox>
				  </td>
				
				<td style="height: 23px">
				<asp:TextBox ID="txtDedAmount3" Text = "0" CssClass="font9Tahoma" runat="server" MaxLength="20" Width="100%"></asp:TextBox>
				</td>				
			</tr>
			
			<tr>
				<td colspan="3" style="height: 23px">&nbsp;</td>								
				<td align="right" style="height: 23px" >
                    &nbsp;</td>						
				<td style="height: 23px"></td>					
			</tr>
			
			<tr>
				<td style="height: 23px">
                    Saldo:</td>
				<td style="height: 23px">&nbsp;<asp:label id="lblSaldoAwal" text="0" runat="server" /></td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>				
			</tr>
			<tr style="height:25px">
				<td >
                    Saldo Bank:</td>
				<td>
                    <asp:TextBox ID="txtSaldoBank" CssClass="font9Tahoma" runat="server" text="0" MaxLength="30" Width="50%"></asp:TextBox>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr style="height:25px">
				<td colspan="4">
                   	<asp:label id="lblErrMessage" visible="false" forecolor="Red" Text="Error while initiating component." runat="server" /></td>
			</tr>
			
		
			
		    <tr>
				<td style="height: 23px">&nbsp;<asp:ImageButton ID="RefreshBtn" onclick="RefreshBtn_Click" ImageUrl="../../images/butt_refresh.gif" AlternateText="Print" visible="false" Runat="server" />&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px">&nbsp;</td>
				<td style="height: 23px" align="right" >&nbsp;Total Mutasi :</td>
				<td style="height: 23px">&nbsp;<asp:label id="lblTotAmt" text="0" runat="server" /></td>				
			</tr>
			<tr>
				<td colspan="5"> 
					<asp:DataGrid id="dgLineDet"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = "none"
						Cellpadding = "2"
						Pagerstyle-Visible="False"				
						AllowSorting="True" class="font9Tahoma">
						<HeaderStyle CssClass="mr-h" />							
						<ItemStyle CssClass="mr-l" />
						<AlternatingItemStyle CssClass="mr-r" />	
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
					<asp:TemplateColumn HeaderText="Type">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#Container.DataItem("PaymentType")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Check/Giro No.">
						<ItemStyle width="12%"/>
						<ItemTemplate>
							<asp:Label ID ="lblChequeNo" runat="server" text = <%#Container.DataItem("ChequeNo")%> />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Issue Date">
						<ItemStyle width="10%"/>
						<ItemTemplate>
							<%#objGlobal.GetLongDate(Container.DataItem("TrxDate"))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Payment ID">
						<ItemStyle width="19%"/>
						<ItemTemplate>
							<asp:Label ID ="lblTrxID" runat="server" text = <%#Container.DataItem("TrxID")%> />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Remarks">
						<ItemStyle width="16%"/>
						<ItemTemplate>
							<asp:Label ID ="lblRemarks" runat="server" text = <%#Container.DataItem("Remarks")%> />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign="Right">
						<ItemStyle width="12%" HorizontalAlign="Right"/>
						<ItemTemplate>
						    <%#objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Amount"), 2)%> 	
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Update By" HeaderStyle-HorizontalAlign="Right">
						<ItemStyle width="10%" HorizontalAlign="Right"/>
						<ItemTemplate>
							<%#Container.DataItem("UserName")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
							<ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" 
                                    Checked = <%#Container.DataItem("IsCheck")%> />
                                    
							</ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
					</asp:TemplateColumn>	
					
					<asp:TemplateColumn HeaderText="Source" Visible ="false">
						<ItemStyle width="19%"/>
						<ItemTemplate>
							<asp:Label ID ="lblSource" runat="server" text = <%#Container.DataItem("TrxSource")%> />
						</ItemTemplate>
					</asp:TemplateColumn>
					
					
					<asp:TemplateColumn HeaderText="Amount1" Visible ="false">
						<ItemStyle width="19%"/>
						<ItemTemplate>
							<asp:Label ID ="lblAmount" runat="server" text = <%#Container.DataItem("Amount")%> />
						</ItemTemplate>
					</asp:TemplateColumn>
					
					</Columns>										
                        <PagerStyle Visible="False" />
					</asp:DataGrid>
				</td>	
			</tr>
			
			<tr>
				<td colspan="3" style="height: 23px">&nbsp;</td>								
				<td align="right" style="height: 23px" >
                    &nbsp;</td>						
				<td style="height: 23px"></td>					
			</tr>
			
			<tr>
				<td colspan="5">&nbsp;</td>
			</tr>
			
			
			<tr>
				<td colspan="5">
					<asp:ImageButton ID="SaveBtn" onclick="SaveBtn_Click" ImageUrl="../../images/butt_save.gif" AlternateText="Save" Runat="server" />&nbsp;
					<asp:ImageButton ID="PrintBtn" onclick="PrintBtn_Click" ImageUrl="../../images/butt_print.gif" AlternateText="Print" visible="false" Runat="server" />
					<asp:ImageButton ID="DeleteBtn" onclick="DeleteBtn_Click" CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText="Delete" Runat="server" />&nbsp;
				    <br />
                    &nbsp;</td>
			</tr>
		</table>
	                                          </div>
            </td>
        </tr>
        </table>  
		</form>
	</body>
</html>
