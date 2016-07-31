define(['models/titled'], function (Titled) {
    function Course(spec) {
        Titled.apply(this, arguments);

        this.type = spec.type;
        this.answersList = spec.answersList;
    }

    return Course;
});