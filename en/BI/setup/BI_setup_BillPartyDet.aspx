<%@ Page Language="vb" src="../../../include/BI_setup_BillPartyDet.aspx.vb" Inherits="BI_Setup_BillPartyDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuBI" src="../../menu/menu_bisetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Bill Party Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style3
            {
                font-size: 9pt;
                font-family: Tahoma;
                width: 188px;
            }
        </style>
	</head>
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>

         <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<asp:label id=lblErrEnter visible=false text="Please enter " runat="server" />
			<asp:label id=lblErrSelect visible=false text="Please select one " runat="server" />
			<asp:label id=lblSelect visible=false text="Select " runat="server" />
			<table border=0 cellspacing="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan=5>
						<UserControl:MenuBI id=MenuBI runat="server" />
					</td>
				</tr>
				<tr>
					<td   colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="style3">
                                   <strong> <asp:label id="lblTitle" runat="server" /> DETAILS </strong></td>
                                <td  class="font9Header" style="text-align: right">
                                    Status : | <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By :<asp:Label id=lblUpdateBy runat=server />
                                </td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td width=20%><asp:label id="lblBillParty" runat="server" /> Code :*</td>
					<td width=30%>
						<asp:TextBox id=txtBillPartyCode width=25% maxlength=8 Enabled=false runat=server CssClass="font9Tahoma" />
						<asp:Label id=lblErrDup visible=false forecolor=red text="<br>The code has been used, please try another code." runat=server />
						<!--asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtBillPartyCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/-->							
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=30%>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id="lblBillPartyName" runat="server" /> :*</td>
					<td><asp:TextBox id=txtName width=100% maxlength=128 runat=server CssClass="font9Tahoma" />
						<asp:RequiredFieldValidator id=rfvName display=dynamic runat=server 
							ControlToValidate=txtName />			
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Contact Person :</td>
					<td><asp:TextBox id=txtContactPerson width=100% maxlength=64 runat=server CssClass="font9Tahoma" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Address :* </td>
					<td rowspan="5">
						<textarea rows="6" id=txtAddress cols="27" style='width:100%;' runat=server></textarea>
						<asp:RequiredFieldValidator id=rfvAddress display=dynamic runat=server 
							ErrorMessage="<br>Please enter Address." 
							ControlToValidate=txtAddress />
						<asp:Label id=lblErrAddress visible=false forecolor=red text="<br>Maximum length for address is up to 512 characters only." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td>Town :</td>
					<td><asp:TextBox id=txtTown width=100% maxlength=64 runat=server CssClass="font9Tahoma" />
						<asp:CompareValidator id="cvTown" display=dynamic runat="server" 
							ControlToValidate="txtTown" Text="The value must whole characters." 
							Type="String" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td>Telephone No. :</td>
					<td><asp:TextBox id=txtTelNo width=100% maxlength=16 runat=server CssClass="font9Tahoma" />
						<asp:RegularExpressionValidator id="revTelNo" 
							ControlToValidate="txtTelNo"
							ValidationExpression="[\d\-\(\)]{1,16}"
							Display="dynamic"
							ErrorMessage="Phone number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td>State :</td>
					<td><asp:TextBox id=txtState width=100% maxlength=64 runat=server CssClass="font9Tahoma" />
						<asp:CompareValidator id="cvState" display=dynamic runat="server" 
							ControlToValidate="txtState" Text="The value must whole characters." 
							Type="string" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td>Fax No. :</td>
					<td><asp:TextBox id=txtFaxNo width=100% maxlength=16 runat=server CssClass="font9Tahoma" />
						<asp:RegularExpressionValidator id="revFaxNo" 
							ControlToValidate="txtFaxNo"
							ValidationExpression="[\d\-\(\)]{1,16}"
							Display="dynamic"
							ErrorMessage="Fax number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
				</tr>
				<tr>
					<td>Post Code :</td>
					<td><asp:TextBox id=txtPostCode width=15% maxlength=16 runat=server CssClass="font9Tahoma"  />
						<asp:CompareValidator id="cvPostCode" display=dynamic runat="server" 
							ControlToValidate="txtPostCode" Text="<br>The value must whole number." 
							Type="integer" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td>NPWP :</td>
					<td><asp:TextBox id=txtEmail width=100% maxlength=64 runat=server CssClass="font9Tahoma" />
					</td>
				</tr>
				<tr>
					<td>Country :</td>
					<td>
						<asp:DropDownList id=ddlCountry width=100% runat=server CssClass="font9Tahoma" />
						<asp:Label id=lblErrCountry visible=false forecolor=red runat=server/>
		  			</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><asp:label id="lblBankCode" runat="server" /></td>
					<td>
						<asp:DropDownList id=ddlBankCode width=100% runat=server CssClass="font9Tahoma" />
		  			</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td><asp:label id="lblBankAccNo" runat="server" /></td>
					<td>
						<asp:TextBox id=txtBankAccNo width=100% runat=server CssClass="font9Tahoma" />
		  			</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td>Credit Term :*</td>
					<td><asp:TextBox id=txtCreditTerm width=15% maxlength=3 Text=30 runat=server CssClass="font9Tahoma" />
						Days
						<asp:RequiredFieldValidator id=rfvCreditTerm display=dynamic runat=server 
							ErrorMessage="<br>Please enter Credit Term." 
							ControlToValidate=txtCreditTerm />
						<asp:CompareValidator id="cvCreditTerm" display=dynamic runat="server" 
							ControlToValidate="txtCreditTerm" Text="<br>The value must whole number." 
							Type="integer" Operator="DataTypeCheck"/>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td>Credit Term Type :*</td>
					<td><asp:DropDownList id="ddlTermType" width=100% runat=server CssClass="font9Tahoma" >
							<asp:ListItem value="0">Days after Acceptance Date</asp:ListItem>
							<asp:ListItem value="1">Days after Bill of Lading</asp:ListItem>
							<asp:ListItem value="2">Days after Date of Draft</asp:ListItem>
							<asp:ListItem value="3">Days after Delivery Order Date</asp:ListItem>
							<asp:ListItem value="4">Days after Date of Invoice</asp:ListItem>
							<asp:ListItem value="5">Days after Shipment Date</asp:ListItem>
							<asp:ListItem value="6">Days Sight</asp:ListItem>
							<asp:ListItem value="7">Days from Acceptance Date</asp:ListItem>
							<asp:ListItem value="8">Days from Bill of Lading</asp:ListItem>
							<asp:ListItem value="9">Days from Date of Draft</asp:ListItem>
							<asp:ListItem value="10">Days from Delivery Order Date</asp:ListItem>
							<asp:ListItem value="11">Days from Date of Invoice</asp:ListItem>
							<asp:ListItem value="12">Sight</asp:ListItem>
							<asp:ListItem value="13" selected>Days from Date of Created Document</asp:ListItem>
						</asp:DropDownList>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td>Credit Limit : </td>
					<td><asp:TextBox id=txtCreditLimit width=25% maxlength=13 Text=0 runat=server CssClass="font9Tahoma" />
						<asp:CompareValidator id="cvCreditLimit" display=dynamic runat="server" 
							ControlToValidate="txtCreditLimit" Text="<br>The value must be numeric." 
							Type="double" Operator="DataTypeCheck"/> </td>
						<asp:RegularExpressionValidator id=revCreditLimit 
							ControlToValidate="txtCreditLimit"
							ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td><asp:label id="lblAccount" runat="server" /> :*</td>
					<td>
						<asp:dropdownlist id=ddlAccCode width=80% runat=server CssClass="font9Tahoma" /> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCode', 'True');" runat=server/>
						<asp:Label id=lblErrAccCode visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Advance COA :*</td>
					<td>
						<asp:dropdownlist id=ddlAccCodeAdv width=80% runat=server CssClass="font9Tahoma" /> 
						<input type="button" id=btnFind2 value=" ... " onclick="javascript:PopCOA('frmMain', '', 'ddlAccCodeAdv', 'True');" runat=server/>
						<asp:Label id=lblErrAccCodeAdv visible=false forecolor=red runat=server/>
					</td>
				</tr>
				<tr>
					<td>Inter Location Billing :*</td>
					<td>
						<asp:dropdownlist id=ddlInterLocCode width=80% runat=server CssClass="font9Tahoma" /> 
						<asp:Label id=lblErrInterLocCode visible=false forecolor=red runat=server/>
						<asp:Label id=lblDupInterLocCode visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				    <td height=25 valign=top>Additional Note : </td>
					<td><textarea rows=6 id=taAdditionalNote cols=50 style='width:100%;' runat=server></textarea></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
					<td class="mt-h">Contract Information</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Contact Person :</td>
					<td><asp:TextBox id=txtPIC width=100% maxlength=64 runat=server CssClass="font9Tahoma" /></td>
					<td>&nbsp;</td>
					<td>Position :</td>
					<td><asp:TextBox id=txtPosition width=100% maxlength=64 runat=server CssClass="font9Tahoma" /></td>
				</tr>
				<tr>
    				<td>PPN :	</td>
    				<td><asp:CheckBox id="chkPPN" Text="  " checked=false AutoPostBack=true OnCheckedChanged=chkPPN_Change runat=server /></td>
				</tr>
				<tr>
				    <td height=25 valign=top>Term of Weighing : </td>
					<td><textarea rows=3 id=taTermOfWeighing cols=50 style='width:100%;' runat=server></textarea></td>
					<td>&nbsp;</td>
					<td>Loading Destination :	</td>
					<td><textarea rows=3 id=taLoadDest cols=50 style='width:100%;' runat=server></textarea></td>
				</tr>
				<tr>
				    <td height=25 valign=top>Term of Payment : </td>
					<td><textarea rows=5 id=taTermOfPayment cols=50 style='width:100%;' runat=server></textarea></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			    <tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
					<td class="mt-h">Crude Palm Oil (CPO)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td class="mt-h">Palm Kernel (PK)</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25" valign=top>Contract Quality :</td>
					<td><textarea rows=5 id=taProdQualityCPO cols=50 style='width:100%;' runat=server></textarea></td>					
					<td>&nbsp;</td>
					<td height="25" valign=top>Contract Quality :</td>
					<td><textarea rows=5 id=taProdQualityPK cols=50 style='width:100%;' runat=server></textarea></td>	
				</tr>
				<tr>
					<td height="25" valign=top>Claim Quality :</td>
					<td><textarea rows=5 id=taProdClaimCPO cols=50 style='width:100%;' runat=server></textarea></td>					
					<td>&nbsp;</td>
					<td height="25" valign=top>Claim Quality :</td>
					<td><textarea rows=5 id=taProdClaimPK cols=50 style='width:100%;' runat=server></textarea></td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">
						<asp:Label id=lblHiddenStatus visible=false text="0" runat=server/>
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnDelete causesvalidation=false imageurl="../../images/butt_delete.gif" visible=true AlternateText=" Delete " onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnUnDelete causesvalidation=false imageurl="../../images/butt_undelete.gif" visible=true AlternateText="Undelete" onclick=btnUnDelete_Click runat=server />
						<asp:ImageButton id=btnBack causesvalidation=false imageurl="../../images/butt_back.gif" AlternateText="  Back  " onclick=btnBack_Click runat=server />
					    <br />
					</td>
				</tr>
			</table>
			<input type=hidden id=bpcode runat=server />
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>    
	</body>
</html>
