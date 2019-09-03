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
            { "orderable": false, "targets": -1 }
        ]
        
    });
    $('#tableBooks').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": [0, -1, -2,] }
        ],
        "ajax": {
            "url": "/OfficeBooks/OfficeBooksGet",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "BookID",
                'visible': false
            },
            { "data": "ISBN" },
            { "data": "Title" },
            { "data": "Author0" },
            { "data": "Description" },
            {
                "data": "BookID",
                "render": function (data) {
                    return '<a href="#" id="editing" onclick="UpdateOfficeBook(' + data + ')"> Edit </a>|<a href="#" id="deleting" onclick="DeleteOfficeBook(' + data + ')"> Delete </a>'
                }
            }
        ]  
    });

    $('#t_current').DataTable();

    $("#SubmitNewOfficeBook").click(function () {
        var itemId = $('#ItemId').val();
        var isbn = $('#ISBN').val();
        var title = $('#Title').val();
        var author = $('#Author').val();
        var description = $('#Description').val();
        $.ajax({
            type: 'POST',
            url: '/OfficeBooks/AddBook',
            data: {
                ItemId: itemId,
                ISBN: isbn,
                ResourceId: "2",
                Title: title,
                Author: author0,
                BookDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Book succesfully added");
                    $('#EditBookForm').modal("hide");
                    $('#tableBooks').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to add book");
                }
            })
        });

    });

    $("#SubmitUpdateOfficeBook").click(function () {

        var itemId = $('#ItemId').val();
        var isbn = $('#ISBN').val();
        var title = $('#Title').val();
        var author = $('#Author').val();
        var description = $('#Description').val();

        $.ajax({
            type: 'POST',
            url: '/OfficeBooks/UpdateBook',
            data: {
                ItemId: itemId,
                ISBN: isbn,
                Title: title,
                ResourceId: "2",
                Author: author,
                BookDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Book succesfully updated");
                    $('#EditBookForm').modal("hide");
                    $('#tableBooks').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to update book");
                }
            })
        });
    });
    $("#SubmitDeleteOfficeBook").click(function () {
        var itemId = $('#ItemId').val();
        var isbn = $('#ISBN').val();
        var title = $('#Title').val();
        var author = $('#Author').val();
        var description = $('#Description').val();

        $.ajax({
            type: 'POST',
            url: '/OfficeBooks/DeleteBook',
            data: {
                ItemId: itemId,
                ISBN: isbn,
                Title: title,
                ResourceId: "2",
                Author: author,
                BookDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Book succesfully deleted");
                    $('#EditBookForm').modal("hide");
                    $('#tableBooks').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to delete book");
                }
            })
        });
    });
    $("#SubmitNewOfficeItem").click(function () {
        var itemId = $('#ItemID').val();
        var serialNo = $('#SerialNo').val();
        var itemName = $('#Title').val();
        var description = $('#ItemDescription').val();
        $.ajax({
            type: 'POST',
            url: '/OfficeItem/AddItem',
            data: {
                ItemId: itemId,
                Title: itemName,
                ResourceId: "1",
                SerialNo: serialNo,
                ItemDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Item succesfully added");
                    $('#EditItemForm').modal("hide");
                    $('#tableItems').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to add item");
                }
            })
        });

    });

    $("#SubmitUpdateOfficeItem").click(function () {

        var itemId = $('#ItemID').val();
        var serialNo = $('#SerialNo').val();
        var itemName = $('#Title').val();
        var description = $('#ItemDescription').val();

        $.ajax({
            type: 'POST',
            url: '/OfficeItems/UpdateItem',
            data: {
                ItemId: itemId,
                Title: itemName,
                ResourceId: "1",
                SerialNo: serialNo,
                ItemDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Item succesfully updated");
                    $('#EditItemForm').modal("hide");
                    $('#tableItems').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to update item");
                }
            })
        });
    });
    $("#SubmitDeleteOfficeItem").click(function () {
        var itemId = $('#ItemID').val();
        var serialNo = $('#SerialNo').val();
        var itemName = $('#Title').val();
        var description = $('#ItemDescription').val();

        $.ajax({
            type: 'POST',
            url: '/OfficeItem/DeleteItem',
            data: {
                ItemId: itemId,
                Title: itemName,
                ResourceId: "1",
                SerialNo: serialNo,
                ItemDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Item succesfully deleted");
                    $('#EditItemForm').modal("hide");
                    $('#tableItems').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to delete item");
                }
            })
        });
    });
});
function UpdateOfficeBook(ItemId) {
    $('#SubmitNewOfficeBook').hide();
    $('#SubmitDeleteOfficeBook').hide();
    $('#SubmitUpdateOfficeBook').show();
    $('#change').text('Edit');
    $.ajax({
        url: '/OfficeBooks/OfficeBooksGetbyId',
        data: { Id: ItemId },
        success: (function (result) {
            $('#ItemId').val(result.BookID);
            $('#ISBN').val(result.ISBN);
            $('#Title').val(result.Title);
            $('#Author').val(result.Author0);
            $('#Description').val(result.Description);
            $('#EditBookForm').modal();
        })
    });

}

