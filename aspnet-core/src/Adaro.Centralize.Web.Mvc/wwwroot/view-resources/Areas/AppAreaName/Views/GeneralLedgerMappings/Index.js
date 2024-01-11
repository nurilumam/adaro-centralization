(function () {
  $(function () {
    var _$generalLedgerMappingsTable = $('#GeneralLedgerMappingsTable');
    var _generalLedgerMappingsService = abp.services.app.generalLedgerMappings;

    var $selectedDate = {
      startDate: null,
      endDate: null,
    };

    $('.date-picker').on('apply.daterangepicker', function (ev, picker) {
      $(this).val(picker.startDate.format('MM/DD/YYYY'));
    });

    $('.startDate')
      .daterangepicker({
        autoUpdateInput: false,
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      })
      .on('apply.daterangepicker', (ev, picker) => {
        $selectedDate.startDate = picker.startDate;
        getGeneralLedgerMappings();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getGeneralLedgerMappings();
      });

    $('.endDate')
      .daterangepicker({
        autoUpdateInput: false,
        singleDatePicker: true,
        locale: abp.localization.currentLanguage.name,
        format: 'L',
      })
      .on('apply.daterangepicker', (ev, picker) => {
        $selectedDate.endDate = picker.startDate;
        getGeneralLedgerMappings();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getGeneralLedgerMappings();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.GeneralLedgerMappings.Create'),
      edit: abp.auth.hasPermission('Pages.GeneralLedgerMappings.Edit'),
      delete: abp.auth.hasPermission('Pages.GeneralLedgerMappings.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/GeneralLedgerMappings/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/GeneralLedgerMappings/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditGeneralLedgerMappingModal',
    });

    var _viewGeneralLedgerMappingModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/GeneralLedgerMappings/ViewgeneralLedgerMappingModal',
      modalClass: 'ViewGeneralLedgerMappingModal',
    });

    var getDateFilter = function (element) {
      if ($selectedDate.startDate == null) {
        return null;
      }
      return $selectedDate.startDate.format('YYYY-MM-DDT00:00:00Z');
    };

    var getMaxDateFilter = function (element) {
      if ($selectedDate.endDate == null) {
        return null;
      }
      return $selectedDate.endDate.format('YYYY-MM-DDT23:59:59Z');
    };

    var dataTable = _$generalLedgerMappingsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _generalLedgerMappingsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#GeneralLedgerMappingsTableFilter').val(),
            gLAccountFilter: $('#GLAccountFilterId').val(),
            gLAccountDescriptionFilter: $('#GLAccountDescriptionFilterId').val(),
            mappingTypeFilter: $('#MappingTypeFilterId').val(),
            valuationClassFilter: $('#ValuationClassFilterId').val(),
            valuationClassDescriptionFilter: $('#ValuationClassDescriptionFilterId').val(),
          };
        },
      },
      columnDefs: [
        {
          className: 'control responsive',
          orderable: false,
          render: function () {
            return '';
          },
          targets: 0,
        },
        {
          width: 120,
          targets: 1,
          data: null,
          orderable: false,
          autoWidth: false,
          defaultContent: '',
          rowAction: {
            cssClass: 'btn btn-brand dropdown-toggle',
            text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
            items: [
              {
                text: app.localize('View'),
                action: function (data) {
                  _viewGeneralLedgerMappingModal.open({ id: data.record.generalLedgerMapping.id });
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.generalLedgerMapping.id });
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteGeneralLedgerMapping(data.record.generalLedgerMapping);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'generalLedgerMapping.glAccount',
          name: 'glAccount',
        },
        {
          targets: 3,
          data: 'generalLedgerMapping.glAccountDescription',
          name: 'glAccountDescription',
        },
        {
          targets: 4,
          data: 'generalLedgerMapping.mappingType',
          name: 'mappingType',
        },
        {
          targets: 5,
          data: 'generalLedgerMapping.valuationClass',
          name: 'valuationClass',
        },
        {
          targets: 6,
          data: 'generalLedgerMapping.valuationClassDescription',
          name: 'valuationClassDescription',
        },
      ],
    });

    function getGeneralLedgerMappings() {
      dataTable.ajax.reload();
    }

    function deleteGeneralLedgerMapping(generalLedgerMapping) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _generalLedgerMappingsService
            .delete({
              id: generalLedgerMapping.id,
            })
            .done(function () {
              getGeneralLedgerMappings(true);
              abp.notify.success(app.localize('SuccessfullyDeleted'));
            });
        }
      });
    }

    $('#ShowAdvancedFiltersSpan').click(function () {
      $('#ShowAdvancedFiltersSpan').hide();
      $('#HideAdvancedFiltersSpan').show();
      $('#AdvacedAuditFiltersArea').slideDown();
    });

    $('#HideAdvancedFiltersSpan').click(function () {
      $('#HideAdvancedFiltersSpan').hide();
      $('#ShowAdvancedFiltersSpan').show();
      $('#AdvacedAuditFiltersArea').slideUp();
    });

    $('#CreateNewGeneralLedgerMappingButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _generalLedgerMappingsService
        .getGeneralLedgerMappingsToExcel({
          filter: $('#GeneralLedgerMappingsTableFilter').val(),
          gLAccountFilter: $('#GLAccountFilterId').val(),
          gLAccountDescriptionFilter: $('#GLAccountDescriptionFilterId').val(),
          mappingTypeFilter: $('#MappingTypeFilterId').val(),
          valuationClassFilter: $('#ValuationClassFilterId').val(),
          valuationClassDescriptionFilter: $('#ValuationClassDescriptionFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditGeneralLedgerMappingModalSaved', function () {
      getGeneralLedgerMappings();
    });

    $('#GetGeneralLedgerMappingsButton').click(function (e) {
      e.preventDefault();
      getGeneralLedgerMappings();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getGeneralLedgerMappings();
      }
    });

    $('.reload-on-change').change(function (e) {
      getGeneralLedgerMappings();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getGeneralLedgerMappings();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getGeneralLedgerMappings();
    });
  });
})();
