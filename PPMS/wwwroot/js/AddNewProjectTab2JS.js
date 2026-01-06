document.addEventListener("DOMContentLoaded", function () {

    //Tab-2
    const container = document.getElementById("objectivesContainer");
    const addBtn = document.getElementById("addObjectiveBtn");
    const form = document.getElementById("AddNewProject");

    // تأكد أن العناصر موجودة (لأنها داخل Tab)
    if (!container || !addBtn || !form) return;

    addBtn.addEventListener("click", () => {

        const index = container.children.length;

        const html = `
        <div class="objective-item mb-3 position-relative">
            <label>Objective ${index + 1}</label>

            <textarea name="Objectives[${index}]"
                      class="form-control mb-2"
                      rows="2"></textarea>

            <div class="d-flex gap-2 mb-2">
                <div class="flex-grow-1">
                    <label class="form-label">Start Date</label>
                    <input type="date"
                           name="StartDates[${index}]"
                           class="form-control">
                </div>

                <div class="flex-grow-1">
                    <label class="form-label">Due Date</label>
                    <input type="date"
                           name="EndDates[${index}]"
                           class="form-control">
                </div>
            </div>

            <!-- Weight below dates -->
            <div class="mb-2" style="max-width:120px;">
                <label class="form-label">Weight (%)</label>
                <input type="number"
                       name="Weights[${index}]"
                       class="form-control"
                       min="0" max="100" value="0">
            </div>

            <button type="button"
                    class="btn btn-outline-danger remove-objective"
                    title="Delete"
                    style="float:right; width:24px; height:24px; padding:0;">
                ✕
            </button>
        </div>
        `;

        container.insertAdjacentHTML("beforeend", html);
    });

    // ===== حذف هدف وإعادة الترقيم =====
    container.addEventListener("click", function (e) {
        if (e.target.classList.contains("remove-objective")) {
            e.target.closest(".objective-item").remove();
            reindexObjectives();
        }
    });

    // ===== إعادة ترقيم الحقول بعد الإضافة أو الحذف =====
    function reindexObjectives() {
        const items = container.querySelectorAll(".objective-item");

        items.forEach((item, index) => {
            // Label
            item.querySelector("label").innerText = `Objective ${index + 1}`;

            // النص
            item.querySelector("textarea").name = `Objectives[${index}]`;

            // التواريخ
            const startInput = item.querySelector('input[name^="StartDates"]');
            const endInput = item.querySelector('input[name^="EndDates"]');
            if (startInput) startInput.name = `StartDates[${index}]`;
            if (endInput) endInput.name = `EndDates[${index}]`;

            // الوزن
            const weightInput = item.querySelector('input[name^="Weights"]');
            if (weightInput) weightInput.name = `Weights[${index}]`;
        });
    }

    // ===== التحقق من مجموع الأوزان قبل الإرسال =====
    form.addEventListener("submit", function (e) {
        const weights = container.querySelectorAll('input[name^="Weights"]');
        let total = 0;

        weights.forEach(input => {
            const val = parseFloat(input.value);
            if (!isNaN(val)) total += val;
        });

        if (total !== 100) {
            e.preventDefault();
            alert(`⚠ Total Weight must be 100%. Current total: ${total}%`);
        }
    });


    // Tab1 Buttons

    document.addEventListener("DOMContentLoaded", function () {

        const saveDraftBtn = document.getElementById("btnSaveDraft");
        const nextBtn = document.getElementById("btnNextToObjectives");

        /* ================= SAVE DRAFT ================= */
        saveDraftBtn?.addEventListener("click", function () {

            const form = document.getElementById("AddNewProject");
            const formData = new FormData(form);

            // flag للدرافت
            formData.append("IsDraft", true);

            fetch("/Project/SaveDraft", {
                method: "POST",
                body: formData
            })
                .then(res => res.json())
                .then(data => {

                    if (data.success) {
                        alert("Draft saved successfully ✔");

                        // حفظ ProjectID للتاب الثاني
                        if (data.projectId) {
                            let hidden = document.getElementById("ProjectIDHidden");
                            if (!hidden) {
                                hidden = document.createElement("input");
                                hidden.type = "hidden";
                                hidden.id = "ProjectIDHidden";
                                hidden.name = "ProjectID";
                                form.appendChild(hidden);
                            }
                            hidden.value = data.projectId;
                        }
                    } else {
                        alert("Failed to save draft");
                    }
                });
        });

        /* ================= NEXT TAB ================= */
        nextBtn?.addEventListener("click", function () {

            const objectivesTab = document.querySelector('#objectives-tab');
            new bootstrap.Tab(objectivesTab).show();
        });

    });


});
