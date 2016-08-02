define(['plugins/dialog', 'knockout', 'models/question'],
    function (dialog, ko, Question, SingleSelectAnswer) {

        var createQuestionDialog = function () { };

        createQuestionDialog.prototype.create = function (type) {
            dialog.close(this, type);
        };

        createQuestionDialog.show = function () {
            return dialog.show(new createQuestionDialog());
        };

        return createQuestionDialog;
    });