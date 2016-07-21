define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository'],
    function (ko, router, app, courseRepository) {

        ko.extenders.validName = function (target, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();

            function validate(newValue) {
                var check;
                if (!newValue)
                    check = false;
                else {
                    newValue = newValue.trim();
                    check = (newValue.length <= 255 && newValue.length > 0);
                }
                target.hasError(!check);
                target.validationMessage(check ? "" : overrideMessage || "This field is required");
            }

            validate(target());
            target.subscribe(validate);
            return target;
        };

        return {
            courseId: ko.observable(),
            courseTitle: ko.observable().extend({
                validName: 'Please, enter course title! Maximum number of characters - 255.'
            }),
            courseSection: ko.observable(),
            courseDescription: ko.observable(),
            activate: function (id) {
                var self = this;
                courseRepository.getCourseById(id).then(function (course) {
                    self.courseTitle(course.title);
                    self.courseSection(course.sectionList);
                    self.courseDescription(course.description);
                    self.courseId(id);
                });
            },
            editTitle: function () {
                var self = this;
                courseRepository.getCourseById(this.courseId()).then(function (course) {
                    if (course.title !== self.courseTitle())
                        if (self.courseTitle() !== "" && self.courseTitle().length <= 255)
                            courseRepository.editCourseTitle(self.courseId(), self.courseTitle()).then(function (result) {
                                alert(result);
                            });
                        else
                            alert("Title must be from 1 to 255 letters");
                });
            },
            editDescription: function () {
                var self = this;
                courseRepository.getCourseById(this.courseId()).then(function (course) {
                    if (course.description !== self.courseDescription())
                        courseRepository.editCourseDescription(self.courseId(), self.courseDescription()).then(function (result) {
                            alert(result);
                        });
                });
            }
        };
    });