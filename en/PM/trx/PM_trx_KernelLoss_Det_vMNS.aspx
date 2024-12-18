<%@ Page Language="vb" Inherits="PM_KernelLoss_Det" Src="../../../include/PM_trx_KernelLoss_Det.aspx.vb"%>
<%@ Register TagPrefix="UserControl" Tagname="MenuPDTrx" src="../../menu/menu_PDtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<HTML>
	<HEAD>
		<title>KERNEL LOSS TRANSACTION</title> 
		<PREFERENCE:PREFHDL id="PrefHdl" runat="server"></PREFERENCE:PREFHDL>
	</HEAD>
	<body>
		<form id="frmMain" runat="server">
			<asp:label id="lblErrMessage" runat="server" visible="false" Text="Error while initiating component."></asp:label><asp:label id="blnUpdate" runat="server" Visible="False"></asp:label>
			<table cellSpacing="0" cellPadding="2" width="100%" border="0">
				<tr>
					<td colSpan="6"><USERCONTROL:MENUPDTRX id="MenuPDTrx" runat="server"></USERCONTROL:MENUPDTRX></td>
				</tr>
				<tr>
					<td class="mt-h" colSpan="3">KERNEL LOSS DETAILS</td>
					<td align="right" colSpan="2"><asp:label id="lblTracker" runat="server"></asp:label></td>
					<td><asp:label id="lblId" runat="server" visible = False></asp:label></td>
				</tr>
				<tr>
					<td colSpan="6">
						<hr noShade SIZE="1">
					</td>
				</tr>
				<tr>
					<td width="20%" height="25">Transaction Date :*
					</td>
					<td width="30%"><asp:textbox id="txtdate" runat="server" width="70%" maxlength="20"></asp:textbox><A href="javascript:PopCal('txtdate');"><asp:image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"></asp:image></A><br>
						<asp:requiredfieldvalidator id="rfvDate" runat="server" ControlToValidate="txtdate" text="Field cannot be blank"
							display="dynamic"></asp:requiredfieldvalidator><asp:label id="lblDate" Text="Date Entered should be in the format " Visible="false" forecolor="red"
							Runat="server"></asp:label><asp:label id="lblFmt" Visible="false" forecolor="red" Runat="server"></asp:label></td>
					<td width="5%">&nbsp;</td>
					<td width="10%">Period :
					</td>
					<td width="20%"><asp:label id="lblPeriod" runat="server"></asp:label></td>
					<td width="5%">&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Test Sample Code :*
					</td>
					<td><asp:dropdownlist id="ddlTestSampleCode" runat="server" width="100%" maxlength="20"></asp:dropdownlist>
					<asp:requiredfieldvalidator id="rfvTestSampleCode" runat="server" ControlToValidate="ddlTestSampleCode" text="Please select a test sample code."
							display="dynamic">Please select a test sample code.</asp:requiredfieldvalidator></td>
					<td>&nbsp;</td>
					<td>Status :</td>
					<td><asp:label id="lblStatus" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Station :*
					</td>
					<td>
						<asp:dropdownlist id="ddlStation" runat="server" width="100%" OnSelectedIndexChanged="ddlStation_SelectedIndexChanged"
							AutoPostBack="True"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvStation" runat="server" ControlToValidate="ddlStation" EnableViewState="False"
							ErrorMessage="Please select a Station." Display="Dynamic"></asp:requiredfieldvalidator>
					</td>
					<td>&nbsp;</td>
					<td>Date Created :</td>
					<td><asp:label id="lblCreateDate" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="25">Machine :*
					</td>
					<td>
						<asp:dropdownlist id="ddlMachine" runat="server" width="100%" OnSelectedIndexChanged="ddlMachine_SelectedIndexChanged"
							AutoPostBack="True"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvMachine" runat="server" ControlToValidate="ddlMachine" EnableViewState="False"
							ErrorMessage="Please select a Machine." Display="Dynamic"></asp:requiredfieldvalidator>
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:label id="lblLastUpdate" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr id="tr1" runat="server">
					<td height="25"><asp:label id="lbl1" runat="server" Visible="False"></asp:label></td>
					<td><asp:textbox id="txt1" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt1" runat="server" ControlToValidate="txt1" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt1" runat="server" ControlToValidate="txt1" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt1" runat="server" ControlToValidate="txt1" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></td>
					<td><asp:label id="lblType1" runat="server" Visible=False></asp:label></td>
					<td>Updated By :</td>
					<td><asp:label id="lblUpdateBy" runat="server"></asp:label></td>
					<td>&nbsp;</td>
				</tr>
				
				<TR id="tr2" runat="server">
					<TD><asp:label id="lbl2" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt2" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt2" runat="server" ControlToValidate="txt2" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt2" runat="server" ControlToValidate="txt2" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt2" runat="server" ControlToValidate="txt2" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType2" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr3" runat="server">
					<TD><asp:label id="lbl3" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt3" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt3" runat="server" ControlToValidate="txt3" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt3" runat="server" ControlToValidate="txt3" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt3" runat="server" ControlToValidate="txt3" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType3" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr4" runat="server">
					<TD><asp:label id="lbl4" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt4" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt4" runat="server" ControlToValidate="txt4" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt4" runat="server" ControlToValidate="txt4" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt4" runat="server" ControlToValidate="txt4" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType4" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr5" runat="server">
					<TD><asp:label id="lbl5" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt5" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt5" runat="server" ControlToValidate="txt5" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt5" runat="server" ControlToValidate="txt5" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt5" runat="server" ControlToValidate="txt5" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType5" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr6" runat="server">
					<TD><asp:label id="lbl6" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt6" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt6" runat="server" ControlToValidate="txt6" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt6" runat="server" ControlToValidate="txt6" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt6" runat="server" ControlToValidate="txt6" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType6" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr7" runat="server">
					<TD><asp:label id="lbl7" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt7" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt7" runat="server" ControlToValidate="txt7" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt7" runat="server" ControlToValidate="txt7" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt7" runat="server" ControlToValidate="txt7" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType7" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr8" runat="server">
					<TD><asp:label id="lbl8" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt8" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt8" runat="server" ControlToValidate="txt8" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt8" runat="server" ControlToValidate="txt8" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt8" runat="server" ControlToValidate="txt8" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType8" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr9" runat="server" >
					<TD><asp:label id="lbl9" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt9" maxlength="11" runat="server"></asp:textbox><br>
						<asp:RequiredFieldValidator id="rfvTxt9" runat="server" ControlToValidate="txt9" Enabled="False" ErrorMessage="Field cannot be blank."
							Display="Dynamic"></asp:RequiredFieldValidator>
						<asp:regularexpressionvalidator id="revTxt9" runat="server" ControlToValidate="txt9" Enabled="False" ErrorMessage="Maximum of 8 digits and 2 decimals allowed."
							Display="Dynamic" ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator>
						<asp:rangevalidator id="rvTxt9" runat="server" ControlToValidate="txt9" Enabled="False" ErrorMessage=" The value is out of range."
							Display="Dynamic" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType9" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr10" runat="server" >
					<TD><asp:label id="lbl10" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt10" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt10" runat="server" ControlToValidate="txt10" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt10" runat="server" ControlToValidate="txt10" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt10" runat="server" ControlToValidate="txt10" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType10" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr11" runat="server" >
					<TD><asp:label id="lbl11" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt11" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt11" runat="server" ControlToValidate="txt11" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt11" runat="server" ControlToValidate="txt11" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt11" runat="server" ControlToValidate="txt11" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType11" runat="server" Visible=False></asp:label></TD>
				</TR>
				<TR id="tr12" runat="server" >
					<TD><asp:label id="lbl12" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt12" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt12" runat="server" ControlToValidate="txt12" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt12" runat="server" ControlToValidate="txt12" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt12" runat="server" ControlToValidate="txt12" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType12" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr13" runat="server" >
					<TD><asp:label id="lbl13" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt13" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt13" runat="server" ControlToValidate="txt13" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt13" runat="server" ControlToValidate="txt13" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt13" runat="server" ControlToValidate="txt13" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType13" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr14" runat="server" >
					<TD><asp:label id="lbl14" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt14" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt14" runat="server" ControlToValidate="txt14" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt14" runat="server" ControlToValidate="txt14" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt14" runat="server" ControlToValidate="txt14" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType14" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr15" runat="server" >
					<TD><asp:label id="lbl15" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt15" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt15" runat="server" ControlToValidate="txt15" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt15" runat="server" ControlToValidate="txt15" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt15" runat="server" ControlToValidate="txt15" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType15" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr16" runat="server" >
					<TD><asp:label id="lbl16" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt16" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt16" runat="server" ControlToValidate="txt16" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt16" runat="server" ControlToValidate="txt16" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt16" runat="server" ControlToValidate="txt16" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType16" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr17" runat="server" >
					<TD><asp:label id="lbl17" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt17" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt17" runat="server" ControlToValidate="txt17" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt17" runat="server" ControlToValidate="txt17" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt17" runat="server" ControlToValidate="txt17" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType17" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr18" runat="server" >
					<TD><asp:label id="lbl18" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt18" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt18" runat="server" ControlToValidate="txt18" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt18" runat="server" ControlToValidate="txt18" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt18" runat="server" ControlToValidate="txt18" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType18" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr19" runat="server" >
					<TD><asp:label id="lbl19" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt19" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt19" runat="server" ControlToValidate="txt19" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt19" runat="server" ControlToValidate="txt19" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt19" runat="server" ControlToValidate="txt19" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType19" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr20" runat="server" >
					<TD><asp:label id="lbl20" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt20" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt20" runat="server" ControlToValidate="txt20" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt20" runat="server" ControlToValidate="txt20" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt20" runat="server" ControlToValidate="txt20" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType20" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr21" runat="server" >
					<TD><asp:label id="lbl21" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt21" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt21" runat="server" ControlToValidate="txt21" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt21" runat="server" ControlToValidate="txt21" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt21" runat="server" ControlToValidate="txt21" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType21" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr22" runat="server" >
					<TD><asp:label id="lbl22" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt22" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt22" runat="server" ControlToValidate="txt22" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt22" runat="server" ControlToValidate="txt22" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt22" runat="server" ControlToValidate="txt22" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType22" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr23" runat="server" >
					<TD><asp:label id="lbl23" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt23" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt23" runat="server" ControlToValidate="txt23" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt23" runat="server" ControlToValidate="txt23" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt23" runat="server" ControlToValidate="txt23" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType23" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr24" runat="server" >
					<TD><asp:label id="lbl24" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt24" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt24" runat="server" ControlToValidate="txt24" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt24" runat="server" ControlToValidate="txt24" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt24" runat="server" ControlToValidate="txt24" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType24" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<TR id="tr25" runat="server" >
					<TD><asp:label id="lbl25" runat="server" Visible="False"></asp:label></TD>
					<TD colSpan="4"><asp:textbox id="txt25" maxlength="11" runat="server"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvTxt25" runat="server" ControlToValidate="txt25" Display="Dynamic" ErrorMessage="Field cannot be blank."
							Enabled="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTxt25" runat="server" ControlToValidate="txt25" Display="Dynamic" Enabled="False"
							ErrorMessage="Maximum of 8 digits and 2 decimals allowed." ValidationExpression="\d{1,8}\.\d{1,2}|\d{1,8}"></asp:regularexpressionvalidator><asp:rangevalidator id="rvTxt25" runat="server" ControlToValidate="txt25" Display="Dynamic" ErrorMessage=" The value is out of range."
							Enabled="False" MaximumValue="99999999.99" MinimumValue="0" Type="Double"></asp:rangevalidator></TD>
					<TD><asp:label id="lblType25" runat="server" Visible=False></asp:label></TD>
				</TR>
				
				<tr>
					<td colSpan="6" height="25"><asp:label id="lblDupMsg" Text="Production for selected date, test sample code, station and machine already exist."
							Visible="false" forecolor="red" Runat="server" /></td>
				</tr>
				<tr>
					<td colSpan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="6">
					<asp:imagebutton id="Save" onclick="btnSave_Click" runat="server" AlternateText="Save" imageurl="../../images/butt_save.gif"></asp:imagebutton>
					<asp:imagebutton id="Delete" onclick="btnDelete_Click" runat="server" Visible="False" AlternateText="Delete" imageurl="../../images/butt_delete.gif" CausesValidation="False"></asp:imagebutton>
					<asp:imagebutton id="btnNew" onclick="btnNew_Click" runat="server" AlternateText="Add New" imageurl="../../images/butt_new.gif" CausesValidation="False"></asp:imagebutton>
					<asp:imagebutton id="Back" onclick="btnBack_Click" runat="server" AlternateText="Back" imageurl="../../images/butt_back.gif" CausesValidation="False"></asp:imagebutton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
