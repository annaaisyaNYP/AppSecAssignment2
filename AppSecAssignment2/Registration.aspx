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
                    <asp:TextBox ID="tbPass" runat="server" style="width: 210px" TextMode="Password"></asp:TextBox>
                </td>
                <td style="width: 200px">&nbsp;</td>
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
    
</asp:Content>
