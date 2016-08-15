define(function () {
    function MultipleSelectQuestionPreview(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        this.result = 0;

        var self = this;

        this.computeCorrectness = function (answer) {

            answer.checked(!answer.checked());
        };

        this.checkForCorrectness = function () {
            
            var countCorrect = 0;

            var correctAnswers = _.filter(self.answersList, function (answer) {
                return answer.isCorrect === answer.checked();
            });

            if (correctAnswers !== undefined) {
                countCorrect = correctAnswers.length;
            }

            this.result = countCorrect / self.answersList.length;
        };
    }

    return MultipleSelectQuestionPreview;
});