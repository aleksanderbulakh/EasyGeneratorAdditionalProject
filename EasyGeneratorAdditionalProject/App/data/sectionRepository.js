define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext'],
    function (mapper, http, courseContext) {
        return {
            getSectionByCourseId: function (courseId) {
                var course = courseContext.courseList.find(function (course) {
                    return course.id === courseId;
                });

                if (course === undefined) {
                    return "Course not found";
                }
                if (course.sectionList != undefined) {
                    return Q(true);
                }
                else {
                    return http.get('section/list', { courseId: courseId, parameterType: 'courseId' })
                        .then(function (result) {
                            if (typeof result != 'object') {
                                alert(result);
                            }

                            var self = this;
                            course.sectionList = [];

                            result.forEach(function (section) {
                                course.sectionList.push(mapper.mapSection(section));
                            });
                        })
                    .fail(function (result) {
                        alert(result);
                    });
                }
            },

            createSection: function (courseId) {
                return http.post('section/create', { courseId: courseId, userId: courseContext.user.id, parameterType: 'courseId' })
                    .then(function (result) {
                        if (typeof result != 'object') {
                            return result;
                        }

                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });

                        if (course === undefined) {
                            return "Course not found";
                        }

                        course.sectionList.push(mapper.mapSection(result));

                        return true;
                    })
                .fail(function (result) {
                    alert(result);
                });
            },

            editSectionTitle: function (sectionId, sectionTitle) {
                return http.post('section/edit/title', { sectionId: sectionId, userId: courseContext.user.id, title: sectionTitle, parameterType: 'sectionId' })
                    .then(function (result) {
                        if (typeof result == "string") {
                            return alert(result);
                        }

                        courseContext.courseList.forEach(function (course) {
                            if (course.sectionList != undefined) {
                                var section = course.sectionList.find(function (section) {
                                    return section.id === sectionId;
                                });

                                if (section !== undefined) {
                                    section.title = sectionTitle;
                                    section.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                                    section.lastModifiedDate = new Date(result).toLocaleDateString();
                                }
                            }
                        });

                        return "Title changed.";
                    })
                    .fail(function (result) {
                        alert(result);
                    });
            },

            deleteSection: function (sectionId) {
                return http.post('section/delete', { sectionId: sectionId, parameterType: 'sectionId' })
                    .then(function (result) {
                        if (typeof result == "string") {
                            return alert(result);
                        }

                        courseContext.courseList.forEach(function (course) {
                            var elementId = 0;
                            var find = false;
                            if (course.sectionList != undefined) {
                                for (var i = 0; i < course.sectionList.length; i++) {
                                    if (course.sectionList[i].id !== sectionId) {
                                        continue;
                                    }

                                    elementId = i;
                                    find = true;
                                    break;
                                }

                                if (find) {
                                    course.sectionList.splice(elementId, 1);
                                }
                            }
                        });

                        return true;
                    })
                .fail(function (result) {
                    alert(result);
                });
            }
        };
    });