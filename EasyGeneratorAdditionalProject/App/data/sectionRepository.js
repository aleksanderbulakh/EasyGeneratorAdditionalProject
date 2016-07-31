define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage'],
    function (mapper, http, courseContext, message) {
        return {
            getSectionsByCourseId: function (courseId) {
                var course = courseContext.courseList.find(function (course) {
                    return course.id === courseId;
                });

                if (course === undefined) {
                    throw "Course not found";
                }

                if (course.sectionList != undefined) {
                    return Q(true);
                }

                return http.get('section/list', { courseId: courseId })
                    .then(function (result) {

                        var self = this;
                        course.sectionList = [];

                        result.forEach(function (section) {
                            course.sectionList.push(mapper.mapSection(section));
                        });

                        return true;
                    });
            },

            createSection: function (courseId) {
                return http.post('section/create', { courseId: courseId, userId: courseContext.user.id })
                    .then(function (result) {

                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });

                        if (course === undefined) {
                            throw "Course not found";
                        }

                        course.sectionList.push(mapper.mapSection(result));

                        return true;
                    });
            },

            editSectionTitle: function (sectionId, sectionTitle) {
                return http.post('section/edit/title', { sectionId: sectionId, userId: courseContext.user.id, title: sectionTitle })
                    .then(function (result) {

                        courseContext.courseList.forEach(function (course) {
                            if (course.sectionList !== undefined) {
                                var section = course.sectionList.find(function (section) {
                                    return section.id === sectionId;
                                });

                                if (section !== undefined) {
                                    section.title = sectionTitle;
                                    section.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                                    section.lastModifiedDate = new Date(result);
                                }
                            }
                        });

                        return true;
                    });
            },

            deleteSection: function (sectionId) {
                return http.post('section/delete', { sectionId: sectionId})
                    .then(function (result) {

                        courseContext.courseList.forEach(function (course) {
                            if (course.sectionList !== undefined) {
                                var sectionIndex = course.sectionList.findIndex(function (section) {
                                    return sectionId === section.id;
                                });

                                if (sectionIndex >= 0) {
                                    course.sectionList.splice(sectionIndex, 1);
                                }
                            }
                        });

                        return true;
                    });
            }
        };
    });