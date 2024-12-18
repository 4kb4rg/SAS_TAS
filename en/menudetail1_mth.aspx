<%@ Page Language="vb" src="../include/menudetail1_mth.aspx.vb" Inherits="menudetail1_mth" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
  
</head>
<body style="margin:0; padding:0">
    <form id="form1" runat="server">
           <table style="width:100%; height:100%">
           <tr style="width:100%; height:100%">
           
           <td style="width:125px">
           <div id="Nav" style="position:absolute; width:125px; top:0; height:100%; left:0">
            <table width="10%">
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
            </table>
            </div>
           
           </td>
           <td style="width: 845px">
             <div style="position:absolute; height:100%; top:0; width:845px" >
          
              <iframe id="Iframe1" name="middleFrame"  style="width:100%; height:100%" 
				 scrolling="auto" ></iframe>
             
                </div>
            
           </td>
           
           </tr>
           
             </table>
            
    </form>
</body>
</html>

