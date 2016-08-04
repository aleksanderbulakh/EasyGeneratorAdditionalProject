define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository',
    'customPlugins/createDialog/createDialog', 'customPlugins/customMessages/customMessage'],
    function (ko, router, app, courseRepository, sectionRepository, createDialog, message) {
        return {
            router: router,
            courseList: ko.observableArray([]),

            activate: function () {
                var self = this;
                courseRepository.getCourseList().then(function (data) {
                    self.courseList(data);
                })
                .fail(function (result) {
                    message.stateMessage(result, "Error");
                });;
            },

            coursePreview: function (id) {
                router.navigate('#preview/' + id);
            },

            deleteCourse: function (id) {
                var self = this;
                message.confirmMessage()
                    .then(function (result) {
                        if (result) {
                            courseRepository.deleteCourse(id)
                                .then(function () {
                                        self.courseList.valueHasMutated();
                                        message.stateMessage("Course has been deleted.", "Success");
                                })
                                .fail(function (result) {
                                    message.stateMessage(result, "Error");
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
                            })
                            .fail(function (result) {
                                message.stateMessage(result, "Error");
                            });
                    });
            },

            toCourse: function (courseId) {
                router.navigate('#course/' + courseId);
            }
        };
    });