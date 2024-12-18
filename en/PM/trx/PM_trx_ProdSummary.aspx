<%@ Page Language="vb" trace="false" src="../../../include/PM_trx_ProdSummary.aspx.vb" Inherits="PM_trx_ProdSummary" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuGLSetup" src="../../menu/menu_pdtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Production Summary</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<script language="javascript">	
        function calSATBS() {
	        var doc = document.frmMain;
	        var a = parseFloat(doc.Textbox15.value);
	        var b = parseFloat(doc.Textbox16.value);				
	        var c = parseFloat(doc.Textbox17.value);	
	        var d = parseFloat(doc.Textbox18.value);	
	        var e = parseFloat(doc.Textbox19.value);
	        var f = parseFloat(doc.Textbox20.value);	
	        var g = parseFloat(doc.Textbox22.value);	
	        var h = parseFloat(doc.Textbox24.value);		
	        doc.Textbox25.value = a+b+c+d+e+f+g-h;
	        if (doc.Textbox25.value == 'NaN')
		        doc.Textbox25.value = '';
	        else
		        doc.Textbox25.value = round(doc.Textbox25.value, 2);
        }	
        function calSACPO() {
	        var doc = document.frmMain;
	        var a = parseFloat(doc.Textbox26.value);
	        var b = parseFloat(doc.Textbox27.value);				
	        var c = parseFloat(doc.Textbox28.value);
	        var d = parseFloat(doc.Textbox29.value);	
			var e = parseFloat(doc.Textbox30.value);	
	        doc.Textbox31.value = a+b+c-d+e;
	        if (doc.Textbox31.value == 'NaN') 
		        doc.Textbox31.value = '';
	        else
		        doc.Textbox31.value = round(doc.Textbox31.value, 2);
        }		
        function calSACPO31() {
	        var doc = document.frmMain;
	        var a = parseFloat(doc.Textbox38.value);
	        var b = parseFloat(doc.Textbox39.value);				
	        var c = parseFloat(doc.Textbox40.value);	
	        doc.Textbox41.value = a+b-c;
	        if (doc.Textbox41.value == 'NaN') 
		        doc.Textbox41.value = '';
	        else
		        doc.Textbox41.value = round(doc.Textbox41.value, 2);
        }		
        function calSACPO32() {
	        var doc = document.frmMain;
	        var a = parseFloat(doc.Textbox50.value);
	        var b = parseFloat(doc.Textbox51.value);				
	        var c = parseFloat(doc.Textbox52.value);	
			var d = parseFloat(doc.Textbox53.value);	
	        doc.Textbox54.value = a-b+c-d;
	        if (doc.Textbox54.value == 'NaN') 
		        doc.Textbox54.value = '';
	        else
		        doc.Textbox54.value = round(doc.Textbox54.value, 2);
        }		
        
        function calSAPK() {
	        var doc = document.frmMain;
	        var a = parseFloat(doc.Textbox33.value);
	        var b = parseFloat(doc.Textbox34.value);				
	        var c = parseFloat(doc.Textbox35.value);
	        var d = parseFloat(doc.Textbox36.value);	
			var e = parseFloat(doc.Textbox37.value);	
	        doc.Textbox38.value = a+b+c-d+e;
	        if (doc.Textbox38.value == 'NaN') 
		        doc.Textbox38.value = '';
	        else
		        doc.Textbox38.value = round(doc.Textbox38.value, 2);
        }		
        function calSAPK31() {
	        var doc = document.frmMain;
	        var a = parseFloat(doc.Textbox43.value);
	        var b = parseFloat(doc.Textbox44.value);				
	        var c = parseFloat(doc.Textbox45.value);	
	        doc.Textbox46.value = a+b-c;
	        if (doc.Textbox46.value == 'NaN') 
		        doc.Textbox46.value = '';
	        else
		        doc.Textbox46.value = round(doc.Textbox46.value, 2);
        }	
        function calSAPK32() {
	        var doc = document.frmMain;
	        var a = parseFloat(doc.Textbox55.value);
	        var b = parseFloat(doc.Textbox56.value);				
	        var c = parseFloat(doc.Textbox57.value);
			var d = parseFloat(doc.Textbox58.value);	
	        doc.Textbox59.value = a-b+c-d;
	        if (doc.Textbox59.value == 'NaN') 
		        doc.Textbox59.value = '';
	        else
		        doc.Textbox59.value = round(doc.Textbox59.value, 2);
        }	      	                
	</script>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu" runat="server">

        <table cellpadding="0" cellspacing="0" style="width: 100%"  >
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">  

			<asp:Label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
			<asp:Label id=lblHiddenSts visible=false text="1" runat=server/>
			<asp:label id=lblCode visible=false text=" Code" runat=server />
			<asp:label id=lblSelect visible=false text="Select " runat=server />
			<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
			<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
			    <tr>
					<td colspan="6"><UserControl:MenuGLSetup id=MenuGLSetup runat="server" /></td>
				</tr>
				<tr>
					<td class="mt-h" colspan="6">PRODUCTION SUMMARY</td>
				</tr>
				<tr>
					<td colspan=6>&nbsp;</td>
				</tr>
				
				<tr>
				    <td height=25 width="20%">Year :</td>
				    <td width="30%"><asp:TextBox ID=txtYear maxlength=4 width=70% Runat=server /></td>
				    <td width="5%">&nbsp;</td>
				    <td width="15%">Status : </td>
					<td width="30%"><asp:Label id=lblStatus runat=server /></td>
			    </tr>
				<tr>
					<td height=25>Month</td>
					<td width=30%><asp:DropDownList id=ddlMonth width=70% AutoPostBack=true OnSelectedIndexChanged="GetData" CssClass="fontObject" runat=server>
								        <asp:ListItem value="0" Selected>All</asp:ListItem>
						                <asp:ListItem value="1">Januari</asp:ListItem>
						                <asp:ListItem value="2">Februari</asp:ListItem>
						                <asp:ListItem value="3">Maret</asp:ListItem>
						                <asp:ListItem value="4">April</asp:ListItem>
						                <asp:ListItem value="5">Mei</asp:ListItem>
						                <asp:ListItem value="6">Juni</asp:ListItem>
						                <asp:ListItem value="7">Juli</asp:ListItem>
						                <asp:ListItem value="8">Agustus</asp:ListItem>
						                <asp:ListItem value="9">September</asp:ListItem>
						                <asp:ListItem value="10">Oktober</asp:ListItem>
						                <asp:ListItem value="11">November</asp:ListItem>
						                <asp:ListItem value="12">Desember</asp:ListItem>
					                </asp:DropDownList>
					<asp:Label id=lblErrPeriode visible=false forecolor=red text=" Please select periode." runat=server />                
					</td>
					<td>&nbsp;</td>
					<td>Date Created : </td>
					<td><asp:Label id=lblDateCreated runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Last Update : </td>
					<td><asp:Label id=lblLastUpdate runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height=25>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Updated By : </td>
					<td><asp:Label id=lblUpdatedBy runat=server /></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
					<td class="mt-h">I. Pengiriman TBS</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25><asp:Label id=Label9 text="PMKS Unit Sendiri" Font-Bold=true Font-Italic=true runat=server /></td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label text="Asal TBS 1" runat=server /></td>
					<td><asp:DropDownList id=ddlAsalTBS1 width=70% CssClass="fontObject" runat=server />
					    <asp:Label id=lblErrAsalTBS1 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label text="Asal TBS 2" runat=server /></td>
					<td><asp:DropDownList id=ddlAsalTBS2 width=70% CssClass="fontObject" runat=server />
					    <asp:Label id=lblErrAsalTBS2 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr1 text="Kuantiti (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox1 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator3 												
                            ControlToValidate="Textbox1"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox1"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo1 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo1 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label id=LabelDescr3 text="Kuantiti (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox3 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator21 												
                            ControlToValidate="Textbox3"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator21" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox3"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo3 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo3 text="" Visible=false runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr2 text="Harga (Rp)" runat=server /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>*sesuai harga papan PMKS/Disbun</i></td>
					<td><asp:Textbox id=Textbox2 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator8 												
                            ControlToValidate="Textbox2"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox2"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo2 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo2 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label id=LabelDescr4 text="Harga (Rp)" runat=server /><br /><i>*sesuai harga papan PMKS</i></td>
					<td><asp:Textbox id=Textbox4 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator22 												
                            ControlToValidate="Textbox4"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator22" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox4"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo4 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo4 text="" Visible=false runat=server />
					</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label54" text="Asal TBS 3" runat=server /></td>
					<td><asp:DropDownList id=ddlAsalTBS3 width=70% CssClass="fontObject" runat=server />
					    <asp:Label id=lblErrAsalTBS3 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="Label56" text="Asal TBS 4" runat=server /></td>
					<td><asp:DropDownList id=ddlAsalTBS4 width=70% runat=server />
					    <asp:Label id=lblErrAsalTBS4 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr5 text="Kuantiti (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox5 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator52 												
                            ControlToValidate="Textbox5"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator33" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox5"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo5 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo5 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label id=LabelDescr7 text="Kuantiti (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox7 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator53 												
                            ControlToValidate="Textbox7"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator34" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox7"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo7 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo7 text="" Visible=false runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr6 text="Harga (Rp)" runat=server /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>*sesuai harga papan PMKS/Disbun</i></td>
					<td><asp:Textbox id=Textbox6 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator54 												
                            ControlToValidate="Textbox6"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator35" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox6"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo6 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo6 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label id=LabelDescr8 text="Harga (Rp)" runat=server /><br /><i>*sesuai harga papan PMKS</i></td>
					<td><asp:Textbox id=Textbox8 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator55 												
                            ControlToValidate="Textbox8"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator36" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox8"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo8 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo8 text="" Visible=false runat=server />
					</td>
				</tr>
				
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td height=25><asp:Label id=Label7 text="PMKS Pihak Ketiga" Font-Bold=true Font-Italic=true runat=server /></td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="LabelDescr9" text="PMKS I" runat=server /></td>
					<td><asp:Textbox id=Textbox9 width=70% CssClass="fontObject" runat=server/>
					    <asp:Label id=lblRefNo9 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="LabelDescr12" text="PMKS II" runat=server /></td>
					<td><asp:Textbox id=Textbox12 width=70% CssClass="fontObject" runat=server/>
					    <asp:Label id=lblRefNo12 text="" Visible=false runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr10 text="Kuantiti (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox10 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator23 												
                            ControlToValidate="Textbox10"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator23" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox10"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo10 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo10 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label id=LabelDescr13 text="Kuantiti (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox13 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator24 												
                            ControlToValidate="Textbox13"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator24" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox13"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo13 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo13 text="" Visible=false runat=server />
					</td>
				</tr>
				<tr>
					<td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr11 text="Harga (Rp) " runat=server /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>*sesuai harga papan PMKS/Disbun</i></td>
					<td><asp:Textbox id=Textbox11 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator25 												
                            ControlToValidate="Textbox11"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator25" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox11"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo11 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo11 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label id=LabelDescr14 text="Harga (Rp) " runat=server /><br /><i>*sesuai harga papan PMKS</i></td>
					<td><asp:Textbox id=Textbox14 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator26 												
                            ControlToValidate="Textbox14"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator26" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox14"
                            Text="Please enter Pengiriman TBS" />
                        <asp:Label id=lblErrNo14 visible=false forecolor=red text=" Please enter Pengiriman TBS." runat=server />
                        <asp:Label id=lblRefNo14 text="" Visible=false runat=server />
					</td>
				</tr>
				
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
					<td class="mt-h">II. Tandan Buah Segar (TBS)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
			    <tr>
				    <td height=25><asp:Label id=LabelDescr15 text="Saldo Awal TBS (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox15 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator13 												
                            ControlToValidate="Textbox15"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox15"
                            Text="Please enter Saldo Awal TBS" />
                        <asp:Label id=lblErrNo15 visible=false forecolor=red text=" Please enter Saldo Awal TBS." runat=server />
                        <asp:Label id=lblRefNo15 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25><asp:Label id=Label34 text="TBS Terima (Kg)" runat=server /></td>
				</tr>
			    <tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr16 text="Kuantiti 1" runat=server /></td>
					<td><asp:Textbox id=Textbox16 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator2 												
                            ControlToValidate="Textbox16"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox16"
                            Text="Please enter TBS Terima" />
                        <asp:Label id=lblErrNo16 visible=false forecolor=red text=" Please enter TBS Terima." runat=server />
                        <asp:Label id=lblRefNo16 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="Label27" text="Asal TBS 1" runat=server /></td>
					<td><asp:DropDownList id=ddlTerimaTBS1 width=70% CssClass="fontObject" runat=server />
					    <asp:Label id=lblErrTerimaTBS1 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr17 text="Kuantiti 2" runat=server /></td>
					<td><asp:Textbox id=Textbox17 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator27 												
                            ControlToValidate="Textbox17"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator27" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox17"
                            Text="Please enter TBS Terima" />
                        <asp:Label id=lblErrNo17 visible=false forecolor=red text=" Please enter TBS Terima." runat=server />
                        <asp:Label id=lblRefNo17 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="Label29" text="Asal TBS 2" runat=server /></td>
					<td><asp:DropDownList id=ddlTerimaTBS2 width=70% CssClass="fontObject" runat=server />
					    <asp:Label id=lblErrTerimaTBS2 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr18 text="Kuantiti 3" runat=server /></td>
					<td><asp:Textbox id=Textbox18 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator50 												
                            ControlToValidate="Textbox18"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator31" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox18"
                            Text="Please enter TBS Terima" />
                        <asp:Label id=lblErrNo18 visible=false forecolor=red text=" Please enter TBS Terima." runat=server />
                        <asp:Label id=lblRefNo18 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="Label24" text="Asal TBS 3" runat=server /></td>
					<td><asp:DropDownList id=ddlTerimaTBS3 width=70% CssClass="fontObject" runat=server />
					    <asp:Label id=lblErrTerimaTBS3 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr19 text="Kuantiti 4" runat=server /></td>
					<td><asp:Textbox id=Textbox19 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator51 												
                            ControlToValidate="Textbox13"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator32" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox19"
                            Text="Please enter TBS Terima" />
                        <asp:Label id=lblErrNo19 visible=false forecolor=red text=" Please enter TBS Terima." runat=server />
                        <asp:Label id=lblRefNo19 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="Label38" text="Asal TBS 4" runat=server /></td>
					<td><asp:DropDownList id=ddlTerimaTBS4 width=70% CssClass="fontObject" runat=server />
					    <asp:Label id=lblErrTerimaTBS4 visible=false forecolor=red text=" Please select estate location." runat=server />
					</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr20 text="Kuantiti 5" runat=server /></td>
					<td><asp:Textbox id=Textbox20 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator28 												
                            ControlToValidate="Textbox20"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator28" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox20"
                            Text="Please enter TBS Terima" />
                        <asp:Label id=lblErrNo20 visible=false forecolor=red text=" Please enter TBS Terima." runat=server />
                        <asp:Label id=lblRefNo20 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="LabelDescr21" text="Asal TBS 5" runat=server /></td>
					<td><asp:Textbox id=Textbox21 width=70% CssClass="fontObject" runat=server/>
					    <asp:Label id=lblRefNo21 text="" Visible=false runat=server />
					</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr22 text="Kuantiti 6" runat=server /></td>
					<td><asp:Textbox id=Textbox22 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator29 												
                            ControlToValidate="Textbox22"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator29" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox22"
                            Text="Please enter TBS Terima" />
                        <asp:Label id=lblErrNo22 visible=false forecolor=red text=" Please enter TBS Terima." runat=server />
                        <asp:Label id=lblRefNo22 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td><asp:Label ID="LabelDescr23" text="Asal TBS 6" runat=server /></td>
					<td><asp:Textbox id=Textbox23 width=70% CssClass="fontObject" runat=server/>
					    <asp:Label id=lblRefNo23 text="" Visible=false runat=server />
					</td>
				</tr>
				<tr>
				    <td height=25><asp:Label id=LabelDescr24 text="TBS Olah (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox24 width=70% Text="0" OnKeyUp="javascript:calSATBS();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator11 												
                            ControlToValidate="Textbox24"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox24"
                            Text="Please enter TBS Olah" />
                        <asp:Label id=lblErrNo24 visible=false forecolor=red text=" Please enter TBS Olah." runat=server />
                        <asp:Label id=lblRefNo24 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25><asp:Label id=LabelDescr25 text="Saldo Akhir TBS (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox25 width=70% Text="0" readonly=true style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator14 												
                            ControlToValidate="Textbox25"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox25"
                            Text="Please enter Saldo Akhir TBS" />
                        <asp:Label id=lblErrNo25 visible=false forecolor=red text=" Please enter Saldo Akhir TBS." runat=server />
                        <asp:Label id=lblRefNo25 text="TBS" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
					<td class="mt-h">III. Crude Palm Oil (CPO)</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td class="mt-h">IV. Palm Kernel (PK)</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25><asp:Label id=Label10 text="PMKS Unit Sendiri" Font-Bold=true Font-Italic=true runat=server /></td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr26 text="Saldo Awal CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox26 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator6 												
                            ControlToValidate="Textbox26"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox26"
                            Text="Please enter saldo awal CPO" />
                        <asp:Label id=lblErrNo26 visible=false forecolor=red text=" Please enter saldo awal CPO." runat=server />
                        <asp:Label id=lblRefNo26 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr33 text="Saldo Awal PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox33 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator7 												
                            ControlToValidate="Textbox33"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox33"
                            Text="Please enter saldo awal PK" />
                        <asp:Label id=lblErrNo33 visible=false forecolor=red text=" Please enter saldo awal PK." runat=server />
                        <asp:Label id=lblRefNo33 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr27 text="Produksi CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox27 width=70% Text="0" OnKeyUp="javascript:calSACPO();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator9 												
                            ControlToValidate="Textbox27"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox27"
                            Text="Please enter Produksi CPO" />
                        <asp:Label id=lblErrNo27 visible=false forecolor=red text=" Please enter Produksi CPO." runat=server />
                        <asp:Label id=lblRefNo27 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr34 text="Produksi PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox34 width=70% Text="0" OnKeyUp="javascript:calSAPK();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator4 												
                            ControlToValidate="Textbox34"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox34"
                            Text="Please enter Produksi PK" />
                        <asp:Label id=lblErrNo34 visible=false forecolor=red text=" Please enter Produksi PK." runat=server />
                        <asp:Label id=lblRefNo34 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr28 text="Pembelian CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox28 width=70% Text="0" OnKeyUp="javascript:calSACPO();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator56 												
                            ControlToValidate="Textbox28"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator37" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox28"
                            Text="Please enter Produksi CPO" />
                        <asp:Label id=lblErrNo28 visible=false forecolor=red text=" Please enter Pembelian CPO." runat=server />
                        <asp:Label id=lblRefNo28 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr35 text="Pembelian PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox35 width=70% Text="0" OnKeyUp="javascript:calSAPK();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator57 												
                            ControlToValidate="Textbox35"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator38" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox35"
                            Text="Please enter Produksi PK" />
                        <asp:Label id=lblErrNo35 visible=false forecolor=red text=" Please enter Pembelian PK." runat=server />
                        <asp:Label id=lblRefNo35 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr29 text="Dispatch/Sales CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox29 width=70% Text="0" OnKeyUp="javascript:calSACPO();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator15 												
                            ControlToValidate="Textbox29"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator15" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox29"
                            Text="Please enter Pengiriman CPO" />
                        <asp:Label id=lblErrNo29 visible=false forecolor=red text=" Please enter CPO Dispatch." runat=server />
                        <asp:Label id=lblRefNo29 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr36 text="Dispatch/Sales PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox36 width=70% Text="0" OnKeyUp="javascript:calSAPK();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator16 												
                            ControlToValidate="Textbox36"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator16" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox36"
                            Text="Please enter Pengiriman PK" />
                        <asp:Label id=lblErrNo36 visible=false forecolor=red text=" Please enter PK Dispatch." runat=server />
                        <asp:Label id=lblRefNo36 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr30 text="Muai(Susut) CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox30 width=70% Text="0" OnKeyUp="javascript:calSACPO();" style="text-align:right" CssClass="fontObject" runat=server/>
					   
                        <asp:RequiredFieldValidator id="RequiredFieldValidator17" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox30"
                            Text="Please enter Pengiriman CPO" />
                        <asp:Label id=lblErrNo30 visible=false forecolor=red text=" Please enter CPO Transit." runat=server />
                        <asp:Label id=lblRefNo30 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr37 text="Muai(Susut) PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox37 width=70% Text="0" OnKeyUp="javascript:calSAPK();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator18 												
                            ControlToValidate="Textbox37"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator18" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox37"
                            Text="Please enter Pengiriman PK" />
                        <asp:Label id=lblErrNo37 visible=false forecolor=red text=" Please enter PK Transit." runat=server />
                        <asp:Label id=lblRefNo37 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr31 text="Saldo Akhir CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox31 width=70% Text="0" style="text-align:right" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator5 												
                            ControlToValidate="Textbox31"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox31"
                            Text="Please enter TBS Olah" />
                        <asp:Label id=lblErrNo31 visible=false forecolor=red text=" Please enter Saldo Akhir CPO." runat=server />
                        <asp:Label id=lblRefNo31 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr38 text="Saldo Akhir PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox38 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator12 												
                            ControlToValidate="Textbox38"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox38"
                            Text="Please enter TBS Olah" />
                        <asp:Label id=lblErrNo38 visible=false forecolor=red text=" Please enter Saldo Akhir PK." runat=server />
                        <asp:Label id=lblRefNo38 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
                <tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr32 text="Titip Olah CPO Pihak Luar (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox32 width=70% Text="0" OnKeyUp="javascript:calSACPO();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator10 												
                            ControlToValidate="Textbox32"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox32"
                            Text="Please enter Titip Olah CPO" />
                        <asp:Label id=lblErrNo32 visible=false forecolor=red text=" Please enter Titip Olah CPO Pihak Luar." runat=server />
                        <asp:Label id=lblRefNo32 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr39 text="Titip Olah PK Pihak Luar (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox39 width=70% Text="0" OnKeyUp="javascript:calSAPK();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator1 												
                            ControlToValidate="Textbox39"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox39"
                            Text="Please enter Titip Olah PK" />
                        <asp:Label id=lblErrNo39 visible=false forecolor=red text=" Please enter Titip Olah PK Pihak Luar." runat=server />
                        <asp:Label id=lblRefNo39 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
				    <td height=25><asp:Label id=Label13 text="PMKS Pihak Ketiga" Font-Bold=true Font-Italic=true runat=server /></td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr40 text="Saldo Awal CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox40 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator30 												
                            ControlToValidate="Textbox40"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator6a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox40"
                            Text="Please enter saldo awal CPO" />
                        <asp:Label id=lblErrNo40 visible=false forecolor=red text=" Please enter saldo awal CPO." runat=server />
                        <asp:Label id=lblRefNo40 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr45 text="Saldo Awal PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox45 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator31 												
                            ControlToValidate="Textbox45"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator7a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox45"
                            Text="Please enter saldo awal PK" />
                        <asp:Label id=lblErrNo45 visible=false forecolor=red text=" Please enter saldo awal PK." runat=server />
                        <asp:Label id=lblRefNo45 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr41 text="Produksi CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox41 width=70% Text="0" OnKeyUp="javascript:calSACPO31();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator32 												
                            ControlToValidate="Textbox41"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator9a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox41"
                            Text="Please enter Produksi CPO" />
                        <asp:Label id=lblErrNo41 visible=false forecolor=red text=" Please enter Produksi CPO." runat=server />
                        <asp:Label id=lblRefNo41 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr46 text="Produksi PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox46 width=70% Text="0" OnKeyUp="javascript:calSAPK31();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator33 												
                            ControlToValidate="Textbox46"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator4a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox46"
                            Text="Please enter Produksi PK" />
                        <asp:Label id=lblErrNo46 visible=false forecolor=red text=" Please enter Produksi PK." runat=server />
                        <asp:Label id=lblRefNo46 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr42 text="Dispatch CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox42 width=70% Text="0" OnKeyUp="javascript:calSACPO31();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator36 												
                            ControlToValidate="Textbox42"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator15a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox42"
                            Text="Please enter Pengiriman CPO" />
                        <asp:Label id=lblErrNo42 visible=false forecolor=red text=" Please enter TBS Olah." runat=server />
                        <asp:Label id=lblRefNo42 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr47 text="Dispatch PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox47 width=70% Text="0" OnKeyUp="javascript:calSAPK31();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator37 												
                            ControlToValidate="Textbox47"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator16a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox47"
                            Text="Please enter Pengiriman PK" />
                        <asp:Label id=lblErrNo47 visible=false forecolor=red text=" Please enter TBS Olah." runat=server />
                        <asp:Label id=lblRefNo47 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr43 text="Saldo Akhir CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox43 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator40 												
                            ControlToValidate="Textbox43"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator5a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox43"
                            Text="Please enter TBS Olah" />
                        <asp:Label id=lblErrNo43 visible=false forecolor=red text=" Please enter TBS Olah." runat=server />
                        <asp:Label id=lblRefNo43 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr48 text="Saldo Akhir PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox48 width=70% Text="0" style="text-align:right"  CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator41 												
                            ControlToValidate="Textbox48"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator12a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox48"
                            Text="Please enter TBS Olah" />
                        <asp:Label id=lblErrNo48 visible=false forecolor=red text=" Please enter TBS Olah." runat=server />
                        <asp:Label id=lblRefNo48 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
                <tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr44 text="Titip Olah CPO Pihak Luar (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox44 width=70% Text="0" OnKeyUp="javascript:calSACPO31();" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator34 												
                            ControlToValidate="Textbox44"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator10a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox44"
                            Text="Please enter Titip Olah CPO" />
                        <asp:Label id=lblErrNo44 visible=false forecolor=red text=" Please enter Titip Olah CPO." runat=server />
                        <asp:Label id=lblRefNo44 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr49 text="Titip Olah PK Pihak Luar (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox49 width=70% Text="0" OnKeyUp="javascript:calSAPK31();" style="text-align:right"  CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator35 												
                            ControlToValidate="Textbox49"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator1a" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox49"
                            Text="Please enter Titip Olah PK" />
                        <asp:Label id=lblErrNo49 visible=false forecolor=red text=" Please enter Titip Olah PK." runat=server />
                        <asp:Label id=lblRefNo49 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
			    <tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
				    <td height=25><asp:Label id=Label14 text="BULKING" Font-Bold=true Font-Italic=true runat=server /></td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr50 text="Saldo Awal CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox50 width=70% Text="0" style="text-align:right" BackColor=darkseagreen  CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator42 												
                            ControlToValidate="Textbox50"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator6b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox50"
                            Text="Please enter saldo awal CPO" />
                        <asp:Label id=lblErrNo50 visible=false forecolor=red text=" Please enter saldo awal CPO." runat=server />
                        <asp:Label id=lblRefNo50 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr55 text="Saldo Awal PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox55 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator43 												
                            ControlToValidate="Textbox55"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator7b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox55"
                            Text="Please enter saldo awal PK" />
                        <asp:Label id=lblErrNo55 visible=false forecolor=red text=" Please enter saldo awal PK." runat=server />
                        <asp:Label id=lblRefNo55 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr51 text="Retur CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox51 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator44 												
                            ControlToValidate="Textbox51"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator9b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox51"
                            Text="Please enter Retur CPO" />
                        <asp:Label id=lblErrNo51 visible=false forecolor=red text=" Please enter Retur CPO." runat=server />
                        <asp:Label id=lblRefNo51 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr56 text="Retur PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox56 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator45 												
                            ControlToValidate="Textbox56"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator4b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox56"
                            Text="Please enter Retur PK" />
                        <asp:Label id=lblErrNo56 visible=false forecolor=red text=" Please enter Retur PK." runat=server />
                        <asp:Label id=lblRefNo56 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr52 text="Muai(Susut) CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox52 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator48 												
                            ControlToValidate="Textbox52"												
                            ValidationExpression="^[-]?\d{1,19}\.\d{1,2}|^[-]?\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator15b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox52"
                            Text="Please enter Muai(Susut) CPO" />
                        <asp:Label id=lblErrNo52 visible=false forecolor=red text=" Please enter Muai(Susut) CPO." runat=server />
                        <asp:Label id=lblRefNo52 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr57 text="Muai (Susut) PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox57 width=70% Text="0" style="text-align:right" BackColor=darkseagreen  CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator49 												
                            ControlToValidate="Textbox57"												
                            ValidationExpression="^[-]?\d{1,19}\.\d{1,2}|^[-]?\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator16b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox57"
                            Text="Please enter Muai(Susut) PK" />
                        <asp:Label id=lblErrNo57 visible=false forecolor=red text=" Muai(Susut) PK" runat=server />
                        <asp:Label id=lblRefNo57 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr53 text="Dispatch CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox53 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator19 												
                            ControlToValidate="Textbox53"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator5b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox53"
                            Text="Please enter Dispatch CPO" />
                        <asp:Label id=lblErrNo53 visible=false forecolor=red text=" Please enter Dispatch CPO." runat=server />
                        <asp:Label id=lblRefNo53 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr58 text="Dispatch PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox58 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator20 												
                            ControlToValidate="Textbox58"												
                            ValidationExpression="^(\-|)\d{1,19}(\.\d{1,2}|\.|)$"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator12b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox58"
                            Text="Please enter Dispatch PK" />
                        <asp:Label id=lblErrNo58 visible=false forecolor=red text=" Please enter Dispatch PK." runat=server />
                        <asp:Label id=lblRefNo58 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
                
			    <tr>
				    <td height=25>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id=LabelDescr54 text="Saldo Akhir CPO (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox54 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator46 												
                            ControlToValidate="Textbox54"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator10b" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox54"
                            Text="Please enter Saldo Akhir CPO" />
                        <asp:Label id=lblErrNo54 visible=false forecolor=red text=" Please enter Saldo Akhir CPO." runat=server />
                        <asp:Label id=lblRefNo54 text="CPO" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr59 text="Saldo Akhir PK (Kg)" runat=server /></td>
					<td><asp:Textbox id=Textbox59 width=70% Text="0" style="text-align:right" BackColor=darkseagreen CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator47 												
                            ControlToValidate="Textbox59"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator30" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox59"
                            Text="Please enter Saldo Akhir PK" />
                        <asp:Label id=lblErrNo59 visible=false forecolor=red text=" Please enter Saldo Akhir PK." runat=server />
                        <asp:Label id=lblRefNo59 text="PK" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
			    
			    
			    <tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td class="mt-h">V. Saldo</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
                <tr>
				    <td height=25><asp:Label id=LabelDescr60 text="Saldo Awal CPO (Rp)" runat=server /></td>
					<td><asp:Textbox id=Textbox60 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator38 												
                            ControlToValidate="Textbox60"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator19" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox60"
                            Text="Please enter Saldo Awal CPO (Rp)" />
                        <asp:Label id=lblErrNo60 visible=false forecolor=red text=" Please enter Saldo Awal CPO (Rp)." runat=server />
                        <asp:Label id=lblRefNo60 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr62 text="Saldo Awal PK (Rp)" runat=server /></td>
					<td><asp:Textbox id=Textbox62 width=70% Text="0" style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator39 												
                            ControlToValidate="Textbox62"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator20" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox62"
                            Text="Please enter Saldo Awal PK (Rp)" />
                        <asp:Label id=lblErrNo62 visible=false forecolor=red text=" Please enter Saldo Awal PK (Rp)." runat=server />
                        <asp:Label id=lblRefNo62 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td height=25><asp:Label id=LabelDescr61 text="Saldo Akhir CPO (Rp)" runat=server /></td>
					<td><asp:Textbox id=Textbox61 width=70% Text="0" readonly=true style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator58 												
                            ControlToValidate="Textbox61"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator39" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox61"
                            Text="Please enter Saldo Akhir CPO (Rp)" />
                        <asp:Label id=lblErrNo61 visible=false forecolor=red text=" Please enter Saldo Akhir CPO (Rp)." runat=server />
                        <asp:Label id=lblRefNo61 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
					<td height=25><asp:Label id=LabelDescr63 text="Saldo Akhir PK (Rp)" runat=server /></td>
					<td><asp:Textbox id=Textbox63 width=70% Text="0" readonly=true style="text-align:right" CssClass="fontObject" runat=server/>
					    <asp:RegularExpressionValidator id=RegularExpressionValidator59 												
                            ControlToValidate="Textbox63"												
                            ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
							Display="Dynamic"
							text = "<BR>Maximum length 19 digits and 2 decimal points"
                            runat="server"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidator40" runat="server" 
                            display="dynamic" 
                            ControlToValidate="Textbox63"
                            Text="Please enter Saldo Akhir PK (Rp)" />
                        <asp:Label id=lblErrNo63 visible=false forecolor=red text=" Please enter Saldo Akhir PK (Rp)." runat=server />
                        <asp:Label id=lblRefNo63 text="" Visible=false runat=server />
					</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6">
					    <asp:ImageButton id=NewBtn AlternateText="  Save  " imageurl="../../images/butt_new.gif" onclick=Button_Click CommandArgument=New runat=server />
					    <asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
					    <asp:ImageButton id=PrintBtn AlternateText="Print Preview" imageurl="../../images/butt_print_preview.gif" onClick="Preview_Click" runat="server" />
					    <asp:ImageButton id=DelBtn AlternateText="  Save  " imageurl="../../images/butt_delete.gif" onclick=Button_Click CommandArgument=Del runat=server />
						<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
					</td>
				</tr>
				<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			    <tr>
				    <td colSpan="6">
				        <div id="div1" style="height:400px;width:1000;overflow: auto;">	
					    <asp:DataGrid id=dgLineDet
						    AutoGenerateColumns="false" width="170%" runat="server"
						    GridLines=Both
						    Cellpadding="2"
						    Pagerstyle-Visible="False" 
                             OnItemDataBound="dgLine_BindGrid"
						    AllowSorting="True">
						    <HeaderStyle CssClass="mr-h"/>
						    <ItemStyle CssClass="mr-l"/>
						    <AlternatingItemStyle CssClass="mr-r"/>
						    <Columns>	
						        <asp:TemplateColumn HeaderText=" NO." HeaderStyle-HorizontalAlign=Center>
								    <ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("RowID") %> id="lblRowID" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" DESCRIPTION" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign=Center>
								    <ItemTemplate>
									    <asp:Label Text=<%# Container.DataItem("Description") %> id="lblDescription" runat="server" />
								    </ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText=" TOTAL" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %> id="lblTotal" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>	
							    <asp:TemplateColumn HeaderText=" JANUARI" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln1"), 2), 2) %>  id="lblBln1" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   	<asp:TemplateColumn HeaderText=" FEBRUARI" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln2"), 2), 2) %> id="lblBln2" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" MARET" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln3"), 2), 2) %> id="lblBln3" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" APRIL" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln4"), 2), 2) %> id="lblBln4" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" MEI" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln5"), 2), 2) %> id="lblBln5" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" JUNI" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln6"), 2), 2) %> id="lblBln6" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" JULI" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln7"), 2), 2) %> id="lblBln7" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" AGUSTUS" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln8"), 2), 2) %> id="lblBln8" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" SEPTEMBER" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln9"), 2), 2) %> id="lblBln9" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" OKTOBER" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln10"), 2), 2) %> id="lblBln10" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" NOVEMBER" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln11"), 2), 2) %> id="lblBln11" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   <asp:TemplateColumn HeaderText=" DESEMBER" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign=Center ItemStyle-HorizontalAlign=Right>
								    <ItemTemplate> 
									    <ItemStyle />
										    <asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Bln12"), 2), 2) %> id="lblBln12" runat="server" />
									    </ItemStyle>
								    </ItemTemplate>
							    </asp:TemplateColumn>
							   			
    								
					    </Columns>										
					    </asp:DataGrid>
					    </div>
				    </td>
			    </tr>
    			<tr>
				    <td colspan=6>&nbsp;</td>
			    </tr>
			</table>

            </div>
            </td>
        </tr>
        </table>
		</form>
	</body>
</html>
