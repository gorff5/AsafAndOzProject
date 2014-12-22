<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
        
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="nameL" runat="server" Text="Name:" Visible="False"></asp:Label>
            <asp:TextBox ID="nameT"  runat="server" Text="" Visible="False"></asp:TextBox>
              <p/>
            <asp:Label ID="mailL" runat="server" Text="Mail:"></asp:Label>
            <asp:TextBox ID="mailT" hint="mail" runat="server" Text=""></asp:TextBox>
              <p/>
            <asp:Label ID="passwordL" runat="server" Text="Password:"></asp:Label>
            <asp:TextBox ID="passwordT"  runat="server" Text=""></asp:TextBox>
              <p/> 
             <asp:Button ID="login_button" runat="server" Text="Login" OnClick="login_Click" />
             <asp:Button ID="register_button" runat="server" Text="Register" OnClick="register_Click" />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
