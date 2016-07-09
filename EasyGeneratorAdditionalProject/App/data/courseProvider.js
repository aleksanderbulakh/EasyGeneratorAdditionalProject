define(['data/courseContext'], function (courseContext) {
    return {
        getCourseList: function () {
            return courseContext.courseList;
        }
    }
});