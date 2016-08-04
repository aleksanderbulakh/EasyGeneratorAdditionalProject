define(['knockout', 'plugins/router', 'durandal/app', 'data/sectionRepository', 'data/questionRepository',
    'customPlugins/customMessages/customMessage', 'customPlugins/createQuestionDialog/createQuestionDialog'],
    function (ko, router, app, sectionRepository, questionRepository, message, createQuestionDialog) {

        return function () {

            return {
                viewUrl: 'views/section',
                courseId: '',
                sectionData: '',
                sectionId: '',
                sectionTitle: ko.observable().extend({
                    validName: 'Please, enter course title! Maximum number of characters - 255.'
                }),
                createdBy: '',
                createdOn: '',
                modifiedBy: '',
                lastModifiedDate: '',
                currentSectionTitle: '',
                questionList: ko.observableArray([]),
                isChangeable: true,

                activate: function (data) {

                    if (data.sectionData !== undefined && data.courseId !== undefined) {
                        this.courseId = data.courseId;
                        this.sectionId = data.sectionData.id;
                        this.sectionTitle(data.sectionData.title);
                        this.createdBy = data.sectionData.createdBy;
                        this.createdOn = data.sectionData.createdOn;
                        this.modifiedBy = data.sectionData.modifiedBy;
                        this.lastModifiedDate = data.sectionData.lastModifiedDate;
                        this.currentSectionTitle = data.sectionData.title;

                        var self = this;
                        questionRepository.getQuestionsBySectionId(this.courseId, this.sectionId)
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
                        var questionIndex = self.questionList().findIndex(function (question) {
                            return question.id === questionId;
                        });

                        if (questionIndex >= 0) { 
                            self.questionList().splice(questionIndex, 1);
                            self.questionList.valueHasMutated();
                        }
                    });
                },
                editSectionTitle: function () {
                    var self = this;

                    sectionRepository.editSectionTitle(this.courseId, this.sectionId, this.sectionTitle())
                        .then(function () {
                            self.currentSectionTitle = self.sectionTitle();
                            message.stateMessage("Title has been changed.", "Success");
                        })
                        .fail(function (result) {
                            message.stateMessage(result, "Error");
                        });
                },
                deleteSection: function () {
                    var self = this;

                    message.confirmMessage()
                        .then(function (result) {
                            if (result) {
                                sectionRepository.deleteSection(self.courseId, self.sectionId)
                                    .then(function () {
                                        app.trigger('section:deleted', self.sectionId);
                                        message.stateMessage("Section has been deleted.", "Success");
                                    })
                                    .fail(function (result) {
                                        message.stateMessage(result, "Error");
                                    });
                            }
                        });
                },

                createQuestion: function () {
                    var self = this;
                    createQuestionDialog.show()
                        .then(function (type) {

                            questionRepository.createQuestion(self.courseId, self.sectionId, type)
                                .then(function (result) {

                                    self.questionList().push(result);
                                    self.questionList.valueHasMutated();
                                    message.stateMessage('Section has been created.', 'Success');
                                })
                                .fail(function (result) {
                                    message.stateMessage(result, "Error");
                                });;
                        });
                }
            };
        };
    });