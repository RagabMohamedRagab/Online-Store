
 /*Processors*/
$(document).ready(function () {
    $("body").on("change", "#ProcessorPrice", function () {
        var $Price = $(this).val(), $html = '', $pagin ='';

        $.ajax({
            type: "GET",
            url: "/Home/AscendingProcessorProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#Pro").empty();
                $("#Processor").empty();
                for (var e of response.data) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img style="max-width: 260px;max-height: 260px" src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i id="' + e.proCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.proCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.proQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.proCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }


                $pagin += '<ul class="store-pagination" id="pagin">'
                $pagin += '<li  onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#Pro").html($html);
                $("#Processor").html($pagin);
            }
        });


    });
    $("body").on("change", "#ProcessorProduct", function () {
        var $Price = $(this).val(), $html = '', $pagin = '';
        $.ajax({
            url: '/Home/DefaultProcessor?PageSize=' + $Price,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log(response);
                $("#Pro").empty();
                $("#Processor").empty();
                for (var e of response.data) {
                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +
                        '<img style="max-width: 260px;max-height: 260px" src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="#">' + e.proName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i id="' + e.proCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.proCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.proQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.proCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }


                $pagin += '<ul class="store-pagination" id="pagin">'
                $pagin += '<li  onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#Pro").html($html);
                $("#Processor").html($pagin);
            }
        });
    })
   var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear", function () {
        var $val = $(this).val().trim();
        var $pagin = '', $html = '';
        
       
        if (this.checked) {
           
            arr.push($val)

        } else {
            for (var i = 0; i < arr.length; i++) {

                if (arr[i] === $val) {
                    
                    arr.splice(i, 1);

                }
            }
        }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfProcessorBrand",
            dataType: 'json',
            data: { brand: arr },

            success: function (response) {
                console.log(response);
                $("#Pro").empty();
                $("#Processor").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#Pro").html($html);
                } else {
                    for (var e of response.data) {
                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +
                            '<img style="max-width: 260px;max-height: 260px" src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="#">' + e.proName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.proPrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.proPrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.proRate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.proRate, 1); i <= 5; i++) {
                            if (Math.round(e.proRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.proRate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.proRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i id="' + e.proCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.proCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.proCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.proCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.proQuantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.proCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }
                    $pagin += '<ul class="store-pagination" id="pagin">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>'
                    $("#Pro").html($html);
                    $("#Processor").html($pagin);

                }
                

            }


        });


    })
});

/*Motherboard*/

$(document).ready(function () {
    $("body").on("change","#MotherPrice", function () {
        var $Price = $(this).val(),
            $html = "", $pagin ='';
        $.ajax({
            url: "/Home/AscendingMotherboardProdoucts?Id=" + $Price,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log(response);
                $("#moth").empty();
                $("#mother").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.motherCode + "'" + ')"class="add-to-wishlist"><i id="' + e.motherCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" +e.motherCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.motherCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.motherCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.motherCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginM">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#moth").html($html);
                $("#mother").html($pagin);

            }

        });


    });
    $("body").on("change","#MotherProduct", function () {
        var $Data = $(this).val(),
            $html = '', $pagin = '';
        $.ajax({
            url: "/Home/DefaultMotherboard?PageSize=" + $Data,
            type: "GET",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                console.log(response);
                $("#moth").empty();
                $("#mother").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="#">' + e.motherName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                        if (Math.round(e.motherRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.motherCode + "'" + ')"class="add-to-wishlist"><i id="' + e.motherCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.motherCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.motherCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.motherCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.motherCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginM">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#moth").html($html);
                $("#mother").html($pagin);

            }

        });
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear1", function () {
        var $val = $(this).val().trim();
        var $pagin = '', $html = '';
        
 
        if (this.checked) {
           
            arr.push($val)

        } else {
            for (var i = 0; i < arr.length; i++) {
                
                if (arr[i] === $val) {
                    
                    arr.splice(i, 1);

                }
            }
        }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfMotherboardBrand",
            data: { brand: arr },
            dataType: 'json',
            success: function (response) {
                console.log(response);
                $("#moth").empty();
                $("#mother").empty();
                console.log(response);
                if (response =="No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#moth").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img style="max-width: 260px;max-height: 260px" src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="#">' + e.motherName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.motherPrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.motherPrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.motherRate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.motherRate, 1); i <= 5; i++) {
                            if (Math.round(e.motherRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.motherRate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.motherRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.motherCode + "'" + ')"class="add-to-wishlist"><i id="' + e.motherCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.motherCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.motherCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.motherCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.motherQuantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.motherCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }
                    $pagin += '<ul class="store-pagination" id="paginM">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>'
                    $("#moth").html($html);
                    $("#mother").html($pagin);
                }
            }

        });


    })
});

 /*HDD*/
