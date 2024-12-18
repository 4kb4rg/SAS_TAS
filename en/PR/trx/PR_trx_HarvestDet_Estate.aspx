<%@ Page Language="vb" src="../../../include/PR_trx_HarvestDet_Estate.aspx.vb" Inherits="PR_trx_HarvestDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
	<title>Harvest Details</title>
	 <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
//		  //            
//			function calKg() 
//			{
//			var frm = document.forms[0];
//			for(i=0;i< frm.length;i++)  
//                {
//                    if (frm.elements[i].name.indexOf('UltraWebTab1') != -1)
//			        {
//			            
//			            e = frm.elements[i];
//			             if (e.id=='UltraWebTab1__ctl0_TxtPPanenJJg')
//			                 {
//			                 if (e.value=='') alert('Please Insert Qty');
//			                 var a = parseFloat(e.value);
//			                 }
//			             if (e.id=='UltraWebTab1__ctl0_TxtPPanenBJR')
//			                 var b = parseFloat(e.value);  
//			             if (e.id=='UltraWebTab1__ctl0_TxtPPanenKg')
//			                 {
//			                   e.value = round(a * b,2);
//			                   if (e.value=='NaN') e.value = 0;
//			                 }
//			            
//			        }
//                 
//			     }
//			     
//			 }
			 
