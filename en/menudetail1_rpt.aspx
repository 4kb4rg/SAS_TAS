<%@ Page Language="vb" src="../include/menudetail1_rpt.aspx.vb" Inherits="menudetail1_rpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
 
</head>
<body style="margin:0; padding:0">
    <form id="form1" runat="server">
           <table style="width:200px; height:100px">
           <tr style="width:200px; height:100px">
           
           <td style="width:125px">
    
          
                <tr>
					<td style="width:100%; height: 21px;">
                        <A HREF="CB/trx/cb_trx_CashBankList.aspx" TARGET="middleFrame">menu 1</A>
                    </td>
				</tr>
				<tr>
				  <td style="width:100%; height: 21px;" >
                        <A HREF="CB/trx/cb_trx_CashBankList.aspx" TARGET="middleFrame">menu 2</A>
                    </td>
				</tr>
           
            
           
           </td>
           <td style="width: 75px">
  		<table width="100%">
       
              		<iframe id="Iframe1" name="middleFrame">
			</iframe>
             
        	</table>    
           </td>
           
           </tr>
           
             </table>
            
    </form>
</body>
</html>