$(document).ready(function () {
    $("body").on("change","#HDDPrice", function () {
        var $Price = $(this).val(),
             $html = '', $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingHDDProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#hdd").empty();
                $("#H").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.hddrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.hddrate, 1); i <= 5; i++) {
                        if (Math.round(e.hddrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddrate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i id="' + e.hddcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.hddcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.hddcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#hdd").html($html);
                $("#H").html($pagin);

            }
        });


    });
    $("body").on("change","#HDDProduct", function () {
        var $Price = $(this).val(), $html = '', $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultHDD?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#hdd").empty();
                $("#H").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="#">' + e.hddname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.hddrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.hddrate, 1); i <= 5; i++) {
                        if (Math.round(e.hddrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddrate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i id="' + e.hddcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.hddcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.hddcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.motherQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginH">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#hdd").html($html);
                $("#H").html($pagin);

            }


        })
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear2", function () {
        var $val = $(this).val().trim(), $html = '', $pagin = '';
        if (this.checked) {
            arr.push($val)
        } else {
            for (var i = 0; i < arr.length; i++) {

                if (arr[i] === $val) {

                    arr.splice(i, 1);
                }

            }
        }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfHDDBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#hdd").empty();
                $("#H").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#hdd").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="#">' + e.hddname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.hddprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.hddprice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.hddrate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.hddrate, 1); i <= 5; i++) {
                            if (Math.round(e.hddrate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.hddrate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.hddrate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i id="' + e.hddcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.hddcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.hddcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.hddcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.motherQuantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.mothCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }

                    $pagin += '<ul class="store-pagination" id="paginH">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>'
                    $("#H").html($pagin);

                    $("#hdd").html($html);
                }

            }
        });


    });
});

/*RAM*/
$(document).ready(function () {
    $("body").on("change", "#RAMPrice", function () {
        var $Price = $(this).val(),
            $html = "",$pagin='';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingRAMProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#ram").empty();
                $("#R").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ramRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ramRate, 1); i <= 5; i++) {
                        if (Math.round(e.ramRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ramRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ramRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i id="' + e.ramCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ramCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ramCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ramQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ramCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginR">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#ram").html($html);
                $("#R").html($pagin);

            }
        });


    });
    $("body").on("change","#RAMProduct",function () {
        var $Price = $(this).val(),
            $html = '', $pagin ='';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultRAM?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#ram").empty();
                $("#R").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4">' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ramName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ramRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ramRate, 1); i <= 5; i++) {
                        if (Math.round(e.ramRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ramRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ramRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i id="' + e.ramCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ramCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ramCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ramQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ramCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginR">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#ram").html($html);
                $("#R").html($pagin);

            }


        })
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear3", function () {
        var $val = $(this).val().trim(),$html = '', $pagin = '';
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfRAMBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#ram").empty();
                $("#R").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#ram").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ramName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.ramPrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.ramPrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.ramRate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.ramRate, 1); i <= 5; i++) {
                            if (Math.round(e.ramRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ramRate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ramRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i id="' + e.ramCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ramCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ramCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.ramCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.ramQuantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.ramCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }
                    $pagin += '<ul class="store-pagination" id="paginR">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>'
                    $("#ram").html($html);
                    $("#R").html($pagin);
                }
            }
        });


    })
});

 /*SSD */
