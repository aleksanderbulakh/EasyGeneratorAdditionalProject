define(['mapper/mapper', 'http/httpWrapper', 'context/courseContext', 'errorHandler/errorHandler',
    'services/validateService', 'constants/constants'],
    function (mapper, http, courseContext, errorHandler, validateService, constants) {
        return {
            getCourseList: function () {

                if (courseContext.courseList !== undefined) {
                    return Q.fcall(function () {
                        return courseContext.courseList;
                    });
                }

                return http.get('course/list')
                    .then(function (result) {

                        if (courseContext.courseList === undefined) { 
                            courseContext.courseList = [];
                        }

                        result.forEach(function (course) {
                            courseContext.courseList.push(mapper.mapCourse(course));
                        });

                        return courseContext.courseList;
                    });
            },

            createCourse: function (courseTitle) {
                if (courseTitle === '') {
                    courseTitle = constants.DEFAULT_COURSE_NAME;
                }
                return http.post('course/create', { courseTitle: courseTitle })
                    .then(function (result) {
                        courseContext.courseList.push(mapper.mapCourse(result));
                        return result.Id;
                    });
            },

            getCourseById: function (courseId) {
                var course = courseContext.courseList.find(function (course) {
                    return course.id === courseId;
                });

                validateService.throwIfObjectIsUndefined(course, constants.MODELS_NAMES.COURSE);

                return Q.fcall(function () {
                    return course;
                });
            },

            editCourseTitle: function (courseId, courseTitle) {
                return http.post('course/edit/title', { courseId: courseId, title: courseTitle })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });

                        validateService.throwIfObjectIsUndefined(course, constants.MODELS_NAMES.COURSE);

                        course.title = courseTitle;
                        course.lastModified = new Date(result);

                        return true;
                    });
            },

            editCourseDescription: function (courseId, courseDescription) {
                return http.post('course/edit/description', { courseId: courseId, description: courseDescription })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });;

                        validateService.throwIfObjectIsUndefined(course, constants.MODELS_NAMES.COURSE);

                        course.description = courseDescription;
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