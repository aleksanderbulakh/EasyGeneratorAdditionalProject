define(['knockout', 'durandal/app', 'repositories/questionRepository', 'customPlugins/customMessages/customMessage',
    'customPlugins/answersEditDialog/answersEditDialog', 'errorHandler/errorHandler'],
    function (ko, app, questionRepository, message, answersEditDialog, errorHandler) {

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
                        message.stateMessage("Data is not found.");
                    }
                },
                editTitle: function () {
                    var self = this;

                    questionRepository.editQuestionTitle(this.id, this.title())
                        .then(function (modifiedDate) {
                            self.currentTitle = self.title();
                            app.trigger('question:modified', modifiedDate);
                            message.stateMessage("Title has been changed.", "Success");
                        });
                },
                deleteQuestion: function () {
                    var self = this;

                    message.confirmMessage()
                        .then(function (result) {
                            if (result) {
                                questionRepository.deleteQuestion(self.id)
                                    .then(function () {

                                        app.trigger('question:deleted', self.id);
                                        message.stateMessage("Question has been deleted.", "Success");
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