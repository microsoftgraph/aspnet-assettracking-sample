// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#t_items').DataTable({
        "columnDefs": [
            {
                "orderable": false,
                "targets": [-1, -2]
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
    $('#t_books').DataTable({
        "columnDefs": [
            {
                "orderable": false,
                "targets": [0, -1]
            }
        ]
    });
    $('#t_borrowed_books').DataTable({
        "columnDefs": [
            {
                "orderable": false,
                "targets": [0, -1]
            }
        ]
    });
    $('#t_current').DataTable();

    $('#returnDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        minDate: new Date(),
        defaultDate: "+2w"
    });
});
$('#counter').each(function() {
  var $this = $(this),
      countTo = $this.attr('data-count');
  
  $({ countNum: $this.text()}).animate({
    countNum: countTo
  },

  {

    duration: 8000,
    easing:'linear',
    step: function() {
      $this.text(Math.floor(this.countNum));
    },
    complete: function() {
      $this.text(this.countNum);
      //alert('finished');
    }

  });  
  
  

});

//var st = document.getElementById("due_date").innerHTML;
//console.log(st);
//var dt = new Date(st);
//var dt = dt.toLocaleDateString('en-GB');
//console.log(dt);
//var due_date = new Date(dt);
//console.log(due);
//var td = new Date();

//function due(td) {
//    if (due_date < td) {
//        console.log("Book is overdue");
//        document.getElementById("overdue").style.visibility = "visible";
//    }
//    else {
//        document.getElementById("overdue").style.visibility = "hidden";
//    };
//};

//due();

today = new Date();
var dd = today.getDate();
var mm = today.getMonth() + 1; //As January is 0.
var yyyy = today.getFullYear();
var sp = "/";
if (dd < 10) dd = '0' + dd;
if (mm < 10) mm = '0' + mm;
var current_day = dd + sp + mm + sp + yyyy;

document.getElementById("borrowDate").value = current_day;
document.getElementById("borrowDate").disabled = true;

//var itemNo = document.getElementById("item_no").innerHTML;
//document.getElementById("itemNo").value = itemNo;
//document.getElementById("itemNo").disabled = true;

//var item_name = document.getElementById("item_name").innerHTML;
//document.getElementById("itemName").value = item_name;
//document.getElementById("itemName").disabled = true;

var isbn = document.getElementById("isbn").innerHTML;
document.getElementById("bookISBN").value = isbn;
document.getElementById("bookISBN").disabled = true;

var title = document.getElementById("title").innerHTML;
document.getElementById("bookTitle").value = title;
document.getElementById("bookTitle").disabled = true;

var author = document.getElementById("author").innerHTML;
document.getElementById("bookAuthor").value = author;
document.getElementById("bookAuthor").disabled = true;




