define(function () {
    function Answer(spec) {
        this.id = spec.id;
        this.questionId = spec.questionId;
        this.text = spec.text;
        this.isCorrect = spec.isCorrect;
    }

    return Answer;
});