define(['mapper/mapper', 'preview/previewContext', 'errorHandler/errorHandler', 'IoC/IoC', 'constants/constants'],
    function (mapper, previewContext, errorHandler, IoC, constants) {

        function getSections(course) {

            return IoC.sectionRepository.getSectionsByCourseId(course.id)
                .then(function (sectionsList) {

                    var lastSection = _.last(sectionsList);
                    var mapSectionsList = [];
                    var defer = Q.defer();

                    _.each(sectionsList, function (section) {

                        getQuestions(section)
                            .then(function (mapSection) {
                                mapSectionsList.push(mapSection);

                                if (lastSection.id === mapSection.id) {
                                    defer.resolve(mapper.mapCoursePreview({
                                        id: course.id,
                                        title: course.title,
                                        description: course.description,
                                        createdBy: course.createdBy,
                                        sectionList: mapSectionsList
                                    }))
                                }
                            });
                    });

                    return defer.promise;
                });
        }

        function getQuestions(section) {
            return IoC.questionRepository.getQuestionsBySectionId(section.id)
                .then(function (questionsList) {

                    var lastQuestion = _.last(questionsList);
                    var mapQuestionList = [];
                    var defer = Q.defer();

                    _.each(questionsList, function (question) {
                        getAnswers(question)
                            .then(function (mapQuestion) {
                                mapQuestionList.push(mapQuestion);

                                if (lastQuestion.id === mapQuestion.id) {
                                    defer.resolve(mapper.mapSectionPreview({
                                        id: section.id,
                                        title: section.title,
                                        questionList: mapQuestionList
                                    }));
                                }
                            });
                    });

                    return defer.promise;                    
                });
        }

        function getAnswers(question) {
            var defer = Q.defer();
            IoC.answerRepository.getAnswersByQuestionId(question.id)
                .then(function (answersList) {

                    var mapAnswersList = _.map(answersList, function (answer) {
                        return mapper.mapAnswerPreview(answer);
                    });

                    if (question.type === constants.SINGLE_SELECT_QUESTION_TYPE) {

                        defer.resolve(mapper.mapSingleSelectQuestionPreview({
                            id: question.id,
                            title: question.title,
                            type: question.type,
                            answersList: mapAnswersList
                        }));

                    } else if (question.type === constants.MULTIPLE_SELECT_QUESTION_TYPE) {

                        defer.resolve(mapper.mapMultipleSelectQuestionPreview({
                            id: question.id,
                            title: question.title,
                            type: question.type,
                            answersList: mapAnswersList
                        }));
                    }
                });
            return defer.promise;
        }

        return {
            getCourseById: function (courseId) {

                if (previewContext.course !== undefined) {
                    if (previewContext.course.id === courseId) {
                        return Q.fcall(function () {
                            return previewContext.course;
                        });
                    }
                }

                return IoC.courseRepository.getCourseById(courseId)
                    .then(function (course) {

                        return getSections(course)
                            .then(function (mapCourse) {
                                previewContext.course = mapCourse;

                                return mapCourse;
                            });
                    });
            },

            getQuestionsBySectionId: function (sectionId) {
                var section = _.find(previewContext.course.sectionList, function (section) {
                    return section.id === sectionId;
                })

                if (section === undefined) {
                    throw "AAAAAAAAAAAAAAAA";
                }

                return Q.fcall(function () {
                    return section.questionList;
                });
            }
        };
    });