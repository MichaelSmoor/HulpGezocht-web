<%@ Page Title="Registreren" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Hulpgezocht_web.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <hr />
        <!--<asp:ValidationSummary runat="server" CssClass="text-danger" />-->
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Naam" CssClass="col-md-2 control-label">Naam</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Naam" CssClass="form-control" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Naam"
                    CssClass="text-danger" ErrorMessage="Het naam veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="Het email veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Wachtwoord</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="Het wachtwoord veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Wachtwoord herhalen</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Het wachtwoord herhaal veld is verplicht." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" ErrorMessage="De wachtwoorden komen niet overeen." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PhoneNumber" CssClass="col-md-2 control-label">Telefoonnummer</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="PhoneNumber" CssClass="form-control" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PhoneNumber"
                    CssClass="text-danger" ErrorMessage="Het telefoonnummer veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DateOfBirth" CssClass="col-md-2 control-label">Geboortedatum</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="DateOfBirth" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="PhoneNumber"
                    CssClass="text-danger" ErrorMessage="Het telefoonnummer veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:CheckBox ID="DriversLicense" runat="server" CssClass="checkbox"  Text="Ik ben in het bezit van een rijbewijs"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Registreren" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
