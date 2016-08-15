define(['preview/models/modelFactory', 'constants/constants'],
    function (modelFactory, constants) {
        return {

            mapCourse: function (spec) {
                return modelFactory.newCourse({
                    id: spec.id,
                    title: spec.title,
                    description: spec.description,
                    createdBy: spec.createdBy,
                    sectionList: spec.sectionList
                });
            },

            mapSection: function (spec) {
                return modelFactory.newSection({
                    id: spec.id,
                    title: spec.title,
                    questionList: spec.questionList
                });
            },

            mapQuestion: function (spec) {
                if (spec.type === constants.QUESTION_TYPE_RADIO) {
                    return modelFactory.newSingleSelectQuestion({
                        id: spec.id,
                        title: spec.title,
                        type: spec.type,
                        answersList: spec.answersList
                    });
                } else if (spec.type === constants.QUESTION_TYPE_CHECKBOX) {
                    return modelFactory.newMultipleSelectQuestion({
                        id: spec.id,
                        title: spec.title,
                        type: spec.type,
                        answersList: spec.answersList
                    });
                }
            },

            mapAnswer: function (spec) {
                return modelFactory.newAnswer({
                    id: spec.id,
                    text: spec.text,
                    isCorrect: spec.isCorrect,
                    checked: spec.checked
                });
            }
        };
    });