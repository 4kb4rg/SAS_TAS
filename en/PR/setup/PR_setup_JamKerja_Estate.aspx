<%@ Page Language="vb" src="../../../include/PR_Setup_Jamkerja_Estate.aspx.vb" Inherits="PR_Setup_Jamkerja_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Jam Kerja</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		function setovrhour()
        {
        var doc = document.frmMain.EventData;
        
        var s = doc.StartTm.value;
        var e = doc.EndTm.value;
//        
        if ((s.length==5) && (e.length==5))
        {
        var s1 = s.substring(2,3);
        var e1 = e.substring(2,3);
        
           if ((s1==":")&&(e1==":"))
            {
                var s2 = parseFloat(s.substring(0,2))
                var ms2 = parseFloat(s.substring(3,5))
                var e2 = parseFloat(e.substring(0,2))
                var me2 = parseFloat(e.substring(3,5))
            
                if (e2 < s2)
                {
                               
                    var a = (24-s2)+e2;
                    var b = ((60+me2)-ms2)/60;
                    doc.TxtQty.value = (a+b).toFixed(2);
                }
                else
                {
                     var a = (e2 - s2)-1;
                     var b = ((60+me2)-ms2)/60;
                     doc.TxtQty.value = (a+b).toFixed(2);
                }
                //setOvertimePsn()                
            }
            else
            {
                exit;
            }
        }
        else
        {
        exit;
        }      
        }
		 </script>
	</head>
