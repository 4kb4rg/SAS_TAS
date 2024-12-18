<%@ Page Language="vb"  codefile="../../../include/GL_trx_PDODet.aspx.vb" Inherits="GL_trx_PDODet" %>

<%@ Register TagPrefix="UserControl" Tagname="MenuGLTrx" src="../../menu/menu_GLtrx.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebTab.v7.3" Namespace="Infragistics.WebUI.UltraWebTab"
  TagPrefix="igtab" %>
<html>
	<head>
		<title>PERMINTAAN DROPING DANA</title>		
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<script language="javascript">
			function gotFocusCR() {
			    var doc = document.frmMain;
			    var dbAmt = parseFloat(doc.hidTtlDBAmt.value);
			    var crAmt = parseFloat(doc.hidTtlCRAmt.value);
			    var dbDBCR = doc.hidDBCR.value;
			    var diffAmt = dbAmt+crAmt;
			    doc.txtDRTotalAmount.value = '';
                
                if (dbDBCR == '')
                    if (diffAmt > 0) 
                        doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2) + round(doc.hidCtrlAmtFig.value, 2);
                    else
                        doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2);
                else
                    if (dbDBCR == 'CR') 
                         doc.txtCRTotalAmount.value = round(doc.hidCRAmt.value, 2) + round(doc.hidCtrlAmtFig.value, 2);
                    else
                        doc.txtCRTotalAmount.value = round(doc.hidDBAmt.value, 2);
                    
	        }
	        function gotFocusDR() {
			    var doc = document.frmMain;
			    var dbAmt = parseFloat(doc.hidTtlDBAmt.value);
			    var crAmt = parseFloat(doc.hidTtlCRAmt.value);
			    var dbDBCR = doc.hidDBCR.value;
			    var diffAmt = dbAmt+crAmt;
			    doc.txtCRTotalAmount.value = '';
			    
			    if (dbDBCR == '') 
	                if (diffAmt > 0) 
	                    doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2);
	                else
                        doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2) + (round(doc.hidCtrlAmtFig.value, 2)*-1);
                else
                    if (dbDBCR == 'DR') 
                        doc.txtDRTotalAmount.value = round(doc.hidDBAmt.value, 2) + (round(doc.hidCtrlAmtFig.value, 2)*-1);
                    else
                        doc.txtDRTotalAmount.value = round(doc.hidCRAmt.value, 2);
	        }
	        
	        function lostFocusCR() {
			    var doc = document.frmMain;
			    var x = doc.txtCRTotalAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtCRTotalAmount.value = toCurrency(round(dbAmt, 2));
	        }
	        
	        function lostFocusDR() {
			    var doc = document.frmMain;
			    var x = doc.txtDRTotalAmount.value;
			    var dbAmt = parseFloat(x.toString().replace(/,/gi,""));
			    doc.txtDRTotalAmount.value = toCurrency(round(dbAmt, 2));
	        }
	        
	        function toCurrency(num) {
              num = num.toString().replace(/\$|\,/g, '')
              if (isNaN(num)) num = "0";
              sign = (num == (num = Math.abs(num)));
              num = Math.floor(num * 100 + 0.50000000001);
              cents = num % 100;
              num = Math.floor(num / 100).toString();
              if (cents < 10) cents = '0' + cents;

              for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
                num = num.substring(0, num.length - (4 * i + 3)) + ',' + num.substring(num.length - (4 * i + 3))
              }

              return (((sign) ? '' : '-') + num + '.' + cents)
            }
            
            function calTaxPriceCR() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmountCR.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));		    
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				
				doc.txtCRTotalAmount.value = newnumber;
				if (doc.txtCRTotalAmount.value == 'NaN')
					doc.txtCRTotalAmount.value = '';
				else
					doc.txtCRTotalAmount.value = doc.txtCRTotalAmount.value;
			}
			
			function calTaxPriceDR() {
				var doc = document.frmMain;
				var a = parseFloat(doc.txtDPPAmountDR.value);
				var b = parseFloat(doc.hidTaxObjectRate.value);		
				var c = (a * (b/100));		    
				var newnumber = new Number(c+'').toFixed(parseInt(0));
				
				doc.txtDRTotalAmount.value = newnumber;
				if (doc.txtDRTotalAmount.value == 'NaN')
					doc.txtDRTotalAmount.value = '';
				else
					doc.txtDRTotalAmount.value = doc.txtDRTotalAmount.value;
			}
			function gotFocusDPPCR() {
			    var doc = document.frmMain;
			    doc.txtDPPAmountDR.value = '';
			    doc.txtDRTotalAmount.value = '';
	        }
	        function gotFocusDPPDR() {
			    var doc = document.frmMain;
			    doc.txtDPPAmountCR.value = '';
			    doc.txtCRTotalAmount.value = '';
	        }
		</script>		
	    <style type="text/css">
            .style2
            {
                width: 198px;
            }
        </style>
	</head>
	
	<body >
		<form id=frmMain runat=server class="main-modul-bg-app-list-pu" >
        <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1000px" valign="top">
			    <div class="kontenlist">


                 
		<asp:label id=lblErrMessage visible=false Text="Error while initiating component." runat=server />
		<asp:label id="issueType" Visible="False" Runat="server" />
		<asp:label id="lblStsHid" Visible="False" Runat="server"/>
		<asp:label id="blnShortCut" Visible="False" Runat="server"/>
		<asp:label id=lblCode visible=false text=" Code" runat=server />
		<asp:label id=lblSelect visible=false text="Select " runat=server />
		<asp:label id=lblPleaseSelect visible=false text="Please select " runat=server />
		<asp:label id="blnUpdate" Visible="False" Runat="server"/>
		<asp:label id=lblTxLnID runat=server Visible="False" />

		<table border=0 width="100%" cellspacing="0" cellpadding="1" class="font9Tahoma">
			<tr>
				<td colspan=6><UserControl:MenuGLTrx EnableViewState=False id=menuIN runat="server" /></td>
			</tr>			
			<tr>
				<td class="mt-h" colspan=6>
                  <strong>  PERMINTAAN DROPPING DANA</strong></td>
			</tr>
			<tr>
				<td colspan=6>
                     <hr style="width :100%" />
                </td>
			</tr>			
			<tr>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td style="width: 198px">&nbsp;</td>
				<td><asp:Label id=lblReprint  Text="<B>( R E P R I N T )</B><br>" Visible=False forecolor=Red runat=server />
				</td>				
			</tr>		
			<tr>
				<td width="20%" height=25>Transaction ID :</td>
				<td width="40%"><asp:label id=lblTxID Runat="server"/></td>
				<td width="5%">&nbsp;</td>
				<td width="15%">Period : </td>
				<td style="width: 198px"><asp:Label id=lblAccPeriod runat=server /></td>
				<td width="5%">&nbsp;</td>
			</tr>
			<tr>
				<td height=25>Description :*</td>
                <td rowspan="3">
                    <asp:Textbox id="txtDesc" Width=100% MaxLength=128 runat=server Height="56px" TextMode="MultiLine" />
 
                </td>
				<td>&nbsp;</td>
				<td>Status : </td>
				<td style="width: 198px"><asp:Label id=Status runat=server /></td>
				<td>&nbsp;</td>
			</tr>			
            <tr>
                <td height="25">
                </td>
                <td>
                </td>
                <td>
                    Date Created : 
                </td>
                <td style="width: 198px">
                    <asp:Label id=CreateDate runat=server /></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 27px">
                </td>
                <td style="height: 27px">
                </td>
                <td style="height: 27px">
                    Last Update :</td>
                <td style="width: 198px; height: 27px;">
                    <asp:Label id=UpdateDate runat=server /></td>
                <td style="height: 27px">
                </td>
            </tr>
			<tr>
				<td style="height: 25px">
                    Document Ref. Date&nbsp; :*</td>
                <td rowspan="3" valign="top">
                    <asp:TextBox id=txtDate Width="20%" maxlength=10  CssClass="fontObject" runat=server />
					<a href="javascript:PopCal('txtDate');"><asp:Image id="btnSelDate" runat="server" ImageUrl="../../Images/calendar.gif"/></a>
					<asp:label id=lblFmt  forecolor=red Visible = false Runat="server"/><br />
					<asp:label id=lblDate Text ="<BR>Date entered should be in the format " forecolor=Red Visible = False Runat="server" Height="16px" Width="224px"/> 
					<asp:RequiredFieldValidator 
						id="validateDate" 
						runat="server" 
						ErrorMessage="<br>Please enter document date" 
						EnableClientScript="True"
						ControlToValidate="txtDate" 
						display="dynamic"/></td>
				<td style="height: 25px">&nbsp;</td>
			    <td style="height: 25px">Updated By :</td>
				<td style="width: 198px; height: 25px"><asp:Label id=UpdateBy runat=server /></td>
				<td style="height: 25px">&nbsp;</td>
			</tr>			
            <tr>
                <td style="height: 25px">
                </td>
                <td style="height: 25px">
                </td>
                <td style="height: 25px">
                    Print Date :</td>
                <td style="width: 198px; height: 25px">
                    <asp:Label id=lblPrintDate visible="false" runat=server /></td>
                <td style="height: 25px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="style2">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 25px">
                    To :</td>
                <td style="height: 25px">
                    <asp:TextBox id=TxtTo Width="100%" CssClass="fontObject" maxlength=128 runat=server  /></td>
                <td style="height: 25px">
                </td>
                <td style="height: 25px">
                </td>
                <td style="width: 198px; height: 25px">
                </td>
                <td style="height: 25px">
                </td>
            </tr>
            <tr>
                <td style="height: 25px">
                    From :</td>
                <td style="height: 25px">
                    <asp:TextBox id=TxtFrom Width="100%" maxlength=128  CssClass="fontObject" runat=server  /></td>
                <td style="height: 25px">
                </td>
                <td style="height: 25px">
                </td>
                <td style="width: 198px; height: 25px">
                </td>
                <td style="height: 25px">
                </td>
            </tr>
            <tr>
                <td style="height: 25px">
                    Subject :</td>
                <td style="height: 25px">
                    <asp:TextBox id=TxtSubject Width="100%" maxlength=128  CssClass="fontObject" runat=server  /></td>
                <td style="height: 25px">
                </td>
                <td style="height: 25px">
                </td>
                <td style="width: 198px; height: 25px">
                </td>
                <td style="height: 25px">
                </td>
            </tr>
            <tr>
                <td style="height: 25px">
                    Step :</td>
                <td style="height: 25px">
                    <asp:DropDownList id="lstStep" Width="20%" AutoPostBack=true  CssClass="fontObject"  runat=server>
                    </asp:DropDownList>&nbsp;
                    <asp:label id=lblPDOStep text="Please enter PDO Step" Visible=False forecolor=Red Runat="server" /></td>
                <td style="height: 25px">
                </td>
                <td style="height: 25px">
                </td>
                <td style="width: 198px; height: 25px">
                </td>
                <td style="height: 25px">
                </td>
            </tr>
			<tr>
				<td style="height: 25px">
                    Remise Period :</td>
				<td style="height: 25px"><asp:DropDownList id="lstMonth" Width="20%" AutoPostBack=true  CssClass="fontObject"  runat=server>
                </asp:DropDownList>
				<asp:DropDownList id="lstYear" Width="20%" AutoPostBack=true  CssClass="fontObject"  runat=server>
					</asp:DropDownList>
                    <asp:ImageButton ID="btnAddAllItem" runat="server" AlternateText="Get Data"
                        CausesValidation="False" ImageAlign="AbsBottom" ImageUrl="../../images/butt_generate.gif"
                        OnClick="BtnAddAllItem_Click" UseSubmitBehavior="false" 
                        ToolTip="Click Here To Make A PDD" /></td>
				<td style="height: 25px">&nbsp;</td>	
				<td style="height: 25px"></td>
				<td style="width: 198px; height: 25px"></td>	
				<td style="height: 25px">&nbsp;</td>	
			</tr>
			<tr>
				<td style="height: 25px" colspan="5">
                     <hr style="width :100%" />
                </td>
				<td style="height: 25px">&nbsp;</td>	
			</tr>
			<tr>
				<td style="height: 25px">
                    Saldo Bank :</td>
				<td style="height: 25px">
                                <asp:Textbox id="txtBalEnd"  CssClass="fontObject"  Width="40%" 
                        maxlength=22 onkeypress="javascript:return isNumberKey(event)" runat=server /></td>
				<td style="height: 25px">&nbsp;</td>	
				<td style="height: 25px">&nbsp;</td>
				<td style="width: 198px; height: 25px">&nbsp;</td>	
				<td style="height: 25px">&nbsp;</td>	
			</tr>
			<tr>
				<td style="height: 25px">
                    Pemakaian Saldo Bank :</td>
				<td style="height: 25px">
                                <asp:Textbox id="txtBalEndUse"  CssClass="fontObject"  Width="40%" 
                        maxlength=22 onkeypress="javascript:return isNumberKey(event)" runat=server /></td>
				<td style="height: 25px">&nbsp;</td>	
				<td style="height: 25px">&nbsp;</td>
				<td style="width: 198px; height: 25px">&nbsp;</td>	
				<td style="height: 25px">&nbsp;</td>	
			</tr>
			<tr>
				<td style="height: 25px">
                    Saldo Akhir Bank :</td>
				<td style="height: 25px">
                                <asp:Textbox id="txtBalEndAct"  CssClass="fontObject"  Width="40%" 
                        maxlength=22 onkeypress="javascript:return isNumberKey(event)" runat=server /></td>
				<td style="height: 25px">&nbsp;</td>	
				<td style="height: 25px">&nbsp;</td>
				<td style="width: 198px; height: 25px">&nbsp;</td>	
				<td style="height: 25px">&nbsp;</td>	
			</tr>
			<tr>
				<td>PDD :</td>
				<td>
                                <asp:Textbox id="txtPDDManual"  CssClass="fontObject"  Width="40%" 
                        maxlength=22 onkeypress="javascript:return isNumberKey(event)" runat=server 
                                    AutoPostBack="True" /></td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td style="width: 198px">&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>PDD Kirim :</td>
				<td>
                                <asp:Textbox id="txtPDDTrf"  CssClass="fontObject"  Width="40%" 
                        maxlength=22 onkeypress="javascript:return isNumberKey(event)" runat=server /></td>
				<td>&nbsp;</td>
				<td></td>
				<td style="width: 198px"></td>				
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td>&nbsp;</td>
				<td>
					&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td style="width: 198px">&nbsp;</td>				
				<td>&nbsp;</td>
			</tr>
            </table>


             <table width="99%" id="tblDetail" class="sub-Add" runat="server" >
			<tr>
				<td colspan="6">
					<table id="tblAdd" border="0" width="100%" cellspacing="0" cellpadding="4" runat="server">
						
						<tr class="mb-c">
                            <td style="width: 134px; height: 31px;">
                                Biaya :</td>
                            <td colspan="3" style="height: 31px">
                                <asp:DropDownList ID="lstgrppdo" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="lstgrppdo_OnSelectedIndexChanged"  CssClass="fontObject" Width="95%"> </asp:DropDownList>
					        </td>
                        </tr>
                        
						
						<tr class="mb-c">
							<td height=25 style="width: 134px">Deskripsi :</td>
							<td colspan=3><asp:TextBox id=txtDescLn Width="95%" maxlength=128  CssClass="fontObject" runat=server />	
											<asp:label id=lblDescErr text="Please enter Line Description" Visible=False forecolor=red Runat="server" /></td>
						</tr>
                        
						<tr class="mb-c">
                            <td height="25" style="width: 134px">
                                Qty / Satuan:</td>
                            <td colspan="3">
                                <asp:Textbox id="txtQty"  Width="20%" maxlength=22 onkeypress="javascript:return isNumberKey(event)"  CssClass="fontObject" runat=server />&nbsp;
								<asp:DropDownList id="ddluom"  Width="20%" maxlength=22  CssClass="fontObject" runat=server />&nbsp;
								
                            </td>
                        </tr>
                        <tr class="mb-c">
                            <td height="25" style="width: 134px">
                                PDD (Rp) :</td>
                            <td colspan="3">
                                <asp:Textbox id="txtDPPAmount"  CssClass="fontObject"  Width="20%" maxlength=22 onkeypress="javascript:return isNumberKey(event)" runat=server />&nbsp;
                                <asp:label id=lblPDOGroup Runat="server" Visible="False"/>&nbsp;<asp:label 
                                    id=lblTyEmp Runat="server" Visible="False"/>
                            </td>
                        </tr>
						<tr id="RowTax" visible=false class="mb-c">
							<td style="width: 134px; height: 49px;">
                                </td>
							<td colspan=3 style="height: 49px">
								<asp:RegularExpressionValidator 
									id="RegularExpressionValidator2" 
									ControlToValidate="txtDPPAmountDR"
									ValidationExpression="\d{1,19}\.\d{1,2}|\d{1,19}"
									Display="Dynamic"
									text = "<BR>Maximum length 19 digits and 2 decimal points"
									runat="server"/>
								<asp:RangeValidator 
									id="RangeValidator2"
									ControlToValidate="txtDPPAmountDR"
									MinimumValue="0.01"
									MaximumValue="9999999999999999999.99"
									Type="double"
									EnableClientScript="True" 
									Text="<BR>The value must be not be negative value"
									runat="server" display="dynamic"/></td>
						</tr>
                        <tr class="mb-c" visible="True">
                            <td style="width: 134px; height: 49px" valign="top">
                                 Note :</td>
                            <td colspan="3" style="height: 49px" valign="top">
                                <asp:TextBox id=TxtAddNote Width="95%" maxlength=128  CssClass="fontObject" runat=server /></td>
                        </tr>
						<tr class="mb-c">
							<td Colspan=2 style="height: 32px">
							              <asp:ImageButton AlternateText="Add" id="Add" ImageURL="../../images/butt_add.gif" OnClick="btnAdd_Click" UseSubmitBehavior="false" Runat="server" /> &nbsp;
							<td style="height: 32px">&nbsp;</td>						
							<td style="height: 32px">&nbsp;</td>	
						</tr>
	
					</table>
				</td>		
			</tr>


            </table>
            <table width="99%" id="Table1"  runat="server" >
            <tr> <td> </td> </tr><tr> <td> </td> </tr>
			<tr>
				<td align="left" colspan="6">
				    <asp:ImageButton id="NewBtn"  UseSubmitBehavior="false" onClick=NewBtn_Click     imageurl="../../images/butt_new.gif"     CausesValidation=False  AlternateText="New"     runat=server/>
					<asp:ImageButton id="Save"    UseSubmitBehavior="false" onClick=btnSave_Click    ImageURL="../../images/butt_save.gif"                            AlternateText="Save"    runat="server"  visible=False />
					<asp:ImageButton id="Delete"  UseSubmitBehavior="false" onClick=btnDelete_Click  ImageURL="../../images/butt_delete.gif"  CausesValidation=False  AlternateText="Delete"  runat="server"  />
					<asp:ImageButton id="Undelete" UseSubmitBehavior="false" onClick="btnUnDelete_Click" ImageUrl="../../images/butt_undelete.gif" CausesValidation=False AlternateText="Undelete"  runat="server"  visible=False />
					<asp:ImageButton id="Button1"  UseSubmitBehavior="false" onClick=btnPrintPDO_Click     imageurl="../../images/butt_print_PDO.gif"     CausesValidation=False  AlternateText="Print PDO"     runat=server/>
					<asp:ImageButton id="Button2"  UseSubmitBehavior="false" onClick=btnPrintLPJ_Click     imageurl="../../images/butt_print_LPJ_PDD.gif"     CausesValidation=False  AlternateText="Print LPJ PDD"     runat=server/>
					<asp:ImageButton id="Back"    UseSubmitBehavior="false" onClick=btnBack_Click    ImageURL="../../images/butt_back.gif"    CausesValidation=False  AlternateText="Back"    runat="server" />
				</td>
			</tr>
            </table>



             <br />
            <igtab:UltraWebTab ID="TABBK" ThreeDEffect="False" TabOrientation="TopLeft"
                        SelectedTab="0" Font-Names="Tahoma" Font-Size="8pt" ForeColor=black runat="server">
                        <DefaultTabStyle Height="22px">
                        </DefaultTabStyle>
                        <HoverTabStyle CssClass="ContentTabHover">
                        </HoverTabStyle>
                        <RoundedImage LeftSideWidth="6" RightSideWidth="6" SelectedImage="../../images/thumbs/ig_tab_winXP1.gif"
                        NormalImage="../../images/thumbs/ig_tab_winXP3.gif" HoverImage="../../images/thumbs/ig_tab_winXP2.gif"
                        FillStyle="LeftMergedWithCenter"></RoundedImage>
                        <SelectedTabStyle CssClass="ContentTabSelected">
                        </SelectedTabStyle>
                            <Tabs>
                                <igtab:Tab Key="PDD01" Text="DETAIL PDD" Tooltip="DETAIL PDD">
                                    <ContentPane>
                                          <table style="width: 1080px" class="font9Tahoma">
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan="6"> 
					<asp:DataGrid id="dgTx"
						AutoGenerateColumns="False" width="100%" runat="server"
						OnItemDataBound="DataGrid_ItemCreated" 
						GridLines = None
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_Delete"
						OnEditCommand="DEDR_Edit"
						OnCancelCommand="DEDR_Cancel"								
						AllowSorting="True"
                        class="font9Tahoma">	
							 
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
					<asp:TemplateColumn HeaderText="No.">
						<ItemStyle width="3%"/>
						<ItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:label id="lblIdx" runat="server" />
						</EditItemTemplate>							
					</asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="LnID" Visible="False">
                            <ItemTemplate>
                                <asp:label text=<%# Container.DataItem("TransactionLNID") %> id="lblLnID" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Line Description">
						<ItemStyle width="15%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("Description") %> id="lblDesc" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Group ID">
						<ItemStyle width="5%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("PDOLnID") %> id="lblGroupIDGrd" runat="server" />
                            <asp:label text=<%# Container.DataItem("TypeEmp") %> id="lblTypeEmpGrd" Visible="false" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Qty">
					        <HeaderStyle HorizontalAlign="Right" />
                        	<ItemStyle width="5%" HorizontalAlign="Right"/>
                            <ItemTemplate>
                           	<asp:label id="lblQty" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Qty"), 2), 2) %>  runat="server" />
							</ItemTemplate>
                    </asp:TemplateColumn>									  
                    <asp:TemplateColumn HeaderText="Satuan">
					        <HeaderStyle HorizontalAlign="Center" />	
                        	<ItemStyle width="5%" HorizontalAlign="Center"/>
                            <ItemTemplate>
                                <asp:label text=<%# Container.DataItem("UOM") %> id="lbluom" runat="server" />
                            </ItemTemplate>
                    </asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Total">
						<HeaderStyle HorizontalAlign="Right" />			
						<ItemStyle width="10%" HorizontalAlign="Right" />						
						<ItemTemplate>
							<asp:label id="lblAmount" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %>  runat="server" />
							<asp:label id="lblAccTx" runat="server" Visible="False" />
							<asp:label id="lblAmt" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %> visible=False runat="server" />
						    <asp:label text=<%# Container.DataItem("TransactionLNID") %> Visible=False id="lblLnIDDet" runat="server" />
							<asp:label text=<%# Container.DataItem("PDOID") %> Visible=False id="lblPDOID" runat="server" />
						</ItemTemplate>							
					</asp:TemplateColumn>										
                        <asp:TemplateColumn HeaderText="Additional Note">
                        <HeaderStyle HorizontalAlign="Center" />	
                           <ItemStyle width="20%"/>
                           <ItemTemplate>
                        <asp:label text=<%# Container.DataItem("AdditionalNote") %> id="lblAddNote" runat="server" />
                        </ItemTemplate>
                        </asp:TemplateColumn>
					<asp:TemplateColumn>		
						<ItemStyle width="5%" HorizontalAlign="Left" />
                        <ItemTemplate>
             				<asp:LinkButton id="Edit" CommandName="Edit" Text="Edit" CausesValidation =False runat="server" Font-Size="Smaller" />
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" Font-Size="Smaller" />&nbsp;
                        </ItemTemplate>
                    	</asp:TemplateColumn>
					</Columns>										
                        <PagerStyle Visible="False" />
					</asp:DataGrid>
				</td>	
			</tr>
			<tr>
				<td colspan=6>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=4>&nbsp;</td>								
				<td height=25 align=right style="width: 198px">Total Amount : <asp:label id="lblTotalAmount" text="0" runat="server" /></td>						
				<td>&nbsp;</td>					
			</tr>
			<tr>
				<td colspan=6 style="height: 21px">&nbsp;</td>
			</tr>
			<tr>
				<td ColSpan="6">
					<asp:label id=lblBPErr text="Unable to add record, please create a Bill Party for " Visible=False forecolor=red Runat="server" />
					<asp:label id=lblLocCodeErr text="" Visible=False forecolor=red Runat="server" />
				</td>
			</tr>
	
			<tr>
				<td align="left" colspan="6">
                                            &nbsp;</td>
			</tr>		
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
			<tr>
			    <td>&nbsp;</td>								
			    <td height=25 align=right>
                    &nbsp;</td>
			    <td>&nbsp;</td>	
			    <td align=right><asp:label id="lblTotalDB" text="0" Visible=false runat="server" /></td>						
			    <td style="width: 198px">&nbsp;</td>		
			    <td align=right><asp:label id="lblTotalCR" text="0" Visible=false runat="server" /></td>				
		    </tr>
		</table>
                                    </ContentPane>
        
                                  </igtab:Tab>

                                <igtab:Tab Key="PDD02" Text="ADD PEMAKAIAN SALDO BANK" Tooltip="ADD PEMAKAIAN SALDO BANK">
                                    <ContentPane>
                     <table style="width: 1080px" class="font9Tahoma">
                    						
						<tr class="mb-c">
							<td height=25 >Deskripsi :</td>
							<td colspan=3><asp:TextBox id=txtBankAddNote Width="95%" maxlength=128  CssClass="fontObject" runat=server />	
											<asp:label id=Label1 text="Please enter Line Description" Visible=False forecolor=red Runat="server" /></td>
						</tr>                        
 
                        <tr class="mb-c">
                            <td height="25">
                                Nominal (Rp) :</td>
                            <td colspan="3">
                                <asp:Textbox id="txtaddBankUse"  CssClass="fontObject"  Width="20%" maxlength=22 onkeypress="javascript:return isNumberKey(event)" runat=server />&nbsp;
                                <asp:label id=Label2 Runat="server" Visible="False"/>&nbsp;<asp:label 
                                    id=Label3 Runat="server" Visible="False"/>
                            </td>

                            <td colspan="3">
                                <asp:ImageButton AlternateText="Add" id="ImageButton1" ImageURL="../../images/butt_add.gif" OnClick="btnAddBank_Click" UseSubmitBehavior="false" Runat="server" /> &nbsp;
                            </td>

                        </tr>  

