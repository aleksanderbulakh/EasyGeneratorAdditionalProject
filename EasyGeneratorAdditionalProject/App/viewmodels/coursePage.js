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
            isChangeable: true,
            activate: function (id) {
                var self = this;

                courseRepository.getCourseById(id)
                    .then(function (result) {
                        self.courseTitle(result.title);
                        self.courseDescription(result.description);
                        self.courseId(id);
                        self.course(result);
                        sectionRepository.getSectionsByCourseId(id)
                            .then(function () {
                                self.courseSection(result.sectionList);
                            });

                        self.isChangeable = ko.computed(function () {
                            return self.courseTitle.hasError() === (self.courseTitle() !== self.course().title);
                        });
                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });

                app.on('data:changed').then(function () {
                    self.courseSection.valueHasMutated();
                });
            },
            editTitle: function () {
                var self = this;

                courseRepository.getCourseById(this.courseId())
                    .then(function (result) {

                        courseRepository.editCourseTitle(self.courseId(), self.courseTitle())
                            .then(function () {
                                self.course().title = self.courseTitle();
                                message.stateMessage("Title has been changed.", "Success");
                            });
                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });
            },
            editDescription: function () {
                var self = this;

                courseRepository.getCourseById(this.courseId())
                    .then(function (result) {

                        courseRepository.editCourseDescription(self.courseId(), self.courseDescription())
                            .then(function () {
                                message.stateMessage("Description has been changed.", "Success");
                            });

                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });
            },
            createSection: function (courseId) {
                var self = this;
                sectionRepository.createSection(courseId())
                    .then(function () {
                        app.trigger('data:changed');
                        message.stateMessage("Section hes been created.", "Success");
                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });
            }
        }
    });