define(['repositories/courseRepository', 'repositories/sectionRepository', 'repositories/questionRepository',
    'repositories/answerRepository', 'preview/resultsRepository', 'constants/constants'],
    function (courseRepository, sectionRepository, questionRepository, answerRepository, resultsRepository, constants) {
        return {
            getRepository(type) {

                switch (type) {
                    case constants.REPOSITORIES_NAMES.COURSE: {
                        return courseRepository;
                    }
                    case constants.REPOSITORIES_NAMES.SECTION: {
                        return sectionRepository;
                    }
                    case constants.REPOSITORIES_NAMES.QUESTION: {
                        return questionRepository;
                    }
                    case constants.REPOSITORIES_NAMES.ANSWER: {
                        return answerRepository;
                    }
                    case constants.REPOSITORIES_NAMES.RESULT: {
                        return resultsRepository;
                    }
                    default: throw constants.MESSAGES.INVALID_DATA;
                }
            }
        };
    });