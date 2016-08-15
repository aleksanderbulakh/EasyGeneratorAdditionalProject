define(['models/modelFactory', 'constants/constants'],
    function (modelFactory, constants) {
        return {

            mapCourse: function (spec) {
                return modelFactory.newCourse({
                    id: spec.Id,
                    title: spec.Title,
                    description: spec.Description,
                    createdOn: new Date(spec.CreatedOn),
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    lastModified: new Date(spec.LastModifiedDate)
                });
            },

            mapCoursePreview: function (spec) {
                return modelFactory.newCoursePreview({
                    id: spec.id,
                    title: spec.title,
                    description: spec.description,
                    createdBy: spec.createdBy,
                    sectionList: spec.sectionList
                });
            },

            mapSection: function (spec, courseId) {
                return modelFactory.newSection({
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
                return modelFactory.newSectionPreview({
                    id: spec.id,
                    title: spec.title,
                    questionList: spec.questionList
                });
            },

            mapQuestion: function (spec, sectionId) {
                return modelFactory.newQuestion({
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
                return modelFactory.newSingleSelectQuestionPreview({
                    id: spec.id,
                    title: spec.title,
                    type: constants.VIEWS_ANSWER_TYPES[spec.type],
                    answersList: spec.answersList,
                    selectedAnswer: undefined
                });
            },

            mapMultipleSelectQuestionPreview: function (spec) {
                return modelFactory.newMultipleSelectQuestionPreview({
                    id: spec.id,
                    title: spec.title,
                    type: constants.VIEWS_ANSWER_TYPES[spec.type],
                    answersList: spec.answersList,
                    selectedAnswers: []
                });
            },

            mapAnswer: function (spec, questionId) {
                return modelFactory.newAnswer({
                    id: spec.Id,
                    questionId: questionId,
                    text: spec.Text,
                    isCorrect: spec.IsCorrect
                });
            },

            mapAnswerPreview: function (spec) {
                return modelFactory.newAnswerPreview({
                    id: spec.id,
                    text: spec.text,
                    isCorrect: spec.isCorrect
                });
            },

            mapResult: function (sectionId, questionId, result) {
                return modelFactory.newResult({
                    sectionId: sectionId,
                    questionId: questionId,
                    result: result
                });
            }
        };
    });