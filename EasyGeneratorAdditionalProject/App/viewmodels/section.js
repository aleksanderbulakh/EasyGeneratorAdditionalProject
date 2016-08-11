define(['knockout', 'durandal/app', 'IoC/IoC', 
    'customPlugins/customMessages/customMessage', 'customPlugins/createQuestionDialog/createQuestionDialog',
    'services/validateService', 'errorHandler/errorHandler', 'constants/constants'],
    function (ko, app, IoC, message, createQuestionDialog, validateService, errorHandler, constants) {

        return function () {

            return {
                viewUrl: 'views/section',
                courseId: '',
                sectionData: '',
                sectionId: '',
                sectionTitle: ko.observable().extend({
                    validName: 'Please, enter section title! Maximum number of characters - 255.'
                }),
                createdBy: '',
                createdOn: '',
                modifiedBy: '',
                lastModifiedDate: '',
                currentSectionTitle: '',
                questionList: ko.observableArray([]),
                isChangeable: true,

                activate: function (data) {
                    if (typeof data !== 'undefined' && typeof data.sectionData !== 'undefined' && typeof data.courseId !== 'undefined') {
                        this.courseId = data.courseId;
                        this.sectionId = data.sectionData.id;
                        this.sectionTitle(data.sectionData.title);
                        this.createdBy = data.sectionData.createdBy;
                        this.createdOn = data.sectionData.createdOn;
                        this.modifiedBy = data.sectionData.modifiedBy;
                        this.lastModifiedDate = data.sectionData.lastModifiedDate;
                        this.currentSectionTitle = data.sectionData.title;

                        var self = this;
                        return IoC.getRepository(constants.REPOSITORIES_NAMES.QUESTION).getQuestionsBySectionId(this.sectionId)
                            .then(function (questionList) {
                                self.questionList(questionList.map(function (question) {
                                    return question;
                                }));
                            });

                        this.isChangeable = ko.computed(function () {
                            return self.sectionTitle.hasError() === (self.sectionTitle() !== self.currentSectionTitle);
                        });
                    }
                    else {
                        message.stateMessage(constants.MESSAGES.DATA_IS_NOT_FOUND);
                    }

                    app.on(constants.EVENTS.QUESTION_DELETE).then(function (questionId) {

                        var question = self.questionList().find(function (question) {
                            return question.id === questionId;
                        });

                        validateService.throwIfObjectIsUndefined(question, constants.MODELS_NAMES.QUESTION);

                        self.questionList.remove(question);
                    });

                    app.on(constants.EVENTS.QUESTION_MODIFIED).then(function (modifiedDate) {

                        var question = self.questionList().find(function (question) {
                            return question.id === questionId;
                        });

                        validateService.throwIfObjectIsUndefined(question, constants.MODELS_NAMES.QUESTION);

                        question.lastModifiedDate = modifiedDate;
                    });
                },
                editSectionTitle: function () {
                    var self = this;

                    IoC.getRepository(constants.REPOSITORIES_NAMES.SECTION).editSectionTitle(this.sectionId, this.sectionTitle())
                        .then(function (modifiedDate) {
                            self.currentSectionTitle = self.sectionTitle();
                            self.lastModifiedDate = modifiedDate;
                            message.stateMessage(constants.MESSAGES.TITLE_CHANGED, constants.MESSAGES_STATE.SUCCESS);
                        });
                },
                deleteSection: function () {
                    var self = this;

                    message.confirmMessage()
                        .then(function (result) {
                            if (result) {
                                IoC.getRepository(constants.REPOSITORIES_NAMES.SECTION).deleteSection(self.sectionId)
                                    .then(function () {

                                        app.trigger(constants.EVENTS.SECTION_DELETED, self.sectionId);
                                        message.stateMessage('Section has been deleted.', constants.MESSAGES_STATE.SUCCESS);
                                    });
                            }
                        });
                },

                createQuestion: function () {
                    var self = this;
                    createQuestionDialog.show()
                        .then(function (type) {

                            IoC.getRepository(constants.REPOSITORIES_NAMES.QUESTION).createQuestion(self.sectionId, type)
                                .then(function (result) {
                                    self.questionList.push(result);
                                    message.stateMessage('Question has been created.', constants.MESSAGES_STATE.SUCCESS);
                                });
                        });
                }
            };
        };
    });