define(['models/entity'], function (Entity) {
    function Course(spec) {
        Entity.apply(this, arguments);

        this.description = spec.description;
        this.sectionList = spec.sectionList;
    }

    return Course;
});