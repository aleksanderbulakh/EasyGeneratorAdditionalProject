define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage',
    'services/findService', 'services/validateService'],
    function (mapper, http, courseContext, message, findService, validateService) {
        return {
            getCourseList: function () {
                
                if (courseContext.courseList !== undefined) { 
                    return Q(courseContext.courseList);
                }

                return http.get('course/list', { userId: courseContext.user.id })
                    .then(function (result) {

                        courseContext.courseList = [];

                        result.forEach(function (course) {
                            courseContext.courseList.push(mapper.mapCourse(course));
                        });

                        return courseContext.courseList;
                    });
            },

            createCourse: function (courseTitle) {
                return http.post('course/create', { userId: courseContext.user.id, courseTitle: courseTitle })
                    .then(function (result) {
                        courseContext.courseList.push(mapper.mapCourse(result));
                        return result.Id;
                    });
            },

            getCourseById: function (courseId) {
                var course = findService.findCourse(courseId);

                validateService.throwIfCourseUndefined(course);

                return Q(course);
            },

            editCourseTitle: function (courseId, courseTitle) {
                return http.post('course/edit/title', { courseId: courseId, userId: courseContext.user.id, title: courseTitle })
                    .then(function (result) {

                        var course = findService.findCourse(courseId);

                        validateService.throwIfCourseUndefined(course);

                        course.title = courseTitle;
                        course.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        course.lastModified = new Date(result);

                        return true;
                    });
            },

            editCourseDescription: function (courseId, courseDescription) {
                return http.post('course/edit/description', { courseId: courseId, userId: courseContext.user.id, description: courseDescription })
                    .then(function (result) {

                        var course = findService.findCourse(courseId);

                        validateService.throwIfCourseUndefined(course);

                        course.description = courseDescription;
                        course.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        course.lastModified = new Date(result);

                        return true;
                    });
            },

            deleteCourse: function (courseId) {
                return http.post('course/delete', { courseId: courseId })
                    .then(function (result) {

                        var courseIndex = courseContext.courseList.findIndex(function (course) {
                            return courseId === course.id;
                        });

                        if (courseIndex >= 0)
                            courseContext.courseList.splice(courseIndex, 1);

                        return true;
                    });
            }
        };
    });