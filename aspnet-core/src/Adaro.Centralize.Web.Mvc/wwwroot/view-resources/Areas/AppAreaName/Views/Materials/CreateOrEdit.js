(function () {
  $(function () {
    var _materialsService = abp.services.app.materials;

    var _$materialInformationForm = $('form[name=MaterialInformationsForm]');
    _$materialInformationForm.validate();

    var _MaterialmaterialGroupLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/Materials/MaterialGroupLookupTableModal',
      scriptUrl:
        abp.appPath + 'view-resources/Areas/AppAreaName/Views/Materials/_MaterialMaterialGroupLookupTableModal.js',
      modalClass: 'MaterialGroupLookupTableModal',
    });
    var _MaterialunspscLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/Materials/UNSPSCLookupTableModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/Materials/_MaterialUNSPSCLookupTableModal.js',
      modalClass: 'UNSPSCLookupTableModal',
    });
    var _MaterialgeneralLedgerMappingLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/Materials/GeneralLedgerMappingLookupTableModal',
      scriptUrl:
        abp.appPath +
        'view-resources/Areas/AppAreaName/Views/Materials/_MaterialGeneralLedgerMappingLookupTableModal.js',
      modalClass: 'GeneralLedgerMappingLookupTableModal',
    });
    var _fileUploading = [];
    var _imageMainToken;

    $('.date-picker').daterangepicker({
      singleDatePicker: true,
      locale: abp.localization.currentLanguage.name,
      format: 'L',
    });

    $('#OpenMaterialGroupLookupTableButton').click(function () {
      var material = _$materialInformationForm.serializeFormToObject();

      _MaterialmaterialGroupLookupTableModal.open(
        { id: material.materialGroupId, displayName: material.materialGroupDisplayProperty },
        function (data) {
          _$materialInformationForm.find('input[name=materialGroupDisplayProperty]').val(data.displayName);
          _$materialInformationForm.find('input[name=materialGroupId]').val(data.id);
        }
      );
    });

    $('#ClearMaterialGroupDisplayPropertyButton').click(function () {
      _$materialInformationForm.find('input[name=materialGroupDisplayProperty]').val('');
      _$materialInformationForm.find('input[name=materialGroupId]').val('');
    });

    $('#OpenUNSPSCLookupTableButton').click(function () {
      var material = _$materialInformationForm.serializeFormToObject();

      _MaterialunspscLookupTableModal.open(
        { id: material.unspscId, displayName: material.unspscDisplayProperty },
        function (data) {
          _$materialInformationForm.find('input[name=unspscDisplayProperty]').val(data.displayName);
          _$materialInformationForm.find('input[name=unspscId]').val(data.id);
        }
      );
    });

    $('#ClearUNSPSCDisplayPropertyButton').click(function () {
      _$materialInformationForm.find('input[name=unspscDisplayProperty]').val('');
      _$materialInformationForm.find('input[name=unspscId]').val('');
    });

    $('#OpenGeneralLedgerMappingLookupTableButton').click(function () {
      var material = _$materialInformationForm.serializeFormToObject();

      _MaterialgeneralLedgerMappingLookupTableModal.open(
        { id: material.generalLedgerMappingId, displayName: material.generalLedgerMappingDisplayProperty },
        function (data) {
          _$materialInformationForm.find('input[name=generalLedgerMappingDisplayProperty]').val(data.displayName);
          _$materialInformationForm.find('input[name=generalLedgerMappingId]').val(data.id);
        }
      );
    });

    $('#ClearGeneralLedgerMappingDisplayPropertyButton').click(function () {
      _$materialInformationForm.find('input[name=generalLedgerMappingDisplayProperty]').val('');
      _$materialInformationForm.find('input[name=generalLedgerMappingId]').val('');
    });

    function save(successCallback) {
      if (!_$materialInformationForm.valid()) {
        return;
      }
      if ($('#Material_MaterialGroupId').prop('required') && $('#Material_MaterialGroupId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('MaterialGroup')));
        return;
      }
      if ($('#Material_UNSPSCId').prop('required') && $('#Material_UNSPSCId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('UNSPSC')));
        return;
      }
      if ($('#Material_GeneralLedgerMappingId').prop('required') && $('#Material_GeneralLedgerMappingId').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('GeneralLedgerMapping')));
        return;
      }

      if (_fileUploading != null && _fileUploading.length > 0) {
        abp.notify.info(app.localize('WaitingForFileUpload'));
        return;
      }

      var material = _$materialInformationForm.serializeFormToObject();

      material.imageMainToken = _imageMainToken;

      abp.ui.setBusy();
      _materialsService
        .createOrEdit(material)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          abp.event.trigger('app.createOrEditMaterialModalSaved');

          if (typeof successCallback === 'function') {
            successCallback();
          }
        })
        .always(function () {
          abp.ui.clearBusy();
        });
    }

    function clearForm() {
      _$materialInformationForm[0].reset();
    }

    $('#saveBtn').click(function () {
      save(function () {
        window.location = '/AppAreaName/Materials';
      });
    });

    $('#saveAndNewBtn').click(function () {
      save(function () {
        if (!$('input[name=id]').val()) {
          //if it is create page
          clearForm();
        }
      });
    });

    $('#Material_ImageMain').change(function () {
      var file = $(this)[0].files[0];
      if (!file) {
        _imageMainToken = null;
        return;
      }

      var formData = new FormData();
      formData.append('file', file);
      _fileUploading.push(true);

      $.ajax({
        url: '/App/Materials/UploadImageMainFile',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
      })
        .done(function (resp) {
          if (resp.success && resp.result.fileToken) {
            _imageMainToken = resp.result.fileToken;
          } else {
            abp.message.error(resp.result.message);
          }
        })
        .always(function () {
          _fileUploading.pop();
        });
    });

    $('#Material_ImageMain_Remove').click(function () {
      abp.message.confirm(app.localize('DoYouWantToRemoveTheFile'), app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          var Material = _$materialInformationForm.serializeFormToObject();
          _materialsService
            .removeImageMainFile({
              id: Material.id,
            })
            .done(function () {
              abp.notify.success(app.localize('SuccessfullyDeleted'));
              _$materialInformationForm.find('#div_current_file').css('display', 'none');
            });
        }
      });
    });

    $('#Material_ImageMain').change(function () {
      var fileName = app.localize('ChooseAFile');
      if (this.files && this.files[0]) {
        fileName = this.files[0].name;
      }
      $('#Material_ImageMainLabel').text(fileName);
    });
  });
})();
