<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Extant.Web.Models.SetupModel>"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Setup
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>Extant Catalogue Initial Setup Page</h3>
    </div>
    <div>
        <p>Thank you for completing the initial Setup for your <strong><em>Extant</em></strong> Catalogue. Please return to the <a href="/">Home Page</a> to start using the applcation.</p>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>