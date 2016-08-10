define(['knockout', 'plugins/router', 'repositories/courseRepository', 'customPlugins/createDialog/createDialog',
    'customPlugins/customMessages/customMessage', 'errorHandler/errorHandler'],
    function (ko, router, courseRepository, createDialog, message, errorHandler) {

        return {
            courseList: ko.observableArray([]),

            activate: function () {

                var self = this;
                return courseRepository.getCourseList().then(function (data) {
                    self.courseList(data);
                });
            },

            routToCoursePreview: function (id) {
                return '#preview/' + id;
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

            navigateToCourse: function (courseId) {
                router.navigate('#course/' + courseId);
            }
        };
    });