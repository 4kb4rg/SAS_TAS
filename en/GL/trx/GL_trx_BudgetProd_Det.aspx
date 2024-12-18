<%@ Page Language="vb" Src="../../../include/GL_trx_BudgetProd_Det.aspx.vb" Inherits="GL_trx_BudgetProd_Det" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>General Ledger - Budget Details</title>
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

			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server"/>
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table cellspacing="1" cellpadding="1" class="font9Tahoma" width="100%" border="0" id="TABLE1"">
 				<tr>
					<td class="mt-h" colspan="7"><table cellpadding="0" 
                            cellspacing="0" class="style1">
                        <tr>
                            <td class="font9Tahoma">
                               <strong>BUDGET PRODUKSI DETAILS</strong> </td>
                            <td class="font9Header"  style="text-align: right">
                        Create Date&nbsp; :
                        <asp:Label id="lblCreateDate" runat="server"/>&nbsp;|
                        Create By &nbsp;&nbsp; &nbsp; :
                        <asp:Label id="lblUpdateBy" runat="server"/></td>
                        </tr>
                        </table>
                        <hr style="width :100%" />
                    </td>
				</tr>				
				<tr>
					<td colspan="7" >&nbsp;</td>
				</tr>				
				<tr>
					<td colspan="2" style="height: 25px">
                        Year Of Budget :</td>
					<td colspan="2" style="height: 25px">
                        &nbsp;<asp:TextBox runat="server" ID="txtYearBudget" MaxLength="4"></asp:TextBox><asp:RequiredFieldValidator id="reqYear" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key Budget Year" 
							ControlToValidate="txtYearBudget" /></td>
					<td colspan="2" style="height:25px">
                        &nbsp;</td>
					<td style="height: 25px; width:15%"></td>
					</tr>
				
				<tr>
					<td  colspan="4" style="height:25px">&nbsp;</td>
					
					<td colspan="2" style="height: 25px">
                        &nbsp;</td>
					<td style="height: 25px"></td>
					
				</tr>
				<tr>
					<td colspan="7" style="height:25px">&nbsp;</td>
				
				</tr>
				<tr>
					<td style="height: 25px"></td>
					<td colspan="3" align="center">BUDGET</td>
					<td colspan="3" style="height: 25px" align="center">REALISASI</td>
				</tr>
				<tr>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px" align="left">PALM</td>
					<td style="height: 25px" align="left">KERNEL</td>
					<td style="height: 25px" align="left">TBS</td>
					<td style="height: 25px" align="left">PALM</td>
					<td style="height: 25px" align="left">KERNEL</td>
					<td style="height: 25px" align="left">TBS</td>
				</tr>				
				
				
				<tr>
				    <td style="height:25px; width:5%">Januari</td>
				   
				    <td style="height:25px; width:5%">
				                    <asp:TextBox runat="server" id="txtB01P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB01P" 
							            ControlToValidate="txtB01P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:5%">
				                    <asp:TextBox runat="server" id="txtB01K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB01K" 
							            ControlToValidate="txtB01K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:5%">
				                    <asp:TextBox runat="server" id="txtB01T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB01T" 
							            ControlToValidate="txtB01T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:5%">
				                    <asp:TextBox runat="server" id="txtR01P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR01P" 
							            ControlToValidate="txtR01P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:5%">
				                    <asp:TextBox runat="server" id="txtR01K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR01K" 
							            ControlToValidate="txtR01K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:5%">
				                    <asp:TextBox runat="server" id="txtR01T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR01T" 
							            ControlToValidate="txtR01T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		            
				</tr>
				
				
				
				<tr>
				    <td style="height:25px; width:16%">
                        Febuari</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB02P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB02P" 
							            ControlToValidate="txtB02P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB02K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB02K" 
							            ControlToValidate="txtB02K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB02T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB02T" 
							            ControlToValidate="txtB02T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR02P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR02P" 
							            ControlToValidate="txtR02P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR02K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR02K" 
							            ControlToValidate="txtR02K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR02T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR02T" 
							            ControlToValidate="txtR02T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				
				<tr>
				    <td style="height:25px; width:16%">
                        Maret</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB03P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB03P" 
							            ControlToValidate="txtB03P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB03K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB03K" 
							            ControlToValidate="txtB03K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB03T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB03T" 
							            ControlToValidate="txtB03T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR03P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regr03P" 
							            ControlToValidate="txtR03P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR03K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR03K" 
							            ControlToValidate="txtR03K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR03T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR03T" 
							            ControlToValidate="txtR03T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:25px; width:16%">
                        April</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB04P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB04P" 
							            ControlToValidate="txtB04P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB04K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB04K" 
							            ControlToValidate="txtB04K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB04T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB04T" 
							            ControlToValidate="txtB04T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR04P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR04P" 
							            ControlToValidate="txtR04P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR04K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR04K" 
							            ControlToValidate="txtR04K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR04T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR04T" 
							            ControlToValidate="txtR04T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				
				<tr>
				    <td style="height:25px; width:16%">
                        Mei</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB05P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB05P" 
							            ControlToValidate="txtB05P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB05K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB05K" 
							            ControlToValidate="txtB05K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB05T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB05T" 
							            ControlToValidate="txtB05T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR05P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="reg05P" 
							            ControlToValidate="txtR05P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR05K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR05K" 
							            ControlToValidate="txtR05K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR05T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR05T" 
							            ControlToValidate="txtR05T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:25px; width:16%">
                        Juni</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB06P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB06P" 
							            ControlToValidate="txtB06P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB06K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB06K" 
							            ControlToValidate="txtB06K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB06T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB06T" 
							            ControlToValidate="txtB06T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR06P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR06P" 
							            ControlToValidate="txtR06P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR06K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR06K" 
							            ControlToValidate="txtR06K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR06T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR06T" 
							            ControlToValidate="txtR06T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:25px; width:16%">
                        Juli</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB07P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB07P" 
							            ControlToValidate="txtB07P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB07K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="rgB07K" 
							            ControlToValidate="txtB07K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB07T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB07T" 
							            ControlToValidate="txtB07T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR07P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR07P" 
							            ControlToValidate="txtR07P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR07K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR07K" 
							            ControlToValidate="txtR07K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR07T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR07T" 
							            ControlToValidate="txtR07T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:25px; width:16%">
                        Agustus</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB08P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB08P" 
							            ControlToValidate="txtB08P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB08K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB08K" 
							            ControlToValidate="txtB08K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB08T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB08T" 
							            ControlToValidate="txtB08T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR08P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR08P" 
							            ControlToValidate="txtR08P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR08K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR08K" 
							            ControlToValidate="txtR08K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR08T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR08T" 
							            ControlToValidate="txtR08T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:11px; width:16%">
                        September</td>
				   
				    <td style="height:11px; width:12%">
				                    <asp:TextBox runat="server" id="txtB09P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB09P" 
							            ControlToValidate="txtB09P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:11px; width:12%">
				                    <asp:TextBox runat="server" id="txtB09K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB09K" 
							            ControlToValidate="txtB09K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:11px; width:12%">
				                    <asp:TextBox runat="server" id="txtB09T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB09T" 
							            ControlToValidate="txtB09T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:11px; width:12%">
				                    <asp:TextBox runat="server" id="txtR09P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR09P" 
							            ControlToValidate="txtR09P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:11px; width:12%">
				                    <asp:TextBox runat="server" id="txtR09K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR09K" 
							            ControlToValidate="txtR09K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:11px; width:12%">
				                    <asp:TextBox runat="server" id="txtR09T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR09T" 
							            ControlToValidate="txtR09T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:25px; width:16%">
                        Oktober</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB10P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB10P" 
							            ControlToValidate="txtB10P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB10K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB10K" 
							            ControlToValidate="txtB10K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB10T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB10T" 
							            ControlToValidate="txtB10T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR10P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR10P" 
							            ControlToValidate="txtR10P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR10K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR10K" 
							            ControlToValidate="txtR10K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR10T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR10T" 
							            ControlToValidate="txtR10T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:25px; width:16%">
                        November</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB11P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB11P" 
							            ControlToValidate="txtB11P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB11K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB11K" 
							            ControlToValidate="txtB11K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB11T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB11T" 
							            ControlToValidate="txtB11T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR11P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR11P" 
							            ControlToValidate="txtR11P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR11K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR11K" 
							            ControlToValidate="txtR11K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR11T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR11T" 
							            ControlToValidate="txtR11T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				<tr>
				    <td style="height:25px; width:16%">
                        Desember</td>
				   
				    <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB12P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="rgB12P" 
							            ControlToValidate="txtB12P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB12K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB12K" 
							            ControlToValidate="txtB12K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtB12T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regB12T" 
							            ControlToValidate="txtB12T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR12P" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR12P" 
							            ControlToValidate="txtR12P"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR12K" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR12K" 
							            ControlToValidate="txtR12K"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             <td style="height:25px; width:12%">
				                    <asp:TextBox runat="server" id="txtR12T" style="width:56%">0</asp:TextBox>
				                       <asp:RegularExpressionValidator id="regR12T" 
							            ControlToValidate="txtR12T"
							            ValidationExpression="^(\-|)\d{1,11}(\.\d{1,2}|\.|)$"
							            Display="Dynamic"
							            text = "Max 11 digits and 2 decimal"
							            runat="server"/>
		            </td>
		             
				</tr>
				
				
				
				
				
				<tr>
				    <td colspan="7" style="height: 2px">&nbsp;<asp:Label id="lblValueError" visible="false" forecolor="red" runat="server"/></td>
				</tr>
				
				<tr>
					<td colspan="7">
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="False" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />
					    <br />
					</td>
				</tr>
			</table>
            <input type=hidden id=hidAccYear value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
