<%@ Page Language="vb" src="../../../include/PR_setup_BlokBJRDet_Estate.aspx.vb" Inherits="PR_setup_BlokBJRDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Block of Division Details</title>
          <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                height: 14px;
            }
            .style2
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
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
           <table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">
                        <table cellpadding="0" cellspacing="0" class="style2">
                            <tr>
                                <td class="font9Tahoma">
                       <strong> BJR DETAILS </strong></td>
                                <td class="font9Header" style="text-align: right">
                                    Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Status : <asp:Label id=lblStatus runat=server />&nbsp;| Last Updated : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdatedBy runat=server />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />

                    </td>
				</tr>
				<tr>
					<td colspan=6 class="style1"></td>
				</tr>
				
				<tr>
					<td align="left" style="width: 114px">
                        Periode Start</td>
					<td align="left">
                        <asp:Textbox id=txtpstart maxlength=6 width="40%" runat=server onkeypress="javascript:return isNumberKey(event)"></asp:Textbox></td>
					<td style="width: 79px">&nbsp;</td>
					<td width=20%>&nbsp;</td>
					<td width=25%>&nbsp;</td>								
				</tr>	
				
				<tr>
					<td align="left" style="height: 22px; width: 114px;">
                        Periode End</td>
					<td align="left" style="height: 22px">
                        <asp:TextBox ID="txtpend" runat="server" MaxLength="6" Width="40%" onkeypress="javascript:return isNumberKey(event)"></asp:TextBox></td>
					<td style="width: 79px; height: 22px;">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
					<td style="height: 22px">&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="width: 114px">
                        Tahun Tanam</td>
					<td align="left">
                        <asp:DropDownList ID="ddlypcode" runat="server" Width="100%">
                        </asp:DropDownList></td>
					<td style="width: 79px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td style="width: 114px">
                        BJR</td>
					<td>                        
                        <asp:Textbox id=txtbjr maxlength=64 width="60%" onkeypress="javascript:return isNumberKey(event)" runat=server></asp:Textbox><td style="width: 79px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="left" style="height: 23px; width: 114px;"></td>
					<td align="left" style="height: 23px">
					</td>
					<td style="width: 79px; height: 23px;">&nbsp;</td>					
					<td style="height: 23px"></td>
					<td style="height: 23px"></td>
				</tr>

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					</td>
				</tr>
				<Input Type=Hidden id=BlokCode runat=server />&nbsp;
				<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
        </div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
