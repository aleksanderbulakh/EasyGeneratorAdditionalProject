define(['models/course', 'http/httpWrapper'], function (Course, http) {

        function initialize() {
            var self = this;
            return http.get('course/list').then(function (data) {
                data.forEach(function (course) {
                    self.courseList.push(new Course({
                        id: course.Id,
                        title: course.Title,
                        description: course.Description,
                        createdOn: course.CreatedOn,
                        createdBy: course.CreatedBy,
                        lastModified: course.LastModifiedDate,
                        sectionList: []
                    }));
                });
            });
        }

        return {
            initialize: initialize,
            courseList: []
        }
    });