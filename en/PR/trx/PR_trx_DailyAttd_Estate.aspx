<%@ Page Language="vb" src="../../../include/PR_trx_DailyAttd_Estate.aspx.vb" Inherits="PR_DailyAttd_Estate"%> 
<%@ Register TagPrefix="UserControl" Tagname="MenuPRTrx" src="../../menu/menu_prtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<html>
	<head>
		<title>Attendace List</title>
		<link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		          
//        function SelectAll(CheckBoxControl)
//            {
//            
//                if (CheckBoxControl.checked == true)
//                {
//                var i;
//                    for (i=0; i < document.forms[0].elements.length; i++)
//                    {
//                        if ((document.forms[0].elements[i].type == 'checkbox') &&
//                            (document.forms[0].elements[i].name.indexOf('dgEmpList') > -1) 
//                            )
//                        {
//                            document.forms[0].elements[i].checked = true;
//                            document.forms[0].elements[i+1].value = 'K';
//                            document.forms[0].elements[i+2].value = '1';
//                         }
//                    }
//                }
//                else
//                {
//                var i;
//                    for (i=0; i < document.forms[0].elements.length; i++)
//                    {
//                        if ((document.forms[0].elements[i].type == 'checkbox') &&
//                            (document.forms[0].elements[i].name.indexOf('dgEmpList') > -1))
//                        {
//                            document.forms[0].elements[i].checked = false;
//                            document.forms[0].elements[i+1].value = 'M';
//                            document.forms[0].elements[i+2].value = '';
//                        }
//                     }
//                }
//            }
            
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
	<%--<Preference:PrefHdl id=PrefHdl runat="server" />--%>
	    <form id=frmMain runat=server class="main-modul-bg-app-list-pu">
			<asp:Label id=SortExpression visible=false runat=server />
			<asp:Label id=SortCol visible=false runat=server />
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />


