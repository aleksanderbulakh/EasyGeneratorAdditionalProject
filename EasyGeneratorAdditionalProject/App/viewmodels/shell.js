define(['plugins/router', 'durandal/app', 'knockout'], function (router, app, ko) {
    return {
        router: router,
        redirectToHome: function () {
            router.navigate('');
        },
        activate: function () {

            router.map([
                { route: '', title: 'Home', moduleId: 'viewmodels/courseCreatePage', nav: true },
                { route: 'content', title: 'Content', moduleId: 'viewmodels/courseContentPage', nav: true },
                { route: 'preview', title: 'Prewiew', moduleId: 'viewmodels/coursePreviewPage', nav: true },
                { route: 'login', title: 'Login', moduleId: 'viewmodels/loginPage', nav: true },
                { route: 'registration', title: 'Registration', moduleId: 'viewmodels/userRegistrationPage', nav: true },
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});