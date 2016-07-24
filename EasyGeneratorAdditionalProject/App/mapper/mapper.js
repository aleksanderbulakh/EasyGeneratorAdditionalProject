define(['models/course', 'models/section', 'models/sectionContent', 'models/materials', 'models/singleSelectAnswer',
    'models/singleSelectAnswer', 'models/singleSelectImageAnswer'],
    function (Course, Section, SectionContent, Material, SingleSelectAnswer, MultipleSelectAnswer, SingleSelectImageAnswer) {
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
            }
        };
    });