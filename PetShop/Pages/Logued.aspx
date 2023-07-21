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
            <asp:Button ID="btnCorrectDB" runat="server" Text="Arreglar BD" OnClick="btnCorrect" />
        </div>
    </form>
</body>
</html>
