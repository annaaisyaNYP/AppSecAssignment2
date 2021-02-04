<%@ Page Title="Settings" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="AppSecAssignment2.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Settings</h2>
    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    <br />
    </br>
    <table class="nav-justified">
        <caption>
            Change Password&nbsp; </br>
                <asp:Label ID="lbMsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lbSuccessMsg" runat="server" ForeColor="Lime"></asp:Label>
            </caption>
        <tr>
            <td style="width: 30%; height: 30px;">
                <asp:TextBox ID="tbCurrPass" runat="server" placeholder="Current Password" TextMode="Password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%; height: 30px;">
                <asp:TextBox ID="tbNewPass" runat="server" placeholder="New Password" TextMode="Password" onkeyup="javascript:validate()"></asp:TextBox>
                <asp:Label ID="lbPassStrength" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%; height: 30px;">
                Requirements: minimum 8 characters, at least one of each: lowercase, uppercase, number, and special character ( !, *, @, #, $, %, ^, &amp;, +, = )</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%; height: 30px;">
                <asp:Button ID="btnChaPass" runat="server" Text="Save" OnClick="btnChaPass_Click" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%= tbNewPass.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById('<%= lbPassStrength.ClientID %>').innerHTML = "Password length must be at least 8 characters.";
                document.getElementById('<%= lbPassStrength.ClientID %>').style.color = "Red";
                return "too_short";
            }

            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById('<%= lbPassStrength.ClientID %>').innerHTML = "Password require some uppercase letters";
                document.getElementById('<%= lbPassStrength.ClientID %>').style.color = "#FFCC00";
                return "no_upc";
            }

            else if (str.search(/[a-z]/) == -1) {
                document.getElementById('<%= lbPassStrength.ClientID %>').innerHTML = "Password require some lowecase letters";
                document.getElementById('<%= lbPassStrength.ClientID %>').style.color = "#FFCC00";
                return "no_lwc";
            }

            else if (str.search(/[0-9]/) == -1) {
                document.getElementById('<%= lbPassStrength.ClientID %>').innerHTML = "Password require at least 1 number";
                document.getElementById('<%= lbPassStrength.ClientID %>').style.color = "#FFCC00";
                return "no_number";
            }

            else if (str.search(/[!*@#$%^&+=]/) == -1) {
                document.getElementById('<%= lbPassStrength.ClientID %>').innerHTML = "Password require at least 1 special character";
                document.getElementById('<%= lbPassStrength.ClientID %>').style.color = "#FFCC00";
                return "no_spc";
            }

            document.getElementById('<%= lbPassStrength.ClientID %>').innerHTML = "Excellent";
            document.getElementById('<%= lbPassStrength.ClientID %>').style.color = "Lime";
            return "good";
        }
    </script>
</asp:Content>
