define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage',
    'services/findService', 'services/validateService'],
    function (mapper, http, courseContext, message, findService, validateService) {
        return {
            getAnswersByQuestionId: function (courseId, sectionId, questionId) {
                var question = findService.findQuestion(courseId, sectionId, questionId);

                validateService.throwIfQuestionUndefined(question);

                if (question.answersList != undefined) {
                    return Q(question.answersList);
                }

                return http.get('answer/list', { questionId: questionId })
                    .then(function (result) {

                        var self = this;
                        question.answersList = [];

                        result.forEach(function (answer) {
                            question.answersList.push(mapper.mapAnswer(answer));
                        });

                        return question.answersList;
                    });
            },

            createAnswer: function (courseId, sectionId, questionId) {
                return http.post('answer/create', { questionId: questionId, userId: courseContext.user.id })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfQuestionUndefined(question);

                        question.answersList.push(mapper.mapAnswer(result));

                        return true;
                    });
            },

            editAnswerText: function (courseId, sectionId, questionId, answerId, answerText) {
                return http.post('answer/edit/text', { answerId: answerId, userId: courseContext.user.id, text: answerText })
                    .then(function (result) {

                        var question = findService.fintQuestion(courseId, sectionId, questionId);

                        validateService.throwIfQuestionUndefined(question);

                        var answer = findService.findAnswer(courseId, sectionId, questionId, answerId);

                        validateService.throwIfAnswerUndefined(answer);

                        answer.text = answerText;
                        question.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        question.lastModifiedDate = new Date(result);

                        return true;
                    });
            },

            editAnswerState: function (courseId, sectionId, questionId, answerId, answerState) {
                return http.post('answer/edit/state', { answerId: answerId, userId: courseContext.user.id, state: answerState })
                    .then(function (result) {

                        var question = findService.fintQuestion(courseId, sectionId, questionId);

                        validateService.throwIfQuestionUndefined(question);

                        var answer = findService.findAnswer(courseId, sectionId, questionId, answerId);

                        validateService.throwIfAnswerUndefined(answer);

                        answer.isCorrect = answerState;
                        question.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        question.lastModifiedDate = new Date(result);

                        return true;
                    });
            },

            deleteAnswer: function (courseId, sectionId, questionId, answerId) {
                return http.post('question/delete', { questionId: questionId })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfQuestionUndefined(question);

                        var answerIndex = question.answersList.findIndex(function (answer) {
                            return answer.id === answerId;
                        });

                        if (answerIndex >= 0) {
                            question.answersList.splice(answerIndex, 1);
                        }

                        return true;
                    });
            }
        };
    });