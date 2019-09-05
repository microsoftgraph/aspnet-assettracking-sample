// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#tableItems').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": [-1, -2] }
        ],
        "ajax": {
            "url": "/OfficeItems/GetOfficeItems",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "ItemID",
                'visible': false
            },
            { "data": "Title" },
            { "data": "SerialNo" },
            { "data": "Description" },
            {
                "data": "ItemID",
                "render": function (data) {
                    return '<a href="#" id="editing" onclick="UpdateOfficeItem(' + data + ')"> Edit </a>|<a href="#" id="deleting" onclick="DeleteOfficeItem(' + data + ')"> Delete </a>'
                }
            }
        ]  
    });
    $('#t_borrowed_items').DataTable({
        "columnDefs": [
            {
                "orderable": false,
                "targets": -1
            }
        ]
        
    });
    $('#tableBooks').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": [0, -1] }
        ]
    });
    $('#t_borrowed_books').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": [0, -1] }
        ]
    });
    $('#t_current').DataTable();
});

var st = document.getElementById("due_date").innerHTML;
console.log(st);
var dt = new Date(st);
var dt = dt.toLocaleDateString('en-GB');
console.log(dt);
var due_date = new Date(dt);
console.log(due);
var td = new Date();

function due(td) {
    if (due_date < td) {
        console.log("Book is overdue");
        document.getElementById("overdue").style.visibility = "visible";
    }
    else {
        document.getElementById("overdue").style.visibility = "hidden";
    };
};

due();