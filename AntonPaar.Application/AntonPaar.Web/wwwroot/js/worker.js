var cacheResponse;
var totalPagesfound;
self.addEventListener('message', function (e) {
        CallReadFile(e.data[0], e.data[1], e.data[2]).then(res => {
            console.log(res);
            this.cacheResponse = res.listOfWordsCountModels;
            this.totalPagesfound = res.totalPages;
            let formatedData = formatTableString();
            //console.log(formatedData);
            let formatTotalPagesdata = formatTotalPages();
            this.self.postMessage([formatedData, formatTotalPagesdata, this.totalPagesfound]);
        });
})

function formatTotalPages() {
    var domtext = "<span>There are total " + this.totalPagesfound + " pages found. Please enter the specific page number if you wish to see it</span>";
    return domtext;
}

function formatTableString() {
    var domtable = "";
    for (var i = 0; i < cacheResponse.length; i++) {
        domtable += "<tr>";
        domtable += "<td>" + cacheResponse[i].wordsCount + "</td>";
        domtable += "<td>" + cacheResponse[i].distinctWords + "</td>";
        domtable += "</tr>";
    }
    return domtable;
}

function CallReadFile(givenfilename, givenpagenumber,givenpagesize) {
    return new Promise((resolve, reject) => {
        fetch('/ReadFile/ReadFile', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'fileSelected': givenfilename,
                'pagenumber': givenpagenumber,
                'pagesize':givenpagesize
            },
        })
            .then(response => {
                if (response.ok) {
                    resolve(response.json());
                } else {
                    reject('API call failed with status: ' + response.status);
                }
            })
            .catch(error => {
                reject(error);
            });
    });
}