define(['knockout', 'preview/previewRepository', 'errorHandler/errorHandler', 'constants/constants', 'mapper/mapper'],
    function (ko, previewRepository, errorHandler, constants, mapper) {

        return {
            sectionId: '',
            questions: ko.observableArray(),
            activate: function (sectionId) {
                this.questions([]);
                this.sectionId = sectionId;

                var self = this;

                return previewRepository.getQuestionsBySectionId(sectionId)
                    .then(function (questionsList) {
                        self.questions(questionsList);
                    });
            },

            checkForCorrectness: function (question) {
                var self = this;
                question.checkForCorrectness();
                alert(question.result);
            }
        };
    });