﻿define(function () {
    function Entity(spec) {
        this.id = spec.id;
        this.title = spec.title;
        this.createdOn = spec.createdOn;
        this.lastModifiedDate = spec.lastModified;
        this.createdBy = spec.createdBy;
        this.modifiedBy = spec.modifiedBy;
    }

    return Entity;
});