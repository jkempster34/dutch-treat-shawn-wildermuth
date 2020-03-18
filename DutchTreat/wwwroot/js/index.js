

const theForm = document.getElementById("theForm");
theForm.hidden = true;

const button = document.getElementById("buyButton");
button.addEventListener("click", () => console.log("Buying item"));

const productInfo = document.getElementsByClassName("product-props");
const listItems = productInfo.item[0].children;