define(['knockout', 'plugins/router', 'durandal/app', 'services/mapService', 'data/courseRepository'],
    function (ko, router, app, mapService, courseRepository) {
        return {
            courseId: ko.observable(),
            courseTitle: ko.observable(),
            courseSection: ko.observable(),
            courseDescription: ko.observable(),
            activate: function (id) {
                var course = mapService.courseMapById(id);

                this.courseTitle(course.title);
                this.courseSection(course.sectionList);
                this.courseDescription(course.description);
                this.courseId(id);
            },
            edit: function () {
                courseRepository.editCourse(this.courseId(), this.courseTitle(), this.courseDescription()).done(function (result) {
                    alert(result);
                });
            }
        };
    });