define(['customPlugins/customMessages/customMessage', 'constants/constants'],
    function (customMessage, constants) {
        $(document).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {

            var errorMessage = '';

            switch (jqXHR.status) {
                case 400: errorMessage = constants.MESSAGES.DATA_IS_NOT_FOUND; break;
                default: errorMessage = constants.MESSAGES.INVALID_DATA; break;
            }

            alert(errorMessage);
            //customMessage.stateMessage(errorMessage, 'Error');
        });

        return {
        }
    });