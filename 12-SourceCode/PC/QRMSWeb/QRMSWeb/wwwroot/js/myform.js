function setValByElId(elId, value) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            el.val(value);
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
    return;
}

function getValByElId(elId) {
    try {
        return $("#" + elId).val();
    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
    return "";
}


function onKeyPressNumber(evt, hasNegative, hasDecimal) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    if (key.length == 0) return true;
    if (hasNegative && hasDecimal) {
        var regex = /^[-?0-9.,\b]+$/;
        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
            return false;
        }
    } else if (hasNegative && !hasDecimal) {
        var regex = /^[-?0-9.\b]+$/;
        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
            return false;
        }
    } else if (!hasNegative && hasDecimal) {
        var regex = /^[0-9.,\b]+$/;
        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
            return false;
        }
    } else if (!hasNegative && !hasDecimal) {
        var regex = /^[0-9.\b]+$/;
        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
            return false;
        }
    } else {
        var regex = /^[0-9\b]+$/;
        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
            return false;
        }
    }
    return true;
}
function onChangeNumberBox(el, lenInteger, lenDecimal, hasPaddingDecimal) {
    var strNum = el.value;
    var _number = paserNnumber(strNum)
    var strFNum = convertNumberToVNNumberFormat(_number, lenInteger, lenDecimal, hasPaddingDecimal);
    el.value = strFNum;
}
function onBlurNumber(data) {
    console.log(data);
}

function onKeyUpNumberBox(el, lenInteger, lenDecimal, hasPaddingDecimal) {
    var strNum = el.value;
    if (strNum == null || strNum == '' || strNum == undefined || strNum.trim() == '') {
        return "";
    }
    strNum = strNum.trim();
    if (strNum == '-') {
        return;
    }
    

    var lastChar = strNum[strNum.length - 1];
    var _number = paserNnumber(strNum, lenInteger, lenDecimal)
    var strFNum = convertNumberToVNNumberFormat(_number, lenInteger, lenDecimal, hasPaddingDecimal);
    var arrNumbers = strNum.split(',');
   

    if ((lastChar == ',' || lastChar == '.') && !strFNum.includes(",")) {
       strFNum = strFNum + lastChar;
    }
    el.value = strFNum;
}
function onBlurNumber(data) {
    console.log(data);
}

function paserNnumber(strNum, lenInteger, lenDecimal) {
    if (strNum == null || strNum == '' || strNum == undefined || strNum.trim() == '') {
        return undefined;
    }
    strNum = strNum.trim();
    if (lenInteger == undefined || lenInteger < 0 || isNaN(lenInteger)) {
        lenInteger = 18;
    }
    if (lenDecimal == undefined || lenDecimal < 0 || isNaN(lenDecimal)) {
        lenDecimal = 0;
    }
    if (lenDecimal >= 18) {
        lenDecimal = 17;
    }
    var integerLen = (lenInteger + lenDecimal) > 18 ? 18 - lenDecimal : lenInteger; //Max length = 18;

    try {
        var arrNumbers = strNum.split(',');
        var partInt = arrNumbers.length > 0 ? arrNumbers[0] : "0";        
        var partDec = arrNumbers.length > 1 ? arrNumbers[1] : "0";
        if (partInt != null && partInt.length > integerLen) {
            partInt = partInt.substring(0, integerLen);
        }
        if (lenDecimal > 0 && partDec != null && partDec.length > lenDecimal) {
            partDec = partDec.substring(0, lenDecimal)
        }
        partInt = partInt.replaceAll(' ', '').replaceAll('.', '');
        partDec = partDec.replaceAll(' ', '').replaceAll('.', '');
        var _number = parseFloat(partInt + "." + partDec );

        return _number;
    } catch (err) {
        console.log(err.message);
    }
    return undefined;
}
function convertNumberToVNNumberFormat(_number, lenInteger, lenDecimal, hasPaddingDecimal) {
    if (_number == undefined || _number == null || isNaN(_number)) {
        return "";
    }
    try {
        var strFormatNum = _number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        var newstrFormatNum = strFormatNum.replaceAll(".", "_");
        newstrFormatNum = newstrFormatNum.replaceAll(",", ".");
        newstrFormatNum = newstrFormatNum.replaceAll("_", ",");
        var arrPartNumber = newstrFormatNum.split(",");
        var strRet = arrPartNumber[0];
        if (lenInteger == undefined || lenInteger < 0 || isNaN(lenInteger)) {
            lenInteger = 18;
        }
        if (lenDecimal == undefined || lenDecimal < 0 || isNaN(lenDecimal)) {
            lenDecimal = 0;
        }
        if (lenDecimal >= 18) {
            lenDecimal = 17;
        }
        var intPartLen = (lenInteger + lenDecimal) > 18 ? 18 - lenDecimal : lenInteger; //Max length = 18;
        if (strRet != undefined && strRet != '' && strRet.length > intPartLen) {
            strRet = strRet.substring(0, intPartLen);
        }
        if (lenDecimal > 0) {
            var partDec = "";
            if (arrPartNumber.length > 1) {
                partDec = arrPartNumber[1].replaceAll(".", "");
            }
            var _padingDec = (hasPaddingDecimal == true) ? lenDecimal - partDec.length : 0;
            if (_padingDec < 0) {
                partDec = partDec.substring(0, lenDecimal);
            } else if (_padingDec > 0) {
                partDec = partDec.padEnd(_padingDec, '0');
            }
            partDec = partDec.trim();
            if (partDec != "") {
                strRet = strRet + "," + partDec;
            } 
        }
        return strRet;
    } catch (err) {

    }
    return "";
}

