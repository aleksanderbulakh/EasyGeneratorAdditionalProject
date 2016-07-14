define(['data/courseProvider'], function (courseProvider) {
    return {
        courseMapById: function (courseId) {
            var selectCourse = {};
            var courseList = courseProvider.getCourseList();
            courseList.forEach(function (course) {
                if (courseId === course.id)
                    selectCourse = course;
            });
            return selectCourse;
        }
    }
});