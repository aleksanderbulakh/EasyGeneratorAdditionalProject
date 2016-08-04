define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository',
    'customPlugins/customMessages/customMessage'],
    function (ko, router, app, courseRepository, sectionRepository, message) {
        return {
            courseId: '',
            courseTitle: ko.observable().extend({
                validName: 'Please, enter course title! Maximum number of characters - 255.'
            }),
            courseSection: ko.observableArray([]),
            courseDescription: ko.observable(),
            currentCourseTitle: '',
            isChangeable: true,
            activate: function (id) {
                var self = this;

                courseRepository.getCourseById(id)
                    .then(function (result) {

                        self.courseId = result.id;
                        self.courseTitle(result.title);
                        self.courseDescription(result.description);
                        self.currentCourseTitle = result.title;
                        sectionRepository.getSectionsByCourseId(id)
                            .then(function (sectionList) {
                                self.courseSection(sectionList.map(function (section) {
                                    return section;
                                }));
                            });

                        self.isChangeable = ko.computed(function () {
                            return self.courseTitle.hasError() === (self.courseTitle() !== self.currentCourseTitle);
                        });
                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });

                app.on('section:deleted').then(function (sectionId) {
                    var sectionIndex = self.courseSection().findIndex(function (section) {
                        return section.id === sectionId;
                    });

                    if (sectionIndex >= 0) { 
                        self.courseSection().splice(sectionIndex, 1);
                        self.answers.valueHasMutated();
                    }
                });
            },
            editTitle: function () {
                var self = this;

                courseRepository.getCourseById(this.courseId)
                    .then(function (result) {

                        courseRepository.editCourseTitle(self.courseId, self.courseTitle())
                            .then(function () {
                                self.currentCourseTitle = self.courseTitle();
                                message.stateMessage("Title has been changed.", "Success");
                            });
                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });
            },
            editDescription: function () {
                var self = this;

                courseRepository.getCourseById(this.courseId)
                    .then(function (result) {

                        courseRepository.editCourseDescription(self.courseId, self.courseDescription())
                            .then(function () {
                                message.stateMessage("Description has been changed.", "Success");
                            });

                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });
            },
            createSection: function () {
                var self = this;
                sectionRepository.createSection(self.courseId)
                    .then(function (result) {
                        self.courseSection().push(result);
                        self.courseSection.valueHasMutated();
                        message.stateMessage("Section hes been created.", "Success");
                    })
                    .fail(function (result) {
                        message.stateMessage(result, "Error");
                    });
            }
        }
    });