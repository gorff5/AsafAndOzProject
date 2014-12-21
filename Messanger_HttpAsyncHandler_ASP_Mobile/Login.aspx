<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <script language="javascript">

        function UserLogin(userName, password) {
            this.userName = userName;
            this.password = password;
        }

        function buttonClick() {
            var userName = getElmentById("userName").value;
            var password = getElmentById("password").value;
            var jsonObj = JSON.stringify({ userName: userName, password: password });

                try {
                    xmlHttp_OneTime = new ActiveXObject("Microsoft.XMLHTTP");
                }
                catch (e) {
                    try {
                        xmlHttp_OneTime = new XMLHttpRequest()
                    }
                    catch (e) {
                    }
                }
                var url = "LoginHandler.ashx?JSON=" + jsonObj;
            xmlHttp_OneTime.open("POST", url, true);
            xmlHttp_OneTime.onreadystatechange = getResponse_Connect;
            xmlHttp_OneTime.send();

        }
    </script>
        
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server">
            <asp:TextBox ID="mail" runat="server" Text=""></asp:TextBox>
              <p/>
            <asp:TextBox ID="password"  runat="server" Text=""></asp:TextBox>
              <p/>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
