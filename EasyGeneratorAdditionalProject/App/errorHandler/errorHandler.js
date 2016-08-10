define(['customPlugins/customMessages/customMessage'],
    function (customMessage) {
        $(document).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {

            var errorMessage = '';

            switch (jqXHR.status) {
                case 400: errorMessage = 'Data not found'; break;
                default: errorMessage = 'Invalid data'; break;
            }

            alert(errorMessage);
            //customMessage.stateMessage(errorMessage, 'Error');
        });

        return {
        }
    });