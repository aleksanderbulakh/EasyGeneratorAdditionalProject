define(['plugins/router', 'durandal/app', 'knockout'], function (router, app, ko) {
    return {
        router: router,
        redirectToHome: function () {
            router.navigate('');
        },
        activate: function () {

            router.map([
                { route: '', title: 'Course list', moduleId: 'viewmodels/courseListPage', nav: true },
                { route: 'course(/:id)', title: 'Course', moduleId: 'viewmodels/coursePage', nav: true },
                { route: 'preview(/:id)', title: 'Prewiew', moduleId: 'viewmodels/coursePreviewPage', nav: true },
                { route: 'login', title: 'Login', moduleId: 'viewmodels/loginPage', nav: true },
                { route: 'registration', title: 'Registration', moduleId: 'viewmodels/userRegistrationPage', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});