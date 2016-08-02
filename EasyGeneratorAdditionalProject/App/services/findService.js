define(['data/courseContext', 'services/validateService'],
    function (courseContext, validateService) {
        return {
            findCourse: function (courseId) {
                return courseContext.courseList.find(function (course) {
                    return course.id === courseId;
                });
            },

            findSection: function (courseId, sectionId) {
                var course = this.findCourse(courseId);

                validateService.throwIfCourseUndefined(course);

                return course.sectionList.find(function (section) {
                    return section.id === sectionId;
                });
            },

            findQuestion: function (courseId, sectionId, questionId) {
                var section = this.findSection(courseId, sectionId);

                validateService.throwIfSectionUndefined(section);
                
                return section.questionList.find(function (question) {
                    return question.id === questionId;
                });
            },

            findAnswer: function (courseId, sectionId, questionId, answerId) {
                var question = this.findQuestion(courseId, sectionId, questionId);

                validateService.throwIfQuestionUndefined(question);

                return question.answersList.find(function (answer) {
                    return answer.id === answerId;
                });
            }
        };
    });