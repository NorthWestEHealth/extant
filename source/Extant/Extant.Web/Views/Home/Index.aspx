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
            <% if (Request.IsLocal && String.IsNullOrEmpty(ConfigurationManager.AppSettings["SetupDate"])) { %>
            <p><strong>If you have not created an Administrator account for Extant yet please <a href="/Setup/">complete initial system setup</a></strong></p>
            <% } %>

            <p style="font-size: 120%; margin-right: 13em;">Welcome to the UK-RiME Data Catalogue. UK-RiME support the view that publicly funded research data should be <a href="https://www.force11.org/group/fairgroup/fairprinciples">Findable, Accessible, Interoperable and Reusable (FAIR)</a>.  The catalogue is intended to facilitate data sharing by recording the details of all UK-RiME affiliated musculoskeletal research studies in an easily searchable form.</p>
            <p>Use the catalogue to share study details and discover datasets of interest.</p>
            <ul class="arrowed">
                <li><p><strong>Share:</strong> If you are a researcher or study coordinator who would like to add their study to the catalogue, please <a href="/Account/Register">Register for a user account</a>.</p>
                    <p>Once your account has been approved you will be able to enter your studies onto the system.</p>
                </li>
                <li><strong>Discover:</strong> To search for a study, click the button to the right.</li>
            </ul>
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