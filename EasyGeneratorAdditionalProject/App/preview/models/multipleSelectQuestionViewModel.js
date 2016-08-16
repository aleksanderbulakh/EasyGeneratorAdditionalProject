define(function () {
    function MultipleSelectQuestion(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        debugger;
        this.checkedAnswer = spec.checkedAnswer;

        var self = this;

        this.computeCorrectness = function (answer) {

            answer.checked(!answer.checked());

            if (answer.checked()) {
                this.checkedAnswer.push(answer.id);
            } else {
                this.checkedAnswer = _.without(this.checkedAnswer, answer.id)
            }
        }

        this.getResult = function () {
            return self.checkedAnswer;
        }
    }

    return MultipleSelectQuestion;
});