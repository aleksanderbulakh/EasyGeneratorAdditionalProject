define(['repositories/courseRepository', 'repositories/sectionRepository', 'repositories/questionRepository',
    'repositories/answerRepository', 'preview/resultsRepository'],
    function (courseRepository, sectionRepository, questionRepository, answerRepository, resultsRepository) {
        return {
            courseRepository: courseRepository,
            sectionRepository: sectionRepository,
            questionRepository: questionRepository,
            answerRepository: answerRepository,
            resultsRepository: resultsRepository
        };
    });