 document.addEventListener("DOMContentLoaded", function () {
        const startDateInput = document.getElementById("startDate");
        const endDateInput = document.getElementById("endDate");

        // Set the minimum date and time to the current date and time for both inputs
        const now = new Date();
        const currentDateTime = now.toISOString().slice(0, 16); // Get current date and time in YYYY-MM-DDTHH:MM format
        startDateInput.setAttribute('min', currentDateTime);
        endDateInput.setAttribute('min', currentDateTime);

        // Ensure the end date is always after or equal to the start date
        startDateInput.addEventListener('change', function () {
            const selectedStartDate = startDateInput.value;
            endDateInput.setAttribute('min', selectedStartDate); // Set the end date's minimum to the selected start date
        });

        endDateInput.addEventListener('change', function () {
            const selectedStartDate = new Date(startDateInput.value);
            const selectedEndDate = new Date(endDateInput.value);

            if (selectedEndDate < selectedStartDate) {
                endDateInput.setCustomValidity("End date cannot be earlier than start date.");
            } else {
                endDateInput.setCustomValidity(""); // Clear error if valid
            }
        });
 });

document.addEventListener("DOMContentLoaded", function () {
    const issuedDateInput = document.getElementById("issuedDate");

    // Set the minimum date and time to the current date and time
    const now = new Date();
    const currentDateTime = now.toISOString().slice(0, 16); // Format: YYYY-MM-DDTHH:MM
    issuedDateInput.setAttribute('min', currentDateTime);
});

