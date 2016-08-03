define(['plugins/dialog', 'durandal/app', 'knockout', 'data/answerRepository', 'data/constants'],
    function (dialog, app, ko, answerRepository, constants) {

        var answerEdit = function (dataObj) {

            this.constants = constants;
            this.courseId = dataObj[0];
            this.sectionId = dataObj[1];
            this.questionId = dataObj[2];
            this.questionType = constants.ANSWER_TYPES[dataObj[3]];
            this.answers = ko.observableArray([]);

            var self = this;
            answerRepository.getAnswersByQuestionId(this.courseId, this.sectionId, this.questionId)
                .then(function (result) {
                    self.answers(result);
                });

            app.on('data:changed')
                .then(function () {
                    self.answers.valueHasMutated();
                });
        };

        answerEdit.prototype.addAnswer = function () {
            debugger;
            answerRepository.createAnswer(this.courseId, this.sectionId, this.questionId)
                .then(function () {
                    app.trigger('data:changed');
                });
        }

        answerEdit.prototype.ok = function () {
            dialog.close(this);
        };

        answerEdit.show = function () {
            return dialog.show(new answerEdit(arguments));
        };

        answerEdit.prototype.editText = function (answerId, text) {
            var self = this;
            debugger;
            var editebleText = $('#' + answerId).text();
            answerRepository.editAnswerText(this.courseId, this.sectionId, this.questionId, answerId, text)
                .then(function () {
                    self.currentText = text;
                    message.stateMessage("Text has been changed.", "Success");
                })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });
        },
        answerEdit.prototype.deleteAnswer = function () {
            var self = this;
            debugger;
            message.confirmMessage(answerId)
                .then(function (result) {
                    if (result) {
                        answerRepository.deleteAnswer(self.courseId, self.sectionId, self.questionId, answerId)
                            .then(function () {

                                app.trigger('data:changed');
                                message.stateMessage("Question has been deleted.", "Success");
                            })
                            .fail(function (result) {
                                message.stateMessage(result, "Error");
                            });
                    }
                });
        }
        return answerEdit;
    });