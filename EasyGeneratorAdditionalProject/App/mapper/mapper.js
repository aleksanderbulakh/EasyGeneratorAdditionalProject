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

            mapAnswer: function (spec, questionId) {
                return modelFactory.newAnswer({
                    id: spec.Id,
                    questionId: questionId,
                    text: spec.Text,
                    isCorrect: spec.IsCorrect
                });
            }
        };
    });