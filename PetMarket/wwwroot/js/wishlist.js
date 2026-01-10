function toggleFavorite(productId, button) {
   
    $(button).prop('disabled', true);

    $.post('/Favorites/Toggle', { productId: productId }, function (data) {
        if (data.success) {
            const icon = $(button).find('i');

            if (data.added) {
               
                icon.removeClass('bi-heart text-secondary').addClass('bi-heart-fill text-danger');
                
                console.log("Produs adăugat la favorite!");
            } else {
                
                icon.removeClass('bi-heart-fill text-danger').addClass('bi-heart text-secondary');

                const productCard = $(button).closest('[id^="favorite-item-"]');
                if (productCard.length > 0) {
                    productCard.fadeOut(400, function () {
                        $(this).remove();
                        
                        if ($('.row .col-md-3').length === 0) {
                            $('.container.my-5').append('<p class="text-center">Nu ai niciun produs la favorite.</p>');
                        }
                    });
                }
            }
        }
    }).fail(function (xhr) {
        if (xhr.status === 401) {
            alert("Trebuie să fiți autentificat!"); 
        } else {
            console.error("Eroare la procesarea wishlist-ului.");
        }
    }).always(function () {
        
        $(button).prop('disabled', false);
    });
}