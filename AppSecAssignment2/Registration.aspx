<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AppSecAssignment2.Registration" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
        <table style="width: 815px;">
            <caption>
                All fields are required.&nbsp;
            </caption>
            <tr style="height: 30px">
                <td style="width: 200px">&nbsp;</td>
                <td colspan="3">
                    <asp:Label ID="lbMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">First Name</td>
                <td style="width: 240px">
                    <asp:TextBox ID="tbFName" runat="server" style="width: 210px"></asp:TextBox>
                </td>
                <td style="width: 200px">Last Name</td>
                <td style="width: 200px">
                    <asp:TextBox ID="tbLName" runat="server" style="width: 210px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">Birth Date</td>
                <td style="width: 240px">
                    <asp:TextBox ID="tbBirthDate" runat="server" style="width: 210px" TextMode="Date"></asp:TextBox>
                </td>
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 200px">&nbsp;</td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 240px">&nbsp;</td>
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 200px">&nbsp;</td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">Credit Card Number</td>
                <td style="width: 240px">
                    <asp:TextBox ID="tbCCNo" runat="server" style="width: 210px"></asp:TextBox>
                </td>
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 200px">&nbsp;</td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">Expiry Month</td>
                <td style="width: 240px">
                    <asp:TextBox ID="tbCCExMth" runat="server" style="width: 210px" TextMode="Month"></asp:TextBox>
                </td>
                <td style="width: 200px; vertical-align:middle">CVV &nbsp;&nbsp;
                    <asp:TextBox ID="tbCVV" runat="server" Width="85px"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    &nbsp;</td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 240px">&nbsp;</td>
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 200px">&nbsp;</td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px; height: 23px;">Email</td>
                <td style="width: 240px; height: 23px;">
                    <asp:TextBox ID="tbEmail" runat="server" style="width: 210px" TextMode="Email"></asp:TextBox>
                </td>
                <td style="width: 200px; height: 23px;"></td>
                <td style="width: 200px; height: 23px;"></td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">Password</td>
                <td style="width: 240px">
                    <asp:TextBox ID="tbPass" runat="server" onkeyup="javascript:validate()" style="width: 210px" TextMode="Password"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    <asp:Label ID="lbPassStrength" runat="server"></asp:Label>
                </td>
                <td style="width: 200px">&nbsp;</td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">&nbsp;</td>
                <td colspan="3">Requirements: minimum 8 characters, at least one of each: lowercase, uppercase, number, and special character ( !, *, @, #, $, %, ^, &amp;, +, = )</td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 240px">
                    <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" />
                </td>
                <td style="width: 200px">&nbsp;</td>
                <td style="width: 200px">&nbsp;</td>
            </tr>
        </table>
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%= tbPass.ClientID %>').value;

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
