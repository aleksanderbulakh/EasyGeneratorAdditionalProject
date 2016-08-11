define(function () {
    function SingleSelectQuestionPreview(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;

        var self = this;

        this.computeCorrectness = function (answerId) {
            var currentCorrectAnswer = self.answersList.find(function (answer) {
                return answer.isCorrect();
            });
            var newCorrectAnswer = self.answersList.find(function (answer) {
                return answer.id === answerId;
            });

            if (currentCorrectAnswer !== undefined) {
                currentCorrectAnswer.isCorrect(false);
            }
            newCorrectAnswer.isCorrect(true);
        }

        this.checkForCorrectness = function (answers) {
            var answerForCheck = self.answersList.find(function (answer) {
                return answer.isCorrect();
            });

            var isAnswerCorrect = answers.find(function (answer) {
                return answer.id === answerForCheck.id && answer.isCorrect === answerForCheck.isCorrect();
            });

            return isAnswerCorrect !== undefined ? 1 : 0;
        }
    }

    return SingleSelectQuestionPreview;
});