define(['knockout', 'plugins/router', 'IoC/IoC', 'customPlugins/createDialog/createDialog',
    'customPlugins/customMessages/customMessage', 'errorHandler/errorHandler', 'constants/constants'],
    function (ko, router, IoC, createDialog, message, errorHandler, constants) {

        return {
            courseList: ko.observableArray([]),

            activate: function () {

                var self = this;
                return IoC.getRepository(constants.REPOSITORIES_NAMES.COURSE).getCourseList().then(function (data) {
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
                            IoC.getRepository(constants.REPOSITORIES_NAMES.COURSE).deleteCourse(id)
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
                        IoC.getRepository(constants.REPOSITORIES_NAMES.COURSE).createCourse(response)
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