$(document).ready(function () {
    $("body").on("change", "#SSDPrice" ,function () {
        var $Price = $(this).val(),
            $html = "",$pagin='';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingSSDProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#SSD").empty();
                $("#S").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#SSD").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ssdname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.ssdrate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.ssdrate, 1); i <= 5; i++) {
                            if (Math.round(e.ssdrate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ssdrate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ssdrate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i id="' + e.ssdcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ssdcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.ssdcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.ssdquantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.ssdcode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }
                    $pagin += '<ul class="store-pagination" id="paginS" style="margin-top: 3.5rem;">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>'
                    $("#SSD").html($html);
                    $("#S").html($pagin);
                }
            }
        });


    });
    $("body").on("change", "#SSDProduct", function () {
        var $Price = $(this).val(),
            $html = '', $pagin ='';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultSSD?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#SSD").empty();
                $("#S").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ssdname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.ssdrate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.ssdrate, 1); i <= 5; i++) {
                        if (Math.round(e.ssdrate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ssdrate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ssdrate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i id="' + e.ssdcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ssdcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.ssdcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.ssdquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.ssdcode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginS" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#SSD").html($html);
                $("#S").html($pagin);
            }


        })
    })
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear4", function () {
   
        var $val = $(this).val().trim(), $html ='',$pagin='';
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }

        
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfSSDBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#SSD").empty();
                $("#S").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#SSD").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.ssdname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.ssdprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.ssdprice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.ssdrate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.ssdrate, 1); i <= 5; i++) {
                            if (Math.round(e.ssdrate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.ssdrate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.ssdrate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i id="' + e.ssdcode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.ssdcode + "'" + ')"class="add-to-wishlist"><i  id="' + e.ssdcode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.ssdcode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.ssdquantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.ssdcode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }
                    $pagin += '<ul class="store-pagination" id="paginS" style="margin-top: 3.5rem;">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>'
                    $("#SSD").html($html);
                    $("#S").html($pagin);
                }
            }

        });


    });
   

});

/*Graphics Card */
$(document).ready(function () {
    $("body").on("change", "#CardPrice", function () {
        var $Price = $(this).val(),
            $html = "",$pagin='';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingCardProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#card").empty();
                $("#C").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4"  >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.vgarate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.vgarate, 1); i <= 5; i++) {
                        if (Math.round(e.vgarate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.vgarate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.vgarate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i id="' + e.vgacode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i  id="' + e.vgacode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.vgacode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.vgaquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.vgacode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginC" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#card").html($html);
                $("#C").html($pagin);
            }
        });


    });
    $("body").on("change", "#CardProduct", function () {
        var $Price = $(this).val(),
            $html = '', $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultCard?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#card").empty();
                $("#C").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.vganame + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.vgarate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.vgarate, 1); i <= 5; i++) {
                        if (Math.round(e.vgarate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.vgarate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.vgarate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i id="' + e.vgacode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i  id="' + e.vgacode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.vgacode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.vgaquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.vgacode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginC" style="margin-top: 3.5rem;">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#card").html($html);
                $("#C").html($pagin);
            }
        })
    });
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear5", function () {
        var $val = $(this).val().trim(), $html = '', $pagin = '';
        if (this.checked) {
            arr.push($val)
        } else {
            for (var i = 0; i < arr.length; i++) {

                if (arr[i] === $val) {

                    arr.splice(i, 1);
                }

            }
        }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfCardBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#card").empty();
                $("#C").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#card").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="javascript:void(0)"style="font-size: 1rem;">' + e.vganame + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.vgaprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.vgaprice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.vgarate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.vgarate, 1); i <= 5; i++) {
                            if (Math.round(e.vgarate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.vgarate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.vgarate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i id="' + e.vgacode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.vgacode + "'" + ')"class="add-to-wishlist"><i  id="' + e.vgacode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.vgacode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.vgaquantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.vgacode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }
                    $pagin += '<ul class="store-pagination" id="paginC" style="margin-top: 3.5rem;">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>'
                    $("#card").html($html);
                    $("#C").html($pagin);
                }
            }
        });


    });
});

