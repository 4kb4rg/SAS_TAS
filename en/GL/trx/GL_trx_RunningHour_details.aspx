<%@ Page Language="vb" trace="False" src="../../../include/GL_Trx_RunningHour_Details.aspx.vb" Inherits="GL_RunningHour_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
 
	<head>
		<title>Actual Station Running Hour Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">	
		    function calTotal() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtRunFrom.value);
				var b = parseFloat(doc.txtRunTo.value);
				var dbAmt = b - a;
				if (doc.txtRunHour.value == 'NaN')
					doc.txtRunHour.value = '';
				else
					doc.txtRunHour.value = round(dbAmt, 2);
			}		
		</script>
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style3
            {
                height: 9px;
            }
        </style>
	</head>

	<body>
  		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
         <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />	
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id=lblPleaseSpecify visible=false text="Please specify " runat=server />		
		<asp:label id=lblID visible=false text=" ID" runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id="lblStsHid" Visible="False" Runat="server" />
		<asp:label id="issueType" Visible="False" Runat="server" />

		<table border=0 width="100%" cellspacing="0" cellpading="1" class="font9Tahoma">
        <tr>
        <td>
   
			<tr>
				<td colspan=6><UserControl:MenuGLTrx EnableViewState=False id=menuGL runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                             <strong>   ACTUAL STATION RUNNING HOUR DETAILS </strong></td>
                            <td class="font9Header"  style="text-align: right">
                                Status : <asp:Label id=Status runat=server />&nbsp;| Date Created : <asp:Label id=CreateDate runat=server />&nbsp;| Last Update : <asp:Label id=UpdateDate runat=server />&nbsp;| Updated By : <asp:Label id=UpdateBy runat=server />&nbsp;| <asp:Label id=lblPDateTag Text="Print Date :" visible=false runat=Server />&nbsp;<asp:Label id=lblPrintDate  visible=false runat=server />
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>				
			</tr>		

			<tr>
				<td width="15%" height=25>Actual Running Hour ID :</td>
				<td width="35%"><asp:label id=lblTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="25%">&nbsp;</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td>Transaction Date :*</td>
				<td><asp:TextBox id=txtDate Width=50% maxlength=10 runat=server CssClass="font9Tahoma"/>
					<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="Please specify document date" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/>
					<asp:label id=lblDate Text ="Date entered should be in the format " forecolor=red Visible=false Runat="server"/> 
					<asp:label id=lblFmt  forecolor=red Visible=false Runat="server"/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>			
			<tr>
				<td class="style3"></td>
				<td class="style3"></td>
				<td class="style3"></td>
				<td class="style3">&nbsp;</td>
				<td class="style3">&nbsp;</td>		
				<td class="style3"></td>
			</tr>			
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
             </td>
        </tr>
            </table>
        <table  border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
			<tr>
				<td colspan="3">
					<table id="tblAdd" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
						 
						<tr id="RowChargeLevel" class="mb-c">
							<td width="15%" height="25">Charge Level :* </td>
							<td width="35%"><asp:DropDownList id="ddlChargeLevel" Width=100% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged runat=server CssClass="font9Tahoma"/> </td>
						    <td width="5%">&nbsp;</td>
				            <td width="15%">&nbsp;</td>
				            <td width="25%">&nbsp;</td>
				            <td width="5%">&nbsp;</td>
						</tr>
						<tr id="RowBlock" class="mb-c">
							<td height="25"><asp:label id=lblBlockTag Runat="server"/> </td>
							<td><asp:DropDownList id="ddlBlock" Width=100% runat=server CssClass="font9Tahoma"/>
							    <asp:label id=lblBlockErr Visible=False forecolor=red Runat="server" /></td>
						    <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
						</tr>
						<tr id="RowSubBlock" class="mb-c">
							<td height="25"><asp:label id=lblSubBlockTag Runat="server"/> :</td>
							<td><asp:DropDownList id="ddlSubBlock" Width=100% runat=server CssClass="font9Tahoma" />
								<asp:label id="lblSubBlockErr" forecolor=red visible=false runat="server" /> </td>
							<td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
						</tr>
						<tr class="mb-c">
							<td height=25>Running Hour From :*</td>
							<td><asp:textbox id="txtRunFrom" Width=50% maxlength=21 Text="0" EnableViewState=False OnKeyUp="javascript:calTotal();"  Runat="server" CssClass="font9Tahoma"/>
								<asp:RequiredFieldValidator 
									id="RequiredFieldValidator1" 
									runat="server" 
									EnableClientScript="True"
									ControlToValidate="txtRunFrom" 
									display="dynamic"/>
								<asp:RegularExpressionValidator id="RegularExpressionValidator1" 
									ControlToValidate="txtRunFrom"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Only Number and 2 decimal points"
									runat="server"/>
							</td>
							<td>&nbsp;</td>
							<td>To :*</td>
							<td><asp:textbox id="txtRunTo" Width=50% maxlength=21 Text="0" EnableViewState=False OnKeyUp="javascript:calTotal();" Runat="server" CssClass="font9Tahoma"/>
								<asp:RequiredFieldValidator 
									id="RequiredFieldValidator2" 
									runat="server" 
									EnableClientScript="True"
									ControlToValidate="txtRunTo" 
									display="dynamic"/>
								<asp:RegularExpressionValidator id="RegularExpressionValidator2" 
									ControlToValidate="txtRunTo"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Only Number and 2 decimal points"
									runat="server"/>
							</td>
							<td>&nbsp;</td>
						</tr>
						<tr class="mb-c">
							<td height=25>Running Hour :*</td>
							<td><asp:textbox id="txtRunHour" Width=50% maxlength=21 Text="0" EnableViewState=False Enabled=false Runat="server" CssClass="font9Tahoma"/>
								<asp:RequiredFieldValidator 
									id="validateHour" 
									runat="server" 
									EnableClientScript="True"
									ControlToValidate="txtRunHour" 
									display="dynamic"/>
								<asp:RegularExpressionValidator id="RegularExpressionValidatorQty" 
									ControlToValidate="txtRunHour"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Only Number and 2 decimal points"
									runat="server"/>
							</td>
							<td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
						</tr>
						<tr class="mb-c">
							<td height=25 Colspan=2><asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" /></td>
							<td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
						</tr>
						<tr class="mb-c">
							<td height=25 colspan=2><asp:ImageButton text="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" />&nbsp;</td>
							<td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
						</tr>
					</table>
				</td>		
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=3><asp:label id=lblConfirmErr text="<BR>Document must contain transaction to Confirm!" Visible=False forecolor=red Runat="server" /></td>
			</tr>
			<tr>
				<td colspan="3"> 
					<asp:DataGrid id="dgStkTx"
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines = none
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						AllowSorting="True" CssClass="font9Tahoma">	
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
						<asp:TemplateColumn HeaderText="Transaction Date">
							<ItemStyle width=15%/>			
							<ItemTemplate>
								<asp:label text=<%# objGlobal.GetLongDate(Trim(Container.DataItem("TransactDate"))) %> id="TrxDate" runat="server" />							
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle width=15% />							
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("BlkCode") %> id="BlkCode" visible=true runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<ItemStyle width=20% />							
							<ItemTemplate>
								<asp:label text=<%# Container.DataItem("Description") %> id="Description" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Running Hour From">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle width=15% HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("RunHourFrom"),2) %> id="RunHourFrom" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>	
						<asp:TemplateColumn HeaderText="Running Hour To">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle width=15% HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("RunHourTo"),2) %> id="RunHourTo" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>	
						<asp:TemplateColumn HeaderText="Running Hour">
							<HeaderStyle HorizontalAlign="Right" />			
							<ItemStyle width=15% HorizontalAlign="Right" />						
							<ItemTemplate>
								<asp:label text=<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("RunHour"),2) %> id="RunHour" runat="server" />
							</ItemTemplate>							
						</asp:TemplateColumn>	
						<asp:TemplateColumn>		
							<ItemStyle width=5% HorizontalAlign="right" />							
							<ItemTemplate>
								<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server" />
								<asp:label text=<%# Container.DataItem("RunHourLnID") %> Visible=False id="lblID" runat="server" />
							</ItemTemplate>
						</asp:TemplateColumn>
						</Columns>										
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td height=25><hr size="1" noshade></td>
				<td width="5%">&nbsp;</td>					
			</tr>	
			<tr>
				<td>&nbsp;</td>
				<td colspan=2>
					<table border=0 width="100%" cellspacing="0" cellpadding="1" runat="server" class="font9Tahoma">
						<tr >		
							<td width=60% height=25>Total Running Hour : </td>
							<td width=20%>&nbsp;</td>
							<td><asp:label id="lblTotRunHour" runat=server /></td>				
							<td width=10%>&nbsp;</td>
						</tr>
					</table>
				</td>						
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
			</tr>
			<tr class="font9Tahoma">
				<td ColSpan="3">
					<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />	
					<asp:Label id=lblErrDupl visible=false forecolor=red text="Transaction on this date already exists." runat=server/>							
				</td>
			</tr>
			<tr class="font9Tahoma">
				<td align="left" colspan="3">
				    <asp:ImageButton id="btnNew"		onClick=btnNew_Click		ImageURL="../../images/butt_New.gif"	CausesValidation =False runat="server" visible=false />
					<asp:ImageButton id="Save"		onClick=btnSave_Click		ImageURL="../../images/butt_save.gif"	CausesValidation =False  runat="server" />
					<asp:ImageButton id="Cancel"	onClick=btnCancel_Click		ImageURL="../../images/butt_Cancel.gif" CausesValidation =False AlternateText="Cancel" Visible=False runat="server" />
					<asp:ImageButton id="Print"		onClick=btnPrint_Click		ImageURL="../../images/butt_print.gif"	CausesValidation =False runat="server" visible=false />
					<asp:ImageButton id="Back"		onClick=btnBack_Click		ImageURL="../../images/butt_back.gif"	CausesValidation =False runat="server" />
				</td>
			</tr>
			<tr class="font9Tahoma">
				<td align="left" colspan="3">
					&nbsp;</td>
			</tr>
		</table>
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=RunHourID runat=server />
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
