define(['plugins/dialog', 'durandal/app', 'knockout', 'IoC/IoC', 'constants/constants',
    'customPlugins/customMessages/customMessage', 'services/validateService'],
    function (dialog, app, ko, IoC, constants, message, validateService) {
        
        function isImageAnswer(questionType) {
            return questionType === constants.SINGLE_SELECT_IMAGE_QUESTION_TYPE ? true : false;
        }

        var answerEdit = function (questionId, questionType) {

            this.questionId = questionId;
            this.isImageAnswer = isImageAnswer(questionType);
            this.questionType = constants.VIEWS_ANSWER_TYPES[questionType];
            this.answers = ko.observableArray([]);
            this.photo = ko.observable({
                dataURL: ko.observable(),
                base64String: ko.observable(),
            });

            var self = this;

            IoC.answerRepository.getAnswersByQuestionId(this.questionId)
                .then(function (answers) {
                    self.answers(_.map(answers, function (answer) {
                        return {
                            id: answer.id,
                            text: ko.observable(answer.text).extend({
                                validName: 'Please, enter answer text!'
                            }),
                            isCorrect: ko.observable(answer.isCorrect)
                        };
                    }));
                });

            app.on(constants.EVENTS.ANSWER_DELETED)
                .then(function (answer) {

                    validateService.throwIfObjectIsUndefined(answer, constants.MODELS_NAMES.ANSWER);

                    self.answers.remove(answer);
                });
        };

        answerEdit.show = function (questionId, questionType) {
            return dialog.show(new answerEdit(questionId, questionType));
        };

        answerEdit.prototype.addAnswer = function () {
            self = this;
            IoC.answerRepository.createAnswer(this.questionId)
                .then(function (answer) {
                    var newAnswer = {};

                    newAnswer = {
                        id: answer.id,
                        text: ko.observable(answer.text).extend({
                            validName: 'Please, enter answer text!'
                        }),
                        isCorrect: ko.observable(answer.isCorrect)
                    };

                    self.answers.push(newAnswer);
                    message.stateMessage("Question has been deleted.", constants.MESSAGES_STATE.SUCCESS);
                });
        };

        answerEdit.prototype.close = function () {
            dialog.close(this);
        };

        answerEdit.prototype.editText = function (answer) {
            var self = this;
            IoC.answerRepository.editAnswerText(this.questionId, answer.id, answer.text())
                .then(function (modifiedDate) {
                    message.stateMessage("Text has been changed.", constants.MESSAGES_STATE.SUCCESS);
                });
        },

        answerEdit.prototype.editState = function (answer) {
            var self = this;
            IoC.answerRepository.editAnswerState(this.questionId, answer.id, this.questionType, answer.isCorrect())
                .then(function (modifiedDate) {
                    message.stateMessage("State has been changed.", constants.MESSAGES_STATE.SUCCESS);
                });
        };

        answerEdit.prototype.deleteAnswer = function (answer) {
            var self = this;
            message.confirmMessage()
                .then(function (result) {
                    if (result) {
                        IoC.answerRepository.deleteAnswer(answer.id)
                            .then(function () {
                                app.trigger(constants.EVENTS.ANSWER_DELETED, answer);
                                message.stateMessage("Question has been deleted.", constants.MESSAGES_STATE.SUCCESS);
                            });
                    }
                });
        };

        answerEdit.prototype.computeCorrectAnswer = function (newAnswer) {
            
            if (this.questionType === constants.QUESTION_TYPE_RADIO) {
                var answer = _.find(this.answers(), function (answer) {
                    return answer.isCorrect();
                });

                answer.isCorrect(!answer.isCorrect());
                newAnswer.isCorrect(!newAnswer.isCorrect());
            } else if (this.questionType === constants.QUESTION_TYPE_CHECKBOX) {
                newAnswer.isCorrect(!newAnswer.isCorrect());
            }
        };

        return answerEdit;
    });