<%--	<Preference:PrefHdl id=PrefHdl runat="server" />--%>
		<body>
		    <form id="frmMain" runat="server">
			<asp:label id="SQLStatement" Visible="False" Runat="server"></asp:label>
			<asp:label id="SortExpression" Visible="False" Runat="server"></asp:label>
			<asp:label id="blnUpdate" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="lblValidate" visible="false" text="Please enter " runat="server" />
			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="curStatus" Visible="False" Runat="server"></asp:label>
			<asp:label id="sortcol" Visible="False" Runat="server"></asp:label>
			<asp:Label id="ErrorMessage" runat="server" />


		<table cellpadding="0" cellspacing="0" style="width: 100%">
				<tr>
					<td colspan="6">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
			<tr>
				<td style="width: 100%; height: 800px" valign="top">
				<div class="kontenlist">
					<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
						<tr>
							<td><strong>JAM KERJA</strong><hr style="width :100%" />   
                            </td>
                            
						</tr>
                        <tr>
                            <td align="right"><asp:label id="lblTracker" runat="server" /></td> 
                        </tr>
				        <tr>
					       <%-- <td colspan=6><hr size="1" noshade></td>--%>
				        </tr>
					 
						    <tr>
							    <td>
							    <table cellpadding="4" cellspacing="1" style="width: 100%">
                                    <tr>
                                    <td>
						            <asp:DataGrid id="EventData"
						                AutoGenerateColumns="false" width="100%" runat="server"
						                GridLines = none
						                Cellpadding = "2"
						                OnEditCommand="DEDR_Edit"
						                OnUpdateCommand="DEDR_Update"
						                OnCancelCommand="DEDR_Cancel"
						                OnDeleteCommand="DEDR_Delete"
						                AllowPaging="True" 
						                Allowcustompaging="False"
						                Pagesize="15" 
						                OnPageIndexChanged="OnPageChanged"
						                Pagerstyle-Visible="False"
						                OnSortCommand="Sort_Grid" 
						                AllowSorting="True"
                                                        class="font9Tahoma">
								
							                            <HeaderStyle CssClass="mr-h" BackColor="#CCCCCC"/>
							                            <ItemStyle CssClass="mr-l" BackColor="#FEFEFE"/>
							                            <AlternatingItemStyle CssClass="mr-r" BackColor="#EEEEEE"/>						
					                <Columns>					
					
					                <asp:TemplateColumn HeaderText="ID">
						                <ItemTemplate>
							                <%#Container.DataItem("IDJAM")%>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="IDJAM" MaxLength="8" width=95%
								                Text='<%# trim(Container.DataItem("IDJAM")) %>' ReadOnly=true runat="server"/><BR>
							                <asp:RequiredFieldValidator id=validateCode display=dynamic runat=server 
								                ControlToValidate=IDJAM />
							                <asp:RegularExpressionValidator id=revCode 
								                ControlToValidate="IDJAM"
								                ValidationExpression="[a-zA-Z0-9\-]{1,8}"
								                Display="Dynamic"
								                text="Alphanumeric without any space in between only."
								                runat="server"/>
							                <asp:label id="lblDupMsg" Text="Code already exist" Visible = false forecolor=red Runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					
					                <asp:TemplateColumn  HeaderText="Periode Start">
						                <ItemTemplate>
							                <%#Container.DataItem("Periodestart")%>
						                </ItemTemplate>
						                <EditItemTemplate>													
							                <asp:TextBox id="Periodestart" width=100% MaxLength="6"	Text='<%# trim(Container.DataItem("Periodestart")) %>' runat="server"/>			
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					
					                <asp:TemplateColumn HeaderText="Periode End">
						                <ItemTemplate>
							                <%#Container.DataItem("Periodeend")%>
						                </ItemTemplate>
						                <EditItemTemplate>													
							                <asp:TextBox id="Periodeend" width=100% MaxLength="6"	Text='<%# trim(Container.DataItem("Periodeend")) %>' runat="server"/>			
						                </EditItemTemplate>
					                </asp:TemplateColumn>	
					
					                <asp:TemplateColumn HeaderText="Description">
						                <ItemTemplate>
							                <%# Container.DataItem("Ket") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="Description" width=100% MaxLength="64"	Text='<%# trim(Container.DataItem("Ket")) %>' runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>		

					                <asp:TemplateColumn HeaderText="Jam Masuk (hh:mm)">
						                <ItemTemplate>
							                <%# Container.DataItem("StartTm")%>
						                </ItemTemplate>
						                <EditItemTemplate>													
							                <asp:TextBox id="StartTm" width=100% MaxLength="5"	Text='<%# trim(Container.DataItem("StartTm")) %>'  runat="server"/>					
						                </EditItemTemplate>
					                </asp:TemplateColumn>						
					
					                <asp:TemplateColumn HeaderText="Jam Keluar (hh:mm)">
						                <ItemTemplate>
							                <%# Container.DataItem("EndTm")%>
						                </ItemTemplate>
						                <EditItemTemplate>													
							                <asp:TextBox id="EndTm" width=100% MaxLength="5"	Text='<%# trim(Container.DataItem("EndTm")) %>'  runat="server"/>			
						                </EditItemTemplate>
					                </asp:TemplateColumn>						
										
					                <asp:TemplateColumn HeaderText="Last Update" SortExpression="A.UpdateDate">
						                <ItemTemplate>
							                <%# objGlobal.GetLongDate(Container.DataItem("UpdateDate")) %>
						                </ItemTemplate>
						                <EditItemTemplate >
							                <asp:TextBox id="UpdateDate" Readonly=TRUE size=8 
								                Visible=False Text='<%# objGlobal.GetLongDate(Now()) %>' runat="server"/>
							                <asp:TextBox id="CreateDate" Visible=False
								                Text='<%# Container.DataItem("CreateDate") %>' runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Status" SortExpression="A.Status">
						                <ItemTemplate>
							                <%# objHR.mtdGetFunctionStatus(Container.DataItem("Status")) %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:DropDownList Visible=False id="StatusList" size=1 runat=server />
							                <asp:TextBox id="Status" Readonly=TRUE Visible = False
								                Text='<%# Container.DataItem("Status")%>' runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					
					                <asp:TemplateColumn HeaderText="Updated By" SortExpression="UserName">
						                <ItemTemplate>
							                <%# Container.DataItem("UserName") %>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:TextBox id="UserName" Readonly=TRUE size=8 
								                Text='<%# Session("SS_USERID") %>' Visible=False runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>					
					                <asp:TemplateColumn>					
						                <ItemTemplate>
							                <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" runat="server"/>
						                </ItemTemplate>
						                <EditItemTemplate>
							                <asp:LinkButton id="Update" CommandName="Update" Text="Save" runat="server"/>
							                <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" runat="server"/>
							                <asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False runat="server"/>
						                </EditItemTemplate>
					                </asp:TemplateColumn>
					                </Columns>
					                </asp:DataGrid><BR>
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
								</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td>
					            <asp:ImageButton id=btnNew OnClick="DEDR_Add" imageurl="../../images/butt_new.gif" AlternateText="New Job Code" runat="server"/>
					        	<asp:ImageButton id=ibPrint imageurl="../../images/butt_print.gif" AlternateText=Print visible=false runat="server"/>
							</td>
						</tr>
                        <tr>
                            <td>
 					            &nbsp;</td>
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



			 <input id="isNew" type="hidden" runat="server" />
			</Form>
		</body>
</html>
