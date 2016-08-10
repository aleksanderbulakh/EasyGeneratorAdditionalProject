define(['models/entity'], function (Entity) {
    function Section(spec) {
        Entity.call(this, spec);

        this.courseId = spec.courseId;
        this.questionList = spec.questionList;
    }

    return Section;
});