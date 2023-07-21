<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logued.aspx.cs" Inherits="PetShop.Pages.Logued" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Se logueo un usuario <asp:Label ID="lblWebMaster" runat="server" />
            <br />
            <asp:Label ID="lblDBBroken" runat="server" />
            <asp:Button ID="btnCorrectDB" runat="server" Text="Arreglar BD" OnClick="btnCorrectAction" />
            <br />
            <asp:Button ID="btnBackup" runat="server" Text="Realizar BackUp" OnClick="btnBackUp" /> 
            <br />
            <br />
            <asp:Button ID="btnRestore" runat="server" Text="Realizar Restore" OnClick="btnRestoreDB" />
            <br />
            <br />
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" GridLines="None"
                AllowPaging="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                PageSize="200">

            </asp:GridView>
        </div>
    </form>
</body>
</html>
