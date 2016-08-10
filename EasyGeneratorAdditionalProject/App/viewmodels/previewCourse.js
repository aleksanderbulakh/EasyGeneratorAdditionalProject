define(['plugins/router', 'repositories/courseRepository', 'repositories/sectionRepository',
    'customPlugins/customMessages/customMessage', 'errorHandler/errorHandler', 'preview/resultsRepository'],
    function (router, courseRepository, sectionRepository, message, errorHandler, resultsRepository) {
        return {
            courseId: '',
            courseTitle: '',
            courseDescription: '',
            createdBy: '',
            courseProgress: '',
            sectionList: [],
            activate: function (id) {
                var self = this;
                if (id == undefined) {
                    message.stateMessage("Invalid id", "Error");
                }
                else {

                    return courseRepository.getCourseById(id)
                        .then(function (result) {

                            self.courseId = result.id;
                            self.courseTitle = result.title;
                            self.courseDescription = result.description;
                            self.createdBy = result.createdBy;

                            return sectionRepository.getSectionsByCourseId(id)
                                .then(function (result) {
                                    self.sectionList = result.map(function (section) {
                                        return {
                                            id: section.id,
                                            courseId: section.courseId,
                                            title: section.title,
                                            createdBy: section.createdBy,
                                            modifiedBy: section.modifiedBy,
                                            createdOn: section.createdOn,
                                            lastModified: section.lastModified,
                                            sectionProgress: 0
                                        }
                                    });

                                    var courseProgress = 0;
                                    debugger;
                                    self.sectionList.forEach(function (section) {
                                        var results = resultsRepository.getResultBySectionId(section.id);

                                        if (results !== undefined) {
                                            var resultSum = 0;
                                            results.forEach(function (result) {
                                                resultSum += result.result;
                                            });
                                            section.sectionProgress = (resultSum / results.length) * 100;
                                        }
                                        courseProgress += section.sectionProgress;
                                    });

                                    self.courseProgress = courseProgress / self.sectionList.length;
                                });
                        });
                }
            },
            navigateToSectionPreview: function (sectionId) {
                router.navigate('#section/' + sectionId);
            }
        };
    });