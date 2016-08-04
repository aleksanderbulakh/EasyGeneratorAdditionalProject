define(['plugins/dialog', 'durandal/app', 'knockout', 'data/answerRepository', 'data/constants',
    'customPlugins/customMessages/customMessage'],
    function (dialog, app, ko, answerRepository, constants, message) {

        var answerEdit = function (dataObj) {

            this.constants = constants;
            this.courseId = dataObj[0];
            this.sectionId = dataObj[1];
            this.questionId = dataObj[2];
            this.questionType = constants.ANSWER_TYPES[dataObj[3]];
            this.answers = ko.observableArray([]);

            var self = this;
            answerRepository.getAnswersByQuestionId(this.courseId, this.sectionId, this.questionId)
                .then(function (answers) {
                    self.answers(answers.map(function (answer) {
                        return {
                            id: answer.id,
                            text: ko.observable(answer.text).extend({
                                validName: 'Please, enter answer text!'
                            }),
                            isCorrect: ko.observable(answer.isCorrect)
                        };
                    }));
                });

            app.on('answer:deleted').then(function (answerId) {
                var answerIndex = self.answers().findIndex(function (answer) {
                    return answer.id === answerId;
                });

                if (answerIndex >= 0)
                    self.answers().splice(answerIndex, 1);
            });
        };

        answerEdit.prototype.addAnswer = function () {
            self = this;
            answerRepository.createAnswer(this.courseId, this.sectionId, this.questionId)
                .then(function (answer) {
                    self.answers().push(answer);
                    self.answers.valueHasMutated();
                    message.stateMessage("Question has been deleted.", "Success");
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
            answerRepository.editAnswerText(this.courseId, this.sectionId, this.questionId, answerId, text())
                .then(function () {
                    message.stateMessage("Text has been changed.", "Success");
                })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });
        },

        answerEdit.prototype.editState = function (answerId, state) {
            var self = this;
            answerRepository.editAnswerState(this.courseId, this.sectionId, this.questionId, answerId, state())
                .then(function () {
                    message.stateMessage("State has been changed.", "Success");
                })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });
        }

        answerEdit.prototype.deleteAnswer = function (answerId) {
            var self = this;
            message.confirmMessage()
                .then(function (result) {
                    if (result) {
                        answerRepository.deleteAnswer(self.courseId, self.sectionId, self.questionId, answerId)
                            .then(function () {
                                app.trigger('answer:deleted', answerId);
                                message.stateMessage("Question has been deleted.", "Success");
                            })
                            .fail(function (result) {
                                message.stateMessage(result, "Error");
                            });
                    }
                });
        }

        answerEdit.prototype.computecorrectAnswer = function (answerId) {

            if (this.questionType === 'radio') {
                this.answers().forEach(function (answer) {
                    if (answer.id === answerId) {
                        answer.isCorrect(true);
                    }
                    else {
                        answer.isCorrect(false);
                    }
                });
            }
            if (this.questionType === 'checkbox') {
                var answer = this.answers().find(function (answer) {
                    return answer.id === answerId;
                });

                if (answer.isCorrect() === true) {
                    answer.isCorrect(false);
                }
                else {
                    answer.isCorrect(true);
                }
            }
        }

        return answerEdit;
    });