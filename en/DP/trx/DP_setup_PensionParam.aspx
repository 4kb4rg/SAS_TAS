<%@ Page Language="vb"  %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<html>
	<head>
		<title>Employee Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<table border=0 cellspacing="1" width="100%" class="font9Tahoma">
				<tr>
					<td class="mt-h" colspan="5">PENSION PARAMETER</td>
				</tr>
				<tr>
					<td colspan=5><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Normal Pension Limit :</td>
					<td width=25%><asp:TextBox id=TextBox1 width=25% runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Status :</td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td height=25>Fasten Pension Limit :</td>
					<td><asp:TextBox id=TextBox2 width=25% runat=server /></td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td height=25>Child Pension Limit :</td>
					<td><asp:TextBox id=TextBox3 width=25% runat=server /></td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>Returning Contribution Limit :</td>
					<td><asp:TextBox id=TextBox4 width=25% runat=server /></td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
                <tr>
					<td height=25><b>BANK DETAIL</b></td>
				</tr>
				<tr>
                     <td height=25>Bank :</td>
                     <td><asp:TextBox id=txtFundBank width=100% runat=server /></td>
                </tr>
				<tr>
                    <td height=25>Bank Account No :</td>
                    <td><asp:TextBox id=txtFundBankAccNo width=100% runat=server /></td>
                </tr>
                <tr>
                    <td height=25>Bank Account Name  :</td>
                    <td><asp:TextBox id=txtFundBankAccName width=100% runat=server /></td>
                </tr>
                <tr>
					<td colspan=5>&nbsp;</td>
				</tr>
                <tr>
					<td height=25><b>AUTHORITY</b></td>
				</tr>
				<tr>
                     <td height=25>Name :</td>
                     <td><asp:TextBox id=TextBox5 width=100% runat=server /></td>
                </tr>
				<tr>
                    <td height=25>Position :</td>
                    <td><asp:TextBox id=TextBox6 width=100% runat=server /></td>
                </tr>
                <tr>
                     <td height=25>Name :</td>
                     <td><asp:TextBox id=TextBox7 width=100% runat=server /></td>
                </tr>
				<tr>
                    <td height=25>Position :</td>
                    <td><asp:TextBox id=TextBox8 width=100% runat=server /></td>
                </tr>
                <tr>
                     <td height=25>Name :</td>
                     <td><asp:TextBox id=TextBox9 width=100% runat=server /></td>
                </tr>
				<tr>
                    <td height=25>Position :</td>
                    <td><asp:TextBox id=TextBox10 width=100% runat=server /></td>
                </tr>
                <tr>
					<td colspan=5>&nbsp;</td>
				</tr>
                <tr>
					<td height=25><b>CONTRIBUTIONS</b></td>
				</tr>
                </table>


                <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
				<tr>
				    <td colspan="6">
					    <table id="tblSelection" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
					        <tr class="mb-c">
                                <td height=25 width=20%>Employee Contribution :</td>
                                <td colspan=4><asp:TextBox id=txtEmpeCon width=15% runat=server /> %
                                </td>
                            </tr>
                            <tr class="mb-c">	
                                <td height=25>Employer Contribution :</td>
                                <td colspan=4><asp:TextBox id=txtEmprCon width=15% runat=server /> %
                                </td>
                            </tr>
                            <tr class="mb-c">
                               <td height=25>Number :*</td>
                               <td colspan=4><asp:TextBox id=txtSKNo maxlength=64 width=90% runat=server />
                                    <asp:RequiredFieldValidator id=RequiredFieldValidator5 display=dynamic runat=server 
                                        ErrorMessage="Please enter Member Name." 
                                        ControlToValidate=txtSKNo />	
                               </td>	
                            </tr>
                            <tr class="mb-c">
                               <td height=25>Date :*</td>
                               <td colspan=4><asp:TextBox id=txtSKDate width=15% runat=server />
                                    <a href="javascript:PopCal('txtSKDate');"><asp:Image id="Image5" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
                                    <asp:Label id=Label7 visible=False forecolor=red text="<br>Date format should be in " runat=server />
                                    <asp:RequiredFieldValidator id=RequiredFieldValidator6 display=dynamic runat=server 
                                        ErrorMessage="<br>Please enter Date Of Birth." 
                                        ControlToValidate=txtSKDate />			
                                </td>
                            </tr>
                            <tr class="mb-c">
                                <td height=25>Notes :</td>
                                <td colspan=4><asp:TextBox id=txtNotes width=90% runat=server /></td>
                            </tr>
                            <tr class="mb-c">
                                <td height=25>File :</td>
                                <td colspan=4><asp:TextBox id=txtFile width=90% runat=server />
                                              <input type=button value=" ... " id="FindSpl" onclick="javascript:PopSupplier('frmMain', '', 'ddlSupplier', 'True');" CausesValidation=False runat=server />
                                              </td>
                            </tr>
                            <tr class="mb-c">											
                                <td colspan=5>
                                    <asp:ImageButton id=btnAdd imageurl="../../images/butt_add.gif" alternatetext=Add  runat=server />
                                </td>
                            </tr>
					    </table>
				    </td>		
			    </tr>
            </table>


        <table style="width: 100%" class="font9Tahoma">
                <tr>
                    <TD colspan = 2 >					
                    <asp:DataGrid id="DgSK"
                        AutoGenerateColumns="false" width="100%" runat="server"
                        GridLines = none
                        Cellpadding = "2"
                        Pagerstyle-Visible="False"
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
                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Employee" >
                        <ItemTemplate>
                            <asp:Label Text=<%# Container.DataItem("EmpeCon") %> id="lblEmpeCon" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="Employer" >
                        <ItemTemplate>
                            <asp:Label Text=<%# Container.DataItem("EmprCon") %> id="lblEmprCon" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>				
                    <asp:TemplateColumn ItemStyle-Width="25%" HeaderText="No." >
                        <ItemTemplate>
                            <asp:Label Text=<%# Container.DataItem("SKNo") %> id="lblSKNo" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>	
                    <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Date" >
                        <ItemTemplate>
                            <asp:Label Text=<%# Container.DataItem("SKDate") %> id="lblSKDate" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>	
                    <asp:TemplateColumn ItemStyle-Width="20%" HeaderText="Remark" >
                        <ItemTemplate>
                            <asp:Label Text=<%# Container.DataItem("Remark") %> id="lblRemark" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateColumn>	
                    <asp:TemplateColumn ItemStyle-Width="5%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
                        <ItemTemplate>
                            <asp:LinkButton id=lbDelete CommandName=Delete Text="Delete" runat=server CausesValidation=False />
                        </ItemTemplate>
                    </asp:TemplateColumn>	
                    </Columns>
                    </asp:DataGrid><BR>
                    </td>
                </tr>    
                
                <tr>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Remark :</td>
					<td><asp:TextBox id=txtRemark width=100% runat=server /></td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 colspan="2">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="Save"  runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" CausesValidation=False AlternateText="Delete"  runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="UnDelete"  runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="Back" runat=server />
					</td>
				</tr>
				<tr>
					<td height=25 colspan="2">
                                            &nbsp;</td>
				</tr>
				<tr>
					<td colspan=2>&nbsp;</td>
				</tr>
			</table>
			<input type=hidden id=EmpCode value='' runat=server />
			<input type=hidden id=hidEmpName value='' runat=server />
			<input type=hidden id=hidStatus value=0 runat=server/>
			<asp:Label id=lblRedirect visible=false runat=server/>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>    
	</body>
</html>
