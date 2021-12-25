function bootnavbar(el, options) {
    if (el == null || el == undefined || el == "") {
        el = "main_navbar"
    }
    let defaultOption  ={

    }

    options = {...defaultOption, ...options }

    this.init = function () {
        var myEl = document.getElementById(el);
        if (myEl == null || myEl == undefined)  return;
        var dropdowns = document.getElementById(el).getElementsByClassName("dropdown");
        console.log(dropdowns);
        for (var i = 0; i < dropdowns.length; i++) {
            var dropdown = dropdowns.item(i);
            dropdown.addEventListener("mouseover", function(){
                this.classList.add('show');
                this.getElementsByClassName("dropdown-menu")[0].classList.add('show');
            });
            dropdown.addEventListener("mouseout", function(){
                this.classList.remove('show');
                this.getElementsByClassName("dropdown-menu")[0].classList.remove('show');
            });
        }
    }

    this.init();    
}
new bootnavbar();