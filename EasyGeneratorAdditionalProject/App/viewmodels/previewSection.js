define(['knockout', 'repositories/questionRepository', 'repositories/answerRepository', 'preview/resultsRepository',
    'errorHandler/errorHandler', 'constants/constants', 'mapper/mapper'],
    function (ko, questionRepository, answerRepository, resultsRepository, errorHandler, constants, mapper) {

        function checkSingleSelectQuestion(question) {

            return answerRepository.getAnswersByQuestionId(question.id)
                .then(function (answers) {

                    return question.checkForCorrectness(answers);
                });
        }

        function checkMultipleSelectQuestion(question) {

            return answerRepository.getAnswersByQuestionId(question.id)
                .then(function (answers) {

                    return question.checkForCorrectness(answers);
                });
        }

        return {
            sectionId: '',
            questions: ko.observableArray(),
            activate: function (sectionId) {
                this.questions([]);
                this.sectionId = sectionId;

                var self = this;

                return questionRepository.getQuestionsBySectionId(sectionId)
                    .then(function (questions) {
                        questions.forEach(function (question) {
                            return answerRepository.getAnswersByQuestionId(question.id)
                                .then(function (answers) {

                                    var answersList = answers.map(function (answer) {
                                        return mapper.mapAnswerPreview(answer);
                                    });

                                    if (question.type === constants.SINGLE_SELECT_QUESTION_TYPE) {
                                        self.questions.push(mapper.mapSingleSelectQuestionPreview({
                                            id: question.id,
                                            title: question.title,
                                            type: question.type,
                                            answersList: answersList
                                        }));
                                    } else if (question.type === constants.MULTIPLE_SELECT_QUESTION_TYPE) {
                                        self.questions.push(mapper.mapMultipleSelectQuestionPreview({
                                            id: question.id,
                                            title: question.title,
                                            type: question.type,
                                            answersList: answersList
                                        }));
                                    }
                                })
                        });
                    });
            },

            sendResult: function () {
                resultsRepositort.setNewResult(this.questions);
            },

            checkForCorrectness: function (questionId) {
                var self = this;
                var question = this.questions().find(function (question) {
                    return question.id === questionId;
                });

                if (question.type === constants.QUESTION_TYPE_RADIO) {
                    checkSingleSelectQuestion(question)
                        .then(function (result) {
                            resultsRepository.setNewResult(self.sectionId, questionId, result);
                            alert(result);
                        });
                } else if (question.type === constants.QUESTION_TYPE_CHECKBOX) {
                    checkMultipleSelectQuestion(question)
                        .then(function (result) {
                            resultsRepository.setNewResult(self.sectionId, questionId, result);
                            alert(result);
                        });
                }
            }
        }
    });