function DeleteOfficeBook(ItemId) {
    $('#SubmitNewOfficeBook').hide();
    $('#SubmitUpdateOfficeBook').hide();
    $('#SubmitDeleteOfficeBook').show();
    $('#change').text('Delete');
    $('#warnMessage').text('Are you sure you want to delete this item?');
    $.ajax({
        url: '/OfficeBooks/OfficeBooksGetbyId',
        data: { Id: ItemId },
        success: (function (result) {
            $('#ItemId').val(result.BookID);
            $('#ISBN').val(result.ISBN).prop('disabled', true);
            $('#Title').val(result.Title).prop('disabled', true);
            $('#Author').val(result.Author0).prop('disabled', true);
            $('#Description').val(result.Description).prop('disabled', true);
            $('#EditBookForm').modal();
        })
    });
}

function AddOfficeBook() {
    $('#EditBookForm').modal();
    $('#change').text('Add');
    $('#SubmitNewOfficeBook').show();
    $('#SubmitUpdateOfficeBook').hide();
    $('#SubmitDeleteOfficeBook').hide();
}

function UpdateOfficeItem(ItemId) {
    $('#SubmitNewOfficeItem').hide();
    $('#SubmitDeleteOfficeItem').hide();
    $('#SubmitUpdateOfficeItem').show();
    $('#itemChange').text('Edit');
    $.ajax({
        url: '/OfficeItems/GetItemsById',
        data: { Id: ItemId },
        success: (function (result) {
            $('#ItemID').val(result.ItemID);
            $('#SerialNo').val(result.SerialNo);
            $('#Title').val(result.Title);
            $('#ItemDescription').val(result.Description);
            $('#EditItemForm').modal();
        })
    });

}

function DeleteOfficeItem(ItemId) {
    $('#SubmitNewOfficeItem').hide();
    $('#SubmitUpdateOfficeItem').hide();
    $('#SubmitDeleteOfficeItem').show();
    $('#itemChange').text('Delete');
    $('#warnMessage').text('Are you sure you want to delete this item?');
    $.ajax({
        url: '/OfficeItems/GetItemsById',
        data: { Id: ItemId },
        success: (function (result) {
            $('#ItemID').val(result.ItemID);
            $('#SerialNo').val(result.SerialNo).prop('disabled', true);
            $('#Title').val(result.Title).prop('disabled', true);
            $('#ItemDescription').val(result.Description).prop('disabled', true);
            $('#EditItemForm').modal();
        })
    });
}

function AddOfficeItem() {
    $('#EditItemForm').modal();
    $('#itemChange').text('Add');
    $('#SubmitNewOfficeItem').show();
    $('#SubmitUpdateOfficeItem').hide();
    $('#SubmitDeleteOfficeItem').hide();
}

