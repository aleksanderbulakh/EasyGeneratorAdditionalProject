define(function () {
    function Result(spec) {

        this.sectionId = spec.sectionId;
        this.questionId = spec.questionId;
        this.result = spec.result;
    }

    return Result;
});