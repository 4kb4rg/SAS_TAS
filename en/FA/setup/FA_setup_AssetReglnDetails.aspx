<%@ Page Language="vb" Src="../../../include/FA_setup_AssetReglnDetails.aspx.vb" Inherits="FA_setup_AssetReglnDetails" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuFASetup" src="../../menu/menu_FASetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>


<html>
	<head>
		<title>FIXED ASSET - Asset Registration Line Details</title>
		<Preference:PrefHdl id="PrefHdl" runat="server" />
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<script language="javascript">
		
			function setLifeTime() {
				var doc = document.frmMain;
				if (doc.cbFiskalSame.checked == false) 
				 {
				    doc.txtLifeTimeF.value = doc.txtLifeTime.value;
				 }
				
			}
            
            function setStartMonth() {
				var doc = document.frmMain;
				if (doc.cbFiskalSame.checked == false) 
				{
				doc.txtStartMonthF.value = doc.txtStartMonth.value;
				}
			}
			
			function setStartYear() {
				var doc = document.frmMain;
				if (doc.cbFiskalSame.checked == false) 
				{
				doc.txtStartYearF.value = doc.txtStartYear.value;
				}
			}
			
			function setAssetValue() {
				var doc = document.frmMain;
				if (doc.cbFiskalSame.checked == false) 
				{
				doc.txtAssetValueF.value = doc.txtAssetValue.value;
				}
			}
			
            function setAccumDeprValue() {
				var doc = document.frmMain;
				if (doc.cbFiskalSame.checked == false) 
				{
				doc.txtAccumDeprValueF.value = doc.txtAccumDeprValue.value;
				}
			}
			
			function setFinalValue() {
				var doc = document.frmMain;
				if (doc.cbFiskalSame.checked == false) 
				{
				doc.txtFinalValueF.value = doc.txtFinalValue.value;
				}
			}
			
			function setBegYearValue() {
				var doc = document.frmMain;
				if (doc.cbFiskalSame.checked == false) 
				{
				doc.txtBegYearValueF.value = doc.txtBegYearValue.value;
				}
			}
			
			function setValue() {
				var doc = document.frmMain;
				if (doc.hidQty.value != 0) 
				 {
				    doc.txtAssetValue.value = (doc.hidAmount.value / doc.hidQty.value) * doc.txtQty.value;
				    doc.txtAssetValueF.value = doc.txtAssetValue.value;
				 }
				
			}

		</script>		
	    <style type="text/css">
            .style1
            {
                height: 26px;
                width: 17%;
            }
            .style2
            {
                width: 17%;
            }
            .style3
            {
                height: 25px;
                width: 17%;
            }
            .style4
            {
                height: 40px;
            }
            .mt-h
            {
                text-align: right;
            }
            .style5
            {
                height: 22px;
            }
        </style>
	</head>
	<body>
		<form id="frmMain"  class="main-modul-bg-app-list-pu"  runat="server">

           <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		    <tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist"> 

			<asp:label id="lblCode" visible="false" text=" Code" runat="server" />
			<asp:label id="lblOper" visible="false" runat="server" />
			<asp:label id="lblSelect" visible="false" text="Select " runat="server" />
			<asp:label id="lblPleaseSelect" visible="false" text="Please select " runat="server" />
			<Input type=hidden id=hidQty value="0" runat=server/>
			<Input type=hidden id=hidAmount value="0" runat=server/>

			<table cellpadding="1" cellspacing="1" width="100%" border="0" class="font9Tahoma">
 				<tr>
					<td colspan="5">
						<UserControl:MenuFASetup id="MenuFASetup" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="style4" colspan="5" width="60%"><asp:label id="lblTitle" runat="server" /><strong> ASSET REGISTRATION LINE DETAILS </strong>   <hr style="width :100%" /><br />
                    </td>
				</tr>
				<tr>
					<td class="mt-h" colspan="3" width="60%">&nbsp;</td>
					<td colspan="2" align="right"><asp:label id="lblTracker" runat="server"/></td>
				</tr>
				
				<tr>
					<td style="height: 25px">Cara Perolehan :*</td>
					<td style="height: 40px"><asp:DropDownList id="ddlAcquisitionMode"  CssClass="font9Tahoma" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectMode" />
						<asp:label id="lblAcquisitionModeErr" Visible="false" forecolor="red" Runat="server" />
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Tipe Aset</td>
					<td width=30%><asp:DropDownList id=ddlTrxType width=100% runat=server  CssClass="font9Tahoma">
								        <asp:ListItem value="1">Leasing</asp:ListItem>
						                <asp:ListItem value="2">Financing/Pembiayaan</asp:ListItem>
						                <asp:ListItem value="3" Selected>Tetap</asp:ListItem>
					                </asp:DropDownList>
					<asp:Label id=lblErrTrxType visible=false forecolor=red text=" Please select Type." runat=server />                
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td height=25>Kategori Aset</td>
					<td width=30%><asp:DropDownList id=ddlKategori width=100% runat=server  CssClass="font9Tahoma">
								        <asp:ListItem value="1" Selected>Umum</asp:ListItem>
						                <asp:ListItem value="2">Tanah</asp:ListItem>
					                </asp:DropDownList>
					<asp:Label id=Label1 visible=false forecolor=red text=" Please select Type." runat=server />                
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				<tr id="trAsset" runat="server">
					<td style="height: 25px">Asset Issue:</td>
					<td style="height: 25px" colspan="3"><asp:DropDownList id="ddlAsset" Width="100%"  CssClass="font9Tahoma" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectAsset"/>
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				
				<tr id="trTransfer" runat="server" visible="false">
					<td style="height: 25px">Asset Transfer:</td>
					<td style="height: 25px" colspan="3"><asp:DropDownList id="ddlTransfer" Width="100%"  CssClass="font9Tahoma" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SelectAssetTran"/>
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				
				<tr>
					<td style= "width:25%; height:25px">Kode Asset :</td>
					<td style= "width:25%; height:25px" valign=center><asp:TextBox id="txtAssetCode"   runat="server" width=80% maxlength="32" CssClass="font9Tahoma"/>
						<asp:Label id="lblDupMsg" visible="False" forecolor="Red" text="<br>This code has been used. Please try again." display=dynamic runat=server/>
					  <asp:DropDownList width=14% id=ddlNumber runat=server  CssClass="font9Tahoma">
								        <asp:ListItem value="01" Selected>01</asp:ListItem>
						                <asp:ListItem value="02">02</asp:ListItem>
						                <asp:ListItem value="03">03</asp:ListItem>
						                <asp:ListItem value="04">04</asp:ListItem>
						                <asp:ListItem value="05">05</asp:ListItem>
						                <asp:ListItem value="06">06</asp:ListItem>
						                <asp:ListItem value="07">07</asp:ListItem>     
						                <asp:ListItem value="08">08</asp:ListItem> 
						                <asp:ListItem value="09">09</asp:ListItem> 
						                <asp:ListItem value="10">10</asp:ListItem> 
					                </asp:DropDownList>
					</td>
					<td style= "width:5%; height:25px">&nbsp;</td>
					<td style= "width:15%; height:25px">Status :</td>
					<td style= "width:25%; height:25px"><asp:Label id="lblStatus" runat="server"/></td>
				</tr>
			
				<tr>
					<td style="height: 25px">Deskripsi :*</td>
					<td valign=center style="height: 25px"><asp:TextBox id="txtDescription"   runat="server" width="100%" maxlength="128"  CssClass="font9Tahoma"/>
						<asp:RequiredFieldValidator 
							id="rfvDescription" 
							runat="server"  
							controltovalidate="txtDescription" 
							text = "Field cannot be blank"
							display="dynamic"/>
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">Date Created :</td>
					<td style="height: 25px"><asp:Label id="lblCreateDate" runat="server"/></td>
				</tr>
				<tr>
					<td>Keterangan :</td>
					<td rowspan="5">
						<textarea rows="8" id="txtExtDescription" cols="42" runat="server"></textarea>
						<asp:Label id="lblExtDescriptionErr" visible="false" forecolor="red" text="<br>Maximum length is up to 512 characters only." runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Last Update :</td>
					<td><asp:Label id="lblLastUpdate" runat="server"/></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>Update By : </td>
					<td><asp:Label id="lblUpdateBy" runat="server"/></td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">&nbsp;</td>
				</tr>
				 <tr>
                    <td style="height: 25px">Qty :*</td>
                    <td style="height: 25px"><asp:TextBox id="txtQty"  CssClass="font9Tahoma" Runat="server" width="40%" maxlength="20" onkeyup="javascript:setValue()"/>
                        Satuan :* <asp:DropDownList id="ddlUOM" width="40%" runat="server"  CssClass="font9Tahoma"/><asp:RequiredFieldValidator 
							id="rfvQty" 
							runat="server"  
							controltovalidate="txtQty" 
							text = "Field cannot be blank"
							display="dynamic"/>&nbsp;
                        <asp:RangeValidator id="rvQty"
							ControlToValidate="txtQty"
							MinimumValue="0"
							MaximumValue="10000"
							Type="double"
							EnableClientScript="True"
							Text="The value is out of range !"
							runat="server" display="dynamic"/>
                        <asp:label id="lblUOMErr" Visible="False" forecolor="Red" Text = "Please Select UOM" Runat="server" /></td>
                    <td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
                </tr>
				
				<tr>
					<td style="height: 25px">Jenis Asset :*</td>
					<td style="height: 25px" colspan="3"><asp:DropDownList id="ddlAssetHeaderCode" Width="100%" runat="server"  CssClass="font9Tahoma"/>
					    <asp:label id="lblAssetHeaderCodeErr" Visible="false" forecolor="red" Runat="server" />
					</td>
                    <td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 25px">Serial No : </td>
					<td style="height: 25px"><asp:TextBox id="txtSerialNo" runat="server" width="100%" maxlength="32"  CssClass="font9Tahoma"/></td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 25px">Kondisi Asset :*</td>
					<td style="height: 25px"><asp:DropDownList id="ddlAssetCondition" Width="100%" runat="server"  CssClass="font9Tahoma"/>
						<asp:label id="lblAssetConditionErr" Visible="false" forecolor="red" Runat="server" />
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				
				<tr>
					<td style="height: 25px">Tanggal Perolehan :*</td>
					<td style="height: 25px">
						<table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
								<td style="width:15%; height: 25px"><asp:TextBox id="txtPurchaseDate" runat="server" width="100%" maxlength="10"  CssClass="font9Tahoma"/></td>
								<td style="width:20%; height: 25px">&nbsp;<a href="javascript:PopCal('txtPurchaseDate');"><asp:Image id="btnSelDateFrom" runat="server" ImageUrl="../../Images/calendar.gif"/></a></td>					
							</tr>
						</table>
						<asp:label id="lblPurchaseDateErr" Text ="<br>Date Entered should be in the format " forecolor="red" Visible = "false" Runat="server"/> 
						<asp:label id="lblFmt"  forecolor="red" Visible = "false" Runat="server"/> 
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				
				<tr>
					<td style="height: 25px">Lokasi Asset :*</td>
					<td><asp:DropDownList id="ddlDeptCode" Width="100%" runat="server"  CssClass="font9Tahoma"/></td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				<tr>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 25px">&nbsp;</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
			
				<tr>
					<td style="height: 25px">Akun Asset :*</td>
					<td>
						<table Width="100%" CellSpacing="0" CellPadding="0" Border="0">
							<tr>
								<td Width="100%"><asp:DropDownList id="ddlGLAssetAccCode" Width="100%" runat=server  CssClass="font9Tahoma"/></td>
							</tr>
						</table>	
						<asp:label id="lblGLAssetAccCodeErr" Visible="false" forecolor="red" Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Akun Asset on Leasing :*</td>
					<td><asp:DropDownList id="ddlGLAssetAccCodeLease" AutoPostBack=False Width="100%" runat="server"  CssClass="font9Tahoma"/>
						<asp:label id="lblGLAssetAccCodeLeaseErr" Visible="false" forecolor="red" Runat="server" />
					</td>
				</tr>
				<tr>
					<td style="height: 25px">Akun Penyusutan :*</td>
					<td><asp:DropDownList id="ddlDeprGLDeprAccCode" AutoPostBack=True OnSelectedIndexChanged="CheckGLDeprAccBlk" Width="100%" runat="server"  CssClass="font9Tahoma"/>
						<asp:label id="lblDeprGLDeprAccCodeErr" Visible="false" forecolor="red" Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr id="GLDeprBlkCode" visible = False runat=server >
					<td style="height: 25px">
                        Cost Center :*</td>
					<td style="height: 25px"><asp:DropDownList id="ddlDeprGLDeprBlkCode" Width="100%" runat=server  CssClass="font9Tahoma"/>
						<asp:label id="lblDeprGLDeprBlkCodeErr" Visible="false" forecolor="red" Runat="server" />
					</td>
					<td style="height: 5px">&nbsp;</td>
					<td style="height: 15px">&nbsp;</td>
                    <td style="height: 25px">&nbsp;</td>
				</tr>
				
				<tr>
					<td style="height: 25px">Akun Ak. Penyusutan :</td>
					<td><asp:DropDownList id="ddlDeprGLAccumDeprAccCode" AutoPostBack=false  Width=100% runat=server  CssClass="font9Tahoma"/>
						<asp:label id="lblDeprGLAccumDeprAccCodeErr" Visible="false" forecolor="red" Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>Akun Ak. Penyusutan on Leasing :*</td>
					<td><asp:DropDownList id="ddlDeprGLAccumDeprAccCodeLease" AutoPostBack=False Width="100%" runat="server"  CssClass="font9Tahoma"/>
						<asp:label id="lblDeprGLAccumDeprAccCodeLeaseErr" Visible="false" forecolor="red" Runat="server" />
					</td>
				</tr>
				<tr>
					<td style = "width:17%; height:25px">Kategory Fiskal :*</td>
					<td style = "width:25%; height:25px"><asp:DropDownList id="ddlFiskalCategory" Width=100% runat=server   CssClass="font9Tahoma"/>
						<asp:label id="lblFiskalCategoryErr" Visible="false" forecolor="red" Runat="server" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>Perhitungan Fiskal Sama:</td>
					<td>
						<asp:Checkbox id="cbFiskalSame" text=" Yes" runat="server"  AutoPostBack=True OnCheckedChanged="ChekFiscalSame" />
					</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				
				<tr>
					<td class="style5"></td>
					<td class="style5"></td>
					<td class="style5"></td>
					<td class="style5"></td>
					<td class="style5"></td>
				</tr>
								
				<tr>
					<td colspan=5 style="height: 25px"> <hr style="width :100%" /></td>
				</tr>
				</table>
				
				<table cellpadding="1" cellspacing="1" width="100%" border="0" class="font9Tahoma">
				<tr>
				    <td colspan="2" style = "width:48%">
				        <table cellpadding="1" cellspacing="1"  border="0" class="font9Tahoma">
				            <tr>
					            <td style="height:25px" colspan="2"><u><b>Commercial Depreciation</b></u>&nbsp;</td>
				            </tr>
				            <tr>
					            <td class="style1">Nilai Perolehan:</td>
					            <td style="height: 26px"><asp:TextBox id="txtAssetValue"  CssClass="font9Tahoma" Runat="server" width="100%" maxlength="20" onkeyup="javascript:setAssetValue()" /></td>
				            </tr>
				            <tr>
					            <td class="style2">Nilai Akumulasi Penyusutan:</td>
					            <td><asp:TextBox id="txtAccumDeprValue"  CssClass="font9Tahoma" Runat="server" width="100%" maxlength="20" onkeyup="javascript:setAccumDeprValue()"/></td>
				            </tr>
				            <tr>
					            <td class="style2">
                                    Final Value:</td>
					            <td><asp:TextBox id="txtFinalValue"  CssClass="font9Tahoma" runat="server" width="100%" maxlength="20" onkeyup="javascript:setFinalValue()"/>
						            <asp:RequiredFieldValidator 
							            id="rfvFinalValue" 
							            runat="server"  
							            ControlToValidate="txtFinalValue" 
							            text = "Field cannot be blank"
							            display="dynamic"/>
            						
            					
					            </td>
				            </tr>
				            <tr>
					            <td class="style2">Beginning Year Value:</td>
					            <td><asp:TextBox id="txtBegYearValue"   CssClass="font9Tahoma" runat="server" width="100%" maxlength="20" onkeyup="javascript:setBegYearValue()"/>
            						
					            </td>
				            </tr>
				            <tr>
					            <td class="style3">Metoda Penyusutan :*</td>
					            <td style = "width:25%; height:25px"><asp:DropDownList id="ddlDeprMethod" Width=100% runat=server  CssClass="font9Tahoma"/>
						            <asp:label id="lblDeprMethodErr" Visible="false" forecolor="red" Runat="server" />
					            </td>
            				
				            </tr>
				            <tr>
					            <td class="style2">Umur (Tahun) :*</td>
					            <td><asp:TextBox id="txtLifeTime"  CssClass="font9Tahoma" runat="server" width="90%" maxlength="4"/>
						            <asp:RequiredFieldValidator 
							            id="rfvLifeTime" 
							            runat="server"  
							            ControlToValidate="txtLifeTime" 
							            text = "Field cannot be blank"
							            display="dynamic"/>
						            <asp:RegularExpressionValidator id="revLifeTime" 
							            ControlToValidate="txtLifeTime"
							            ValidationExpression="\d{1,4}"
							            Display="Dynamic"
							            text = "Maximum length 4 digits and 0 decimal points"
							            runat="server"/>
						            <asp:RangeValidator id="rvLifeTime"
							            ControlToValidate="txtLifeTime"
							            MinimumValue="1"
							            MaximumValue="1200"
							            Type="double"
							            EnableClientScript="True"
							            Text="The value is out of range !"
							            runat="server" display="dynamic"/>
					            </td>
				            </tr>
				            <tr>
					            <td class="style2">Mulai Penyusutan (Bln/Thn) :*</td>
					            <td>
						            <asp:TextBox id="txtStartMonth"  CssClass="font9Tahoma"  runat="server" width="15%" maxlength="2" onkeyup="javascript:setStartMonth()"/>
						            <asp:RequiredFieldValidator 
							            id="rfvStartMonth" 
							            runat="server"  
							            ControlToValidate="txtStartMonth" 
							            text = "Field cannot be blank. "
							            display="dynamic"/>
						            <asp:RegularExpressionValidator id="revStartMonth" 
							            ControlToValidate="txtStartMonth"
							            ValidationExpression="\d{1,2}"
							            Display="Dynamic"
							            text = "Please enter 1 to 12. "
							            runat="server"/>
						            <asp:RangeValidator id="rvStartMonth"
							            ControlToValidate="txtStartMonth"
							            MinimumValue="1"
							            MaximumValue="12"
							            Type="double"
							            EnableClientScript="True"
							            Text="The value is out of range! "
							            runat="server" display="dynamic"/>/
						            <asp:TextBox id="txtStartYear"  CssClass="font9Tahoma" runat="server" width="28%" maxlength="4" onkeyup="javascript:setStartYear()"/>
						            <asp:RequiredFieldValidator 
							            id="rfvStartYear" 
							            runat="server"  
							            ControlToValidate="txtStartYear" 
							            text = "Field cannot be blank. "
							            display="dynamic"/>
						            <asp:RegularExpressionValidator id="revStartYear" 
							            ControlToValidate="txtStartYear"
							            ValidationExpression="\d{4}"
							            Display="Dynamic"
							            text = "Please enter 4 digits. "
							            runat="server"/>
						            <asp:RangeValidator id="rvStartYear"
							            ControlToValidate="txtStartYear"
							            MinimumValue="1900"
							            MaximumValue="9999"
							            Type="double"
							            EnableClientScript="True"
							            Text="The value is out of range! "
							            runat="server" display="dynamic"/>
					            </td>
            				
				            </tr>
				            <tr>
					            <td height="25px" class="style2">Per. Dikurangi Nilai Akhir:</td>
					            <td align=left valign=top>
						            <asp:Checkbox id="cbDeprInd" text=" Yes" runat=server />
					            </td>
				            </tr>
            				
            				
				            <tr>
					            <td class="style2">Penyusutan Perbulan :</td>
					            <td><asp:label id="lblDeprMonth" Runat="server"/></td>
				            </tr>
				            <tr>
					            <td colspan="2"> <hr style="width :100%" /></td>
				            </tr>
				
            				
				            <tr>
					            <td class="style2">
                                    Mutasi Nilai Perolehan:</td>
                                <td><asp:label id="txtDispWOAssetValue" Runat="server"/></td>
				            </tr>
				            <tr>
					            <td class="style2">
                                    Mutasi Akumulasi Penyusutan:</td>
					            <td><asp:label id="txtDispWOAccumDeprValue" Runat="server" /></td>
				            </tr>
				            <tr>
					            <td class="style2">
                                    Net Value:</td>
					            <td><asp:label id="lblNetValue" Runat="server"/></td>
				            </tr>
            			</table>
            				    
				     </td>
				     <td style = "width:4%" >   </td> 
				     <td colspan="2" style = "width:48%" >
				        <table cellpadding="1" cellspacing="1" width="100%" border="0" id="tblFiscal" runat="server"  class="font9Tahoma">
                                    <tr>
					            <td colspan=2 style="height: 21px"><u><b>Fiskal Depreciation</b></u></td>
                                    </tr>
                                    <tr>
					            <td>Nilai Perolehan</td>
					            <td><asp:TextBox id="txtAssetValueF"  CssClass="font9Tahoma" Runat="server" width="100%" maxlength="20"/></td>
                                    </tr>
                                    <tr>
					            <td>Nilai Akumulasi Penyusutan:</td>
					            <td><asp:TextBox id="txtAccumDeprValueF"  CssClass="font9Tahoma" Runat="server" width="100%" maxlength="20"/></td>
                                    </tr>
                                    <tr>
					            <td>Final Value:</td>
					            <td><asp:TextBox id="txtFinalValueF"  CssClass="font9Tahoma" runat="server" width="100%" maxlength="20"/>
                                    <asp:RequiredFieldValidator 
							            id="rfvFinalValueF" 
							            runat="server"  
							            ControlToValidate="txtFinalValueF" 
							            text = "Field cannot be blank"
							            display="dynamic"/>
					             </td>
                                    </tr>
                                    <tr>
					            <td>Beginning Year Value:</td>
					            <td><asp:TextBox id="txtBegYearValueF"  CssClass="font9Tahoma" runat="server" width="100%" maxlength="20"/>
					             </td>
                                    </tr>
                                    <tr>
					            <td style = "width:17%; height:25px">Metoda Penyusutan :*</td>
					            <td style = "width:25%; height:25px"><asp:DropDownList id="ddlDeprMethodF" Width=100% runat=server   CssClass="font9Tahoma"/>
						            <asp:label id="lblDeprMethodErrF" Visible="false" forecolor="red" Runat="server" />
					            </td>
                                    </tr>
                                    <tr>
						            <td>Umur (Tahun) :*</td>
						            <td><asp:TextBox id="txtLifeTimeF"  CssClass="font9Tahoma" runat="server" width="90%" maxlength="4" />
            						
						            <asp:RequiredFieldValidator 
							            id="rfvLifeTimeF" 
							            runat="server"  
							            ControlToValidate="txtLifeTimeF" 
							            text = "Field cannot be blank"
							            display="dynamic"/>
						            <asp:RegularExpressionValidator id="revLifeTimeF" 
							            ControlToValidate="txtLifeTimeF"
							            ValidationExpression ="\d{1,4}"
							            Display="Dynamic"
							            text = "Maximum length 4 digits and 0 decimal points"
							            runat="server"/>
						            <asp:RangeValidator id="rvLifeTimeF"
							            ControlToValidate="txtLifeTimeF"
							            MinimumValue="1"
							            MaximumValue="1200"
							            Type="double"
							            EnableClientScript="True"
							            Text="The value is out of range !"
							            runat="server" display="dynamic"/>
						            </td>
                                    </tr>
                                    <tr>
					            <td>Mulai Penyusutan (Bln/Thn) :*</td>
					            <td><asp:TextBox id="txtStartMonthF"  CssClass="font9Tahoma" runat="server" width="15%" maxlength="2"/>
                                    <asp:RequiredFieldValidator 
							            id="rfvStartMonthF" 
							            runat="server"  
							            ControlToValidate="txtStartMonthF" 
							            text = "Field cannot be blank. "
							            display="dynamic"/>
                                    <asp:RegularExpressionValidator id="revStartMonthF" 
							            ControlToValidate="txtStartMonthF"
							            ValidationExpression="\d{1,2}"
							            Display="Dynamic"
							            text = "Please enter 1 to 12. "
							            runat="server"/>
                                    <asp:RangeValidator id="rvStartMonthF"
							            ControlToValidate="txtStartMonthF"
							            MinimumValue="1"
							            MaximumValue="12"
							            Type="double"
							            EnableClientScript="True"
							            Text="The value is out of range! "
							            runat="server" display="dynamic"/>/ <asp:TextBox id="txtStartYearF"  CssClass="font9Tahoma"  runat="server" width="28%" maxlength="4"/>
                                    <asp:RequiredFieldValidator 
							            id="rfvStartYearF" 
							            runat="server"  
							            ControlToValidate="txtStartYearF" 
							            text = "Field cannot be blank. "
							            display="dynamic"/>
                                    <asp:RegularExpressionValidator id="revStartYearF" 
							            ControlToValidate="txtStartYearF"
							            ValidationExpression="\d{4}"
							            Display="Dynamic"
							            text = "Please enter 4 digits. "
							            runat="server"/>
                                    <asp:RangeValidator id="rvStartYearF"
							            ControlToValidate="txtStartYearF"
							            MinimumValue="1900"
							            MaximumValue="9999"
							            Type="double"
							            EnableClientScript="True"
							            Text="The value is out of range! "
							            runat="server" display="dynamic"/></td>
                                    </tr>
                                    <tr>
					            <td height="25px">Per. Dikurangi Nilai Akhir:</td>
					            <td align=left valign=top>
						            <asp:Checkbox id="cbDeprIndF" text=" Yes" runat=server />
					            </td>
                                    </tr>
                                    <tr>
					            <td style="height: 21px">Penyusutan Perbulan :</td>
					            <td style="height: 21px"><asp:label id="lblDeprMonthF" Runat="server"/></td>
                                    </tr>
                         
				            <tr>
					            <td colspan=2>
                                     <hr style="width :100%" /></td>
				            </tr>
				            <tr><td>
                                Mutasi Nilai Perolehan:</td>
                                <td>
                                    <asp:label id="txtDispWOAssetValueF" Runat="server"/></td>
				            </tr>
				            <tr><td>
                                Mutasi Akumulasi Penyusutan:</td>
                                <td>
                                    <asp:label id="txtDispWOAccumDeprValueF" Runat="server" /></td>
				            </tr>
				            <tr><td>
                                Net Value:</td>
                                <td>
                                    <asp:label id="lblNetValueF" Runat="server"/></td>
				            </tr>
            			</table>        
				                
				                
				      </td>
				  </tr>
				
				</table>
				
				
				
				
				
				<table>
										
				<tr>
					<td colspan="5" style="height: 21px"><asp:label id="lblSaveErr" text="Invalid data. Data cannot be saved!" Visible=False forecolor=red Runat="server" />
					</td>				
				</tr>
				<tr>
					<td colspan="5"><asp:label id="lblDeleteErr" text="You are not allowed to delete this asset!" Visible=False forecolor=red Runat="server" />
					</td>				
				</tr>
				<tr>
				   <td colspan="5">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="5">
				        <asp:ImageButton id="NewBtn"  imageurl="../../images/butt_new.gif"  AlternateText="  New  " onClick="NewBtn_Click" runat="server"/>
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />
						<asp:ImageButton id="btnDelete" imageurl="../../images/butt_delete.gif" visible="true" CausesValidation="false" AlternateText=" Delete " onclick="btnDelete_Click" runat="server" />
						<asp:ImageButton id="btnUnDelete" imageurl="../../images/butt_undelete.gif" visible="true" AlternateText=" Undelete " onclick="btnUnDelete_Click" runat="server" />
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="false" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />
					    <br />
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
