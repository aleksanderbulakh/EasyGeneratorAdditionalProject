define(function () {
    function SSIQ(spec) {
        this.id = spec.id;
        this.text = spec.text;
        this.isAnswer = spec.isAnswer;
        this.photo = spec.photo;
    }

    return SSIQ;
});