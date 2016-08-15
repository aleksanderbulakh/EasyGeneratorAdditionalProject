define(['plugins/router', 'preview/previewRepository', 'customPlugins/customMessages/customMessage',
    'errorHandler/errorHandler', 'mapper/mapper', 'constants/constants'],
    function (router, previewRepository, message, errorHandler, mapper, constants) {


        return {
            course: {},
            activate: function (id) {

                var self = this;
                if (id === undefined) {
                    message.stateMessage("Invalid id", "Error");
                }
                else {

                    return previewRepository.getCourseById(id)
                        .then(function (course) {
                            course.computeProgress();
                            self.course = course;
                        });
                }
            },
            navigateToPreviewSection: function (section) {
                router.navigate('#section/' + section.id);
            }
        };
    });