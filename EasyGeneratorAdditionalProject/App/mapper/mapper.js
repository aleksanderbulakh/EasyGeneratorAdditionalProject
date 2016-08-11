define(['models/course', 'models/section', 'models/question', 'models/answer', 'models/result', 'models/sectionPreview',
    'models/singleSelectQuestionPreview', 'models/multipleSelectQuestionPreview', 'models/answerPreview',
    'constants/constants'],
    function (Course, Section, Question, Answer, Result, SectionPreview, SingleSelectQuestionPreview,
        MultipleSelectQuestionPreview, AnswerPreview, constants) {
        return {

            mapCourse: function (spec) {
                return new Course({
                    id: spec.Id,
                    title: spec.Title,
                    description: spec.Description,
                    createdOn: new Date(spec.CreatedOn),
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    lastModified: new Date(spec.LastModifiedDate)
                });
            },

            mapSection: function (spec, courseId) {
                return new Section({
                    id: spec.Id,
                    courseId: courseId,
                    title: spec.Title,
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    createdOn: new Date(spec.CreatedOn),
                    lastModified: new Date(spec.LastModifiedDate)
                });
            },

            mapSectionPreview: function (spec) {
                return new SectionPreview({
                    id: spec.id,
                    title: spec.title
                });
            },

            mapQuestion: function (spec, sectionId) {
                return new Question({
                    id: spec.Id,
                    sectionId: sectionId,
                    title: spec.Title,
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    createdOn: new Date(spec.CreatedOn),
                    lastModified: new Date(spec.LastModifiedDate),
                    type: spec.Type
                });
            },

            mapSingleSelectQuestionPreview: function(spec){
                return new SingleSelectQuestionPreview({
                    id: spec.id,
                    title: spec.title,
                    type: constants.VIEWS_ANSWER_TYPES[spec.type],
                    answersList: spec.answersList
                });
            },

            mapMultipleSelectQuestionPreview: function (spec) {
                return new MultipleSelectQuestionPreview({
                    id: spec.id,
                    title: spec.title,
                    type: constants.VIEWS_ANSWER_TYPES[spec.type],
                    answersList: spec.answersList
                });
            },

            mapAnswer: function (spec, questionId) {
                return new Answer({
                    id: spec.Id,
                    questionId: questionId,
                    text: spec.Text,
                    isCorrect: spec.IsCorrect
                });
            },

            mapAnswerPreview: function(spec){
                return new AnswerPreview({
                    id: spec.id,
                    text: spec.text,
                    isCorrect: false
                });
            },

            mapResult: function (sectionId, questionId, result) {
                return new Result({
                    sectionId: sectionId,
                    questionId: questionId,
                    result: result
                });
            }
        };
    });