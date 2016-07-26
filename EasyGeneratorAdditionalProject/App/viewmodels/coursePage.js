define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository'],
    function (ko, router, app, courseRepository, sectionRepository) {

        ko.extenders.validName = function (target, overrideMessage) {
            target.hasError = ko.observable();
            target.validationMessage = ko.observable();

            function validate(newValue) {
                var check;
                if (!newValue)
                    check = false;
                else {
                    newValue = newValue.trim();
                    check = newValue.length <= 255 && newValue.length > 0;
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
            course: ko.observable(),
            activate: function (id) {
                var self = this;

                courseRepository.getCourseById(id).then(function (result) {
                    if (typeof result === "object") {
                        self.courseTitle(result.title);
                        self.courseSection(result.sectionList);
                        self.courseDescription(result.description);
                        self.courseId(id);
                        self.course(result);
                    }
                    else
                        alert(result);
                });

                app.on('data:changed').then(function () {
                    self.courseSection.valueHasMutated();
                });
            },
            editTitle: function () {
                var self = this;
                courseRepository.getCourseById(this.courseId())
                    .then(function (result) {
                        if (typeof result === "object") {
                            if (result.title !== self.courseTitle())
                                courseRepository.editCourseTitle(self.courseId(), self.courseTitle())
                                    .then(function (result) {
                                        alert(result);
                                    });
                        }
                        else
                            alert(result);
                    });
            },
            editDescription: function () {
                var self = this;
                courseRepository.getCourseById(this.courseId())
                    .then(function (result) {
                        if (typeof result === "object") {
                            if (result.description !== self.courseDescription())
                                courseRepository.editCourseDescription(self.courseId(), self.courseDescription())
                                    .then(function (result) {
                                        alert(result);
                                    });
                        }
                        else
                            alert(result);
                    });
            },
            createSection: function (courseId) {
                var self = this;
                sectionRepository.createSection(courseId())
                    .then(function (result) {
                        if (typeof result !== 'object')
                            alert(result);
                        else {
                            if (result.Success)
                                app.trigger('data:changed');
                        }
                    });
            }
        };
    });