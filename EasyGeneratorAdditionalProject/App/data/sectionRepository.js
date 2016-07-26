define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext'],
    function (mapper, http, courseContext) {
        return {
            getSectionByCourseId: function (courseId) {
                return http.get('section/list', { courseId: courseId }).then(function (result) {
                    if (typeof result !== 'object')
                        alert(result);

                    if (result.Success) {
                        var course = courseContext.courseList.find(function (course) {
                            return course.id === courseId;
                        });

                        if (course === undefined)
                            return "Course not found";

                        var self = this;

                        result.RequestData.forEach(function (section) {
                            course.sectionList.push(mapper.mapSection(section));
                        });
                    }
                });
            },

            createSection: function (courseId) {
                return http.post('section/create', { courseId: courseId }).then(function (result) {
                    if (typeof result === 'object') {
                        if (result.Success) {
                            var course = courseContext.courseList.find(function (course) {
                                return course.id === courseId;
                            });

                            if (course === undefined)
                                return "Course not found";

                            course.sectionList.push(mapper.mapSection(result.RequestData));

                            return result;
                        }
                    }
                    else alert(result.RequestData);
                });
            },

            editSectionTitle: function (sectionId, sectionTitle) {
                return http.post('section/edit/title', { sectionId: sectionId, title: sectionTitle }).then(function (result) {
                    if (typeof result !== "object")
                        return alert(result);

                    if (result.Success) {
                        var resultMessage = "Section is not found";
                        courseContext.courseList.forEach(function (course) {
                            var section = course.sectionList.find(function (section) {
                                return section.id == sectionId;
                            });

                            if (section !== undefined) {
                                section.title = sectionTitle;
                                section.lastModifiedDate = new Date(result.RequestData).toLocaleDateString();
                                result.RequestData = "Section title is changed.";
                             }
                        });

                        return result;
                    }

                    return "Title not changed.";
                });
            },

            deleteSection: function (sectionId) {
                return http.post('section/delete', { sectionId: sectionId })
                    .then(function (result) {
                        if (typeof result !== "object")
                            return alert(result);

                        if (result.Success) {
                            courseContext.courseList.forEach(function (course) {
                                var elementId = 0;
                                for (var i = 0; i < course.sectionList.length; i++) {
                                    if (course.sectionList[i].id !== sectionId)
                                        continue;

                                    elementId = i;
                                    break;
                                }

                                course.sectionList.splice(elementId, 1);
                            });                            
                        }

                        return result;
                    })
                .fail(function (result) {
                    alert(result);
                });
            }
        };
    });