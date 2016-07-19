define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository'],
    function (ko, router, app, courseRepository) {
        return {
            router: router,
            courseList: ko.observableArray(),
            activate: function () {
                this.courseList(courseRepository.getCourseList());
            },
            coursePreview: function () {
                router.navigate('#preview');
            },
            deleteCourse: function (id, canClick) {
                if (confirm("Are you sure?"))
                    courseRepository.deleteCourse(id).done(function (result) {
                        alert(result);
                        location.reload();
                    });
            },
            createCourse: function () {
                courseRepository.createCourse().done(function (courseId) {
                    router.navigate('#new-course/' + courseId);
                });
            }
        };
    });