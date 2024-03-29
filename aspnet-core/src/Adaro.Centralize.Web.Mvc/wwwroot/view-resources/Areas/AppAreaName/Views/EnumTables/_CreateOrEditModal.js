﻿(function ($) {
  app.modals.CreateOrEditEnumTableModal = function () {
    var _enumTablesService = abp.services.app.enumTables;

    var _modalManager;
    var _$enumTableInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$enumTableInformationForm = _modalManager.getModal().find('form[name=EnumTableInformationsForm]');
      _$enumTableInformationForm.validate();
    };

    this.save = function () {
      if (!_$enumTableInformationForm.valid()) {
        return;
      }

      var enumTable = _$enumTableInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _enumTablesService
        .createOrEdit(enumTable)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditEnumTableModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
