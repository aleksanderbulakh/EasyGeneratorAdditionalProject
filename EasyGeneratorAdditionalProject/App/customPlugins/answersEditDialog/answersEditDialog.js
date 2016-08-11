define(['plugins/dialog', 'durandal/app', 'knockout', 'repositories/answerRepository', 'constants/constants',
    'customPlugins/customMessages/customMessage', 'services/validateService'],
    function (dialog, app, ko, answerRepository, constants, message, validateService) {

        var answerEdit = function (questionId, questionType) {

            this.questionId = questionId;
            this.questionType = constants.VIEWS_ANSWER_TYPES[questionType];
            this.answers = ko.observableArray([]);

            var self = this;

            answerRepository.getAnswersByQuestionId(this.questionId)
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

            app.on(constants.EVENTS.ANSWER_DELETED).then(function (answerId) {
                var answer = self.answers().find(function (answer) {
                    return answer.id === answerId;
                });

                validateService.throwIfObjectIsUndefined(answer, constants.MODELS_NAMES.ANSWER);

                self.answers.remove(answer);
            });
        };
        
        answerEdit.show = function (questionId, questionType) {
            return dialog.show(new answerEdit(questionId, questionType));
        };

        answerEdit.prototype.addAnswer = function () {
            self = this;
            answerRepository.createAnswer(this.questionId)
                .then(function (answer) {
                    self.answers.push(answer);
                    message.stateMessage("Question has been deleted.", constants.MESSAGES_STATE.SUCCESS);
                });
        };

        answerEdit.prototype.close = function () {
            dialog.close(this);
        };

        answerEdit.prototype.editText = function (answerId, text) {
            var self = this;
            answerRepository.editAnswerText(this.questionId, answerId, text())
                .then(function (modifiedDate) {
                    message.stateMessage("Text has been changed.", constants.MESSAGES_STATE.SUCCESS);
                });
        },

        answerEdit.prototype.editState = function (answerId, state) {
            var self = this;
            answerRepository.editAnswerState(this.questionId, answerId, this.questionType, state())
                .then(function (modifiedDate) {
                    message.stateMessage("State has been changed.", constants.MESSAGES_STATE.SUCCESS);
                });
        };

        answerEdit.prototype.deleteAnswer = function (answerId) {
            var self = this;
            message.confirmMessage()
                .then(function (result) {
                    if (result) {
                        answerRepository.deleteAnswer(answerId)
                            .then(function () {
                                app.trigger(constants.EVENTS.ANSWER_DELETED, answerId);
                                message.stateMessage("Question has been deleted.", constants.MESSAGES_STATE.SUCCESS);
                            });
                    }
                });
        };

        answerEdit.prototype.computeCorrectAnswer = function (answerId) {

            if (this.questionType === constants.QUESTION_TYPE_RADIO) {
                this.answers().forEach(function (answer) {
                    if (answer.id === answerId) {
                        answer.isCorrect(true);
                    } else {
                        answer.isCorrect(false);
                    }
                });
            } else if (this.questionType === constants.QUESTION_TYPE_CHECKBOX) {
                var answer = this.answers().find(function (answer) {
                    return answer.id === answerId;
                });

                if (answer.isCorrect()) {
                    answer.isCorrect(false);
                } else {
                    answer.isCorrect(true);
                }
            }
        };

        return answerEdit;
    });