<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.SetupModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Setup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>Extant Catalogue Initial Setup Page</h3>
    </div>
    <div>
        <p>To finish the setup and configuration of your <strong><em>Extant</em></strong> catalogue please complete the form below:</p>
    </div>
    <% if (!ViewData.ModelState.IsValid)
    { %>
    <div>The following errors were found with your previous setup values: please fix the errors and try again:<br />
        <%= Html.ValidationSummary() %>
    </div>
    <% } %>
    <div>
        <% Html.BeginForm("Complete", "Setup");  %>
        <fieldset>
        <h3>Adminstrator User Setup</h3>
            <%= Html.LabelFor(m => m.AdminUserName) %>: <%= Html.EditorFor(m => m.AdminUserName) %><br />
            <%= Html.LabelFor(m => m.AdminEmail) %>: <%= Html.EditorFor(m => m.AdminEmail) %><br />
            <%= Html.LabelFor(m => m.AdminPassword) %>: <%= Html.PasswordFor(m => m.AdminPassword) %><br />
            <%= Html.LabelFor(m => m.AdminRepeatPassword) %>: <%= Html.PasswordFor(m => m.AdminRepeatPassword) %><br />
        </fieldset>
        <fieldset>
            <h3>Organisation Details</h3>
            <%= Html.LabelFor(m => m.OrganisationName) %>: <%= Html.EditorFor(m => m.OrganisationName) %><br />
            <%= Html.LabelFor(m => m.CatalogueName) %>: <%= Html.EditorFor(m => m.CatalogueName) %><br />
            <%= Html.LabelFor(m => m.SupportEmail) %>: <%= Html.EditorFor(m => m.SupportEmail) %><br />
        </fieldset>
        <fieldset>
            <h3>Mailer Setup</h3>
            <%= Html.LabelFor(m => m.MailServerIPAddress) %>: <%= Html.EditorFor(m => m.MailServerIPAddress) %><br />
            <%= Html.LabelFor(m => m.MailUsername) %>: <%= Html.EditorFor(m => m.MailUsername) %><br />
            <%= Html.LabelFor(m => m.MailPassword) %>: <%= Html.EditorFor(m => m.MailPassword) %><br />
            <%= Html.LabelFor(m => m.FromEmail) %>: <%= Html.EditorFor(m => m.FromEmail) %><br />
        </fieldset>
        <input type="submit" />
        <% Html.EndForm(); %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>