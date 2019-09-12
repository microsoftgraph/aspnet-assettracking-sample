// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    //hiding the div element that displays alert messages
    $('#result').hide();

    //Adding a click event to the 'x' button to close immediately
    $('.alert .close').on("click", function (e) {
        $(this).parent().fadeTo(500, 0).slideUp(500);
    });

    //Displays Office Items in a table
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
                    return '<a href="#EditItemForm" data-toggle="modal" onclick="UpdateOfficeItem(' + data + ')"> Edit </a>|<a href="#EditItemForm" data-toggle="modal" onclick="DeleteOfficeItem(' + data + ')"> Delete </a>'
                }
            },
            {
                "data": "ItemID",
                "render": function (data) {
                    return '<a href="#BorrowItemModal" onclick="BorrowItem(' + data + ')">Borrow</a>'
                }
            }
        ]  
    });

    //Displays Office Books in a table
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
                "data": "id",
                'visible': false
            },
            { "data": "ISBN" },
            { "data": "Title" },
            { "data": "Author0" },
            { "data": "Description" },
            {
                "data": "id",
                "render": function (data) {
                    return '<a href="#EditBookForm" data-toggle="modal" onclick="UpdateOfficeBook(' + data + ')"> Edit </a>|<a href="#EditBookForm" data-toggle="modal" onclick="DeleteOfficeBook(' + data + ')"> Delete </a>'
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return '<a href="#BorrowFormModal" id="borrowBook('+data+')" onclick="BorrowBook(' + data + ')">Borrow</a>'
                }
            }
        ]  
    });

    //Display's currently borrowed Items and Books
    $('#currentlyBorrowed').DataTable({
        "columnDefs": [
                { "orderable": false, "targets": [0, -1, -2,] }
        ],
        "ajax": {
            "url": "",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "BorrowedResourceID",
                'visible': false
            },
            { "data": "Title" },
            { "data": "BorrowDate" },
            { "data": "DueDate" },
            {
                "data": "BorrowedResourceID",
                "render": function (data) {
                    return '<a href="#" id="current(' + data + ')"> Return </a>'
                }
            }
        ]
    });

    //resetting form elements whenever a div is closed
    $('.modal').on('hidden.bs.modal', function () {
        $(this).find('form')[0].reset();
    });

    //Posting book details when one Adds a book
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
                itemId: itemId,
                ISBN: isbn,
                ResourceId: "1",
                Title: title,
                Author: author,
                Description: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    $('#result').show();
                    $('#result').addClass("alert alert-success alert-dismissible fade show");
                    $('#result').html('<button type="button" class="close">×</button><b>Success!</b> Book Successfully Added</div >');
                    $('#EditBookForm').modal("hide");
                    $('#tableBooks').DataTable().ajax.reload();
                }
                else {
                    $('#result').show();
                    $('#result').addClass("alert alert-danger alert-dismissible fade show");
                    $('#result').html('<button type="button" class="close">×</button><strong>Error!</strong> Unable to Add book</div >');
                }
            })
        });
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 5000);
    });

    //Posting book details when one Updates a book
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
                itemId: itemId,
                isbn: isbn,
                title: title,
                ResourceId: "1",
                Author: author,
                Description: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    $('#result').show();
                    $('#result').addClass("alert alert-success alert-dismissible fade show");
                    $('#result').html('<button type="button" class="close">×</button><b>Success!</b> Book Successfully Updated</div >');
                    $('#EditBookForm').modal("hide");
                    $('#tableBooks').DataTable().ajax.reload();
                }
                else {
                    $('#result').show();
                    $('#result').addClass("alert alert-danger alert-dismissible fade show");
                    $('#result').html('<button type="button" class="close">×</button><strong>Error!</strong> Unable To Update Book</div >');
                }
            })
        });
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 5000);
    });

    //Posting book details when one Deletes a book
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
                itemId: itemId,
                isbn: isbn,
                title: title,
                ResourceId: "1",
                Author: author,
                Description: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    $('#result').show();
                    $('#result').addClass("alert alert-success alert-dismissible fade show");
                    $('#result').html('<button type="button" class="close">×</button><b>Success!</b> Book Successfully Deleted</div>');
                    $('#EditBookForm').modal("hide");
                    $('#tableBooks').DataTable().ajax.reload();
                }
                else {
                    $('#result').show();
                    $('#result').addClass("alert alert-danger alert-dismissible fade show");
                    $('#result').html('<button type="button" class="close">×</button><strong>Error!</strong> Unable To Delete Book</div >');
                }
            })
        });
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 5000);
    });

    //Adding a new Office Item 
    $("#SubmitNewOfficeItem").click(function () {
        var itemId = $('#ItemId').val();
        var serialNo = $('#SerialNo').val();
        var itemName = $('#Title').val();
        var description = $('#ItemDescription').val();
        $.ajax({
            type: 'POST',
            url: '/OfficeItems/AddItem',
            data: {
                ItemId: itemId,
                Title: itemName,
                ResourceId: "2",
                SerialNo: serialNo,
                ItemDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    $('#itemResult').show();
                    $('#itemResult').addClass("alert alert-success alert-dismissible fade show");
                    $('#itemResult').html('<button type="button" class="close">×</button><b>Success!</b> Item Successfully Added</div >');
                    $('#EditItemForm').modal("hide");
                    $('#tableItems').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to add item");
                }
            })
        });
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 5000);
    });

    //Posting an Item's Details when one Updates an Item
    $("#SubmitUpdateOfficeItem").click(function () {

        var itemId = $('#ItemId').val();
        var serialNo = $('#SerialNo').val();
        var itemName = $('#Title').val();
        var description = $('#ItemDescription').val();

        $.ajax({
            type: 'POST',
            url: '/OfficeItems/UpdateItem',
            data: {
                ItemId: itemId,
                Title: itemName,
                ResourceId: "2",
                SerialNo: serialNo,
                ItemDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    $('#itemResult').show();
                    $('#itemResult').addClass("alert alert-success alert-dismissible fade show");
                    $('#itemResult').html('<button type="button" class="close">×</button><b>Success!</b> Item Successfully Updated</div >');
                    $('#EditItemForm').modal("hide");
                    $('#tableItems').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to update item");
                }
            })
        });
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 5000);
    });

    //Posting an Item's Details when one Updates an Item
    $("#SubmitDeleteOfficeItem").click(function () {
        var itemId = $('#ItemId').val();
        var serialNo = $('#SerialNo').val();
        var itemName = $('#Title').val();
        var description = $('#ItemDescription').val();

        $.ajax({
            type: 'POST',
            url: '/OfficeItem/DeleteItem',
            data: {
                ItemId: itemId,
                Title: itemName,
                ResourceId: "2",
                SerialNo: serialNo,
                ItemDescription: description
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    $('#itemResult').show();
                    $('#itemResult').addClass("alert alert-success alert-dismissible fade show");
                    $('#itemResult').html('<button type="button" class="close">×</button><b>Success!</b> Item Successfully Deleted</div >');
                    $('#EditItemForm').modal("hide");
                    $('#tableItems').DataTable().ajax.reload();
                }
                else {
                    alert("Unable to delete item");
                }
            })
        });
        window.setTimeout(function () {
            $(".alert").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 5000);
    });

    //Borrowing a Book
    $("#SubmitBorrowBook").click(function () {
        var itemId = $('#borrowItemId').val();
        var isbn = $('#bookISBN').val();
        var title = $('#bookTitle').val();
        var author = $('#bookAuthor').val();
        var borrowDate = $('#borrowDate').val();
        var returnDate = $('#returnDate').val();
        $.ajax({
            type: 'POST',
            url: '/OfficeBooks/BorrowBook',
            data: {
                ItemId: itemId,
                ISBN: isbn,
                BookTitle: title,
                Author: author,
                BorrowDate: borrowDate,
                ReturnDate: returnDate
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Book Borrowed");
                    $('#BorrowFormModal').modal("hide");
                }
                else {
                    alert("Unable to Borrow Book")
                }
            })
        });
    });

    //Borrowing an Item
    $("#SubmitBorrowItem").click(function () {
        var itemId = $('#borrowItemId').val();
        var serialNo = $('#itemSerialNo').val();
        var itemName = $('#borrowItemTitle').val();
        var borrowDate = $('#borrowItemDate').val();
        var returnDate = $('#returnItemDate').val();
        $.ajax({
            type: 'POST',
            url: '/OfficeItems/BorrowItem',
            data: {
                ItemId: itemId,
                SerialNo: serialNo,
                Title: itemName,
                BorrowDate: borrowDate,
                ReturnDate: returnDate
            },
            error: function (xhr) {
                alert('Error: ' + xhr.statusText);
            },
            success: (function (result) {
                if (result) {
                    alert("Item Borrowed");
                    $('#BorrowItemModal').modal("hide");
                }
                else {
                    alert("Unable to Borrow Item")
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
            $('#ItemId').val(result.id);
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
            $('#ItemId').val(result.id);
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
            $('#ItemId').val(result.ItemID);
            $('#SerialNo').val(result.SerialNo);
            $('#Title').val(result.Title);
            $('#ItemDescription').val(result.Description);
            $('#EditItemForm').modal("show");
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
            $('#ItemId').val(result.ItemID);
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

function BorrowBook(borrowItemId) {
    $.ajax({
        url: '/OfficeBooks/OfficeBooksGetbyId',
        data: { Id: borrowItemId },
        success: (function (result) {
            console.log(result);
            $('#borrowItemId').val(result.id);
            $('#bookISBN').val(result.ISBN);
            $('#bookTitle').val(result.Title);
            $('#bookAuthor').val(result.Author0);
            $('#BorrowFormModal').modal();
        })
    });
}

function BorrowItem(borrowItemId) {
    $.ajax({
        url: '/OfficeItems/GetItemsById',
        data: { Id: borrowItemId },
        success: (function (result) {
            $('#ItemId').val(result.ItemID);
            $('#SerialNo').val(result.SerialNo);
            $('#Title').val(result.Title);
            $('#ItemDescription').val(result.Description);
            $('#BorrowItemModal').modal();
        })
    });
}

