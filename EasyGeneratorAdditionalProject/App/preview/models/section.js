define(function () {
    function Section(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.progress = 0;
        this.questionList = spec.questionList;

        this.computeProgress = function () {

            if (this.questionList.length === 0) {
                this.progress = 0;
            } else {
                var resultSum = 0;

                _.each(this.questionList, function (question) {
                    resultSum += question.result;
                });
                this.progress = (resultSum / this.questionList.length) * 100;
            }
        }
    }

    return Section;
});