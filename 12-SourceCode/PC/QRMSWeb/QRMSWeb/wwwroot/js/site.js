// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showPrompt(title, text, confirmText, cancelText, messageTitle, messageText, messageType) {
    return new Promise(resolve => {
        swal({
            title,
            text,
            type: 'warning',
            showCancelButton: true,
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger m-l-10',
            confirmButtonText: confirmText,
            cancelButtonText: cancelText
        }).then(function (result) {
            if (result) {
                /*
                swal(
                    messageTitle,
                    messageText,
                    messageType
                )*/
                //showMessage(messageTitle, messageText, messageType)
            }

            return resolve(result);
        }).catch(() => resolve(false));
    })
}

function initRichTextEditor(selector, options) {
    return new Promise(resolve => {
        $(document).ready(function () {
            setTimeout(() => {
                if ($(selector).length > 0) {
                    tinymce.init({
                        selector: `textarea${selector}`,
                        theme: "modern",
                        encoding: 'html' ,
                        height: 300,
                        font_formats: "Arial=arial,helvetica,sans-serif;Helvetica Neue=helvetica neue;Mulish=mulish,sans-serif;",
                        content_style: "@import url('https://fonts.googleapis.com/css2?family=Mulish:wght@600&display=swap'); body { font-family: mulish,sans-serif !important;font-size:14pt !important }",
                        plugins: [
                            "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker",
                            "searchreplace wordcount visualblocks visualchars code insertdatetime media nonbreaking",
                            "save table contextmenu directionality emoticons template paste textcolor"
                        ],
                        toolbar: "insertfile undo redo | fontselect | styleselect | fontsizeselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | l      ink image | print preview media fullpage | forecolor backcolor emoticons",
                        style_formats: [
                            {title: 'Bold text', inline: 'b'},
                            {title: 'Red text', inline: 'span', styles: {color: '#ff0000'}},
                            {title: 'Red header', block: 'h1', styles: {color: '#ff0000'}},
                            {title: 'Example 1', inline: 'span', classes: 'example1'},
                            {title: 'Example 2', inline: 'span', classes: 'example2'},
                            {title: 'Table styles'},
                            {title: 'Table row 1', selector: 'tr', classes: 'tablerow1'}
                        ],
                        image_title: true,
                        automatic_uploads: true,
                        file_picker_types: 'image',
                        file_picker_callback: function (cb, value, meta) {
                            var input = document.createElement('input');
                            input.setAttribute('type', 'file');
                            input.setAttribute('accept', 'image/*');

                            /*
                              Note: In modern browsers input[type="file"] is functional without
                              even adding it to the DOM, but that might not be the case in some older
                              or quirky browsers like IE, so you might want to add it to the DOM
                              just in case, and visually hide it. And do not forget do remove it
                              once you do not need it anymore.
                            */

                            input.onchange = function () {
                                var file = this.files[0];

                                var reader = new FileReader();
                                reader.onload = function () {
                                    /*
                                      Note: Now we need to register the blob in TinyMCEs image blob
                                      registry. In the next release this part hopefully won't be
                                      necessary, as we are looking to handle it internally.
                                    */
                                    var id = 'blobid' + (new Date()).getTime();
                                    var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                                    var base64 = reader.result.split(',')[1];
                                    var blobInfo = blobCache.create(id, file, base64);
                                    blobCache.add(blobInfo);

                                    /* call the callback and populate the Title field with the file name */
                                    cb(blobInfo.blobUri(), { title: file.name });
                                };
                                reader.readAsDataURL(file);
                            };

                            input.click();
                        },
                        ...options
                    });
                }

                resolve();
            }, 500)
        });
    })
}

function setContent(selector, content) {
    return new Promise((resolve, reject) => {
        $(document).ready(function () {
           try {
               tinymce.get(selector).setContent(content);
               resolve();
           } catch (e) {
               reject(e);
           }
        })
    })
}

function getContent(selector) {
    return new Promise((resolve, reject) => {
        $(document).ready(function () {
            try {
                //console.log(tinymce.get(selector).getContent())
                resolve(tinymce.get(selector).getContent());
            } catch (e) {
                reject(e);
            }
        })
    })
}

function showMessage(title, message, type = 'info') {
    if ((message == undefined || message == '') && (title == '' || title == undefined)) {
        new Promise((resolve) => {
            resolve();
        })
    }
    return new Promise((resolve) => {
        toastr[type](message, title,{
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "3000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut",
        });

        resolve();
    })
}

function navigateToPage(page) {
    const delayed = (time) => new Promise(resolve => setTimeout(resolve, time));
    return new Promise(async resolve => {
        window.location.reload();
        window.location.href = page;
        await delayed(10000);
        resolve();
    })
}

function ShowModal(modalID) {
    $("#" + modalID).modal('show');
    // $("#" + modalID).modal('toggle');
}

function closeModal(modalID) {
    try {
        
       // $("#" + modalID).hide();
        $("#" + modalID).modal("hide");
        $('body').removeClass('modal-open');
        //$('.modal-backdrop').remove();
    } catch (error) {
        console.log(error);
    }

}

async function handleException(exceptionCode, msgDefault, redirectToPage, isBackHistoryPage) {
    if (exceptionCode == undefined || exceptionCode == null || exceptionCode == '' || exceptionCode.trim() == '') {
        return;
    }
    exceptionCode_temp = exceptionCode.trim().toLowerCase();
    var message = (msgDefault == undefined || msgDefault == null || msgDefault == '' || msgDefault.trim() == '') ? "Xin lỗi. Hệ thống có lỗi xảy ra." : msgDefault;
    switch (exceptionCode_temp) {
        case "Unauthorized".toLowerCase():
            message = "Chưa đăng nhập hoặc hêt thời hạn hoạt động";
            redirectToPage = "/auth/login"
            break;
        case "AccessDenied".toLowerCase():
            message = "Bạn không có quyền truy cập chức năng này.";
            break;            
        case "Forbidden".toLowerCase():
            message = "Hêt thời hạn hoạt động hoặc chưa đăng nhập";
            break;
        case "GatewayTimeout".toLowerCase():
        case "RequestTimeout".toLowerCase():
            message = "Yêu cầu bị hết thời hạn";
            break;
        case "NotFound".toLowerCase():
        default:
            if (1 == 1) { //debug
                message = exceptionCode;
            }
            break;
    }
    const delayed = (time) => new Promise(resolve => setTimeout(resolve, time));
    return new Promise(async resolve => {
        await showMessage('Thông báo', message, 'error');
        await delayed(3000);
        if (isBackHistoryPage == true) {
            window.history.back();
            await delayed(3000)
        } else {
            if (!(redirectToPage == undefined || redirectToPage == null || redirectToPage == '' || redirectToPage.trim() == '')) {
                window.location.replace(redirectToPage.trim());
                await delayed(3000);
            }
        }
        return resolve(true);
    });

}

function FirstLetterUpcase(input) {
    if (input == undefined || input == "" || input.trim() == "") return "";

    return input.charAt(0).toUpperCase() + (input.length > 1 ? input.slice(1) : "");
}