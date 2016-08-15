define(['models/course', 'models/section', 'models/question', 'models/answer', 'models/result',
    'models/coursePreview', 'models/sectionPreview', 'models/singleSelectQuestionPreview',
    'models/multipleSelectQuestionPreview', 'models/answerPreview', 'constants/constants'],
    function (Course, Section, Question, Answer, Result, CoursePreview, SectionPreview, SingleSelectQuestionPreview,
        MultipleSelectQuestionPreview, AnswerPreview, constants) {

        return {
            newCourse: function (spec) {
                return new Course(spec);
            },
            newCoursePreview: function(spec){
                return new CoursePreview(spec);
            },
            newSection: function (spec) {
                return new Section(spec);
            },
            newSectionPreview: function (spec) {
                return new SectionPreview(spec);
            },
            newQuestion: function (spec) {
                return new Question(spec);
            },
            newSingleSelectQuestionPreview: function (spec) {
                return new SingleSelectQuestionPreview(spec);
            },
            newMultipleSelectQuestionPreview: function (spec) {
                return new MultipleSelectQuestionPreview(spec);
            },
            newAnswer: function (spec) {
                return new Answer(spec);
            },
            newAnswerPreview: function (spec) {
                return new AnswerPreview(spec);
            },
            newResult: function (spec) {
                return new Result(spec);
            }
        };
    });