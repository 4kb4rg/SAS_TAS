<%@ Page Language="vb" src="../../../include/PR_setup_YearPlanDet_Estate.aspx.vb" Inherits="PR_setup_YearPlanDet_Estate" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuHRSetup" src="../../menu/menu_hrsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<script runat="server">

Protected Sub rbTypeMatureField_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
</script>

<html>
	<head>
		<title>Block of Division Details</title>
                <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
                	<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
	
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
<tr>
	<td style="width: 100%; height: 1500px" valign="top">
	<div class="kontenlist">
			<asp:Label id="lblErrMessage" visible="false" ForeColor=red Text="Error while initiating component." runat="server" />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
				<tr>
					<td colspan="5">
						<UserControl:MenuHRSetup id=MenuHRSetup runat="server" />
					</td>
				</tr>
				<tr>                    
					<td class="mt-h" colspan="5">
                        &nbsp;Tahun Tanam Detail</td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
										
				<tr>
					<td width=20%>
                        Tahun Tanam Code</td>
					<td width=30%>
                        <asp:Textbox id=txtypcode maxlength=4 width="60%" runat=server />
					<td style="width: 79px">&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>								
				</tr>				
				
				<tr>
					<td align="left" style="height: 28px">
                        Deskripsi Tahun Tanam</td>
					<td align="left" style="height: 28px">
						<asp:Textbox id=txtypdeskripsi  maxlength=100 width="100%" runat=server /></td>
					<td style="width: 79px; height: 28px;">&nbsp;</td>
					<td width=20% style="height: 28px">Status : </td>
					<td width=25% style="height: 28px"><asp:Label id=lblStatus runat=server /></td>								
				</tr>	
				
				<tr>
					<td align="left">
                        Divisi Code</td>
					<td align="left">
                        <asp:DropDownList ID="ddldivisi" runat="server" Width="60%">
                        </asp:DropDownList></td>
					<td style="width: 79px">&nbsp;</td>
					<td>Last Updated : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
				</tr>
				<tr>
					<td align="left" style="height: 44px">
                        Tahun Tanam Type</td>
					<td align="left" style="height: 44px">
                        <asp:RadioButton ID="rbTypeInMatureField" runat="server" GroupName="blocktype" Text=" Immature Field" /><br />
                        <asp:RadioButton ID="rbTypeMatureField" runat="server" GroupName="blocktype" Text=" Mature Field" /><br />
                        <asp:RadioButton ID="rbTypeNursery" runat="server" GroupName="blocktype" Text=" Nursery" /></td>
					<td style="width: 79px; height: 44px;">&nbsp;</td>
					<td style="height: 44px">Updated By : </td>
					<td style="height: 44px"><asp:Label id=lblUpdatedBy runat=server /></td>
				</tr>
				<tr>
					<td><asp:Label id=lbl_newyr runat=server Visible="False" >Tahun Tanam</asp:Label></td>
					<td><asp:Textbox id=txt_newyr maxlength=4 width="40%" runat=server Visible="False" /><td style="width: 79px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>

				<td colspan="5" style="height: 23px">&nbsp;</td>
				<tr>
					<td colspan="5" style="height: 28px">
						<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
						<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					    <br />
					<asp:Button ID="Issue4" class="button-small" 
                        runat="server" Text="Save"  />
					</td>
				</tr>
				<Input Type=Hidden id=YpCode runat=server />
				<Input Type=Hidden id=isNew runat=server />			
			</table>
     	</div>
        </td>
        </tr>
        </table>
		</form>
	</body>
</html>
