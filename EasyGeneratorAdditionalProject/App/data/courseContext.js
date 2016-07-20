define(['mapper/mapper', 'http/httpWrapper'], function (mapper, http) {
        function initialize() {
            var self = this;
            return http.get('course/list').then(function (data) {
                data.forEach(function (course) {
                    self.courseList.push(mapper.mapCourse(course));
                });
            });
        }

        return {
            initialize: initialize,
            courseList: []
        }
    });