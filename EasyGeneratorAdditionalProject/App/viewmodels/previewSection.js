define(['knockout', 'plugins/router', 'durandal/app', 'customPlugins/customMessages/customMessage'],
    function (ko, router, app, message) {
        return function () {
            return {
                viewUrl: 'views/previewSection',
                sectionTitle: '',
                createdBy: '',
                contentList: [],
                activate: function (data) {
                    if (data != undefined) {
                        this.sectionTitle = data.title;
                        this.createdBy = data.createdBy;
                        this.contentList = data.contentList;
                    }
                    else {
                        message.stateMessage("Section is not found.", "Error");
                    }
                }
            }
        };
    });