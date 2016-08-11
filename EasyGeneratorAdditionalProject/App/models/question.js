define(['models/entity'], function (Entity) {
    function Question(spec) {
        Entity.call(this, spec);

        this.sectionId = spec.sectionId;
        this.type = spec.type;
    }

    return Question;
});