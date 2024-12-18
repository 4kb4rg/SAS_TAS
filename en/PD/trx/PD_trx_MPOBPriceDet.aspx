<%@ Page Language="vb" src="../../../include/PD_trx_MPOBPriceDet.aspx.vb" Inherits="PD_trx_MPOBPriceDet" %>
<%@ Register TagPrefix="UserControl" Tagname="menu_PD_Trx" src="../../menu/menu_PDTrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>MPOB Price Details</title>
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
					<td colspan="6"><UserControl:menu_PD_Trx id=menu_CM_Trx runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">MPOB PRICE DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr id=TrWarning runat=server >
					<td colspan=6><b>Note : When you click Save, price of the contracts registered for this MPOB month and product is also updated.</b></td>
				</tr>
				<tr>
					<td width=20% height=25>MPOB Month :* </td>
					<td width=30%>
						<asp:DropDownList id="ddlAccMonth" width=24% runat=server>
							<asp:ListItem text="Jan" value="1" />
							<asp:ListItem text="Feb" value="2" />
							<asp:ListItem text="Mar" value="3" />
							<asp:ListItem text="Apr" value="4" />
							<asp:ListItem text="May" value="5" />
							<asp:ListItem text="Jun" value="6" />
							<asp:ListItem text="Jul" value="7" />
							<asp:ListItem text="Aug" value="8" />
							<asp:ListItem text="Sep" value="9" />
							<asp:ListItem text="Oct" value="10" />
							<asp:ListItem text="Nov" value="11" />
							<asp:ListItem text="Dec" value="12" />										
						</asp:DropDownList>
						<asp:DropDownList id="ddlAccYear" width=25% runat=server />
						<asp:Label id=lblAccMonth runat=server /><asp:Label id=lblAccYear runat=server />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Product :*</td>
					<td><asp:dropdownlist id=ddlProduct width=50% runat=server />
						<asp:label id=lblProduct runat=server />
						<asp:label id=lblProdCode visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Price :*</td>
					<td><asp:Textbox id=txtPrice maxlength=128 width=50% runat=server/>
						<asp:RequiredFieldValidator 
							id=rfvPrice
							display=dynamic 
							runat=server
							ControlToValidate=txtPrice
							text="<br>Please specify the Price." />
						<asp:RegularExpressionValidator id="revPrice" 
							ControlToValidate="txtPrice"
							ValidationExpression="\d{1,19}"
							Display="Dynamic"
							text = "<br>Maximum length 19 digits and 0 decimal points."
							runat="server"/>
						<asp:RangeValidator 
							id="rgvPrice"
							ControlToValidate="txtPrice"
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
				<tr>
					<td height=25>DN/CN Date :   </td>
					<td><asp:TextBox id=txtDocDate width=37% maxlength="10" runat="server"/>
					<a href="javascript:PopCal('txtDocDate');"><asp:Image id="btnDocDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:CustomValidator id="cvDocDate"
										ControlToValidate=txtDocDate
										Display=dynamic
										ErrorMessage="Invalid date format"
										OnServerValidate=DateValidation
										runat=server />
					<asp:label id=lblDateError Text ="<br>Document date is needed to generate DN/CN." forecolor=red Visible = false Runat="server"/></td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6><asp:label id=lblErrDup visible=false forecolor=red runat="server"/>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete "  CausesValidation=False imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText=" Undelete " imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
						<asp:ImageButton id=GenDNCNBtn AlternateText="  Generate DN/CN  " imageurl="../../images/butt_gen_dncn.gif" onclick=onClick_GenDNCN runat=server />
						<asp:label id=lblGenDNCNStatus visible=false runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=tbcode runat=server />
				<asp:Label id=lblHiddenSts visible=false text=0 runat=server/>
				<asp:label id=lblHidPrice visible=false runat=server />
				<asp:label id=lblHidAccMonth visible=false runat=server />
				<asp:label id=lblHidAccYear visible=false runat=server />
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
