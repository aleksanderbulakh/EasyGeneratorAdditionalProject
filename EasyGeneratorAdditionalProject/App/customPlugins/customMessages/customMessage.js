define(['durandal/app', 'knockout'], function (app, ko) {
    return {
        stateMessage: function (title, state) {
            app.showMessage(title, state, ["Ok"], true, null);
        },
        confirmMessage: function () {
            return app.showMessage("Are you sure?", "", [{ text: "Yes", value: true }, { text: "No", value: false }], false)
        }
    };
});