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
                    if (_.isUndefined(data)) {
                        message.stateMessage(constants.MESSAGES.DATA_IS_NOT_FOUND);
                    } else if (!_.isUndefined(data.sectionData) && !_.isUndefined(data.courseId)) {
                        this.courseId = data.courseId;
                        this.sectionId = data.sectionData.id;
                        this.sectionTitle(data.sectionData.title);
                        this.createdBy = data.sectionData.createdBy;
                        this.createdOn = data.sectionData.createdOn;
                        this.modifiedBy = data.sectionData.modifiedBy;
                        this.lastModifiedDate = data.sectionData.lastModifiedDate;
                        this.currentSectionTitle = data.sectionData.title;

                        var self = this;
                        this.isChangeable = ko.computed(function () {
                            return self.sectionTitle.hasError() === (self.sectionTitle() !== self.currentSectionTitle);
                        });

                        app.on(constants.EVENTS.QUESTION_DELETE)
                            .then(function (questionId) {

                                var question = _.find(self.questionList(), function (question) {
                                    return question.id === questionId;
                                });

                                if (!_.isUndefined(question)) {

                                    self.questionList.remove(question);
                                }
                            });

                        return IoC.questionRepository.getQuestionsBySectionId(this.sectionId)
                            .then(function (questionList) {

                                self.questionList(_.map(questionList, function (question) {
                                    return question;
                                }));
                            });
                    }
                    else {
                        message.stateMessage(constants.MESSAGES.DATA_IS_NOT_FOUND);
                    }
                },
                editSectionTitle: function () {
                    var self = this;

                    IoC.sectionRepository.editSectionTitle(this.sectionId, this.sectionTitle())
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
                                IoC.sectionRepository.deleteSection(self.sectionId)
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

                            IoC.questionRepository.createQuestion(self.sectionId, type)
                                .then(function (result) {
                                    self.questionList.push(result);
                                    message.stateMessage('Question has been created.', constants.MESSAGES_STATE.SUCCESS);
                                });
                        });
                }
            };
        };
    });