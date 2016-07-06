define(['plugins/router', 'durandal/app', 'knockout'], function (router, app, ko) {
    return {
        router: router,
        redirectToHome: function () {
            router.navigate('');
        },
        activate: function () {

            router.map([
                { route: '', title: 'Home', moduleId: 'viewmodels/home', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});