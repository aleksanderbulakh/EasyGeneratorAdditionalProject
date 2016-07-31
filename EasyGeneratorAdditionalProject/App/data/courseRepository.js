define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage'],
    function (mapper, http, courseContext, message) {
        return {
            getCourseList: function () {
                if (courseContext.courseList !== undefined)
                    return Q(courseContext.courseList);

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
                var course = courseContext.courseList.find(function (item) {
                    return item.id === courseId;
                });

                if (course === undefined) {
                    throw 'Course not found.';
                }

                return Q(course);
            },

            editCourseTitle: function (courseId, courseTitle) {
                return http.post('course/edit/title', { courseId: courseId, userId: courseContext.user.id, title: courseTitle })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (item) {
                            return item.id === courseId;
                        });

                        if (course === undefined) {
                            throw 'Course not found.';
                        }

                        course.title = courseTitle;
                        course.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        course.lastModified = new Date(result);

                        return true;
                    });
            },

            editCourseDescription: function (courseId, courseDescription) {
                return http.post('course/edit/description', { courseId: courseId, userId: courseContext.user.id, description: courseDescription })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (item) {
                            return item.id === courseId;
                        });

                        if (course === undefined) {
                            throw 'Course not found.';
                        }

                        course.description = courseDescription;
                        course.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        course.lastModified = new Date(result);

                        return true;
                    });
            },

            deleteCourse: function (courseId) {
                return http.post('course/delete', { courseId: courseId})
                    .then(function (result) {

                        var courseIndex = courseContext.courseList.findIndex(function (course) {
                            return courseId === course.id;
                        });

                        if (courseIndex === undefined)
                            throw "Course not found.";

                        courseContext.courseList.splice(courseIndex, 1);

                        return true;
                    });
            }
        };
    });