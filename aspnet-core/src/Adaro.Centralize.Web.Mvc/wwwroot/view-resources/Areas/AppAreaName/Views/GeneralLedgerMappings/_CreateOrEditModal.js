(function ($) {
  app.modals.CreateOrEditGeneralLedgerMappingModal = function () {
    var _generalLedgerMappingsService = abp.services.app.generalLedgerMappings;

    var _modalManager;
    var _$generalLedgerMappingInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$generalLedgerMappingInformationForm = _modalManager
        .getModal()
        .find('form[name=GeneralLedgerMappingInformationsForm]');
      _$generalLedgerMappingInformationForm.validate();
    };

    this.save = function () {
      if (!_$generalLedgerMappingInformationForm.valid()) {
        return;
      }

      var generalLedgerMapping = _$generalLedgerMappingInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _generalLedgerMappingsService
        .createOrEdit(generalLedgerMapping)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditGeneralLedgerMappingModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
