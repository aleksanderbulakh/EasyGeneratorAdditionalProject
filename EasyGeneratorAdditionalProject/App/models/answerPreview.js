define(['knockout'], function (ko) {
    function AnswerPreview(spec) {

        this.id = spec.id;
        this.text = spec.text;
        this.isCorrect = spec.isCorrect;
        this.checked = ko.observable(false);
    }

    return AnswerPreview;
});