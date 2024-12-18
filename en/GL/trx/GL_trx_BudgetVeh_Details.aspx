<%@ Page Language="vb" Src="../../../include/GL_trx_BudgetVeh_Details.aspx.vb" Inherits="GL_trx_BudgetVeh_Details" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>General Ledger - Budget Details</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
          <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <style type="text/css">

            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            .button-small {
	border: thin #009EDB solid;
	text-align:center;
	text-decoration:none;
	padding: 5px 10px 5px 10px;
	font-size: 7pt;
	font-weight:bold;
	color: #FFFFFF;
	background-color: #009EDB;
}
            </style>
</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu"  runat="server">

           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 


			<asp:label id="lblPleaseSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server"/>
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table cellspacing="1" cellpadding="1" class="font9Tahoma" width="100%" border="0" id="TABLE1"">
 				<tr>
					<td colspan="6">
					    <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td class="font9Tahoma">
                                 <strong>  BUDGET KENDARAAN DETAILS </strong> </td>
                                <td class="font9Header" style="text-align: right">
                        Date Created :
                        <asp:Label id="lblCreateDate" runat="server"/>&nbsp;|
                        Update By :
                        <asp:Label id="lblUpdateBy" runat="server"/>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
					&nbsp;
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" >&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 25px; width:25%">
                        Year Of Budget:</td>
					<td style="height: 25px; width:45%" colspan="3">
					    <asp:TextBox runat="server" ID="txtYearBudget" Width="25%" CssClass="font9Tahoma"> </asp:TextBox><asp:RequiredFieldValidator id="reqYear" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key Budget Year" 
							ControlToValidate="txtYearBudget" /></td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
					
				
				<tr>
					<td style="height: 25px; width:25%">
                        <asp:label id="lblVehicle" runat="server" />
                        :</td>
					<td style="height: 25px; width:45%" colspan="3">
                        <asp:Dropdownlist id="ddlVehCode" CssClass="font9Tahoma" width="90%" runat="server"/><asp:Label id="lblErrVehicle" visible="false" forecolor="red" runat="server"/></td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
					
				</tr>
				
				
				
				<tr>
				    <td colspan="6">&nbsp;<asp:Label id="lblValueError" visible="false" forecolor="red" runat="server"/></td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">Januari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb01" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb01" 
							            ControlToValidate="txtb01"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Februari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb02" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb02" 
							            ControlToValidate="txtb02"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">Maret</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb03" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb03" 
							            ControlToValidate="txtb03"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">April</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb04" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb04" 
							            ControlToValidate="txtb04"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">May</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb05" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb05" 
							            ControlToValidate="txtb05"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Juni</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb06" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb06" 
							            ControlToValidate="txtb06"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">Juli</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb07" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb07" 
							            ControlToValidate="txtb07"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Agustus</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb08" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb08" 
							            ControlToValidate="txtb08"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">September</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb09" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb09" 
							            ControlToValidate="txtb09"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Oktober</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb10" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb10" 
							            ControlToValidate="txtb10"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">November</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb11" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb11" 
							            ControlToValidate="txtb11"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Desember</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtb12" CssClass="font9Tahoma">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regb12" 
							            ControlToValidate="txtb12"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				
				<tr>
				    <td colspan="6" style="height: 2px">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />&nbsp;
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="False" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />
					    <br />
					</td>
				</tr>
			</table>
            <input type=hidden id=hidAccYear value="" runat=server/>
             <input type=hidden id=hidVehCode value="" runat=server/>
             </div>
             </td>
             </tr>
             </table>
		</form>
	</body>
</html>
