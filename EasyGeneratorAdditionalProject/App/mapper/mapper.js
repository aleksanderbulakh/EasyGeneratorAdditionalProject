define(['models/course', 'models/section', 'models/sectionContent', 'models/materials', 'models/singleSelectAnswer',
    'models/singleSelectAnswer', 'models/singleSelectImageAnswer'],
    function (Course, Section, SectionContent, Material, SingleSelectAnswer, MultipleSelectAnswer, SingleSelectImageAnswer) {
        return {
            mapCourse: function (spec) {
                return new Course({
                    id: spec.Id,
                    title: spec.Title,
                    description: spec.Description,
                    createdOn: spec.CreatedOn,
                    createdBy: spec.CreatedBy,
                    lastModified: spec.LastModifiedDate,
                    sectionList: []
                });
            }
        }
    });