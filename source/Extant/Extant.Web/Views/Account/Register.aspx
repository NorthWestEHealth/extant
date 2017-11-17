<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.RegisterModel>" %>

<%@ Import Namespace="Extant.Web.Helpers" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Create a New Account
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>Passwords must adhere to the University of Manchester's complexity standards:</p>
        <ul style="font-style: italic;">
            <li>between 10 and 16 characters in length</li>
            <li>contain at least:
                <ul>
                    <li>one lower case letter</li>
                    <li>one UPPER CASE letter</li>
                    <li>one numeral (0-9)</li>
                    <li>one special character from the range: <strong>! @ # $ % ^ & + = / ? [ ] . , _ ~ -</strong></li>
                </ul>
            </li>
        </ul>
        <p><strong>You must not use the same password as your UoM Central IT password (or equivalent if from another institution).</strong></p>

    <% using (Html.BeginForm(null, null, FormMethod.Post, new { @autocomplete = "off" })){ %>
        <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
        <fieldset>
            <%: Html.LabelValidationAndTextBoxFor(m => m.Name, new { @class = "login" }, true )%>
            <%: Html.LabelValidationAndTextBoxFor(m => m.Email, new { @class = "login" }, true)%>
            <%: Html.LabelValidationAndTextBoxFor(m => m.Affiliation, new { @class = "login" }, true) %>
            <%: Html.LabelValidationAndDropDownListFor(m => m.DiseaseAreaId, new SelectList(Model.DiseaseAreas, "Id", "DiseaseAreaName"), "-- Please select --", null, true)%>
            <div class="form-row"><em>If you are involved in multiple disease areas please select the one which you work in most of the time</em></div>
            <%: Html.LabelValidationAndPasswordFor(m => m.Password, new { @class = "login" }, true)%>
            <%: Html.LabelValidationAndPasswordFor(m => m.ConfirmPassword, new { @class = "login" }, true)%>
            <%: Html.LabelValidationAndYesNoRadioFor(m => m.AcceptTerms) %> <span style="font-style: italic;">I confirm that I have read and understand the <a href="/Terms">terms &amp; conditions and privacy page</a> and agree to the terms therein.</span>
                
            <div class="form-row">
                <button type="submit">Register</button>
            </div>
        </fieldset>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>" type="text/javascript"></script>
</asp:Content>