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
            deleteCourse: function (id) {
                var self = this;
                if (confirm("Are you sure?"))
                    courseRepository.deleteCourse(id).then(function (result) {
                        if (typeof result === "object") {
                            if (result.Success) {
                                self.courseList.valueHasMutated();
                            }
                            alert(result.RequestData);
                        }
                        else
                            alert(result.RequestData);
                    });
            },
            createCourse: function () {
                courseRepository.createCourse().then(function (courseId) {
                    router.navigate('#course/' + courseId);
                });
            }
        };
    });