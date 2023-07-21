<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PetShop.Pages.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Inicio de sesión</title>

</head>
<body>
      <h2>Inicio de sesión</h2>
    <form id="formLogin" runat="server">
         <div>
           <asp:Label runat="server" Text="Nombre del usuario" />
           <asp:TextBox ID="txtBoxLogin" runat="server" TextMode="SingleLine" /> 
             <%--<asp:RequiredFieldValidator id="rfvLogin" runat="server"
  ControlToValidate="txtBoxLogin"
  ErrorMessage="El usuario es requerido."
  ForeColor="Red">
</asp:RequiredFieldValidator>--%>
        </div>

    <div>
      <asp:Label runat="server" Text="Contraseña" />
      <asp:TextBox ID="txtBoxPassword" runat="server" TextMode="Password"/>
<%--        <asp:RequiredFieldValidator id="rfvPassword" runat="server"
  ControlToValidate="txtBoxPassword"
  ErrorMessage="La contraseña es requerida."
  ForeColor="Red">
</asp:RequiredFieldValidator>
    </div>--%>
        <br />
    <asp:Button ID="btnLogin" type="submit" runat="server" Text="Iniciar sesión" OnClick="btnLogin_Click"/>
        <asp:Label runat="server" ID="lblUserNotExist" Text="El usuario no existe." Visible="false" />
    </form>
</body>
</html>
