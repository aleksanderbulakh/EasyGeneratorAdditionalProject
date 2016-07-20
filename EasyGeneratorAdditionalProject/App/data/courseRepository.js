define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext'],
    function (mapper, http, courseContext) {
        return {
            getCourseList: function () {
                return Q(courseContext.courseList);
            },

            createCourse: function () {
                return Q(http.post('course/create').then(function (course) {
                    courseContext.courseList.push(mapper.mapCourse(course));
                    return course.Id;
                }));
            },

            getCourseById: function (courseId) {
                var course = courseContext.courseList.find(function (item) {
                    if (item.id === courseId)
                        return item.id = courseId;
                });
                return Q(course);
            },

            editCourseTitle: function (courseId, courseTitle) {
                return http.post('course/edit/title', { courseId: courseId, title: courseTitle }).then(function (result) {
                    if (result) {
                        var course = courseContext.courseList.find(function (item) {
                            if (item.id === courseId)
                                return item.id = courseId;
                        });

                        course.title = courseTitle;
                        course.lastModified = new Date().toDateString();
                    }

                    return Q(result);
                });
            },

            editCourseDescription: function (courseId, courseDescription) {
                return http.post('course/edit/description', { courseId: courseId, description: courseDescription }).then(function (result) {
                    if (result) {
                        var course = courseContext.courseList.find(function (item) {
                            if (item.id === courseId)
                                return item.id = courseId;
                        });

                        course.description = courseDescription;
                        course.lastModified = new Date().toDateString();
                    }

                    return Q(result);
                });
            },

            deleteCourse: function (courseId) {
                return http.post('course/delete', { id: courseId }).then(function (result) {
                    if (result) {
                        var course = courseContext.courseList;
                        var elementId = 0;
                        for (var i = 0; i < course.length; i++) {
                            if (course[i].id !== courseId)
                                continue;

                            elementId = i;
                            break;
                        }

                        courseContext.courseList.splice(elementId, 1);
                    }

                    return Q(result);
                });
            }
        }
    });