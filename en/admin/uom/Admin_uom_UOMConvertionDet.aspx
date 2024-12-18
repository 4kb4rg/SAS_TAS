<%@ Page Language="vb" src="../../../include/Admin_uom_UOMConvertionDet.aspx.vb" Inherits="Admin_UOMConvertionDet" %>
<%@ Register TagPrefix="UserControl" TagName="MenuAdmin" src="../../menu/menu_admin.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Unit of Measurement Convertion Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	    <style type="text/css">

a {
	text-decoration:none;
    text-align: right;
}


hr {
	width: 1368px;
    border-top-style: none;
    border-top-color: inherit;
    border-top-width: medium;
    margin-left: 0px;
    margin-bottom: 0px;
}
        </style>
	</head>
	<body>
		<form id=frmUOMConvertionDet class="main-modul-bg-app-list-pu" runat="server">
                        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 2000px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 

    		<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:label id="SortExpression" Visible="False" Runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma"> 
				<tr>
					<td colspan="5">
						<UserControl:MenuAdmin id=MenuAdmin runat="server" />
					</td>
				</tr>
				<tr>
					<td class="font9Tahoma" colspan="5"><strong> UNIT OF MEASUREMENT CONVERTION DETAILS</strong></td>
				</tr>
				<tr>
					<td colspan=5><hr style="width:100%">
                        </td>
				</tr>
				<tr>
					<td width=18%>From UOM Code :* </td>
					<td width=32%><asp:dropdownlist id=ddlUOMFrom width=100% runat=server></asp:dropdownlist>
						<asp:Label id=lblErrUOMFrom visible=false forecolor=red text="<BR>Please select one unit of measurement." runat=server/>
						<asp:label id=lblErrDup text="Duplicate Convertion Code" visible=false forecolor=red runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=30%>
						<asp:Label id=lblStatus runat=server />
						<asp:Label id=lblHiddenStatus text="0" visible=false runat=server />
					</td>
				</tr>
				<tr>
					<td>To UOM Code :* </td>
					<td><asp:dropdownlist id=ddlUOMTo width=100% runat=server></asp:dropdownlist>
						<asp:Label id=lblErrUOMTo visible=false forecolor=red text="<BR>Please select one unit of measurement." runat=server/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
				</tr>
				<tr>
					<td>Convertion Rate :* </td>
					<td><asp:TextBox id=txtRate width=50% maxlength=15 runat=server />
						<asp:RequiredFieldValidator id=rfvRate display=dynamic runat=server 
							ErrorMessage="<br>Please enter convertion rate. " 
							ControlToValidate=txtRate />
	                    <asp:RegularExpressionValidator id="revRate" 
							ControlToValidate="txtRate"
							ValidationExpression="\d{1,9}\.\d{1,5}|\d{1,9}"
							Display="Dynamic"
							text = "Maximum length 9 digits and 5 decimal points"
							runat="server"/>
						<asp:CompareValidator id="ChckRate" runat="server" 
							display=dynamic ControlToValidate="txtRate" Text="The value must be numeric." 
							Type="double" Operator="DataTypeCheck"/> </td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr><td colspan=5>&nbsp;</td></tr>
				<tr>
					<td colspan="5">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onClick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " CausesValidation=False imageurl="../../images/butt_delete.gif" onClick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onClick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onClick=BackBtn_Click runat=server />
					</td>
				</tr>
				<Input Type=Hidden id=UOMFrom runat=server />
				<Input Type=Hidden id=UOMTo runat=server />
			</table>

                    </div>
            </td>
            </tr>
            </table>
			</form>
	</body>
</html>
