<%@ Page Language="vb" src="../../../include/PU_trx_RPHDet.aspx.vb" Inherits="PU_trx_RPHDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuPU" src="../../menu/menu_putrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
  TagPrefix="igtab" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

 

<html>
	<head>
		<title>Quotation (RPH) Details</title>	
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />	
		<Preference:PrefHdl id=PrefHdl runat="server" />
	    <style type="text/css">
            .style1
            {
                height: 21px;
                width: 879px;
            }
            .style2
            {
                width: 19%;
            }
            .style3
            {
                height: 14px;
                width: 218px;
            }
            .style4
            {
                width: 218px;
            }
            .style5
            {
                height: 3px;
                width: 218px;
            }
            .style6
            {
                height: 21px;
                width: 218px;
            }
            .style7
            {
                width: 344px;
            }
            
html.RadForm_Black, 
html.RadForm_BlackMetroTouch {
    background-color: #fff;
}
 
.size-custom {
    height: 365px;
    width: 1208px;
}
            
        </style>
	</head>
	<script language="javascript">			
		
			function calUnitCost1() {			
				var doc = document.frmMain;				
				var a = parseFloat(doc.txtQtyOrder1.value);
				var b = parseFloat(doc.txtTtlCost1.value);				
				doc.txtCost1.value = b / a;
				if (doc.txtCost1.value == 'NaN')
					doc.txtCost1.value = '';
				else
					doc.txtCost1.value = round(doc.txtCost1.value, 2);										
			}
			
			function calTtlCost1() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder1.value);
				var b = parseFloat(doc.txtCost1.value);
				var c = parseFloat(doc.txtDiscount1.value);
			    doc.txtTtlCost1.value = (a * b) - ((a * b) * (c / 100));
				if (doc.txtTtlCost1.value == 'NaN')
					doc.txtTtlCost1.value = '';
				else
					doc.txtTtlCost1.value = round(doc.txtTtlCost1.value, 0);
			}
            function calTtlQtyCost1() {
                var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder1.value);
				var b = parseFloat(doc.txtCost1.value);				
				doc.txtTtlCost1.value = a * b;
				if (doc.txtTtlCost1.value == 'NaN')
				    doc.txtTtlCost1.value = '';
				else
					doc.txtTtlCost1.value = round(doc.txtTtlCost1.value, 2);
			}	
			
			function calUnitCost2() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder2.value);
				var b = parseFloat(doc.txtTtlCost2.value);
				doc.txtCost2.value = b / a;
				if (doc.txtCost2.value == 'NaN')
					doc.txtCost2.value = '';
				else
					doc.txtCost2.value = round(doc.txtCost2.value, 2);
			}			
			function calTtlCost2() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder2.value);
				var b = parseFloat(doc.txtCost2.value);
				var c = parseFloat(doc.txtDiscount2.value);
				doc.txtTtlCost2.value = (a * b) - ((a * b) * (c / 100));
				if (doc.txtTtlCost2.value == 'NaN')
					doc.txtTtlCost2.value = '';
				else
					doc.txtTtlCost2.value = round(doc.txtTtlCost2.value, 0);
			}
			function calTtlQtyCost2() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder2.value);
				var b = parseFloat(doc.txtCost2.value);				
				doc.txtTtlCost2.value = a * b;
				if (doc.txtTtlCost2.value == 'NaN')
					doc.txtTtlCost2.value = '';
				else
					doc.txtTtlCost2.value = round(doc.txtTtlCost2.value, 2);
			}	
			
			function calUnitCost3() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder3.value);
				var b = parseFloat(doc.txtTtlCost3.value);
				doc.txtCost3.value = b / a;
				if (doc.txtCost3.value == 'NaN')
					doc.txtCost3.value = '';
				else
					doc.txtCost3.value = round(doc.txtCost3.value, 2);
			}			
			function calTtlCost3() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder3.value);
				var b = parseFloat(doc.txtCost3.value);
				var c = parseFloat(doc.txtDiscount3.value);
				doc.txtTtlCost3.value = (a * b) - ((a * b) * (c / 100));
				if (doc.txtTtlCost3.value == 'NaN')
					doc.txtTtlCost3.value = '';
				else
					doc.txtTtlCost3.value = round(doc.txtTtlCost3.value, 0);
			}
			function calTtlQtyCost3() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtQtyOrder3.value);
				var b = parseFloat(doc.txtCost3.value);				
				doc.txtTtlCost3.value = a * b;
				if (doc.txtTtlCost3.value == 'NaN')
					doc.txtTtlCost3.value = '';
				else
					doc.txtTtlCost3.value = round(doc.txtTtlCost3.value, 2);
			}	
			
			function calAddDiscount() {
				var doc = document.frmMain;
				var a = parseFloat(doc.hidTtlAmount.value);
				var b = parseFloat(doc.txtAddDisc.value);
				doc.txtTtlAftDisc.value = a - b;
				if (doc.txtAddDisc.value == 'NaN')
				    doc.txtTtlAftDisc.value = doc.hidTtlAmount.value
				else
				    doc.txtTtlAftDisc.value = round(doc.txtTtlAftDisc.value, 0);
			}


			(function (global, undefined) {
			    var demo = {};

			    function togglePopupModality() {
			        var wnd = getModalWindow();
			        wnd.set_modal(!wnd.get_modal());
			        setCustomPosition(wnd);

			        if (!wnd.get_modal()) {
			            document.documentElement.focus();
			        }
			    }

			    function setCustomPosition(sender, args) {
			        sender.moveTo(sender.getWindowBounds().x, 280);
			    }

			    function showDialogInitially() {
			        var wnd = getModalWindow();
			        wnd.show();
			        Sys.Application.remove_load(showDialogInitially);
			    }
			    Sys.Application.add_load(showDialogInitially);

			    function getModalWindow() { return $find(demo.modalWindowID); }

			    global.$modalWindowDemo = demo;
			    global.togglePopupModality = togglePopupModality;
			    global.setCustomPosition = setCustomPosition;
			})(window);
             

	</script>	
	<body id="aaa">
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server>
         
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
               
        <table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
		<tr>
             <td style="width: 100%; height: 800px" valign="top">
			    <div class="kontenlist">  

			<asp:Label id=lblHidStatus visible=false runat=server />
			<asp:Label id=lblErrMessage visible=False Text="Error while initiating component." runat=server />
			<asp:label id=lblSelectListLoc visible=false text="Please Select Purchase Requisition Ref " runat="server"/>
			<asp:label id=lblSelectListItem visible=false text="Please Select " runat="server" />
			<asp:label id=lblPR visible=false text="PR " runat="server" />
			<asp:label id=lblPRRef visible=false text="PR Ref. " runat="server" />
			<input type=hidden id=hidOrgQtyOrder runat=server NAME="hidOrgQtyOrder"/>	
			<Input Type=Hidden id=hidCentralizedtype runat=server />	
            <input type=hidden id=hidTtlAmount runat=server/>					 	
            <input type=hidden id=hidAddDisc runat=server/>					 	
            <input type=hidden id=hidTtlAmtAftDisc runat=server/>	
            				 	
			<table border="0" cellspacing="3" cellpadding="1" width="100%" class="font9Tahoma">
				<tr>
					<td colspan="6"><UserControl:MenuPU id=menuPU runat="server" /></td>
				</tr>			
				<tr>
					<td class="font9Tahoma" colspan="6"><strong>QUOTATION (DTH) DETAILS</strong> </td>
				</tr>
				<tr>
					<td colspan="6">
                        <hr style="width :100%" />
                    </td>
				</tr>
				<tr>
					<td class="style7">
                        DTH ID :</td>
					<td style="width: 814px"><asp:Label id=lblRPHId runat=server /></td>
					<td style="width: 46px">&nbsp;</td>
					<td width="15%">Period :</td>
					<td width="20%"><asp:Label id=lblAccPeriod runat=server />&nbsp;</td>
				</tr>
				<tr>
					<td class="style7">
                        DTH Date :*</td>
					<td style="width: 814px"><asp:TextBox id=txtRPHDate width="37%" maxlength=10 
                            CssClass="fontObject" runat=server />
						<a href="javascript:PopCal('txtRPHDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>						
						<asp:Label id=lblErrRPHDate visible=False forecolor=red text="<br>Invalid date format." runat=server />
						<asp:RequiredFieldValidator 
							id="rfvRPHDate" 
							runat="server" 
							ErrorMessage="<br>Please key in RPH Date" 
							ControlToValidate="txtRPHDate" 
							display="dynamic"/></td>
					<td style="width: 46px;">&nbsp;</td>
					<td>Status :</td>
					<td><asp:Label id=lblStatus runat=server /></td>
					
				</tr>			
				<tr>
					<td height="25" class="style7">
                        DTH Type :</td>
					<td style="width: 814px"><asp:label id=lblRPHTypeName runat=server />
						<asp:label id=lblRPHType runat=server Visible="False" /></td>
					<td style="width: 46px">&nbsp;</td>
					<td>Date Created :</td>
					<td>&nbsp;<asp:Label id=lblCreateDate runat=server /></td>
					
				</tr>	
				<tr>
					<td class="style7">Dept Code :*</td>
					<td style="width: 814px"><asp:DropDownList id=ddlDeptCode width="100%" CssClass="font9Tahoma" runat=server  />
						<asp:Label id=lblDeptCode text="Please select Dept Code" forecolor=red visible=false runat=server /></td>
					<td style="width: 46px">&nbsp;</td>
					<td>Last Update :</td>
					<td>&nbsp;<asp:Label id=lblUpdateDate runat=server /></td>
					
				</tr>			
				<tr>
					<td class="style7">
                        DTH Issued Location :*</td>
					<td style="width: 814px"><asp:DropDownList id=ddlPOIssued width="100%" CssClass="font9Tahoma"  runat=server  /> 
						<asp:Label id=lblPOIssued text="Please select RPH Issued Location" forecolor=red visible=false runat=server /></td>
					<td style="width: 46px">&nbsp;</td>
					<td>Updated By :</td>
					<td><asp:Label id=lblUpdateBy runat=server /></td>
					
				</tr>			
				<tr>
					<td class="style7">
                        Tempat Penyerahan :</td>
					<td style="width: 814px">
                        <asp:DropDownList ID="DDLLocPenyerahan" CssClass="font9Tahoma"  runat="server" AutoPostBack="False" OnSelectedIndexChanged="LocIndexChanged"
                            Width="100%">
                        </asp:DropDownList><asp:Label ID="lblTptPenyerahan" CssClass="font9Tahoma"  runat="server" ForeColor="Red"
                            Text="Please Select Tempat Penyerahan" Visible="False"></asp:Label></td>
					<td style="width: 46px">&nbsp;</td>
					<td>Print Date :</td>
					<td><asp:Label id=lblPrintDate runat=server /></td>		
					
				</tr>			
				<tr>
					<td class="style7">Currency Code :</td>
					<td style="width: 814px"><asp:DropDownList id=ddlCurrency CssClass="font9Tahoma"  
                            width="37%" runat=server/></td>
					<td style="width: 46px">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
					
				</tr>
				<tr>
					<td class="style7">Exchange Rate :</td>
					<td style="width: 814px"><asp:TextBox id=txtExRate text="1" width="37%" 
                            maxlength=20 CssClass="fontObject"  runat=server /></td>
					<td style="width: 46px;">&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>				
					
				</tr>		
                <tr>
                    <td class="style7">
                        Centralized :</td>
                    <td style="width: 814px">
                        <asp:CheckBox id="chkCentralized" Text="  Yes" checked=true AutoPostBack=true OnCheckedChanged=Centralized_Type runat=server Enabled="False" />
                        <asp:CheckBox id="ChkPPN" checked=false runat=server Visible="False" /></td>
                    <td style="width: 46px">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="text-align: right">
						<table id=tblRPHLine width="100%" cellspacing="1" cellpadding="1" border="0" align="center" class="font9Tahoma"  runat=server >
							<tr>						
								<td class="style1">

                                    <asp:label id=lblRphLNID visible=true runat=server /></td>

							</tr>
						</table>
                        </td>
                </tr>			
			</table>

	        <table width="99%" id=tblDetail class="sub-Add"  runat=server >
	    <tr>
	        <td> 
	        					
				<table id=tblPR width="100%" class="font9Tahoma" cellspacing="0" cellpadding="1" border="0" align="center" runat=server >
										<tr id=Centralized_Yes runat="server" style="color: #000000">
											<td class="style3"> </td>
											<td style="height: 14px;" >
												 <asp:Label id=lblErrPRID forecolor=red visible=false text="Please select Purchase Requisition ID " runat=server/>
                                                <asp:Label id=lblErrManySelectDoc forecolor=Red 
                                                     text="Note : Please select Purchase Requisition ID to add the line items or enter Purchase Requisition Ref. Information" 
                                                     runat=server/></td>
										</tr>
                    <tr id="Tr1" runat="server" visible="True">
                        <td class="style4">
                            Requisition Ref. <asp:label id="lblLocation" runat="server" /> :</td>
                        <td style="width: 932px">
                            <asp:DropDownList id=ddlPRRefLocCode width="88%" CssClass="fontObject"
                                runat=server onSelectedIndexChanged="LocIndexChanged" />
                                                <asp:Label id=lblErrRef forecolor=red visible=false runat=server /></td>
                    </tr>
                                        <tr runat="server" visible="True" id="Tr2">
                                            <td class="style5">
                                                Requisition ID :*</td>
                                            <td style="width: 932px; height: 3px">
                                                <asp:TextBox ID="txtPRID_Plmph" CssClass="fontObject"  runat="server" 
                                                    AutoPostBack="False" MaxLength="64"
                                                    Width="88%" ForeColor="Black" Height="19px" Font-Bold="True"></asp:TextBox>
                                                &nbsp;<input id="BtnViewPR"  runat="server" causesvalidation="False" onclick="javascript:PopPR_RPH('frmMain', '', 'txtItemCode','TxtItemName','txtPrCostOrder','txtPrQtyOrder','txtPRID_Plmph','txtPrTCostOrder','lblPRLocCode','txtAddNote','txtPRRefId','txtPurchUOM','False');"
                                                    type="button" value=" ... " /><input id="BtnViewSPK" runat="server" causesvalidation="False" onclick="javascript:PopPRSPK_RPH('frmMain', '', 'txtItemCode','TxtItemName','txtPrCostOrder','txtPrQtyOrder','txtPRID_Plmph','txtPrTCostOrder','lblPRLocCode','txtAddNote','txtPRRefId','txtPurchUOM','False');"
                                                    type="button" value=" ... " /></td>
                                        </tr>
                                        <tr runat="server" visible="true" id="Tr3">
                                            <td class="style5">
                                                Requisition Ref. No. :</td>
                                            <td style="width: 932px; height: 3px">
                                                <asp:TextBox id=txtPRRefId width="88%" maxlength=20 CssClass="fontObject"  
                                                    runat=server ForeColor="Black" Height="19px" Font-Bold="True" /></td>
                                        </tr>
										<tr id=Centralized_No visible=false runat="server">
											<td class="style5"> Requisition ID :*</td>
											<td style="height: 3px; width: 932px;" ><asp:TextBox id=txtPRID width="70%" maxlength=30 runat=server Height="19px" CssClass="fontObject" Font-Bold="True" />
											     <asp:Label id=lblErrTxtPRID forecolor=red text="Please insert PRID" runat=server /></td>
										</tr>
										<tr>
											<td class="style4"><asp:label id="lblStockItem" runat="server" /> :*</td>
											<td style="width: 932px;" >
                                                <asp:TextBox ID="txtItemCode" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="15"
                                                    Width="20%" Height="19px" Font-Bold="True"></asp:TextBox>
                                                &nbsp;<asp:TextBox ID="TxtItemName"  CssClass="fontObject" runat="server" MaxLength="128"
                                                        Width="67%" Height="19px" Font-Bold="True"></asp:TextBox>&nbsp;<input type=button value=" ... " id="FindDC"  Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'False');" CausesValidation=False runat=server />
    										    <input type=button value=" ... " id="FindWS" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'False');" CausesValidation=False runat=server />
    										    <input type=button value=" ... " id="FindNU" Visible=False onclick="javascript:PopItem('frmMain', '', 'ddlItemCode', 'False');" CausesValidation=False runat=server />
												<asp:Label id=lblErrItem forecolor=red visible=false text="Please select one Stock Item" runat=server />
												<asp:Label id=lblErrItemExist forecolor=red visible=false text="Stock Item already exist." runat=server /></td>
										</tr>                                                        															
                    <tr>
                        <td class="style6">
                          Replace With:  </td>
                        <td style="width: 932px; height: 21px">
                            <asp:TextBox ID="txtItemCode_Rep" CssClass="fontObject"   runat="server" AutoPostBack="False" Height="19px" MaxLength="15"
                                Width="20%" Font-Bold="True"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="TxtItemName_Rep" CssClass="fontObject"  runat="server" 
                                Height="19px" MaxLength="128" Width="67%" Font-Bold="True"></asp:TextBox>

                                                &nbsp;<input id="FindIN" runat="server" causesvalidation="False" 
                                                        onclick="javascript:PopPOItem_New('frmMain', '', 'txtItemCode_Rep','TxtItemName_Rep','txtPrCostOrder','txtPurchUOM', 'False');"
                                                        type="button" value=" ... " />
                            <asp:LinkButton ID="linkClear" runat="server" OnClick="linkClear_Click" Visible="False" >Clear Item</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Requisition <asp:label id="lblLocCode" runat="server" /> :</td>
                        <td style="width: 932px; height: 3px">
                            <asp:TextBox id=lblPRLocCode width="20%" maxlength=30 runat=server Height="19px" Font-Bold="True" /></td>
                    </tr>
                                        <tr>
                                            <td class="style5">
                                                Requisition Qty Order : *</td>
                                            <td style="width: 932px; height: 3px">
                                                <asp:TextBox ID="txtPrQtyOrder" CssClass="fontObject" runat="server" AutoPostBack="False" Height="21px" MaxLength="15"
                                                    Width="10%" BackColor="Transparent" BorderStyle="None" Font-Bold="True" ></asp:TextBox>&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; UOM :
                                                <asp:TextBox id=txtPurchUOM width="12%" maxlength=20 CssClass="fontObject"  runat=server Height="21px"   BackColor="Transparent" BorderStyle="None" Font-Bold="True" />
                                                &nbsp; &nbsp; &nbsp;&nbsp;
                                                Cost :&nbsp;
                                                <asp:TextBox ID="txtPrCostOrder" CssClass="fontObject"  runat="server" AutoPostBack="False" Height="21px"
                                                    MaxLength="15" Width="10%" BackColor="Transparent" BorderStyle="None" Font-Bold="True" ></asp:TextBox>&nbsp; &nbsp;&nbsp; Total Cost :&nbsp;
                                                <asp:TextBox ID="txtPrTCostOrder" CssClass="fontObject"  runat="server" AutoPostBack="False" Height="21px" MaxLength="15"
                                                    Width="10%" BackColor="Transparent" BorderStyle="None" Font-Bold="True"  ></asp:TextBox>
                                                <asp:DropDownList id=ddlPurchUom1 width=23%  CssClass="font9Tahoma"  runat=server Visible="False" /></td>
                                        </tr>
                                        <tr>
                                            <td class="style5">
                                            </td>
                                            <td style="width: 932px; height: 3px" valign="top">
                                                <asp:ImageButton
                                                    ID="ImgGen" runat="server" AlternateText="ImgGenerate" CssClass="button-form" CausesValidation="false"
                                                    ImageUrl="../../Images/butt_generate.gif" OnClick="btnGenerate_Click" UseSubmitBehavior="false" ToolTip="Click to Generate All PR Item" /></td>
                                        </tr>
									</table>
			<br />	
                      
				<table id="tblTabulasi" border="0" width="100%" class="font9Tahoma" cellspacing="0" cellpadding="1">  								
							<igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft" 
                            SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" Width="100%" ForeColor=black BackColor=transparent runat="server">
                            <DefaultTabStyle Height="22px">
                            </DefaultTabStyle>
                            <HoverTabStyle CssClass="ContentTabHover">
                            </HoverTabStyle>
                            <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                            NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                            FillStyle="LeftMergedWithCenter" ></RoundedImage>
                            <SelectedTabStyle CssClass="ContentTabSelected">
                            </SelectedTabStyle>
                        <Tabs>
                        
                        <igtab:Tab Key="SUP1" Text="SUPPLIER 1" Tooltip="PLEASE INPUT SUPPLIER 1">
                                <ContentPane>                                		                 
                        <table style="width: 100%"  class="font9Tahoma" cellspacing="0" cellpadding="1">
             										<tr>
											<td style="height: 27px; width: 19%;">Supplier Code 1 :*</td>
											<td style="height: 27px;">
                                                <asp:TextBox ID="txtSuppCode1" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="16" Width="10%"  Height="21px"></asp:TextBox>
                                                <asp:TextBox ID="txtSuppName1"  CssClass="fontObject"  runat="server"   ForeColor="Black" MaxLength="128"
                                                    Width="55%" Height="21px"></asp:TextBox>                                                
                                                <asp:Button ID="BtnFindSup1" OnClick="BtnSup1Find_Click" CssClass="button-small" runat="server" Text="Find" Font-Bold=true />
                                                <asp:Label id=lblErrSuppCode1 forecolor=red text="Please select one Supplier" runat=server /><asp:CheckBox id=ChkSupp1 checked=false runat=server />
											</td>
											  <td style="width: 222px; height: 27px;">
                        </td>								
									</tr>
                                        <tr>
                                            <td style="height: 1px">
                                            </td>
                                            
                            <td style="width: 932px; height: 1px">                                                                                       
                            <asp:DataGrid ID="dgLine" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="2" GridLines="None" 
                            OnItemCommand="OnSelectItem" 
                             OnItemDataBound="dgLine_BindGrid"
                             PagerStyle-Visible="False" Width="65%" class="font9Tahoma">	

                            <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>	
                            <Columns>                            
                                <asp:BoundColumn DataField="SupplierCode" HeaderText="Supplier Code" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Name" HeaderText="Name" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                                <asp:HyperLinkColumn DataTextField="SupplierCode" HeaderText="Supplier Code" SortExpression="SupplierCode">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="CreditTerm" HeaderText="Credit Term" SortExpression="CreditTerm">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="PPNInit" HeaderText="PPN" SortExpression="PPNInit">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                            <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Button ID="BtnSup1Close" OnClick=BtnSup1Close_Click runat="server" Text="X" Font-Bold=true />
                                    </HeaderTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Visible="False" />
                        </asp:DataGrid>
                                                                                                       
                                            </td>
                                        
                                        <td style="width: 222px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25">
                                                PPN :*</td>
                                            <td style="width: 932px">
                                                <asp:DropDownList ID="ddlPPN1" CssClass="font9Tahoma" runat="server" Width="10%">
                                                </asp:DropDownList></td>
                                            <td width="1%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
										<tr>
											<td style="height: 26px">Quantity Order :*</td>
											<td style="height: 26px; width: 932px;" ><asp:TextBox id=txtQtyOrder1 width=10% maxlength=14    CssClass="fontObject" runat=server />
												<asp:label id=lblErrQtyOrder1 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
											</td>
											<td width="1%" style="height: 26px">&nbsp;</td>
											<td style="height: 26px">&nbsp;</td>	
											<td style="height: 26px">&nbsp;</td>	
										</tr>
										<tr>
											<td height="25">Unit Cost :*</td>
											<td style="width: 932px"><asp:TextBox id=txtCost1 CssClass="fontObject" width=23% maxlength=21   runat=server />
												<asp:label id=lblErrCost1 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
									
										<tr>
											<td height="25">Total Cost :*</td>
											<td style="width: 932px"><asp:TextBox id=txtTtlCost1 width=23% maxlength=21    CssClass="fontObject" runat=server />
												<asp:label id=lblErrTtlCost1 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr style="visibility:hidden">
											<td height="25">Potongan/Discount :*</td>
											<td style="width: 932px"><asp:TextBox id=txtDiscount1 width=10% maxlength=5   CssClass="fontObject" runat=server />
											    <asp:label id=lblDiscount1 text="%" Runat="server" />
												<asp:label id=Label1 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>										
										<tr>
											<td>PBB-KB :*</td>
											<td style="width: 932px"><asp:TextBox id=txtPBBKB1 CssClass="fontObject"  width=10% maxlength=5 runat=server Font-Underline="True" />
											    <asp:label id=lblPercent1 text="%" Runat="server" />
                                                <asp:Label ID="Label5" runat="server" Text="Rate"></asp:Label>
                                                <asp:TextBox ID="txtPBBKBRate1" CssClass="fontObject" runat="server" MaxLength="4" Width="8%"></asp:TextBox>
                                                 
												<asp:label id=lblErrPBBKB1 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>										
										<tr valign="top">
											<td>PPH 22 :*</td>
											<td style="width: 932px"><asp:TextBox id=txtPPN221 CssClass="fontObject"  width=10% maxlength=5 runat=server />
											    <asp:label id=lblPercent4 text="%" Runat="server" />
												<asp:label id=lblErrPPN221 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
												<br /><br /> <asp:Button ID="BtnCalc1" CssClass="button-small" OnClick=BtnCalcSup1_Click runat="server" Text="CALC" Height=23px Width=45px Font-Bold=true />
											</td>
										</tr>									
									    <tr>
										    <td style="width: 19%">Supplier Note :</td>
					                        <td><asp:TextBox id=txtsuppNote1  CssClass="fontObject" TextMode=MultiLine width=70% Height="40" runat=server /></td>
										</tr>
										
										<!------------------------------------------->
                                        <tr>
                                            <td height="25">
                                                <strong><span style="text-decoration: underline">Ongkos Angkut</span></strong></td>
                                            <td style="width: 932px">
                                            </td>
                                        </tr>
  
                                        <tr>
                                            <td height="25">
                                                Supplier/Transporter :</td>
                                            <td style="width: 932px">
                                                <asp:TextBox ID="txttransporterCode1" CssClass="fontObject"  runat="server" AutoPostBack="False" MaxLength="15" Width="10%"></asp:TextBox>
                                                <asp:TextBox ID="txttransporterName1" CssClass="fontObject"  runat="server" Font-Bold="True" ForeColor="Black" MaxLength="128"
                                                    Width="55%"></asp:TextBox>
                                                    <asp:Label id=lblErrTransporter forecolor=red text="Please select one transporter" runat=server />
                                                <asp:Button ID="BtnFind1" OnClick="BtnTrans1Find_Click" CssClass="button-small" runat="server" Text="Find" Font-Bold=true />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 1px">
                                            </td>
                                                <td style="width: 932px; height: 1px">
                                                                            <asp:DataGrid ID="dgLineTrans1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    CellPadding="2" GridLines="None" 
                                                    OnItemCommand="OnSelectTrans1"
                                                     OnItemDataBound="dgLine_BindGrid"
                                                     PagerStyle-Visible="False" Width="65%" class="font9Tahoma">								
 								<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
                                                    <Columns>
                                                    
                                                        <asp:BoundColumn DataField="SupplierCode" HeaderText="Supplier Code" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Name" HeaderText="Name" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>
                                                        <asp:ButtonColumn CommandName="Select" Text="Select">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemStyle Width="10%" />
                                                        </asp:ButtonColumn>
                                                        <asp:HyperLinkColumn DataTextField="SupplierCode" HeaderText="Supplier Code" SortExpression="SupplierCode">
                                                            <HeaderStyle Width="20%" />
                                                            <ItemStyle Width="20%" />
                                                        </asp:HyperLinkColumn>
                                                        <asp:HyperLinkColumn DataTextField="Name" HeaderText="Name" SortExpression="Name">
                                                            <HeaderStyle Width="60%" />
                                                            <ItemStyle Width="60%" />
                                                        </asp:HyperLinkColumn>
                                                        <asp:HyperLinkColumn DataTextField="CreditTerm" HeaderText="Credit Term" SortExpression="CreditTerm">
                                                            <HeaderStyle Width="20%" />
                                                            <ItemStyle Width="20%" />
                                                        </asp:HyperLinkColumn>
                                                        <asp:HyperLinkColumn DataTextField="PPNInit" HeaderText="PPN" SortExpression="PPNInit">
                                                            <HeaderStyle Width="20%" />
                                                            <ItemStyle Width="20%" />
                                                        </asp:HyperLinkColumn>
                                                    <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:Button ID="Btntrans2Close" OnClick=BtnTrans1Close_Click runat="server" Text="X" Font-Bold=true />
                                                            </HeaderTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <PagerStyle Visible="False" />
                                                </asp:DataGrid></td>
                                                                    <td style="width: 222px">
                                                                    </td>
                                        </tr>
                                                                                
                                        <tr>
                                            <td height="25">
                                                PPN : *</td>
                                            <td style="width: 932px">
                                                <asp:DropDownList ID="DDLPPNTr1" CssClass="fontObject" runat="server" Width="10%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td height="25">
                                                Amount :</td>
                                            <td style="width: 932px">
                                                <asp:TextBox id=txtAmtTransportFee CssClass="fontObject" width="15%" runat=server /></td>
                                        </tr>

                                             
                                            </table>
				                </ContentPane>
			                    </igtab:Tab> 			                    			            
	                    <igtab:Tab Key="SUP2" Text="SUPPLIER 2" Tooltip="PLEASE INPUT SUPPLIER 2">
                        <ContentPane>  
                       <table style="width: 100%"  class="font9Tahoma"  cellspacing="0" cellpadding="1">
                           				<tr>
											<td style="width: 19%">Supplier Code 2 :*</td>
											<td>
                                                <asp:TextBox ID="txtSuppCode2" CssClass="fontObject" runat="server" AutoPostBack="False" MaxLength="16"
                                                    Width="10%" ></asp:TextBox>
                                                <asp:TextBox ID="txtSuppName2"  CssClass="fontObject"  runat="server"  ForeColor="Black"
                                                    MaxLength="128" Width="55%"></asp:TextBox>                                                
                                                    <asp:Button ID="FindSup2" OnClick="BtnSup2Find_Click" class="button-small" runat="server" Text="Find" Font-Bold=true />
												<asp:Label id=lblErrSuppCode2 forecolor=red text="Please select one Supplier" runat=server /><asp:CheckBox id=ChkSupp2 checked=false runat=server />
											</td>								
										</tr>
                            <tr>
                                            <td style="height: 1px">
                                            </td>
                                            <td style="width: 932px; height: 1px">
                                                    <asp:DataGrid ID="dgLineSup2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="2" GridLines="None" 
                            OnItemCommand="OnSelectSup2"
                             OnItemDataBound="dgLine_BindGrid"
                             PagerStyle-Visible="False" Width="65%" class="font9Tahoma">								
 								<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
                            <Columns>
                            
                                <asp:BoundColumn DataField="SupplierCode" HeaderText="Supplier Code" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Name" HeaderText="Name" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                                <asp:HyperLinkColumn DataTextField="SupplierCode" HeaderText="Supplier Code" SortExpression="SupplierCode">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="CreditTerm" HeaderText="Credit Term" SortExpression="CreditTerm">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="PPNInit" HeaderText="PPN" SortExpression="PPNInit">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                            <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Button ID="BtnSup2Close" OnClick="BtnSup2Close_Click" runat="server" Text="X" Font-Bold=true />
                                    </HeaderTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Visible="False" />
                        </asp:DataGrid></td>
                                            <td style="width: 222px">
                                            </td>
                                        </tr>										
                                        <tr>
                                            <td height="25">
                                                PPN :*</td>
                                            <td style="width: 932px">
                                                <asp:DropDownList ID="ddlPPN2"  CssClass="font9Tahoma"  runat="server" Width="10%">
                                                </asp:DropDownList></td>
                                            <td width="1%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
										<tr>
											<td height="25">Quantity Order :*</td>
											<td style="width: 932px" ><asp:TextBox id=txtQtyOrder2  CssClass="font9Tahoma"  width=10% maxlength=15 OnKeyUp="javascript:calTtlQtyCost2();" runat=server />
 
												<asp:label id=lblErrQtyOrder2 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
											</td>
											<td width="1%">&nbsp;</td>
											<td>&nbsp;</td>	
											<td>&nbsp;</td>	
										</tr>
										<tr>
											<td height="25">Unit Cost :*</td>
											<td style="width: 932px"><asp:TextBox id=txtCost2  CssClass="font9Tahoma"  width=23% maxlength=18 OnKeyUp="javascript:calTtlCost2();" runat=server />
 
												<asp:label id=lblErrCost2 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>		
										<!-- Minamas CR-MNS0701010065 14 Feb 2007 PRM-->
										<tr>
											<td height="25">Total Cost :*</td>
											<td style="width: 932px"><asp:TextBox id=txtTtlCost2  CssClass="font9Tahoma"  width=23% maxlength=18 OnKeyUp="javascript:calUnitCost2();" runat=server />
 
												<asp:label id=lblErrTtlCost2 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>
										<tr style="visibility:hidden">
											<td height="25">Potongan/Discount :*</td>
											<td style="width: 932px"><asp:TextBox id=txtDiscount2  CssClass="font9Tahoma"  width=10% maxlength=5 OnKeyUp="javascript:calTtlCost2();" runat=server />
 
												<asp:label id=Label2 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td>PBB-KB :*</td>
											<td style="width: 932px"><asp:TextBox id=txtPBBKB2  CssClass="font9Tahoma"  width=10% maxlength=5 runat=server />
											    <asp:label id=lblPercent2 text="%" Runat="server" />
	                                            <asp:Label ID="Label4" runat="server" Text="Rate"></asp:Label>
                                                <asp:TextBox ID="txtPBBKBRate2" CssClass="fontObject" runat="server" MaxLength="4" Width="8%"> </asp:TextBox>
												<asp:label id=lblErrPBBKB2 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>
										
										<tr valign="top">
											<td>PPH 22 :*</td>
											<td style="width: 932px"><asp:TextBox id=txtPPN222  CssClass="font9Tahoma"  width=10% maxlength=5 runat=server />
											    <asp:label id=lblPercent5 text="%" Runat="server" />
			 
												<asp:label id=lblErrPPN222 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												<br /><br /> <asp:Button ID="BtnCalc2" CssClass="button-small" OnClick=BtnCalcSup2_Click runat="server" Text="CALC" Height=23px Width=45px Font-Bold=true />												
											</td>
										</tr>
										<!------------------------------------------->										
									    <tr>
										    <td style="width: 19%">Supplier Note :</td>
					                        <td><asp:TextBox id=txtsuppNote2    TextMode=MultiLine width=70% Height="40" CssClass="font9Tahoma" runat=server /></td>
										</tr>
																				
                                        <tr>
                                            <td height="25">
                                                <strong><span style="text-decoration: underline">Ongkos Angkut</span></strong></td>
                                            <td style="width: 932px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25">
                                                Supplier/Transporter :</td>
                                            <td style="width: 932px">
                                                <asp:TextBox ID="txttransporter2" CssClass="font9Tahoma" runat="server" AutoPostBack="False" MaxLength="15" Width="10%"></asp:TextBox>
                                                <asp:TextBox ID="txttransporterName2" CssClass="font9Tahoma" runat="server" Font-Bold="True" ForeColor="Black" MaxLength="128"
                                                    Width="55%"></asp:TextBox>
                                                <asp:Button ID="FindTrans2" CssClass="button-small" OnClick="BtnTrans2Find_Click" runat="server" Text="Find" Font-Bold=true />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 1px">
                                            </td>
                                            <td style="width: 932px; height: 1px">
                                                    <asp:DataGrid ID="dgLineTrans2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="2" GridLines="None" 
                            OnItemCommand="OnSelectTrans2"
                             OnItemDataBound="dgLine_BindGrid"
                             PagerStyle-Visible="False" Width="65%" class="font9Tahoma">								
 								<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
                            <Columns>
                            
                                <asp:BoundColumn DataField="SupplierCode" HeaderText="Supplier Code" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Name" HeaderText="Name" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                                <asp:HyperLinkColumn DataTextField="SupplierCode" HeaderText="Supplier Code" SortExpression="SupplierCode">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="CreditTerm" HeaderText="Credit Term" SortExpression="CreditTerm">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="PPNInit" HeaderText="PPN" SortExpression="PPNInit">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                            <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Button ID="Btntrans2Close" CssClass="button-small" OnClick=BtnTrans2Close_Click runat="server" Text="X" Font-Bold=true />
                                    </HeaderTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Visible="False" />
                        </asp:DataGrid></td>
                                            <td style="width: 222px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td height="25">
                                                PPN : *
                                            </td>
                                            <td style="width: 932px">
                                                <asp:DropDownList ID="DDLPPNTr2" class="fontObject" runat="server" Width="10%">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 26px">
                                                Amount :</td>
                                            <td style="height: 26px; width: 932px;">
                                                <asp:TextBox id=txtAmtTransportFee2 width="15%" class="fontObject" runat=server /></td>
                                        </tr>                                        
                                        </table>
                        </ContentPane>
                        
			            </igtab:Tab> 	
			      	    <igtab:Tab Key="SUP3" Text="SUPPLIER 3" Tooltip="PLEASE INPUT SUPPLIER 3">
                        <ContentPane>                          
                        <table style="width: 100%"  class="font9Tahoma"  cellspacing="0" cellpadding="1">
                       					<tr>
											<td style="height: 21px; width: 19%">Supplier Code 3 :*</td>
											<td style="height: 21px;">
                                                <asp:TextBox ID="txtsuppCode3" CssClass="font9Tahoma" runat="server" AutoPostBack="False" MaxLength="15" Width="10%" Font-Bold="True" Height="21px"></asp:TextBox>
                                                <asp:TextBox ID="txtsuppName3" CssClass="font9Tahoma" runat="server" Font-Bold="True" ForeColor="Black" MaxLength="128"
                                                    Width="55%" Height="21px"></asp:TextBox>
                                                <asp:Button ID="FindSup3" OnClick="BtnSup3Find_Click" CssClass="button-small" runat="server" Text="Find" Font-Bold=true />
												<asp:Label id=lblErrSuppCode3 forecolor=red text="Please select one Supplier" runat=server /><asp:CheckBox id=ChkSupp3 checked=false runat=server />
											</td>								
										</tr>
										
                                        <tr>
                                            <td style="height: 1px">
                                            </td>
                                            <td style="width: 932px; height: 1px">
                                                    <asp:DataGrid ID="dgLineSup3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="2" GridLines="None" 
                            OnItemCommand="OnSelectSup3"
                             OnItemDataBound="dgLine_BindGrid"
                             PagerStyle-Visible="False" Width="65%" class="font9Tahoma">								
 								<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
                            <Columns>
                            
                                <asp:BoundColumn DataField="SupplierCode" HeaderText="Supplier Code" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Name" HeaderText="Name" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                                <asp:HyperLinkColumn DataTextField="SupplierCode" HeaderText="Supplier Code" SortExpression="SupplierCode">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="CreditTerm" HeaderText="Credit Term" SortExpression="CreditTerm">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="PPNInit" HeaderText="PPN" SortExpression="PPNInit">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                            <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Button ID="BtnSup3Close" OnClick="BtnSup3Close_Click" runat="server" Text="X" Font-Bold=true />
                                    </HeaderTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Visible="False" />
                        </asp:DataGrid></td>
                                            <td style="width: 222px">
                                            </td>
                                        </tr>	
                                        										
                                        <tr>
                                            <td height="25">
                                                PPN :*</td>
                                            <td style="width: 932px">
                                                <asp:DropDownList ID="ddlPPN3" CssClass="font9Tahoma" runat="server" Width="10%">
                                                </asp:DropDownList></td>
                                            <td width="1%">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        
										<tr>
											<td height="25">Quantity Order :*</td>
											<td style="width: 932px" ><asp:TextBox id=txtQtyOrder3 CssClass="font9Tahoma" width=10% maxlength=15 OnKeyUp="javascript:calTtlQtyCost3();" runat=server />
 
												<asp:label id=lblErrQtyOrder3 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
											</td>
											<td width="1%">&nbsp;</td>
											<td>&nbsp;</td>	
											<td>&nbsp;</td>	
										</tr>
										
										<tr>
											<td height="25">Unit Cost :*</td>
											<td style="width: 932px"><asp:TextBox id=txtCost3 CssClass="font9Tahoma" width=23% maxlength=18 OnKeyUp="javascript:calTtlCost3();" runat=server />
 
												<asp:label id=lblErrCost3 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
																	
										<tr>
											<td height="25">Total Cost :*</td>
											<td style="width: 932px"><asp:TextBox id=txtTtlCost3 CssClass="font9Tahoma" width=23% maxlength=18 OnKeyUp="javascript:calUnitCost3();" runat=server />
 
												<asp:label id=lblErrTtlCost3 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										
										<tr style="visibility:hidden">
											<td height="25">Potongan/Discount :*</td>
											<td style="width: 932px"><asp:TextBox id=txtDiscount3 CssClass="font9Tahoma" width=10% maxlength=5 OnKeyUp="javascript:calTtlCost3();" runat=server />
											    <asp:label id=lblDiscount3 text="%" Runat="server" />
 
												<asp:label id=Label3 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
												&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										
										<tr>
											<td>PBB-KB :*</td>
											<td style="width: 932px"><asp:TextBox id=txtPBBKB3 CssClass="font9Tahoma" width=10% maxlength=5 runat=server />
											    <asp:label id=lblPercent3 text="%" Runat="server" />
 
                                                <asp:Label ID="lblPBBKBRate" runat="server" Text="Rate"></asp:Label>
                                                <asp:TextBox ID="txtPBBKBRate3" CssClass="fontObject" runat="server" MaxLength="4" Width="8%"></asp:TextBox>
                                              
												<asp:label id=lblErrPBBKB3 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
											</td>
										</tr>
										
                                        <tr valign="top">
                                            <td>
                                                PPH 22 :*</td>
                                            <td style="width: 932px">
                                                <asp:TextBox id=txtPPN223 CssClass="font9Tahoma" width=10% maxlength=5 runat=server />
											    <asp:label id=lblPercent6 text="%" Runat="server" />
 
												<asp:label id=lblErrPPN223 text="Number generated is too big!" Visible=False forecolor=red Runat="server" />												
												
												<br /><br /> <asp:Button ID="BtnCalc3" OnClick=BtnCalcSup3_Click CssClass="button-small" runat="server" Text="CALC" Height=23px Width=45px Font-Bold=true />	
                                            </td>
                                        </tr>										
									    <tr>
										    <td style="width: 19%">Supplier Note :</td>
					                        <td><asp:TextBox id=txtsuppNote3 CssClass="font9Tahoma" TextMode=MultiLine width=70% Height="40" runat=server /></td>
										</tr>                                        
                                        <tr>
                                            <td>
                                                <strong><span style="text-decoration: underline">Ongkos Angkut</span></strong></td>
                                            <td style="width: 932px">
                                            </td>
                                        </tr>
										
								<tr>
												<td style="height: 27px">
                                                Supplier/Transporter :</td>	
                                                
                                                <td style="width: 222px">
                                                   <asp:TextBox ID="txttransporter3" CssClass="font9Tahoma" runat="server" AutoPostBack="False" MaxLength="15" Width="10%" Height="20px"></asp:TextBox>
                                                    <asp:TextBox ID="txttransporterName3"  CssClass="font9Tahoma" runat="server" Font-Bold="True" ForeColor="Black" MaxLength="128"
                                                                                Width="55%" Height="20px"></asp:TextBox>
                                                     <asp:Button ID="FindTrans3" OnClick="BtnTrans3Find_Click" CssClass="button-small" runat="server" Text="Find" Font-Bold=true /></td>
                                                <td style="width: 34px">
                                                </td>
                                    </tr>
										<!------------------------------------------->
                                        <tr>
                                            <td style="height: 26px">
                                                PPN : *</td>
                                            <td style="width: 932px; height: 26px">
                                                <asp:DropDownList ID="DDLPPNTr3" CssClass="fontObject" runat="server" Width="10%">
                                                </asp:DropDownList></td>
                                        </tr>
              
                                        <tr>
                                            <td style="height: 1px">
                                            </td>
                                            <td style="width: 932px; height: 1px">
                                                    <asp:DataGrid ID="dgLineTrans3" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CellPadding="2" GridLines="None" 
                            OnItemCommand="OnSelectTrans3"
                             OnItemDataBound="dgLine_BindGrid"
                             PagerStyle-Visible="False" Width="65%" class="font9Tahoma">								
 								<HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
                            <Columns>
                            
                                <asp:BoundColumn DataField="SupplierCode" HeaderText="Supplier Code" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Name" HeaderText="Name" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CreditTerm" HeaderText="Credit Term" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PPNInit" HeaderText="PPNInit" Visible="False"></asp:BoundColumn>
                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Width="10%" />
                                </asp:ButtonColumn>
                                <asp:HyperLinkColumn DataTextField="SupplierCode" HeaderText="Supplier Code" SortExpression="SupplierCode">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle Width="60%" />
                                    <ItemStyle Width="60%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="CreditTerm" HeaderText="Credit Term" SortExpression="CreditTerm">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                                <asp:HyperLinkColumn DataTextField="PPNInit" HeaderText="PPN" SortExpression="PPNInit">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle Width="20%" />
                                </asp:HyperLinkColumn>
                            <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <asp:Button ID="BtnTrans3Close" OnClick="BtnTrans3Close_Click" runat="server" Text="X" Font-Bold=true />
                                    </HeaderTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Visible="False" />
                        </asp:DataGrid></td>
                                            <td style="width: 222px">
                                            </td>
                                        </tr>
                                        	
                                        <tr>
                                            <td style="height: 26px">
                                                Amount :</td>
                                            <td style="height: 26px; width: 932px;">
                                                <asp:TextBox id=txtAmtTransportFee3 width="15%" CssClass="font9Tahoma" runat=server /></td>
                                        </tr>
                                        
                        </table>
                        </ContentPane>
                        </igtab:Tab> 	     
			            </Tabs>            			            
			            </igtab:UltraWebTab>
			</table>						 
                <table id=tblNote width="99%" class="mb-c" cellspacing="2" cellpadding="2" border="0" runat=server >
    <bR />
			<tr>
										    <td style="text-align: left; padding:2px" class="style2">
                                                Additional Note :</td>
					                        <td style="text-align: left; padding:2px">&nbsp;&nbsp; <asp:TextBox id=txtAddNote width="99%" CssClass="font9Tahoma" runat=server /></td>
										</tr>
										<tr>
											<td colspan=2 height="25"><asp:label id=lblValidationQtyOrder text="Quantity is exceeding the required amount" Visible=False forecolor=red Runat="server" />&nbsp;
                                                <asp:Label id=lblExistSuppCode forecolor=red text="This Supplier already selected, please select the other Supplier" Visible=False runat=server />&nbsp;
                                                <asp:Label id=lblCheckBoxError text="Please ticked the checkbox before you press Add Button" visible=false forecolor=red runat="server" /></td>
										</tr>																											
										<tr>
											<td colspan=2><asp:Imagebutton id="btnAdd" OnClick="btnAdd_Click" ImageURL="../../images/butt_add.gif" AlternateText=Add Runat="server" />&nbsp;<br />
            
            <table style="width: 100%">
  		
				<tr>
					<td colspan=5>
					<div id="divgd" style="width:99%;height:350px;overflow: auto;">   	
						<asp:DataGrid id=dgRPHDet
							AutoGenerateColumns="False" width="100%" runat="server"
							GridLines=None
							Cellpadding="1"
							Pagerstyle-Visible="False"
							OnDeleteCommand="DEDR_Delete"
							OnCancelCommand="DEDR_Cancel"
							OnEditCommand="DEDR_Edit"
						    OnUpdateCommand="DEDR_Update"
						    OnItemDataBound="dgLine_BindGrid" 
							AllowSorting="True" class="font9Tahoma" >
                            <HeaderStyle CssClass="mr-h"   Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle CssClass="mr-l"  Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle CssClass="mr-r"  Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							
							<Columns>
							<asp:TemplateColumn HeaderText="NO">
                                            <ItemTemplate>                                                
                                                <asp:Label ID="lblNo" runat="server"></asp:Label>&nbsp;                                                                                       
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                             </asp:TemplateColumn>
                                        
								<asp:BoundColumn Visible=False DataField="RPHLnId" />
								<asp:BoundColumn Visible=False DataField="ItemCode" />
								<asp:BoundColumn Visible=False DataField="QtyOrder1" />
								<asp:BoundColumn Visible=False DataField="QtyReceive1" />
								<asp:BoundColumn Visible=False DataField="PRLocCode" />
								<asp:BoundColumn Visible=False DataField="PRRefLocCode" />
								<asp:TemplateColumn HeaderText="PR ID">
									<ItemStyle Width="8%"/>								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("PRID") %> id="lblPRId" runat="server" />
										<br />
										<asp:Label Text=<%# Container.DataItem("RPHLnId") %> id="lblRPHLnDetId" 
                                            runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Item &lt;br&gt;Ongkos Angkut">
								    <HeaderStyle HorizontalAlign="Left" /> 
									<ItemStyle Width="17%" HorizontalAlign="Left" /> 								
									<ItemTemplate>
										<asp:Label Text=<%# Container.DataItem("Description") %> id="lblItemCode" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("AmtTransportFee"), 2) %> id="lblTransportFee" runat="server" />
                                        <br />
                                        <asp:Label id="lblItemRepNote" runat="server" /><br />                                        
                                        <asp:Label Text=<%# Container.DataItem("ItemCode_Repl") %> id="lblItemCode_Repl" BackColor="yellow" runat="server" />
                                        <asp:Label Text=<%# Container.DataItem("ItemRep_Description") %> id="lblDesc_Repl"  BackColor="yellow" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Supplier 1 &lt;br&gt;Order Quantity">
									<HeaderStyle HorizontalAlign="Left" /> 
									<ItemStyle Width="10%" HorizontalAlign="Left" /> 
									<ItemTemplate>
										<asp:CheckBox id="chkDetSup1" Checked=false runat="server"/>
										<asp:Label Text=<%# Container.DataItem("SuppName1") %> id="lblSuppName1" width=100 runat="server" />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOrder1"), 5) %> id="lblQtyOrder1" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost &lt;br&gt;Discount">
									<HeaderStyle HorizontalAlign="Left" /> 
									<ItemStyle Width="10%" HorizontalAlign="Left" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("CostToDisplay1"), 0)) %> id="lblCost1" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Discount1"), 2) %> 
                                            id="lblDiscount4" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Supplier 2 &lt;br&gt;Order Quantity">
									<HeaderStyle HorizontalAlign="Left" /> 
									<ItemStyle Width="10%" HorizontalAlign="Left" /> 
									<ItemTemplate>
										<asp:CheckBox id="chkDetSup2" Checked=false runat="server"/>
										<asp:Label Text=<%# Container.DataItem("SuppName2") %> id="lblSuppName2" width=100 runat="server" />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOrder2"), 5) %> id="lblQtyOrder2" runat="server" />
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost &lt;br&gt;Discount">
									<HeaderStyle HorizontalAlign="Left" /> 
									<ItemStyle Width="7%" HorizontalAlign="Left" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("CostToDisplay2"), 0)) %> id="lblCost2" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Discount2"), 2) %> 
                                            id="lblDiscount5" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>															
								<asp:TemplateColumn HeaderText="Supplier 3 &lt;br&gt;Order Quantity">
									<HeaderStyle HorizontalAlign="Left" /> 
									<ItemStyle Width="10%" HorizontalAlign="Left" /> 
									<ItemTemplate>
										<asp:CheckBox id="chkDetSup3" Checked=false runat="server"/>
										<asp:Label Text=<%# Container.DataItem("SuppName3") %> id="lblSuppName3" width=100 runat="server" />
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("QtyOrder3"), 5) %> id="lblQtyOrder3" runat="server" />										
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Unit Cost &lt;br&gt;Discount">
									<HeaderStyle HorizontalAlign="Left" /> 
									<ItemStyle Width="7%" HorizontalAlign="Left" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator(FormatNumber(Container.DataItem("CostToDisplay3"), 0)) %> id="lblCost3" runat="server" /><br>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(Container.DataItem("Discount3"), 2) %> 
                                            id="lblDiscount6" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>															
								<asp:TemplateColumn HeaderText="Amount">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="5%" HorizontalAlign="Right" /> 
									<ItemTemplate>
										<asp:Label Text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("TotalAmountToDisplay"), 2), 2) %> id="lblTotalAmount" runat="server" />
									</ItemTemplate>							
								</asp:TemplateColumn>									
								<asp:TemplateColumn>		
									<ItemStyle Width="5%" HorizontalAlign="Right" /> 
									<ItemTemplate>
									    <asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation=False  runat="server"/>
									    <asp:LinkButton id="Update" CommandName="Update" Text="Update" CausesValidation=False  runat="server"/>
										<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation=False runat="server"/>
										<asp:LinkButton id="Cancel" CommandName="Cancel" Text="Cancel" CausesValidation=False  runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>						
							</Columns>										
                            <PagerStyle Visible="False" />
						</asp:DataGrid>
					</div>	
					</td>	
				</tr>
				<TR>
					<TD colspan=2></TD>
					<TD colspan=2>
                        <hr style="width :100%" />
                         </TD>
					<td>&nbsp;</td>					
				</TR>
				<TR>
					<td colspan=2></td>
					<TD height=25 align=left></TD>
					<TD Align=right><asp:Label ID=lblTotalAmountAll CssClass="font9Tahoma" Runat=server Visible="False" />
                        <asp:TextBox ID=txtTtlAftDisc text="0" ForeColor=black Enabled=false Runat=server Visible="False" /></TD>
				</TR>
				<TR class="font9Tahoma">
					<td colspan=2></td>
					<TD height=25 align=left>Additional Discount :</TD>
					<td Align=right class="font9Tahoma">&nbsp;<asp:TextBox id=txtAddDisc text="0" maxlength=18 OnKeyUp="javascript:calAddDiscount();" CssClass="fontObject "  runat=server /><asp:RegularExpressionValidator id="RegularExpressionValidator4" 
 
						<asp:label id=lblerrAddDisc text="Number generated is too big!" Visible=False forecolor=red Runat="server" />
					</td>
				</TR>
				<tr>
					<td style="height: 26px" valign="top" class="font9Tahoma">Remarks :</td>	
					<td colspan="4" style="height: 26px"><asp:TextBox id="txtRemark" maxlength="512" width="97%" CssClass="fontObject" runat="server" Height="19px" /></td>
				</tr>
       
            
				<tr>
					<td colspan=5 height="25" class="font9Tahoma"><asp:label id=lblErrSelected text="There are unselected supplier winner. <br> To confirm and create PO, please pick one supplier per item or remove item from the list." Visible=False forecolor=red Runat="server" /><br />
                        <asp:CheckBox ID="chkPrinted" runat="server" Checked="false" Text="  Mark as printed" /></td>
				</tr>
				<tr>
					<td align="left" colspan="5"> 
						<asp:ImageButton id="btnSave" UseSubmitBehavior="false" onClick="btnSave_Click" ImageUrl="../../images/butt_save.gif" AlternateText=Save CausesValidation=False runat="server" />
                        <asp:ImageButton ID="btnEdited" runat="server" AlternateText="Edit" CausesValidation="False"
                            onClick="btnEdit_Click"   ImageUrl="../../images/butt_edit.gif" UseSubmitBehavior="false" Visible="False" />
                        <asp:ImageButton ID="BtnVerified" runat="server" AlternateText="Verified" ImageUrl="../../images/butt_verified.gif"
                            OnClick="btnVerified_Click" UseSubmitBehavior="false" />
						<asp:ImageButton id="btnConfirm" UseSubmitBehavior="false" onClick="btnConfirm_Click" ImageUrl="../../images/butt_approve.gif" AlternateText=Approve CausesValidation=False runat="server" />
						<asp:ImageButton id="btnPrint" UseSubmitBehavior="false" onClick="btnPreview_Click" ImageUrl="../../images/butt_print.gif" AlternateText=Print CausesValidation=False runat="server" />
						<asp:ImageButton id="btnDelete" UseSubmitBehavior="false" onClick="btnDelete_Click" ImageUrl="../../images/butt_delete.gif" AlternateText=Delete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnUndelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" AlternateText=Undelete CausesValidation=False runat="server" />
						<asp:ImageButton id="btnBack" UseSubmitBehavior="false" onClick="btnBack_Click" ImageUrl="../../images/butt_back.gif" AlternateText=Back CausesValidation=False runat="server" />
                        <asp:TextBox ID="txtPPNInit" runat="server" BackColor="Transparent" BorderStyle="None"
                            Width="9%" ForeColor="Transparent"></asp:TextBox>
                        <asp:TextBox ID="txtCreditTerm" runat="server" BackColor="Transparent"
                                BorderStyle="None" Width="9%" ForeColor="Transparent"></asp:TextBox>
                        <asp:TextBox ID="txtPPN" runat="server" BackColor="Transparent" BorderStyle="None"
                            Width="9%" ForeColor="Transparent"></asp:TextBox>
                        <asp:TextBox ID="txtRphTipe" runat="server" BackColor="Transparent" BorderStyle="None"
                            Width="9%" ForeColor="Transparent"></asp:TextBox></td>
				</tr>		
				    <tr>
					<td colspan=5>
						<asp:DataGrid id=dgRPHPO
							AutoGenerateColumns="false" width="30%" runat="server"
							GridLines=none
							Cellpadding="1"
							Pagerstyle-Visible="False"
							AllowSorting="false" class="font9Tahoma">
                        	
                            <HeaderStyle  BackColor="#CCCCCC" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<ItemStyle BackColor="#FEFEFE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>
							<AlternatingItemStyle BackColor="#EEEEEE" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False"/>		
							<Columns>
								<asp:HyperLinkColumn HeaderText="PO Created" 
									SortExpression="POID" 
									DataNavigateUrlField="POID" 
									DataNavigateUrlFormatString="PU_trx_PODet.aspx?POId={0}"
									DataTextFormatString="{0:c}"
									DataTextField="POID" />
			                    
			                    <asp:TemplateColumn HeaderText="PO ID" Visible="False">
									<HeaderStyle HorizontalAlign="Right" /> 
									<ItemStyle Width="8%"  HorizontalAlign="Right" />								
									<ItemTemplate>										
										<asp:Label Text=<%# Container.DataItem("POID") %> id="lblPOID" runat="server" /><br>
									</ItemTemplate>
								</asp:TemplateColumn>
																										
							</Columns>
						</asp:DataGrid>

                        <br />

                        <br />

                     <telerik:RadInputManager  ID="RadInputManager1" runat="server">
                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior1" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtQtyOrder1" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior2" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtCost1" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>      

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior3" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtTtlCost1" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>      

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior4" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPBBKB1" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior5" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtDiscount1" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior6" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtAmtTransportFee" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>   

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior7" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPPN221" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior8" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtQtyOrder2" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior9" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtCost2" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior10" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtTtlCost2" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior11" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtDiscount2" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior12" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPBBKB2" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>  

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior13" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPPN222" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>    

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior14" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtAmtTransportFee2" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>    
                        

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior15" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtQtyOrder3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>    

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior16" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtCost3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>      


                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior17" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtCost3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>         
                        

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior18" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtTtlCost3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>         
                        

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior19" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtDiscount3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>         
                        

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior20" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPBBKB3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>       
                        

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior21" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPPN223" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>      

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior22" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtAddDisc" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>      

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior23" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtAmtTransportFee3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>   
                        
                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior24" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPBBKBRate1" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>     

                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior25" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPBBKBRate2" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>     


                         <telerik:NumericTextBoxSetting BehaviorID="NumericBehavior26" EmptyMessage="please type">
                            <TargetControls>
                                <telerik:TargetInput ControlID="txtPBBKBRate3" />
                            </TargetControls>
                        </telerik:NumericTextBoxSetting>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
                    </telerik:RadInputManager>
 

 
				<br />
                </td>
				</tr>
            </table>

                                        </td>
									</tr>
            </table>					    

           </td>
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