/* Case*/
$(document).ready(function () {
    $("body").on("change","#CasePrice", function () {
        var $Price = $(this).val(),
            $html = "", $pagin ='';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingCaseProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#Case").empty();
                $("#C").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)" style="font-size: 1rem;">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.caseRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.caseRate, 1); i <= 5; i++) {
                        if (Math.round(e.caseRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.caseCode + "'" + ')"class="add-to-wishlist"><i id="' + e.caseCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.caseCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.caseCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.caseQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.caseCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginC">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#Case").html($html);
                $("#C").html($pagin);

            }
        });


    });
    $("body").on("change","#CaseProduct", function () {
        var $Price = $(this).val(),
            $html = '', $pagin ='';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultCase?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#Case").empty();
                $("#C").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name"><a href="javascript:void(0)" style="font-size: 1rem;">' + e.caseName + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.caseRate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.caseRate, 1); i <= 5; i++) {
                        if (Math.round(e.caseRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.caseCode + "'" + ')"class="add-to-wishlist"><i id="' + e.caseCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.caseCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.caseCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.caseQuantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.caseCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginC">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#Case").html($html);
                $("#C").html($pagin);

            }


        })
    });
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear6", function () {
       
        var $val = $(this).val().trim(), $html ='',$pagin='';
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfCaseBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#Case").empty();
                $("#C").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#Case").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="javascript:void(0)" style="font-size: 1rem;">' + e.caseName + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.casePrice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.casePrice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.caseRate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.caseRate, 1); i <= 5; i++) {
                            if (Math.round(e.caseRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.caseRate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.caseRate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.caseCode + "'" + ')"class="add-to-wishlist"><i id="' + e.caseCode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.caseCode + "'" + ')"class="add-to-wishlist"><i  id="' + e.caseCode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.caseCode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.caseQuantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.caseCode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }

                    $pagin += '<ul class="store-pagination" id="paginC">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>';
                    $("#C").html($pagin);


                    $("#Case").html($html);
                }


            }

        });


    });
});

