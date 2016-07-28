define(['plugins/dialog', 'knockout'], function (dialog, ko) {

    var CreateCourseDialog = function () {
        this.courseTitle = ko.observable('');
    };

    CreateCourseDialog.prototype.ok = function () {
        dialog.close(this, this.courseTitle());
    };

    CreateCourseDialog.show = function () {
        return dialog.show(new CreateCourseDialog());
    };

    return CreateCourseDialog;
});