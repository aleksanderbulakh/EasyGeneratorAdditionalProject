define(['knockout', 'models/user', 'models/course', 'models/section', 'models/question', 'models/answer'],
    function (ko, User, Course, Section, Question, Answer) {
        return {
            mapUser: function (spec) {
                return new User({
                    id: spec.Id,
                    firstName: spec.FirstName,
                    surname: spec.Surname
                });
            },

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

            mapSection: function (spec) {
                return new Section({
                    id: spec.Id,
                    title: spec.Title,
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    createdOn: new Date(spec.CreatedOn),
                    lastModified: new Date(spec.LastModifiedDate)
                });
            },

            mapQuestion: function (spec) {
                return new Question({
                    id: spec.Id,
                    title: spec.Title,
                    createdBy: spec.CreatedBy,
                    modifiedBy: spec.ModifiedBy,
                    createdOn: new Date(spec.CreatedOn),
                    lastModified: new Date(spec.LastModifiedDate),
                    type: spec.Type
                });
            },

            mapAnswer: function (spec) {
                return new Answer({
                    id: spec.Id,
                    text: spec.Text,
                    isCorrect: spec.IsCorrect
                });
            },

            mapCourseToView: function (spec) {
                return {
                    id: spec.id,
                    title: spec.title,
                    description: spec.description
                };
            },

            mapSectionToView: function (spec) {
                return {
                    id: spec.id,
                    title: spec.title,
                    createdOn: spec.createdOn,
                    lastModifiedDate: spec.lastModifiedDate,
                    createdBy: spec.createdBy,
                    modifiedBy: spec.modifiedBy
                };
            },

            mapQuestionToView: function (spec) {
                return {
                    id: spec.id,
                    title: spec.title,
                    createdOn: spec.createdOn,
                    lastModifiedDate: spec.lastModifiedDate,
                    createdBy: spec.createdBy,
                    modifiedBy: spec.modifiedBy,
                    type: spec.type
                };
            },

            mapAnswerToView: function (spec) {
                return {
                    id: spec.id,
                    text: ko.observable(spec.text).extend({
                        validName: 'Please, enter course title! Maximum number of characters - 255.'
                    }),
                    isCorrect: ko.observable(spec.isCorrect)
                }
            }
        };
    });