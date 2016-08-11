define(['models/course', 'models/section', 'models/question', 'models/answer', 'models/result', 'models/sectionPreview',
    'models/singleSelectQuestionPreview', 'models/multipleSelectQuestionPreview', 'models/answerPreview', 'constants/constants'],
    function (Course, Section, Question, Answer, Result, SectionPreview, SingleSelectQuestionPreview,
        MultipleSelectQuestionPreview, AnswerPreview, constants) {

        return {
            newModel: function (spec, type) {
                switch (type) {
                    case constants.MODELS_NAMES.COURSE: {
                        return new Course(spec);
                    }
                    case constants.MODELS_NAMES.SECTION: {
                        return new Section(spec);
                    }
                    case constants.MODELS_NAMES.SECTION_PREVIEW: {
                        return new SectionPreview(spec);
                    }
                    case constants.MODELS_NAMES.QUESTION: {
                        return new Question(spec);
                    }
                    case constants.MODELS_NAMES.SINGLE_SELECT_QUESTION_PREVIEW: {
                        return new SingleSelectQuestionPreview(spec);
                    }
                    case constants.MODELS_NAMES.MULTIPLE_SELECT_QUESTION_PREVIEW: {
                        return new MultipleSelectQuestionPreview(spec);
                    }
                    case constants.MODELS_NAMES.ANSWER: {
                        return new Answer(spec);
                    }
                    case constants.MODELS_NAMES.ANSWER_PREVIEW: {
                        return new AnswerPreview(spec);
                    }
                    case constants.MODELS_NAMES.RESULT: {
                        return new Result(spec);
                    }
                    default: throw constants.MESSAGES.INVALID_DATA;
                }
            }
        };
    });