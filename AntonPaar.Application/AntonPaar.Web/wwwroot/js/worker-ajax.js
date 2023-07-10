self.addEventListener("callingapi", function (e) {
    var d = e.data;
    debugger;
    console.log(e.data);
    let formatedData = callservice(e.data);
    console.log("formatedData");
    console.log(formatedData);
    this.self.postMessage(formatedData);
});

function callservice(parameterValue) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/ReadFile/ReadFile",
        dataType: "json",
        data: { fileName: parameterValue },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $("#tblresults").find("tr:gt(0)").remove();
            if (data != null) {
                console.log(data);
                dataResults = data;
                //loadTable(dataResults);
                //$("#divresults").show();
                return formatTableString(data);
            }
        },
        error: function () {
            alert("Error occured!!");
        }
    });
}
function formatTableString(data) {
    var domtable = "";
    for (var i = 0; i < data.length; i++) {
        domtable += "<tr>";
        domtable += "<td>" + data[i].wordsCount + "</td>";
        domtable += "<td>" + data[i].distinctWords + "</td>";
        domtable += "</tr>";
    }
    return domtable;
}