define(['knockout', 'plugins/router', 'durandal/app', 'data/questionRepository', 'data/answerRepository',
    'customPlugins/customMessages/customMessage', 'customPlugins/createQuestionDialog/createQuestionDialog',
    'customPlugins/answersEditDialog/answersEditDialog'],
    function (ko, router, app, questionRepository, answerRepository, message, createQuestionDialog, questionEditDialog) {

        return function () {

            return {
                viewUrl: 'views/question',
                courseId: '',
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

                    if (data.questionData !== undefined && data.courseId !== undefined && data.sectionId !== undefined) {
                        this.courseId = data.courseId;
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
                        message.stateMessage("Data is not found.");
                    }
                },
                editTitle: function () {
                    var self = this;
                    
                    questionRepository.editQuestionTitle(this.courseId, this.sectionId, this.id, this.title())
                        .then(function () {
                            self.currentTitle = self.title();
                            message.stateMessage("Title has been changed.", "Success");
                        })
                        .fail(function (result) {
                            message.stateMessage(result, "Error");
                        });
                },
                deleteQuestion: function () {
                    var self = this;

                    message.confirmMessage()
                        .then(function (result) {
                            if (result) {
                                questionRepository.deleteQuestion(self.courseId, self.sectionId, self.id)
                                    .then(function () {
                                        app.trigger('question:deleted', self.id);
                                        message.stateMessage("Question has been deleted.", "Success");
                                    })
                                    .fail(function (result) {
                                        message.stateMessage(result, "Error");
                                    });
                            }
                        });
                },

                openAnswerDialog: function () {
                    questionEditDialog.show(this.courseId, this.sectionId, this.id, this.type);
                }
            };
        };
    });