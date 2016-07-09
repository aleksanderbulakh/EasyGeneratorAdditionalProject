define(['knockout', 'plugins/router', 'durandal/app', 'data/courseProvider'], function (ko, router, app, courseProvider) {
    return {

        courseList: ko.observableArray(),

        activate: function () {
            this.courseList(courseProvider.getCourseList());
        },
        coursePreview: function ()
        {
            router.navigate('#preview');
        },
        deleteCourse: function () {
            if (confirm("Are you sure?"))
                alert("deleted");
        },
        createCourse: function () {
            router.navigate("#new-course");
        },
        courseEdit: function () {

        }
    };
});