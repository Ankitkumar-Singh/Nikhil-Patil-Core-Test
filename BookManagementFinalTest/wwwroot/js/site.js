$(document).ready(function () {
    $('#bookForm').submit(function () {
        if ($(this).valid())
            toastr.success("Book added successfully");
        else
            toastr.warning("Please enter required values", "Form invalid");
    })

    $('#authorForm').submit(function () {
        if ($(this).valid())
            toastr.success("Author added successfully");
        else
            toastr.warning("Please enter required values", "Form invalid");
    })

    $('#btnDeleteBook').click(function () {
        toastr.success("Book has been deleted","Deleted successfully");
    })

    $('#btnDeleteAuthor').click(function () {
        toastr.success("Author has been deleted", "Deleted successfully");
    })
});