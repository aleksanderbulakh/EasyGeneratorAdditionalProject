define(['models/entity'], function (Entity) {
    function Course(spec) {
        Entity.apply(this, arguments);

        this.type = spec.type;
        this.answersList = spec.answersList;
    }

    return Course;
});