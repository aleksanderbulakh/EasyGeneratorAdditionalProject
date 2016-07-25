define(['models/titled'], function (Titled) {
    function Section(spec) {
        Titled.apply(this, arguments);

        this.contentList = spec.contentList;
    }

    return Section;
});