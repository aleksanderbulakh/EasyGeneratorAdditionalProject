define(['preview/models/course', 'preview/models/section', 'preview/models/singleSelectQuestion',
    'preview/models/multipleSelectQuestion', 'preview/models/answer', 'constants/constants'],
    function (Course, Section, SingleSelectQuestion, MultipleSelectQuestion, Answer, constants) {

        return {
            newCourse: function (spec) {
                return new Course(spec);
            },
            newSection: function (spec) {
                return new Section(spec);
            },
            newSingleSelectQuestion: function (spec) {
                return new SingleSelectQuestion(spec);
            },
            newMultipleSelectQuestion: function (spec) {
                return new MultipleSelectQuestion(spec);
            },
            newAnswer: function (spec) {
                return new Answer(spec);
            }
        };
    });