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
                var result = courseRepository.getCourseById(id);
                if (typeof result === "object") {
                    this.courseTitle(result.title);
                    this.courseSection(result.sectionList);
                    this.courseDescription(result.description);
                    this.courseId(id);
                }
                else
                    alert(result);

            },
            editTitle: function () {
                var result = courseRepository.getCourseById(this.courseId());
                if (typeof result === "object") {
                    if (result.title !== this.courseTitle())
                        courseRepository.editCourseTitle(this.courseId(), this.courseTitle()).then(function (result) {
                            alert(result);
                        });
                }
                else
                    alert(result);
            },
            editDescription: function () {
                var result = courseRepository.getCourseById(this.courseId());
                if (typeof result === "object") {
                if (result.description !== this.courseDescription())
                    courseRepository.editCourseDescription(this.courseId(), this.courseDescription()).then(function (result) {
                        alert(result);
                    });
                }
                else
                    alert(result);
            }
        };
    });