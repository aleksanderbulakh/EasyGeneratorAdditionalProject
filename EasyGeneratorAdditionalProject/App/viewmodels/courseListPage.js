define(['knockout', 'plugins/router', 'durandal/app', 'data/courseRepository', 'data/sectionRepository'],
    function (ko, router, app, courseRepository, sectionRepository) {
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
                if (confirm("Are you sure?")) {
                    courseRepository.deleteCourse(id)
                        .then(function (result) {
                            if (typeof result == "boolean") {
                                self.courseList.valueHasMutated();
                                alert("Course was been deleted.");
                            }
                            else {
                                alert(result);
                            }
                        });
                }
            },
            createCourse: function () {
                courseRepository.createCourse().then(function (courseId) {
                    router.navigate('#course/' + courseId);
                });
            },
            toCourse: function (courseId) {
                router.navigate('#course/' + courseId);
            }
        };
    });