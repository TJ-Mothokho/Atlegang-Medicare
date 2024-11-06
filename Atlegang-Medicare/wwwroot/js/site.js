document.getElementById('IDNumber').addEventListener('input', function () {
    var idNumber = this.value;

    // Check if the IDNumber has at least 6 digits
    if (idNumber.length >= 6) {
        // Extract the first 6 digits
        var datePart = idNumber.substring(0, 6);

        // Parse the date part (yymmdd)
        var year = datePart.substring(0, 2);
        var month = datePart.substring(2, 4);
        var day = datePart.substring(4, 6);

        // Determine the century (assuming IDs in the 1900s and 2000s)
        var currentYear = new Date().getFullYear() % 100;
        var fullYear = parseInt(year);
        if (fullYear <= currentYear) {
            fullYear += 2000;
        } else {
            fullYear += 1900;
        }

        // Format the date in a readable format (yyyy-mm-dd)
        var formattedDate = fullYear + '-' + month + '-' + day;

        // Set the DateOfBirth input field
        document.getElementById('DateOfBirth').value = formattedDate;
    } else {
        // Clear the DateOfBirth field if IDNumber is too short
        document.getElementById('DateOfBirth').value = '';
    }
});