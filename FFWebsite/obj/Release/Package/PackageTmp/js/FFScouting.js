var pdb = null;
$(document).ready(function () {
    console.log('starting...');
    openDB();
    testAdd();
    getAllRecords();
});

function openDB() {
    pdb = new PouchDB("Teams");
}

function getAllRecords() {
    console.log('get all records...');
    if (pdb != null && pdb != undefined) {
        pdb.allDocs({
            include_docs: true,
            attachments: false
        }).then(function (result) {
          //  console.log('results: ' + result);

            $.each(result.rows, function (key, data) {
                var output = "id: " + data.id + " :: " + data.doc.teamName;
                $('#dataHere').append(output);
            });

        }).catch(function (err) {
            console.log(err);
        }); 
    }
}

function addRecord(newRecord) {
    if (pdb != null && pdb != undefined) {
        pdb.put(newRecord).then(function (response) {
            // handle response
            console.log('response: ' + response);
        }).catch(function (err) {
            console.log(err);
        });
    }
}

function testAdd() {
    var testAddRecord = {
        _id: '503',
        teamName: 'Frog Force'
    };
    addRecord(testAddRecord);
}