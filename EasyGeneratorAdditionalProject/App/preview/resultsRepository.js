define(['mapper/mapper', 'preview/resultsContext', 'errorHandler/errorHandler'],
    function (mapper, resultsContext, errorHandler) {
        return {
            setNewResult: function (sectionId, questionId, result) {

                if (resultsContext.resultsList === undefined) {
                    resultsContext.resultsList = [];

                    resultsContext.resultsList.push(mapper.mapResult(sectionId, questionId, result));
                } else {
                    var resultObj = resultsContext.resultsList.find(function (result) {
                        return result.sectionId === sectionId && result.questionId === questionId;
                    });

                    if (resultObj !== undefined) {
                        resultObj.result = result;
                    } else {
                        resultsContext.resultsList.push(mapper.mapResult(sectionId, questionId, result));
                    }
                }
            },
            getResultByQuestionId: function (questionId) {

                if (resultsContext.resultsList !== undefined) {
                    return resultsContext.resultsList.find(function (result) {
                        return result.questionId === questionId;
                    });
                }
            },
            getResultBySectionId: function (sectionId) {

                if (resultsContext.resultsList !== undefined) {
                    return resultsContext.resultsList.filter(function (result) {
                        return result.sectionId === sectionId;
                    });
                }
            }
        };
    });