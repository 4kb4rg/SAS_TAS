<%@ Page Language="vb" src="../../../include/PD_trx_YearOfPlantYieldDet.aspx.vb" Inherits="PD_trx_YearOfPlantYieldDet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_PD_Trx" src="../../menu/menu_PDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Year of Planting Yield Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:menu_PD_Trx id=menu_PD_Trx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">YEAR OF PLANTING YIELD DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr valign=top>
					<td width=20% height=25>Year of Planting :* </td>
					<td width=30%>
						<asp:textbox id=txtYearOfPlant width=50% maxlength=6 runat=server />
						<asp:RequiredFieldValidator 
							id=rfvYearOfPlant
							display=dynamic 
							runat=server
							ControlToValidate=txtYearOfPlant
							text="<br>Please specify the Year of Planting." />
						<asp:label id=lblErrMatchYear visible=false forecolor=red runat=server />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Period : </td>
					<td width=25%><asp:Label id=lblPeriod runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td width=20% height=25>Group Reference : </td>
					<td width=30%><asp:textbox id=txtGroupRef width=100% maxlength=20 runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Reference Date : </td>
					<td>
						<asp:textbox id=txtRefDate width=50% runat=server />
						<a href="javascript:PopCal('txtRefDate');"><asp:Image id=btnSelDate runat=server ImageUrl="../../images/calendar.gif"/></a>
						<asp:label id=lblErrRefDate forecolor=red visible=false runat=server />
						<asp:label id=lblErrRefDateDesc visible=false text="<br>Date format should be in " runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Rate/Weight :*</td>
					<td><asp:Textbox id=txtRate maxlength=128 width=50% runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvRate
							display=dynamic 
							runat=server
							ControlToValidate=txtRate
							text="<br>Please specify the Rate." />
						<asp:RegularExpressionValidator id="revRate" 
							ControlToValidate="txtRate"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvRate"
							ControlToValidate="txtRate"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr valign=top>
					<td height=25>Total Weight (MT) :*</td>
					<td><asp:textbox id=txtTotalWeight width=50% runat=server />
						<asp:RequiredFieldValidator 
							id=rfvTotalWeight
							display=dynamic 
							runat=server
							ControlToValidate=txtTotalWeight
							text="<br>Please specify the Total Weight." />
						<asp:RegularExpressionValidator id="revTotalWeight" 
							ControlToValidate="txtTotalWeight"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "<br>Maximum length 15 digits and 5 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvTotalWeight"
							ControlToValidate="txtTotalWeight"
							MinimumValue="0"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>Incorrect format or out of acceptable range. "
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><asp:label id=lblErrDup text="<br>Year of Planting Yield is already existed. Please try again.<br>" visible=false forecolor=red runat="server"/>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText=" Undelete " imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<input type=hidden id=tbcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>
				<asp:Label id=lblBlock visible=false runat=server />
				<tr>
					<td colspan="6">
                                            &nbsp;</td>
				</tr>
				</table>

        <br />
        </div>
        </td>
        </tr>
        </table>


		</form>
	</body>
</html>
