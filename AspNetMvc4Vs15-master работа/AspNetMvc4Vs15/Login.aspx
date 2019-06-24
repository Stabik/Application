<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AspNetMvc4Vs15.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table ID="Tabel1" runat="server" Width="62px">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Label ID="Label1" runat="server" Text="Форма входа"></asp:Label>

                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Login"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>                       
                        <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
                    </asp:TableCell>
                     <asp:TableCell>
                         <asp:TextBox ID="TextBox2" TextMode="Password" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Registration.aspx" runat="server">Зарегистрироваться</asp:HyperLink>
                    </asp:TableCell>
                    <asp:TableCell  HorizontalAlign="Right">
                        <asp:Button ID="Button1" runat="server" Height="35" Width="79" Text="Войти" OnClick="Button1_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