<tr>
				<td colspan="6"> 
					<asp:DataGrid id="dgPDDBank"
						AutoGenerateColumns="False" width="100%" runat="server"
				 
						GridLines = None
						Cellpadding = "2"
						Pagerstyle-Visible="False"
						OnDeleteCommand="DEDR_DeleteBank"											
						AllowSorting="True"
                        class="font9Tahoma">	
							 
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
 
                        <asp:TemplateColumn HeaderText="Transaction ID" Visible="true">
                            <ItemTemplate>
                                <asp:label text=<%# Container.DataItem("TransactionLNID") %> id="lblLnIDBank" Visible=false runat="server" />
                                <asp:label text=<%# Container.DataItem("TransactionID") %> id="lbltrxIDBank" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Line Description">
						<ItemStyle width="15%"/>
						<ItemTemplate>
							<asp:label text=<%# Container.DataItem("AdditionalNote") %> id="lblDescBank" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
 
                    <asp:TemplateColumn HeaderText="Nominal (Rp)">
					        <HeaderStyle HorizontalAlign="Right" />
                        	<ItemStyle width="5%" HorizontalAlign="Right"/>
                            <ItemTemplate>
                            <asp:label id="lblTotalBankhiden" Visible=false text=<%# Container.DataItem("Total") %>  runat="server" />
                           	<asp:label id="lblTotalBank" text=<%# objGlobal.GetIDDecimalSeparator_FreeDigit(FormatNumber(Container.DataItem("Total"), 2), 2) %>  runat="server" />
							</ItemTemplate>
                    </asp:TemplateColumn>									  
 
					<asp:TemplateColumn>		
						<ItemStyle width="5%" HorizontalAlign="Left" />
                        <ItemTemplate>             				
							<asp:LinkButton id="Delete" CommandName="Delete" Text="Delete" CausesValidation =False runat="server" />&nbsp;
                        </ItemTemplate>
                    	</asp:TemplateColumn>
					</Columns>										
                        <PagerStyle Visible="False" />
					</asp:DataGrid>
				</td>	
			</tr>
					</table>
                                    </ContentPane>
                                </igtab:Tab>
                            </Tabs>
             </igtab:UltraWebTab>
                         
       


        <br />
        </div>
        </td>
        </tr>
        </table>


            

		</form>
	</body>
</html>
