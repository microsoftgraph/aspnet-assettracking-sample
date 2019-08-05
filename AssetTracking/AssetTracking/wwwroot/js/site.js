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

var isbn = document.getElementById("isbn").innerHTML;
document.getElementById("bookISBN").value = isbn;
document.getElementById("bookISBN").disabled = true;

var title = document.getElementById("title").innerHTML;
document.getElementById("bookTitle").value = title;
document.getElementById("bookTitle").disabled = true;

var author = document.getElementById("author").innerHTML;
document.getElementById("bookAuthor").value = author;
document.getElementById("bookAuthor").disabled = true;