/*PowerSuply*/
$(document).ready(function () {
    $("body").on("change","#PSPrice" ,function () {
        var $Price = $(this).val(),
            $html = "", $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/AscendingPowerSuplyProdoucts?Id=" + $Price,
            success: function (response) {
                console.log(response);
                $("#PWS").empty();
                $("#PS").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="javascript:void(0)">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.psurate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.psurate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.psucode + "'" + ')"class="add-to-wishlist"><i id="' + e.psucode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.psucode + "'" + ')"class="add-to-wishlist"><i  id="' + e.psucode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.psquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.psucode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginPS">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#PWS").html($html);
                $("#PS").html($pagin);

            }
        });
    });
    $("body").on("change", "#PSProduct", function () {
        var $Price = $(this).val(),
            $html = '', $pagin = '';
        $.ajax({
            type: "GET",
            url: "/Home/DefaultPowerSuply?PageSize=" + $Price,
            success: function (response) {
                console.log(response);
                $("#PWS").empty();
                $("#PS").empty();
                for (var e of response.data) {

                    $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                        '<div class="product">' +
                        '<div class="product-img">' +

                        '<img src="/Images/' + e.image[0] + '"/>' +

                        '</div>' +
                        '<div class="product-body">' +
                        '<h3 class="product-name text-truncate"><a href="javascript:void(0)">' + e.psuname + '</a></h3>' +
                        '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                        '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                        //Rate
                        '<div class="product-rating">';
                    for (var i = 1; i < Math.round(e.psurate, 1); i++) {
                        $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                    }
                    for (var i = Math.round(e.psurate, 1); i <= 5; i++) {
                        if (Math.round(e.proRate, 1) != 0) {
                            if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                            }
                            else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
                                $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                            }
                            else {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                        else {
                            if (i < 5) {
                                $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                            }
                        }
                    }
                    $html += '</div><div class="product-btns">';
                    if (e.wishList == true) {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.psucode + "'" + ')"class="add-to-wishlist"><i id="' + e.psucode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                    }
                    else {
                        $html += '<button onclick="AddOrDeleteWL(' + "'" + e.psucode + "'" + ')"class="add-to-wishlist"><i  id="' + e.psucode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                    }
                    $html += '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                    if (e.psquantity == 0) {
                        $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                    }
                    else {
                        $html += '<button onclick="AddToCart({Code:' + "'" + e.psucode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                            '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                    }
                    $html += ' </div></div></div></div>';
                }
                $pagin += '<ul class="store-pagination" id="paginPS">'
                $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                for (var i = 1; i <= response.totalPages; i++) {
                    if (i == response.currentPage) {
                        $pagin += '<li class="toggle add">' + i + '</li>'
                    } else {
                        $pagin += '<li class="add">' + i + '</li>'
                    }
                }
                $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                $pagin += '</ul>'
                $("#PWS").html($html);
                $("#PS").html($pagin);

            }


        })
    });
    var arr = [];
    $("body").on("click", "input[type='checkbox'].Kabear7", function () {
     
        var $val = $(this).val().trim(), $html = '', $pagin = '';
            if (this.checked) {
                arr.push($val)
            } else {
                for (var i = 0; i < arr.length; i++) {

                    if (arr[i] === $val) {

                        arr.splice(i, 1);
                    }

                }
            }
        $.ajax({
            type: "POST",
            url: "/Home/ProductsOfPowerSuplyBrand",
            dataType: "json",
            data: { brand: arr },
            success: function (response) {
                console.log(response);
                $("#PWS").empty();
                $("#PS").empty();
                if (response == "No") {
                    $html = '<img src="/img/noproductfound.png" style="margin-left: 30%;width: 37%;"/>'
                    $("#PWS").html($html);
                } else {
                    for (var e of response.data) {

                        $html += '<div class="col-md-4" style = "margin-bottom:6%" >' +
                            '<div class="product">' +
                            '<div class="product-img">' +

                            '<img src="/Images/' + e.image[0] + '"/>' +

                            '</div>' +
                            '<div class="product-body">' +
                            '<h3 class="product-name text-truncate"><a href="javascript:void(0)">' + e.psuname + '</a></h3>' +
                            '<h4 class="product-price"><span class="price">' + e.psuprice + ' LE</span>' +
                            '<del class="product-old-price" > ' + (e.psuprice + 100) + ' LE</del ></h4 >' +

                            //Rate
                            '<div class="product-rating">';
                        for (var i = 1; i < Math.round(e.psurate, 1); i++) {
                            $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                        }
                        for (var i = Math.round(e.psurate, 1); i <= 5; i++) {
                            if (Math.round(e.proRate, 1) != 0) {
                                if (Math.floor((i - Math.floor(i)) * 10) == 0 && i == Math.round(e.psurate, 1)) {
                                    $html += '<i class="fa fa-star" style="color: #D10024"></i> ';
                                }
                                else if (Math.floor((i - Math.floor(i)) * 10) >= 5 && i == Math.round(e.psurate, 1)) {
                                    $html += '<i class="fa fa-star-half-o" style="color: #D10024"></i> ';
                                }
                                else {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                            else {
                                if (i < 5) {
                                    $html += '<i class="fa fa-star-o" style="color: #D10024"></i> ';
                                }
                            }
                        }
                        $html += '</div><div class="product-btns">';
                        if (e.wishList == true) {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.psucode + "'" + ')"class="add-to-wishlist"><i id="' + e.psucode + '" style="color: #D10024" class="fa fa-heart"></i><span class="tooltipp">Remove from wishlist</span></button>';
                        }
                        else {
                            $html += '<button onclick="AddOrDeleteWL(' + "'" + e.psucode + "'" + ')"class="add-to-wishlist"><i  id="' + e.psucode + '" class="fa fa-heart-o"></i><span class="tooltipp">Add to wishlist</span></button>';
                        }
                        $html += '<button onclick="gotoDetails(' + "'" + e.psucode + "'" + ')" class="quick-view"><i class="fa fa-eye"></i><span class="tooltipp">quick view</span></button>';
                        if (e.psquantity == 0) {
                            $html += ' <button style="background: white; cursor: auto" data-toggle="blog-tags" data-placement="top"><i class="fa fa-shopping-cart" style="color: #cdcdcd;"></i></button>'
                        }
                        else {
                            $html += '<button onclick="AddToCart({Code:' + "'" + e.psucode + "'" + ', Quantity: 1 })" data-toggle="blog-tags" data-placement="top" title="Add TO CART">' +
                                '<i class="fa fa-shopping-cart"></i><span class="tooltipp">add to Cart</span></button>';

                        }
                        $html += ' </div></div></div></div>';
                    }

                    $pagin += '<ul class="store-pagination" id="paginPS">'
                    $pagin += '<li onclick=GetPerPageNumber(' + response.currentPage + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-left"></i></a></li>'
                    for (var i = 1; i <= response.totalPages; i++) {
                        if (i == response.currentPage) {
                            $pagin += '<li class="toggle add">' + i + '</li>'
                        } else {
                            $pagin += '<li class="add">' + i + '</li>'
                        }
                    }
                    $pagin += '<li onclick=GetNextPageNumber(' + response.currentPage + ',' + response.totalPages + ')><a href="javascript:void(0)" class="active"><i class="fa fa-angle-right"></i></a></li>'
                    $pagin += '</ul>';
                    $("#PS").html($pagin);

                    $("#PWS").html($html);
                }



            }
        });
    });
});

