(function ($) {
  app.modals.CreateOrEditAirportModal = function () {
    var _airportsService = abp.services.app.airports;

    var _modalManager;
    var _$airportInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$airportInformationForm = _modalManager.getModal().find('form[name=AirportInformationsForm]');
      _$airportInformationForm.validate();
    };

    this.save = function () {
      if (!_$airportInformationForm.valid()) {
        return;
      }

      var airport = _$airportInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _airportsService
        .createOrEdit(airport)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditAirportModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
