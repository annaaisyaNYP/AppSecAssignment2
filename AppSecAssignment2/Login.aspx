<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppSecAssignment2.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <script src="https://www.google.com/recaptcha/api.js?render=[ENTER_SITE_KEY]"></script>
    <h2>Login</h2>
    <table style="width:100%;">
        <caption>
        </caption>
        <tr style="height: 30px">
            <td style="width: 200px">&nbsp;</td>
            <td>
                <asp:Label ID="lbMsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 200px">Email</td>
            <td>
                <asp:TextBox ID="tbEmail" runat="server" TextMode="Email"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 200px">Password</td>
            <td>
                <asp:TextBox ID="tbPass" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 200px">&nbsp;</td>
            <td>
                <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 200px">&nbsp;</td>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" name ="action" value="Login"/>
            </td>
            <td>&nbsp;</td>
        </tr>
      </table>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('ENTER SITE KEY', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</asp:Content>
