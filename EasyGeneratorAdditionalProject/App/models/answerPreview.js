define(['knockout'], function (ko) {
    function AnswerPreview(spec) {

        this.id = spec.id;
        this.text = spec.text;
        this.isCorrect = ko.observable(spec.isCorrect);
    }

    return AnswerPreview;
});