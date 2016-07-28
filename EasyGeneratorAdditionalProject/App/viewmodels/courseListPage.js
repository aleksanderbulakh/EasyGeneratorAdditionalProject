define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository', 'customPlugins/createDialog', 'customPlugins/customMessage'],
    function (ko, router, app, courseRepository, sectionRepository, createDialog, message) {
        return {
            router: router,
            courseList: ko.observableArray(),

            activate: function () {
                var self = this;
                courseRepository.getCourseList().then(function (data) {
                    self.courseList(data);
                });
            },

            coursePreview: function () {
                router.navigate('#preview');
            },

            deleteCourse: function (id) {
                var self = this;
                message.confirmMessage()
                    .then(function (result) {
                        if (result) {
                            courseRepository.deleteCourse(id)
                                .then(function (result) {
                                    if (typeof result === "boolean") {
                                        self.courseList.valueHasMutated();
                                        message.stateMessage("Course was been deleted.", "Success");
                                    }
                                    else {
                                        if (result !== undefined) {
                                            message.stateMessage(result, "Error");
                                        }
                                    }
                                });
                        }
                    });
            },

            createCourse: function () {
                createDialog.show()
                    .then(function (response) {
                        courseRepository.createCourse(response)
                            .then(function (courseId) {
                                router.navigate('#course/' + courseId);
                            });
                    });
            },

            toCourse: function (courseId) {
                router.navigate('#course/' + courseId);
            }
        };
    });