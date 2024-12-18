<%@ Page Language="vb" trace="False" src="../../../include/GL_Trx_VehicleUsage_Details.aspx.vb" Inherits="GL_VehUse_Det" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Vehicle Issue Details</title>		
		<Preference:PrefHdl id=PrefHdl runat="server" />
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">	
		 function callVehActCode() {
				var doc = document.frmMain;
				var arrVehActCode;
				
				if (doc.lstAccCode.selectedIndex > 0) {
				    arrVehActCode = doc.lstAccCode.options[doc.lstAccCode.selectedIndex].text.split(" - ");
				    doc.hidVehActCode.value = arrVehActCode[0].replace(" ","");
				}
		}
		function calTotal() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtRunFrom.value);
				var b = parseFloat(doc.txtRunTo.value);
				var dbAmt = b - a;
				if (doc.txtUnit.value == 'NaN')
					doc.txtUnit.value = '';
				else
					doc.txtUnit.value = round(dbAmt, 2);
		}	
		
	</script>
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
          <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 700px" valign="top">
			    <div class="kontenlist"> 

		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />	
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id=lblPleaseSpecify visible=false text="Please specify " runat=server />		
		<asp:label id=lblID visible=false text=" ID" runat=server />
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id="lblStsHid" Visible="False" Runat="server" />
		<asp:label id="issueType" Visible="False" Runat="server" />
		<table border=0 width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
			<tr>
				<td colspan=6><UserControl:MenuGLTrx EnableViewState=False id=menuGL runat="server" /><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server /></td>
			</tr>			
			<tr>
				<td   colspan=6>
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                               <strong><asp:label id="lblTitle" runat="server" /> DETAILS </strong> </td>
                            <td class="font9Header"  style="text-align: right">
                                Period : <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=Status runat=server />&nbsp;| Date Created : <asp:Label id=CreateDate runat=server />&nbsp;| Last Update : <asp:Label id=UpdateDate runat=server />&nbsp;| Updated By : <asp:Label id=UpdateBy runat=server />
                            </td>
                        </tr>
                    </table>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>
                    &nbsp;</td>				
			</tr>		
			<tr>
				<td width="15%" height=25><asp:label id=lblVehUsageID Runat="server"/> :</td>
				<td width="35%"><asp:label id=lblTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">&nbsp;</td>
				<td width="25%">&nbsp;</td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25><asp:label id="lblVehTag" Runat="server"/> :*</td>
				<td><asp:DropDownList id="lstVehCode" Width=100% AutoPostBack=True OnSelectedIndexChanged=lstVehCode_OnSelectedIndexChanged runat=server CssClass="font9Tahoma"/>
					<asp:label id=lblVehCodeErr Visible=False forecolor=red Runat="server" /></td>
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
			<tr>
				<td style="height: 25px">&nbsp;<asp:label id="lblAccCodeTag" Runat="server" Visible="False"/></td>
				<td style="height: 25px"><asp:RequiredFieldValidator 
									id="validateAccCode" 
									runat="server" 
									EnableClientScript="True"
									ControlToValidate="lstAccCode" 
									display="dynamic"/>
                    <asp:RequiredFieldValidator 
									id="lblVehLnId" 
									runat="server" 
									EnableClientScript="True"
									ControlToValidate="lstAccCode" 
									display="dynamic" Visible="False"/></td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>			
				<td style="height: 25px">&nbsp;</td>
			</tr>
            </table>
            <table   border="0" width="100%" cellspacing="0" cellpadding="4" runat="server" class="font9Tahoma">
            <tr>
            <td>

			<tr>
				<td colspan="6" style="height: 400px">
					<table id="tblAdd" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server" class="sub-add">
						<tr class="mb-c">
							<td width="15%">Transaction Date :*</td>
							<td width="35%"><asp:TextBox id=txtDate Width="32%" maxlength=10 runat=server CssClass="font9Tahoma" />
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
							<td width="5%">&nbsp;</td>
				            <td width="15%">&nbsp;</td>
				            <td width="25%">&nbsp;<asp:DropDownList id="ddlType" Width="29%" AutoPostBack=True OnSelectedIndexChanged=ddlType_OnSelectedIndexChanged runat=server Visible="False" CssClass="font9Tahoma">
								        <asp:ListItem value="TM">TM</asp:ListItem>
										<asp:ListItem value="TBM">TBM</asp:ListItem>										
										<asp:ListItem value="LC">LC</asp:ListItem>
										<asp:ListItem value="BBT">BIBITAN</asp:ListItem>
										<asp:ListItem value="ADM" Selected = "True" >ADM</asp:ListItem>
										<asp:ListItem value="LLN">LAIN-LAIN</asp:ListItem>
							</asp:DropDownList> <asp:DropDownList id="lstAccCode" Width="50%" AutoPostBack=True OnSelectedIndexChanged=CallCheckVehicleUse onchange="javascript:callVehActCode();" runat=server Visible="False" /></td>
				            <td width="5%">&nbsp;</td>
						</tr>
						
						<tr id="RowAccCode2" class="mb-c">
							<td style="height: 32px">Kode Akun :* </td>
                            <td colspan="3" style="height: 32px">
                                <asp:TextBox ID="txtAccCode" runat="server" AutoPostBack="True" MaxLength="15" Width="20%" CssClass="font9Tahoma"></asp:TextBox>
                                <asp:TextBox ID="txtAccName" runat="server" Font-Bold="True" ForeColor="Black" MaxLength="10" Width="68%" Wrap="False" CssClass="font9Tahoma"></asp:TextBox>
                                <input
                                    id="FindAcc" runat="server" causesvalidation="False" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                                    type="button" value=" ... " /></td>
				            <td style="height: 32px"></td>
				            <td style="height: 32px">&nbsp;</td>							
						</tr>
                        <tr class="mb-c">
                            <td height="25">
                                Additional Note :</td>
                            <td colspan="3">
                                <textarea rows=6 id=txtAddNote runat=server style="width: 560px"></textarea></td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr class="mb-c">
                            <td style="height: 39px">
                                Charge Level :*</td>
                            <td style="height: 39px">
                                <GG:AutoCompleteDropDownList id="lstChargeLevel" Width="82%" AutoPostBack=True OnSelectedIndexChanged=lstChargeLevel_OnSelectedIndexChanged runat=server CssClass="font9Tahoma"/><asp:label id=lblChargeMsg Text ="Please Select Charge Level" forecolor=Red Visible=False Runat="server"/></td>
                            <td style="height: 39px">
                            </td>
                            <td style="height: 39px">
                            </td>
                            <td style="height: 39px">
                            </td>
                            <td style="height: 39px">
                            </td>
                        </tr>
                        <tr class="mb-c">
                            <td height="25">
                            <asp:label id=lblPreBlkTag Runat="server"/></td>
                            <td>
                            <asp:DropDownList id="lstPreBlock" Width="82%"  runat=server  CssClass="font9Tahoma" /><br />
							<asp:label id=lblPreBlockErr text="Please select one Block Code" Visible=False forecolor=Red Runat="server" /></td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
					
						<tr class="mb-c">
							<td height=25>Running Hour From :*</td>
							<td><asp:textbox id="txtRunFrom" Width=35% maxlength=21 Text="0" EnableViewState=False OnKeyUp="javascript:calTotal();"  Runat="server" CssClass="font9Tahoma"/>
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
									text = "Only Number and 2 decimal points"
									runat="server"/>
									&nbsp;&nbsp;To :*
									&nbsp;
									<asp:textbox id="txtRunTo" Width=35% maxlength=21 Text="0" EnableViewState=False OnKeyUp="javascript:calTotal();" Runat="server" CssClass="font9Tahoma"/>
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
									text = "Only Number and 2 decimal points"
									runat="server"/>
                                <br />
                                <asp:label id=lblRunErr Visible=False forecolor=Red Runat="server" /></td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
							<td>&nbsp;</td>
						</tr>
						<tr class="mb-c">
							<td><asp:label id="lblUsageUnit" Runat="server" /> :*</td>
							<td><asp:textbox id="txtUnit" Width=35% maxlength=15 EnableViewState=False Runat="server" CssClass="font9Tahoma"/>
								<asp:RequiredFieldValidator 
									id="validateUnit" 
									runat="server" 
									EnableClientScript="True"
									ControlToValidate="txtUnit" 
									display="dynamic"/>
								<asp:RegularExpressionValidator id="RegularExpressionValidatorQty" 
									ControlToValidate="txtUnit"
									ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
									Display="Dynamic"
									text = "Maximum length 9 digits and 5 decimal points"
									runat="server"/>
								<asp:RangeValidator id="RangeMeter"
									ControlToValidate="txtUnit"
									MinimumValue="0"
									MaximumValue="999999999.99999"
									Type="double"
									EnableClientScript="True"
									Text="The value must be from 0"
									runat="server" display="dynamic"/></td>
							<td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
						</tr>
						<tr class="mb-c">
							<td Colspan=2><asp:label id=lblerror text="Number generated is too big!" Visible=False forecolor=red Runat="server" /></td>
							<td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
				            <td>&nbsp;</td>
						</tr>
						<tr class="mb-c">
                            <td colspan="2" style="height: 32px">
                                <asp:ImageButton text="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" Runat="server" />&nbsp;<asp:ImageButton
                                    ID="btnUpdate" runat="server" ImageUrl="../../images/butt_save.gif" OnClick="btnUpdate_Click"
                                    text="Save" Visible="false" />&nbsp;&nbsp; 
					        &nbsp;<asp:label id=lblConfirmErr text="Document must contain transaction to Confirm!" Visible=False forecolor=Red Runat="server" /></td>
	                        <td style="height: 32px">&nbsp;</td>
				            <td style="height: 32px">&nbsp;</td>
				            <td style="height: 32px">&nbsp;</td>
				            <td style="height: 32px">&nbsp;</td>
						</tr>
					</table>
                    <br />
                    <div id="divgd" style="width:100%;height:400px;overflow: auto;">
                    <asp:DataGrid ID="dgStkTx" runat="server" AllowSorting="True"                 
                            AutoGenerateColumns="False"
                            CellPadding="2" GridLines="None" 
                            OnDeleteCommand="DEDR_Delete"
                            OnEditCommand="DEDR_Edit"
                            OnCancelCommand="DEDR_Cancel"
                            OnItemCreated="DataGrid_ItemCreated"										
                            PagerStyle-Visible="False" Width="100%" class="font9Tahoma">	
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
                            <asp:TemplateColumn HeaderText="LNID" Visible="False">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="LNID" runat="server" Text='<%# Container.DataItem("VehUsageLnID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Transaction Date">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>

                                    <asp:Label ID="lblTransDate" runat="server" Text='<%# objGlobal.GetLongDate(Trim(Container.DataItem("TransactDate"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Charge To" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblChargeLocCode" runat="server" Text='<%# Container.DataItem("ChargeLocCode") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="12%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COA Code">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAccCode" runat="server" Text='<%# Container.DataItem("AccCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Description">
                                <ItemStyle Width="25%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAccDesc" runat="server" Text='<%# Container.DataItem("AccDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Additional Note">
                                 <ItemStyle Width="25%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAddNote" runat="server" Text='<%# Container.DataItem("AdditionalNote") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Block/Machine">
                                <ItemStyle Width="15%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBlock" runat="server" Text='<%# Container.DataItem("BlkCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Running Hour From">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="RunHourFrom" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("RunHourFrom"),2) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Running Hour To">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="RunHourTo" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("RunHourTo"),2) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn  headertext="Vehicle Usage/Run Unit" >
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Width="15%"/>
                                <ItemTemplate>
                                    <asp:Label ID="UsageUnit" runat="server" Text='<%# ObjGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("UsageUnit"),5) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="5%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
							        <asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server" />																					
                                    <asp:LinkButton ID="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" Visible="false"></asp:LinkButton><br />
                                    <asp:Label ID="lblID" runat="server" Text='<%# Container.DataItem("VehUsageLnID") %>'
                                        Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Visible="False" />
                    </asp:DataGrid>
                    </div>
                    </td>		
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr size="1" noshade style="width : 100%"></td>
				<td width="5%">&nbsp;</td>					
			</tr>	
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=3>
					<table border=0 width="100%" cellspacing="0" cellpadding="1" runat="server" id="Table1" class="font9Tahoma">
						<tr>		
							<td width=60% height=25><asp:label id="lblTotalAmt" runat=server /> :</td>
							<td width=30% align="right"><asp:label id="lblTotAmtFig" runat="server" /></td>						
							<td width=10%>&nbsp;</td>
						</tr>
					</table>
				</td>						
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td ColSpan="6"><asp:label id=lblBPErr text="Unable to add record, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
								<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
				</td>
			</tr>
			<tr>
				<td align="left" colspan="6">
				    <asp:ImageButton id="btnNew"	UseSubmitBehavior="false" onClick=btnNew_Click		ImageURL="../../images/butt_New.gif"	CausesValidation =False runat="server" visible=false />
					<asp:ImageButton id="Save"		UseSubmitBehavior="false" onClick=btnSave_Click		ImageURL="../../images/butt_save.gif"	CausesValidation =False  runat="server" />
					<asp:ImageButton id="Cancel"	UseSubmitBehavior="false" onClick=btnCancel_Click		ImageURL="../../images/butt_Cancel.gif" CausesValidation =False AlternateText="Cancel" Visible=False runat="server" />
					<asp:ImageButton id="Print"		UseSubmitBehavior="false" onClick=btnPrint_Click		ImageURL="../../images/butt_print.gif"	CausesValidation =False runat="server"  />
					<asp:ImageButton id="Back"		UseSubmitBehavior="false" onClick=btnBack_Click		ImageURL="../../images/butt_back.gif"	CausesValidation =False runat="server" />
				    <br />
				</td>
			</tr>
		</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidVehActCode value="" runat=server/>
			<Input type=hidden id=hidAccCode value="" runat=server/>
            </div>
            </td>
            </tr>
                        </td>
            </tr>
            </table>
		</form>
	</body>
</html>
