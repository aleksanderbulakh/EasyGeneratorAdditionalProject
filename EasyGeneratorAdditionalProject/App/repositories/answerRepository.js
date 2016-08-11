define(['mapper/mapper', 'http/httpWrapper', 'errorHandler/errorHandler', 'context/answerContext',
    'repositories/questionRepository', 'services/validateService', 'constants/constants'],
    function (mapper, http, errorHandler, answerContext, questionRepository, validateService, constants) {

        function editSingleAnswerState(questionId, answerId) {
            return http.post('answer/single/edit/state', { questionId: questionId, answerId: answerId })
                    .then(function (result) {

                        var answers = answerContext.answerList.filter(function (answer) {
                            return answer.questionId === questionId;
                        });

                        validateService.throwIfObjectIsUndefined(answers, constants.MODELS_NAMES.ANSWER);

                        answers.forEach(function (answer) {
                            if (answer.id === answerId) {
                                answer.isCorrect = true;
                            } else {
                                answer.isCorrect = false;
                            }
                        });

                        var newDate = new Date(result);

                        questionRepository.modify(questionId, newDate);

                        return newDate;
                    });
        }

        function editMultipleAnswerState(questionId, answerId, state) {
            return http.post('answer/multiple/edit/state', { questionId: questionId, answerId: answerId, state: state })
                    .then(function (result) {

                        var answer = answerContext.answerList.find(function (answer) {
                            return answer.id === answerId;
                        });

                        validateService.throwIfObjectIsUndefined(answer, constants.MODELS_NAMES.ANSWER);

                        answer.isCorrect = state;

                        var newDate = new Date(result);

                        questionRepository.modify(questionId, newDate);

                        return newDate;
                    });
        }

        return {
            getAnswersByQuestionId: function (questionId) {

                if (answerContext.answerList !== undefined) {

                    var answers = answerContext.answerList.filter(function (answer) {
                        return answer.questionId === questionId;
                    });

                    if (answers.length !== 0) {
                        return Q.fcall(function () {
                            return answers;
                        });
                    }
                }

                return http.get('answer/list', { questionId: questionId })
                    .then(function (result) {

                        if (answerContext.answerList === undefined) { 
                            answerContext.answerList = [];
                        }

                        result.forEach(function (answer) {
                            answerContext.answerList.push(mapper.mapAnswer(answer, questionId));
                        });

                        var answers = answerContext.answerList.filter(function (answer) {
                            return answer.questionId === questionId;
                        });

                        return  answers;
                        
                    });
            },

            createAnswer: function (questionId) {
                return http.post('answer/create', { questionId: questionId })
                    .then(function (result) {

                        var answer = mapper.mapAnswer(result, questionId);

                        answerContext.answerList.push(answer);

                        return answer;
                    });
            },

            editAnswerText: function (questionId, answerId, answerText) {

                return http.post('answer/edit/text', { answerId: answerId, text: answerText })
                    .then(function (result) {

                        var answer = answerContext.answerList.find(function (answer) {
                            return answer.id === answerId;
                        });

                        validateService.throwIfObjectIsUndefined(answer, constants.MODELS_NAMES.ANSWER);

                        answer.text = answerText;

                        var newDate = new Date(result);

                        questionRepository.modify(questionId, newDate);

                        return newDate;
                    });
            },

            editAnswerState: function (questionId, answerId, type, state) {

                if (type === constants.QUESTION_TYPE_RADIO) {
                    return editSingleAnswerState(questionId, answerId);
                } else if (type === constants.QUESTION_TYPE_CHECKBOX) {
                    return editMultipleAnswerState(questionId, answerId, state);
                }
            },

            deleteAnswer: function (answerId) {
                return http.post('answer/delete', { answerId: answerId })
                    .then(function (result) {

                        var answerIndex = answerContext.answerList.findIndex(function (answer) {
                            return answer.id === answerId;
                        });

                        if (answerIndex >= 0) {
                            answerContext.answerList.splice(answerIndex, 1);
                        }

                        return true;
                    });
            }
        };
    });