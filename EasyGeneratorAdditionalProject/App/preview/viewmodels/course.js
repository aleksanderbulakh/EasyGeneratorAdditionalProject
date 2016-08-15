define(['plugins/router', 'preview/data/previewRepository', 'customPlugins/customMessages/customMessage'],
    function (router, previewRepository, message) {


        return {
            course: {},
            activate: function (id) {

                var self = this;
                if (_.isUndefined(id)) {
                    message.stateMessage("Invalid id", "Error");
                }
                else {

                    return previewRepository.getCourseById(id)
                        .then(function (course) {
                            self.course = {
                                id: course.id,
                                title: course.title,
                                description: course.description,
                                createdBy: course.createdBy,
                                progress: course.progress,
                                sectionList: _.map(course.sectionList, function (section) {
                                    return {
                                        id: section.id,
                                        title: section.title,
                                        progress: section.progress
                                    }
                                })
                            };

                        });
                }
            },
            navigateToSection: function (section) {
                router.navigate('#section/' + section.id);
            }
        };
    });