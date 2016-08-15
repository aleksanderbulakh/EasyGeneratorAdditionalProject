define(function () {
    function MultipleSelectQuestion(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        this.checkedAnswers = [];
        this.result = 0;

        var self = this;

        this.computeCorrectness = function (answer) {
            
            answer.checked(!answer.checked());

            if (answer.checked()) {
                this.checkedAnswers.push(answer.id);
            } else {
                this.checkedAnswers = _.without(this.checkedAnswers, answer.id)
            }            
        };

        this.checkForCorrectness = function () {
            
            var countCorrect = 0;

            var correctAnswers = _.filter(self.answersList, function (answer) {
                return answer.isCorrect === answer.checked();
            });

            if (!_.isUndefined(correctAnswers)) {
                countCorrect = correctAnswers.length;
            }

            this.result = countCorrect / self.answersList.length;
        };

        this.getResults = function () {
            return {
                checkedAnswers: this.checkedAnswers,
                result: this.result
            }
        }
    }

    return MultipleSelectQuestion;
});