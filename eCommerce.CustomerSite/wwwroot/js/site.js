// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('body').on('click', '.btn-add-cart', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    /*alert("day la " + id);*/
    $.ajax({
        type: "POST",
        url: '/Cart/AddToCart',
        data: {
            id: id
        },
        success: function (res) {
            console.log(res);
        },
        error: function (error) {
            console.log(error);
        }
    })
})