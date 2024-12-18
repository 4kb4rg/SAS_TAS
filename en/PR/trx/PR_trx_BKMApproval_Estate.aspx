<%@ Page Language="vb" src="../../../include/PR_trx_BKMApproval_Estate.aspx.vb" Inherits="PR_trx_BKMApproval_Estate"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Attendace List</title>
		 <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		          
        function SelectAll(CheckBoxControl)
            {
			var frm = document.forms[0];
            
                if (CheckBoxControl.checked == true)
                {
                var i;
                    for (i=0; i < document.forms[0].elements.length; i++)
                    {
                        if ((document.forms[0].elements[i].type == 'checkbox') &&
                            (document.forms[0].elements[i].name.indexOf('dgEmpList') > -1))
                        {
                            if (document.forms[0].elements[i].Enabled) 
								{
									document.forms[0].elements[i].checked = true;
								}
                        }
                     }
                }
                else
                {
                var i;
                    for (i=0; i < document.forms[0].elements.length; i++)
                    {
                        if ((document.forms[0].elements[i].type == 'checkbox') &&
                            (document.forms[0].elements[i].name.indexOf('dgEmpList') > -1))
                        {
                            if (document.forms[0].elements[i].Enabled) 
								{
									document.forms[0].elements[i].checked = false;
								}
                        }
                     }
                }
            }
            
            function CheckChanged()     
            {
                     
                var frm = document.forms[0];
                var boolAllChecked; 
                boolAllChecked=true;
                for(i=0;i< frm.length;i++)  
                {
                    e=frm.elements[i];  
                    if ( e.type=='checkbox' && e.name.indexOf('dgEmpList') != -1 )
                        if(e.checked== false) 
                        {
                        boolAllChecked=false;
                        break;
                        }
                 }                
//                for(i=0;i< frm.length;i++)
//                {
//                e=frm.elements[i];
//                    if ( e.type=='checkbox' && e.name.indexOf('SelectAllCheckBox') != -1 )
//                    {
//                     if( boolAllChecked==false) 
//                     e.checked= false ;     
//                     else
//                     e.checked= true;
//                     break;
//                    }
//                }
//                
                for(i=0;i< frm.length;i++)
                {
                e=frm.elements[i];
                
                if (e.name.indexOf('dgEmpList') != -1)
                {
                    
                    if ( e.type=='checkbox' && e.name.indexOf('dgEmpList') != -1 )
                    {
                    //alert(i +' '+frm.elements[i+1].name+' ' +frm.elements[i+1].value)
                     if( e.checked==false) 
                        { 
                            frm.elements[i+3].value = '';
                            frm.elements[i+2].value = 'M';
                        }
                     else
                        {
                        frm.elements[i+2].value = 'K';
                        if(frm.elements[i+1].value == 'BHL')
                           {
                           frm.elements[i+3].value = '0.71';
                           }
                        else
                            {   
                            if (frm.elements[i+3].value == '')
                                frm.elements[i+3].value = '1';
                            }
                        }
           
                    }
                }
                }
                
                    
            }
            
            function CheckValue()     
            {
                     
              var frm = document.forms[0];
              
              for(i=0;i< frm.length;i++)
              {
                e=frm.elements[i];
                   if ( e.type=='text' && e.name.indexOf('dgEmpList') != -1 )
                    {
                        if( frm.elements[i].value > 2) 
                        { 
                            alert('Max value 2')
                            e.value = 1
                            exit;
                         }
                      }
                }
                
             }
             
             function ddlchange()
             {
                  var frm = document.forms[0];
              
                    for(i=0;i< frm.length;i++)
                    {
                        e=frm.elements[i];
                        if ( e.type=='select-one' && e.name.indexOf('dgEmpList') != -1 )
                        {
                            if( frm.elements[i-2].checked && e.value == 'M') 
                            { 
                             frm.elements[i+1].value = '0';
                             exit;
                            }
                        }
                
                    }
             }
                
                
                          
            
        </script>

	</head>
	<body onload="javascript:document.frmMain.txtEmpCode.focus();">
	<Preference:PrefHdl id=PrefHdl runat="server" />


	    <form id=frmMain class="main-modul-bg-app-list-pu" runat=server >
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />


   		<table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">  

	    	<table border=0 cellspacing=0 cellpadding=2 width=99%>
				<tr>
					<td colspan=6 ><UserControl:MenuPRTrx id=MenuPRTrx runat=server /></td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan="3"><strong>APPROVAL BKM</strong> </td>
					<td colspan="3" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=6 width=100% class="font9Tahoma">
						<table width="100%" cellspacing="0" cellpadding="3" border="0" align="center" class="font9Tahoma">
							<tr class="mb-t">
								<td height="26" style="width: 15%">
								Tgl.BKM :<BR>
							    <asp:TextBox id=txtAttdDate CssClass="font9Tahoma"  Font-Bold="True" width="79%" maxlength="10" runat="server"  />
							    <a href="javascript:PopCal('txtAttdDate');"><asp:Image ID="btnSelDOB" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
								</td>
                                <td height="26" style="width: 13%">Divisi :<BR><asp:DropDownList id="ddlEmpDiv" width=100% CssClass="font9Tahoma" runat="server" /></td>
								<td height="26" style="width: 18%">Status :<BR><asp:DropDownList id="ddlEmpType" width=100% CssClass="font9Tahoma" runat="server" /></td>
                                <td height="26" width="60%"  valign=bottom align=right><asp:Button id=SearchBtn CssClass="button-small" Text="Search" OnClick=srchBtn_Click runat="server" /></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan=6 width=100%>					
						<asp:DataGrid id=dgEmpList
							AutoGenerateColumns=False width=100% runat=server
							GridLines=None 
							Cellpadding=2 
							AllowPaging=False 
							Pagerstyle-Visible=False 
						    OnItemCommand=EmpLink_Click  class="font9Tahoma">	
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
								<asp:TemplateColumn HeaderText="Kategory">
									<ItemTemplate>
									<%# Container.DataItem("idcat") %>&nbsp;-&nbsp;<%# Container.DataItem("idsubcat") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="#BKM">
									<ItemTemplate>
										<asp:Label id=lblEmpCode text='<%# Container.DataItem("BKMCode") %>'  visible=False runat=server />
										<asp:Label id=lblidx text='<%# Container.DataItem("idx") %>'  visible=False runat=server />
										<asp:Label id=lblcat text='<%# Container.DataItem("idcat") %>'  visible=False runat=server />
										<asp:Label id=lblStatus text='<%# Container.DataItem("Status") %>'  visible=False runat=server />
										<asp:Label id=lblID text='<%# Container.DataItem("ID") %>'  visible=False runat=server />
										<asp:LinkButton id=lnkEmpCode CommandName=Item text='<%# Container.DataItem("BKMCode") %>'  runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
							
								<asp:TemplateColumn HeaderText="Mandor/supir" >
									<ItemTemplate>
										<asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName")%>'  runat=server/>
									</ItemTemplate>
								</asp:TemplateColumn>
								
                                <asp:TemplateColumn HeaderText="Pekerjaan" >
                                	<ItemTemplate>
										<asp:Label id=lbljob text='<%# Container.DataItem("job") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
								
								 <asp:TemplateColumn HeaderText="Blok" >
                                	<ItemTemplate>
										<asp:Label id=lblblk text='<%# Container.DataItem("CodeBlok") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="HK" >
                                	<ItemTemplate>
										<asp:Label id=lblHk text='<%# Container.DataItem("Hk") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Hasil" >
                                	<ItemTemplate>
										<asp:Label id=lblHasil text='<%# Container.DataItem("Hasil") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Unit" >
                                	<ItemTemplate>
										<asp:Label id=lbluom text='<%# Container.DataItem("uom") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Bahan" >
                                	<ItemTemplate>
										<asp:Label id=lblstrname text='<%# Container.DataItem("stkname") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Qty" >
                                	<ItemTemplate>
										<asp:Label id=lblstkqty text='<%# Container.DataItem("stkqty") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
								
								<asp:TemplateColumn HeaderText="Unit" >
                                	<ItemTemplate>
										<asp:Label id=lblstkuom text='<%# Container.DataItem("stkuom") %>'  runat=server/>
									</ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Approve">
                                <HeaderTemplate >
                                <input type="CheckBox" CssClass="mr-h" name="SelectAllCheckBox" onclick="SelectAll(this)"> Approve
                                </HeaderTemplate>
                                <ItemTemplate>
								
				                <asp:CheckBox id="Absent" onclick="CheckChanged()" Checked=<%# GetCheck(Container.DataItem("status")) %>  Enabled = <%# GetEnable(Container.DataItem("Status"))%> visible = <%#  ShowCheck(Container.DataItem("id")) %>  runat="server"/>	
			               	    </ItemTemplate>
	                    		</asp:TemplateColumn>
	                    										
							</Columns>
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
					</td>
				</tr>
				</table>
		    <table colspan=6 width=100%>
				    <tr>
				        <td align=left width=50% style="height: 26px">
					    <%-- <asp:ImageButton id="ImageButton1" onClick="checkAll" runat="server" imageurl="../../images/icn_prev.gif"  />
					    <asp:ImageButton id="ImageButton2" onClick="unCheckAll" runat="server" imageurl="../../images/icn_prev.gif"  />
					    --%>
                        <asp:ImageButton ID="btnSave" OnClick="Btnsave_Click" runat="server" AlternateText="Save" ImageUrl="../../images/butt_save.gif" />
					    </td>
				    </tr>
				    </table>

						<td align="left" width="100%" ColSpan=6>
                            &nbsp;
                </div>
            </td>
        </tr>
        </table>
			</form>	
	</body>
</html>