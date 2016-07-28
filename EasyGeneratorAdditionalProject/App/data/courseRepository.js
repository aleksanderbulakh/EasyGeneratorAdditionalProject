define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage'],
    function (mapper, http, courseContext, message) {
        return {
            getCourseList: function () {
                if (courseContext.courseList !== undefined)
                    return Q(courseContext.courseList);

                return http.get('course/list', { userId: courseContext.user.id, parameterType: "userId" })
                    .then(function (result) {
                        if (typeof result !== "object") {
                            message.stateMessage(result, "Error");
                        }

                        courseContext.courseList = [];

                        result.forEach(function (course) {
                            courseContext.courseList.push(mapper.mapCourse(course));
                        });

                        return courseContext.courseList;
                    })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });
            },

            createCourse: function (courseTitle) {
                return http.post('course/create', { userId: courseContext.user.id, courseTitle: courseTitle, parameterType: "userId" })
                    .then(function (result) {
                        if (typeof result === 'object') {
                            courseContext.courseList.push(mapper.mapCourse(result));
                            return result.Id;
                        }
                        message.stateMessage(result, "Error");
                    })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });
            },

            getCourseById: function (courseId) {
                var course = courseContext.courseList.find(function (item) {
                    return item.id === courseId;
                });

                if (course === undefined) {
                    return "Course not found.";
                }

                return Q(course);
            },

            editCourseTitle: function (courseId, courseTitle) {
                return http.post('course/edit/title', { courseId: courseId, userId: courseContext.user.id, title: courseTitle, parameterType: 'courseId' })
                    .then(function (result) {
                        if (typeof result === "string") {
                            message.stateMessage(result, "Error");
                        }

                        var course = courseContext.courseList.find(function (item) {
                            return item.id === courseId;
                        });

                        if (course === undefined) {
                            return "Course not found.";
                        }

                        course.title = courseTitle;
                        course.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        course.lastModified = new Date(result);

                        return "Title changed.";
                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });
            },

            editCourseDescription: function (courseId, courseDescription) {
                return http.post('course/edit/description', { courseId: courseId, userId: courseContext.user.id, description: courseDescription, parameterType: 'courseId' })
                    .then(function (result) {
                        if (typeof result === "string") {
                            message.stateMessage(result, "Error");
                        }

                        var course = courseContext.courseList.find(function (item) {
                            return item.id === courseId;
                        });

                        if (course === undefined) {
                            return "Course not found.";
                        }

                        course.description = courseDescription;
                        course.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        course.lastModified = new Date(result);

                        return "Description changed.";
                    })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });
            },

            deleteCourse: function (courseId) {
                return http.post('course/delete', { courseId: courseId, parameterType: 'courseId' })
                    .then(function (result) {
                        if (typeof result === "string") {
                            message.stateMessage(result, "Error");
                        }

                        if (result) {
                            var course = courseContext.courseList;
                            var elementId = 0;
                            for (var i = 0; i < course.length; i++) {
                                if (course[i].id !== courseId) {
                                    continue;
                                }

                                elementId = i;
                                break;
                            }

                            courseContext.courseList.splice(elementId, 1);
                        }

                        return result;
                    })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });
            }
        };
    });