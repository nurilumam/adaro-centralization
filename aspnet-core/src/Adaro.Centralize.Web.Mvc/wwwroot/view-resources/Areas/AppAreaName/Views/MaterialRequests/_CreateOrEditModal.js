﻿(function ($) {
  app.modals.CreateOrEditMaterialRequestModal = function () {
    var _materialRequestsService = abp.services.app.materialRequests;

    var _modalManager;
    var _$materialRequestInformationForm = null;

    var _MaterialRequestunspscLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MaterialRequests/UNSPSCLookupTableModal',
      scriptUrl:
        abp.appPath +
        'view-resources/Areas/AppAreaName/Views/MaterialRequests/_MaterialRequestUNSPSCLookupTableModal.js',
      modalClass: 'UNSPSCLookupTableModal',
    });
    var _MaterialRequestgeneralLedgerMappingLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MaterialRequests/GeneralLedgerMappingLookupTableModal',
      scriptUrl:
        abp.appPath +
        'view-resources/Areas/AppAreaName/Views/MaterialRequests/_MaterialRequestGeneralLedgerMappingLookupTableModal.js',
      modalClass: 'GeneralLedgerMappingLookupTableModal',
    });
    var _fileUploading = [];
    var _pictureToken;

    this.init = function (modalManager) {
      _modalManager = modalManager;

      var modal = _modalManager.getModal();
      modal.find('.date-picker').daterangepicker({
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      });

      _$materialRequestInformationForm = _modalManager.getModal().find('form[name=MaterialRequestInformationsForm]');
      _$materialRequestInformationForm.validate();
    };

    $('#OpenUNSPSCLookupTableButton').click(function () {
      var materialRequest = _$materialRequestInformationForm.serializeFormToObject();

      _MaterialRequestunspscLookupTableModal.open(
        { id: materialRequest.unspscId, displayName: materialRequest.unspscDisplayProperty },
        function (data) {
          _$materialRequestInformationForm.find('input[name=unspscDisplayProperty]').val(data.displayName);
          _$materialRequestInformationForm.find('input[name=unspscId]').val(data.id);
        }
      );
    });

    $('#ClearUNSPSCDisplayPropertyButton').click(function () {
      _$materialRequestInformationForm.find('input[name=unspscDisplayProperty]').val('');
      _$materialRequestInformationForm.find('input[name=unspscId]').val('');
    });

    $('#OpenGeneralLedgerMappingLookupTableButton').click(function () {
      var materialRequest = _$materialRequestInformationForm.serializeFormToObject();

      _MaterialRequestgeneralLedgerMappingLookupTableModal.open(
        { id: materialRequest.valuationClassId, displayName: materialRequest.generalLedgerMappingDisplayProperty },
        function (data) {
          _$materialRequestInformationForm
            .find('input[name=generalLedgerMappingDisplayProperty]')
            .val(data.displayName);
          _$materialRequestInformationForm.find('input[name=valuationClassId]').val(data.id);
        }
      );
    });

    $('#ClearGeneralLedgerMappingDisplayPropertyButton').click(function () {
      _$materialRequestInformationForm.find('input[name=generalLedgerMappingDisplayProperty]').val('');
      _$materialRequestInformationForm.find('input[name=valuationClassId]').val('');
    });

    this.save = function () {
      if (!_$materialRequestInformationForm.valid()) {
        return;
      }
      if ($('#MaterialRequest_UNSPSCId').prop('required') && $('#MaterialRequest_UNSPSCId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('UNSPSC')));
        return;
      }
      if (
        $('#MaterialRequest_ValuationClassId').prop('required') &&
        $('#MaterialRequest_ValuationClassId').val() == ''
      ) {
        abp.message.error(app.localize('{0}IsRequired', app.localize('GeneralLedgerMapping')));
        return;
      }

      if (_fileUploading != null && _fileUploading.length > 0) {
        abp.notify.info(app.localize('WaitingForFileUpload'));
        return;
      }

      var materialRequest = _$materialRequestInformationForm.serializeFormToObject();

      materialRequest.pictureToken = _pictureToken;

      _modalManager.setBusy(true);
      _materialRequestsService
        .createOrEdit(materialRequest)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          _modalManager.close();
          abp.event.trigger('app.createOrEditMaterialRequestModalSaved');
        })
        .always(function () {
          _modalManager.setBusy(false);
        });
    };

    $('#MaterialRequest_Picture').change(function () {
      var file = $(this)[0].files[0];
      if (!file) {
        _pictureToken = null;
        return;
      }

      var formData = new FormData();
      formData.append('file', file);
      _fileUploading.push(true);

      $.ajax({
        url: '/App/MaterialRequests/UploadPictureFile',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
      })
        .done(function (resp) {
          if (resp.success && resp.result.fileToken) {
            _pictureToken = resp.result.fileToken;
          } else {
            abp.message.error(resp.result.message);
          }
        })
        .always(function () {
          _fileUploading.pop();
        });
    });

    $('#MaterialRequest_Picture_Remove').click(function () {
      abp.message.confirm(app.localize('DoYouWantToRemoveTheFile'), app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          var MaterialRequest = _$materialRequestInformationForm.serializeFormToObject();
          _materialRequestsService
            .removePictureFile({
              id: MaterialRequest.id,
            })
            .done(function () {
              abp.notify.success(app.localize('SuccessfullyDeleted'));
              _$materialRequestInformationForm.find('#div_current_file').css('display', 'none');
            });
        }
      });
    });

    $('#MaterialRequest_Picture').change(function () {
      var fileName = app.localize('ChooseAFile');
      if (this.files && this.files[0]) {
        fileName = this.files[0].name;
      }
      $('#MaterialRequest_PictureLabel').text(fileName);
    });
  };
})(jQuery);
