<%@ Page Language="vb" src="../include/sysmenu.aspx.vb" Inherits="sysmenu" %>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="include/preference/preference_handler.ascx"%>
<html>
<head>
<Preference:PrefHdl id=PrefHdl runat="server" />
<title>System Menu</title>
</head>
<body class="menu" onload="javascript:togglebox(tblOther);" topmargin="0" leftmargin="0">
<form id=frmSysMenu runat=server>
 

<table cellpadding="0" cellspacing="0" style="width: 100%">
	<tr>
		<td class="cell-left" valign="top">
		<table cellpadding="0" cellspacing="0" style="width: 254px">
 
			<tr>
				<td valign="top">
				    <button class="accordion">Inventory</button>					
					<div class="panel">
                        <table id="tlbStpIN"  cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td><a href="#"><div class="childmenu">
                               <asp:hyperlink id="lnkStpIN01" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdType.aspx" text="Product Type" ></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu">
                                <asp:hyperlink id="lnkStpIN02" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdBrand.aspx" text="Product Brand" ></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN03" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdModel.aspx" text="Product Model"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN04" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdCategory.aspx" text="Product Category" ></asp:hyperlink>
                                    </div></a></td>
							</tr>

                            <tr>
								<td ><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN05" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_ProdMaterial.aspx" text="Product Material" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td ><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN06" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockAnalysis.aspx" text="Analisis Stock" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr >
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN07" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockMaster.aspx" text="Stock Master" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN08" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_StockItem.aspx" text="Stock Item" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN09" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_DirectMaster.aspx" text="Direct Charge Master" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN10" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_DirectCharge.aspx" text="Direct Charge Item" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								<td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpIN11" runat="server" target="middleFrame" NavigateUrl="/en/IN/setup/IN_Approval_Level.aspx" text="User Level Approval" cssclass="lb-mt"></asp:hyperlink></div></a></td>
							</tr>

						</table>
					</div>
                  <button class="accordion">Purchasing</button>					
					<div class="panel">
                        <table id="tlbStpPU" cellSpacing="1" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink class="lb-mt" id="lnkStpPU01" runat="server" target="middleFrame" NavigateUrl="/en/PU/setup/PU_setup_SuppList.aspx" text="Supplier"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU02" runat="server" NavigateUrl="/en/CM/setup/CM_Setup_CurrencyList.aspx" target="middleFrame" text="Currency"></asp:hyperlink></div></a></td>
							</tr>
							<tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU03" runat="server" NavigateUrl="/en/CM/setup/CM_setup_ExchangeRate.aspx" target="middleFrame" text="Exchange Rate"></asp:hyperlink></div></a></td>
							</tr>
			  			    <tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU08" runat="server" target="middleFrame" NavigateUrl="/en/PU/setup/PU_setup_UserGroupList.aspx" text="User Group Item"></asp:hyperlink></div></a></td>
							    </tr>
				            <tr>
								    <td><a href="#"><div class="childmenu"><asp:hyperlink id="lnkStpPU09" runat="server" NavigateUrl="/en/PU/setup/PU_Setup_LocMGr.aspx" target="middleFrame" text="Purchasing Manager"></asp:hyperlink></div></a></td>
					        </tr>
                        </table>
                    </div>

                <div style="position:absolute; top:0px; width:87%; left:125px; height:1000px" >
          
                    <iframe id="Iframe1" name="middleFrame"  style="border-style: none; border-color: inherit; border-width: 0; width:100%; height:100%; background-color:white; margin-top:0px; margin-left: 109px;"
				        scrolling="auto" src="black.aspx"  ></iframe>
             
               </div>

                </td>
            </tr>
        </table>
    </tr>
</table>


<table border=0 cellpadding="0" cellspacing="0" width=100%>
	<tr>
		<td align="center"><img src="images/spacer.gif" border="0" width="5" height="5"></td>
	</tr>
	
	<tr>
		<td>
			<table width=100% border=0 cellpading=0 cellspacing=0>
				<tr height="25" >
					<td width="20" class="lb-ht"></td>
					<td width="2"></td>
					<td class="lb-ht"><a href="javascript:togglebox(tblSysCfg);" class="lb-ti">System Setup</a></td>
				</tr>
				
			</table>
			<table id=tblSysCfg style="visibility:hidden; position: absolute;" width=100% border=0 cellpading=0 cellspacing=0 runat=server>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkSysCfg
								class="lb-mt"
								text="System Configuration"
								target=right
								runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkParam
								class="lb-mt"
								text="Parameters Setting"
								target=right
								runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkAppUser
							class="lb-mt"
							text="Application User"
							target=right
							runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkLangCap
							class="lb-mt"
							text="Penamaan Istilah"
							target=right
							runat=server />    
					</td>
				</tr>				
			</table>
			
			<table width=100% cellpading=0 cellspacing=0>
				<tr><td colspan="2"><img src="images/spacer.gif" border="0" width="5" height="5"></td></tr>
			</table>

			<table width=100% cellpading=0 cellspacing=0>
				<tr height="25" >
					<td width="20" class="lb-ht"></td>
					<td width="2"></td>
					<td class="lb-ht"><a href="javascript:togglebox(tblAdmin);" class="lb-ti">Administration</a></td>
				</tr>
			</table>
			<table id=tblAdmin style="visibility:hidden; position: absolute;" width=100% border=0 cellpading=0 cellspacing=0 runat=server>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkAdminSetup
							class="lb-mt"
							text="Setup"
							target=right
							runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkAdminDT
							class="lb-mt"
							text="Data Transfer"
							target=right
							runat=server />    
					</td>
				</tr>
			</table>
			

			<table width=100% cellpading=0 cellspacing=0>
				<tr><td colspan="2"><img src="images/spacer.gif" border="0" width="5" height="5"></td></tr>
			</table>

			<table width=100% cellpading=0 cellspacing=0>
				<tr height="25" >
					<td width="20" class="lb-ht"></td>
					<td width="2"></td>
					<td class="lb-ht"><a href="javascript:togglebox(tblOther);" class="lb-ti">Others</a></td>
				</tr>
			</table>
			<table id=tblOther style="visibility:hidden; position: absolute;" width=100% border=0 cellpading=0 cellspacing=0 runat=server>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkChgPwd
							class="lb-mt"
							text="Change Password"
							target=left
							runat=server />    
					</td>
				</tr>
				<tr height="20">
					<td width="7"></td>
					<td width="14">&nbsp;<IMG src="images/leftdot.gif" border="0" width="1" height="1"></td>
					<td class="lb-mti"><asp:Hyperlink id=lnkLogout
							class="lb-mt"
							navigateurl="/logout.aspx"
							text="Log Out"
							target=right
							runat=server />    
					</td>
				</tr>
			</table>

		</td>
	</tr>
</table>	


</form>

</body>

</html>
