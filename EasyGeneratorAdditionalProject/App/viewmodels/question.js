define(['knockout', 'durandal/app', 'repositories/questionRepository', 'customPlugins/customMessages/customMessage',
    'customPlugins/answersEditDialog/answersEditDialog', 'errorHandler/errorHandler', 'constants/constants'],
    function (ko, app, questionRepository, message, answersEditDialog, errorHandler, constants) {

        return function () {

            return {
                viewUrl: 'views/question',
                sectionId: '',
                currentTitle: '',

                id: '',
                title: ko.observable().extend({
                    validName: 'Please, enter course title! Maximum number of characters - 255.'
                }),
                createdOn: '',
                createdBy: '',
                modifiedBy: '',
                lastModifiedDate: '',
                type: '',

                isChangeable: true,

                activate: function (data) {

                    if (typeof data.questionData !== 'undefined' && typeof data.questionData !== 'undefined'
                        && typeof data.courseId !== 'undefined' && typeof data.sectionId !== 'undefined') {
                        this.sectionId = data.sectionId;
                        this.id = data.questionData.id;
                        this.title(data.questionData.title);
                        this.createdOn = data.questionData.createdOn;
                        this.createdBy = data.questionData.createdBy;
                        this.modifiedBy = data.questionData.modifiedBy;
                        this.lastModifiedDate = data.questionData.lastModifiedDate;
                        this.type = data.questionData.type;

                        this.currentTitle = data.questionData.title;

                        var self = this;
                        self.isChangeable = ko.computed(function () {
                            return self.title.hasError() === (self.title() !== self.currentTitle);
                        });

                    }
                    else {
                        message.stateMessage(constants.MESSAGES.DATA_IS_NOT_FOUND);
                    }
                },
                editTitle: function () {
                    var self = this;

                    questionRepository.editQuestionTitle(this.id, this.title())
                        .then(function (modifiedDate) {
                            self.currentTitle = self.title();
                            app.trigger(constants.EVENTS.QUESTION_MODIFIED, modifiedDate);
                            message.stateMessage("Title has been changed.", constants.MESSAGES_STATE.SUCCESS);
                        });
                },
                deleteQuestion: function () {
                    var self = this;

                    message.confirmMessage()
                        .then(function (result) {
                            if (result) {
                                questionRepository.deleteQuestion(self.id)
                                    .then(function () {

                                        app.trigger(constants.EVENTS.QUESTION_DELETE, self.id);
                                        message.stateMessage("Question has been deleted.", constants.MESSAGES_STATE.SUCCESS);
                                    });
                            }
                        });
                },

                openAnswerDialog: function () {
                    answersEditDialog.show(this.id, this.type);
                }
            };
        };
    });