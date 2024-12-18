<%@ Page Language="vb" src="../include/sysmenu.aspx.vb" Inherits="sysmenu" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>
<html>
<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>System Menu</title>
</head>
<body class="menu" onload="javascript:togglebox(tblOther);" topmargin="0" leftmargin="0">
<form id=frmSysMenu runat=server>
<table border=0 cellpadding="0" cellspacing="0" width=100%>
	<tr>
		<td align="center"><img src="images/spacer.gif" border="0" width="5" height="5"></td>
	</tr>
	
	<tr>
		<td>
			<table width=100% border=0 cellpading=0 cellspacing=0>
				<tr height="25" >
					<td width="20" class="lb-ht"></td>
					<td width="2"></td>
					<td class="lb-ht"><a href="javascript:togglebox(tblSysCfg);" class="lb-ti">System Setup</a></td>
				</tr>
				
			</table>
			<table id=tblSysCfg style="visibility:hidden; position: absolute;" width=100% border=0 cellpading=0 cellspacing=0 runat=server>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkSysCfg
								class="lb-mt"
								text="System Configuration"
								target=right
								runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkParam
								class="lb-mt"
								text="Parameters Setting"
								target=right
								runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkAppUser
							class="lb-mt"
							text="Application User"
							target=right
							runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkLangCap
							class="lb-mt"
							text="Penamaan Istilah"
							target=right
							runat=server />    
					</td>
				</tr>				
			</table>
			
			<table width=100% cellpading=0 cellspacing=0>
				<tr><td colspan="2"><img src="images/spacer.gif" border="0" width="5" height="5"></td></tr>
			</table>

			<table width=100% cellpading=0 cellspacing=0>
				<tr height="25" >
					<td width="20" class="lb-ht"></td>
					<td width="2"></td>
					<td class="lb-ht"><a href="javascript:togglebox(tblAdmin);" class="lb-ti">Administration</a></td>
				</tr>
			</table>
			<table id=tblAdmin style="visibility:hidden; position: absolute;" width=100% border=0 cellpading=0 cellspacing=0 runat=server>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkAdminSetup
							class="lb-mt"
							text="Setup"
							target=right
							runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkAdminDT
							class="lb-mt"
							text="Data Transfer"
							target=right
							runat=server />    
					</td>
				</tr>
			</table>
			

			<table width=100% cellpading=0 cellspacing=0>
				<tr><td colspan="2"><img src="images/spacer.gif" border="0" width="5" height="5"></td></tr>
			</table>

			<table width=100% cellpading=0 cellspacing=0>
				<tr height="25" >
					<td width="20" class="lb-ht"></td>
					<td width="2"></td>
					<td class="lb-ht"><a href="javascript:togglebox(tblOther);" class="lb-ti">Others</a></td>
				</tr>
			</table>
			<table id=tblOther style="visibility:hidden; position: absolute;" width=100% border=0 cellpading=0 cellspacing=0 runat=server>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkChgPwd
							class="lb-mt"
							text="Change Password"
							target=left
							runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkLogout
							class="lb-mt"
							navigateurl="/logout.aspx"
							text="Log Out"
							target=right
							runat=server />    
					</td>
				</tr>
			</table>

		</td>
	</tr>
</table>	
</form>

</body>

</html>
