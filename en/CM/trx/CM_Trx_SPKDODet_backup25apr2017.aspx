<%@ Page Language="vb" src="../../../include/CM_Trx_SPKDODet.aspx.vb" Inherits="CM_Trx_SPKDODet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_CM_Trx" src="../../menu/menu_CMTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>SPK DO Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function calRemAmount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDOQty.value);
				var b = parseFloat(doc.txtRemContractQty1.value);
	
				doc.txtRemContractQty.value = b - a;

				if (doc.txtDOQty.value == 'NaN'){
					doc.txtDOQty.value = '0';
				}
				if (doc.txtRemContractQty.value == 'NaN'){
					doc.txtRemContractQty.value = '0';					
				}
				if (doc.txtRemContractQty.value < 2){
					doc.txtRemContractQty.value = '0';					
				}
			}
		</script>
	</head>
	<body>
		<Form id=frmMain runat="server">
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100%>
				<tr>
					<td colspan="6"><UserControl:menu_CM_Trx id=menu_CM_Trx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">DO REGISTRATION DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>DO No :*</td>
					<td width=30%>
						<asp:textbox id=lblDONo maxlength=128 width=100% runat=server/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>DO Destination : </td>
					<td><asp:RadioButtonList id=rdDODest repeatdirection=horizontal onSelectedIndexChanged=onChange_DODestination autopostback=true runat=server>
							<asp:ListItem id=item1 value=1 text="Customer" Selected=True runat=server />
							<asp:ListItem id=item2 Value=2 text="Bulking" runat=server />
						</asp:RadioButtonList>
					</td>
				</tr>
				
				<tr>
					<td height=25>DO Date :*</td>
					<td><asp:Textbox id=txtDODate maxlength=128 width=25% runat=server/>
						<a href="javascript:PopCal('txtDODate');">
						<asp:Image id="btnDODate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator 
							id=rfvDODate
							display=dynamic 
							runat=server
							ControlToValidate=txtDODate
							text="<br>Please enter DO Date." />
						<asp:Label id=lblDODate text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblDODateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>DO Quantity : </td>
					<td><asp:Textbox id=txtDOQty maxlength=128 style="text-align:right" width=25% OnKeyUp="javascript:calRemAmount();"  runat=server/>
						<asp:RegularExpressionValidator id="revDOQty" 
							ControlToValidate="txtDOQty"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvDOQty"
							ControlToValidate="txtDOQty"
							MinimumValue="-999999"
							MaximumValue="9999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
						<asp:label id=lblErrDOQty visible=false forecolor=red text="<br>Total of DO Quantity." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>							

				<tr>
					<td height=25><asp:CheckBox ID="chktitipolah" Text=" Titip Olah" runat="server" OnCheckedChanged="chktitipolah_changed" AutoPostBack="true"/></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Contract No :*</td>
					<td><asp:dropdownlist id=ddlContNo width=100% autopostback=true onselectedindexchanged=onChanged_ContractNo runat=server/>
						<asp:label id=lblErrContNo visible=false forecolor=red text="<br>Please Select Contract No." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Customer Code :*</td>
					<td><asp:dropdownlist id=ddlBillParti width=100% runat=server/>
						<asp:label id=lblErrBillParti visible=false forecolor=red text="<br>Please Select Customer." runat=server/>
					</td>
				</tr>
				
				<tr>
					<td height=25>Term of Delivery :*</td>
					<td><asp:dropdownlist id=ddlTerm width=100% runat=server/></td>
				</tr>

				<tr>
					<td height=25>Contract Quantity : </td>
					<td><asp:Textbox id=txtContractQty maxlength=128 width=25% style="text-align:right" runat=server/>
						<asp:RegularExpressionValidator id="revContractQty" 
							ControlToValidate="txtContractQty"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvContractQty"
							ControlToValidate="txtContractQty"
							MinimumValue="0"
							MaximumValue="9999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
						<asp:label id=lblErrContractQty visible=false forecolor=red text="<br>Total of Contract Quantity." runat=server/>
					</td>
				</tr>
				
				<tr>
					<td height=25>Remaining Contract Quantity : </td>
					<td><asp:Textbox id=txtRemContractQty maxlength=128 width=25% style="text-align:right" runat=server/>
						<asp:RegularExpressionValidator id="revRemContractQty" 
							ControlToValidate="txtRemContractQty"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 2 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvRemContractQty"
							ControlToValidate="txtRemContractQty"
							MinimumValue="0"
							MaximumValue="9999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
						<asp:label id=lblErrRemQty visible=false forecolor=red text="<br>Total of Remaining Contract Quantity." runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Product Spesification : </td>
					<td><textarea rows=5 id=taProductSpesification cols=52 runat=server></textarea>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Quality Note : </td>
					<td><textarea rows=3 id=taProductQuality cols=52 runat=server></textarea>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Quantity Note: </td>
					<td><textarea rows=3 id=taProductQuantity cols=52 runat=server></textarea>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Transporter Code :*</td>
					<td>
					    <asp:dropdownlist id=ddlTransporter width=100% runat=server/>
					    <asp:label id=lblErrddlTransporter visible=false forecolor=red text="<br>Please select transporter." runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Ongkos Angkut :*</td>
					<td>
					    <asp:dropdownlist id=ddlOAngkut width=100% runat=server autopostback=true onselectedindexchanged=onChanged_ddlOAngkut runat=server/>
					    <asp:label id=lblErrddlOAngkut visible=false forecolor=red text="<br>Please select cost transporter." runat=server/>					   
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>
						Harga : <asp:Textbox id=txtOangkutharga width=30% enabled=False runat=server/> &nbsp;	PPN : <asp:Textbox id=OangkuthargaPPN width=30% enabled=False runat=server/>
					</td>
				</tr>
				
				<tr>
					<td height=25></td>
					<td><asp:DataGrid id=dgAPNote
						AutoGenerateColumns="false" width="30%" runat="server"
						GridLines=none
						Cellpadding="1"
						Pagerstyle-Visible="False"
						OnItemCommand=EmpLink_Click
						AllowSorting="false">	
						<HeaderStyle CssClass="mr-h"/>
						<ItemStyle CssClass="mr-l"/>
						<AlternatingItemStyle CssClass="mr-r"/>
						<Columns>
						<asp:TemplateColumn HeaderText="NO:PO-SPK">
									<ItemTemplate>
									<asp:LinkButton id=lnkEmpCode CommandName=Item text='<%# Container.DataItem("Poid") %>'  runat=server />
									<asp:Label id=lblEmpCode text='<%# Container.DataItem("POid") %>'  visible=False runat=server />
									</ItemTemplate>
								</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid> 
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>
					</td>
				</tr>
				
				<tr>
					<td height=25>Loading Destination : </td>
					<td><textarea rows=3 id=taLoadDest cols=52 runat=server></textarea>
					</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td width=20% height=25>Packaging :</td>
					<td width=30%>
						<asp:textbox id=txtPackaging maxlength=128 width=100% runat=server/>
					</td>
				</tr>
				<tr>
					<td height=25>Delivery Note : </td>
					<td><textarea rows=9 id=taDeliveryNote cols=52 runat=server></textarea>
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Shipping Name : </td>
					<td><asp:textbox id=txtShipName width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
				</tr>	
				
				<tr>
					<td height=25>Estimation Arrival Date :</td>
					<td><asp:Textbox id=txtEstimationDate maxlength=128 width=25% runat=server/>
						<a href="javascript:PopCal('txtEstimationDate');">
						<asp:Image id="btnEstDate" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						
						<asp:Label id=lblEstDate text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblEstDateFmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Expired Date From :</td>
					<td><asp:Textbox id=txtExpiredDate1 maxlength=128 width=25% runat=server/>
						<a href="javascript:PopCal('txtExpiredDate1');">
						<asp:Image id="btnExpDate1" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						
						<asp:Label id=lblExpDate1 text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblExpDate1Fmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
				</tr>
				<tr>	
					<td height=25>Expired Date To :</td>
					<td><asp:Textbox id=txtExpiredDate2 maxlength=128 width=25% runat=server/>
						<a href="javascript:PopCal('txtExpiredDate2');">
						<asp:Image id="btnExpDate2" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						
						<asp:Label id=lblExpDate2 text="<br>Date entered should be in " display=dynamic forecolor=red visible=false runat="server"/> 
						<asp:Label id=lblExpDate2Fmt display=dynamic forecolor=red visible=false runat="server"/> 
					</td>
					
				</tr>
				<tr>
					<td height=25><asp:label id=lblProduct visible=false runat=server /></td>
					<td><asp:dropdownlist id=ddlProduct width=50% visible=false runat=server/>
						<asp:label id=lblErrProduct visible=false forecolor=red text="<br>Please Select Product." runat=server/>
					</td>
				</tr>
				<tr>
					<td width=20% height=25>Qty Matched :</td>
					<td width=30%>
						<asp:textbox id=txtMatched maxlength=128 width=25% style="text-align:right" runat=server/>
					</td>
				</tr>
				
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>NPWP : </td>
					<td><asp:textbox id=txtNPWP width=50% runat=server/>
					</td>
					<td>&nbsp;</td>					
				</tr>				
				
				<tr>
					<td height=25 valign=top>Address :</td>
					<td valign=top>
						<textarea rows="4" id=txtAddress cols="52" runat=server></textarea>
						<asp:Label id=lblErrAddress visible=false forecolor=red text="Maximum length for address is up to 512 characters only." runat=server />
					</td>
					<td colspan=3>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<asp:label id=lblErrQty visible=false forecolor=red text="Total of DO Quantity is Greater Than Remaining Quantity, Please less the DO Quantity !" runat=server/>
						<asp:label id=lblMsgUpdatingDO visible=false forecolor=red text="Updating DO Success !" runat=server/>
						<asp:label id=lblMsgGenerateInv visible=false forecolor=red text="Generate Invoice Success !" runat=server/>
						<asp:label id=lblMsgGenCheck visible=false forecolor=red text="Please run updating DO first!" runat=server/>
						<asp:label id=lblMsgWMCheck visible=false forecolor=red text="Please fill Weigh Bridge Ticket Data for this DO No!" runat=server/>
						<asp:label id=lblMsgGenQtyDOCheck visible=false forecolor=red text="Generate invoice cannot be done because the Quantity is less than DO Quantity tolerance!" runat=server/>
						<asp:label id=lblMsgQtyDOCheck visible=false forecolor=red text="Closing DO cannot be done because the Quantity is less than DO Quantity tolerance!" runat=server/>
						<asp:label id=lblMsgMatchDOCheck visible=false forecolor=red text="Closing DO cannot be done because the Quantity is less than DO Quantity tolerance!" runat=server/>
						<asp:label id=lblMsgCloseDO visible=false forecolor=red text="Closing DO Success !" runat=server/>
						<asp:label id=lblProductCat visible=false forecolor=red text="" runat=server/>
						<asp:label id=lblProductFlag visible=false forecolor=red text="" runat=server/>
						<asp:label id=lblQtyMatched visible=false text=0 runat=server />
						<asp:label id=txtQtyMatched visible=false text=0 runat=server />
					</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
                    <td colspan="3">
                    <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
                </tr>
                <tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=NewTBBtn OnClick="NewTBBtn_Click" imageurl="../../images/butt_new.gif" AlternateText="New Contract Registration" runat="server"/>
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=SaveButton_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
						<asp:ImageButton id="btnDelete"    UseSubmitBehavior="false" AlternateText="Delete"     onClick="btnDelete_Click"    ImageURL="../../images/butt_delete.gif"    CausesValidation=False runat="server" />
						
						<asp:Button id=UpdDOBtn Text=" Updating DO " OnClick=UpdDOButton_Click CommandArgument=UpdDO  runat="server" />
						<asp:Button id=GenInvoiceBtn Text=" Generate Invoice " OnClick=Button_GenInvoice runat="server" />
						<asp:Button id=DeactivateDOBtn Text=" Deactivate DO " OnClick=Button_DeactivateDO runat="server" />
						<asp:Button id=GenSPK Text=" Generate SPK " OnClick=Button_GenSPK visible=False runat="server" />
					
					</td>
				</tr>
				<tr>
				<td colspan="6">
				<asp:dropdownlist id=ddlprint width=10%  runat=server>
					    <asp:ListItem value="1">Print DO</asp:ListItem>
						<asp:ListItem value="2">Print Nota Konfirmasi</asp:ListItem>
				</asp:dropdownlist>
				<asp:ImageButton id=PrintBtn AlternateText="Print Preview"  ImageUrl="../../Images/butt_print.gif" onClick="btnPrintPrev_Click" runat=server />		
				</td>
				</tr>
				
				<Input Type=Hidden id=tbcode runat=server />
				<Input Type=Hidden id=tbCtrNo runat=server />
				<Input Type=Hidden id=txtRemContractQty1 runat=server />				
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblActiveMatchExist visible=false text=0 runat=server />
				<asp:label id=lblShowGenInvoice visible=false text="no" runat=server />
				
			</table>
		</form>
	</body>
</html>
