define(['mapper/mapper', 'preview/resultsContext', 'errorHandler/errorHandler', 'repositories/courseRepository',
    'repositories/sectionRepository', 'repositories/questionRepository', 'repositories/answerRepository', 'constants/constants'],
    function (mapper, resultsContext, errorHandler, courseRepository, sectionRepository,
        questionRepository, answerRepository, constants) {
        return {
            getCourseById(courseId) {
                return courseRepository.getCourseById(courseId)
                    .then(function (course) {

                        return sectionRepository.getSectionsByCourseId(courseId)
                            .then(function (sectionList) {

                                var sections = [];

                                sectionList.forEach(function (section) {
                                    return questionRepository.getQuestionsBySectionId(section.id)
                                        .then(function (questionList) {

                                            questions = [];

                                            questionList.forEach(function (question) {
                                                return answerRepository.getAnswersByQuestionId(question.id)
                                                    .then(function (answerList) {
                                                        var answers = answerList.map(function (answer) {
                                                            return {
                                                                id: answer.id,
                                                                questionId: answer.questionId,
                                                                text: answer.text,
                                                                isCorrect: false,
                                                            }
                                                        });

                                                        questions.push({
                                                            id: question.id,
                                                            sectionId: question.sectionId,
                                                            title: question.title,
                                                            type: constants.ANSWER_TYPES[question.type],
                                                            answersList: answers
                                                        });
                                                    });
                                            });

                                            sections.push({
                                                id: section.id,
                                                title: section.title,
                                                createdOn: section.createdOn,
                                                lastModifiedDate: section.lastModified,
                                                createdBy: section.createdBy,
                                                modifiedBy: section.modifiedBy,
                                                courseId: section.courseId,
                                                questionList: questions
                                            });
                                        });
                                });

                                return {
                                    id: course.id,
                                    title: course.title,
                                    createdOn: course.createdOn,
                                    lastModifiedDate: course.lastModified,
                                    createdBy: course.createdBy,
                                    modifiedBy: course.modifiedBy,
                                    description: course.description,
                                    sectionList: sections,
                                }
                            });                        
                    });
            }
        };
    });