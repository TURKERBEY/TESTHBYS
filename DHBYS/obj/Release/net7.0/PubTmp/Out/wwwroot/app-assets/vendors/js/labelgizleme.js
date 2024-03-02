// script.js
function showAdditionalLabel() {
    var selectElement = document.getElementById("HastaKayitTip_id");
    var additionalLabel = document.getElementById("additionalLabel");

    if (selectElement.value === "2") {
        additionalLabel.style.display = "block"; 
    } else {
        additionalLabel.style.display = "none";
    }
}