<%@ Page Language="vb" src="../../../include/PR_trx_MuatTBSDet_Estate.aspx.vb" Inherits="PR_MuatTBSDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
	<title>Harvest Details</title>
	<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
        </script>	
	</head>
	<body>
	    <Preference:PrefHdl id=PrefHdl runat="server" />
	   		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">
                                <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
	   		<asp:Label id=lblErrMessage visible=false Text="" ForeColor="red" runat=server />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select one " runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table border=0 cellspacing=1 cellpadding=1 width=99% class="font9Tahoma">
				<tr>
					<td colspan=6><UserControl:MenuPRTrx id=MenuPRTrx runat=server /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3"><strong>BUKU MUAT TBS DETAIL<asp:label id="lbl_header" runat="server"/></strong> </td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td height=25 style="width: 206px">
                        ID</td>
					<td style="width: 359px">
                        <asp:Label ID="LblIDM" runat="server" Width="95px"></asp:Label>-<asp:Label ID="LblIDD"
                            runat="server" Width="95px"></asp:Label></td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px">Periode : </td>
					<td style="width: 236px"><asp:Label id=lblPeriod runat=server /></td>
					
				</tr>
				
				<tr>
					<td height=25 style="width: 206px">Divisi Code:*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddldivisicode" AutoPostBack=true runat="server" Width="88%" OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" />
                        <asp:Label ID="lbldivisicode" Visible=false runat="server" Width="100%" />
                    </td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px">Status : </td>
					<td style="width: 236px"><asp:Label id=lblStatus runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Mandor Code :*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddlMandorCode" runat="server" AutoPostBack=true Width="88%" />
                        <asp:Label ID="LblMandorCode" Visible=false runat="server" Width="100%" />    
                    </td>
               		<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px" >Date Created : </td>
					<td style="width: 236px;"><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">KCS Code : *</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddlKraniCode" runat="server" AutoPostBack=true Width="88%" /><asp:Label ID="LblKraniCode" Visible=false runat="server" Width="100%" />
                    </td>    
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px">Last Updated : </td>
					<td style="width: 236px"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Employee Code :*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddlEmpCode" runat="server" AutoPostBack=true Width="88%" OnSelectedIndexChanged="ddlEmpCode_OnSelectedIndexChanged"/><asp:Label ID="lblEmpCode" Visible=false runat="server" Width="100%" />
                     </td>
                  	<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px">Updated by : </td>
					<td style="width: 236px"><asp:Label id=lblupdatedby runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Block Code:*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddlblokcode" AutoPostBack=true runat="server" Width="88%" OnSelectedIndexChanged="ddlblokcode_OnSelectedIndexChanged" />
                        <asp:Label ID="lblblokcode" Visible=false runat="server" Width="100%" />
                    </td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px"></td>
					<td style="width: 236px;"></td>
				</tr>
				
				<tr>
					<td height=25 style="width: 206px">Date : *</td>
					<td style="width: 359px">
                        <asp:TextBox ID="txtWPDate" runat="server" MaxLength="10" Width="50%" />
                        <asp:Image   ID="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif" />
                     </td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px"></td>
					<td style="width: 236px"></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
				                	<td height=25 style="width: 206px">HK: </td>
					                <td style="width: 359px">
                                    <asp:TextBox Id="TxtPPanenHK" CssClass="mr-h" ReadOnly=true runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px">&nbsp;</td>
					                <td style="width: 192px">
                                        Denda </td>
					                <td style="width: 236px">
					                <asp:TextBox Id="txt_tdkbsis" onkeypress="javascript:return isNumberKey(event)" Text="0" runat="server" Width="50%"  /></td>
					                
					            </tr>
					            
                                <tr>
				                	<td style="width: 206px; height: 26px;">KG :</td>
					                <td style="width: 359px; height: 26px;">
                                    <asp:TextBox Id="TxtPPanenKg" onkeypress="javascript:return isNumberKey(event)" runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px; height: 26px;">&nbsp;</td>
					                <td style="width: 192px; height: 26px">
                                        </td>
					                <td style="width: 236px; height: 26px;">
                                        &nbsp;
					                </td>
					            </tr>
					            
					            <tr>
				                	<td style="width: 206px; height: 24px;">
                                        Muat / Bongkar :
                                    </td>
					                <td style="width: 359px; height: 24px;">
                                        <asp:RadioButton ID="rbmuat" runat="server" Checked=true GroupName="mb" Text=" Muat TBS" />
                                        <asp:RadioButton ID="rbbongkar" runat="server" GroupName="mb" Text=" Bongkar TBS" /></td>
					                <td style="width: 29px; height: 24px;">&nbsp;</td>
					                <td style="width: 192px; height: 24px;">
                                        </td>
					                <td style="width: 236px; height: 24px;">
                                        &nbsp;
					                </td>
					            </tr>
					              
					              <tr>
					                <td colspan=5><hr size="1" noshade></td>
				                  </tr>
					              <tr>
					                <td colspan=5 style="height: 26px">
					                <asp:ImageButton id=ImageButton1 OnClick="BtnSavePrm_OnClick" imageurl="../../images/butt_save.gif" AlternateText="Save"  runat=server />
						            <asp:ImageButton id=ImageButton2 imageurl="../../images/butt_delete.gif" AlternateText="Delete"  runat=server />
						            <asp:ImageButton id="PrintBtn" AlternateText=" Print "  ImageURL="../../images/butt_print.gif" CausesValidation=false runat="server" />
                					<asp:ImageButton id="BackBtn" AlternateText="  Back  " OnClick="BackBtn_Click" CausesValidation=False imageurl="../../images/butt_back.gif"  runat=server />
						            </td>
				                </tr>
			
				<tr>
					<td colspan=6 style="height: 42px">
					    <table border=0 cellspacing=1 cellpadding=1 width=100%>
					    <tr>
					        <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted; border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent; width: 100%;">
                                Bongkar/Muat TBS</td>
				        </tr>
					    <tr>
					        <td valign="top" style="width :100%">
                            <asp:DataGrid id=dgpremiln
							AutoGenerateColumns=False width=100% runat=server
							GridLines=None 
							Cellpadding=2 
							AllowSorting=True 
							ShowFooter=True
							OnItemDataBound=KeepRunningSum_premi CssClass="font9Tahoma"
							OnItemCommand=GetItem_dtl>
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
                             	<asp:TemplateColumn HeaderText="ID" SortExpression="MtbLnID" >
									<ItemTemplate>
										<asp:Label id=lblID text='<%# Container.DataItem("MtbLnID") %>'  Visible=false runat=server/>
										<asp:LinkButton id=lbID CommandName=Item text='<%# Container.DataItem("MtbLnID") %>' runat=server />
									</ItemTemplate>
									<ItemStyle Width=10% />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Date" SortExpression="MtbDate">
									<ItemTemplate>
									    <asp:Label id=lbldt text='<%# Format(Container.DataItem("MtbDate"),"dd/MM/yyyy") %>'  Visible=false runat=server/>
										<asp:Label id=lblDate text='<%# objGlobal.GetLongDate(Container.DataItem("MtbDate")) %>'  runat=server/>
									</ItemTemplate>
									<ItemStyle Width=15% />
								</asp:TemplateColumn>
							
							    <asp:TemplateColumn HeaderText="HK" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblhk text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Hk"),2),2) %>' runat=server/>
									    <asp:Label id=lbhk text='<%# Container.DataItem("Hk") %>' Visible=false runat=server/>
									</ItemTemplate>
								    <FooterTemplate >
									    <asp:Label ID=lbtothk runat=server />
									 </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width=10% />
                                    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								</asp:TemplateColumn>
								
																
								<asp:TemplateColumn HeaderText="KG" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblkg text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("kg"),2),2) %>' runat=server/>
									    <asp:Label id=lbkg text='<%# Container.DataItem("kg") %>' Visible=false runat=server/>
									</ItemTemplate>
									<FooterTemplate >
									    <asp:Label ID=lbtotkg runat=server />
									 </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width=10%/>
                                    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Muat/Bongkar" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblrts text='<%# Container.DataItem("BM") %>' runat=server/>
										<asp:Label id=lbrts text='<%# Container.DataItem("BM") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width=10%/>
                                </asp:TemplateColumn>
							
							   <asp:TemplateColumn HeaderText="Premi/Kg" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblprm text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Premi"),2),2) %>' runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" />
							   </asp:TemplateColumn>
							
							   <asp:TemplateColumn HeaderText="Premi" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lbltprm text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("SubTotalPremi"),2),2) %>' runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" />
								    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                </asp:TemplateColumn>
								
							   <asp:TemplateColumn HeaderText="Denda" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red >
									<ItemTemplate>
										<asp:Label id=lblDtot text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotDenda"),2),2) %>' runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" />
								    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                </asp:TemplateColumn>
							
							   <asp:TemplateColumn HeaderText="Sub Total" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lbltot text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"),2),2) %>' runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" />
								    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
                                </asp:TemplateColumn>
								
																																	
							</Columns>
                            <PagerStyle Visible="False" />
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" />
						    </asp:DataGrid>  
					        </td>
					      </tr>
					    
					    </table>
                </tr>
            <tr>
				<td colspan=5>
                    &nbsp;</td>
				</tr>
		  </table>	
		  <Input type=hidden id=Hidblok value="" runat=server/>
		  <Input type=hidden id=isNew value="" runat=server/>
		  <Input type=hidden id=tothk value="" runat=server/>
          <Input type=hidden id=totkg value="" runat=server/>
          <Input type=hidden id=totprm value="" runat=server/>
          <Input type=hidden id=totDenda value="" runat=server/>
          <Input type=hidden id=totupah value="" runat=server/>
          </div>
          </td>
          </tr>
          </table>
		</form>
	</body>
</html>
