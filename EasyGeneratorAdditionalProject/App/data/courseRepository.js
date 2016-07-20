define(['models/course', 'models/section', 'models/sectionContent', 'models/materials', 'models/singleSelectQuestion',
    'models/singleSelectQuestion', 'models/singleSelectImageQuestion', 'http/httpWrapper', 'data/courseContext'],
    function (Course, Section, SectionContent, Material, SSQ, MSQ, SSIQ, http, courseContext) {

        /*function mapContentValue(contentValue, contentType) {
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
        }*/

        return {
            getCourseList: function () {
                return courseContext.courseList;
            },

            createCourse: function () {
                return http.post('course/create').then(function (course) {
                    var courseId = course.Id;
                    courseContext.courseList.push(new Course({
                        id: course.Id,
                        title: course.Title,
                        description: course.Description,
                        createdOn: course.CreatedOn,
                        createdBy: course.CreatedBy,
                        lastModified: course.LastModifiedDate,
                        sectionList: []
                    }));
                    return courseId;
                });
            },

            getCourseById: function (courseId) {
                var course = courseContext.courseList.find(function (item) {
                    if (item.id === courseId)
                        return item.id = courseId;
                });
                return course;
            },

            editCourse: function (courseId, courseTitle, courseDescription) {
                return http.post('course/edit', { Id: courseId, Title: courseTitle, Description: courseDescription }).then(function (result) {
                    if (result) {
                        var course = courseContext.courseList.find(function (item) {
                            if (item.id === courseId)
                                return item.id = courseId;
                        });

                        course.title = courseTitle;
                        course.description = courseDescription;
                        course.lastModified = new Date().toDateString();
                    }

                    return result;
                });
            },

            deleteCourse: function (courseId) {
                return http.post('course/delete', { id: courseId }).then(function (result) {
                    if (result) {
                        var course = courseContext.courseList;
                        var elementId = 0;
                        for (var i = 0; i < course.length; i++) {
                            if (course[i].id !== courseId)
                                continue;

                            elementId = i;
                            break;
                        }

                        courseContext.courseList.splice(elementId, 1);
                    }

                    return result;
                });
            }
        }
    });