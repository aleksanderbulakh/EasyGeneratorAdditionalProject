define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessages/customMessage',
    'services/findService', 'services/validateService'],
    function (mapper, http, courseContext, message, findService, validateService) {
        return {
            getAnswersByQuestionId: function (courseId, sectionId, questionId) {
                var question = findService.findQuestion(courseId, sectionId, questionId);

                validateService.throwIfObjectUndefined(question, 'Question');

                if (question.answersList != undefined) {
                    return Q(question.answersList);
                }

                return http.get('answer/list', { questionId: questionId })
                    .then(function (result) {

                        question.answersList = [];

                        result.forEach(function (answer) {
                            question.answersList.push(mapper.mapAnswer(answer));
                        });

                        return Q(question.answersList);
                    });
            },

            createAnswer: function (courseId, sectionId, questionId) {
                return http.post('answer/simple/create', { questionId: questionId, userId: courseContext.user.id })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfObjectUndefined(question, 'Question');

                        var answer = mapper.mapAnswer(result);

                        question.answersList.push(answer);

                        return answer;
                    });
            },

            editAnswerText: function (courseId, sectionId, questionId, answerId, answerText) {
                return http.post('answer/simple/edit/text', { answerId: answerId, userId: courseContext.user.id, text: answerText })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfObjectUndefined(question, 'Question');

                        var answer = findService.findAnswer(courseId, sectionId, questionId, answerId);

                        validateService.throwIfObjectUndefined(answer, 'Answer');

                        answer.text = answerText;
                        question.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        question.lastModifiedDate = new Date(result);

                        return true;
                    });
            },

            editAnswerState: function (courseId, sectionId, questionId, answerId, answerState) {
                return http.post('answer/simple/edit/state', { answerId: answerId, userId: courseContext.user.id, state: answerState })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfObjectUndefined(question, 'Question');

                        var answer = findService.findAnswer(courseId, sectionId, questionId, answerId);

                        validateService.throwIfObjectUndefined(answer, 'Answer');

                        answer.isCorrect = answerState;
                        question.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        question.lastModifiedDate = new Date(result);

                        return true;
                    });
            },

            deleteAnswer: function (courseId, sectionId, questionId, answerId) {
                return http.post('answer/simple/delete', { answerId: answerId })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfObjectUndefined(question, 'Question');

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