ko.bindingHandlers.editableHTML = {
  init: function(element, valueAccessor) {
    var $element = $(element);
    var initialValue = ko.utils.unwrapObservable(valueAccessor());
    $element.html(initialValue);
    $element.on('keyup', function() {
      observable = valueAccessor();
      observable($element.html());
    });
  }
};