define(function () {
    function Course(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.description = spec.description;
        this.createdOn = spec.createdOn;
        this.createdBy = spec.createdBy;
        this.lastModified = spec.lastModified;
    }

    return Course;
});