$(document).ready(function () {

    // Load Branches
    function loadBranches(courseId, selectedBranchId = null) {

        $("#BranchId").empty();
        $("#BranchId").append('<option value="">Select Branch</option>');

        if (courseId !== "") {
            $.ajax({
                url: '/Home/GetBranchesByCourse',
                type: 'GET',
                data: { courseId: courseId },

                success: function (data) {

                    $.each(data, function (index, item) {

                        $("#BranchId").append(
                            `<option value="${item.branchId}">
                                ${item.branchName}
                             </option>`
                        );

                    });

                    if (selectedBranchId) {
                        $("#BranchId").val(selectedBranchId);
                    }
                },

                error: function () {
                    alert("Failed to load branches.");
                }
            });
        }
    }

    // Course change
    $("#CourseId").change(function () {
        var courseId = $(this).val();
        loadBranches(courseId);
    });

    // Edit mode auto branch load
    var courseId = $("#CourseId").val();
    var branchId = $("#BranchId").data("selected");

    if (courseId) {
        loadBranches(courseId, branchId);
    }

    // Save / Edit AJAX
    $("#studentForm").submit(function (e) {

        e.preventDefault();

        var form = $(this);

        $.ajax({
            url: form.attr("action"),
            type: 'POST',
            data: form.serialize(),

            success: function (response) {

                if (response.success) {

                    alert(response.message);

                    // Edit mode
                    if (form.attr("action").includes("Edit")) {

                        $("#studentForm")[0].reset();

                        $("#StudentId").val(0);

                        $("#BranchId").empty();
                        $("#BranchId").append('<option value="">Select Branch</option>');

                        window.location.href = '/Home/Index';
                    }
                    else {
                        // Save mode
                        location.reload();
                    }
                }
            },

            error: function () {
                alert("Operation failed.");
            }
        });

    });

    // Delete AJAX
    $(document).on("click", ".deleteStudent", function (e) {

        e.preventDefault();

        if (!confirm("Are you sure you want to delete this student?")) {
            return false;
        }

        var studentId = $(this).data("id");

        $.ajax({
            url: '/Home/Delete',
            type: 'POST',
            data: { studentId: studentId },

            success: function (response) {

                alert(response.message);

                if (response.success) {
                    location.reload();
                }
            },

            error: function () {
                alert("Delete failed.");
            }
        });

    });

});

function loadSubjectBranches(courseId, selectedBranchId = null) {

    $("#SubjectBranchId").empty();
    $("#SubjectBranchId").append('<option value="">Select Branch</option>');

    if (courseId !== "") {
        $.ajax({
            url: '/Subject/GetBranchesByCourse',
            type: 'GET',
            data: { courseId: courseId },

            success: function (data) {

                $.each(data, function (index, item) {

                    $("#SubjectBranchId").append(
                        `<option value="${item.branchId}">
                            ${item.branchName}
                         </option>`
                    );

                });

                if (selectedBranchId) {
                    $("#SubjectBranchId").val(selectedBranchId);
                }
            },

            error: function () {
                alert("Failed to load branches.");
            }
        });
    }
}

// SUBJECT COURSE CHANGE
$("#SubjectCourseId").change(function () {
    var courseId = $(this).val();
    loadSubjectBranches(courseId);
});

// SUBJECT EDIT MODE
var subjectCourseId = $("#SubjectCourseId").val();
var subjectBranchId = $("#SubjectBranchId").data("selected");

if (subjectCourseId) {
    loadSubjectBranches(subjectCourseId, subjectBranchId);
}

// SUBJECT SAVE / EDIT AJAX
$("#subjectForm").submit(function (e) {

    e.preventDefault();

    var form = $(this);

    $.ajax({
        url: form.attr("action"),
        type: 'POST',
        data: form.serialize(),

        success: function (response) {

            if (response.success) {

                alert(response.message);

                if (form.attr("action").includes("Edit")) {
                    window.location.href = '/Subject/Index';
                }
                else {
                    location.reload();
                }
            }
        },

        error: function () {
            alert("Operation failed.");
        }
    });

});

// SUBJECT DELETE AJAX
$(document).on("click", ".deleteSubject", function () {

    if (!confirm("Are you sure you want to delete this subject?")) {
        return;
    }

    var subjectId = $(this).data("id");

    $.ajax({
        url: '/Subject/Delete',
        type: 'POST',
        data: { subjectId: subjectId },

        success: function (response) {

            alert(response.message);

            if (response.success) {
                location.reload();
            }
        },

        error: function () {
            alert("Delete failed.");
        }
    });

});