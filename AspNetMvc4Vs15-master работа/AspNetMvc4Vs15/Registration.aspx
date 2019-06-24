<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AspNetMvc4Vs15.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table ID="Table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Label ID="Label1" runat="server" Text="Регистрация"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell >
                        <asp:Label ID="Label2" runat="server" Text="Login"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell >
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell >
                        <asp:Label ID="Label3" runat="server" Text="Password"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell >
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell >
                        <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Login.aspx" runat="server">На главную</asp:HyperLink>
                    </asp:TableCell>
                    <asp:TableCell  HorizontalAlign="Right">
                        <asp:Button ID="Button1" runat="server" Text="Зарегистрироваться" OnClick="Button2_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>
