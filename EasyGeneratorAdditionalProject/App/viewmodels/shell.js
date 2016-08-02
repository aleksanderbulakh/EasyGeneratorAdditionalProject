﻿define(['plugins/router', 'durandal/app', 'knockout'], function (router, app, ko) {
    return {
        router: router,
        redirectToHome: function () {
            router.navigate('');
        },
        activate: function () {

            router.map([
                { route: '', title: 'Course list', moduleId: 'viewmodels/courseList', nav: true },
                { route: 'course(/:id)', title: 'Course', moduleId: 'viewmodels/course', nav: true },
                { route: 'preview(/:id)', title: 'Prewiew', moduleId: 'viewmodels/previewCourse', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});