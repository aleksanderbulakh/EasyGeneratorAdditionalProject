define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository'],
    function (ko, router, app, courseRepository) {
        return {
            courseId: ko.observable(),
            courseTitle: ko.observable(),
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