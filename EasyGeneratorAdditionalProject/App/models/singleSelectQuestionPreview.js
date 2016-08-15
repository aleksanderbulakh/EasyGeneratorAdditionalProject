define(function () {
    function SingleSelectQuestionPreview(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;
        this.result = 0;

        var self = this;

        this.computeCorrectness = function (answer) {
            var currentCorrectAnswer = _.find(self.answersList, function (answer) {
                return answer.checked();
            });

            if (currentCorrectAnswer !== undefined) {
                currentCorrectAnswer.checked(!currentCorrectAnswer.checked());
            }
            answer.checked(!answer.checked());
        };

        this.checkForCorrectness = function () {

            var answersCheck = _.find(self.answersList, function (answer) {
                var r = answer.isCorrect & answer.checked();
                return r;
            });

            this.result = answersCheck !== undefined ? 1 : 0;
        };
    }

    return SingleSelectQuestionPreview;
});