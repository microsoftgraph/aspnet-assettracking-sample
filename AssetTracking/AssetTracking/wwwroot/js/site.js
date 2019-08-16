// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#t_items').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": [-1, -2] }
        ]
    });
    $('#t_borrowed_items').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": -1 }
        ]
        
    });
    $('#tableBooks').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": [0, -1] }
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
            { "data": "isbn" },
            { "data": "title" },
            { "data": "Author0" },
            { "data": "BookTitle" },
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
        var description = $('#BookDescription').val();
        $.ajax({
            type: 'POST',
            url: '/OfficeBooks/AddBook',
            data: {
                ItemId: itemId,
                ISBN: isbn,
                ResourceId: "2",
                Title: title,
                Author: author,
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
        var description = $('#BookDescription').val();

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
        var description = $('#BookDescription').val();

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
            $('#ISBN').val(result.isbn);
            $('#Title').val(result.title);
            $('#Author').val(result.Author0);
            $('#BookDescription').val(result.BookTitle);
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
            $('#ISBN').val(result.isbn).prop('disabled', true);
            $('#Title').val(result.title).prop('disabled', true);
            $('#Author').val(result.Author0).prop('disabled', true);
            $('#BookDescription').val(result.BookTitle).prop('disabled', true);
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

