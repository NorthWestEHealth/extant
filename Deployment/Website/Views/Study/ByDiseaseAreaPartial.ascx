﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Extant.Web.Models.ByDiseaseAreaModel>" %>
<%@ Import Namespace="NWeH.Paging" %>

<ul class="arrowed">
<% foreach (var study in Model.Studies){ %>
    <li><a href="/Study/Index/<%:study.Id %>"><%: study.StudyName %></a>
    <div class="description"><span class="ellipsis_text"><%:study.Description %></span></div>
    </li>
<% } %>
</ul>

<div class="pager">
    <%=Html.AjaxPager(Model.Studies.PageSize, Model.Studies.PageIndex, Model.Studies.TotalItemCount, "search-results",
                        new { controller = "Study", action = "ByDiseaseArea", _id = Model.DiseaseArea.Id })%>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('.description').ThreeDots({ max_rows: 2 });
    });
</script>
