(function ($) {
  app.modals.CreateOrEditMaterialGroupModal = function () {
    var _materialGroupsService = abp.services.app.materialGroups;

    var _modalManager;
    var _$materialGroupInformationForm = null;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$materialGroupInformationForm = _modalManager.getModal().find('form[name=MaterialGroupInformationsForm]');
      _$materialGroupInformationForm.validate();
    };

    this.save = function () {
      if (!_$materialGroupInformationForm.valid()) {
        return;
      }

      var materialGroup = _$materialGroupInformationForm.serializeFormToObject();

      _modalManager.setBusy(true);
      _materialGroupsService
        .createOrEdit(materialGroup)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditMaterialGroupModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };
  };
})(jQuery);
