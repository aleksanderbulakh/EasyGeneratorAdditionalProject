define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository'],
    function (ko, router, app, courseRepository) {
        return {
            router: router,
            courseList: ko.observableArray(),
            activate: function () {
                var self = this;
                courseRepository.getCourseList().then(function (data) {
                    self.courseList(data);
                });
            },
            coursePreview: function () {
                router.navigate('#preview');
            },
            deleteCourse: function (id, canClick) {
                if (confirm("Are you sure?"))
                    courseRepository.deleteCourse(id).then(function (result) {
                        alert(result);
                        location.reload();
                    });
            },
            createCourse: function () {
                courseRepository.createCourse().then(function (courseId) {
                    router.navigate('#new-course/' + courseId);
                });
            }
        };
    });