<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuPRTrx id=MenuPRTrx runat=server />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>LIST ABSENSI HARIAN</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
						<tr>
							<td style="background-color:#FFCC00" >
							<table cellpadding="4" cellspacing="0" style="width: 100%">
								<tr class="font9Tahoma">
								<td height="26" style="width: 15%">
								Attendance Date :<BR>
							    <asp:TextBox id=txtAttdDate  AutoPostBack=True Font-Bold="True" width="79%" maxlength="10" runat="server"  />
							    <a href="javascript:PopCal('txtAttdDate');"><asp:Image ID="btnSelDOB" runat="server" ImageUrl="../../Images/calendar.gif" /></a>
								</td>
								<td height="26" style="width: 11%">Employee Code :<BR><asp:TextBox id=txtEmpCode width=100% maxlength="20" runat="server" /></td>
                                <td height="26" style="width: 15%">Name :<BR><asp:TextBox id=txtEmpName width=100% maxlength="128" runat="server" /></td>
                                <td height="26" style="width: 10%">Division :<BR><asp:DropDownList id="ddlEmpDiv" width=100% runat="server" /></td>
								<td height="26" style="width: 13%">Type :<BR><asp:DropDownList id="ddlEmpType" width=100% runat="server" /></td>
                                <td height="26" style="width: 11%">Job :<BR><asp:DropDownList id="ddlEmpJob" width=100% runat="server" /></td>
                                <td height="26" width="10%"  valign=bottom align=right><asp:Button id=SearchBtn Text="Search" OnClick=srchBtn_Click runat="server" class="button-small"/></td>
								</tr>
							</table>
							
							</td>
						</tr>
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id=dgEmpList
							                AutoGenerateColumns=False width=100% runat=server
							                GridLines=None 
							                Cellpadding=2 
							                AllowPaging=True 
							                Pagesize=15 
							                OnItemDataBound=BindAbsType
							                OnPageIndexChanged=OnPageChanged 
							                Pagerstyle-Visible=False 
							                OnSortCommand=Sort_Grid  
						                    OnItemCommand=EmpLink_Click 
							                AllowSorting=True
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>
							
							                <Columns>
								
								                <asp:TemplateColumn HeaderText="Employee Code" SortExpression="EmpCode">
									                <ItemTemplate>
										                <asp:Label id=lblEmpCode text='<%# Container.DataItem("EmpCode") %>' visible=<%# not GetCheck(Container.DataItem("isAbsent"))%> runat=server/>
										                <asp:LinkButton id=lbEmpCode CommandName=Item text='<%# Container.DataItem("EmpCode") %>' visible=<%# GetCheck(Container.DataItem("isAbsent"))%> runat=server />
									                </ItemTemplate>
								                </asp:TemplateColumn>
							
								                <asp:TemplateColumn HeaderText="Name" SortExpression="EmpName">
									                <ItemTemplate>
										                <asp:Label id=lblEmpName text='<%# Container.DataItem("EmpName") %>' visible=<%# not GetCheck(Container.DataItem("isAbsent"))%> runat=server/>
										                <asp:LinkButton id=lbEmpName CommandName=Item text='<%# Container.DataItem("EmpName") %>' visible=<%# GetCheck(Container.DataItem("isAbsent"))%> runat=server />
									                </ItemTemplate>
								                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Employee Type" SortExpression="CodeEmpty">
                                	                <ItemTemplate>
										                <asp:Label id=lblEmptype text='<%# Container.DataItem("CodeEmpty") %>' visible=true runat=server/>
									                </ItemTemplate>
                                                </asp:TemplateColumn>
                                
                                                <asp:TemplateColumn HeaderText="Absent">
                                               <%-- <HeaderTemplate >
                                                <input type="CheckBox" CssClass="mr-h" name="SelectAllCheckBox" onclick="SelectAll(this)"> Absent
                                                </HeaderTemplate>--%>
                                                <ItemTemplate>
				                                <asp:CheckBox id="Absent" onclick="CheckChanged()" Checked=<%# GetCheck(Container.DataItem("isAbsent")) %> Enabled = <%# Not GetCheck(Container.DataItem("isAbsent")) %>  runat="server"/>	
			               	                    </ItemTemplate>
	                    		                </asp:TemplateColumn>
	                    		
	                    		                 <asp:TemplateColumn HeaderText="">
                                                 <ItemTemplate>
				                                      <asp:HiddenField id=tmpEmptype Value='<%# Container.DataItem("CodeEmpty") %>' runat=server/>
								                </ItemTemplate>
	                    		                </asp:TemplateColumn>
	                    		                    		
                                
                                                <asp:TemplateColumn HeaderText=" ">
                                                <ItemTemplate>
                                                   <asp:DropDownList ID="ddlabsen"  onchange="ddlchange()" runat="server" />
                                                   <asp:Label ID="lblabsen" Text='<%# trim(Container.DataItem("CodeAbsent"))%>' Visible=false runat="server" /> 
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
	                    		
                                            <asp:TemplateColumn HeaderText="HK">
                                                <ItemTemplate>
				                                <asp:TextBox id="HK" Width="50px" Text='<%# trim(Container.DataItem("Hk")) %>' Enabled = <%# Not GetCheck(Container.DataItem("isAbsent")) %>   onkeypress="javascript:return isNumberKey(event)" onkeyup="CheckValue()" runat="server"/>	
				                                </ItemTemplate>
                                                </asp:TemplateColumn>
                             
                                             <%--   
                                                <asp:TemplateColumn HeaderText="Unit">
                                                <ItemTemplate>
                                                   <asp:DropDownList ID="ddlunit" runat="server" Enabled = <%# Not GetCheck(Container.DataItem("isAbsent")) %> onchange="CheckValue()">
                                                    <asp:ListItem>HK</asp:ListItem>
                                                    <asp:ListItem>KG</asp:ListItem>
                                                   </asp:DropDownList>                                                         
                                                </ItemTemplate>
                                                </asp:TemplateColumn>--%>
								
								                <asp:TemplateColumn HeaderText="Last Update" SortExpression="UpdateDate">
									                <ItemTemplate>
										                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
								
								                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
									                <ItemTemplate>
										                <%# Container.DataItem("UserName") %>
									                </ItemTemplate>
								                </asp:TemplateColumn>
											
							                </Columns>
                                            <PagerStyle Visible="False" />
						                </asp:DataGrid>
                                    </td>
                                    </tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
							    &nbsp;</td>
						</tr>
						<tr>
							<td>
							<table cellpadding="2" cellspacing="0" style="width: 100%">
								<tr>
									<td style="width: 100%">&nbsp;</td>
									<td><img height="18px" src="../../../images/btfirst.png" width="18px" class="button" /></td>
									<td><asp:ImageButton ID="btnPrev" runat="server" alternatetext="Previous" commandargument="prev" imageurl="../../../images/btprev.png" onClick="btnPrevNext_Click" /></td>
									<td><asp:DropDownList ID="lstDropList" runat="server" AutoPostBack="True" onSelectedIndexChanged="PagingIndexChanged" /></td>
									<td><asp:ImageButton ID="btnNext" runat="server" alternatetext="Next" commandargument="next" imageurl="../../../images/btnext.png" onClick="btnPrevNext_Click" /></td>
									<td><img height="18px" src="../../../images/btlast.png" width="18px" class="button" /></td>
                                    <asp:Label id=lblCurrentIndex visible=false text=0 runat=server/>
						            <asp:Label id=lblPageCount visible=false text=1 runat=server/>
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <%-- <asp:ImageButton id="ImageButton1" onClick="checkAll" runat="server" imageurl="../../images/icn_prev.gif"  />
					            <asp:ImageButton id="ImageButton2" onClick="unCheckAll" runat="server" imageurl="../../images/icn_prev.gif"  />
					            --%>
                                <asp:ImageButton ID="btnSave" OnClick="Btnsave_Click" runat="server" AlternateText="Save" ImageUrl="../../images/butt_save.gif" />
                                <asp:Label id=lblRedirect visible=false runat=server/></td>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;
                                               
                            </td>
                        </tr>
					</table>
				</div>
				</td>
		        <table cellpadding="0" cellspacing="0" style="width: 20px">
			        <tr>
				        <td>&nbsp;</td>
			        </tr>
		        </table>
				</td>
			</tr>
		</table>




						<td align="left" width="100%" ColSpan=6>
                            &nbsp;
			</form>	
	</body>
</html>