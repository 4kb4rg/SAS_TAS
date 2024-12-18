<%@ Page Language="vb"  src="../../../include/WS_Setup_MillProcDistr.aspx.vb" Inherits="WS_Setup_MillProcDistr" clienttarget="downlevel" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWS" src="../../menu/menu_wssetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Mill Process Distribution Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
			<Form id=frmWorkCodeDet runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<table border=0 cellpadding=2 width=100% class="font9Tahoma">
				<asp:label id=lblCode text=" Code" visible=false runat=server/>
				<asp:label id=lblPleaseEnter text="<br>Please enter " visible=false runat=server />
				<asp:label id=lblSelect visible=false text="Select " runat=server />
				<Input Type=Hidden id=WorkCode runat=server />				
				<tr>
					<td colspan="6"><UserControl:MenuWS id=menuWS runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">MONTHLY MILL PROCESS DISTRIBUTION</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20%>January :*</td>
					<td width=30%>
						<asp:TextBox id=txtJan width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankJan text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />						
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status : </td>
					<td width=25%><asp:Label id=lblStatus runat=server /> </td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td>February :*</td>
					<td><asp:TextBox id=txtFeb width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankFeb text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />												
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>March :*</td>
					<td><asp:TextBox id=txtMar width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankmar text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />						
					</td>
					<td>&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>April :*</td>
					<td><asp:TextBox id=txtApr width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankApr text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>	
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>May :*</td>
					<td><asp:TextBox id=txtMay width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankMay text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>June :*</td>
					<td><asp:TextBox id=txtJun width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankJun text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td>July :*</td>
					<td><asp:TextBox id=txtJul width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankJul text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td>August :*</td>
					<td><asp:TextBox id=txtAug width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankAug text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td>September :*</td>
					<td><asp:TextBox id=txtSep width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankSep text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td>October :*</td>
					<td><asp:TextBox id=txtOct width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankOct text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td>November :*</td>
					<td><asp:TextBox id=txtNov width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankNov text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td>December :*</td>
					<td><asp:TextBox id=txtDec width=50% maxlength=8 Text=0 runat=server />
						<asp:label id=lblErrBlankDec text = "<br>Data should be Numeric and Maximum length 7 digits. " visible=false forecolor=red runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onClick=SaveBtn_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn CausesValidation=false AlternateText=" Delete " imageurl="../../images/butt_delete.gif" onClick=DeleteBtn_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onClick=UnDelBtn_Click CommandArgument=UnDel runat=server />
					</td>
				</tr>
				<tr>
					<td colspan="6">
                                            &nbsp;</td>
				</tr>
			</table>
			<input Type=Hidden ID="hidBlockCharge" Value="" RunAt="Server" />
			<input Type=Hidden ID="hidChargeLocCode" Value="" RunAt="Server" />	
			<asp:Label id=lblErrDupl visible=false forecolor=red text="Data already exists." runat=server/>		

        <br />
        </div>
        </td>
        </tr>
        </table>


			</form>
	</body>
</html>
