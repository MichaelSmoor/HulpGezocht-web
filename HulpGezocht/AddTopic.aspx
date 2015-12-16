<%@ Page Title="Vraag plaatsen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTopic.aspx.cs" Inherits="HulpGezocht.AddTopic" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <div class="form-horizontal">
        <hr />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Titel" CssClass="col-md-2 control-label">Titel</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Titel" CssClass="form-control textbox-register" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Titel"
                    CssClass="text-danger" ErrorMessage="Het titel veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Details" CssClass="col-md-2 control-label">Details</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Details" CssClass="form-control textbox-register" TextMode="MultiLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Details"
                    CssClass="text-danger" ErrorMessage="Het detail veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Locatie" CssClass="col-md-2 control-label">Locatie</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Locatie" CssClass="form-control textbox-register" TextMode="SingleLine" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Locatie"
                    CssClass="text-danger" ErrorMessage="Het titel veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Transportvorm" CssClass="col-md-2 control-label">Transportvorm</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="Transportvorm" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Reistijd" CssClass="col-md-2 control-label">Reistijd (in minuten)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Reistijd" CssClass="form-control textbox-register" TextMode="Number" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Reistijd"
                    CssClass="text-danger" ErrorMessage="Het reistijd veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:CheckBox ID="Urgent" runat="server" CssClass="checkbox" Text="Deze vraag is urgent"/>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:RadioButtonList runat="server" ID="DatumOpties" OnSelectedIndexChanged="DateOptionChange" AutoPostBack="true">
                    <asp:ListItem Selected="True">Specifieke datum</asp:ListItem>
                    <asp:ListItem>Tussen twee datums</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="Datum1Label" AssociatedControlID="Datum1" CssClass="col-md-2 control-label">Datum #1</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Datum1" CssClass="form-control textbox-register" TextMode="Date" />
                <asp:RequiredFieldValidator ID="Datum1Validator" runat="server" ControlToValidate="Datum1"
                    CssClass="text-danger" ErrorMessage="Dit datum veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" ID="Datum2Label" AssociatedControlID="Datum2" CssClass="col-md-2 control-label">Datum #2</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Datum2" CssClass="form-control textbox-register" TextMode="Date" />
                <asp:RequiredFieldValidator ID="Datum2Validator" runat="server" ControlToValidate="Datum2"
                    CssClass="text-danger" ErrorMessage="Dit datum veld is verplicht." Display="Dynamic" />
            </div>
        </div>
        
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Vraag plaatsen" CssClass="btn btn-default" onclick="AddTopic_Click"/>
            </div>
        </div>

    </div>

</asp:Content>
