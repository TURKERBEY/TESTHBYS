/*=========================================================================================
    File Name: form-repeater.js
    Description: form repeater page specific js
    ----------------------------------------------------------------------------------------
    Item Name: Vuexy HTML Admin Template
    Version: 1.0
    Author: PIXINVENT
    Author URL: http://www.themeforest.net/user/pixinvent
==========================================================================================*/

$(function () {
  'use strict';

  // form repeater jquery
  $('.invoice-repeater, .repeater-default').repeater({
    show: function () {
      $(this).slideDown();
      // Feather Icons
      if (feather) {
        feather.replace({ width: 14, height: 14 });
      }
    },
    hide: function (deleteElement) {
      //if (confirm('Silmek istediğinize eminmisiniz?')) {
        //$(this).slideUp(deleteElement);
        //}
        var row = this;

        Swal.fire({
            title: 'Emin misin?',
            text: "Kayıt silinecek!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Sil!',
            cancelButtonText: 'Vazgeç',
            customClass: {
                confirmButton: 'btn btn-primary',
                cancelButton: 'btn btn-outline-danger ms-1'
            },
            buttonsStyling: false
        }).then(function (result) {
            if (result.value) {
                $(row).slideUp(deleteElement);
                
            }
        });

      }
  });
});
