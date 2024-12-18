<%@ Page Language="vb" trace=false src="../../../include/HR_setup_StaffDet.aspx.vb" Inherits="HR_setup_StaffDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_HRsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Human Resource - Staff Details</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
                  <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />   
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>
	</head>
	<body>
		<form id=frmTransporterDet class="main-modul-bg-app-list-pu" runat=server>
                   <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist">
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<asp:label id=lblSelectList visible=false text="Select " runat="server" />
			<asp:label id=lblErrSelect visible=false text="Please select " runat="server" />
			<table border=0 cellspacing="1" cellpadding=1 width="100%" class="font9Tahoma">
			<input type=hidden id=hidStaffID runat=server />
				<tr>
					<td colspan=6><UserControl:MenuPU id=MenuPU runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">
                        <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                <strong>  STAFF DETAILS</strong>  </td>
                                <td  class="font9Header" style="text-align: right">
                                    Status : <asp:Label id=lblStatus runat=server />&nbsp;| Date Created : <asp:Label id=lblDateCreated runat=server />&nbsp;| Last Update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Updated By : <asp:Label id=lblUpdateBy runat=server />
                                </td>
                            </tr>
                        </table>
                    </td>
				</tr>
				<tr>
					<td colspan=6>
                    <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td width=20% height=25>Staff ID :*</td>
					<td width=30%>
						<asp:TextBox id=txtStaffID BorderStyle=None ForeColor=white BackColor=transparent width=50% Enabled=false maxlength=8 runat=server />
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>&nbsp;</td>
					<td width=25%>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Type :*</td>
					<td>
					    <asp:RadioButton id="rbInt" runat="server" Text="Internal" Checked="True" GroupName="SType"></asp:RadioButton>  
					    <asp:RadioButton id="rbExt" runat="server" Text="External" GroupName="SType"></asp:RadioButton>     		
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width=20% height=25>Name :*</td>
					<td width=30%>
						<asp:TextBox id=txtName width=100% maxlength=128 runat=server /></td>
					<td width=5%>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>NIK :</td>
					<td><asp:TextBox id=txtNIK width=50% maxlength=32 runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Address :</td>
					<td valign=top rowspan="4">
						<textarea rows="6" id=txtAddress cols="27" style='width:100%;' runat=server></textarea>
						<asp:Label id=lblErrAddress visible=false forecolor=red text="<br>Maximum length for address is up to 512 characters only." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=5>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Bank :</td>
					<td><asp:DropDownList id=ddlBankCode width=100% runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>Bank AC No.:</td>
					<td><asp:TextBox id=txtBankAccNo width=50% maxlength=128 runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Telephone No. :</td>
					<td><asp:TextBox id=txtTelNo width=70% TextMode=MultiLine style='width:50%;' runat=server /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6 height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id=btnNew imageurl="../../images/butt_new.gif" AlternateText="  New  " onclick=btnNew_Click runat=server />
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" visible=true CausesValidation=false AlternateText=" Delete " onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" visible=true AlternateText="Undelete" onclick=btnUnDelete_Click runat=server />
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
