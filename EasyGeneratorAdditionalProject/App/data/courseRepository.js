define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext'],
    function (mapper, http, courseContext) {
        return {
            getCourseList: function () {
                return Q(courseContext.courseList);
            },

            createCourse: function () {
                return http.post('course/create')
                    .then(function (result) {
                        if (typeof result === 'object') {
                            if (result.Success) {
                                courseContext.courseList.push(mapper.mapCourse(result.RequestData));
                                return result.RequestData.Id;
                            }
                        }
                        else alert(result.RequestData);
                    })
                .fail(function (result) {
                    alert(result);
                });
            },

            getCourseById: function (courseId) {
                var course = courseContext.courseList.find(function (item) {
                    return item.id === courseId;
                });

                if (course === undefined)
                    return "Course not found.";

                return Q(course);
            },

            editCourseTitle: function (courseId, courseTitle) {
                return http.post('course/edit/title', { courseId: courseId, title: courseTitle })
                    .then(function (result) {
                        if (typeof result !== "object")
                            return alert(result);

                        if (result.Success) {
                            var course = courseContext.courseList.find(function (item) {
                                return item.id === courseId;
                            });

                            if (course === undefined)
                                return "Course not found.";

                            course.title = courseTitle;
                            course.lastModified = new Date(result.RequestData).toLocaleDateString();

                            return "Title changed.";
                        }

                        return "Title not changed";
                    })
                    .fail(function (result) {
                        alert(result);
                    });
            },

            editCourseDescription: function (courseId, courseDescription) {
                return http.post('course/edit/description', { courseId: courseId, description: courseDescription })
                    .then(function (result) {
                        if (typeof result !== "object")
                            return alert(result);

                        if (result.Success) {
                            var course = courseContext.courseList.find(function (item) {
                                return item.id === courseId;
                            });

                            if (course === undefined)
                                return "Course not found.";

                            course.description = courseDescription;
                            course.lastModified = new Date(result.RequestData).toLocaleDateString();
                        }

                        return "Description changed.";
                    })
                .fail(function (result) {
                    alert(result);
                });
            },

            deleteCourse: function (courseId) {
                return http.post('course/delete', { courseId: courseId })
                    .then(function (result) {
                        if (typeof result !== "object")
                            return alert(result);

                        if (result.Success) {
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

                        return result;
                    })
                .fail(function (result) {
                    alert(result);
                });
            }
        };
    });