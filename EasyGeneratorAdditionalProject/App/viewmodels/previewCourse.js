define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository',
    'customPlugins/customMessages/customMessage'],
    function (ko, router, app, courseRepository, sectionRepository, message) {
        return {
            courseTitle: '',
            courseDescription: '',
            createdBy: '',
            sectionList: [],
            activate: function (id) {
                var self = this;
                if (id == undefined) {
                    message.stateMessage("Invalid id", "Error");
                }
                else {
                    courseRepository.getCourseById(id)
                        .then(function (result) {
                            self.courseTitle = result.title;
                            self.courseDescription = result.description;
                            self.createdBy = result.createdBy;
                            sectionRepository.getSectionsByCourseId(id)
                                .then(function () {
                                    self.sectionList = result.sectionList;
                                });
                        })
                        .fail(function (result) {
                            message.stateMessage(result, "Error");
                        });;
                }
            }
        };
    });