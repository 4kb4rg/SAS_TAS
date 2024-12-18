<%@ Page Language="vb" src="../../../include/PR_setup_DendaDet.aspx.vb" Inherits="PR_Setup_DendaDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPRSetup" src="../../menu/menu_prsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Denda Code Details</title>
           <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:Label id="lblErrSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:Label id="lblDefault" visible="false" text="Default" runat="server" />
			<asp:label id="blnUpdate" Visible="False" Runat="server" />
			<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
			<Input Type=Hidden id=Dendacode runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPRSetup id=MenuPRSetup runat="server" /></td>
				</tr>
				<tr>
					<td  colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                 <strong>  DENDA DETAILS</strong> </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Updated : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
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
					<td width="20%" height="25">Denda Code :* </td>
					<td width="30%">
						<asp:Textbox id="txtDendaCode" width="50%" maxlength="8" runat="server"/>
						<asp:Label id=lblErrDupDendaCode visible=false forecolor=red text="<br>Denda code in used, please try other Denda Code." runat=server/>
						<asp:RequiredFieldValidator id=validateCode display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Denda Code"
							ControlToValidate=txtDendaCode />
						<asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtDendaCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id="lblDesc" runat="server" /> :* </td>
					<td><asp:Textbox id=txtDesc maxlength=128 width=100% runat=server/>
						<asp:RequiredFieldValidator id=validateDesc display=Dynamic runat=server 
							ErrorMessage="<br>Please Enter Description"
							ControlToValidate=txtDesc />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Denda Type :*</td>
					<td><asp:dropdownlist id=ddlDendaType OnSelectedIndexChanged=OnChangeIndexAD AutoPostBack=true width=100% runat=server/>
						<asp:Label id=lblErrDendaType visible=false forecolor=red text="<br>Please select one Denda Type." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25>Allowance & Deduction Code :*</td>
					<td><asp:DropDownList id=ddlAD width=85% runat=server/> 
						<input type="button" id=btnFind1 value=" ... " onclick="javascript:findcode('frmMain','','','','','','','','','','','','','','ddlAD','1',hidBlockCharge.value,hidChargeLocCode.value);" runat=server/>
						<asp:Label id=lblErrAD visible=false forecolor=red text="<br>Please select Allowance & Deduction code." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25 valign=top>Denda Rate :*</td>
					<td><asp:Textbox id=txtDendaRate maxlength=15 width=50% runat=server/>
						<asp:RequiredFieldValidator id=rvfDendaRate display=Dynamic runat=server 
							ErrorMessage="<br>Please enter Load Volume Baisis."
							ControlToValidate=txtDendaRate />
						<asp:CompareValidator id="cvDendaRate" display=dynamic runat="server" 
							ControlToValidate="txtDendaRate" Text="<br>Denda Rate must be an double. " 
							Type="Double" Operator="DataTypeCheck"/>	
						<asp:RegularExpressionValidator 
							id=revDendaRate
							ControlToValidate=txtDendaRate
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text = "<br>Maximum length 19 digits and 0 decimals. "
							runat="server"/></td>
					<td colspan=4>&nbsp;</td>					
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
             	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
