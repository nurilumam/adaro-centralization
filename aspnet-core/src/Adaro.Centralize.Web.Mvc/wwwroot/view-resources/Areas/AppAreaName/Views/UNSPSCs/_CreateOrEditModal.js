﻿(function ($) {
  app.modals.CreateOrEditUNSPSCModal = function () {
    var _unspsCsService = abp.services.app.uNSPSCs;

    var _modalManager;
    var _$unspscInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$unspscInformationForm = _modalManager.getModal().find('form[name=UNSPSCInformationsForm]');
      _$unspscInformationForm.validate();
    };

    this.save = function () {
      if (!_$unspscInformationForm.valid()) {
        return;
      }

      var unspsc = _$unspscInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _unspsCsService
        .createOrEdit(unspsc)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditUNSPSCModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
