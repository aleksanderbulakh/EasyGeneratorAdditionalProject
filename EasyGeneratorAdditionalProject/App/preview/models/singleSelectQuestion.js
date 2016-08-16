define(function () {
    function SingleSelectQuestion(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        this.checkedAnswer = 0;
        this.result = 0;

        var self = this;

        this.checkForCorrectness = function () {

            var answersCheck = _.find(self.answersList, function (answer) {
                return answer.isCorrect & answer.checked;
            });

            this.result = !_.isUndefined(answersCheck) ? 1 : 0;
        };
    }

    return SingleSelectQuestion;
});