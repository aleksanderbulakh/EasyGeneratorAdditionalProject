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

                            return mapper.mapQuestionViewModel({
                                id: question.id,
                                title: question.title,
                                type: question.type,
                                checked: question.checkedAnswer,
                                answersList: _.map(question.answersList, function (answer) {
                                    return {
                                        id: answer.id,
                                        text: answer.text,
                                        checked: ko.observable(answer.checked)
                                    }
                                })
                            });
                        }));
                    });
            },

            checkForCorrectness: function (question) {
                var result = previewRepository.checkAnswer(this.sectionId, question.id, question.getResult());
                alert(result);
            }
        };
    });