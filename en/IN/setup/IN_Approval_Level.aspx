<%@ Page Language="vb" Src="../../../include/IN_Approval_Level.aspx.vb" Inherits="IN_Approval_Level" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINSetup" src="../../menu/menu_INsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>User Level Approval</title>
		<Preference:PrefHdl id=PrefHdl runat="server" />
    <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                width: 353px;
            }
        </style>
	</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
         <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
			<asp:Label id="lblErrMessage" visible=false Text="Error while initiating component." runat="server" />
			<table cellpadding="2" cellspacing="2" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuINSetup id=MenuINSetup runat="server" />
                        <table class="style1">
                            <tr>
                                <td class="font9Tahoma"><strong>
                                    USER LEVEL APPROVAL</strong></td>
                                <td class="font9Header"  style="text-align: right">
                                    Date Created : | <asp:Label id="lblCreateDate" runat="server"/>&nbsp;| Last Update : 
                                    | <asp:Label id="lblLastUpdate" runat="server"/>&nbsp;| Updated By : <asp:Label id="lblUpdateBy" runat="server"/></td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="50%">&nbsp;</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colspan="2" align="center">AMOUNT</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>User Level</td>
					<td>From</td>
					<td>To</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>
                        Supervisor</td>
					<td width="20%" valign=center><asp:TextBox id="txtLevel1From" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel1From" 
							runat="server"  
							ControlToValidate="txtLevel1From" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel1From" 
							ControlToValidate="txtLevel1From"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
						
					</td>
					<td width="20%"><asp:TextBox id="txtLevel1To" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel1To" 
							runat="server"  
							ControlToValidate="txtLevel1To" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel1To" 
							ControlToValidate="txtLevel1To"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
						
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Manager</td>
					<td><asp:TextBox id="txtLevel2From" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel2From" 
							runat="server"  
							ControlToValidate="txtLevel2From" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel2From" 
							ControlToValidate="txtLevel2From"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
						
					</td>
					<td><asp:TextBox id="txtLevel2To" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel2To" 
							runat="server"  
							ControlToValidate="txtLevel2To" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel2To" 
							ControlToValidate="txtLevel2To"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">General Manager</td>
					<td><asp:TextBox id="txtLevel3From" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel3From" 
							runat="server"  
							ControlToValidate="txtLevel3From" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel3From" 
							ControlToValidate="txtLevel3From"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
					</td>
					<td><asp:TextBox id="txtLevel3To" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel3To" 
							runat="server"  
							ControlToValidate="txtLevel3To" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel3To" 
							ControlToValidate="txtLevel3To"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">VP/CEO</td>
					<td><asp:TextBox id="txtLevel4From" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel4From" 
							runat="server"  
							ControlToValidate="txtLevel4From" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel4From" 
							ControlToValidate="txtLevel4From"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
						
					</td>
					<td><asp:TextBox id="txtLevel4To" CssClass="font9Tahoma"  runat="server" width="70%" maxlength="18"/>                       
						<asp:RequiredFieldValidator 
							id="rfvLevel4To" 
							runat="server"  
							ControlToValidate="txtLevel4To" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revLevel4To" 
							ControlToValidate="txtLevel4To"
							ValidationExpression="\d{1,15}\.\d{1,2}|\d{1,15}"
							Display="Dynamic"
							text = "Maximum length 15 digits and 2 decimal places"
							runat="server"/>
						
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25"></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" height="25">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
					</td>
				</tr>
			</table>
                    </div>
            </td>
        </tr>
           </table  
		</form>
	</body>
</html>
