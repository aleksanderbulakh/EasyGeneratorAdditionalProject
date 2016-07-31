define(function () {
    function SingleSelectAnswer(spec) {
        this.id = spec.id;
        this.text = spec.text;
        this.isCorrect = spec.isCorrect;
    }

    return SingleSelectAnswer;
});