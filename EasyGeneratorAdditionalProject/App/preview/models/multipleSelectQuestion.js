define(function () {
    function MultipleSelectQuestion(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        this.checkedAnswer = [];
        this.result = 0;

        var self = this;

        this.checkForCorrectness = function () {
            
            var countCorrect = 0;

            var correctAnswers = _.filter(self.answersList, function (answer) {
                return answer.isCorrect === answer.checked;
            });

            if (!_.isUndefined(correctAnswers)) {
                countCorrect = correctAnswers.length;
            }

            this.result = countCorrect / self.answersList.length;
        };
    }

    return MultipleSelectQuestion;
});