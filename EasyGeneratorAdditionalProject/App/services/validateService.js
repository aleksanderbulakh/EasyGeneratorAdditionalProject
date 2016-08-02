define(function () {
    return {
        throwIfCourseUndefined: function (course) {
            if (course === undefined)
                throw 'Course is not found';
        },

        throwIfSectionUndefined: function (section) {
            if (section === undefined)
                throw 'Section is not found';
        },

        throwIfQuestionUndefined: function (question) {
            if (question === undefined)
                throw 'Question is not found';
        },

        throwIfAnswerUndefined: function (answer) {
            if (answer === undefined)
                throw 'Answer is not found';
        }
    };
});