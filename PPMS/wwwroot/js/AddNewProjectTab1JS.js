// wwwroot/js/addNewProject.js

document.addEventListener("DOMContentLoaded", function () {

    // ================= Project Charter File Name =================
    const fileInput = document.getElementById("projectCharter");
    const fileNameDisplay = document.getElementById("projectCharterName");
    if (fileInput && fileNameDisplay) {
        fileInput.addEventListener("change", function () {
            fileNameDisplay.textContent =
                fileInput.files.length > 0
                    ? `Selected file: ${fileInput.files[0].name}`
                    : "";
        });
    }

    // ================= Ongoing Toggle =================
    const isOngoing = document.getElementById("IsOngoing");
    const toDateContainer = document.getElementById("toDateContainer");
    const toDateInput = document.getElementById("ToDate");
    const fromDateInput = document.getElementById("FromDate");

    function toggleToDate() {
        if (isOngoing && isOngoing.checked) {
            toDateContainer.classList.add("d-none");
            toDateInput.value = "";
            toDateInput.classList.remove("is-invalid");
        } else {
            toDateContainer.classList.remove("d-none");
        }
    }

    if (isOngoing) {
        toggleToDate();
        isOngoing.addEventListener("change", toggleToDate);
    }   

    // ================= Prevent Submit =================
    const form = document.getElementById("AddNewProject");
    if (form) {
        form.addEventListener("submit", function (e) {
            if (!validateDates()) {
                e.preventDefault();
            }
        });
    }

    // ================= Participants Search =================
    function searchParticipants() {
        const searchInput = document.getElementById("participantSearch");
        if (!searchInput) return;

        const searchValue = searchInput.value.toLowerCase();
        const items = document.querySelectorAll(".participant-item");

        items.forEach(item => {
            const nameEl = item.querySelector(".participant-name");
            if (!nameEl) return;

            const name = nameEl.innerText.toLowerCase();
            item.style.display = name.includes(searchValue) ? "block" : "none";
        });
    }

    const searchInput = document.getElementById("participantSearch");
    if (searchInput) {
        searchInput.addEventListener("keyup", searchParticipants);
    }


    //----------

         const fromDate = document.getElementById('fromDate');
        const toDate = document.getElementById('toDate');
        const errorMsg = document.getElementById('dateError'); 
                            const validateDates = () => { 
                                const fromVal = fromDate.value ? new Date(fromDate.value) : null;
        const toVal = toDate.value ? new Date(toDate.value) : null;
        // Reset state 
        errorMsg.style.display = 'none';
        fromDate.classList.remove('is-invalid');
        toDate.classList.remove('is-invalid');
        if (!fromVal || !toVal) return;
                                // Don’t validate until both are filled
                                  if (fromVal >= toVal) {
            //  Show error state 
            errorMsg.style.display = 'block'; 
                                toDate.classList.add('is-invalid'); }
        else {
            // Update min for "To" to ensure UX consistency 
            toDate.min = fromDate.value; errorMsg.style.display = 'none'; 
                                    toDate.classList.remove('is-invalid'); } };
                                    // Bind events 
                                    fromDate.addEventListener('input', () => {
            // Set the minimum selectable "To" date dynamically 
            toDate.min = fromDate.value || ''; validateDates(); });
            toDate.addEventListener('input', validateDates); })();

