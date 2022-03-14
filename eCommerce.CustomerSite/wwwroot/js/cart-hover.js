
$(document).ready(function () {
    loadData();
});

function loadData() {
    const culture = $('#hidCulture').val();
    $.ajax({
        type: "GET",
        url: '/Cart/GetListItems',
        success: function (res) {
            var html = '';
            var total = 0;

            $.each(res, function (i, item) {
                //var amount = item.price * item.quantity;
                //<ul class="cart_list">
                //    <li>
                //        <a href="#"><img src="~/assets/images/cart_thamb1.jpg" alt="cart_thumb1">a</a>
                //           <span class="cart_amount"> <span class="price_symbole">$</span>5</span>
                //                </li>
                                   
                //            </ul>

                html += "<ul class=\"cart_list\">"
                    + "<li>"
                    + "<a href=\"#\"><img src=\"~/assets/images/cart_thamb1.jpg\" alt=\"cart_thumb1\">a</a>"
                    + "<span class=\"cart_amount\"> <span class=\"price_symbole\">$</span>5</span>"
                    + "</li>"
                    + " </ul>"
            });
            $('#cart_hover').html(html);
        }
    });
}
