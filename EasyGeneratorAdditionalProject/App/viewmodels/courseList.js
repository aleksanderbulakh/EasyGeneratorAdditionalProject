define(['knockout', 'plugins/router', 'IoC/IoC', 'customPlugins/createDialog/createDialog', 'mapper/mapper',
    'customPlugins/customMessages/customMessage', 'errorHandler/errorHandler', 'constants/constants'],
    function (ko, router, IoC, createDialog, mapper, message, errorHandler, constants) {

        function filteringCourseList(courseList, courseName, startDate, endDate, dateIntervalIsNotCorrect) {

            var filteredCourseForName = _.filter(courseList, function (course) {

                var courseTitle = course.title.replace(/\s+/g, "").toLowerCase();
                var filteringCourseTitle = courseName.replace(/\s+/g, "").toLowerCase();

                return courseTitle.indexOf(filteringCourseTitle) !== -1;
            });

            if (dateIntervalIsNotCorrect) {
                return filteredCourseForName;
            }

            var startDateObj = startDate === '' ? new Date('1970-01-01') : new Date(startDate);
            var endDateObj = endDate === '' ? new Date() : new Date(endDate);

            return _.filter(filteredCourseForName, function (course) {
                return course.createdOn >= startDateObj && course.lastModifiedDate <= endDateObj;
            });
        }

        return {
            courseNameFilter: ko.observable(),
            startDateFilter: ko.observable(),
            endDateFilter: ko.observable(),
            courseList: ko.observableArray([]),
            filteredCourseList: false,
            dateIntervalIsNotCorrect: '',
            activate: function () {

                var self = this;
                return IoC.courseRepository.getCourseList()
                    .then(function (data) {

                        self.courseList(_.map(data, function (course) {
                            return {
                                id: course.id,
                                title: course.title,
                                createdOn: course.createdOn,
                                lastModifiedDate: course.lastModifiedDate,
                                createdBy: course.createdBy,
                                modifiedBy: course.modifiedBy,
                                description: course.description
                            };
                        }));

                        self.courseNameFilter('');
                        self.startDateFilter('');
                        self.endDateFilter('');

                        self.dateIntervalIsNotCorrect = ko.computed(function () {
                            
                            var startDateObj = self.startDateFilter() === '' ? new Date('1970-01-01') : new Date(self.startDateFilter());
                            var endDateObj = self.endDateFilter() === '' ? new Date() : new Date(self.endDateFilter());

                            return startDateObj > endDateObj;
                        });

                        self.filteredCourseList = ko.computed(function () {

                            return filteringCourseList(self.courseList(), self.courseNameFilter(), self.startDateFilter(), self.endDateFilter(), self.dateIntervalIsNotCorrect());
                        });
                    });
            },

            routToCoursePreview: function (id) {
                return '#preview/' + id;
            },

            deleteCourse: function (course) {
                var self = this;
                message.confirmMessage()
                    .then(function (result) {
                        if (result) {
                            IoC.courseRepository.deleteCourse(course.id)
                                .then(function () {
                                    self.courseList.valueHasMutated();
                                    message.stateMessage("Course has been deleted.", "Success");
                                });
                        }
                    });
            },

            createCourse: function () {
                createDialog.show()
                    .then(function (response) {
                        IoC.courseRepository.createCourse(response)
                            .then(function (courseId) {
                                router.navigate('#course/' + courseId);
                            });
                    });
            },

            navigateToCourse: function (course) {
                router.navigate('#course/' + course.id);
            }
        };
    });