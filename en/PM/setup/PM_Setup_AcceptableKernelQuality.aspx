<%@ Page Language="vb" Src="../../../include/PM_Setup_AcceptableKernelQuality.aspx.vb" Inherits="PM_Setup_AcceptableKernelQuality" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="../../menu/menu_PDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Acceptable Kernel Quality Details</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
	</head>
	<body>
		<form id="frmMain" runat="server" class="main-modul-bg-app-list-pu">

        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1200px" valign="top">
			    <div class="kontenlist">


			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id="blnUpdate" runat="server" Visible="False"/>
			<table cellpadding="2" cellspacing="2" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuPDSetup id=MenuPD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="50%">ACCEPTABLE KERNEL QUALITY DETAILS</td>
					<td colspan="3" align=right width="50%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td><U><H5>Dry Separator Line No. 1</H5></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><B>KERNEL DIRT</B></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% BK :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtDryBK1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryBK1" 
							runat="server"  
							ControlToValidate="txtDryBK1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryBK1" 
							ControlToValidate="txtDryBK1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryBK1"
							ControlToValidate="txtDryBK1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td width=15%>Status :</td>
					<td width=25%><asp:Label id="lblStatus" runat="server"/></td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% L Shell :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtDryLShell1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryLShell1" 
							runat="server"  
							ControlToValidate="txtDryLShell1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryLShell1" 
							ControlToValidate="txtDryLShell1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryLShell1"
							ControlToValidate="txtDryLShell1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:Label id="lblCreateDate" runat="server"/>
						<asp:Label id="txtCreateDate" runat="server" Visible="False"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /WN :*</td>
					<td><asp:TextBox id="txtDryShellWN1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryShellWN1" 
							runat="server"  
							ControlToValidate="txtDryShellWN1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryShellWN1" 
							ControlToValidate="txtDryShellWN1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryShellWN1"
							ControlToValidate="txtDryShellWN1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /BN :*</td>
					<td><asp:TextBox id="txtDryShellBN1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryShellBN1" 
							runat="server"  
							ControlToValidate="txtDryShellBN1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryShellBN1" 
							ControlToValidate="txtDryShellBN1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryShellBN1"
							ControlToValidate="txtDryShellBN1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Total Dirt :*</td>
					<td><asp:TextBox id="txtDryTotalDirt1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryTotalDirt1" 
							runat="server"  
							ControlToValidate="txtDryTotalDirt1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryTotalDirt1" 
							ControlToValidate="txtDryTotalDirt1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryTotalDirt1"
							ControlToValidate="txtDryTotalDirt1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>FLOW RATE :*</td>
					<td><asp:TextBox id="txtDryFlowRate1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryFlowRate1" 
							runat="server"  
							ControlToValidate="txtDryFlowRate1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryFlowRate1" 
							ControlToValidate="txtDryFlowRate1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryFlowRate1"
							ControlToValidate="txtDryFlowRate1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>					
				<tr>
					<td colspan="6" height=5>&nbsp;</td>
				</tr>							
				<tr>
					<td><U><H5>Dry Separator Line No. 2</H5></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><B>KERNEL DIRT</B></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% BK :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtDryBK2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryBK2" 
							runat="server"  
							ControlToValidate="txtDryBK2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryBK2" 
							ControlToValidate="txtDryBK2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryBK2"
							ControlToValidate="txtDryBK2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% L Shell :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtDryLShell2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryLShell2" 
							runat="server"  
							ControlToValidate="txtDryLShell2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryLShell2" 
							ControlToValidate="txtDryLShell2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryLShell2"
							ControlToValidate="txtDryLShell2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /WN :*</td>
					<td><asp:TextBox id="txtDryShellWN2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryShellWN2" 
							runat="server"  
							ControlToValidate="txtDryShellWN2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryShellWN2" 
							ControlToValidate="txtDryShellWN2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryShellWN2"
							ControlToValidate="txtDryShellWN2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /BN :*</td>
					<td><asp:TextBox id="txtDryShellBN2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryShellBN2" 
							runat="server"  
							ControlToValidate="txtDryShellBN2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryShellBN2" 
							ControlToValidate="txtDryShellBN2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryShellBN2"
							ControlToValidate="txtDryShellBN2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Total Dirt :*</td>
					<td><asp:TextBox id="txtDryTotalDirt2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryTotalDirt2" 
							runat="server"  
							ControlToValidate="txtDryTotalDirt2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryTotalDirt2" 
							ControlToValidate="txtDryTotalDirt2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryTotalDirt2"
							ControlToValidate="txtDryTotalDirt2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>FLOW RATE :*</td>
					<td><asp:TextBox id="txtDryFlowRate2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvDryFlowRate2" 
							runat="server"  
							ControlToValidate="txtDryFlowRate2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revDryFlowRate2" 
							ControlToValidate="txtDryFlowRate2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvDryFlowRate2"
							ControlToValidate="txtDryFlowRate2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="6" height=5>&nbsp;</td>
				</tr>					
				<tr>
					<td><U><H5>Claybath / Hydro Cyclone 1</H5></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><B>KERNEL DIRT</B></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% BK :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtClayBathBK1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathBK1" 
							runat="server"  
							ControlToValidate="txtClayBathBK1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathBK1" 
							ControlToValidate="txtClayBathBK1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathBK1"
							ControlToValidate="txtClayBathBK1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% L Shell :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtClayBathlshell1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathlshell1" 
							runat="server"  
							ControlToValidate="txtClayBathlshell1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathlshell1" 
							ControlToValidate="txtClayBathlshell1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathlshell1"
							ControlToValidate="txtClayBathlshell1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /WN :*</td>
					<td><asp:TextBox id="txtClayBathShellWN1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathShellWN1" 
							runat="server"  
							ControlToValidate="txtClayBathShellWN1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathShellWN1" 
							ControlToValidate="txtClayBathShellWN1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathShellWN1"
							ControlToValidate="txtClayBathShellWN1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /BN :*</td>
					<td><asp:TextBox id="txtClayBathShellBN1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathShellBN1" 
							runat="server"  
							ControlToValidate="txtClayBathShellBN1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathShellBN1" 
							ControlToValidate="txtClayBathShellBN1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathShellBN1"
							ControlToValidate="txtClayBathShellBN1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Total Dirt :*</td>
					<td><asp:TextBox id="txtClayBathTotalDirt1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathTotalDirt1" 
							runat="server"  
							ControlToValidate="txtClayBathTotalDirt1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathTotalDirt1" 
							ControlToValidate="txtClayBathTotalDirt1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathTotalDirt1"
							ControlToValidate="txtClayBathTotalDirt1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>FLOW RATE :*</td>
					<td><asp:TextBox id="txtClayBathFlowRate1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathFlowRate1" 
							runat="server"  
							ControlToValidate="txtClayBathFlowRate1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathFlowRate1" 
							ControlToValidate="txtClayBathFlowRate1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathFlowRate1"
							ControlToValidate="txtClayBathFlowRate1"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="6" height=5>&nbsp;</td>
				</tr>				
				<tr>
					<td><U><H5>Claybath / Hydro Cyclone 2</H5></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td><B>KERNEL DIRT</B></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% BK :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtClayBathBK2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathBK2" 
							runat="server"  
							ControlToValidate="txtClayBathBK2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathBK2" 
							ControlToValidate="txtClayBathBK2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathBK2"
							ControlToValidate="txtClayBathBK2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td width=5%>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td width=5%>&nbsp;</td>
				</tr>
				<tr>
					<td width="20%" height=25>% L Shell :*</td>
					<td width="30%" valign=center><asp:TextBox id="txtClayBathlshell2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathlshell2" 
							runat="server"  
							ControlToValidate="txtClayBathlshell2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathlshell2" 
							ControlToValidate="txtClayBathlshell2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathlshell2"
							ControlToValidate="txtClayBathlshell2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /WN :*</td>
					<td><asp:TextBox id="txtClayBathShellWN2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathShellWN2" 
							runat="server"  
							ControlToValidate="txtClayBathShellWN2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathShellWN2" 
							ControlToValidate="txtClayBathShellWN2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathShellWN2"
							ControlToValidate="txtClayBathShellWN2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Shell /BN :*</td>
					<td><asp:TextBox id="txtClayBathShellBN2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathShellBN2" 
							runat="server"  
							ControlToValidate="txtClayBathShellBN2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathShellBN2" 
							ControlToValidate="txtClayBathShellBN2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathShellBN2"
							ControlToValidate="txtClayBathShellBN2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Total Dirt :*</td>
					<td><asp:TextBox id="txtClayBathTotalDirt2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathTotalDirt2" 
							runat="server"  
							ControlToValidate="txtClayBathTotalDirt2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathTotalDirt2" 
							ControlToValidate="txtClayBathTotalDirt2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathTotalDirt2"
							ControlToValidate="txtClayBathTotalDirt2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>FLOW RATE :*</td>
					<td><asp:TextBox id="txtClayBathFlowRate2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvClayBathFlowRate2" 
							runat="server"  
							ControlToValidate="txtClayBathFlowRate2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revClayBathFlowRate2" 
							ControlToValidate="txtClayBathFlowRate2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvClayBathFlowRate2"
							ControlToValidate="txtClayBathFlowRate2"
							MinimumValue="0.01"
							MaximumValue="99.99"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" height=25>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
						<asp:ImageButton id="Save" imageurl="../../images/butt_save.gif" onclick="btnSave_Click" runat="server" AlternateText="Save"/>
						<asp:ImageButton id="Back" imageurl="../../images/butt_back.gif" onclick="btnBack_Click" runat="server" AlternateText="Back" CausesValidation="False"/>
					</td>
				</tr>
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
