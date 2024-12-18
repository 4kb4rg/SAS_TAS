<%@ Page Language="vb" src="../include/menu.aspx.vb" Inherits="menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GG-Menu</title>
    <link href="include/css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
      
      
         
        <div class="view" id="view">
        
        <div style="position:absolute; right:5px; top:5px; width:650px; padding:0px 0px 0px 0px; display:block">
          
           <div id="navigationContainer" class="navigationContainer">
           <div class="navigationButton"><a href="../login.aspx"> 
			<asp:Image runat="server" ID="imglogOff" ImageUrl="images/thumbs/LogOff.gif" ToolTip="Log Off" /></a>
	  </div>
           <div class="navigationButton styleButton"><a href="system/user/setlocation.aspx">
                     <asp:Image ID="imgLoc" runat="server" ImageUrl="images/thumbs/Loc.gif" ToolTip="Change Active Location" /></a>
          </div>
          </div>
          
          <table border="0" cellspacing="0" cellpadding="0" width="100%" >
             <tr>
               <td align="right" style="height: 25px">
                 <asp:Label id="lblUser" runat="server" cssclass="login"  />
               </td>
             </tr>
           </table>
         </div>
        
       
        
        <div class="gallery2" >
        
        
        
            <ul>
                <li>
                    <div class="info2 mm_style">
                        <a href="menu_mm.aspx">
                            <asp:Image runat="server" ID="img1" ImageUrl="images/thumbs/MMThumb.jpg" ToolTip="Material Management"  /></a>
                        <div class="tooltip2">
                           
                            <div class="description2">
                                <p>
                                    MODUL  MM, BERTUJUAN UNTUK MENCATAT SELURUH AKTIVITAS YANG BERKAITAN DENGAN MUTASI BARANG YANG TERJADI DI PERUSAHAAN.</p>
                               
                            </div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="info2 pm_style">
                        <a href="menu_pm.aspx">
                            <asp:Image runat="server" ID="img2" ImageUrl="images/thumbs/PMThumb.jpg" ToolTip="Plant Maintenance" /></a>
                        <div class="tooltip2">
                           
                            <div class="description2">
                                <p>
                                    MODUL PLANT MAINTENANCE DIGUNAKAN UNTUK MENCATAT AKTIVITAS PEMELIHARAAN YANG DILAKUKAN DI LOKASI.</p>
                            </div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="info2 pd_style" >
                        <a href="menu_pd.aspx">
                            <asp:Image runat="server" ID="img3" ImageUrl="images/thumbs/PDThumb.jpg" ToolTip="Production"/>
                        </a>
                        <div class="tooltip2">        
                     
                            <div class="description2">
                                <p>
                                    PENCATATAN PRODUKSI KEBUN PER BLOK, PRODUKSI PABRIK, INPUT DATA QUALITY ATAS PRODUK YANG ADA, DAPAT DILAKUKAN DI MODUL PRODUKSI.
                                </p>
                            </div>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
       
        <div id="gallery" class="gallery" >
            <div class="description defaultdescription">
                <p><img src="images/thumbs/webshowcase.jpg" width="409" height="237" />&nbsp;</p>
            </div>
            <ul>
                <li>
                    <div class="info sd_style">
                        <a href="menu_sd.aspx">
                            <asp:Image runat="server" ID="img4" ImageUrl="images/thumbs/SDThumb.jpg" ToolTip="Sales and Distribution" /></a>
                        <div class="tooltip">
                          
                            <div class="description">
                                <p>
                                    MODUL SALES , BERTUJUAN MENCATAT TRANSAKSI YANG BERKAITAN DENGAN PENJUALAN PRODUK YANG DIHASILKAN OLEH UNIT YANG ADA DALAM PERUSAHAAN.
				</p>
                            </div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="info fi_style">
                        <a href="menu_fi.aspx">
                            <asp:Image runat="server" ID="img5" ImageUrl="images/thumbs/FIThumb.jpg" ToolTip="Financial Management"/></a>
                        <div class="tooltip">
                          
                            <div class="description">
                                <p>
                                    MODUL FINANCIAL  DIGUNAKAN UNTUK MENCATAT SELURUH KEGIATAN YANG BERKAITAN DENGAN KEUANGAN.
				</p>
                            </div>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="info hr_style" >
                        <a href="menu_hr.aspx">
                            <asp:Image runat="server" ID="img6" ImageUrl="images/thumbs/HRThumb.jpg" ToolTip="Human Resources Management" />
                        </a>
                        <div class="tooltip">        
                           
                            <div class="description">
                                <p>
                                    MODUL HUMAN RESOURCES BERTUJUAN UNTUK MENCATAT SEGALA AKTIVITAS YANG DILAKUKAN OLEH BAGIAN PERSONALIA.
                                </p>
                            </div>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        </div>
        <div class="BackgroundTopCorner"></div>
    </form>
</body>
</html>

