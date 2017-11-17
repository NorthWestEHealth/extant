<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Extant.Web.Models.DocumentApprovalModel>>" %>

<%@ Import Namespace="Extant.Web.Helpers" %>

<%@ Import Namespace="Extant.Web.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Document Approval <%= "" %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Approval of Uploaded Documents</h2>

<div><p>The following documents have been uploaded to studies in your disease areas and are awaiting approval for publishing to the catalogue. Please review the documents and approve or delete as appropriate.</p></div>

    <div>
        <dl>
            <% int laststudyid = -1;  foreach (var m in Model)
                {
                    if (m.StudyId != laststudyid)
                    { %>
            <dt><%= m.StudyTitle %>
                <form class="study-approve">
                    <input type="hidden" name="StudyId" value="<%= m.StudyId %>" />
                    <input type="submit" name="submit" value="Approve All" title="Approve all outstanding documents for this study" />
                </form>
            </dt> 
             <%         laststudyid = m.StudyId;
                                                                  } %>
                <dd><%= m.Document.ToString() %> 
                    <form class="doc-preview" target="_blank" action="/Admin/Preview" method="get">
                        <input type="hidden" name="StudyId" value="<%= m.StudyId %>" />
                        <input type="hidden" name="Type" value="<%= m.Document %>" />
                        <% if (m.Document == DocumentApprovalModel.DocumentType.AdditionalDocument) { %><input type="hidden" name="FileName" value="<%= m.DocumentDetails.FileName %>" /><% } %>
                        <input type="submit" name="submit" value="Preview" />
                    </form>
                    <form class="doc-approve">
                        <input type="hidden" name="StudyId" value="<%= m.StudyId %>" />
                        <input type="hidden" name="Type" value="<%= m.Document %>" />
                        <% if (m.Document == DocumentApprovalModel.DocumentType.AdditionalDocument) { %><input type="hidden" name="FileName" value="<%= m.DocumentDetails.FileName %>" /><% } %>
                        <input type="submit" name="submit" value="Approve" />
                    </form>
                </dd>
            <% } %>
        </dl>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $("form.doc-approve").ajaxForm({
            method: "post",
            action: "/Admin/ApproveSingle",
            success: function (response, status, request, form) { 
                form.replaceWith('<span style="color: #080;">Document Approved</span>');
            },
            error: function () { alert("An error occurred while attempting to approve this document.\n\nPlease refresh the list and try again."); }
        });
        $("form.study-approve").ajaxForm({
            method: "post",
            action: "/Admin/ApproveAll",
            success: function (response, status, request, form) {
                form.parent.siblings.remove("form");
                form.replaceWith('<span style="color: #080;">All Documents Approved</span>');
            },
            error: function () { alert("An error occurred while attempting to approve the documents for this study.\n\nPlease refresh the list and try again."); }
    })
    })
</script>
</asp:Content>
