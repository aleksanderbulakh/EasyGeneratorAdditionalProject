define(['http/httpWrapper', 'mapper/mapper'], function (http, mapper) {

    function initialize() {

        var self = this;
        return http.get('course/list')
            .then(function (courseList) {
                _.each(courseList, function (course) { 
                    self.courseList.push(mapper.mapCourse(course));
                });
            });
    }

    return {
        initialize: initialize,
        courseList: []
    };
});