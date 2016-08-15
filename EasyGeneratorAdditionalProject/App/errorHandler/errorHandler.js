define(['customPlugins/customMessages/customMessage', 'constants/constants'],
    function (customMessage, constants) {

        $(document).ajaxError(function (event, jqxhr, settings, thrownError) {
            var errorMessage = '';

            switch (jqxhr.status) {
                case 400: errorMessage = constants.MESSAGES.DATA_IS_NOT_FOUND; break;
                default: errorMessage = constants.MESSAGES.INVALID_DATA; break;
            }

            customMessage.stateMessage(errorMessage, constants.MESSAGES_STATE.ERROR + ' ' + jqxhr.status.toLocaleString());
        });

        return {
        };
    });