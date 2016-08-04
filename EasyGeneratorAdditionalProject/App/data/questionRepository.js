define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessages/customMessage',
    'services/findService', 'services/validateService'],
    function (mapper, http, courseContext, message, findService, validateService) {
        return {
            getQuestionsBySectionId: function (courseId, sectionId) {
                
                var section = findService.findSection(courseId, sectionId);

                validateService.throwIfObjectUndefined(section, 'Section');

                if (section.questionList !== undefined) {
                    return Q(section.questionList);
                }

                return http.get('question/list', { sectionId: sectionId })
                    .then(function (result) {

                        section.questionList = [];

                        result.forEach(function (question) {
                            section.questionList.push(mapper.mapQuestion(question));
                        });

                        return section.questionList;
                    });
            },

            createQuestion: function (courseId, sectionId, questionType) {

                return http.post('question/create', { sectionId: sectionId, type: questionType, userId: courseContext.user.id })
                    .then(function (result) {

                        var section = findService.findSection(courseId, sectionId);

                        validateService.throwIfObjectUndefined(section, 'Section');

                        var question = mapper.mapQuestion(result);

                        section.questionList.push(question);

                        return question;
                    });
            },

            editQuestionTitle: function (courseId, sectionId, questionId, questionTitle) {

                return http.post('question/edit/title', { questionId: questionId, userId: courseContext.user.id, title: questionTitle })
                    .then(function (result) {

                        var question = findService.findQuestion(courseId, sectionId, questionId);

                        validateService.throwIfObjectUndefined(question, 'Question');

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

                        validateService.throwIfObjectUndefined(section, 'Section');

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