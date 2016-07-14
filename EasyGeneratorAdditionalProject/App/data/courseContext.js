define(['models/course', 'models/section', 'models/sectionContent', 'models/materials', 'models/singleSelectQuestion', 'models/singleSelectQuestion', 'models/singleSelectImageQuestion', 'http/httpWrapper'],
    function (Course, Section, SectionContent, Material, SSQ, MSQ, SSIQ, http) {

        function mapContentValue(contentValue, contentType) {
            switch (contentType) {
                case "material":
                    return new Material({
                        id: contentValue.Id,
                        text: contentValue.Text
                    });
                case "single":
                    return new SSQ({
                        id: contentValue.Id,
                        text: contentValue.Text,
                        isAnswer: contentValue.IsAnswer
                    });
                case "multiple":
                    return new MSQ({
                        id: contentValue.Id,
                        text: contentValue.Text,
                        isAnswer: contentValue.IsAnswer
                    });

                case "single_image":
                    return new SSIQ({
                        id: contentValue.Id,
                        text: contentValue.Text,
                        isAnswer: contentValue.IsAnswer,
                        photo: contentValue.Photo
                    });
                default:
                    return null;
            }
        }

        function mapContent(contentList) {
            var contentValues = [];
            contentList.Content.forEach(function (contentValue) {
                contentValues.push(mapContentValue(contentValue, contentList.Type));
            });

            return new SectionContent({
                id: contentList.Id,
                title: contentList.Title,
                type: contentList.Type,
                contentValueList: contentValues
            });
        }

        function mapSection(sectionList) {
            var contents = [];
            sectionList.ContentList.forEach(function (content) {
                contents.push(mapContent(content));
            });

            return new Section({
                id: sectionList.Id,
                title: sectionList.Title,
                createdBy: sectionList.CreatedBy,
                createdOn: sectionList.CreatedOn,
                lastModified: sectionList.LastModifiedDate,
                contentList: contents
            });
        }

        function mapCourse(course) {
            var sections = [];
            course.SectionsList.forEach(function (section) {
                sections.push(mapSection(section));
            });

            return new Course({
                id: course.Id,
                title: course.Title,
                description: course.Description,
                createdOn: course.CreatedOn,
                createdBy: course.CreatedBy,
                lastModified: course.LastModifiedDate,
                sectionList: sections
            });
        }

        function initialize() {
            var self = this;
            return http.post('courses').then(function (data) {
                data.forEach(function (course) {
                    self.courseList.push(mapCourse(course));
                });
            });
        }

        return {
            initialize: initialize,
            courseList: []
        }
    });