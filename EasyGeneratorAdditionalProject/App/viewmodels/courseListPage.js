define(['knockout', 'plugins/router', 'durandal/app', 'data/courseProvider'], function (ko, router, app, courseProvider) {
    return {
        router: router,
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
        }
    };
});