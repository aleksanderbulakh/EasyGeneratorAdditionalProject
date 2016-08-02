define(['plugins/dialog', 'durandal/app', 'knockout', 'data/answerRepository', 'data/constants'],
    function (dialog, app, ko, asnwerRepository, constants) {

        var answerEdit = function (dataObj) {

            this.constants = constants;
            this.courseId = dataObj[0];
            this.sectionId = dataObj[1];
            this.questionId = dataObj[2];
            this.questionType = constants.ANSWER_TYPES[dataObj[3]];
            this.answers = ko.observableArray([]);

            var self = this;
            asnwerRepository.getAnswersByQuestionId(this.courseId, this.sectionId, this.questionId)
                .then(function (result) {
                    self.answers(result);
                });

            app.on('data:changed')
                .then(function () {
                    self.answers.valueHasMutated();
                });
        };

        answerEdit.prototype.addAnswer = function () {
            asnwerRepository.createAnswer(this.courseId, this.sectionId, this.questionId)
                .then(function () {
                    app.trigger('data:changed');
                });
        }

        answerEdit.prototype.ok = function () {
            dialog.close(this);
        };

        answerEdit.show = function () {
            return dialog.show(new answerEdit(arguments));
        };

        return answerEdit;
    });