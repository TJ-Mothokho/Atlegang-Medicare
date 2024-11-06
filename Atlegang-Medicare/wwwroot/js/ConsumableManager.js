//Add Consumables

// Function to enable or disable the submit button based on field values
function toggleSubmitButton() {
    var consumableName = document.getElementById('Name').value.trim();
    var quantity = document.getElementById('quantity').value.trim();
    var cost = document.getElementById('cost').value.trim();
    var supplierSelect = document.getElementById('supplierSelect').value;
    var OnHandThreshold = document.getElementById('OnHandThreshold').value.trim();
    var wardThreshold = document.getElementById('wardThreshold').value.trim();
    var submitButton = document.getElementById('submitButton');

    // Disable the submit button if all fields are empty
    if (!consumableName || !quantity || !cost || !supplierSelect || !OnHandThreshold || !wardThreshold) {
        submitButton.disabled = true;
    } else {
        submitButton.disabled = false;
    }
}

// Event listeners for change/input in each field
document.getElementById('Name').addEventListener('input', toggleSubmitButton);
document.getElementById('quantity').addEventListener('input', toggleSubmitButton);
document.getElementById('cost').addEventListener('input', toggleSubmitButton);
document.getElementById('supplierSelect').addEventListener('change', toggleSubmitButton);
document.getElementById('OnHandThreshold').addEventListener('input', toggleSubmitButton);
document.getElementById('wardThreshold').addEventListener('input', toggleSubmitButton);

// Initial check in case fields are already empty when the page loads
window.onload = function () {
    toggleSubmitButton();
};



// View Consumables
document.getElementById('consumableSelect').addEventListener('change', function () {
    var consumableSelect = document.getElementById('consumableSelect');
    var searchButton = document.getElementById('searchButton');
    var errorSpan = document.getElementById('consumableError');

    if (consumableSelect.value === "") {
        searchButton.disabled = true;
        errorSpan.textContent = "Please select a consumable.";
    } else {
        searchButton.disabled = false;
        errorSpan.textContent = ""; // Clear the error message
    }
});

// Reset button functionality
document.getElementById('resetButton').addEventListener('click', function () {
    var consumableSelect = document.getElementById('consumableSelect');

    // Set ConsumableID to 0
    consumableSelect.value = ""; // Set the dropdown to default option
    document.getElementById('searchButton').disabled = true; // Disable the search button again

    // Create a hidden input to store ConsumableID = 0
    var hiddenInput = document.createElement('input');
    hiddenInput.type = 'hidden';
    hiddenInput.name = 'ConsumableID';
    hiddenInput.value = '0';

    // Append hidden input to the form
    var form = document.getElementById('consumableForm');
    form.appendChild(hiddenInput);

    // Submit the form
    form.submit();
});