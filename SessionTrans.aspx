<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SessionTrans.aspx.cs" Inherits="SessionTrans" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SRI LANKA INSURANCE</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    <div>
        <asp:HiddenField ID="Hd_UserEpf" runat="server" />
        <asp:HiddenField ID="Hd_UserBranch" runat="server" />
        <asp:HiddenField ID="Auth_Code" runat="server" />
        <asp:HiddenField ID="Hd_UserId" runat="server" />  
        <asp:HiddenField ID="UserId" runat="server" />
        <asp:HiddenField ID="brCode" runat="server" />
    </div> 
    <a href="SessionTrans.aspx">SessionTrans.aspx</a>
            <%--Message popup area start--%>
                    
            <asp:Button runat="server" ID="btnMessagePopupTargetButton" Style="display: none;" />
            
            <%--<asp:ModalPopupExtender ID="mpeMessagePopup" runat="server" PopupControlID="pnlMessageBox"
                TargetControlID="btnMessagePopupTargetButton" OkControlID="btnOk" CancelControlID="btnCancel">
            </asp:ModalPopupExtender>--%>
            
            <asp:Panel runat="server" ID="pnlMessageBox" BackColor="White" Width="520" Style="display:none;">

                <div class="popupHeader" style="width: 520px;">
                    <asp:Label ID="lblMessagePopupHeading" Text="Information"         
                        runat="server" Font-Size="14px"></asp:Label><asp:LinkButton
                        ID="LinkButton1" runat="server" Style="float: right; margin-right: 5px;">X</asp:LinkButton></div><div  style="max-height: 500px; width: 520px; overflow: hidden;">
                    <div style="float:left; width:480px; margin:20px;">
                       
                        <table style="padding: 0; border-spacing: 0; border-collapse: collapse; width: 100%;">
                            <tr>
                                <td style="text-align: left; vertical-align: top; width: 11%;">
                                    <asp:Literal runat="server" ID="ltrMessagePopupImage"></asp:Literal></td><td style="width: 2%;">
                                </td>
                                <td style="text-align: left; vertical-align: top; width: 87%;">
                                 <p style="margin: 0px; padding: 0px; color: #5F0202;">                                 
                                     <asp:Label runat="server" ID="lblMessagePopupText">
                                     </asp:Label>
                                     </p>
                                     </td>
                                     </tr>
                                  <tr>
                                <td style="text-align: right; vertical-align: top;" colspan="3">
                                    <br />
                                   </td>
                                 </tr>
                               </table>
                            </div>
                         </div>
                      </asp:Panel>                  
        <%--Message popup area END--%>
    </form>
</body>
</html>
