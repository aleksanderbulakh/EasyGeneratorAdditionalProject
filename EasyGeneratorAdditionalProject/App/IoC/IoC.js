define(['repositories/courseRepository', 'repositories/sectionRepository', 'repositories/questionRepository',
    'repositories/answerRepository'],
    function (courseRepository, sectionRepository, questionRepository, answerRepository) {
        return {
            courseRepository: courseRepository,
            sectionRepository: sectionRepository,
            questionRepository: questionRepository,
            answerRepository: answerRepository
        };
    });