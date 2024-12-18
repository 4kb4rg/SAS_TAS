<%@ Page Language="vb" codefile="~/include/WM_Setup_FFBPriceDet.vb"  Inherits="WM_Setup_FFBPriceDet" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuWM" src="../../menu/menu_wmsetup.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<html>
	<head>
		<title>FFB Price Details</title>
         <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />

	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            </style>

	</head>
	<body>
		<Preference:PrefHdl id=PrefHdl runat="server" />
		<Form id=frmMain class="main-modul-bg-app-list-pu"  runat="server">                
			<table cellpadding="0" cellspacing="0" style="width: 100%" class="font9Tahoma">
				<tr>
					<td style="width: 100%; height: 1500px" valign="top">
						<div class="kontenlist">   
							<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server" />
							<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
							<asp:Label id="lblSelect" visible="false" text="Please select " runat="server" />
							<asp:Label id="lblList" visible="false" text="Select " runat="server" />
							
							<table border=0 cellspacing=0 cellpadding=2 width=100% class="font9Tahoma">
								<tr>
									<td colspan="5">
										<usercontrol:menuwm id=MenuWM runat=server />
									</td>
								</tr>
								<tr>                    
									<td class="mt-h" colspan="5">
										<br />
									
										<table cellpadding="0" cellspacing="0" class="style1">
											<tr>
												<td class="font9Tahoma">
											<strong>   FFB PRICE DETAILS</strong>  </td>
												<td class="font9Header" style="text-align: right">
													Status : <asp:Label id=lblStatus runat=server />&nbsp;| Tgl buat : <asp:Label id=lblDateCreated runat=server />&nbsp;| Tgl update : <asp:Label id=lblLastUpdate runat=server />&nbsp;| Diupdate : <asp:Label id=lblUpdatedBy runat=server />
												</td>
											</tr>
										</table>
										<hr style="width :100%" />
									</td>
								</tr>
								
								<tr>
									<td colspan=6>&nbsp;</td>
								</tr>
								
								<tr>
									<td style="width: 224px;">
										
										Periode</td>
									<td style="width: 346px;">
										<asp:TextBox id=txtDateCreated CssClass="font9Tahoma"  width=25% maxlength="10" runat="server"/>
									<a href="javascript:PopCal('txtDateCreated');"><asp:Image id="btnDateCreated" ImageAlign=AbsMiddle runat="server" ImageUrl="../../Images/calendar.gif"/></a>
									</td>
									<td style="width: 16px;">&nbsp;</td>
									<td style="width: 169px;">&nbsp;</td>
									<td >&nbsp;</td>								
								</tr>
								<tr>
									<td align="left" style="width: 224px">Buah Besar :</td>
									<td align="left" style="width: 346px">
										<asp:DropDownList id="srchlocation" width=100% runat=server class="font9Tahoma" />
									</td>
									<td style="width: 16px">&nbsp;</td>
									<td style="width: 169px">&nbsp;</td>
									<td>&nbsp;</td>
								</tr>								
								<tr>
									<td align="left" style="width: 224px">Buah Besar :</td>
									<td align="left" style="width: 346px">
										<telerik:RadNumericTextBox ID="txtBBesar"  CssClass="font9Tahoma"    
													Runat="server" LabelWidth="64px">     
													<NumberFormat ZeroPattern="n"></NumberFormat>
												<EnabledStyle HorizontalAlign="Right" />
												</telerik:RadNumericTextBox></td>
									<td style="width: 16px">&nbsp;</td>
									<td style="width: 169px">&nbsp;</td>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td align="left" style="width: 224px">
										Buah Kecil :</td>
									<td align="left" style="width: 346px">
									<telerik:RadNumericTextBox ID="txtBSedang"  CssClass="font9Tahoma"    
													Runat="server" LabelWidth="64px">     
											<NumberFormat ZeroPattern="n"></NumberFormat>
												<EnabledStyle HorizontalAlign="Right" />
												</telerik:RadNumericTextBox></td>
									<td style="width: 16px">&nbsp;</td>
									<td style="width: 169px">&nbsp;</td>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td align="left" style="width: 224px">
										Buah Sedang :</td>
									<td align="left" style="width: 346px">
									<telerik:RadNumericTextBox ID="txtBKecil"  CssClass="font9Tahoma"    
													Runat="server" LabelWidth="64px">     
											<NumberFormat ZeroPattern="n"></NumberFormat>
												<EnabledStyle HorizontalAlign="Right" />
												</telerik:RadNumericTextBox></td>
									<td style="width: 16px">&nbsp;</td>
									<td style="width: 169px">&nbsp;</td>
									<td>&nbsp;</td>
								</tr>
								
								<td colspan="5" style="height: 23px">&nbsp;</td>
								<tr>
									<td colspan="5" style="height: 28px">
										<asp:ImageButton id=SaveBtn AlternateText="  Save  " imageurl="../../images/butt_save.gif" onclick=Button_Click CommandArgument=Save runat=server />
										<asp:ImageButton id=DelBtn AlternateText=" Delete " imageurl="../../images/butt_delete.gif" CausesValidation=False onclick=Button_Click CommandArgument=Del runat=server />
										<asp:ImageButton id=UnDelBtn AlternateText="Undelete" imageurl="../../images/butt_undelete.gif" onclick=Button_Click CommandArgument=UnDel runat=server />
										<asp:ImageButton id=BackBtn AlternateText="  Back  " CausesValidation=False imageurl="../../images/butt_back.gif" onclick=BackBtn_Click runat=server />
										<br />
									</td>
								</tr>
								<Input Type=Hidden id=SalCode runat=server />
								<asp:Label id=lblNoRecord visible=false text="Blok details not found." runat=server/>
								<asp:Label id=lblHiddenSts visible=false text="0" runat=server/>								
			</table>
			<Input type=hidden id=hidBlockCharge value="" runat=server/>
			<Input type=hidden id=hidChargeLocCode value="" runat=server/>
         				</div>
        			</td>
        		</tr>
        	</table>
         <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
		</form>
	</body>
</html>
