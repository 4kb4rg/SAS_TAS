<%@ Page Language="vb" src="../../../include/IN_MthEnd_Process.aspx.vb" Inherits="IN_mthend_Process" %>
<%@ Register TagPrefix="UserControl" Tagname="MenuINMthEnd" src="../../menu/menu_INMthEnd.ascx"%>
<%@ Register TagPrefix="Preference" Tagname="PrefHdl" src="../../include/preference/preference_handler.ascx"%>
<html>
	<head>
		<title>Inventory Month End Process</title>
        <link href="../../include/css/gopalms.css" rel="stylesheet" type="text/css" />
	</head>
	<Preference:PrefHdl id=PrefHdl runat="server" />
	<body>
		<form id=frmMain class="main-modul-bg-app-list-pu" runat=server  onsubmit="ShowLoading()">
         <table cellpadding="0" cellspacing="0" style="width: 100%" >
		<tr>
             <td style="width: 100%; height: 1500px" valign="top">
			    <div class="kontenlist"> 
		<table border="0" width="100%" cellpadding="1" cellspacing="0" class="font9Tahoma">
			<tr>
				<td colspan=5>
                  <strong> MM MONTHLY PROCESS</strong></td>
			</tr>
			<tr>
				<td colspan=5>
                         <hr style="width :100%" />
                    </td>
			</tr>
			<tr>
				<td width=40% height=25>Period To Be Processed :</td> 
				<td width=30%>
					<asp:DropDownList id=ddlAccMonth runat=server/> / 
					<asp:DropDownList id=ddlAccYear OnSelectedIndexChanged=OnIndexChage_ReloadAccPeriod AutoPostBack=True runat=server />
				</td>
				<td width=5%>&nbsp;</td>
				<td width=15%>&nbsp;</td>
				<td width=10%>&nbsp;</td>
			</tr>
			<tr>
				<td colspan=5 height=25>
                    <asp:Label id=lblErrProcess visible=false forecolor=red text="There are some errors when processing month end." runat=server/>
				</td>
			</tr>
			<tr>
				<td colspan=5>
					<asp:ImageButton id=btnProceed imageurl="../../images/butt_process.gif" AlternateText="Proceed with month end process" OnClick="btnProceed_Click" runat="server" />
				&nbsp;<asp:Button 
                        ID="Issue5" class="button-small" runat="server" Text="Process" 
                        Visible="False"  />
				</td>
			</tr>
			
			<tr>
				<td colspan=5>&nbsp;</td>
			</tr>
		</table>
		<asp:Label id=lblErrMesage visible=false Text="Error while initiating component."  class="font9Tahoma" runat=server />
    </div>
    </td>
    </tr>
    </table>
		</form>
	</body>
    <script type="text/javascript">  
        function ShowLoading(e) {
            var div = document.createElement('div');
            var img = document.createElement('img');
            img.src = '/../en/images/load2.gif';
            div.innerHTML = "<br />";
            div.style.cssText = 'position: fixed; top: 0%; left: 0%; z-index: auto; width: 100%; height: 100%; text-align: center; background-color:rgba(255, 255, 255, 0.8);';
            div.appendChild(img);
            document.body.appendChild(div);
            return true;
            // These 2 lines cancel form submission, so only use if needed.  
            //window.event.cancelBubble = true;  
            //e.stopPropagation();  
        }
      </script>
	</html>
