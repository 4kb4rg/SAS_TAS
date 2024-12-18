<%@ Page Language="vb" Src="../../../include/PM_Setup_AcceptableOilQuality.aspx.vb" Inherits="PM_Setup_AcceptableOilQuality" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDSetup" src="../../menu/menu_PDSetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Acceptable Oil Quality Details</title>
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
			<table cellpadding="2" cellspacing="0" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="6"><UserControl:MenuPDSetup id=MenuPD runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="50%">ACCEPTABLE OIL QUALITY DETAILS</td>
					<td colspan="3" align=right width="50%"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				<tr>
					<td colspan=6><hr size="1" noshade></td>
				</tr>
				<tr>
					<td><U><B>Oil ex Pure Oil Tank</B></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Moist :*</td>
					<td width="30%"><asp:TextBox id="txtOilTankMoist" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvOilTankMoist" 
							runat="server"  
							ControlToValidate="txtOilTankMoist" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revOilTankMoist" 
							ControlToValidate="txtOilTankMoist"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvOilTankMoist"
							ControlToValidate="txtOilTankMoist"
							MinimumValue="0.01"
							MaximumValue="999"
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
					<td height=25>% Dirt :*</td>
					<td><asp:TextBox id="txtOilTankDirt" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvOilTankDirt" 
							runat="server"  
							ControlToValidate="txtOilTankDirt" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revOilTankDirt" 
							ControlToValidate="txtOilTankDirt"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvOilTankDirt"
							ControlToValidate="txtOilTankDirt"
							MinimumValue="0.01"
							MaximumValue="999"
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
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" height=5>&nbsp;</td>
				</tr>
				<tr>
					<td><U><B>Oil ex Purifier</B></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td><U><B>CPO Tank</B></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Moist at Purifier No. 1 :*</td>
					<td><asp:TextBox id="txtPurifierMoist1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierMoist1" 
							runat="server"  
							ControlToValidate="txtPurifierMoist1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierMoist1" 
							ControlToValidate="txtPurifierMoist1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierMoist1"
							ControlToValidate="txtPurifierMoist1"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% FFA ( Tank No.1 ) :*</td>
					<td><asp:TextBox id="txtCPOTankFFA1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA1" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA1" 
							ControlToValidate="txtCPOTankFFA1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA1"
							ControlToValidate="txtCPOTankFFA1"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Dirt at Purifier No. 1 :*</td>
					<td><asp:TextBox id="txtPurifierDirt1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierDirt1" 
							runat="server"  
							ControlToValidate="txtPurifierDirt1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierDirt1" 
							ControlToValidate="txtPurifierDirt1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierDirt1"
							ControlToValidate="txtPurifierDirt1"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.1 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist1" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist1" 
							ControlToValidate="txtCPOTankMoist1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist1"
							ControlToValidate="txtCPOTankMoist1"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Moist at Purifier No. 2 :*</td>
					<td><asp:TextBox id="txtPurifierMoist2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierMoist2" 
							runat="server"  
							ControlToValidate="txtPurifierMoist2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierMoist2" 
							ControlToValidate="txtPurifierMoist2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierMoist2"
							ControlToValidate="txtPurifierMoist2"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.1 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt1" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt1" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt1" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt1" 
							ControlToValidate="txtCPOTankDirt1"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt1"
							ControlToValidate="txtCPOTankDirt1"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Dirt at Purifier No. 2 :*</td>
					<td><asp:TextBox id="txtPurifierDirt2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierDirt2" 
							runat="server"  
							ControlToValidate="txtPurifierDirt2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierDirt2" 
							ControlToValidate="txtPurifierDirt2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierDirt2"
							ControlToValidate="txtPurifierDirt2"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% FFA ( Tank No.2 ) :*</td>
					<td><asp:TextBox id="txtCPOTankFFA2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA2" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA2" 
							ControlToValidate="txtCPOTankFFA2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA2"
							ControlToValidate="txtCPOTankFFA2"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Moist at Purifier No. 3 :*</td>
					<td><asp:TextBox id="txtPurifierMoist3" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierMoist3" 
							runat="server"  
							ControlToValidate="txtPurifierMoist3" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierMoist3" 
							ControlToValidate="txtPurifierMoist3"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierMoist3"
							ControlToValidate="txtPurifierMoist3"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.2 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist2" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist2" 
							ControlToValidate="txtCPOTankMoist2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist2"
							ControlToValidate="txtCPOTankMoist2"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25>% Dirt at Purifier No. 3 :*</td>
					<td><asp:TextBox id="txtPurifierDirt3" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierDirt3" 
							runat="server"  
							ControlToValidate="txtPurifierDirt3" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierDirt3" 
							ControlToValidate="txtPurifierDirt3"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierDirt3"
							ControlToValidate="txtPurifierDirt3"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.2 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt2" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt2" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt2" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt2" 
							ControlToValidate="txtCPOTankDirt2"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt2"
							ControlToValidate="txtCPOTankDirt2"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>									
				<tr>
					<td height=25>% Moist at Purifier No. 4 :*</td>
					<td><asp:TextBox id="txtPurifierMoist4" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierMoist4" 
							runat="server"  
							ControlToValidate="txtPurifierMoist4" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierMoist4" 
							ControlToValidate="txtPurifierMoist4"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierMoist4"
							ControlToValidate="txtPurifierMoist4"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% FFA ( Tank No.3 ) :*</td>
					<td><asp:TextBox id="txtCPOTankFFA3" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA3" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA3" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA3" 
							ControlToValidate="txtCPOTankFFA3"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA3"
							ControlToValidate="txtCPOTankFFA3"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Dirt at Purifier No. 4 :*</td>
					<td><asp:TextBox id="txtPurifierDirt4" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvPurifierDirt4" 
							runat="server"  
							ControlToValidate="txtPurifierDirt4" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revPurifierDirt4" 
							ControlToValidate="txtPurifierDirt4"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvPurifierDirt4"
							ControlToValidate="txtPurifierDirt4"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.3 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist3" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist3" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist3" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist3" 
							ControlToValidate="txtCPOTankMoist3"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist3"
							ControlToValidate="txtCPOTankMoist3"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.3 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt3" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt3" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt3" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt3" 
							ControlToValidate="txtCPOTankDirt3"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt3"
							ControlToValidate="txtCPOTankDirt3"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
				</tr>
				
				
				
				
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% FFA ( Tank No.4 ) :*</td>
					<td><asp:TextBox id="txtCPOTankFFA4" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA4" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA4" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA4" 
							ControlToValidate="txtCPOTankFFA4"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA4"
							ControlToValidate="txtCPOTankFFA4"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.4 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist4" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist4" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist4" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist4" 
							ControlToValidate="txtCPOTankMoist4"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist4"
							ControlToValidate="txtCPOTankMoist4"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25><U><B>Production Oil</B></U></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.4 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt4" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt4" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt4" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt4" 
							ControlToValidate="txtCPOTankDirt4"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt4"
							ControlToValidate="txtCPOTankDirt4"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% FFA :*</td>
					<td><asp:TextBox id="txtProductOilFFA" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvProductOilFFA" 
							runat="server"  
							ControlToValidate="txtProductOilFFA" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revProductOilFFA" 
							ControlToValidate="txtProductOilFFA"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvProductOilFFA"
							ControlToValidate="txtProductOilFFA"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% FFA ( Tank No.5 ) :*</td>
					<td><asp:TextBox id="txtCPOTankFFA5" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA5" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA5" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA5" 
							ControlToValidate="txtCPOTankFFA5"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA5"
							ControlToValidate="txtCPOTankFFA5"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>% Moist :*</td>
					<td><asp:TextBox id="txtProductOilMoist" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvProductOilMoist" 
							runat="server"  
							ControlToValidate="txtProductOilMoist" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revProductOilMoist" 
							ControlToValidate="txtProductOilMoist"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvProductOilMoist"
							ControlToValidate="txtProductOilMoist"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.5 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist5" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist5" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist5" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist5" 
							ControlToValidate="txtCPOTankMoist5"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist5"
							ControlToValidate="txtCPOTankMoist5"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>	
				<tr>
					<td height=25>% Dirt :*</td>
					<td><asp:TextBox id="txtProductOilDirt" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvProductOilDirt" 
							runat="server"  
							ControlToValidate="txtProductOilDirt" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revProductOilDirt" 
							ControlToValidate="txtProductOilDirt"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvProductOilDirt"
							ControlToValidate="txtProductOilDirt"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.5 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt5" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt5" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt5" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt5" 
							ControlToValidate="txtCPOTankDirt5"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt5"
							ControlToValidate="txtCPOTankDirt5"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>									
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% FFA ( Tank No.6 ) :*</td>
					<td><asp:TextBox id="txtCPOTankFFA6" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankFFA6" 
							runat="server"  
							ControlToValidate="txtCPOTankFFA6" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankFFA6" 
							ControlToValidate="txtCPOTankFFA6"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankFFA6"
							ControlToValidate="txtCPOTankFFA6"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Moist ( Tank No.6 ) :*</td>
					<td><asp:TextBox id="txtCPOTankMoist6" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankMoist6" 
							runat="server"  
							ControlToValidate="txtCPOTankMoist6" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankMoist6" 
							ControlToValidate="txtCPOTankMoist6"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankMoist6"
							ControlToValidate="txtCPOTankMoist6"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td height=25>% Dirt ( Tank No.6 ) :*</td>
					<td><asp:TextBox id="txtCPOTankDirt6" runat="server" width=50% maxlength="6"/>                       
						<asp:RequiredFieldValidator 
							id="rfvCPOTankDirt6" 
							runat="server"  
							ControlToValidate="txtCPOTankDirt6" 
							text = "Field cannot be blank"
							display="dynamic"/>
						<asp:RegularExpressionValidator id="revCPOTankDirt6" 
							ControlToValidate="txtCPOTankDirt6"
							ValidationExpression="\d{1,2}\.\d{1,2}|\d{1,2}"
							Display="Dynamic"
							text = "Maximum length 2 digits and 2 decimal places"
							runat="server"/>
						<asp:RangeValidator id="rvCPOTankDirt6"
							ControlToValidate="txtCPOTankDirt6"
							MinimumValue="0.01"
							MaximumValue="999"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
					</td>
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
