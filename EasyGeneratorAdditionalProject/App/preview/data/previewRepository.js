define(['preview/mapper/mapper', 'preview/data/previewContext', 'IoC/IoC', 'constants/constants',
    'services/validateService'],
    function (mapper, previewContext, IoC, constants, validateService) {

        function getMappedCourse(course) {

            return IoC.sectionRepository.getSectionsByCourseId(course.id)
                .then(function (sectionsList) {

                    if (sectionsList.length === 0) {
                        return mapper.mapCourse({
                            id: course.id,
                            title: course.title,
                            description: course.description,
                            createdBy: course.createdBy,
                            sectionList: []
                        });
                    }

                    var lastSection = _.last(sectionsList);
                    var mapSectionsList = [];
                    var defer = Q.defer();

                    _.each(sectionsList, function (section) {

                        getMappedSection(section)
                            .then(function (mapSection) {
                                mapSectionsList.push(mapSection);

                                if (lastSection.id === mapSection.id) {
                                    defer.resolve(mapper.mapCourse({
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

        function getMappedSection(section) {
            return IoC.questionRepository.getQuestionsBySectionId(section.id)
                .then(function (questionsList) {

                    if (questionsList.length === 0) {
                        return mapper.mapSection({
                            id: section.id,
                            title: section.title,
                            questionList: []
                        })
                    }

                    var lastQuestion = _.last(questionsList);
                    var mapQuestionList = [];
                    var defer = Q.defer();

                    _.each(questionsList, function (question) {
                        getMappedQuestion(question)
                            .then(function (mapQuestion) {
                                mapQuestionList.push(mapQuestion);

                                if (lastQuestion.id === mapQuestion.id) {
                                    defer.resolve(mapper.mapSection({
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

        function getMappedQuestion(question) {
            var defer = Q.defer();
            IoC.answerRepository.getAnswersByQuestionId(question.id)
                .then(function (answersList) {

                    var mapAnswersList = [];

                    if (answersList.length !== 0) {
                        mapAnswersList = _.map(answersList, function (answer) {
                            return mapper.mapAnswer({
                                id: answer.id,
                                text: answer.text,
                                isCorrect: answer.isCorrect,
                                checked: false
                            });
                        });
                    }

                    defer.resolve(mapper.mapQuestion({
                        id: question.id,
                        title: question.title,
                        type: constants.VIEWS_ANSWER_TYPES[question.type],
                        answersList: mapAnswersList,
                        result: 0
                    }));
                });
            return defer.promise;
        }

        return {
            getCourseById: function (courseId) {

                if (!_.isUndefined(previewContext.course)) {
                    if (previewContext.course.id === courseId) {
                        return Q.fcall(function () {
                            previewContext.course.computeProgress();
                            return previewContext.course;
                        });
                    }
                }

                return IoC.courseRepository.getCourseById(courseId)
                    .then(function (course) {

                        return getMappedCourse(course)
                            .then(function (mapCourse) {

                                previewContext.course = mapCourse;

                                previewContext.course.computeProgress();

                                return previewContext.course;
                            });
                    });
            },

            checkAnswer: function (sectionId, questionId, results) {

                var section = _.find(previewContext.course.sectionList, function (section) {
                    return section.id === sectionId;
                });

                validateService.throwIfObjectIsUndefined(section, constants.MODELS_NAMES.SECTION);

                var question = _.find(section.questionList, function (question) {
                    return question.id === questionId;
                });

                validateService.throwIfObjectIsUndefined(question, constants.MODELS_NAMES.QUESTION);

                question.checkAnswer = results;

                if (question.type === constants.QUESTION_TYPE_RADIO) {
                    var currentCorrectAnswer = _.find(question.answersList, function (answer) {
                        return answer.checked;
                    });

                    var newCorrectAnswer = _.find(question.answersList, function (answer) {
                        return answer.id === results;
                    });

                    if (!_.isUndefined(currentCorrectAnswer)) {
                        currentCorrectAnswer.checked = !currentCorrectAnswer.checked;
                    }
                    newCorrectAnswer.checked = !newCorrectAnswer.checked;
                } else if (question.type === constants.QUESTION_TYPE_CHECKBOX) {
                    _.each(question.answersList, function (answer) {
                        if (results.indexOf(answer.id) !== -1) {
                            answer.checked = true;
                        } else {
                            answer.checked = false;
                        }
                    });
                }

                question.checkForCorrectness();

                return question.result;
            },

            getQuestionsBySectionId: function (sectionId) {
                var section = _.find(previewContext.course.sectionList, function (section) {
                    return section.id === sectionId;
                });

                validateService.throwIfObjectIsUndefined(section, constants.MODELS_NAMES.SECTION);

                return Q.fcall(function () {
                    return section.questionList;
                });
            }
        };
    });