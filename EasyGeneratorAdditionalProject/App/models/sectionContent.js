define(function () {
    function SectionContent(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.type = spec.type;
        this.contentValueList = spec.contentValueList
    }

    return SectionContent;
});