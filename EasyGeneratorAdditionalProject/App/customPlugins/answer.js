define(['knockout', 'plugins/router', 'durandal/app', 'data/answerRepository', 'customPlugins/customMessage'],
    function (ko, router, app, answerRepository, message) {

        return function () {

            return {
                viewUrl: 'customPlugins/answer',
                courseId: '',
                sectionId: '',
                questionId: '',
                questionType: '',
                answerId: '',
                currentText: '',
                text: ko.observable().extend({
                    validName: 'Please, enter question title! Maximum number of characters - 255.'
                }),
                isCorrect: ko.observable(),
                isChangeable: true,

                activate: function (data) {
                    if (data !== undefined) {
                        this.courseId = data.courseId;
                        this.sectionId = data.sectionId;
                        this.questionId = data.questionId;
                        this.questionType = data.questionType;
                        this.answerId = data.answerData.id;
                        this.currentText = data.answerData.text;
                        this.text(data.answerData.text);
                        this.isCorrect(data.answerData.isCorrect);

                        var self = this;

                        this.isChangeable = ko.computed(function () {
                            return self.text.hasError() === (self.text() !== self.currentText);
                        });
                    }

                },
                editText: function () {
                    var self = this;

                    answerRepository.editAnswerText(this.courseId, this.sectionId, this.questionId, this.answerId, this.text())
                        .then(function () {
                            self.currentText = self.text();
                            message.stateMessage("Text has been changed.", "Success");
                        })
                        .fail(function (result) {
                            message.stateMessage(result, "Error");
                        });
                },
                deleteAnswer: function () {
                    var self = this;

                    message.confirmMessage()
                        .then(function (result) {
                            if (result) {
                                answerRepository.deleteAnswer(self.courseId, self.sectionId, self.questionId, self.answerId)
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
            };
        };
    });