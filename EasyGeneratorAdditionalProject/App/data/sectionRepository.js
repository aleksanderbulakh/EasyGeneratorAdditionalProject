define(['mapper/mapper', 'http/httpWrapper', 'data/courseContext', 'customPlugins/customMessage',
    'services/findService', 'services/validateService'],
    function (mapper, http, courseContext, message, findService, validateService) {
        return {
            getSectionsByCourseId: function (courseId) {
                var course = findService.findCourse(courseId);

                validateService.throwIfObjectUndefined(course, 'Course');

                if (course.sectionList != undefined) {
                    return Q(true);
                }

                return http.get('section/list', { courseId: courseId })
                    .then(function (result) {

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

                        var course = findService.findCourse(courseId);

                        validateService.throwIfObjectUndefined(course, 'Course');

                        course.sectionList.push(mapper.mapSection(result));

                        return true;
                    });
            },

            editSectionTitle: function (courseId, sectionId, sectionTitle) {
                return http.post('section/edit/title', { sectionId: sectionId, userId: courseContext.user.id, title: sectionTitle })
                    .then(function (result) {

                        var section = findService.findSection(courseId, sectionId);

                        validateService.throwIfObjectUndefined(section, 'Section');

                        section.title = sectionTitle;
                        section.modifiedBy = courseContext.user.firstName + " " + courseContext.user.surname;
                        section.lastModifiedDate = new Date(result);

                        return true;
                    });
            },

            deleteSection: function (courseId, sectionId) {
                return http.post('section/delete', { sectionId: sectionId })
                    .then(function (result) {

                        var course = findService.findCourse(courseId);

                        validateService.throwIfObjectUndefined(course, 'Course');

                        var sectionIndex = course.sectionList.findIndex(function (section) {
                            return sectionId === section.id;
                        });

                        if (sectionIndex >= 0) {
                            course.sectionList.splice(sectionIndex, 1);
                        }

                        return true;
                    });
            }
        };
    });