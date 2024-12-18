<%@ Page Language="vb" Src="../../../include/GL_trx_BudgetProd_Karet_Estate_Det.aspx.vb" Inherits="GL_trx_BudgetProd_Karet_Estate_Det" %>
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

			<asp:label id="lblPleaseSelect" visible="false" text="Please select " runat="server" />
			<asp:Label id="lblErrMessage" visible="false" Text="Error while initiating component." runat="server"/>
			<asp:Label id="lblCode" visible="false" text=" Code" runat="server" />
			<table cellspacing="1" cellpadding="1" class="font9Tahoma" width="100%" border="0" id="TABLE1"">
 				<tr>
					<td colspan="6">
					    <table cellpadding="0" cellspacing="0" class="style1">
                            <tr>
                                <td class="font9Tahoma">
                                  <strong>  BUDGET KARET DETAILS</strong></td>
                                <td class="font9Header"  style="text-align: right">
                        Date Created :
                        <asp:Label id="lblCreateDate" runat="server"/>&nbsp;|
                        Update By :
                        <asp:Label id="lblUpdateBy" runat="server"/>
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
				<tr>
					<td style="height: 25px; width:25%">
                        Periode Budget:</td>
                </tr>
					<td style="height: 25px; width:45%" colspan="3">
					    <asp:TextBox runat="server" ID="txtYearBudget" Width="25%"> </asp:TextBox><asp:RequiredFieldValidator id="reqYear" display="dynamic" runat="server" 
							ErrorMessage="<br>Please key Budget Year" 
							ControlToValidate="txtYearBudget" /></td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
					
				
				<tr>
					<td style="height: 25px; width:25%">
                        Tahun Tanam :</td>
					<td style="height: 25px; width:45%" colspan="3">
                        <asp:Dropdownlist id="ddlGroupCOA" width="90%" OnSelectedIndexChanged="ddlGroupCOA_OnSelectedIndexChanged" AutoPostBack="true" runat="server"/> 
						<br><asp:Label id="lblErrVehicle" visible="false" forecolor="red" runat="server"/></td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
					<td style="height: 25px; width:15%">
                        <br />
                        </td>
				</tr>
				<tr>
					<td style="height: 25px; width:25%">
                        Blok :</td>
					<td style="height: 25px; width:45%" colspan="3">
                        <asp:Dropdownlist id="ddlSubBlok" width="90%" runat="server"/></td>
					<td style="height: 25px; width:15%"></td>
					<td style="height: 25px; width:15%"></td>
				</tr>
				
				
				
				<tr>
				    <td colspan="6">&nbsp;<asp:Label id="lblValueError" visible="false" forecolor="red" runat="server"/></td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:12%"></td>
				    <td style="height:25px; width:12%">Latek</td>
				    <td style="height:25px; width:12%">Kadar Karet Kering</td>
		            <td style="height:25px; width:12%">Slab</td>
				    <td style="height:25px; width:12%">L.Mangkok</td>
				    <td style="height:25px; width:12%">G.Scrap</td>
				</tr>
				
				<tr>
				    <td style="height:25px; width:12%">Januari</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX01">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK01">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL01">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM01">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS01">0</asp:TextBox></td>
		                     
				 </tr>
				<tr>
				    <td style="height:25px; width:12%">Februari</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX02">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK02">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL02">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM02">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS02">0</asp:TextBox></td>
			       
				</tr>
				<tr>
				    <td style="height:25px; width:12%">Maret</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX03">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK03">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL03">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM03">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS03">0</asp:TextBox></td>
	        
				</tr>
				<tr>
				    <td style="height:25px; width:12%">April</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX04">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK04">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL04">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM04">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS04">0</asp:TextBox></td>
			     
			</tr>
				<tr>
				    <td style="height:25px; width:12%">Mei</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX05">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK05">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL05">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM05">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS05">0</asp:TextBox></td>
	       
				</tr>
				<tr>
				    <td style="height:25px; width:12%">Juni</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX06">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK06">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL06">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM06">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS06">0</asp:TextBox></td>
		        
				</tr>
				<tr>
				    <td style="height:25px; width:12%">Juli</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX07">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK07">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL07">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM07">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS07">0</asp:TextBox></td>
		     
				</tr>
				<tr>
				    <td style="height:25px; width:12%">Agustus</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX08">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK08">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL08">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM08">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS08">0</asp:TextBox></td>
		       
				</tr>
				<tr>
				    <td style="height:25px; width:12%">September</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX09">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK09">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL09">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM09">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS09">0</asp:TextBox></td>
		      
				</tr>
				<tr>
				    <td style="height:25px; width:12%">Oktober</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX10">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK10">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL10">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM10">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS10">0</asp:TextBox></td>
		      
				</tr>
				<tr>
				    <td style="height:25px; width:12%">November</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX11">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK11">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL11">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM11">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS11">0</asp:TextBox></td>
			    
				</tr>
				<tr>
				    <td style="height:25px; width:12%">Desember</td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLX12">0</asp:TextBox></td>
				    <td style="height:25px; width:12%"><asp:TextBox runat="server" id="BKKK12">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BSL12">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BLM12">0</asp:TextBox></td>
					<td style="height:25px; width:12%"><asp:TextBox runat="server" id="BGS12">0</asp:TextBox></td>
		      
				</tr>
				
				<tr>
				    <td colspan="6" style="height: 2px">&nbsp;</td>
				</tr>
				
				<tr>
					<td colspan="6">
						<asp:ImageButton id="btnSave" imageurl="../../images/butt_save.gif" AlternateText="  Save  " onclick="btnSave_Click" runat="server" />
						<asp:ImageButton id="btnBack" imageurl="../../images/butt_back.gif" CausesValidation="False" AlternateText="  Back  " onclick="btnBack_Click" runat="server" />
					    <br />
					</td>
				</tr>
			</table>

            <input type=hidden id=hidAccYear value="" runat=server/>
             <input type=hidden id=hidVehCode value="" runat=server/>
            </div>
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
