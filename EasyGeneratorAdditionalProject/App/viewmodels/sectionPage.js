define(['knockout', 'plugins/router', 'durandal/app', 'data/sectionRepository', 'customPlugins/customMessage', 'customPlugins/createQuestionDialog'],
    function (ko, router, app, sectionRepository, message, createQuestionDialog) {

        return function () {

            return {
                viewUrl: 'views/sectionPage',
                sectionData: ko.observable(),
                sectionId: ko.observable(),
                sectionTitle: ko.observable().extend({
                    validName: 'Please, enter section title! Maximum number of characters - 255.'
                }),
                questionList: ko.observable(),
                isChangeable: true,

                activate: function (sectionData) {
                    if (sectionData !== undefined) {
                        this.sectionData(sectionData);
                        this.sectionId(sectionData.id);
                        this.sectionTitle(sectionData.title);
                        this.questionList(sectionData.questionList);

                        var self = this;
                        this.isChangeable = ko.computed(function () {
                            return self.sectionTitle.hasError() === (self.sectionTitle() !== self.sectionData().title);
                        });
                    }
                    else {
                        message.stateMessage("Section is not found.");
                    }
                },
                editSectionTitle: function () {
                    var self = this;

                    sectionRepository.editSectionTitle(this.sectionId(), this.sectionTitle())
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
                            sectionRepository.deleteSection(self.sectionId())
                                .then(function () {
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
                    createQuestionDialog.show().then(function (type) {
                        alert(type);
                    });
                }
            };
        };
    });