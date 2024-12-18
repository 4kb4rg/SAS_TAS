<%@ Page Language="vb" src="../../../include/PD_trx_POMProdDet.aspx.vb" Inherits="PD_trx_POMProdDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Palm Oil Mill Production Details</title>
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
					<td colspan="5">
						<UserControl:MenuPDTrx id=MenuPDTrx runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="5"><strong>PALM OIL MILL PRODUCTION DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan=5><hr style="width :100%" /> </td>
				</tr>
				<tr>
					<td height=25>P.O.M. Yield ID : </td>
					<td>
						<asp:Label id=lblPOMYieldID runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Period : </td>
					<td><asp:Label id=lblPeriod runat=server /></td>
				</tr>
				<tr>
					<td>Date :* </td>
					<td>
						<asp:Textbox id=txtDate width=50% maxlength=10 runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
						<asp:RequiredFieldValidator id=rfvDate display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Date."
								ControlToValidate=txtDate />
						<asp:Label id=lblErrDate forecolor=red runat=server/>
						<asp:Label id=lblErrDateMsg visible=false text="<br>Date Format should be in " runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Status : </td>
					<td><asp:Label id=lblStatus runat=server /></td>
				</tr>
				<tr>
					<td width=20%>Production :* </td>
					<td width=30%>
						<asp:Dropdownlist id=ddlProduction width=100% autopostback=true onselectedindexchanged=BindProdNameRel runat=server />
						<asp:Label id=lblErrProd visible=false forecolor=red text="<br>Please select one Production." runat=server />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=20%>Date Created : </td>
					<td width=25%><asp:Label id=lblDateCreated runat=server/></td>
				</tr>
				<tr>
					<td>Type :*</td>
					<td>
						<asp:Dropdownlist id=ddlType width=100% runat=server/>
						<asp:Label id=lblErrType visible=false forecolor=red text="<br>Please select one Type." runat=server/>
						<asp:Label id=lblErrRelDesc visible=false forecolor=red text=" cannot be " runat=server/>
						<asp:Label id=lblErrRel visible=false forecolor=red text="<br>" runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left">Storage :</td>
					<td align="left">
						<asp:Dropdownlist id=ddlStorage width=100% runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td>Weight :*</td>
					<td>
						<asp:Textbox id=txtWeight maxlength=21 width=50% runat=server/>&nbsp;&nbsp; Metric Ton
						<asp:RequiredFieldValidator id=rfvWeight display=Dynamic runat=server 
								ErrorMessage="<br>Please Enter Weight."
								ControlToValidate=txtWeight />
						<asp:CompareValidator id="cvWeight" display=dynamic runat="server" 
							ControlToValidate="txtWeight" Text="<br>The value must whole number or with decimal. " 
							Type="Double" Operator="DataTypeCheck"/>
						<asp:RegularExpressionValidator id=revWeight 
							ControlToValidate="txtWeight"
							ValidationExpression="\d{1,15}\.\d{1,5}|\d{1,15}"
							Display="Dynamic"
							text = "Weight must be greater than zero, with maximum length 15 digits and 5 decimal points. "
							runat="server"/>
						<asp:RangeValidator id="Range1"
							ControlToValidate="txtWeight"
							MinimumValue="1"
							MaximumValue="999999999999999"
							Type="double"
							EnableClientScript="True"
							Text="<br>The value must be greater than 0!"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td></td>
					<td></td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Confirm  " imageurl="../../images/butt_confirm.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText=" Undelete " imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=btnNew AlternateText=" New " imageurl="../../images/butt_new.gif" onclick=Button_Click CommandArgument=New runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=pomid runat=server />
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server />
				<tr>
					<td colspan="5">
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
