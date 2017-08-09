<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Extant.Web.Models.StudyBasicModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="home-content">
        <div id="home-search">
            <div id="home-search-top">
                <strong>Search for a study</strong>
            </div>
            <div id="home-search-bottom">
                <form action="/Study/Search" method="get">
                    <input type="text" name="term" id="home-search-textbox" />
                    <input type="submit" class="home-search-button" value="" />
                </form>
            </div>
        </div>

        <div id="home-main">
            <h2>Welcome</h2>

            <p>Welcome to MskSearch, the UKRiME catalogue of musculoskeletal research studies.</p>
            <% if (Request.IsLocal && String.IsNullOrEmpty(ConfigurationManager.AppSettings["SetupDate"])) { %>
            <p><strong>If you have not created an Administrator account for Extant yet please <a href="/Setup/">complete initial system setup</a></strong></p>
            <% } %>
            <p>The catalogue is intended to record the details of all UKRiME affiliated research studies in an easily searchable form. To search for a study click the button to the right.</p>

            <p>If you are a researcher or study coordinator who would like to add their study or studies to the catalogue you first need to <a href="/Account/Register">Register</a> for a user account. 
            Once your account has been approved you will be able to enter your studies onto the system.</p>
        </div>
    </div>

    <div class="topborder clear">
        <p><em>The most recent studies added to the catalogue:</em></p>
        <ul class="arrowed">
    <% foreach (var study in Model){ %>
            <li><a href="/Study/Index/<%:study.Id %>"><%: study.StudyName %></a>
                <div class="description"><span class="ellipsis_text"><%:study.Description %></span></div>
            </li>
    <% } %>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>