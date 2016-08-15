define(['knockout', 'preview/data/previewRepository', 'preview/mapper/mapper'],
    function (ko, previewRepository, mapper) {

        return {
            sectionId: '',
            questions: ko.observableArray(),
            activate: function (sectionId) {
                this.questions([]);
                this.sectionId = sectionId;

                var self = this;

                return previewRepository.getQuestionsBySectionId(sectionId)
                    .then(function (questionsList) {
                        self.questions(_.map(questionsList, function (question) {

                            return mapper.mapQuestion({
                                id: question.id,
                                title: question.title,
                                type: question.type,
                                answersList: _.map(question.answersList, function (answer) {
                                    return {
                                        id: answer.id,
                                        text: answer.text,
                                        isCorrect: answer.isCorrect,
                                        checked: ko.observable(answer.checked)
                                    }
                                })
                            });
                        }));
                    });
            },

            checkForCorrectness: function (question) {
                question.checkForCorrectness();
                previewRepository.saveAnswer(this.sectionId, question.id, question.getResults());
                alert(question.result);
            }
        };
    });