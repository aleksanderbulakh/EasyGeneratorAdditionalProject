define(['data/courseRepository'], function (courseRepository) {
    return {
        courseMapById: function (courseId) {
            var selectCourse = {};
            var courseList = courseRepository.getCourseList();
            courseList.forEach(function (course) {
                if (courseId === course.id)
                    selectCourse = course;
            });
            return selectCourse;
        }
    }
});