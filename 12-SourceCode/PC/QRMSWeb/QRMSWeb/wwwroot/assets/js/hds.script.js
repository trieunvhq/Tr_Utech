
// yêu cầu phải có Jquery
/*Sau khi Load ajax*/
//$(document).ajaxComplete(function () {
//    deExceedTooltip('[data-toggle="tooltip"]')
//    $('[data-toggle="tooltip"]').tooltip();
//});

/*lấy dữ liệu đã nhập dưới dạng parameter kiểu GET bao gồm các trường đã bị tắt*/
$.fn.serializeIncludeDisabled = function () {
    let disabled = this.find(":input:disabled").removeAttr("disabled");
    let serialized = this.serialize();
    disabled.attr("disabled", "disabled");
    return serialized;
};

/*lấy dữ liệu đã nhập dưới dạng JSON bao gồm các trường đã bị tắt*/
$.fn.serializeIncludeDisabledJSON = function () {
    let disabled = this.find(":input:disabled").removeAttr("disabled");
    let serialized = this.serializeArray();
    var formJSON = {};
    serialized.forEach(function (value) {
        formJSON[value.name] = value.value;
    })
    $(this).find('input[type=checkbox]').each(function () { formJSON[this.name] = this.checked; });
    disabled.attr("disabled", "disabled");
    return formJSON;
};

/*lấy dữ liệu từ các attributes của một DOM element*/
$.fn.attr2JSON = function () {
    var data = {};
    var attrs = this[0].attributes;
    $.each(this[0].attributes, function (i,attr) {
        if (attr.specified) {
            data[attr.name] = attr.value;
        }
    });
    return data;
};

/*lấy dữ liệu từ các attributes của một DOM element*/
$.fn.HtmlKeepVal = function (htmlSTR) {
    var thisVal = this.val();
    this.html(htmlSTR);
    this.val(thisVal);
};

/*chỉ cho 1 checkbox có cùng selector được chọn trong 1 lúc*/
function onlyOne(checkbox, selector) {
    var checkboxes = $(selector);
    var i;
    for (i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i] !== checkbox) checkboxes[i].checked = !checkbox.checked;
    }
}

/* Dùng cho việc kiểm tra số lượng nhập trong bảng cho phép sửa */
function UpdateQuantityToBtn(cell, quantity, btnSelector) {
    var btnData = $(cell).parents('tr').find(btnSelector);
    btnData.attr('data-quantity', quantity);
}
$.fn.numericInput = function (options) {
    'use strict';
    var element = $(this),
        footer = element.find('tfoot tr'),
        dataRows = element.find('tbody tr'),
        initialTotal = function () {
            var column, total;
            for (column = 1; column < footer.children().length; column++) {
                total = 0;
                dataRows.each(function () {
                    var row = $(this);
                    total += parseFloat(row.children().eq(column).text());
                });
                footer.children().eq(column).text(total);
            };
        };
    element.find('td').unbind("change").on('change', function (evt) {
        var cell = $(this),
            column = cell.index(),
            total = 0;
        try { UpdateQuantityToBtn(this, cell[0].innerText, options.btnSelector); } catch (e) { console.log(e) }
        if (cell[0].innerText > options.maxValue) {
            //$('.alert').show("không nhập vượt quá" + options.maxValue);
            window.alert(options.alert + options.maxValue);
            return false; // changes can be rejected
        } else {
            $('.alert').hide();
        }
    }).on('validate', function (evt, value) {
        var cell = $(this),
            column = cell.index();
        if (column === 0) {
            return !!value && value.trim().length > 0;
        } else {
            return !isNaN(parseFloat(value)) && isFinite(value);
        }
    });
    return this;
};

/*Dùng cho việc lấy tham số từ url*/
function getJSONfromURL(url) {
    if (url) {
        var _isQuery = url.lastIndexOf('?');
        if (_isQuery > 0) {
            var _url = url.substring(_isQuery + 1);
            var searchParams = new URLSearchParams(_url);
            var objectJSON = {};
            for (var key of searchParams.keys()) {
                var _value = searchParams.getAll(key);
                objectJSON[key] = _value[0];
            }
            //var objectJSON = JSON.parse('{"' + decodeURIComponent(_url).replace(/"/g, '\\"').replace(/&/g, '","').replace(/=/g, '":"') + '"}');
            return objectJSON;
        } else {
            return { "id": "0" };
        }
    } else {
        return { "id": "0" };
    }
}

/*Hủy tooltip element*/
function deExceedTooltip(selector) {
    $(selector).mouseleave(function () {
        $(this).tooltip('hide');
        $('.tooltip').remove();
        $('.tooltip-inner').remove();
    });
}

function SearchData(ajaxUrl,tableSelector,afterAjax) {
    var url = window.location.href;
    var objectJSON = getJSONfromURL(url);
    var promiseSearch = $.ajax({
        type: "GET",
        url: ajaxUrl,
        data: objectJSON
    });
    promiseSearch.done(function (data) {
        $(tableSelector).html(data);
        afterAjax();
    });
}

