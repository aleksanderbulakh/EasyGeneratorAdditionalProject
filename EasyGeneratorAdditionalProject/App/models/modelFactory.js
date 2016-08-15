define(['models/course', 'models/section', 'models/question', 'models/answer', 'constants/constants'],
    function (Course, Section, Question, Answer, constants) {

        return {
            newCourse: function (spec) {
                return new Course(spec);
            },
            newSection: function (spec) {
                return new Section(spec);
            },
            newQuestion: function (spec) {
                return new Question(spec);
            },
            newAnswer: function (spec) {
                return new Answer(spec);
            }
        };
    });