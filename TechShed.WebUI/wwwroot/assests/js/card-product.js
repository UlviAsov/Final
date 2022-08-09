var incrementButton = document.getElementsByClassName('inc');
var decrementButton = document.getElementsByClassName('dec');


for(var i = 0;i < incrementButton.length; i++){
    var button = incrementButton[i];
    button.addEventListener('click', function(e){
        //var buttonClicked = e.target;
        //var input = buttonClicked.parentElement.children[2];

        var inputValue = input.value;
         var newValue = parseInt(inputValue) + 1
         inputValue = newValue
    })
}