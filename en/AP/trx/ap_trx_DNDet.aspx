<%@ Page Language="vb" src="../../../include/ap_trx_DNDet.aspx.vb" Inherits="ap_trx_DNDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuAP" src="../../menu/menu_aptrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>Supplier Debit Note Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
            <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		    function lostFocusAmount() {
			    var doc = document.frmMain;
			    var x = doc.txtDebitAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtDebitAmount.value = toCurrency(round(dbAmt, 0));
	        }
			
			function toCurrency(num) {
              num = num.toString().replace(/\$|\,/g, '')
              if (isNaN(num)) num = "0";
              sign = (num == (num = Math.abs(num)));
              num = Math.floor(num * 100 + 0.50000000001);
              cents = num % 100;
              num = Math.floor(num / 100).toString();
              if (cents < 10) cents = '0' + cents;

              for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3))
              }

              return (((sign) ? '' : '-') + num + '.' + cents)
            }

			function lostFocusDPP() {
			    var doc = document.frmMain;
			    var x = doc.txtDPPAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtDPPAmount.value = toCurrency(round(dbAmt, 0));
	        }

			function calTaxPrice() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmount.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));		    
				var d = (a * (10/100));
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				var newnumberPPN = new Number(d+'').toFixed(parseInt(0));
				
				if (doc.hidTaxPPN.value == '0')
				    doc.txtDebitAmount.value = newnumber;
				else
				    doc.txtDebitAmount.value = newnumberPPN;
				    
				if (doc.txtDebitAmount.value == 'NaN')
					doc.txtDebitAmount.value = '';
				else
					doc.txtDebitAmount.value = toCurrency(doc.txtDebitAmount.value);
			}
		</script>
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                height: 25px;
                width: 17%;
            }
            .style3
            {
                width: 17%;
            }
            </style>
	</head>
	<body>
		<form id=frmMain  class="main-modul-bg-app-list-pu"  runat=server>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">  
		<table id="tblHeader" cellSpacing="0" cellPadding="2" width="100%" border="0" class="font9Tahoma">
			<tr>
				<td colspan="6"><UserControl:MenuAP id=MenuAP runat="server" /></td>
			</tr>
			<tr>
				<td class="mt-h" colspan="6">
                    <br />
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma" style="text-align: left">
                             <strong>  SUPPLIER DEBIT NOTE DETAILS</strong> </td>
                            <td class="font9Header" style="text-align: right">
                                Period : <asp:Label id=lblAccPeriod runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label ID=lblLastUpdate runat=server />&nbsp;| Updated By :&nbsp; <asp:Label ID=lblUpdatedBy runat=server />
                            </td>
                        </tr>
                    </table>
                </td>
			</tr>
			<tr>
				<td colspan=6>
                    <hr style="width :100%" />
                </td>
			</tr>
			<tr>
				<td class="style2">Debit Note ID :</td>
				<td width="40%" style="height: 25px"><asp:Label id=lblDebitNoteID runat=server /></td>
				<td width="5%" style="height: 25px">&nbsp;</td>
				<td width="15%" style="height: 25px">&nbsp;</td>
				<td width="20%" style="height: 25px">&nbsp;</td>
				<td style="height: 25px">&nbsp;</td>
			</tr>
			<tr>
				<td height=25 class="style3">Debit Note Ref. No. : </td>
				<td><asp:TextBox id=txtDNRefNo width=100% maxlength=20 runat=server />
				<asp:Label id=lblUnqErrRefNo visible=false text="Ref. No is not unique" forecolor=red runat=server/></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 class="style3">Debit Note Ref. Date : </td>
				<td><asp:TextBox id=txtDNRefDate width="32%" maxlength=10 CssClass="fontObject" runat=server />
					<a href="javascript:PopCal('txtDNRefDate');"><asp:Image id="btnSelDate" ImageAlign=absMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:Label id=lblErrTransDate forecolor=red text="Date format " runat=server />
					<asp:label id=lblFmtTransDate  forecolor=red Visible = false Runat="server"/> 
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 class="style3">Supplier Code :* </td>
				<td>
                    <asp:TextBox ID="txtSupCode" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="15" 
                        Width="32%"></asp:TextBox>
                    <input id="FindSpl" class="button-small" runat="server" causesvalidation="False" onclick="javascript:PopSupplier_New('frmMain','','txtSupCode','txtSupName','txtCreditTerm','txtPPN','txtPPNInit', 'False');"
                        type="button" value=" ... " />
                                            <asp:Button ID="btnSuppRef" runat="server"
                                                    Font-Bold="True" OnClick="GetSupplierBtn_Click" 
                        Text="Click Here" ToolTip="Click For Refresh COA "
                                                    Width="64px" CssClass="button-small"/>
                                            &nbsp;<asp:TextBox ID="txtSupName"
                                runat="server" BackColor="Transparent" BorderStyle="None" Font-Bold="True"  CssClass="fontObject"
                                MaxLength="10" Width="99%"></asp:TextBox><br />
					<asp:Label id=lblErrSupplier forecolor=red visible=true text="Please select Supplier Code" runat=server/>
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25 class="style3"><asp:Label id=lblInvoiceRcvRefNo runat=server /> : </td>
				<td><asp:DropDownList id=ddlInvoiceRcvRefNo width=100% CssClass="fontObject" runat=server /> </td>
				<td>&nbsp;</td>
				<td height=25>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
            </table>

            <table cellSpacing="0" cellPadding="4" width="100%" border="0" class="font9Tahoma" runat=server >
            <tr>
            <td>
           
			<tr>

				<td colSpan="6" vAlign="top">
					<!--<table width="100%" class="mb-c" cellspacing="0" cellpadding="4" border="0">
						<tr>						
							<td>-->
								<table id="tblSelection" cellSpacing="0" cellPadding="4" width="100%" border="0" runat=server class="sub-Add">
									<tr class="mb-c">
										<td height=25>Descrip: </td>
										<td><asp:Textbox id=txtDescription width=95% maxlength=64 CssClass="fontObject" runat=server /></td>
									</tr>
									<tr class="mb-c">
										<td width="20%" height=25><asp:label id=lblAccount runat=server /> (DR) :* </td>
										<td width="80%">
                                            <asp:TextBox ID="txtAccCode" CssClass="fontObject" runat="server" AutoPostBack="True" MaxLength="15" OnTextChanged="onSelect_StrAccCode"
                                                Width="20%"></asp:TextBox>
                                            <input id="btnFind1" runat="server" onclick="javascript:PopCOA_Desc('frmMain', '', 'txtAccCode', 'txtAccName', 'False');"
                                               class="button-small"  type="button" value=" ... " />
                                            <asp:Button ID="CoaChangeButton2" runat="server"
                                                    Font-Bold="True" OnClick="onSelect_Account" Text="Click Here" ToolTip="Click For Refresh COA "
                                                    Width="64px" CssClass="button-small"/>
                                            <asp:TextBox ID="txtAccName" runat="server" BackColor="Transparent" BorderStyle="None"
                                              CssClass="fontObject"   Font-Bold="True"  MaxLength="10" Width="62%"></asp:TextBox><br />
											<asp:Label id=lblErrDebitAccCode visible=false forecolor=red runat=server/>
										</td>
									</tr>
									<tr id="RowChargeLevel" class="mb-c">
										<td height="25">Charge Level :* </td>
										<td><asp:DropDownList id="ddlChargeLevel" Width=95% AutoPostBack=True OnSelectedIndexChanged=ddlChargeLevel_OnSelectedIndexChanged CssClass="fontObject" runat=server /> </td>
									</tr>
									<tr id="RowPreBlk" class="mb-c">
										<td height="25"><asp:label id=lblPreBlkTag Runat="server"/> </td>
										<td><asp:DropDownList id="ddlPreBlock" Width=95% runat=server />
											<asp:label id=lblPreBlockErr Visible=False forecolor=red Runat="server" /></td>
									</tr>
									<tr id="RowBlk" class="mb-c">
										<td height=25><asp:label id=lblBlock runat=server /> :</td>
										<td><asp:DropDownList id=ddlBlock width=95% CssClass="fontObject" runat=server/>
											<asp:Label id=lblErrBlock visible=false forecolor=red runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id=lblVehicle runat=server /> :</td>
										<td><asp:Dropdownlist id=ddlVehCode width=95% CssClass="fontObject" runat=server/>
											<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/></td>
									</tr>
									<tr class="mb-c">
										<td height=25><asp:label id=lblVehExpense runat=server /> :</td>
										<td><asp:Dropdownlist id=ddlVehExpCode width=95% CssClass="fontObject" runat=server/>
											<asp:Label id=lblErrVehicleExp visible=false forecolor=red runat=server/></td>
									</tr>
                                    <tr id="RowFP" visible=false class="mb-c">
							            <td height=25 style="width: 166px">Tax Invoice/Faktur Pajak No. :</td>
							            <td colspan=4><asp:TextBox id=txtFakturPjkNo Width=22% maxlength=19 CssClass="fontObject" runat=server />	
											            <asp:label id=lblErrFakturPjk text="Please enter tax invoice/faktur pajak no." Visible=False forecolor=red Runat="server" /></td>
						            </tr>
						            <tr id="RowFPDate" visible=false class="mb-c">
							            <td height=25 style="width: 166px">Tax Invoice Date/Tgl. Faktur Pajak :</td>
							            <td colspan=4><asp:TextBox id=txtFakturDate width=22% maxlength="10" CssClass="fontObject" runat="server"/>
							                        <a href="javascript:PopCal('txtFakturDate');"><asp:Image id="Image1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					                                <asp:label id=lblDateFaktur Text ="<br>Date Entered should be in the format " forecolor=red Visible = false Runat="server"/> 
					                                <asp:label id=lblFmtFaktur  forecolor=red Visible = false Runat="server"/></td>
						            </tr>
						            <tr id="RowTax" visible=false class="mb-c">
							            <td height=25 style="width: 166px">Tax Object :</td>
							            <td colspan=4><asp:DropDownList id="lstTaxObject" Width=95% AutoPostBack=True OnSelectedIndexChanged=lstTaxObject_OnSelectedIndexChanged CssClass="fontObject" runat=server />
									              <asp:label id=lblTaxObjectErr Visible=False forecolor=red Runat="server" />
						                </td>
						            </tr>
						            <tr id="RowTaxAmt" visible=false class="mb-c">
						                <td height=25>DPP Amount : </td>
						                <td><asp:Textbox id="txtDPPAmount" CssClass="fontObject" Font-Bold="true" style="text-align:right" Text=0 Width=20% maxlength=22 OnKeyUp="javascript:calTaxPrice();" OnBlur="javascript:lostFocusDPP();" runat=server />
								        </td>
						            </tr>
									<tr class="mb-c">
										<td height=25>Debit Amount :*</td>
										<td><asp:TextBox id=txtDebitAmount width=20% maxlength=22 style="text-align:right" text=0 OnBlur="javascript:lostFocusAmount();" CssClass="fontObject" Font-Bold="true" runat=server />
											<asp:Label id=errReqDTAmount visible=false forecolor=red text="Please enter debit amount." runat=server />
										</td>
									</tr>
									<tr class="mb-c">
										<td vAlign="top" height=25 colspan=2>
											<asp:ImageButton id=AddBtn imageurl="../../images/butt_add.gif" alternatetext=Add onclick=AddBtn_Click UseSubmitBehavior="false" runat=server /> 
										&nbsp;</td>
									</tr>
								</table>
							<!--</td>
						</tr>
					</table>-->
				</td>
			</tr>
			<tr>
				<td colSpan="6">
					<asp:DataGrid id=dgLineDet
						AutoGenerateColumns=false width="100%" runat=server
						GridLines=none
						Cellpadding=2
						OnDeleteCommand=DEDR_Delete 
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"
						Pagerstyle-Visible=False
                         OnItemDataBound="dgLine_BindGrid"
						AllowSorting="True" class="font9Tahoma">				
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>

						<Columns>						
							<asp:TemplateColumn HeaderText="Description" ItemStyle-Width="25%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescription" runat="server" /><br>
									<asp:label text=<%# Container.DataItem("SPLFaktur") %> id="lblSPLFaktur" Visible=false runat="server" />
									<asp:label text=<%# Container.DataItem("SPLFakturDate") %> id="lblSPLFakturDate" Visible=false runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>							
							<asp:TemplateColumn ItemStyle-Width="15%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("AccCode") %> id="lblAccCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>						
							<asp:TemplateColumn HeaderText="COA Descr" ItemStyle-Width="15%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("COADescr") %> id="lblCOADescr" runat="server" /><br>
									<asp:label text= '<%# Container.DataItem("TaxObject") %>' id="lblTaxObject" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>	
							<asp:TemplateColumn ItemStyle-Width="12%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Blkcode") %> id="lblBlkCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="12%">
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("VehCode") %> id="lblVehCode" runat="server" /><BR>
									<asp:Label Text=<%# Container.DataItem("Vehexpensecode") %> id="lblVehExpCode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Debit Amount" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Right ItemStyle-HorizontalAlign=Right>
								<ItemTemplate> 
									<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Amount"), 2), 0) %> id="lblViewAmount" runat="server" />
									<asp:Label Text=<%# FormatNumber(Container.DataItem("Amount"), 2,True,False,False) %> id="lblAmount" visible = False runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn ItemStyle-Width="5%" ItemStyle-HorizontalAlign=Right>
								<ItemTemplate>									
									<asp:label id=dnlnid text=<%# Container.DataItem("DebitNoteLNID")%> visible = False runat="server" />									
									<asp:LinkButton id=lbDelete CommandName=Delete Text=Delete runat=server />
									<asp:LinkButton id=lbEdit CommandName="Edit" Text="Edit" runat="server"/>
									<asp:LinkButton id=lbCancel CommandName="Cancel" Text="Cancel" runat="server"/>
									<asp:label text= '<%# Container.DataItem("TaxLnID") %>' Visible=False id="lblTaxLnID" runat="server" />
									<asp:label text= '<%# Container.DataItem("TaxRate") %>' Visible=False id="lblTaxRate" runat="server" />
									<asp:label text= '<%# Container.DataItem("DPPAmount") %>' Visible=False id="lblDPPAmount" runat="server" />
									
								</ItemTemplate>
							</asp:TemplateColumn>	
						</Columns>
					</asp:DataGrid>
				</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td colspan=2 height=25><hr style="width :100%" /></td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td height=25>Total Debit Amount : </td>
				<td Align=right><asp:Label ID=lblViewTotalDebitAmount Runat=server />&nbsp;</td>
				<td Align=right><asp:Label ID=lblTotalDebitAmount visible = False Runat=server />&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=3>&nbsp;</td>
				<td height=25>Outstanding Payment Amount : </td>
				<td Align=right><asp:Label ID=lblViewOutPayAmount Runat=server />&nbsp;</td>
				<td Align=right><asp:Label ID=lblOutPayAmount visible = False Runat=server />&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Remarks : </td>
				<td colSpan="5"><asp:TextBox ID=txtRemark maxlength=256 width=100% Runat=server /></td>
			</tr>
			<tr>
				<td colSpan="6">&nbsp;</td>
			</tr>			
			<tr>
				<td colSpan="6">
						        <asp:ImageButton id=NewDNBtn UseSubmitBehavior="false" onClick=NewDNBtn_Click imageurl="../../images/butt_new.gif" AlternateText="New Debit Note" runat=server/>
					<asp:ImageButton ID=SaveBtn UseSubmitBehavior="false" onclick=SaveBtn_Click ImageUrl="../../images/butt_save.gif" AlternateText=Save Runat=server /> 
					<asp:ImageButton ID=RefreshBtn UseSubmitBehavior="false" CausesValidation=False onclick=RefreshBtn_Click ImageUrl="../../images/butt_refresh.gif" AlternateText=Refresh Runat=server />
					<asp:ImageButton ID=ConfirmBtn UseSubmitBehavior="false" onclick=ConfirmBtn_Click ImageUrl="../../images/butt_confirm.gif" AlternateText=Confirm Runat=server />
					<asp:ImageButton ID=PrintBtn UseSubmitBehavior="false" ImageUrl="../../images/butt_print.gif" AlternateText=Print visible=false Runat=server />
					<asp:ImageButton ID=CancelBtn UseSubmitBehavior="false" onclick=CancelBtn_Click ImageUrl="../../images/butt_cancel.gif" AlternateText=Cancel Runat=server />
					<asp:ImageButton ID=DeleteBtn UseSubmitBehavior="false" onclick=DeleteBtn_Click CausesValidation=false ImageUrl="../../images/butt_delete.gif" AlternateText=Delete Runat=server />
					<asp:ImageButton ID=UnDeleteBtn UseSubmitBehavior="false" onclick=UnDeleteBtn_Click ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete Runat=server />
					<asp:ImageButton ID=BackBtn UseSubmitBehavior="false" CausesValidation=False onclick=BackBtn_Click ImageUrl="../../images/butt_back.gif" AlternateText=Back Runat=server />
					<Input type=hidden id=dbnid value="" runat=server />
				</td>
			</tr>

			<tr id=TrLink runat=server>
				<td colspan=5>
					<asp:LinkButton id=lbViewJournal text="View Journal Predictions" causesvalidation=false runat=server /> 
				</td>
			</tr>
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=6>
					<asp:DataGrid id=dgViewJournal
						AutoGenerateColumns="false" width="100%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						AllowSorting="false" class="font9Tahoma">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
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
							<asp:TemplateColumn HeaderText="COA Code">
							    <ItemStyle Width="20%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("ActCode") %> id="lblCOACode" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Description">
							     <ItemStyle Width="40%" /> 
								<ItemTemplate>
									<asp:Label Text=<%# Container.DataItem("Description") %> id="lblCOADescr" runat="server" />
								</ItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Debet">
							    <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
							    <ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountDB"), 2), 2) %> id="lblAmountDB" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>									
						    <asp:TemplateColumn HeaderText="Credit">
						        <HeaderStyle HorizontalAlign="Right" /> 
								<ItemStyle HorizontalAlign="Right" Width="20%" /> 
								<ItemTemplate>
								    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("AmountCR"), 2), 2) %> id="lblAmountCR" runat="server" />
							    </ItemTemplate>
						    </asp:TemplateColumn>		
						    <asp:TemplateColumn>		
								<ItemStyle HorizontalAlign="Right" /> 									
								<ItemTemplate>
									
								</ItemTemplate>
							</asp:TemplateColumn>							
						</Columns>
					</asp:DataGrid>

                     <br />

                     <telerik:RadInputManager  ID="RadInputManager1" runat="server">
                             <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type" KeepTrailingZerosOnFocus="False">
                                <TargetControls>
                                    <telerik:TargetInput ControlID="txtDPPAmount" />
                                </TargetControls>
                            </telerik:NumericTextBoxSetting>  
                    </telerik:RadInputManager>


                     <telerik:RadInputManager  ID="RadInputManager2" runat="server">
                             <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type" KeepTrailingZerosOnFocus="False">
                                <TargetControls>
                                    <telerik:TargetInput ControlID="txtDebitAmount" />
                                </TargetControls>
                            </telerik:NumericTextBoxSetting>  
                    </telerik:RadInputManager>

				</td>
			</tr>	
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
			    <td>&nbsp;</td>								
			    <td height=25 align=right><asp:Label id=lblTotalViewJournal Visible=false runat=server /> </td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td>&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>
		    
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server /><asp:label id=lblDebit visible=false text="Debit " runat=server /><asp:label id=lblCode visible=false text=" Code" runat=server /><asp:label id=lblPleaseSelectOneDebit visible=false text="Please select one debit " runat=server /><asp:label id=lblPleaseSelect visible=false text="Please select " runat=server /><asp:Label id=lblStatusHidden visible=false runat=server /><asp:Label id=lblVehicleOption visible=false text=false runat=server/><Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
			<Input type=hidden id=hidNPWPNo value="" runat=server />
			<Input type=hidden id=hidTaxObjectRate value=0 runat=server />
			<Input type=hidden id=hidCOATax value=0 runat=server />
			<Input type=hidden id=hidTaxStatus value=1 runat=server />
			<Input type=hidden id=hidHadCOATax value=0 runat=server />
			<Input type=hidden id=hidFFBSpl value="0" runat=server />
			<Input type=hidden id=hidSplCode value="" runat=server />
			<Input type=hidden id=hidTaxPPN value="0" runat=server />
            
			<asp:label id=lblTxLnID visible=false runat=server />
                         </td>
                        </tr>
		</table>
            <asp:TextBox ID="txtPPN" runat="server" BackColor="Transparent" ForeColor="Transparent" BorderStyle="None"
                Width="9%"></asp:TextBox><asp:TextBox ID="txtCreditTerm" runat="server" ForeColor="Transparent" BackColor="Transparent"
                    BorderStyle="None" Width="9%"></asp:TextBox><asp:TextBox ID="txtPPNInit" runat="server"
                        BackColor="Transparent" ForeColor="Transparent" BorderStyle="None" Width="9%"></asp:TextBox>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
