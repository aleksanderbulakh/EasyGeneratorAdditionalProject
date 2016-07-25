define(['models/course', 'models/section'],
    function (Course, Section) {
        return {
            mapCourse: function (spec) {
                return new Course({
                    id: spec.Id,
                    title: spec.Title,
                    description: spec.Description,
                    createdOn: new Date(spec.CreatedOn).toLocaleDateString(),
                    createdBy: spec.CreatedBy,
                    lastModified: new Date(spec.LastModifiedDate).toLocaleDateString(),
                    sectionList: []
                });
            },

            mapSection: function (spec) {
                return new Section({
                    id: spec.Id,
                    title: spec.Title,
                    createdBy: spec.CreatedBy,
                    createdOn: new Date(spec.CreatedOn).toLocaleDateString(),
                    lastModified: new Date(spec.LastModifiedDate).toLocaleDateString(),
                    contentList: []
                });
            }
        };
    });