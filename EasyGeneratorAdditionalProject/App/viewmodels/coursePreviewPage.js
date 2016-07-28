define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository'],
    function (ko, router, app, courseRepository) {
        return {
            courseTitle: ko.observable(),
            courseDescription: ko.observable(),
            createdBy: ko.observable(),
            sectionList: ko.observableArray(),
            activate: function (id) {
                var self = this;

                courseRepository.getCourseById(id)
                    .then(function (result) {
                        if (typeof result != "object") {
                            alert(result);
                        }
                        else {
                            self.courseTitle(result.title);
                            self.courseDescription(result.description);
                            self.createdBy(result.createdBy);
                            sectionRepository.getSectionByCourseId(id)
                                .then(function () {
                                    self.sectionList(result.sectionList);
                                });
                        }
                    });
            }
        };
    });