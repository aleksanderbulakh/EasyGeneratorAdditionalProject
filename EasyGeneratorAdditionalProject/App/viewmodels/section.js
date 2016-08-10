define(['knockout', 'durandal/app', 'repositories/sectionRepository', 'repositories/questionRepository',
    'customPlugins/customMessages/customMessage', 'customPlugins/createQuestionDialog/createQuestionDialog',
    'services/validateService', 'errorHandler/errorHandler'],
    function (ko, app, sectionRepository, questionRepository, message, createQuestionDialog, validateService, errorHandler) {

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
                        return questionRepository.getQuestionsBySectionId(this.sectionId)
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
                        message.stateMessage("Data is not found.");
                    }

                    app.on('question:deleted').then(function (questionId) {
                        debugger;
                        var question = self.questionList().find(function (question) {
                            return question.id === questionId;
                        });

                        validateService.throwIfObjectIsUndefined(question, 'Question');

                        self.questionList.remove(question);
                    });

                    app.on('question:modified').then(function (modifiedDate) {

                        var question = self.questionList().find(function (question) {
                            return question.id === questionId;
                        });

                        validateService.throwIfObjectIsUndefined(question, 'Question');

                        question.lastModifiedDate = modifiedDate;
                    });
                },
                editSectionTitle: function () {
                    var self = this;

                    sectionRepository.editSectionTitle(this.sectionId, this.sectionTitle())
                        .then(function (modifiedDate) {
                            self.currentSectionTitle = self.sectionTitle();
                            self.lastModifiedDate = modifiedDate;
                            message.stateMessage("Title has been changed.", "Success");
                        });
                },
                deleteSection: function () {
                    var self = this;

                    message.confirmMessage()
                        .then(function (result) {
                            if (result) {
                                sectionRepository.deleteSection(self.sectionId)
                                    .then(function () {
                                        debugger;
                                        app.trigger('section:deleted', self.sectionId);
                                        message.stateMessage("Section has been deleted.", "Success");
                                    });
                            }
                        });
                },

                createQuestion: function () {
                    var self = this;
                    createQuestionDialog.show()
                        .then(function (type) {

                            questionRepository.createQuestion(self.sectionId, type)
                                .then(function (result) {
                                    self.questionList.push(result);
                                    message.stateMessage('Section has been created.', 'Success');
                                });
                        });
                }
            };
        };
    });