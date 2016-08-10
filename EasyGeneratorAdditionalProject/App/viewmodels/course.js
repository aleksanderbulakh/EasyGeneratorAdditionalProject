define(['knockout', 'durandal/app', 'repositories/courseRepository', 'repositories/sectionRepository',
    'customPlugins/customMessages/customMessage', 'services/validateService', 'errorHandler/errorHandler'],
    function (ko, app, courseRepository, sectionRepository, message, validateService, errorHandler) {
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

                return courseRepository.getCourseById(id)
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
                    });

                app.on('section:deleted').then(function (sectionId) {
                    debugger;
                    var section = self.courseSection().find(function (section) {
                        return section.id === sectionId;
                    });

                    validateService.throwIfObjectIsUndefined(section, 'Section');

                    self.courseSection.remove(section);
                });
            },
            editTitle: function () {
                var self = this;

                courseRepository.editCourseTitle(self.courseId, self.courseTitle())
                    .then(function () {
                        self.currentCourseTitle = self.courseTitle();
                        message.stateMessage("Title has been changed.", "Success");
                    });
            },
            editDescription: function () {
                var self = this;

                courseRepository.editCourseDescription(self.courseId, self.courseDescription())
                    .then(function () {
                        message.stateMessage("Description has been changed.", "Success");
                    });
            },
            createSection: function () {
                var self = this;
                sectionRepository.createSection(self.courseId)
                    .then(function (result) {
                        self.courseSection.push(result);
                        message.stateMessage("Section hes been created.", "Success");
                    });
            }
        }
    });