/*
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.7.1/js/bootstrap-datepicker.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    */

function vnDateTimePicker() {
    try {
        console.log('vnDateTimePicker')
        var el = $('.pjc-date-time-picker');
        if (el.length > 0) {
            $('.pjc-date-time-picker').datetimepicker({
                format: 'HH:mm DD/MM/YYYY',
                locale: 'vi',
                widgetPositioning: {
                    vertical: "auto"
                },
                daysOfWeekDisabled: [],
            }
            );
            // $('.pjc-date-picker').datepicker("setDate", new Date());
        }
    } catch (err) {
        console.log("vnDateTimePicker: " + err.message)
    }
}
function vnDatePicker() {
    try {        
        var el = $('.pjc-date-picker');
        if (el.length > 0) {
            $('.pjc-date-picker').datetimepicker({
                format: 'DD/MM/YYYY',
                locale: 'vi',
                widgetPositioning: {
                    vertical: "auto"
                },
                daysOfWeekDisabled: [],
                
            }
            );
        }
               

    } catch (err) {
        console.log("vnDateTimePicker: " + err.message)
    }
}

function vnDatePicker2() {
    try {
        var el = $('.pjc-date-picker');
        if (el.length > 0) {
            $('.pjc-date-picker').datepicker({
                language: "vi",
                todayHighlight: true,
                todayBtn: true,
                weekStart: 1,
                orientation: "bottom auto",
                daysOfWeekHighlighted: "6,0",
                autoclose: true,
            }
            );
            // $('.pjc-date-picker').datepicker("setDate", new Date());
        }
    } catch (err) {
        console.log("vnDatePicker: " + err.message)
    }
}
function vnYearPicker() {
    try {
        var el = $('.pjc-year-picker');
        if (el.length > 0) {
            $('.pjc-year-picker').datepicker({
                language: "vi",                
                orientation: "bottom auto",
                daysOfWeekHighlighted: "6,0",
                autoclose: true,
                format: "yyyy",
                viewMode: "years",
                minViewMode: "years"
            }
            );
            // $('.pjc-date-picker').datepicker("setDate", new Date());
        }
    } catch (err) {
        console.log("vnDatePicker: " + err.message)
    }
}

function setValDateTimePickerByElId(elId, value) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            el.val(value);
            el.datetimepicker.date = value;
            el.datetimepicker('clear');
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
    return;
    
}
function clearDateTimePickerByElId(elId) {
    try {
        var el = $("#" + elId);
        if (el != undefined && el != null) {
            el.val(null);
            el.datetimepicker.date = null;
            el.datetimepicker('clear');
        }

    } catch (err) {
        console.log("Có lỗi xảy ra errr: " + err.message)
    }
    return;

}
