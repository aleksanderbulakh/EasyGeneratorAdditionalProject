define(['models/titled'], function (Titled) {
    function Course(spec) {
        Titled.apply(this, arguments);

        this.description = spec.description;
        this.sectionList = spec.sectionList;
    }

    return Course;
});