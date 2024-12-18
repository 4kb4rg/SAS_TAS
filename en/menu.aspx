<%@ Page Language="vb" src="../include/menu.aspx.vb" Inherits="menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
     <link href="include/css/gopalms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style3
        {
            width: 273px;
        }
    </style>
</head>
<body style="margin: 0; background-color: #d0d0d0;">
    <form id="form1" runat="server">
      <table cellpadding="0" cellspacing="0" style="width: 99%; height: 98%;" align="center" class="main-modul-bg">
	<tr>
		<td class="cell-left" valign="top">
		
		<table cellpadding="0" cellspacing="0" style="width: 100%; height: 50px">
			<tr>
				<td class="cell-right" valign="bottom">
				<table cellpadding="0" cellspacing="0" style="width: 100%">
					<tr>
						<td style="width: 100%" class="cell-right"><span class="font9">
						&nbsp;</span><table cellpadding="0" cellspacing="0" class="style1">
                                <tr>
                                    <td style="text-align:right" class="style3">
                                        &nbsp;</td>
                                    <td style="text-align:right">
										 	
											&nbsp;<a href="../login.aspx"> 
											</a>
										    <a href="../default.aspx">
											&nbsp;</a><span class="font9"><strong>Welcome, 
                 <asp:Label id="lblUser" runat="server" cssclass="login"  /></strong></span></td>
                                </tr>
                            </table>
                        </td>
						<td valign="bottom">
							<table align="right" cellpadding="0" cellspacing="0" style="width: 120px">
								<tr>
									<td style="height: 60px">
										<div class="dropdown">	
										</div>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
																			 	
											<a href="system/user/setlocation.aspx"><img height="27" src="images/btlogin.png" width="23" class="button" />
                                            </a><a href="../login.aspx">
                                            &nbsp;<img height="27" src="../images/btlogout.png" width="23" class="button" /></a></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				</td>
			</tr>
		</table>
		<table cellpadding="0" cellspacing="2" style="width: 100%; height:600px">
			<tr>
				<td style="width: 90px" valign="top">
				</td>
				<td style="width: 534px" valign="top">
				<table cellpadding="1" cellspacing="0" style="width: 100%">
					<tr>
						<td style="height: 160px" colspan="3">&nbsp;</td>
					</tr>
					<tr>
						<td style="width: 178px; height: 178px" valign="top" class="cell-left"><a href="menu_mm.aspx"><img height="176px" src="../images/mainmodule1.png" width="176px" class="button" /></a></td>
						<td style="width: 178px; height: 178px" valign="top" class="cell-center"><a href="menu_pm.aspx"><img height="176px" src="../images//mainmodule2.png" width="176px" class="button" /></a></td>
						<td style="width: 178px; height: 178px" valign="top" class="cell-right"><a href="menu_pd.aspx"><img height="176px" src="../images/mainmodule3.png" width="176px" class="button" /></a></td>
					</tr>
					<tr>
						<td style="width: 178px; height: 178px" valign="bottom" class="cell-left"><a href="menu_sd.aspx"><img height="176px" src="../images/mainmodule4.png" width="176px" class="button" /></a></td>
						<td style="width: 178px; height: 178px" valign="bottom" class="cell-center"><a href="menu_fi.aspx"><img height="176px" src="../images/mainmodule5.png" width="176px" class="button" /></a></td>
						<td style="width: 178px; height: 178px" valign="bottom" class="cell-right"><a href="menu_hr.aspx"><img height="176px" src="../images/mainmodule6.png" width="176px" class="button" /></td>
					</tr>
					<tr>
						<td class="cell-center" colspan="3" style="height: 43px" valign="bottom">
						<table cellpadding="0" cellspacing="0" style="width: 100%; height: 40px">
							<tr>
								<td class="white-on-black50"><span class="font9">U S E R&nbsp;&nbsp; A C C E S S</span></td>
							</tr>
						</table>
						</td>
					</tr>
				</table>
				</td>
				<td style="width: 204px" valign="top">
          
                    &nbsp;</td>
				<td style="width: 204px" valign="top">
          
                    &nbsp;</td>
				<td style="width: 204px" valign="top">
          
          <table border="0" cellspacing="0" cellpadding="0" width="100%" >
             <tr>
               <td align="right" style="height: 25px; text-align: left;">
                   &nbsp;</td>
             </tr>
           </table>
                </td>
				<td style="width: 426px" valign="top">
				<table cellpadding="0" cellspacing="0" style="width: 100%">
					 <tr>
						<td style="height: 320px" colspan="3">&nbsp;</td>
					</tr>
					<tr>
						<td style="width: 142px; height: 197px" valign="bottom" class="cell-center"><a href="master.aspx"  target="_blank"><img height="140px" src="../images/adminmodule1.png" width="140px" class="button" /></a></td>
						<td style="width: 142px; height: 197px" valign="bottom" class="cell-center"><a href="menu_mgmrpt.aspx"><img height="140px" src="../images/adminmodule2.png" width="140px" class="button" /></a></td>                                                
						<td style="width: 142px; height: 197px" valign="bottom" class="cell-center"><a href="../Dashboard/Cost.aspx" target="_blank"><img height="140px" src="../images/adminmodule3.png" width="140px" class="button" /></a></td>
					</tr>
					<tr>
						<td class="cell-center" colspan="3" style="height: 43px" valign="bottom">
						<table cellpadding="0" cellspacing="0" style="width: 100%; height: 40px">
							<tr>
								<td class="white-on-black50"><span class="font9">A D M I N&nbsp;&nbsp; A C C E S S</span></td>
							</tr>
						</table>
						</td>
					</tr>
				</table>
				</td>
				<td style="width: 96px" valign="top">
				&nbsp;</td>
			</tr>
		</table>
		</td>
	</tr>
</table>
      
         
        <div class="view" id="view">
        
       

        </div>
    </form>
</body>
</html>

