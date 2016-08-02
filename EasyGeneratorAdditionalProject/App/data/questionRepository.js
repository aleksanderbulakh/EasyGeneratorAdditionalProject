define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage',
    'services/findService', 'services/validateService'],
    function (mapper, http, courseContext, message, findService, validateService) {
        return {
            getQuestionsBySectionId: function (courseId, sectionId) {
                
                var section = findService.findSection(courseId, sectionId);

                validateService.throwIfSectionUndefined(section);

                if (section.questionList !== undefined) {
                    return Q(true);
                }

                return http.get('question/list', { sectionId: sectionId })
                    .then(function (result) {

                        var self = this;
                        section.questionList = [];

                        result.forEach(function (question) {
                            section.questionList.push(mapper.mapQuestion(question));
                        });

                        return true;
                    });
            },

            createQuestion: function (courseId, sectionId, questionType) {

                return http.post('question/create', { sectionId: sectionId, type: questionType, userId: courseContext.user.id })
                    .then(function (result) {

                        var section = findService.findSection(courseId, sectionId);

                        validateService.throwIfSectionUndefined(section);

                        section.questionList.push(mapper.mapQuestion(result));

                        return true;
                    });
            },

            editQuestionTitle: function (courseId, sectionId, questionId, questionTitle) {

                return http.post('question/edit/title', { questionId: questionId, userId: courseContext.user.id, title: questionTitle })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfQuestionUndefined(question);

                        question.title = questionTitle;
                        question.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        question.lastModifiedDate = new Date(result);

                        return true;
                    });
            },

            deleteQuestion: function (courseId, sectionId, questionId) {
                return http.post('question/delete', { questionId: questionId })
                    .then(function (result) {
                        
                        var section = findService.findSection(courseId, sectionId);

                        validateService.throwIfSectionUndefined(section);

                        var questionIndex = section.questionList.findIndex(function (question) {
                            return question.id === questionId;
                        });

                        if (questionIndex >= 0) {
                            section.questionList.splice(questionIndex, 1);
                        }

                        return true;
                    });
            }
        };
    });