const worker = new Worker('../js/worker.js');
var totalpage = 1;
var fileName = "";
var pagesize = 0;
var pagenumber = 1;
function Reset() {
    $("#fileNames").show();
    ResetTable();
}

function ResetTable() {
    pagesize = 0;
    totalpage = 1;
    pagenumber = 1;
    hidePopup();
    $("#divpagesize").hide();
    $("#tblresults").find("tr:gt(0)").remove();
    //$("pagination").remove();
    $("#pagenumber").val("");
    $("#pagination span").remove();
}

function uploadFile() {
    var fileUpload = $("#uploadfile").get(0);
    var selectedfile = fileUpload.files;

    var uploadedData = new FormData();

    for (var i = 0; i < selectedfile.length; i++) {
        uploadedData.append(selectedfile[i].name, selectedfile[i]);
    }
    $.ajax({
        url: '/ReadFile/UploadFiles',
        type: "POST",
        contentType: false,
        processData: false, 
        data: uploadedData,
        success: function (result) {
            alert(result);
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}

function onSelectingFileType(selectedFileType) {
    var selectedValue = selectedFileType.value;
    var dataResults;
    if (selectedFileType.checked == true) {

        $.ajax({
            type: "GET",
            url: "/ReadFile/GetListOfFiles",
            dataType: "json",
            data: { fileTypeSelected: selectedValue },
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data != null) {
                    dataResults = data;
                    loadDropDownFiles(data);
                    $("#fileNames").show();
                }
            },
            error: function () {
                alert("Error occured!!");
            }
        });
    }
}

function loadDropDownFiles(data) {
    $("#ddlFiles").find("option").remove().end();
    var domdropdownElement = "<option value='-- Select--'>--Select--</option>";
    for (var i = 0; i < data.length; i++) {
        domdropdownElement += "<option value='" + data[i] + "'>" + data[i] + "</option>";
    }
    $("#ddlFiles").append(domdropdownElement);
}

function filesDropDownOnChange() {
    ResetTable();
    showPopup();
    $("#divpagesize").show();
    fileName = $("#ddlFiles :selected").val();
    if (fileName != '--Select--')
        loadTable();
    else
        $("#tblresults").find("tr:gt(0)").remove();
}


function loadTable() {
    $("#tblresults").find("tr:gt(0)").remove();
    pagesize = $("#pagesize :selected").val();
    if (typeof (Worker) !== "undefined") {
        worker.postMessage([fileName, pagenumber, pagesize]);
        worker.onmessage = function (e) {
            $("#divresults").show();
            $("#tblresults").append(e.data[0]);
            $("#pagination").append(e.data[1]);
            totalpage = e.data[2];
            hidePopup();
        }
    }
}

function cancel() {
    //worker.postMessage(['close']);
    hidePopup();
    window.location.reload();
}

function pageChanged() {
    pagenumber = $("#pagenumber").val();
    var maxpages = totalpage + 1;
    if (pagenumber > 0 && pagenumber < maxpages) {
        $("#pagination span").remove();
        loadTable();
    }
    else {
        alert("Please enter a valid page number");
        return false;
    }
}

function showPopup() {

    //$(".popup-overlay, .popup-content").addClass("active");
    $('#myModal').show();
}

function hidePopup() {
    $('#myModal').hide();
    //$(".popup-overlay, .popup-content").removeClass("active");
}

