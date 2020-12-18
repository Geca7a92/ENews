$(function () {
    $("#ddlcategory").change(getSubCategories)
})

$(onload => getSubCategories());

function getSubCategories () {
    $.getJSON("/MembersArea/Article/GetSubcategories", { id: $("#ddlcategory").val() }, function (d) {
        var row = "";
        $("#ddlsubCategory").empty();
        $.each(d, function (i, v) {
            row += "<option value=" + v.id + ">" + v.title + "</option>";
        });
        $("#ddlsubCategory").html(row);
    })
    $.ajax()
}