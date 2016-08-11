define(['plugins/dialog', 'knockout', 'constants/constants'],
    function (dialog, ko, constants) {

        var createQuestionDialog = function () {
            this.constants = constants;
        };

        createQuestionDialog.prototype.create = function (type) {
            dialog.close(this, type);
        };

        createQuestionDialog.show = function () {
            return dialog.show(new createQuestionDialog());
        };

        return createQuestionDialog;
    });