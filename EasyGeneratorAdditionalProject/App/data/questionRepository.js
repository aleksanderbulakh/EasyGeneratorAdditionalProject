define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage'],
    function (mapper, http, courseContext, message) {
        return {
            getQuestionsBySectionId: function (courseId, sectionId) {
                var course = courseContext.courseList.find(function (course) {
                    return course.id === courseId;
                });

                if (course === undefined) {
                    throw "Course not found";
                }

                var section = course.sectionList.find(function (section) {
                    return section.id === sectionId;
                });

                if (section === undefined) {
                    throw "Section not found";
                }

                if (section.questionList != undefined) {
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

            createQuestion: function (courseId, sectionId) {
                return http.post('question/create', { sectionId: sectionId, userId: courseContext.user.id })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });

                        if (course === undefined) {
                            throw "Course not found";
                        }

                        var section = course.sectionList.find(function (section) {
                            return section.id === sectionId;
                        });

                        if (section === undefined) {
                            throw "Section not found";
                        }

                        section.questionList.push(mapper.mapQuestion(result));

                        return true;
                    });
            },

            editQuestionTitle: function (courseId, sectionId, questionId, questionTitle) {
                return http.post('question/edit/title', { questionId: questionId, userId: courseContext.user.id, title: questionTitle })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });

                        if (course === undefined) {
                            throw "Course not found";
                        }

                        var section = course.sectionList.find(function (section) {
                            return section.id === sectionId;
                        });

                        if (section === undefined) {
                            throw "Section not found";
                        }

                        var question = section.questionList.find(function (question) {
                            return question.id === questionId;
                        });

                        if (section === undefined) {
                            throw "Question not found";
                        }

                        question.title = questionTitle;
                        question.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        question.lastModifiedDate = new Date(result);

                        return true;
                    });
            },

            deleteSection: function (courseId, sectionId, questionId) {
                return http.post('question/delete', { questionId: questionId })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });

                        if (course === undefined) {
                            throw "Course not found";
                        }

                        var section = course.sectionList.find(function (section) {
                            return section.id === sectionId;
                        });

                        if (section === undefined) {
                            throw "Section not found";
                        }

                        var questionIndex = section.questionList.findIndex(function (question) {
                            return question.id === questionId;
                        });

                        if (sectionIndex >= 0) {
                            course.sectionList.splice(sectionIndex, 1);
                        }

                        return true;
                    });
            }
        };
    });