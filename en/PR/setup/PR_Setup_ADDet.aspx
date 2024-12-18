<%@ Page Language="vb" src="../../../include/PR_setup_ADdet.aspx.vb" Inherits="PR_setup_ADdet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Allowance And Deduction Code Details</title>
                 <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />   
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"   runat="server">
               <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblDefault" visible="false" text="Default" runat="server" />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=adcode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6"><strong>ALLOWANCE AND DEDUCTION CODE DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width="12%" height="25">Allowance and Deduction Code :* </td>
					<td width="57%">
						<asp:Textbox id="txtADCode" width="50%" maxlength="8" runat="server"/>
						<asp:Label id=lblErrDupADCode visible=false forecolor=red text="<br>AD code in used, please try other AD Code." runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Allowance and Deduction Code"
							ControlToValidate=txtADCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtADCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
					</td>
					<td width=3%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=10%><asp:Label id=lblStatus runat=server /></td>
					<td width=3%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Description :* </td>
					<td><asp:Textbox id=txtDesc maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Description"
							ControlToValidate=txtDesc />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>A.D. Group Code :*</td>
					<td><asp:DropDownList id=ddlADGroup width=100% runat=server/>
						<asp:Label id=lblErrADGroup visible=false forecolor=red text="<br>Please select one Allowance and Deduction Group." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Type :*</td>
					<td><asp:DropDownList id=ddlType width=100% onSelectedIndexChanged=onChange_ADType autopostback=true runat=server/>
						<asp:Label id=lblErrADType visible=false forecolor=red text="<br>Please select one Allowance and Deduction type." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Default <asp:label id="lblDefAcc" runat="server" /> :*</td>
					<td><asp:DropDownList id=ddlDefAccCode onSelectedIndexChanged=onSelect_DefAccount width=94% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','ddlDefAccCode','6','ddlDefBlkCode','','','','','','','','','','','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrDefAccCode visible=false forecolor=red text="<br>Please select one Account." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td><asp:label id=lblJamSostek runat=server /> Contribution : </td>
					<td><asp:CheckBox id=cbJamsContribute text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Default <asp:label id="lblDefBlock" runat="server" /> :</td>
					<td><asp:DropDownList id=ddlDefBlkCode width=100% runat=server/>
						<asp:Label id=lblErrDefBlkCode visible=false forecolor=red text="<br>Please select one Block." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Tax Contribution :</td>
					<td><asp:CheckBox id=cbTaxContribute text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblVehicle" runat="server" /> :</td>
					<td><asp:Dropdownlist id=ddlVehCode width=100% runat=server/>
						<asp:Label id=lblErrVehicle visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Maternity Allowance :</td>
					<td><asp:CheckBox id=cbMaternityAllowance text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblVehExpense" runat="server" /> :</td>
					<td><asp:Dropdownlist id=ddlVehExpCode width=100% runat=server/>
						<asp:Label id=lblErrVehExp visible=false forecolor=red runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Bonus :</td>
					<td><asp:CheckBox id=cbBonus text=" Yes" OnCheckedChanged="Bonus_Clicked" autopostback=true runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Payslip A.D. :</td>
					<td><asp:DropDownList id=ddlPaySlip width=94% runat=server/> 
						<input type="button" id=btnFind2 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlPaySlip','',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>THR :</td>
					<td><asp:CheckBox id=cbTHR text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>House Rent Allowance :</td>
					<td><asp:CheckBox id=cbHouseRent text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Transport Allowance :</td>
					<td><asp:CheckBox id=cbTransport text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Medical Allowance :</td>
					<td><asp:CheckBox id=cbMedical text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Meal Allowance :</td>
					<td><asp:CheckBox id=cbMeal text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Leave Allowance :</td>
					<td><asp:CheckBox id=cbLeave text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Air Fare/Bus Ticket :</td>
					<td><asp:CheckBox id=cbAirBus text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Tax Allowance :</td>
					<td><asp:CheckBox id=cbTax text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Dana Pensiun :</td>
					<td><asp:CheckBox id=cbDanaPensiun text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Relocation Allowance :</td>
					<td><asp:CheckBox id=cbRelocation text=" Yes" runat=server/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
     	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
