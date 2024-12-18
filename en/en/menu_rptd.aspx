<%@ Page Language="vb" src="../include/menu_rptd.aspx.vb" Inherits="menu_rptd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    
    <link href="include/css/MenuStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="/en/include/script/jscript.js" type="text/jscript"></script>

</head>
<body bgcolor="black" style="padding-right: 0px; padding-left: 0px; margin-left: 0px; margin-right: 0px" >
    <form id="form1" runat="server" >
         
           <div id="Nav" style="position:absolute; width:20%; top:0px; left:0px; height:1000px">
            	
            <table>
			    <tr height="20">
			        <td width="20"></td>
			    </tr>
			</table> 

            <table id="tblMMHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			    <tr height="20" >
				    <td width="20"></td>
				    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
				    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblMM);">Material Management</A></td>
			    </tr>
			</table>
			<table id="tblMM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptMM1" runat="server" target="middleFrame"  text="Inventory" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptMM2" runat="server" target="middleFrame"  text="Purchasing" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			</table>
			
			<table id="tblspc1" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
			
			<table id="tblPMHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			    <tr height="20" >
				    <td width="20"></td>
				    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
				    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblPM);">Plant Maintenance</A></td>
			    </tr>
			</table>
			<table id="tblPM" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptPM1" runat="server" target="middleFrame"  text="Nursery" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptPM2" runat="server" target="middleFrame"  text="Workshop" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			</table>
			
			<table id="tblspc2" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
			<table id="tblPDHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			    <tr height="20" >
				    <td width="20"></td>
				    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
				    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblPD);">Production</A></td>
			    </tr>
			</table>
			<table id="tblPD" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptPD1" runat="server" target="middleFrame"  text="Production" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			</table>
			
			<table id="tblspc3" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
			<table id="tblSDHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			    <tr height="20" >
				    <td width="20"></td>
				    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
				    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblSD);">Sales & Distribution</A></td>
			    </tr>
			</table>
			<table id="tblSD" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptSD1" runat="server" target="middleFrame"  text="Contract Management" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			     <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptSD2" runat="server" target="middleFrame"  text="Weighing Management" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			</table>
			
			<table id="tblspc4" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
		    <table id="tblFIHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			    <tr height="20" >
				    <td width="20"></td>
				    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
				    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblFI);">Financial Management</A></td>
			    </tr>
			</table>
			<table id="tblFI" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptFI1" runat="server" target="middleFrame"  text="General Ledger" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptFI2" runat="server" target="middleFrame"  text="Account Payable" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptFI3" runat="server" target="middleFrame"  text="Account Receivable" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptFI4" runat="server" target="middleFrame"  text="Cash Bank Management" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptFI5" runat="server" target="middleFrame"  text="Fixed Asset" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			</table>
			
            <table id="tblspc5" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td colSpan="2"><IMG height="0" src="images/spacer.gif" width="5" border="0"></td>
				</tr>
			</table>
			
			<table id="tblHRHead" cellSpacing="0" cellPadding="0" width="100%" runat="server">
			    <tr height="20" >
				    <td width="20"></td>
				    <td width="14"><IMG src="images/arow.gif" border="0" align="left"></td>
				    <td class="lb-hti"><A class="lb-tti" href="javascript:togglebox(tblHR);">HR & Payroll</A></td>
			    </tr>
			</table>
			<table id="tblHR" style="VISIBILITY: hidden; POSITION: absolute" cellSpacing="0" cellPadding="0"
				width="100%" border="0" runat="server">
			    <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptHR1" runat="server" target="middleFrame"  text="Human Resource" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			     <tr height="20">
				    <td width="20"></td>
				    <td width="14"><IMG src="images/spacer.gif" border="0" align="left"></td>
				    <td class="lb-mti"><asp:hyperlink id="lnkRptHR2" runat="server" target="middleFrame"  text="Payroll" cssclass="lb-mt"></asp:hyperlink></td>
			    </tr>
			</table>
			
            </div>
         
             <div style="position:absolute; top:0px; width:85%; left:179px; height:1000px" >
          
              	<iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%; background-color:Black"
				 scrolling="auto" src="black.aspx"></iframe>
             
               </div>
            
           </td>
           <div class="BackgroundTopCorner"></div>
          </form>
           
    </form>
</body>
</html>

