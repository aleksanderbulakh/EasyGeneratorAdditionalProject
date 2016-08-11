define(['plugins/router', 'repositories/courseRepository', 'repositories/sectionRepository',
    'customPlugins/customMessages/customMessage', 'errorHandler/errorHandler', 'preview/resultsRepository', 'mapper/mapper'],
    function (router, courseRepository, sectionRepository, message, errorHandler, resultsRepository, mapper) {

        function computeProgressForSection(sectionsList) {
            sectionsList.forEach(function (section) {
                var results = resultsRepository.getResultBySectionId(section.id);

                if (results !== undefined) {
                    var resultSum = 0;
                    results.forEach(function (result) {
                        resultSum += result.result;
                    });
                    section.progress = (resultSum / results.length) * 100;
                }
            });
        }

        function computeProgressForCourse(sectionsList) {

            var courseProgress = 0;

            sectionsList.forEach(function (section) {
                courseProgress += section.progress;
            });

            return courseProgress / sectionsList.length;
        }

        return {
            courseId: '',
            courseTitle: '',
            courseDescription: '',
            createdBy: '',
            courseProgress: '',
            sectionList: [],
            activate: function (id) {
                debugger;
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
                                        return mapper.mapSectionPreview(section);
                                    });
                                    debugger;
                                    computeProgressForSection(self.sectionList);

                                    self.courseProgress = computeProgressForCourse(self.sectionList);
                                });
                        });
                }
            },
            navigateToSectionPreview: function (sectionId) {
                router.navigate('#section/' + sectionId);
            }
        };
    });