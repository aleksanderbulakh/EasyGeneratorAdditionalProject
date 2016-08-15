define(['mapper/mapper', 'http/httpWrapper', 'errorHandler/errorHandler',
    'context/questionContext', 'services/validateService', 'constants/constants'],
    function (mapper, http, errorHandler, questionContext, validateService, constants) {
        return {
            getQuestionsBySectionId: function (sectionId) {

                if (questionContext.questionList !== undefined) {

                    var questions = _.filter(questionContext.questionList, function (question) {
                        return question.sectionId === sectionId;
                    });


                    if (questions.length !== 0) {
                        return Q.fcall(function () {
                            return questions;
                        });
                    }
                }

                return http.get('question/list', { sectionId: sectionId })
                    .then(function (result) {

                        if (questionContext.questionList === undefined) {
                            questionContext.questionList = [];
                        }

                        _.each(result, function (question) {
                            questionContext.questionList.push(mapper.mapQuestion(question, sectionId));
                        });

                        var questions = _.filter(questionContext.questionList, function (question) {
                            return question.sectionId === sectionId;
                        });

                        return questions;
                    });
            },

            createQuestion: function (sectionId, questionType) {

                return http.post('question/create/simple-question', { sectionId: sectionId, type: questionType })
                    .then(function (result) {

                        var question = mapper.mapQuestion(result, sectionId);

                        questionContext.questionList.push(question);

                        return question;
                    });
            },

            editQuestionTitle: function (questionId, questionTitle) {

                return http.post('question/edit/title', { questionId: questionId, title: questionTitle })
                    .then(function (result) {

                        var question = _.find(questionContext.questionList, function (question) {
                            return question.id === questionId;
                        });

                        validateService.throwIfObjectIsUndefined(question, constants.MODELS_NAMES.QUESTION);

                        question.title = questionTitle;
                        question.lastModifiedDate = new Date(result);

                        return question.lastModifiedDate;
                    });
            },

            deleteQuestion: function (questionId) {
                return http.post('question/delete', { questionId: questionId })
                    .then(function (result) {

                        var questionIndex = questionContext.questionList.findIndex(function (question) {
                            return question.id === questionId;
                        });

                        if (questionIndex >= 0) {
                            questionContext.questionList.splice(questionIndex, 1);
                        }

                        return true;
                    });
            },

            modify: function (questionId, newDate) {

                var question = _.find(questionContext.questionList, function (question) {
                    return question.id === questionId;
                });

                validateService.throwIfObjectIsUndefined(question, constants.MODELS_NAMES.QUESTION);

                question.lastModifiedDate = newDate;
            }
        };
    });