define(['plugins/dialog', 'knockout'],
    function (dialog, ko, Question) {

        var createQuestionDialog = function () { };

        createQuestionDialog.prototype.create = function (type) {
            dialog.close(this, type);
        };

        createQuestionDialog.show = function () {
            return dialog.show(new createQuestionDialog());
        };

        return createQuestionDialog;
    });