define(function () {
    function Section(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.createdOn = spec.createdOn;
        this.lastModifiedDate = spec.lastModifiedDate;
        this.createdBy = spec.createdBy;
        this.contentList = spec.contentList;
    }

    return Section;
});