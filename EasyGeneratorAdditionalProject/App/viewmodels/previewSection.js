define(['knockout', 'repositories/questionRepository', 'repositories/answerRepository', 'preview/resultsRepository',
    'errorHandler/errorHandler', 'constants/constants'],
    function (ko, questionRepository, answerRepository, resultsRepository, errorHandler, constants) {

        function checkSingleSelectQuestion(questionId, answersList) {

            return answerRepository.getAnswersByQuestionId(questionId)
                .then(function (answers) {

                    var answerForCheck = answersList.find(function (answer) {
                        return answer.isCorrect();
                    });

                    var isAnswerCorrect = answers.find(function (answer) {
                        return answer.id === answerForCheck.id && answer.isCorrect === answerForCheck.isCorrect();
                    });

                    return isAnswerCorrect !== undefined ? 1 : 0;
                });
        }

        function checkMultipleSelectQuestion(questionId, answersList) {

            return answerRepository.getAnswersByQuestionId(questionId)
                .then(function (answers) {

                    var correctCount = 0;
                    var countAnswers = answers.length;

                    answers.forEach(function (correctAnswer) {
                        var answerForCheck = answersList.find(function (answer) {
                            return answer.id === correctAnswer.id;
                        });

                        if (correctAnswer.isCorrect === answerForCheck.isCorrect()) {
                            correctCount++;
                        }
                    });

                    return correctCount / countAnswers;
                });
        }

        return {
            sectionId: '',
            questions: ko.observableArray([]),
            activate: function (sectionId) {

                this.sectionId = sectionId;

                var self = this;

                return questionRepository.getQuestionsBySectionId(sectionId)
                    .then(function (questions) {
                        questions.forEach(function (question) {
                            return answerRepository.getAnswersByQuestionId(question.id)
                                .then(function (answers) {

                                    var answersList = answers.map(function (answer) {
                                        return {
                                            id: answer.id,
                                            questionId: answer.questionId,
                                            text: answer.text,
                                            isCorrect: ko.observable(false)
                                        }
                                    });

                                    self.questions.push({
                                        id: question.id,
                                        title: question.title,
                                        type: constants.ANSWER_TYPES[question.type],
                                        answersList: answersList
                                    })
                                })
                        });
                    });
            },
            computeCorrectAnswer: function (questionId, answerId) {
                var question = this.questions().find(function (question) {
                    return question.id === questionId;
                });

                if (question === undefined)
                    throw 'error';

                if (question.type === 'radio') {
                    question.answersList.forEach(function (answer) {
                        if (answer.id === answerId) {
                            answer.isCorrect(true);
                        } else {
                            answer.isCorrect(false);
                        }
                    });
                } else if (question.type === 'checkbox') {
                    var answer = question.answersList.find(function (answer) {
                        return answer.id === answerId;
                    });

                    if (answer.isCorrect()) {
                        answer.isCorrect(false);
                    } else {
                        answer.isCorrect(true);
                    }
                }
            },
            sendResult: function () {
                resultsRepositort.setNewResult(this.questions);
            },
            checkForCorrectness: function (questionId) {
                var self = this;
                var question = this.questions().find(function (question) {
                    return question.id === questionId;
                });

                if (question.type === 'radio') {
                    checkSingleSelectQuestion(questionId, question.answersList)
                        .then(function (result) {
                            resultsRepository.setNewResult(self.sectionId, questionId, result);
                            alert(result);
                        });
                } else {
                    checkMultipleSelectQuestion(questionId, question.answersList)
                        .then(function (result) {
                            resultsRepository.setNewResult(self.sectionId, questionId, result);
                            alert(result);
                        });
                }
            }
        }
    });