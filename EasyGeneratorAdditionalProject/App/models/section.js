define(['models/entity'], function (Entity) {
    function Section(spec) {
        Entity.apply(this, arguments);

        this.questionList = spec.questionList;
    }

    return Section;
});