<%@ Page Language="vb" src="../../../include/PR_trx_KutipBrondolanDet_Estate.aspx.vb" Inherits="PR_trx_KutipBrondolanDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
</script>

<html>
	<head>
	<title>Harvest Details</title>
	    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
         function hit()
		 {
            var a = document.frmMain.TxtPPanenJJg.value
            var b = document.frmMain.TxtPPanenBJR.value
            var c = round(a * b,2)
               if (c.value=='NaN') c.value = 0;
                            
            document.frmMain.TxtPPanenKg.value = c
                 
         }

         function chkdenda_brdktr()
         {
            if (document.frmMain.chk_brdktr.checked )
             {
                var a = document.frmMain.lbl_brdktr.value
                document.frmMain.txt_brdktr.value = a
             }
             else
             {
                document.frmMain.txt_brdktr.value = 0
             }
         }
         
         
          function chkdenda_tdkTPH()
         {
            if (document.frmMain.chk_tdkTPH.checked )
             {
                var a = document.frmMain.lbl_tdkTPH.value
                document.frmMain.txt_tdkTPH.value = a
             }
             else
             {
                document.frmMain.txt_tdkTPH.value = 0
             }
         }
         
     
			 
	</script>	
	</head>
	<body>
	    <Preference:PrefHdl id=PrefHdl runat="server" />
	   		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
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
					<td class="mt-h" colspan="3"><strong>BUKU KUTIP BRONDOLAN DETAIL<asp:label id="lbl_header" runat="server"/></strong> </td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 style="width: 206px">
                        ID</td>
					<td style="width: 359px">
                        <asp:Label ID="LblIDM" runat="server" Width="95px"></asp:Label>-<asp:Label ID="LblIDD"
                            runat="server" Width="95px"></asp:Label></td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 210px">Periode : </td>
					<td style="width: 236px"><asp:Label id=lblPeriod runat=server /></td>
					
				</tr>
				
				<tr>
					<td height=25 style="width: 206px">Divisi Code:*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddldivisicode" AutoPostBack=true runat="server" Width="88%" OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" />
                        <asp:Label ID="lbldivisicode" Visible=false runat="server" Width="100%" />
                    </td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 210px">Status : </td>
					<td style="width: 236px"><asp:Label id=lblStatus runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Mandor Code :*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddlMandorCode" runat="server" AutoPostBack=true Width="88%" />
                        <asp:Label ID="LblMandorCode" Visible=false runat="server" Width="100%" />    
                    </td>
               		<td style="width: 29px">&nbsp;</td>
					<td style="width: 210px" >Date Created : </td>
					<td style="width: 236px;"><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Employee Code :*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddlEmpCode" runat="server" AutoPostBack=true Width="88%" OnSelectedIndexChanged="ddlEmpCode_OnSelectedIndexChanged"/><asp:Label ID="lblEmpCode" Visible=false runat="server" Width="100%" /></td>    
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 210px">Last Updated : </td>
					<td style="width: 236px"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Block Code:*</td>
					<td style="width: 359px">
                        <asp:DropDownList ID="ddlblokcode" AutoPostBack=true runat="server" Width="88%" OnSelectedIndexChanged="ddlblokcode_OnSelectedIndexChanged" />
                        <asp:Label ID="lblblokcode" Visible=false runat="server" Width="100%" /></td>
                  	<td style="width: 29px">&nbsp;</td>
					<td style="width: 210px">Updated by : </td>
					<td style="width: 236px"><asp:Label id=lblupdatedby runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">
                        Date : *</td>
					<td style="width: 359px"><asp:DropDownList ID="ddlwpdate" AutoPostBack=true runat="server" Width="50%" OnSelectedIndexChanged="ddlwpdate_OnSelectedIndexChanged" />&nbsp;
                    </td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 210px"></td>
					<td style="width: 236px;"></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
				                	<td style="width: 206px; height: 26px;">HK: </td>
					                <td style="width: 359px; height: 26px;">
                                    <asp:TextBox Id="TxtPPanenHK" CssClass="mr-h" ReadOnly=true runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px; height: 26px;">&nbsp;</td>
					                <td style="width: 210px; height: 26px;">
                                        Denda Tdk Bersih Ngutip</td>
					                <td style="width: 236px; height: 26px;"><asp:TextBox Id="txt_ktpktr" Text="0" onkeypress="javascript:return isNumberKey(event)" runat="server" Width="50%"  /></td>
					                
					            </tr>
					            
					            <tr>
				                	<td height=25 style="width: 206px; height: 26px;">KG :</td>
					                <td style="width: 359px;height: 26px;">
                                    <asp:TextBox Id="TxtPPanenKg" onkeypress="javascript:return isNumberKey(event)" runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px;height: 26px;">&nbsp;</td>
					                <td style="width: 210px;height: 26px;">
					                <asp:CheckBox ID="chk_brdktr" runat="server" Text=" Denda Brondolan Kotor" onclick="chkdenda_brdktr()" /></td>
					                <td style="width: 236px;height: 26px;">
					                <asp:TextBox Id="txt_brdktr" CssClass="mr-h" ReadOnly=true Text="0" runat="server" Width="50%"  />
					                <Input type=hidden id="lbl_brdktr" runat=server /></td>
					              </tr>
                                	     
                                <tr>
				                	<td height=25 style="width: 206px; height: 26px;">Ha :</td>
					                <td style="width: 359px;height: 26px;">
                                    <asp:TextBox Id="TxtPPanenHa" onkeypress="javascript:return isNumberKey(event)" runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px;height: 26px;">&nbsp;</td>
					                <td style="width: 210px;height: 26px;">
					                <asp:CheckBox ID="chk_tdkTPH" runat="server" Text=" Denda Tdk TPH" onclick="chkdenda_tdkTPH()" /></td>
					                <td style="width: 236px;height: 26px;">
					                <asp:TextBox Id="txt_tdkTPH" Text="0" CssClass="mr-h" ReadOnly=true runat="server" Width="50%"  />
					                <Input type=hidden id="lbl_tdkTPH" runat=server /></td>
					              </tr>       
					            					            
					             <tr>
				                	<td style="width: 206px; height: 26px;">Rotasi ke :</td>
					                <td style="width: 359px; height: 26px;">
                                    <asp:TextBox Id="TxtPPanenRotasi" onkeypress="javascript:return isNumberKey(event)" runat="server" MaxLength="2" Width="50%"  /></td>
					                <td style="width: 29px; height: 26px;">&nbsp;</td>
					                <td style="width: 210px; height: 26px"></td>
                                    <td style="width: 236px; height: 26px;"></td>
					              </tr>
					              
					            				              
					              
					              
					              <tr>
					                <td colspan=5><hr size="1" noshade></td>
				                  </tr>
					              <tr>
					                <td colspan=5 style="height: 26px">
					                <asp:ImageButton id=ImageButton1 OnClick="BtnSavePrm_OnClick" imageurl="../../images/butt_save.gif" AlternateText="Save"  runat=server />
						            <asp:ImageButton id=ImageButton2 imageurl="../../images/butt_delete.gif" AlternateText="Delete"  runat=server />
						            <asp:ImageButton id="ImageButton3" AlternateText="  Print "  ImageURL="../../images/butt_print.gif" CausesValidation=false runat="server" />
					                <asp:ImageButton id="ImageButton4" AlternateText="  Back  " OnClick="BackBtn_Click" CausesValidation=False imageurl="../../images/butt_back.gif"  runat=server />
						            </td>
				                </tr>
			
				<tr>
					<td colspan=6 style="height: 42px">
					    <table border=0 cellspacing=1 cellpadding=1 width=100%>
					    <tr>
					        <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted; border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent; width: 100%;">Kutip Brondolan</td>
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
                             	<asp:TemplateColumn HeaderText="ID" SortExpression="BrdLnID" >
									<ItemTemplate>
										<asp:Label id=lblID text='<%# Container.DataItem("BrdLnID") %>'  Visible=false runat=server/>
										<asp:LinkButton id=lbID CommandName=Item text='<%# Container.DataItem("BrdLnID") %>' runat=server />
									</ItemTemplate>
									<ItemStyle Width=7% />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Date" SortExpression="BrdDate">
									<ItemTemplate>
									    <asp:Label id=lbldt text='<%# Format(Container.DataItem("BrdDate"),"dd/MM/yyyy") %>'  Visible=false runat=server/>
										<asp:Label id=lblDate text='<%# objGlobal.GetLongDate(Container.DataItem("BrdDate")) %>'  runat=server/>
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
								
								<asp:TemplateColumn HeaderText="Ha" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblha text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("ha"),2),2) %>' runat=server/>
									    <asp:Label id=lbha text='<%# Container.DataItem("Ha") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width=10%/>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Rotasi" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblrts text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rotasi"),2),0) %>' runat=server/>
										<asp:Label id=lbrts text='<%# Container.DataItem("Rotasi") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width=7%/>
                                </asp:TemplateColumn>
								
							    <asp:TemplateColumn HeaderText="Tdk Bersih Ngutip" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDktpktr text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_KtpKtr"),2),2) %>' runat=server/>
										<asp:Label id=lbDktpktr text='<%# Container.DataItem("Dnd_KtpKtr") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width=10% />
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Brondolan Kotor" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDbrdktr text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_Brdktr"),2),2) %>' runat=server/>
										<asp:Label id=lbDbrdktr text='<%# Container.DataItem("Dnd_Brdktr") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width=10%/>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Brondolan tdk TPH" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDtph text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_TPH"),2),2) %>' runat=server/>
										<asp:Label id=lbDtph text='<%# Container.DataItem("Dnd_TPH") %>' Visible=false runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" Width=10%/>
                                </asp:TemplateColumn>
								
												
								<asp:TemplateColumn HeaderText="Total Denda" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblDtot text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotDenda"),2),2) %>' runat=server/>
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
          <Input type=hidden id=totjjg value="" runat=server/>
          <Input type=hidden id=totkg value="" runat=server/>
          <Input type=hidden id=totDenda value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
