define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository', 'customPlugins/customMessage'],
    function (ko, router, app, courseRepository, sectionRepository, message) {
        return {
            courseId: ko.observable(),
            courseTitle: ko.observable().extend({
                validName: 'Please, enter course title! Maximum number of characters - 255.'
            }),
            courseSection: ko.observable(),
            courseDescription: ko.observable(),
            course: ko.observable(),
            activate: function (id) {
                var self = this;

                courseRepository.getCourseById(id)
                    .then(function (result) {
                        if (typeof result !== "object") {
                            message.stateMessage(result, "Error");
                        }
                        else {
                            self.courseTitle(result.title);
                            self.courseDescription(result.description);
                            self.courseId(id);
                            self.course(result);
                            sectionRepository.getSectionByCourseId(id)
                                .then(function () {
                                    self.courseSection(result.sectionList);
                                });
                        }
                    });

                app.on('data:changed').then(function () {
                    self.courseSection.valueHasMutated();
                });
            },
            editTitle: function () {
                var self = this;
                courseRepository.getCourseById(this.courseId())
                    .then(function (result) {
                        if (typeof result === "object") {
                            if (result.title !== self.courseTitle()) {
                                courseRepository.editCourseTitle(self.courseId(), self.courseTitle())
                                    .then(function (result) {
                                        if (result !== undefined) {
                                            message.stateMessage(result, "Success");
                                        }
                                    });
                            }
                        }
                        else {
                            if (result !== undefined) {
                                message.stateMessage(result, "Error");
                            }
                        }
                    });
            },
            editDescription: function () {
                var self = this;
                courseRepository.getCourseById(this.courseId())
                    .then(function (result) {
                        if (typeof result === "object") {
                            if (result.description !== self.courseDescription()) {
                                courseRepository.editCourseDescription(self.courseId(), self.courseDescription())
                                    .then(function (result) {
                                        if (result !== undefined) {
                                            message.stateMessage(result, "Success");
                                        }
                                    });
                            }
                        }
                        else {
                            if (result !== undefined) {
                                message.stateMessage(result, "Error");
                            }
                        }
                    });
            },
            createSection: function (courseId) {
                var self = this;
                sectionRepository.createSection(courseId())
                    .then(function (result) {
                        if (typeof result === 'boolean') {
                            app.trigger('data:changed');
                            message.stateMessage("Section was been created.", "Success");
                        }
                        else {
                            if (result !== undefined) {
                                message.stateMessage(result, "Error");
                            }
                        }
                    });
            }
        }
    });