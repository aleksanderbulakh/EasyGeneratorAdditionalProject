define(function () {
    function Course(spec) {

        this.id = spec.id;
        this.title = spec.title;
        this.description = spec.description;
        this.createdBy = spec.createdBy;
        this.sectionList = spec.sectionList;
        this.progress = 0;

        this.computeProgress = function () {

            var courseProgress = 0;

            _.each(this.sectionList, function (section) {
                section.computeProgress();
                courseProgress += section.progress;
            });

            this.progress = courseProgress / this.sectionList.length;
        }
    }

    return Course;
});