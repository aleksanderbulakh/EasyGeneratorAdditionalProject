requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',
        'transitions': '../Scripts/durandal/transitions'
    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['durandal/system', 'durandal/app', 'durandal/viewLocator', 'durandal/composition', 'context/courseContext'],
    function (system, app, viewLocator, composition, courseContext) {
        system.debug(true);

        app.title = 'EG Additional Project';

        app.configurePlugins({
            router: true,
            dialog: true
        });
        
        app.start()
            .then(function () {
                viewLocator.useConvention();

                courseContext.initialize()
                    .then(function () {
                        app.setRoot('viewmodels/shell', 'entrance')
                    });
            });
    });