//////////////////////////////////////////Shared Functions//////////////////////////////////////////

//Details page
function gotoDetails(ProductCode) {
    if (ProductCode.startsWith("SSD")) {
        window.location = "/Home/SsdDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("R")) {
        window.location = "/Home/RamDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("CAS")) {
        window.location = "/Home/CaseDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("V")) {
        window.location = "/Home/GraphicsCardDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("PS")) {
        window.location = "/Home/PowerSupplyDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("PR")) {
        window.location = "/Home/ProcessorDetails?code=" + ProductCode
    }
    else if (ProductCode.startsWith("MO")) {
        window.location = "/Home/MotherboardDetails?code=" + ProductCode
    } else if (ProductCode.startsWith("HD")) {
        window.location = "/Home/HddDetails?code=" + ProductCode
    }
}

//AddToCart
function AddToCart(Product) {
    var cart = getItemStorage("Cart") || [];
    let counter = 0;
    if (cart.length == 0) {
        setItemStorage("Cart", cart);
        addValueToItemStorage("Cart", Product);
        toastr.success('Done', '', { timeOut: 7000 });
        cartconnection();
    }
    else {
        $.each(cart, function (key, item) {
            if (!(item.Code == Product.Code)) {
                counter++;
                if (counter == cart.length) {
                    setItemStorage("Cart", cart);
                    addValueToItemStorage("Cart", Product);
                    toastr.success('Done', '', { timeOut: 7000 });
                    cartconnection();
                }
            }
            else {
                toastr.error('You Choose This Product Before!!', '', { timeOut: 7000 });
            }
        });
    }
}

//Add Or Delete From WL
function AddOrDeleteWL(Product){
    if ($('#' + Product).hasClass("fa fa-heart")) {
        //لو ملونه هيروح بالاجكس على اكشن للديليت
        $.ajax({
            type: "POST",
            url: "/Home/DeleteFromWL?ProductCode=" + Product,
            dataType: "json",
            success: function (result) {
                if (result == "Deleted Done") {
                    $('#' + Product).removeClass('fa fa-heart').addClass("fa fa-heart-o").css('color', '');
                    toastr.success('Deleted Done.', '', { timeOut: 7000 });
                    wishlistconnection();
                }
                else if (result == "Error") {
                    toastr.error('Something Wrong to delete , Try Again.', '', { timeOut: 7000 });
                }

            }
        });
    }
    else {
        $.ajax({
            //لو مش ملونه
            url: "/Home/AddToWL?ProductCode=" + Product,
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result == 'Saved Done') {
                    $('#' + Product).removeClass('fa fa-heart-o').addClass("fa fa-heart").css('color', '#D10024');
                    toastr.success('Product successfully added to your wishlist', '', { timeOut: 7000 });
                    wishlistconnection();
                }
                else if (result == 'LoginRegisterPopup') {
                    $('#login-register').click();
                }
                else {
                    toastr.error('Something Wrong to add , Try again!!', '', { timeOut: 7000 });
                }

            }
        });
    }
}

//start statement of LocalStorage
function setItemStorage(itemKey, itemValue) {
    localStorage.setItem(itemKey, JSON.stringify(itemValue));
}
function getItemStorage(itemKey) {
    return JSON.parse(localStorage.getItem(itemKey));
}
function addValueToItemStorage(itemKey, val) {
    let item = getItemStorage(itemKey);
    item.push(val);
    setItemStorage(itemKey, item);
}
function removeItemStorage(itemKey, val) {
    let item = getItemStorage(itemKey);
    item.splice(val, 1);
    setItemStorage(itemKey, item);
}
function removeStorage(itemKey) {
    localStorage.removeItem(itemKey);
}
