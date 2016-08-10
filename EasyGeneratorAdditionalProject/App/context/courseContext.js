define(['http/httpWrapper', 'mapper/mapper'], function (http, mapper) {

    function initialize() {

        var self = this;
        return http.get('course/list')
            .then(function (courseList) {
                self.courseList = [];
                courseList.forEach(function (course) { 
                    self.courseList.push(mapper.mapCourse(course));
                });
            });
    }

    return {
        initialize: initialize,
        courseList: undefined
    };
});