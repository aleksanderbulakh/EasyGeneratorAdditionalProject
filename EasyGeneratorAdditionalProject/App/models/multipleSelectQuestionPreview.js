define(function () {
    function MultipleSelectQuestionPreview(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.answersList = spec.answersList;
        this.type = spec.type;

        var self = this;

        this.computeCorrectness = function (answerId) {
            var answer = self.answersList.find(function (answer) {
                return answer.id === answerId;
            });

            if (answer.isCorrect()) {
                answer.isCorrect(false);
            } else {
                answer.isCorrect(true);
            }
        };

        this.checkForCorrectness = function (answers) {
            var correctCount = 0;
            var countAnswers = answers.length;

            answers.forEach(function (correctAnswer) {
                var answerForCheck = self.answersList.find(function (answer) {
                    return answer.id === correctAnswer.id && answer.isCorrect() === correctAnswer.isCorrect;
                });

                if (answerForCheck !== undefined) {
                    correctCount++;
                }
            });

            return correctCount / countAnswers;
        };
    }

    return MultipleSelectQuestionPreview;
});