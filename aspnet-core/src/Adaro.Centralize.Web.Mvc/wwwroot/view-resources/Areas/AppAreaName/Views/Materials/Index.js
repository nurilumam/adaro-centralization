(function () {
  $(function () {
    var _$materialsTable = $('#MaterialsTable');
    var _materialsService = abp.services.app.materials;

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
        getMaterials();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getMaterials();
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
        getMaterials();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getMaterials();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.Materials.Create'),
      edit: abp.auth.hasPermission('Pages.Materials.Edit'),
      delete: abp.auth.hasPermission('Pages.Materials.Delete'),
    };

    var _viewMaterialModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/Materials/ViewmaterialModal',
      modalClass: 'ViewMaterialModal',
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

    var dataTable = _$materialsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _materialsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#MaterialsTableFilter').val(),
            materialNoFilter: $('#MaterialNoFilterId').val(),
            materialNameFilter: $('#MaterialNameFilterId').val(),
            materialNameSAPFilter: $('#MaterialNameSAPFilterId').val(),
            descriptionFilter: $('#DescriptionFilterId').val(),
            sizeFilter: $('#SizeFilterId').val(),
            uoMFilter: $('#UoMFilterId').val(),
            brandFilter: $('#BrandFilterId').val(),
            imageMainFilter: $('#ImageMainFilterId').val(),
            materialGroupDisplayPropertyFilter: $('#MaterialGroupDisplayPropertyFilterId').val(),
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
                  window.location = '/AppAreaName/Materials/ViewMaterial/' + data.record.material.id;
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  window.location = '/AppAreaName/Materials/CreateOrEdit/' + data.record.material.id;
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteMaterial(data.record.material);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'material.materialNo',
          name: 'materialNo',
        },
        {
          targets: 3,
          data: 'material.materialName',
          name: 'materialName',
        },
        {
          targets: 4,
          data: 'material.description',
          name: 'description',
        },
        {
          targets: 5,
          data: 'material.uoM',
          name: 'uoM',
        },
        {
          targets: 6,
          data: 'material',
          render: function (material) {
            if (!material.imageMain) {
              return '';
            }
            return `<a href="/File/DownloadBinaryFile?id=${material.imageMain}" target="_blank">${material.imageMainFileName}</a>`;
          },
        },
        {
          targets: 7,
          data: 'materialGroupDisplayProperty',
          name: 'materialGroupFk.displayProperty',
        },
        {
          targets: 8,
          data: 'unspscDisplayProperty',
          name: 'unspscFk.displayProperty',
        },
        {
          targets: 9,
          data: 'generalLedgerMappingDisplayProperty',
          name: 'generalLedgerMappingFk.displayProperty',
        },
      ],
    });

    function getMaterials() {
      dataTable.ajax.reload();
    }

    function deleteMaterial(material) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _materialsService
            .delete({
              id: material.id,
            })
            .done(function () {
              getMaterials(true);
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
      _materialsService
        .getMaterialsToExcel({
          filter: $('#MaterialsTableFilter').val(),
          materialNoFilter: $('#MaterialNoFilterId').val(),
          materialNameFilter: $('#MaterialNameFilterId').val(),
          materialNameSAPFilter: $('#MaterialNameSAPFilterId').val(),
          descriptionFilter: $('#DescriptionFilterId').val(),
          sizeFilter: $('#SizeFilterId').val(),
          uoMFilter: $('#UoMFilterId').val(),
          brandFilter: $('#BrandFilterId').val(),
          imageMainFilter: $('#ImageMainFilterId').val(),
          materialGroupDisplayPropertyFilter: $('#MaterialGroupDisplayPropertyFilterId').val(),
          unspscDisplayPropertyFilter: $('#UNSPSCDisplayPropertyFilterId').val(),
          generalLedgerMappingDisplayPropertyFilter: $('#GeneralLedgerMappingDisplayPropertyFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditMaterialModalSaved', function () {
      getMaterials();
    });

    $('#GetMaterialsButton').click(function (e) {
      e.preventDefault();
      getMaterials();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getMaterials();
      }
    });

    $('.reload-on-change').change(function (e) {
      getMaterials();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getMaterials();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getMaterials();
    });
  });
})();
