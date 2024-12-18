<%@ Page Language="vb" trace=false src="../../../include/PopUpFind.aspx.vb" Inherits="PopUpFind" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../preference/preference_handler.ascx"%>

<head>
    <title>Green Golden - Find</title> 
           <link href="../css/gopalms.css" rel="stylesheet" type="text/css" />
    <Script Language="Javascript">
		function keypress() {
			if (event.keyCode == 27)	//escape key press
				window.close();			//close window
		}
    </Script>
</head>
<Preference:PrefHdl id=PrefHdl runat="server" />

<body onload="javascript:self.focus();onload_setfocus();" leftmargin="2" topmargin="2">
    <form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">

    <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma" >
		    <tr>
             <td style="width: 100%; height: 1500px" valign="top" class="font9Tahoma" >
			    <div class="kontenlist"> 
		<table id=tblMain width=100% class="font9Tahoma" runat=server>
			<tr>
				<td class="mt-h">FIND</td>
				<td></td>
			</tr>
			<tr>
				<td colspan=2><hr style="width:100%">
                    </td>
			</tr>
			<tr visible=false id=trAcc>
				<td width=40%><asp:Label id="lblChartOfAccount" runat="server" />&nbsp;Code : </td>
				<td width=60%>
					<asp:TextBox id=txtAccCode width=100% onkeypress="javascript:keypress()" runat=server/>
					<asp:RequiredFieldValidator id=validateCompCode display=dynamic runat=server 
						ErrorMessage="<br>Please enter account code. " 
						ControlToValidate=txtAccCode />
					<asp:RegularExpressionValidator id=revAccCode 
						ControlToValidate="txtAccCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,32}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trBlk>
				<td width=40%><asp:Label id="lblBlock" runat="server" />&nbsp;Code : </td>
				<td width=60%>
					<asp:TextBox id=txtBlkCode width=50% maxlength=8 onkeypress="javascript:keypress()" runat=server />
					<asp:RegularExpressionValidator id=revBlkCode 
						ControlToValidate="txtBlkCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,8}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trSubBlk>
				<td width=40%><asp:Label id="lblSubBlock" runat="server" />&nbsp;Code : </td>
				<td width=60%>
					<asp:TextBox id=txtSubBlkCode width=50% maxlength=8 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revSubBlkCode 
						ControlToValidate="txtSubBlkCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,8}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trVeh>
				<td width=40%><asp:Label id="lblVehicle" runat="server" />&nbsp;Code : </td>
				<td width=60%>
					<asp:TextBox id=txtVehCode width=50% maxlength=8 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revVehCode 
						ControlToValidate="txtVehCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,8}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trVehExp>
				<td width=40%><asp:Label id="lblVehicleExpense" runat="server" />&nbsp;Code : </td>
				<td width=60%>
					<asp:TextBox id=txtVehExpCode width=50% maxlength=8 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revVehExpCode 
						ControlToValidate="txtVehExpCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,8}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trEmp>
				<td width=40%>Employee Code : </td>
				<td width=60%>
					<asp:TextBox id=txtEmpCode width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
				
					
				</td>
			</tr>
			<tr visible=false id=trEmpName>
				<td width=40%>Employee Name : </td>
				<td width=60%>
					<asp:TextBox id=txtEmpName width=100% maxlength=64 onkeypress="javascript:keypress()" runat=server/>
					<asp:DropDownList id=lstEmpCode Visible=False runat=server />
				</td>
			</tr>
			<tr visible=false id=trINItem>
				<td width=40%>Inventory Item Code : </td>
				<td width=60%>
					<asp:TextBox id=txtINItemCode width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revINItemCode 
						ControlToValidate="txtINItemCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trCTItem>
				<td width=40%>Canteen Item Code : </td>
				<td width=60%>
					<asp:TextBox id=txtCTItemCode width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
					<asp:RequiredFieldValidator id=validateCTItemCode display=dynamic runat=server 
						ErrorMessage="<br>Please enter canteen item code. " 
						ControlToValidate=txtCTItemCode />
					<asp:RegularExpressionValidator id=revCTItemCode 
						ControlToValidate="txtCTItemCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trWSItem>
				<td width=40%>Workshop Item Code : </td>
				<td width=60%>
					<asp:TextBox id=txtWSItemCode width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revWSItemCode 
						ControlToValidate="txtWSItemCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trWSItemPart>
				<td width=40%>Item Part No : </td>
				<td width=60%>
					<asp:TextBox id=txtItemPartNo width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
					<asp:RegularExpressionValidator id=revItemPartNo 
						ControlToValidate="txtWSItemCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trDCItem>
				<td width=40%>Direct Charge Item Code : </td>
				<td width=60%>
					<asp:TextBox id=txtDCItemCode width=100% maxlength=20 onkeypress="javascript:keypress()" runat=server/>
					<asp:RequiredFieldValidator id=validateDCItemCode display=dynamic runat=server 
						ErrorMessage="<br>Please enter direct charge item code. " 
						ControlToValidate=txtDCItemCode />
					<asp:RegularExpressionValidator id=revDCItemCode 
						ControlToValidate="txtDCItemCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,20}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr visible=false id=trAD>
				<td width=40%>Allowance & Deduction Code : </td>
				<td width=60%>
					<asp:TextBox id=txtADCode width=80% maxlength=8 onkeypress="javascript:keypress()" runat=server/>
					<asp:RequiredFieldValidator id=validateADCode display=dynamic runat=server 
						ErrorMessage="<br>Please enter allowance and deduction code. " 
						ControlToValidate=txtADCode />
					<asp:RegularExpressionValidator id=revADCode 
						ControlToValidate="txtADCode"
						ValidationExpression="[a-zA-Z0-9\-]{1,8}"
						Display="Dynamic"
						text="<br>Alphanumeric without any space in between only."
						runat="server"/>
				</td>
			</tr>
			<tr>
				<td></td>
				<td>
					<asp:Label id=lblErrWSCode visible=false forecolor=red text="Enter either Item Code or Item Part No.<br>" runat=server/>
					<asp:Label id=lblErrEmpCode visible=false forecolor=red text="Enter either Employee Code or Employee Name.<br>" runat=server/>
					
					<asp:ImageButton id=ibConfirm alternatetext="Confirm" imageurl="../../images/butt_confirm.gif" runat=server/> 
					<input type=image src="../../images/butt_cancel.gif" alt=Cancel onclick="javascript:window.close();" width="58" height="20">
				</td>
			</tr>
		</table>
                </div>
            </td>
            </tr>
            </table>
    </form>
</body>
</html>
