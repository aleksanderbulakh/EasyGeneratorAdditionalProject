define(['models/titled'], function (Titled) {
    function Section(spec) {
        Titled.apply(this, arguments);

        this.questionList = spec.questionList;
    }

    return Section;
});