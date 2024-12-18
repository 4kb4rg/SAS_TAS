<%@ Page Language="vb" Src="../../../include/GL_trx_BudgetDetails.aspx.vb" Inherits="GL_trx_BudgetDetails" %>
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
            .style1
            {
                width: 100%;
            }
            </style>
</head>
	<body>
		<form id="frmMain" class="main-modul-bg-app-list-pu" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<asp:label id="lblPleaseSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server"/>
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table cellspacing="1" cellpadding="1" class="font9Tahoma" width="100%" border="0" id="TABLE1"" >
 				<tr>
					<td colspan="6">
					    <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong> BUDGET DETAILS</strong> </td>
                                <td class="font9Header"   style="text-align: right">
                                    Status : | <asp:Label id="lblStatus" runat="server"/>&nbsp;| Date Created : <asp:Label id="lblCreateDate" runat="server"/>&nbsp;| Last Update : <asp:Label id="lblLastUpdate" runat="server"/>&nbsp;| Update By :&nbsp; <asp:Label id="lblUpdateBy" runat="server"/></td>
                            </tr>
                        </table>
                         <hr style="width :100%" />
					</td>
				</tr>
				<tr>
					<td  colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" >&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 25px; width:25%">
                        Group COA :</td>
					<td style="height: 25px; width:45%" colspan="3">
						    <asp:DropDownList width="90%" id="ddlGroupCOA" runat="server" autopostback="false" CssClass="font9Tahoma" />				
					</td>
					<td style="height: 25px; width:15%"></td>
					<td style="height: 25px; width:15%"></td>
					
				
				<tr>
					<td style="height: 25px; width:25%">
                        <asp:label id="lblAccount" runat="server" />:</td>
					<td style="height: 25px; width:45%" colspan="3">
						    <asp:DropDownList width="90%" id="ddlAccount" onselectedindexchanged="onSelect_Account" runat="server" CssClass="font9Tahoma" />
							<asp:Label id="lblErrAccount" visible="false" forecolor="red" runat="server"/>
							 <asp:RequiredFieldValidator id="reqAccount" display="dynamic" runat="server" 
							    ErrorMessage="<br>Please Select Account" 
							    ControlToValidate="ddlAccount" />	
							</td>
					<td style="height: 25px; width:15%">&nbsp;</td>
					<td style="height: 25px; width:15%">&nbsp;</td>
					
				</tr>
				<tr>
					<td style="height:25px"><asp:label id="lblBlock" runat="server" />:</td>
					<td valign="middle" colspan="3" style="height: 25px">
					        <asp:DropDownList id="ddlBlock" width="90%" runat="server" CssClass="font9Tahoma" />
					        <asp:Label id="lblErrBlock" visible="false" forecolor="red" runat="server"/>
					</td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td style="height:25px"><asp:label id="lblVehicle" runat="server" />:</td>
					<td colspan="3">
                        <asp:Dropdownlist id="ddlVehCode" width="90%" runat="server" CssClass="font9Tahoma" />
                        <asp:Label id="lblErrVehicle" visible="false" forecolor="red" runat="server"/>
                     </td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td style="height:25px">
                        Year Of Budget:</td>
					<td style="height: 25px" colspan="3">
					    <asp:TextBox runat="server" ID="txtYearBudget" Width="25%" CssClass="font9Tahoma" > </asp:TextBox>
					    <asp:RequiredFieldValidator id="reqYear" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key Budget Year" 
							ControlToValidate="txtYearBudget" />	
					</td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">&nbsp;</td>
				</tr>
				
				<tr>
				    <td style="height:25px">
                        Total Amount:</td>
				    <td style="height: 25px" colspan="3">
					    <asp:TextBox runat="server" ID="txtAmount" Width="50%" CssClass="font9Tahoma" >0</asp:TextBox>
					    <asp:RequiredFieldValidator id="rfvAmount" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key in Amount" 
							ControlToValidate="txtAmount" />																
						<%--<asp:CompareValidator id="cvAmount" display="dynamic" runat="server" 
							ControlToValidate="txtAmount" Text="The value must greater then 0. " 
							ValueToCompare="0" Operator="GreaterThan" Type="Double"/>--%>
							<asp:RegularExpressionValidator id="revAmount" 
							ControlToValidate="txtAmount"
							ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
							<asp:Label id="lblErrAmount" visible="false" text="Amount is invalid." forecolor="red" runat="server"/>
					</td>
				    <td>&nbsp;</td>
				    <td>&nbsp;</td>
				</tr>
				
				
				<tr>
				    <td colspan="6">&nbsp;<asp:Label id="lblValueError" visible="false" forecolor="red" runat="server"/></td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%"></td>
				    <td style="height:25px; width:12%">Januari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtRp1" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp1" 
							            ControlToValidate="txtRp1"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Februari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtRp2" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp2" 
							            ControlToValidate="txtRp2"
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
				                    <asp:TextBox runat="server" id="txtRp3" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp3" 
							            ControlToValidate="txtRp3"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">April</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtRp4" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp4" 
							            ControlToValidate="txtRp4"
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
				                    <asp:TextBox runat="server" id="txtRp5" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp5" 
							            ControlToValidate="txtRp5"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Juni</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtRp6" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp6" 
							            ControlToValidate="txtRp6"
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
				                    <asp:TextBox runat="server" id="txtRp7" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp7" 
							            ControlToValidate="txtRp7"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Agustus</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtRp8" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp8" 
							            ControlToValidate="txtRp8"
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
				                    <asp:TextBox runat="server" id="txtRp9" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp9" 
							            ControlToValidate="txtRp9"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Oktober</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtRp10" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp10" 
							            ControlToValidate="txtRp10"
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
				                    <asp:TextBox runat="server" id="txtRp11" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp11" 
							            ControlToValidate="txtRp11"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Desember</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtRp12" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp12" 
							            ControlToValidate="txtRp12"
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
				    <td colspan="6" style="height: 2px">&nbsp;--------------------------------------------------------------------------------------------------------BUDGET
                        FISIK KEBUN-----------------------------------------------------------------------------------------------------------------------------</td>
				</tr>
				<tr>
				    <td colspan="6" style="height: 2px">&nbsp;</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%"></td>
				    <td style="height:25px; width:12%">Januari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS1" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS1" 
							            ControlToValidate="txtFS1"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Februari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS2" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS2" 
							            ControlToValidate="txtFS2"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">Maret</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS3" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS3" 
							            ControlToValidate="txtFS3"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">April</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS4" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS4" 
							            ControlToValidate="txtFS4"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">May</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS5" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS5" 
							            ControlToValidate="txtFS5"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Juni</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS6" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS6" 
							            ControlToValidate="txtFS6"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">Juli</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS7" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS7" 
							            ControlToValidate="txtFS7"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Agustus</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS8" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS8" 
							            ControlToValidate="txtFS8"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">September</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS9" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS9" 
							            ControlToValidate="txtFS9"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Oktober</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS10" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS10" 
							            ControlToValidate="txtFS10"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">November</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS11" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS11" 
							            ControlToValidate="txtFS11"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Desember</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtFS12" CssClass="font9Tahoma" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regFS12" 
							            ControlToValidate="txtFS12"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				
				<tr>
				    <td colspan="6" style="height: 2px">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />
						<asp:ImageButton id="btnDelete" imageurl="../../images/butt_delete.gif" visible="true" CausesValidation="false" AlternateText=" Delete " onclick="btnDelete_Click" runat="server" />
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="False" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />
					    <br />
					</td>
				</tr>
			</table>
            <input type=hidden id=hidTrxID value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
