define(['models/modelFactory', 'constants/constants'],
    function (modelFactory, constants) {
        return {

            mapCourse: function (spec) {
                return modelFactory.newModel({
                    id: spec.Id,
                    title: spec.Title,
                    description: spec.Description,
                    createdOn: new Date(spec.CreatedOn),
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    lastModified: new Date(spec.LastModifiedDate)
                }, constants.MODELS_NAMES.COURSE);
            },

            mapSection: function (spec, courseId) {
                return modelFactory.newModel({
                    id: spec.Id,
                    courseId: courseId,
                    title: spec.Title,
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    createdOn: new Date(spec.CreatedOn),
                    lastModified: new Date(spec.LastModifiedDate)
                }, constants.MODELS_NAMES.SECTION);
            },

            mapSectionPreview: function (spec) {
                return modelFactory.newModel({
                    id: spec.id,
                    title: spec.title
                }, constants.MODELS_NAMES.SECTION_PREVIEW);
            },

            mapQuestion: function (spec, sectionId) {
                return modelFactory.newModel({
                    id: spec.Id,
                    sectionId: sectionId,
                    title: spec.Title,
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    createdOn: new Date(spec.CreatedOn),
                    lastModified: new Date(spec.LastModifiedDate),
                    type: spec.Type
                }, constants.MODELS_NAMES.QUESTION);
            },

            mapSingleSelectQuestionPreview: function(spec){
                return modelFactory.newModel({
                    id: spec.id,
                    title: spec.title,
                    type: constants.VIEWS_ANSWER_TYPES[spec.type],
                    answersList: spec.answersList
                }, constants.MODELS_NAMES.SINGLE_SELECT_QUESTION_PREVIEW);
            },

            mapMultipleSelectQuestionPreview: function (spec) {
                return modelFactory.newModel({
                    id: spec.id,
                    title: spec.title,
                    type: constants.VIEWS_ANSWER_TYPES[spec.type],
                    answersList: spec.answersList
                }, constants.MODELS_NAMES.MULTIPLE_SELECT_QUESTION_PREVIEW);
            },

            mapAnswer: function (spec, questionId) {
                return modelFactory.newModel({
                    id: spec.Id,
                    questionId: questionId,
                    text: spec.Text,
                    isCorrect: spec.IsCorrect
                }, constants.MODELS_NAMES.ANSWER);
            },

            mapAnswerPreview: function(spec){
                return modelFactory.newModel({
                    id: spec.id,
                    text: spec.text,
                    isCorrect: false
                }, constants.MODELS_NAMES.ANSWER_PREVIEW);
            },

            mapResult: function (sectionId, questionId, result) {
                return modelFactory.newModel({
                    sectionId: sectionId,
                    questionId: questionId,
                    result: result
                }, constants.MODELS_NAMES.RESULT);
            }
        };
    });