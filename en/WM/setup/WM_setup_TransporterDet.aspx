<%@ Page Language="vb" trace=false src="../../../include/WM_setup_TransporterDet.aspx.vb" Inherits="WM_TransporterDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_wmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Weighing Management - Transporter Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	    </head>
	<body>
		<form id=frmTransporterDet runat=server class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">
			<asp:Label id=lblErrMessage visible=false text="Error while initiating component." runat=server />
			<asp:label id=lblCode visible=false text=" Code" runat="server" />
			<asp:label id=lblSelectList visible=false text="Select " runat="server" />
			<asp:label id=lblErrSelect visible=false text="Please select " runat="server" />
			<table border=0 cellspacing="1" cellpadding=1 width="100%" class="font9Tahoma">
			<input type=hidden id=hidTransCode runat=server />
				<tr>
					<td colspan=6><UserControl:MenuPU id=MenuPU runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">TRANSPORTER DETAILS</td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td width=20% height=25>Transporter Code :*</td>
					<td width=30%>
						<asp:TextBox id=txtTransCode width=50% maxlength=8 runat=server />
						<asp:Label id=lblErrDup visible=false forecolor=red text="Duplicate Transporter Code." runat=server />
						<asp:RequiredFieldValidator id=validateTransCode display=dynamic runat=server 
							ErrorMessage="<br>Please enter Transporter Code." 
							ControlToValidate=txtTransCode />
						<!--asp:RegularExpressionValidator id=revCode 
							ControlToValidate="txtTransCode"
							ValidationExpression="[a-zA-Z0-9\-]{1,8}"
							Display="Dynamic"
							text="<br>Alphanumeric without any space in between only."
							runat="server"/-->
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status :</td>
					<td width=25%><asp:Label id=lblStatus runat=server /></td>
					<td width=5%>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Transporter Type :*</td>
					<td>
					<asp:RadioButton id="rbExt" runat="server" Text="External" Checked="True" GroupName="TType"></asp:RadioButton>     		
					<asp:RadioButton id="rbInt" runat="server" Text="Internal" GroupName="TType"></asp:RadioButton>  
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					 <td height="25">Supplier Code :*</td>
					<td><asp:TextBox ID="txtSupCode" runat="server" AutoPostBack="False" MaxLength="15" Width="88%"></asp:TextBox>
						<input type=button value=" ... " id="Find" onclick="javascript:PopSupplier_New('frmTransporterDet','','txtSupCode','txtName','lbllbl','lbllbl','lbllbl','False');"  causesvalidation="False" runat="server"/>
						<asp:Label id=lblSuppCode text="Please Select Supplier Code" forecolor=red visible=false runat=server />
						<asp:RequiredFieldValidator id="validateSuppCode" 
													runat="server" 
													ErrorMessage="Please Specify Supplier Code" 
													ControlToValidate="txtSupCode" 
													display="dynamic"/>&nbsp;</td>	
						<asp:Label id=lbllbl visible=false runat=server />
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Name :*</td>
					<td><asp:TextBox id=txtName width=100% maxlength=128 runat=server />
						<asp:RequiredFieldValidator id=validateName display=dynamic runat=server 
							ErrorMessage="Please enter Transporter Name." 
							ControlToValidate=txtName />			
					</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>Address :</td>
					<td valign=top rowspan="4">
						<textarea rows="6" id=txtAddress cols="27" runat=server></textarea>
						<asp:Label id=lblErrAddress visible=false forecolor=red text="<br>Maximum length for address is up to 512 characters only." runat=server />
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6 height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6 height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6 height=25>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Telephone No. :</td>
					<td><asp:TextBox id=txtTelNo width=100% maxlength=16 runat=server />
						<asp:RegularExpressionValidator id="revTelNo" 
							ControlToValidate="txtTelNo"
							ValidationExpression="[\d\-\(\)]{1,16}"
							Display="dynamic"
							ErrorMessage="<br>Phone number must be in numeric digits"
							EnableClientScript="True" 
							runat="server"/>
					</td>
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
						<asp:ImageButton id=btnSave imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick=btnSave_Click runat=server />
						<asp:ImageButton id=btnDelete imageurl="../../images/butt_delete.gif" visible=true CausesValidation=false AlternateText=" Delete " onclick=btnDelete_Click runat=server />
						<asp:ImageButton id=btnUnDelete imageurl="../../images/butt_undelete.gif" visible=true AlternateText="Undelete" onclick=btnUnDelete_Click runat=server />
						<asp:ImageButton id=btnBack imageurl="../../images/butt_back.gif" CausesValidation=False AlternateText="  Back  " onclick=btnBack_Click runat=server />
					</td>
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
