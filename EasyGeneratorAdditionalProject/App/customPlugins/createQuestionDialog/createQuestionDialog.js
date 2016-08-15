define(['plugins/dialog', 'constants/constants'],
    function (dialog, constants) {

        var createQuestionDialog = function () { };

        createQuestionDialog.prototype.createMaterial = function () {
            dialog.close(this, constants.SINGLE_SELECT_QUESTION_TYPE);
        };

        createQuestionDialog.prototype.createSingleSelectQuestion = function () {
            dialog.close(this, constants.SINGLE_SELECT_QUESTION_TYPE);
        };

        createQuestionDialog.prototype.createMultipleSelectQuestion = function () {
            dialog.close(this, constants.MULTIPLE_SELECT_QUESTION_TYPE);
        };

        createQuestionDialog.prototype.createSingleSelectImageQuestion = function () {
            dialog.close(this, constants.SINGLE_SELECT_IMAGE_QUESTION_TYPE);
        };

        createQuestionDialog.show = function () {
            return dialog.show(new createQuestionDialog());
        };

        return createQuestionDialog;
    });