<%@ Page Language="vb" Src="../../../include/FA_setup_AssetPermitDetails.aspx.vb" Inherits="FA_setup_AssetPermitDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFASetup" src="../../menu/menu_FASetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>FIXED ASSET - Asset Permission Details</title>
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
                width: 237px;
            }
        </style>
	</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu"   runat="server">
           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id="lblOper" Visible="False" Runat="server"></asp:label>
			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<table cellspacing="1" cellpadding="1" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6">
						<UserControl:MenuFASetup id=MenuFASetup runat="server" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6" width=60%>
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="style3">
                                  <strong><asp:label id="lblTitle" runat="server" /> DETAILS </strong>  </td>
                                <td class="font9Header" style="text-align: right">
                                    Status : <asp:Label id="lblStatus" runat="server"/>&nbsp;| Date Created :<asp:Label id="lblCreateDate" runat="server"/>&nbsp;| Last Update : <asp:Label id="lblLastUpdate" runat="server"/>&nbsp;| Update By : <asp:Label id="lblUpdateBy" runat="server"/>
                                </td>
                            </tr>
                        </table>
                           <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4" width=60%>&nbsp;</td>
					<td colspan="2" align=right><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td width="30%" height=25><asp:label id=lblAssetCodeTag Runat="server"/> :*</td>
					<td width="25%"><asp:TextBox id="txtAssetCode" Width=100% runat=server />
						<asp:label id=lblAssetCodeErr Visible=False forecolor=red Runat="server" />
					</td>
					<td width="5%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="15%">&nbsp;</td>
					<td width="10%">&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAssetAddPermTag Runat="server"/> :</td>
					<td align=left valign=top>
						<asp:Checkbox id=cbAssetAddPerm text=" Yes" runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAssetGenDeprPermTag Runat="server"/> :</td>
					<td align=left valign=top>
						<asp:Checkbox id=cbAssetGenDeprPerm text=" Yes" runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAssetManDeprPermTag Runat="server"/> :</td>
					<td align=left valign=top>
						<asp:Checkbox id=cbAssetManDeprPerm text=" Yes" runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAssetDispPermTag Runat="server"/> :</td>
					<td align=left valign=top>
						<asp:Checkbox id=cbAssetDispPerm text=" Yes" runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><asp:label id=lblAssetWOPermTag Runat="server"/> :</td>
					<td align=left valign=top>
						<asp:Checkbox id=cbAssetWOPerm text=" Yes" runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				   <td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="  Back  " onclick=btnBack_Click runat=server />
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
