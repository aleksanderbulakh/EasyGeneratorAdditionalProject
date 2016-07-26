define(['knockout', 'plugins/router', 'durandal/app', 'data/sectionRepository'],
    function (ko, router, app, sectionRepository) {

        return function () {
            ko.extenders.validName = function (target, overrideMessage) {
                target.hasError = ko.observable();
                target.validationMessage = ko.observable();

                function validate(newValue) {
                    var check;
                    if (!newValue)
                        check = false;
                    else {
                        newValue = newValue.trim();
                        check = newValue.length <= 255 && newValue.length > 0;
                    }
                    target.hasError(!check);
                    target.validationMessage(check ? "" : overrideMessage || "This field is required");
                }

                validate(target());
                target.subscribe(validate);
                return target;
            };
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

                    if (this.sectionData().title !== this.sectionTitle())
                        sectionRepository.editSectionTitle(this.sectionId(), this.sectionTitle())
                            .then(function (result) {
                                if (result.Success) {
                                    alert(result.RequestData);
                                }
                                else "Title not found";
                            });

                },
                deleteSection: function () {
                    var self = this;
                    sectionRepository.deleteSection(this.sectionId())
                        .then(function (result) {
                            if (typeof result === "object") {
                                if (result.Success)
                                    app.trigger('data:changed');
                                alert(result.RequestData);
                            }
                            else
                                alert(result.RequestData);
                        });
                }
            };
        };
    });