//			 function hit()
//			 {
// 			    var ultraTab = igtab_getTabById("UltraWebTab1");
//                if (ultraTab == null)
//                return; 
//                
//                var tab = ultraTab.Tabs[0]; 
//                var a = tab.findControl("TxtPPanenJJg");
//                var b = tab.findControl("TxtPPanenBJR");
//                var c = tab.findControl("TxtPPanenKg");
//                c.value = round(parseFloat(a.value) * parseFloat(b.value),2)
//                if (c.value=='NaN') c.value = 0;
//                     
//			 }

         function hit()
		 {
            var a = document.frmMain.TxtPPanenJJg.value
            var b = document.frmMain.TxtPPanenBJR.value
            var c = round(a * b,2)
               if (c.value=='NaN') c.value = 0;
                            
            document.frmMain.TxtPPanenKg.value = c
                      
         }

         function chkdenda_buahmth()
         {
            if (document.frmMain.chk_buahmth.checked )
             {
                var a = document.frmMain.lbl_buahmth.value
                document.frmMain.txt_buahmth.value = a
             }
             else
             {
                document.frmMain.txt_buahmth.value = 0
             }
         }
         
          function chkdenda_buahtgl()
         {
            if (document.frmMain.chk_buahtgl.checked )
             {
                var a = document.frmMain.lbl_buahtgl.value
                document.frmMain.txt_buahtgl.value = a
             }
             else
             {
                document.frmMain.txt_buahtgl.value = 0
             }
         }
         
          function chkdenda_buahTPH()
         {
            if (document.frmMain.chk_buahTPH.checked )
             {
                var a = document.frmMain.lbl_buahTPH.value
                document.frmMain.txt_buahTPH.value = a
             }
             else
             {
                document.frmMain.txt_buahTPH.value = 0
             }
         }
         
          function chkdenda_tangkaiPj()
         {
            if (document.frmMain.chk_tangkaiPj.checked )
             {
                var a = document.frmMain.lbl_tangkaiPj.value
                document.frmMain.txt_tangkaiPj.value = a
             }
             else
             {
                document.frmMain.txt_tangkaiPj.value = 0
             }
         }
         
          function chkdenda_plepahskl()
         {
            if (document.frmMain.chk_plepahskl.checked )
             {
                var a = document.frmMain.lbl_plepahskl.value
                document.frmMain.txt_plepahskl.value = a
             }
             else
             {
                document.frmMain.txt_plepahskl.value = 0
             }
         }
         
			 
	</script>	
	</head>
	<body>
	    <Preference:PrefHdl id=PrefHdl runat="server" />
	   		<Form id=frmMain runat="server">
                          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
	   		<asp:Label id=lblErrMessage visible=false Text="" ForeColor="red" runat=server />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select one " runat="server" />
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table border=0 cellspacing=1 cellpadding=1 width=99%  class="font9Tahoma">
				<tr>
					<td colspan=6><UserControl:MenuPRTrx id=MenuPRTrx runat=server /></td>
				</tr>
				<tr>
					<td c colspan="3"><strong>BUKU POTONG BUAH DETAIL<asp:label id="lbl_header" runat="server"/> </strong> </td>
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
                        <GG:AutoCompleteDropDownList ID="ddldivisicode" AutoPostBack=true runat="server" Width="88%" OnSelectedIndexChanged="ddldivisicode_OnSelectedIndexChanged" />
                        <asp:Label ID="lbldivisicode" Visible=false runat="server" Width="100%" />
                    </td>
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px">Status : </td>
					<td style="width: 236px"><asp:Label id=lblStatus runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Mandor Code :*</td>
					<td style="width: 359px">
                        <GG:AutoCompleteDropDownList ID="ddlMandorCode" runat="server" AutoPostBack=true Width="88%" />
                        <asp:Label ID="LblMandorCode" Visible=false runat="server" Width="100%" />    
                    </td>
               		<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px" >Date Created : </td>
					<td style="width: 236px;"><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">KCS Code : *</td>
					<td style="width: 359px">
                        <GG:AutoCompleteDropDownList ID="ddlKraniCode" runat="server" AutoPostBack=true Width="88%" /><asp:Label ID="LblKraniCode" Visible=false runat="server" Width="100%" />
                    </td>    
					<td style="width: 29px">&nbsp;</td>
					<td style="width: 192px">Last Updated : </td>
					<td style="width: 236px"><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				
				<tr>
				    <td height=25 style="width: 206px">Employee Code :*</td>
					<td style="width: 359px">
                        <GG:AutoCompleteDropDownList ID="ddlEmpCode" runat="server" AutoPostBack=true Width="88%" OnSelectedIndexChanged="ddlEmpCode_OnSelectedIndexChanged"/><asp:Label ID="lblEmpCode" Visible=false runat="server" Width="100%" />
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
					<td height=25 style="width: 206px">
                        Attendace Date : *</td>
					<td style="width: 359px">
					    <GG:AutoCompleteDropDownList ID="ddlWpDate" AutoPostBack=true runat="server" Width="50%" OnSelectedIndexChanged="ddlWpDate_OnSelectedIndexChanged" />
                        &nbsp;
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
                                        <asp:CheckBox ID="chk_buahmth" runat="server" Text=" Denda Buah Mentah" onclick="chkdenda_buahmth()"/></td>
					                <td style="width: 236px">
					                <asp:TextBox Id="txt_buahmth"  Text="0" CssClass="mr-h" ReadOnly=true runat="server" Width="50%"  />
					                <Input type=hidden id="lbl_buahmth" runat=server />
					                </td>
					                
					            </tr>
					            
                                <tr>
				                	<td style="width: 206px; height: 26px;">Janjang: *</td>
					                <td style="width: 359px; height: 26px;">
                                    <asp:TextBox Id="TxtPPanenJJg" onkeypress="javascript:return isNumberKey(event)" onkeyup="hit()" runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px; height: 26px;">&nbsp;</td>
					                <td style="width: 192px; height: 26px">
                                        <asp:CheckBox ID="chk_buahtgl" runat="server" Text=" Denda Buah Tinggal" onclick="chkdenda_buahtgl()"/></td>
					                <td style="width: 236px; height: 26px;">
					                <asp:TextBox Id="txt_buahtgl" Text="0" CssClass="mr-h" ReadOnly=true runat="server" Width="50%"  />
					                <Input type=hidden ID="lbl_buahtgl" runat=server />
					                </td>
					            </tr>
					            
					            <tr>
				                	<td height=25 style="width: 206px">Hektar: *</td>
					                <td style="width: 359px">
                                    <asp:TextBox Id="TxtPPanenHa" onkeypress="javascript:return isNumberKey(event)"  runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px">&nbsp;</td>
					                <td style="width: 192px">
                                        <asp:CheckBox ID="chk_buahTPH" runat="server" Text=" Denda Buah tdk TPH" onclick="chkdenda_buahTPH()" /></td>
					                <td style="width: 236px">
					                <asp:TextBox Id="txt_buahTPH" Text="0" CssClass="mr-h" ReadOnly=true runat="server" Width="50%"  />
					                <Input type=hidden ID="lbl_buahTPH" runat=server />
					                </td>
					            </tr>
					            
					             <tr>
				                	<td style="width: 206px; height: 26px;">Rotasi ke :</td>
					                <td style="width: 359px; height: 26px;">
                                    <asp:TextBox Id="TxtPPanenRotasi" onkeypress="javascript:return isNumberKey(event)" runat="server" MaxLength="2" Width="50%"  /></td>
					                <td style="width: 29px; height: 26px;">&nbsp;</td>
					                <td style="width: 192px; height: 26px">
                                        <asp:CheckBox ID="chk_tangkaiPj" runat="server" Text=" Denda Tangkai Panjang" onclick="chkdenda_tangkaiPj()" /></td>
					                <td style="width: 236px; height: 26px;">
					                <asp:TextBox Id="txt_tangkaiPj" Text="0" CssClass="mr-h" ReadOnly=true runat="server" Width="50%"  />
					                <Input type=hidden ID="lbl_tangkaiPj" runat=server />
					                </td>
					              </tr>
					              
					            <tr>
				                	<td height=25 style="width: 206px">BJR :</td>
					                <td style="width: 359px">
                                    <asp:TextBox Id="TxtPPanenBJR" CssClass="mr-h" Text="0" ReadOnly=true runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px">&nbsp;</td>
					                <td style="width: 192px">
                                        <asp:CheckBox ID="chk_plepahskl" runat="server" Text=" Denda Pelepah Sengkleh" onclick="chkdenda_plepahskl()" /></td>
					                <td style="width: 236px">
					                <asp:TextBox Id="txt_plepahskl" Text="0" CssClass="mr-h" ReadOnly=true runat="server" Width="50%"  />
					                <Input type=hidden ID="lbl_plepahskl" runat=server />
					                </td>
					              </tr>  
					              
					              <tr>
				                	<td height=25 style="width: 206px">KG :</td>
					                <td style="width: 359px">
                                    <asp:TextBox Id="TxtPPanenKg" CssClass="mr-h" ReadOnly=true runat="server" MaxLength="10" Width="50%"  /></td>
					                <td style="width: 29px">&nbsp;</td>
					                <td style="width: 192px">
                                        Denda Tdk Dpt Basis</td>
					                <td style="width: 236px">
					                <asp:TextBox Id="txt_tdkbsis" Text="0" runat="server" Width="50%"  />
					                </td>
					              </tr>
					              
					              <tr>
					                <td colspan=5><hr size="1" noshade></td>
				                  </tr>
					              <tr>
					                <td colspan=5 style="height: 26px">
					                <asp:ImageButton id=BtnSavePrm OnClick="BtnSavePrm_OnClick" imageurl="../../images/butt_save.gif" AlternateText="Save"  runat=server />
						            <asp:ImageButton id=BtnDelPrm OnClick="BtnDelPrm_Click" imageurl="../../images/butt_delete.gif" AlternateText="Delete"  runat=server />
						          	<asp:ImageButton id="PrintBtn" AlternateText=" Print "  ImageURL="../../images/butt_print.gif" CausesValidation=false runat="server" />
                					<asp:ImageButton id="BackBtn" AlternateText="  Back  " OnClick="BackBtn_Click" CausesValidation=False imageurl="../../images/butt_back.gif"  runat=server />
						            </td>
				                </tr>
			
				<tr>
					<td colspan=6 style="height: 42px">
					    <table border=0 cellspacing=1 cellpadding=1 width=100%>
					    <tr>
					        <td align="center" style="border-right: green 1px dotted; border-top: green 1px dotted; border-left: green 1px dotted; border-bottom: green 1px dotted; background-color: transparent; width: 100%;">Premi Panen</td>
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
                             	<asp:TemplateColumn HeaderText="ID" SortExpression="PrmLnID" >
									<ItemTemplate>
										<asp:Label id=lblID text='<%# Container.DataItem("PrmLnID") %>'  Visible=false runat=server/>
										<asp:LinkButton id=lbID CommandName=Item text='<%# Container.DataItem("PrmLnID") %>' runat=server />
									</ItemTemplate>
									<ItemStyle Width=7% />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Date" SortExpression="PrmDate">
									<ItemTemplate>
									    <asp:Label id=lbldt text='<%# Format(Container.DataItem("PrmDate"),"dd/MM/yyyy") %>'  Visible=false runat=server/>
										<asp:Label id=lblDate text='<%# objGlobal.GetLongDate(Container.DataItem("PrmDate")) %>'  runat=server/>
									</ItemTemplate>
									<ItemStyle Width=10% />
								</asp:TemplateColumn>
							
							    <asp:TemplateColumn HeaderText="HK" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblhk text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Hk"),2),2) %>' runat=server/>
									    <asp:Label id=lbhk text='<%# Container.DataItem("Hk") %>' Visible=false runat=server/>
									</ItemTemplate>
								    <FooterTemplate >
									    <asp:Label ID=lbtothk runat=server />
									 </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width=7% />
                                    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Hektar" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblha text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Ha"),2),2) %>' runat=server/>
										<asp:Label id=lbha text='<%# Container.DataItem("Ha") %>' Visible=false runat=server/>
									</ItemTemplate>
								   <FooterTemplate >
									    <asp:Label ID=lbtotha runat=server />
									 </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width=7%/>
                                     <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								</asp:TemplateColumn>
								
								
								<asp:TemplateColumn HeaderText="JJG" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lbljjg text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("jjg"),2),2) %>' runat=server/>
										<asp:Label id=lbjjg text='<%# Container.DataItem("jjg") %>' Visible=false runat=server/>
									</ItemTemplate>
								   <FooterTemplate >
									    <asp:Label ID=lbtotjjg runat=server />
									 </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width=7% />
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
                                    <ItemStyle HorizontalAlign="Right" Width=7%/>
                                    <FooterStyle BorderWidth=1 BorderStyle=Outset BorderColor="#000000" HorizontalAlign="Right" BackColor="#cccccc" ForeColor="#000000" />
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Rotasi" HeaderStyle-HorizontalAlign="Right" >
									<ItemTemplate>
										<asp:Label id=lblrts text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Rotasi"),2),0) %>' runat=server/>
										<asp:Label id=lbrts text='<%# Container.DataItem("Rotasi") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width=5%/>
                                </asp:TemplateColumn>
								
							    <asp:TemplateColumn HeaderText="Buah Mentah" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDmentah text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_Mentah"),2),2) %>' runat=server/>
										<asp:Label id=lbDmentah text='<%# Container.DataItem("Dnd_Mentah") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width=7% />
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Buah Tinggal" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDtinggal text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_Tinggal"),2),2) %>' runat=server/>
										<asp:Label id=lbDtinggal text='<%# Container.DataItem("Dnd_Tinggal") %>' Visible=false runat=server/>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Right" Width=7%/>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Buah tdk TPH" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDtph text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_TPH"),2),2) %>' runat=server/>
										<asp:Label id=lbDtph text='<%# Container.DataItem("Dnd_TPH") %>' Visible=false runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" Width=7%/>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Tangkai Panjang" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDpjg text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_Pjng"),2),2) %>' runat=server/>
										<asp:Label id=lbDpjg text='<%# Container.DataItem("Dnd_Pjng") %>' Visible=false runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" Width=7%/>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Pelepah Sengkleh" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDSengkleh text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_Sengkleh"),2),2) %>' runat=server/>
										<asp:Label id=lbDSengkleh text='<%# Container.DataItem("Dnd_Sengkleh") %>' Visible=false runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" Width=7%/>
								</asp:TemplateColumn>
																
								<asp:TemplateColumn HeaderText="Tdk Dpt Basis" HeaderStyle-HorizontalAlign="Right" HeaderStyle-ForeColor=red>
									<ItemTemplate>
										<asp:Label id=lblDbasis text='<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Dnd_Basis"),2),2) %>' runat=server/>
										<asp:Label id=lbDbasis text='<%# Container.DataItem("Dnd_Basis") %>' Visible=false runat=server/>
									</ItemTemplate>
								    <ItemStyle HorizontalAlign="Right" Width=7%/>
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
				<tr>
					<td colspan=5>
									</td>
				</tr>
			</table>	
		  <Input type=hidden id=Hidblok value="" runat=server/>
		  <Input type=hidden id=isNew value="" runat=server/>
		  <Input type=hidden id=totha value="" runat=server/>
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
