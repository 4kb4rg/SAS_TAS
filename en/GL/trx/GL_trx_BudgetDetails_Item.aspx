<%@ Page Language="vb" Src="../../../include/GL_trx_BudgetDetails_Item.aspx.vb" Inherits="GL_trx_BudgetDetails_Item" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>General Ledger - Budget Pemakaian Bahan Details</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
             <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .style1
            {
                width: 100%;
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
					    <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                <strong>   BUDGET DETAILS</strong> </td>
                                <td class="font9Header"  style="text-align: right">
                                    Status : <asp:Label id="lblStatus" runat="server"/>&nbsp;| Date Created : <asp:Label id="lblCreateDate" runat="server"/>&nbsp;| Last Update : <asp:Label id="lblLastUpdate" runat="server"/>&nbsp;| Update By : <asp:Label id="lblUpdateBy" runat="server"/>
                                </td>
                            </tr>
                        </table>
                        <hr style="width :100%" />
					</td>
				</tr>
				<tr>
					<td class="mt-h" colspan="4">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" >&nbsp;</td>
				</tr>
				<%--<tr>
					<td style="height: 25px; width:25%">
                        Group COA :</td>
					<td style="height: 25px; width:45%" colspan="3">
						    <asp:DropDownList width="90%" id="ddlGroupCOA" runat="server" autopostback="false" />				
					</td>
					<td style="height: 25px; width:15%"></td>
					<td style="height: 25px; width:15%"></td>--%>
					
				
				<tr>
					<td style="height: 25px; width:25%">
                        <asp:label id="lblAccount" runat="server" />:</td>
					<td style="height: 25px; width:45%" colspan="3">
						    <asp:DropDownList width="90%" id="ddlAccount"  autopostback="false" Enabled="False" runat="server"/>
							<asp:Label id="lblErrAccount" visible="false" forecolor="red" runat="server"/>
							 <asp:RequiredFieldValidator id="reqAccount" display="dynamic" runat="server" 
							    ErrorMessage="<br>Please Select Account" 
							    ControlToValidate="ddlAccount" />	
							</td>
					<td style="height: 25px; width:15%">&nbsp;</td>
					<td style="height: 25px; width:15%">&nbsp;</td>
					
				</tr>
				<tr>
					<td style="height:25px">Item Code :</td>
					<td valign="middle" colspan="3" style="height: 25px">
                        <asp:DropDownList ID="ddlItem" runat="server" AutoPostBack="false" Enabled="False" EnableViewState="True" Width="86%">
                        </asp:DropDownList>&nbsp;
                        <%--<input id="Find" runat="server" causesvalidation="False" onclick="javascript:PopItem('frmMain', '', 'ddlItem', 'false');"  type="button" value=" ... " />--%>
							<asp:Label id="lblErrBlock" visible="false" forecolor="red" runat="server"/>
					</td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td style="height:25px">
                        Year Of Budget :</td>
					<td colspan="3">
					    <asp:TextBox runat="server" ID="txtYearBudget" Width="25%" Enabled="False" CssClass="font9Tahoma"> </asp:TextBox><asp:RequiredFieldValidator id="reqYear" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key Budget Year" 
							ControlToValidate="txtYearBudget" /></td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td style="height:25px">
                        Total Amount :</td>
					<td style="height: 25px" colspan="3">
					    <asp:TextBox CssClass="font9Tahoma" runat="server" ID="txtAmount" Width="50%" onkeypress="return isNumberKey(event)" >0</asp:TextBox>
				    	<asp:ImageButton id="btnGenAmount" imageurl="../../images/butt_generate.gif" AlternateText="  Generate  " onclick="btnGenAmount_Click" runat="server" />&nbsp;
				
						<asp:RequiredFieldValidator id="rfvAmount" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key in Amount" 
							ControlToValidate="txtAmount" /><asp:RegularExpressionValidator id="revAmount" 
							ControlToValidate="txtAmount"
							ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 19 digits and 2 decimal points. "
							runat="server"/>
						<asp:Label id="lblErrAmount" visible="false" text="Amount is invalid." forecolor="red" runat="server"/>
							
					</td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td style="height:25px">
                        Total Fisik :</td>
					<td style="height: 25px" colspan="3">
					    <asp:TextBox CssClass="font9Tahoma" runat="server" ID="txtTtlFisik" Width="50%" onkeypress="return isNumberKey(event)">0</asp:TextBox><asp:RequiredFieldValidator id="rfvFisik" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key in Total Fisik" 
							ControlToValidate="txtTtlFisik" /><asp:RegularExpressionValidator id="revFisik" 
							ControlToValidate="txtTtlFisik"
							ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							Display="Dynamic"
							text = "Maximum length 21 digits and 5 decimal points. "
							runat="server"/><asp:Label id="Label1" visible="false" text="Amount is invalid." forecolor="red" runat="server"/></td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">&nbsp;</td>
				</tr>		

				<tr>
					<td style="height:25px">
                        Divisi / T.Tanam :</td>
					<td style="height: 25px" colspan="3">
					    <asp:TextBox runat="server" ID="txtDiv" Width="22%" Enabled="False" runat="server" CssClass="font9Tahoma"/> / <asp:TextBox runat="server" ID="txtTT" Width="25%" Enabled="False" runat="server"/>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">&nbsp;</td>
				</tr>							
							
				<tr>
				    <td colspan="6">&nbsp;<asp:Label id="lblValueError" visible="false" forecolor="red" runat="server"/></td>
				</tr>
				<tr>
					<td style="height:25px"></td>
					<td colspan="3" style="text-decoration: underline">
                        BUDGET BIAYA</td>
					<td style="text-decoration: underline">
                        BUDGET FISIK</td>
					<td></td>
				</tr>
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">Januari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" CssClass="font9Tahoma" id="txtRp1b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp1b" 
							            ControlToValidate="txtRp1b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Januari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp1f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp1f" 
							            ControlToValidate="txtRp1f"
							           ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Februari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp2b" onkeypress="return isNumberKey(event)" >0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp2b" 
							            ControlToValidate="txtRp2b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Febuari</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp2f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp2f" 
							            ControlToValidate="txtRp2f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Maret</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp3b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp3b" 
							            ControlToValidate="txtRp3b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Maret</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp3f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp3f" 
							            ControlToValidate="txtRp3f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        April</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp4b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp4b" 
							            ControlToValidate="txtRp4b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        April</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp4f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp4f" 
							            ControlToValidate="txtRp4f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        May</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp5b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp5b" 
							            ControlToValidate="txtRp5b"
							           ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        May</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp5f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp5f" 
							            ControlToValidate="txtRp5f"
							           ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Juni</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp6b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp6b" 
							            ControlToValidate="txtRp6b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Juni</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp6f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp6f" 
							            ControlToValidate="txtRp6f"
							           ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>			
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        July</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp7b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp7b" 
							            ControlToValidate="txtRp7b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        July</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp7f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp7f" 
							            ControlToValidate="txtRp7f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Agustus</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp8b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp8b" 
							            ControlToValidate="txtRp8b"
							           ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Agustus</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp8f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp8f" 
							            ControlToValidate="txtRp8f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        September</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp9b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp9b" 
							            ControlToValidate="txtRp9b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$" Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        September</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp9f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp9f" 
							            ControlToValidate="txtRp9f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Oktober</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp10b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp10b" 
							            ControlToValidate="txtRp10b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Oktober</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp10f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp10f" 
							            ControlToValidate="txtRp10f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        November</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp11b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp11b" 
							            ControlToValidate="txtRp11b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        November</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp11f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp11f" 
							            ControlToValidate="txtRp11f"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,5}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 5 decimal"
							            runat="server"/>
					</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:25%">&nbsp;</td>
				    <td style="height:25px; width:12%">
                        Desember</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp12b" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp12b" 
							            ControlToValidate="txtRp12b"
							            ValidationExpression="^(\-|)\d{1,21}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 21 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            <td style="height:25px; width:5%">&nbsp;</td>
				    <td style="height:25px; width:12%">Desember</td>
				    <td style="height:25px; width:12%">
				                    <asp:TextBox CssClass="font9Tahoma" runat="server" id="txtRp12f" onkeypress="return isNumberKey(event)">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regRp12f" 
							            ControlToValidate="txtRp12f"
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
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />&nbsp;
						<asp:ImageButton id="btnDelete" imageurl="../../images/butt_delete.gif" visible="true" CausesValidation="false" AlternateText=" Delete " onclick="btnDelete_Click" runat="server" />
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="False" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />
					    <br />
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
