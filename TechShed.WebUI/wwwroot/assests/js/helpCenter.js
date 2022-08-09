var x = document.getElementById("settingFaq-content")
var y = document.getElementById("generalFaq-content")

function generalFaqContent(){
    if(x.style.display=="block"){
        x.style.display="none"
        y.style.display="block"
    }
    else{
       x.style.display="block"
    }
}

function settingFaqContent(){
    if(y.style.display=="block"){
        y.style.display="none"
        x.style.display="block"
    }
    else{
       y.style.display="block"
    }
}