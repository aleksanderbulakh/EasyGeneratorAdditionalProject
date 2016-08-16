define(function () {
    function SingleSelectQuestion(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        this.checkedAnswer = spec.checkedAnswer;

        var self = this;

        this.computeCorrectness = function (answer) {
            var currentCorrectAnswer = _.find(self.answersList, function (answer) {
                return answer.checked();
            });

            if (!_.isUndefined(currentCorrectAnswer)) {
                currentCorrectAnswer.checked(!currentCorrectAnswer.checked());
            }
            answer.checked(!answer.checked());

            this.checkedAnswer = answer.id;
        }

        this.getResult = function () {
            return self.checkedAnswer;
        }
    }

    return SingleSelectQuestion;
});