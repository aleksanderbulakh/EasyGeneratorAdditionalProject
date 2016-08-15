define(function () {
    function SingleSelectQuestion(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        this.checkedAnswer = 0;
        this.result = 0;

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
        };

        this.checkForCorrectness = function () {

            var answersCheck = _.find(self.answersList, function (answer) {
                var r = answer.isCorrect & answer.checked();
                return r;
            });

            this.result = !_.isUndefined(answersCheck) ? 1 : 0;
        };

        this.getResults = function () {
            return {
                checkedAnswer: this.checkedAnswer,
                result: this.result
            }
        }
    }

    return SingleSelectQuestion;
});