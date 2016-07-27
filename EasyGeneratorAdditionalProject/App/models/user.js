define([], function () {
    function User(spec) {
        this.id = spec.id,
        this.firstName = spec.firstName;
        this.surname = spec.surname;
    }

    return User;
});