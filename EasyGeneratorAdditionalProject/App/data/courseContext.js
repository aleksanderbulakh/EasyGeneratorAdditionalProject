define(['models/course', 'http/httpWrapper'], function (Course, http) {

    function mapCourse(course) {
        return new Course({
            id: course.Id,
            title: course.Title,
            description: course.Description,
            createdOn: course.CreatedOn,
            createdBy: course.UserName,
            lastModified: course.LastModifiedDate
        });
    }

    function initialize() {
        var self = this;
        return http.post('courses').then(function (data) {
            data.forEach(function (course) {
                self.courseOptions.push(mapOption(course));
            });
        });
    }

    return {
        initialize: initialize,
        courseList: []
    }
});