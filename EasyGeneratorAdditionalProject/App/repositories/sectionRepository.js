﻿define(['mapper/mapper', 'http/httpWrapper', 'errorHandler/errorHandler', 'context/sectionContext',
    'services/validateService', 'constants/constants'],
    function (mapper, http, errorHandler, sectionContext, validateService, constants) {

        return {
            getSectionsByCourseId: function (courseId) {

                if (!_.isUndefined(sectionContext.sectionList)) {

                    var sections = _.filter(sectionContext.sectionList, function (section) {
                        return section.courseId === courseId;
                    });

                    if (!_.isUndefined(sections)) { 
                        return Q.fcall(function () {
                            return sections;
                        });
                    }
                }

                return http.get('section/list', { courseId: courseId })
                    .then(function (result) {

                        if (_.isUndefined(sectionContext.sectionList)) { 
                            sectionContext.sectionList = [];
                        }

                        _.each(result, function (section) {
                            sectionContext.sectionList.push(mapper.mapSection(section, courseId));
                        });

                        var sections = _.filter(sectionContext.sectionList, function (section) {
                            return section.courseId === courseId;
                        });

                        if (_.isUndefined(sections)) {
                            sectionContext = [];
                        }

                        return sections;
                    });
            },

            createSection: function (courseId) {
                return http.post('section/create', { courseId: courseId })
                    .then(function (result) {

                        var section = mapper.mapSection(result, courseId);

                        sectionContext.sectionList.push(section);

                        return section;
                    });
            },

            editSectionTitle: function (sectionId, sectionTitle) {

                return http.post('section/edit/title', { sectionId: sectionId, title: sectionTitle })
                    .then(function (result) {

                        var section = _.find(sectionContext.sectionList, function (section) {
                            return section.id === sectionId;
                        });

                        validateService.throwIfObjectIsUndefined(section, constants.MODELS_NAMES.SECTION);

                        section.title = sectionTitle;
                        section.lastModifiedDate = new Date(result);

                        return section.lastModifiedDate;
                    });
            },

            deleteSection: function (sectionId) {
                return http.post('section/delete', { sectionId: sectionId })
                    .then(function (result) {

                        var sectionIndex = sectionContext.sectionList.findIndex(function (section) {
                            return sectionId === section.id;
                        });

                        if (sectionIndex >= 0) {
                            sectionContext.sectionList.splice(sectionIndex, 1);
                        }

                        return true;
                    });
            }
        };
    });