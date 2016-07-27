define(['knockout', 'plugins/router', 'durandal/app', 'data/sectionRepository'],
    function (ko, router, app, sectionRepository) {

        return function () {

            return {
                viewUrl: 'views/sectionPage',
                sectionData: ko.observable(),
                sectionId: ko.observable(),
                sectionTitle: ko.observable().extend({
                    validName: 'Please, enter section title! Maximum number of characters - 255.'
                }),
                contentList: ko.observable(),

                activate: function (sectionData) {
                    this.sectionData(sectionData);
                    this.sectionId(sectionData.id);
                    this.sectionTitle(sectionData.title);
                    this.contentList(sectionData.contentList);
                },
                editSectionTitle: function () {
                    var self = this;

                    if (this.sectionData().title !== this.sectionTitle()) {
                        sectionRepository.editSectionTitle(this.sectionId(), this.sectionTitle())
                            .then(function (result) {
                                alert(result);
                            });
                    }

                },
                deleteSection: function () {
                    var self = this;
                    sectionRepository.deleteSection(this.sectionId())
                        .then(function (result) {
                            if (typeof result == "boolean") {
                                app.trigger('data:changed');
                                alert("Section was been deleted.");
                            }
                            else
                                alert(result);
                        });
                }
            };
        };
    });