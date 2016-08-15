define(['customPlugins/customMessages/customMessage'], function (customMessage) {
    function NotImplementedError(message) {
        var temp = Error.apply(this, arguments);
        temp.name = this.name = 'NotImplementedError';
        this.stack = temp.stack;
        this.message = temp.message;

        customMessage.stateMessage(this.message, temp.name);
    }

    NotImplementedError.prototype = Object.create(Error.prototype, {
        constructor: {
            value: NotImplementedError,
            writable: true,
            configurable: true
        }
    });

    return NotImplementedError;
});