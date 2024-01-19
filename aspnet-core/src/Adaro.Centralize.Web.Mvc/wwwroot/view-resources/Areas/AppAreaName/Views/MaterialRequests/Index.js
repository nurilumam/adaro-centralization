(function () {
  $(function () {
    var _$materialRequestsTable = $('#MaterialRequestsTable');
    var _materialRequestsService = abp.services.app.materialRequests;

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
        getMaterialRequests();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getMaterialRequests();
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
        getMaterialRequests();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getMaterialRequests();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.MaterialRequests.Create'),
      edit: abp.auth.hasPermission('Pages.MaterialRequests.Edit'),
      delete: abp.auth.hasPermission('Pages.MaterialRequests.Delete'),
    };

    var _viewMaterialRequestModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MaterialRequests/ViewmaterialRequestModal',
      modalClass: 'ViewMaterialRequestModal',
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

    var dataTable = _$materialRequestsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _materialRequestsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#MaterialRequestsTableFilter').val(),
            requestNoFilter: $('#RequestNoFilterId').val(),
            requestStatusFilter: $('#RequestStatusFilterId').val(),
            materialNameFilter: $('#MaterialNameFilterId').val(),
            descriptionFilter: $('#DescriptionFilterId').val(),
            purposeFilter: $('#PurposeFilterId').val(),
            materialTypeFilter: $('#MaterialTypeFilterId').val(),
            categoryFilter: $('#CategoryFilterId').val(),
            sizeFilter: $('#SizeFilterId').val(),
            uoMFilter: $('#UoMFilterId').val(),
            packageSizeFilter: $('#PackageSizeFilterId').val(),
            generalLedgerFilter: $('#GeneralLedgerFilterId').val(),
            brandFilter: $('#BrandFilterId').val(),
            weightFilter: $('#WeightFilterId').val(),
            pictureFilter: $('#PictureFilterId').val(),
            imageColletionFilter: $('#ImageColletionFilterId').val(),
            unspscDisplayPropertyFilter: $('#UNSPSCDisplayPropertyFilterId').val(),
            generalLedgerMappingDisplayPropertyFilter: $('#GeneralLedgerMappingDisplayPropertyFilterId').val(),
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
                  window.location =
                    '/AppAreaName/MaterialRequests/ViewMaterialRequest/' + data.record.materialRequest.id;
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  window.location = '/AppAreaName/MaterialRequests/CreateOrEdit/' + data.record.materialRequest.id;
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteMaterialRequest(data.record.materialRequest);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'materialRequest.requestNo',
          name: 'requestNo',
        },
        {
          targets: 3,
          data: 'materialRequest.requestStatus',
          name: 'requestStatus',
          render: function (requestStatus) {
            return app.localize('Enum_MaterialRequestStatus_' + requestStatus);
          },
        },
        {
          targets: 4,
          data: 'materialRequest.materialName',
          name: 'materialName',
        },
        {
          targets: 5,
          data: 'materialRequest.description',
          name: 'description',
        },
        {
          targets: 6,
          data: 'materialRequest.generalLedger',
          name: 'generalLedger',
        },
        {
          targets: 7,
          data: 'materialRequest',
          render: function (materialRequest) {
            if (!materialRequest.picture) {
              return '';
            }
            return `<a href="/File/DownloadBinaryFile?id=${materialRequest.picture}" target="_blank">${materialRequest.pictureFileName}</a>`;
          },
        },
        {
          targets: 8,
          data: 'unspscDisplayProperty',
          name: 'unspscFk.displayProperty',
        },
        {
          targets: 9,
          data: 'generalLedgerMappingDisplayProperty',
          name: 'valuationClassFk.displayProperty',
        },
      ],
    });

    function getMaterialRequests() {
      dataTable.ajax.reload();
    }

    function deleteMaterialRequest(materialRequest) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _materialRequestsService
            .delete({
              id: materialRequest.id,
            })
            .done(function () {
              getMaterialRequests(true);
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

    $('#ExportToExcelButton').click(function () {
      _materialRequestsService
        .getMaterialRequestsToExcel({
          filter: $('#MaterialRequestsTableFilter').val(),
          requestNoFilter: $('#RequestNoFilterId').val(),
          requestStatusFilter: $('#RequestStatusFilterId').val(),
          materialNameFilter: $('#MaterialNameFilterId').val(),
          descriptionFilter: $('#DescriptionFilterId').val(),
          purposeFilter: $('#PurposeFilterId').val(),
          materialTypeFilter: $('#MaterialTypeFilterId').val(),
          categoryFilter: $('#CategoryFilterId').val(),
          sizeFilter: $('#SizeFilterId').val(),
          uoMFilter: $('#UoMFilterId').val(),
          packageSizeFilter: $('#PackageSizeFilterId').val(),
          generalLedgerFilter: $('#GeneralLedgerFilterId').val(),
          brandFilter: $('#BrandFilterId').val(),
          weightFilter: $('#WeightFilterId').val(),
          pictureFilter: $('#PictureFilterId').val(),
          imageColletionFilter: $('#ImageColletionFilterId').val(),
          unspscDisplayPropertyFilter: $('#UNSPSCDisplayPropertyFilterId').val(),
          generalLedgerMappingDisplayPropertyFilter: $('#GeneralLedgerMappingDisplayPropertyFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditMaterialRequestModalSaved', function () {
      getMaterialRequests();
    });

    $('#GetMaterialRequestsButton').click(function (e) {
      e.preventDefault();
      getMaterialRequests();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getMaterialRequests();
      }
    });

    $('.reload-on-change').change(function (e) {
      getMaterialRequests();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getMaterialRequests();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getMaterialRequests();
    });
  });
})();
