define(['models/course', 'models/section', 'models/question', 'models/answer', 'models/result'],
    function (Course, Section, Question, Answer, Result) {
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

            mapAnswer: function (spec, questionId) {
                return new Answer({
                    id: spec.Id,
                    questionId: questionId,
                    text: spec.Text,
                    isCorrect: spec.IsCorrect
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