function getCheckBoxValue(checkBoxName) {
    var retCheckeds = "";
    $('input[name="' + checkBoxName + '"]:checked').each(function () {
        retCheckeds += ";_#-" + this.value; 
    });
    return retCheckeds;
}

function shortOptionSelectbox() {
    var elements = document.querySelectorAll('option')
    const tail = '...';
    if (elements && elements.length) {
        for (const element of elements) {
            let text = element.innerText;
            if (element.hasAttribute('data-limit')) {
                if (text.length > element.dataset.limit) {
                    element.innerText = `${text.substring(0, element.dataset.limit - tail.length).trim()}${tail}`;
                }
            } else {
              
            }
        }
    }
}

function trimAllElementOfForm() {
    $(":input").children().each(function () {
        console.log("1 - "+ this);
        this.value = $(this).val().trim();
        //$("#asset").val($.trim($("#asset").val()));
        console.log("2 - " + $(this).val());
    })
}

function disableAutofill() {
    try {
        $('input[autocomplete="off"]').disableAutofill();
        $('input[autofill="off"]').disableAutofill();
    } catch (e) {
        console.log(e);
    }
}

function setRadioChecked(rdId) {
    try {
        radiobtn = document.getElementById(rdId);
        if (radiobtn != undefined) {
            radiobtn.checked = true;
        }
    } catch (e) {
        console.log(e);
    }
}

function initMultiSelection(option) {
    try {

        var el = $('.multi-selection-option');
        if (el.length > 0) {
            $('.multi-selection-option').multiselect({
                includeSelectAllOption: true
            });
            
        }
    } catch (err) {
        console.log("initMultiSelection: " + err.message)
    }
}

function setMultiSelectByElId(elId, value) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            el.val(value);
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
    return;
}

function getMultiSelectByElId(elId) {
    try {
        return $("#" + elId).val();
    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
    return "";
}
var comboTrees = new Array(100);
function InitComboBoxTree(elId, jsonData, idx) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            var comboTree1 = el.comboTree({
                source: jsonData,
                isMultiple: true,
                cascadeSelect: true,
                collapse: false
            });
            comboTrees[idx] = comboTree1;
        }
    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
}

function setSourceComboBoxTree(elId, jsonData) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            el.setSource(jsonData);
        }
    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
}
function getComboBoxTreeIDs(elId, idx) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null && el.length > 0) {
            var selectedIds = comboTrees[idx].getSelectedIds();
            if (selectedIds) {
                return JSON.stringify(selectedIds); //; selectedIds?.toString()//
            } else {
                return "";
            }
            
        } else {
            return []
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
}
function getComboBoxTreeTitles(elId) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null ) {
            return el.getSelectedNames();
        } else {
            return []
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
}
function setSelectComboxTree(elId, selectedIds, idx) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            //return el.setSelection(selectedIds);
            //return comboTrees[idx].setSelection(selectedIds)

            comboTrees[idx].setSelection(selectedIds)
            return 1

        } else {
            return []
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
}
function clearSelectComboxTree(elId, idx) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            comboTrees[idx].clearSelection()
            return 1

        } else {
            return []
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
}
function destroyComboBoxTreeTitles(elId) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            el.destroy();
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
}
