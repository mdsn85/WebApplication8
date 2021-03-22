
function AllowChangeQty() {
    let qtyInput = document.getElementsByName("Qty");
    console.log("qtyInput.length = " + qtyInput.length);
    for (let i = 0; i < qtyInput.length; i++) {
        qtyInput[i].readOnly = false;
        console.log("qtyInput.length = " + qtyInput[i].value);
    }
}

function AllowRemoveMaterial() {
    let deleteCell = document.getElementsByClassName("delete");

    for (let i = 0; i < deleteCell.length; i++) {
        deleteCell[0].hidden = false;
    }

}

$(document).ready(function () {
    console.log("Admin login");
    AllowChangeQty();
    AllowRemoveMaterial();
});



