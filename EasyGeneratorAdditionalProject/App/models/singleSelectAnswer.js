define(function () {
    function SingleSelectAnswer(spec) {
        this.id = spec.id;
        this.text = spec.text;
        this.isAnswer = spec.isAnswer;
    }

    return SingleSelectAnswer;
});