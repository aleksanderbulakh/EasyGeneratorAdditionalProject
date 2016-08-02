define(['knockout', 'plugins/router', 'durandal/app', 'data/sectionRepository', 'data/questionRepository', 'customPlugins/customMessage', 'customPlugins/createQuestionDialog'],
    function (ko, router, app, sectionRepository, questionRepository, message, createQuestionDialog) {

        return function () {

            return {
                viewUrl: 'views/section',
                courseId: '',
                sectionData: ko.observable(),
                sectionId: '',
                sectionTitle: ko.observable().extend({
                    validName: 'Please, enter section title! Maximum number of characters - 255.'
                }),
                questionList: ko.observable(),
                isChangeable: true,

                activate: function (data) {
                    if (data.sectionData !== undefined && data.courseId !== undefined) {
                        this.courseId = data.courseId;
                        this.sectionId = data.sectionData.id;
                        this.sectionData(data.sectionData);
                        this.sectionTitle(data.sectionData.title);

                        var self = this;
                        questionRepository.getQuestionsBySectionId(this.courseId, this.sectionId).then(function () {
                            self.questionList(data.sectionData.questionList);
                        });

                        this.isChangeable = ko.computed(function () {
                            return self.sectionTitle.hasError() === (self.sectionTitle() !== self.sectionData().title);
                        });
                    }
                    else {
                        message.stateMessage("Data is not found.");
                    }
                },
                editSectionTitle: function () {
                    var self = this;

                    sectionRepository.editSectionTitle(this.courseId, this.sectionId, this.sectionTitle())
                        .then(function () {
                            self.sectionData().title = self.sectionTitle();
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
                                        debugger;
                                        app.trigger('data:changed');
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
                                .then(function () {
                                    app.trigger('data:changed');
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