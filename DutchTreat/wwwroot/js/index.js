$(document).ready(function () {

    const theForm = $("#theForm");
    theForm.hide();

    const button = $("#buyButton");
    button.on("click", () => console.log("Buying item"));

    const productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You clicked on " + $(this).text());
    })

    const $loginToggle = $("#loginToggle");
    const $popupForm = $(".popup-form");

    $loginToggle.on("click", () => {
        $popupForm.fadeToggle(1000);
    })
})