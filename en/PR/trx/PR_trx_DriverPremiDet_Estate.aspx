<%@ Page Language="vb" src="../../../include/PR_trx_DriverPremiDet_Estate.aspx.vb" Inherits="PR_trx_DriverPremiDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Contract Payment Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		  function hit()
		 {
            var UOM = document.frmMain.TmpDrvBasisUOM.value
            if (UOM != '')
                {
                    var Tmp = document.frmMain.TmpDrvBasis.value
                    var Bss = document.frmMain.TxtDrvBasis.value
                    var Kg  = document.frmMain.TxtDrvKg.value
                    var Trp = document.frmMain.TxtDrvTrip.value
                    var Prm = document.frmMain.TxtDrvPremi.value
                                      
                    if (UOM=='KG')
                    {
                        var hsl = Kg - Tmp
                        if (hsl < 0) hsl = 0
                    }
                    else
                    {
                       var hsl = Trp - Tmp
                       if (hsl < 0) hsl = 0
                    }
                    document.frmMain.TxtDrvBasis.value = hsl
                    var Tot = round(parseFloat(hsl) * parseFloat(Prm),0)
                    document.frmMain.TxtDrvTotal.value = Tot
                    
                
                }
                else
                {
                alert('Pilih Job Desc dahulu !')
                document.frmMain.TxtDrvKg.value = ''
                document.frmMain.TxtDrvTrip.value = ''
                document.frmMain.TxtDrvTotal.value = ''
                }
                document.frmMain.TmpDrvPremi.value = hsl
                document.frmMain.TmpDrvTotal.value = document.frmMain.TxtDrvTotal.value
          }
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
			<table border=0 cellspacing=0 cellpadding=2 width=99% id="TABLE1" class="font9Tahoma">
				<tr>
					<td colspan="5"><UserControl:MenuPRTrx id=MenuPRTrx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5">
                      <strong>  PREMIUM DRIVER DETAILS</strong> <br /><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td colspan=5 style="height: 38px">&nbsp;</td>
				</tr>
				<tr>
					<td width=15% height=25>
                        Premi Driver Code :
                        </td>
					<td style="width: 425px"><asp:Label id=LblidM runat=server />
                        -
                        <asp:Label id=LblidD runat=server /></td>
					<td>&nbsp;</td>
					<td width=15%>Period : </td>
					<td style="width: 267px"><asp:Label id=lblPeriod runat=server /></td>
					
				</tr>
				<tr>
					<td height=25>
                        Driver : *</td>
					<td style="width: 425px; height: 27px"><asp:DropDownList ID="ddlEmpCode" runat="server" Width="88%" AutoPostBack=true />
                        <asp:Label id=lblEmpCode runat=server Visible="False" /></td>
					<td >&nbsp;</td>
					<td >Status : </td>
					<td style="width: 267px;"><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td style="height: 25px">
                        Car No : *
                    </td>
					<td style="width: 425px; height: 25px;" valign="top"><asp:DropDownList ID="ddlCarNo" runat="server" Width="88%" AutoPostBack=true /><asp:Label id=lblCarNo runat=server Visible="False" /></td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">Date Created : </td>
					<td style="width: 267px;"><asp:Label id=lblCreateDate runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Kenek 1 :</td>
					<td style="width: 425px; height: 27px">
                        <asp:DropDownList ID="ddlEmpCode_Kenek1" runat="server" Width="88%" AutoPostBack=true />
                        <br /><asp:Label id=lblKenek1 runat=server />
                            <asp:TextBox ID="txtpsn1" runat="server" Enabled="False" MaxLength="10" Width="10%"></asp:TextBox>
                        <asp:Label id=lblPsn1 runat=server Visible="False" />
                        %</td>       
					<td >&nbsp;</td>
					<td >Last Updated : </td>
					<td style="width: 267px;"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Kenek 2 :</td>
					<td style="width: 425px; height: 27px;">
					<asp:DropDownList ID="ddlEmpCode_Kenek2" runat="server" Width="88%" AutoPostBack=true />
                        <br /><asp:Label id=lblKenek2 runat=server Visible="False" />
                            <asp:TextBox ID="txtpsn2" runat="server" Enabled="False" MaxLength="10" Width="10%"></asp:TextBox>
                        <asp:Label id=lblPsn2 runat=server Visible="False" />
                        %</td>
					<td >&nbsp;</td>
					<td >Updated By : </td>
					<td style="width: 267px;"><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td height=25>
                        Date : *</td>
					<td style="width: 425px; height: 27px;">
                        <asp:TextBox ID="txtWPDate" runat="server" MaxLength="10" Width="50%"></asp:TextBox>
                        <asp:Image ID="btnSelDate" runat="server" ImageUrl="../../images/calendar.gif" /></td>
					<td >&nbsp;</td>
					<td >&nbsp;</td>
					<td style="width: 267px;">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td style="height: 26px">
                        Job Desc : *</td>
					<td style="height: 26px" ><asp:DropDownList ID="ddlJobDesc" runat="server" Width="88%" OnSelectedIndexChanged="ddlJobDesc_OnSelectedIndexChanged" AutoPostBack=true /></td>
					<td style="height: 26px"></td>
					
					<td style="height: 26px" ></td>
					<td style="width: 267px; height: 26px;" ></td>
				</tr>						
				<tr>
					<td height=25>Qty </td>
					<td style="width: 425px">
                        <asp:TextBox ID="TxtDrvKg" runat="server" MaxLength="10" Width="24%" onkeypress="return isNumberKey(event)" onkeyup="hit()" ></asp:TextBox>
                        Kg</td>
					<td></td>
					<td></td>
					<td style="width: 267px">
                        &nbsp;&nbsp;<!-- Modified BY ALIM -->
					</td>
				</tr>
				<tr>
					<td height=25>Trip</td>
					<td style="width: 425px">
                        <asp:TextBox ID="TxtDrvTrip" runat="server" MaxLength="10" Width="24%" onkeypress="return isNumberKey(event)" onkeyup="hit()" ></asp:TextBox></td>
					<td></td>
					<td></td>
				    <td style="width: 267px">
                        &nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td height=25>Ovr Basis</td>
					<td style="width: 425px">
                        <asp:TextBox ID="TxtDrvBasis" runat="server" MaxLength="10" Width="24%" CssClass="mr-h" ReadOnly=true></asp:TextBox>
                        <input type="hidden" id="TmpDrvBasis" value="0" runat=server/> 
                        <input type="hidden" id="TmpDrvBasisUOM" value="" runat=server/>
                    </td>
					<td></td>
					<td></td>
					<td style="width: 267px">
                        &nbsp;&nbsp;<!-- Modified BY ALIM -->
					</td>
				</tr>
				<tr>
					<td style="height: 28px"> Premi</td>
					<td style="width: 425px; height: 28px;">
                        <asp:TextBox ID="TxtDrvPremi" runat="server" MaxLength="10" Width="24%" CssClass="mr-h" ReadOnly=true></asp:TextBox></td>
					    <input type="hidden" id="TmpDrvPremi" value="" runat=server/>
					<td style="height: 28px"></td>
					<td style="height: 28px"></td>
					<td style="width: 267px; height: 28px;">
                        &nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td height=25>Sub Total</td>
					<td style="width: 425px">
                        <asp:TextBox ID="TxtDrvTotal" runat="server" MaxLength="10" Width="24%" CssClass="mr-h" ReadOnly=true></asp:TextBox></td>
					 <input type="hidden" id="TmpDrvTotal" value="" runat=server/>
					<td></td>
					<td></td>
					<td style="width: 267px">
                        &nbsp;&nbsp;
					</td>
				</tr>
				 <tr>
					                <td colspan=5><hr style="width :100%" /> </td>
				 </tr>
				                  
				<tr >
					<td colspan=5 style="height: 26px">
					<asp:ImageButton id=SaveBtn OnClick="SaveBtn_OnClick" AlternateText="  Save  " imageurl="../../images/butt_save.gif" runat=server />
			     	<asp:ImageButton id=DeleteBtn AlternateText=" Close " imageurl="../../images/butt_delete.gif" runat=server />
					<asp:ImageButton id="PrintBtn" AlternateText=" Print "  ImageURL="../../images/butt_print.gif" CausesValidation=false runat="server" />
					<asp:ImageButton id=BackBtn OnClick="BackBtn_OnClick" AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif"  runat=server />
					&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6 style="height: 42px">
					<table border=0 cellspacing=1 cellpadding=1 width=100%>
					    <tr>
					        <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted; border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent; width: 100%;">
                                Premi Driver
                            </td>
				        </tr>
					    <tr>
					    <td valign="top" style="width :100%">
					    <asp:DataGrid ID="dgDrvDet" 
                                      runat="server" 
                                      AllowSorting="True"
                                      AutoGenerateColumns="False" 
                                      CellPadding="2" 
                                      GridLines="None" 
                                      PagerStyle-Visible="False" 
                                      Width="100%"
                                      OnItemDataBound="KeepRunningSum_premi"
                                      ShowFooter=True CssClass="font9Tahoma">
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
                                <asp:TemplateColumn HeaderText="ID" SortExpression="PrmDrvLnID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# Container.DataItem("PrmDrvLnID") %>'
                                            Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lbID" runat="server" CommandName="Item" Text='<%# Container.DataItem("PrmDrvLnID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Date" SortExpression="PrmDate">
                                    <ItemTemplate>
                                       <asp:Label ID="lbldate" runat="server" Text='<%# format(Container.DataItem("PrmDate"),"dd/MM/yyyy") %>' Visible="false" />
                                       <asp:Label id=lbdate text='<%# objGlobal.GetLongDate(Container.DataItem("PrmDate")) %>'  runat=server/>
                                     </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Job Desc">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJob" runat="server" Text='<%# trim(Container.DataItem("CodeJob")) %>'/>
                                        <asp:Label ID="lbjob" visible=false runat="server" Text='<%# trim(Container.DataItem("CodePRDriver")) %>'/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Kg">
                                    <ItemTemplate>
                                    <asp:Label id=lblkg text='<%# Container.DataItem("kg") %>'  runat=server/>
				                   </ItemTemplate>
				                   <FooterTemplate >
									    <asp:Label ID=lbTotKg runat=server />
									</FooterTemplate>
                                    <ItemStyle BorderWidth=1px BorderStyle=Outset BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                    <FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Trip">
                                    <ItemTemplate>
									<asp:Label id=lbltrip text='<%# Container.DataItem("trip") %>'  runat=server/>
									</ItemTemplate>
									<FooterTemplate >
									    <asp:Label ID=lbTotTrip runat=server />
									 </FooterTemplate>
                                    <ItemStyle BorderWidth=1px BorderStyle=Outset BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                    <FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Right" />

                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Ovr Basis">
                                    <ItemTemplate>
									<asp:Label id=lblpremi text='<%# Container.DataItem("Premi") %>'  runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />

                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Premi">
                                    <ItemTemplate>
									<asp:Label id=lblrate text='<%# Container.DataItem("Rate") %>'  runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Sub Total">
                                <ItemTemplate>
										<asp:Label id=lbltotal text='<%# Container.DataItem("Total") %>'  runat=server/>
									</ItemTemplate>
									<FooterTemplate >
									    <asp:Label ID=lbltot runat=server />
									 </FooterTemplate>
                                    <ItemStyle BorderWidth=1px BorderStyle=Outset BorderColor="#CCCCCC" HorizontalAlign="Right" />
                                    <FooterStyle BorderWidth=1px BorderStyle=Outset BorderColor="Black" HorizontalAlign="Right" BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                               </Columns>
                            <PagerStyle Visible="False" />
                        </asp:DataGrid>
                    </td>
				</tr>
			 </table></tr>
				<tr>
					 <td colspan=5><hr style="width :100%" /> </td>				
				</tr>	
				<tr>
					<td colspan=3 valign="top">
                        Driver 100% x
                <asp:Label ID="LblLbr1" runat="server" ForeColor="Gold"></asp:Label>
                        =
                <asp:Label ID="LblAmntP1" runat="server" ForeColor="Gold"></asp:Label><br />
                        Kenek1
                                <asp:Label ID="lbpsn1" runat="server" ForeColor="Gold"></asp:Label>%
                            x
                            <asp:Label ID="LblLbr2" runat="server" ForeColor="Gold"></asp:Label>
                        =
                            <asp:Label ID="LblAmntP2" runat="server" ForeColor="Gold"></asp:Label><br />
                        Kenek2
                                <asp:Label ID="lbpsn2" runat="server" ForeColor="Gold"></asp:Label>%
                            x
                <asp:Label ID="LblLbr3" runat="server" ForeColor="Gold"></asp:Label>
                        =
                <asp:Label ID="LblAmntP3" runat="server" ForeColor="Gold"></asp:Label></td>			
				</tr>
				<tr>
					<td colspan="5">					                    
                    </td>
				</tr>
		    </table>
            <input type="hidden" id="isNew" value="" runat=server/>
            <input type="hidden" id="totkg" value="" runat=server/>
            <input type="hidden" id="tottrip" value="" runat=server/>
            <input type="hidden" id="totpremi" value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
			</form>
		
		
	</body>
</html>
