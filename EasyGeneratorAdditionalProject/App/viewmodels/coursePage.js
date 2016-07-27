define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository'],
    function (ko, router, app, courseRepository, sectionRepository) {
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
                        if (typeof result == "object") {
                            self.courseTitle(result.title);
                            self.courseDescription(result.description);
                            self.courseId(id);
                            self.course(result);
                            sectionRepository.getSectionByCourseId(id)
                                .then(function () {
                                    self.courseSection(result.sectionList);
                                });
                        }
                        else {
                            alert(result);
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
                        if (typeof result == "object") {
                            if (result.title !== self.courseTitle()) {
                                courseRepository.editCourseTitle(self.courseId(), self.courseTitle())
                                    .then(function (result) {
                                        alert(result);
                                    });
                            }
                        }
                        else {
                            alert(result);
                        }
                    });
            },
            editDescription: function () {
                var self = this;
                courseRepository.getCourseById(this.courseId())
                    .then(function (result) {
                        if (typeof result == "object") {
                            if (result.description !== self.courseDescription()) {
                                courseRepository.editCourseDescription(self.courseId(), self.courseDescription())
                                    .then(function (result) {
                                        alert(result);
                                    });
                            }
                        }
                        else {
                            alert(result);
                        }
                    });
            },
            createSection: function (courseId) {
                var self = this;
                sectionRepository.createSection(courseId())
                    .then(function (result) {
                        if (typeof result == 'boolean') {
                            app.trigger('data:changed');
                            alert("Section was been created.");
                        }
                        else {
                            alert(result);
                        }
                    });
            }
        }
    });