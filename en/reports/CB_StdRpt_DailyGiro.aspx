<%@ Page Language="vb" src="../../include/reports/CB_StdRpt_DailyGiro.aspx.vb" Inherits="CB_StdRpt_DailyGiro" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../include/preference/preference_handler.ascx"%>
<%@ Register TagPrefix="UserControl" Tagname="CB_STDRPT_SELECTION_CTRL" src="../include/reports/CB_StdRpt_Selection_Ctrl.ascx"%>
<HTML>
	<HEAD>
		<title>Cash And Bank - Daily Giro Transaction</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
                  <link href="../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form runat="server" class="main-modul-bg-app-list-pu" ID="frmMain">

        <table cellpadding="0" cellspacing="0" style="width: 100%"  class="font9Tahoma">
		<tr>
           <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 

			<table border="0" cellspacing="1" cellpadding="1" width="100%" ID="Table1" class="font9Tahoma">
				<tr>
					<td  colspan="3"><strong> CASH AND BANK - DAILY GIRO/CHEQUE TRANSACTION</strong></td>
					<td align="right" colspan="3"><asp:label id="lblTracker" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6"><UserControl:CB_StdRpt_Selection_Ctrl id="RptSelect" runat="server" /></td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>			
				<tr>
					<td colspan="6">
                        <hr style="width :100%" /> 
                    </td>
				</tr>
			</table>
			
			<table width=100% border="0" cellspacing="1" cellpadding="1" ID="Table2" class="font9Tahoma" runat=server>	
				
				<tr>
					<td width ="20%">Date:</td>
					<td colspan ="2">    
						<asp:Textbox id="txtDate" width="15%" maxlength=10 CssClass="fontObject" runat=server/>
						<a href="javascript:PopCal('txtDate');"><asp:Image id="btnDate" runat="server" ImageUrl="../images/calendar.gif"/></a>
					</td>					
											
				</tr>	
			
				<tr>
					<td></td>
					<td></td>					
					<td></td>							
				</tr>	

				<tr>
				    <td width="15%">Bank/Cash :</td>
                    <td width="30%">    
                        <asp:DropDownList width="95%" id=ddlBank CssClass="fontObject" runat=server /><asp:Label id=lblErrBank forecolor=red visible=false text="Please select Bank Code"  runat=server/>&nbsp;</td>				    				        
				    <td>&nbsp;</td>	
					<td>&nbsp;</td>	
			    </tr>
			    <tr>
					<td width ="20%">Range Amount:</td>
					<td colspan="2">    
					 <asp:Textbox id="txtGrpAmount" text = "0" width="25%" maxlength=32 CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator 
									id="RegularExpressionValidator2" 
									ControlToValidate="txtGrpAmount"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
						&nbsp; to &nbsp;
						<asp:Textbox id="txtGrpAmountTo" text = "0" width="25%" maxlength=32 runat=server/>
					    <asp:RegularExpressionValidator 
									id="RegularExpressionValidator1" 
									ControlToValidate="txtGrpAmountTo"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
					</td>					
				</tr>	
                <tr>
                    <td width="20%">
                        Trans ID :</td>
                    <td colspan="2">
                        <asp:Textbox id="txtTransIdFr" width="25%" maxlength=32 CssClass="fontObject" runat=server/>
                        &nbsp; to &nbsp;
                        <asp:Textbox id="txtTransIdTo" width="25%" maxlength=32 CssClass="fontObject" runat=server/></td>
                </tr>
				<tr>
					<td colspan="3" style="height: 21px"> </td>		
				</tr>
			    <tr>
					<td colspan="3">
                       <asp:CheckBox id="cbExcel" text=" Export To Excel" checked="false" runat="server" /></td>
				</tr>
				
				<tr>
					<td colspan="3">
						<asp:Label id=lblErrDate visible=false forecolor=red runat=server/>
					</td>			
				</tr>
				<tr>
					<td colspan="3"> </td>		
				</tr>
				<tr>
					<td colspan="3"><asp:ImageButton id="PrintPrev" AlternateText="Print Preview" imageurl="../images/butt_print_preview.gif" onClick="btnPrintPrev_Click" runat="server" /></td>						
				</tr>				
			</table>
           </div>
           </td>
           </tr>
           </table>
		</form>
		<asp:Label id="lblErrMessage" visible="false" text="Error while initiating component." runat="server" />
		
	</body>
</HTML>
