﻿(function () {
  $(function () {
    var _$unspsCsTable = $('#UNSPSCsTable');
    var _unspsCsService = abp.services.app.uNSPSCs;

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
        getUNSPSCs();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getUNSPSCs();
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
        getUNSPSCs();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getUNSPSCs();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.UNSPSCs.Create'),
      edit: abp.auth.hasPermission('Pages.UNSPSCs.Edit'),
      delete: abp.auth.hasPermission('Pages.UNSPSCs.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/UNSPSCs/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/UNSPSCs/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditUNSPSCModal',
    });

    var _viewUNSPSCModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/UNSPSCs/ViewunspscModal',
      modalClass: 'ViewUNSPSCModal',
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

    var dataTable = _$unspsCsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _unspsCsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#UNSPSCsTableFilter').val(),
            uNSPSC_CodeFilter: $('#UNSPSC_CodeFilterId').val(),
            descriptionFilter: $('#DescriptionFilterId').val(),
            accountCodeFilter: $('#AccountCodeFilterId').val(),
            descriptionIdFilter: $('#DescriptionIdFilterId').val(),
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
                  _viewUNSPSCModal.open({ id: data.record.unspsc.id });
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.unspsc.id });
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteUNSPSC(data.record.unspsc);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'unspsc.unspsC_Code',
          name: 'unspsC_Code',
        },
        {
          targets: 3,
          data: 'unspsc.description',
          name: 'description',
        },
        {
          targets: 4,
          data: 'unspsc.accountCode',
          name: 'accountCode',
        },
      ],
    });

    function getUNSPSCs() {
      dataTable.ajax.reload();
    }

    function deleteUNSPSC(unspsc) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _unspsCsService
            .delete({
              id: unspsc.id,
            })
            .done(function () {
              getUNSPSCs(true);
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

    $('#CreateNewUNSPSCButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _unspsCsService
        .getUNSPSCsToExcel({
          filter: $('#UNSPSCsTableFilter').val(),
          uNSPSC_CodeFilter: $('#UNSPSC_CodeFilterId').val(),
          descriptionFilter: $('#DescriptionFilterId').val(),
          accountCodeFilter: $('#AccountCodeFilterId').val(),
          descriptionIdFilter: $('#DescriptionIdFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditUNSPSCModalSaved', function () {
      getUNSPSCs();
    });

    $('#GetUNSPSCsButton').click(function (e) {
      e.preventDefault();
      getUNSPSCs();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getUNSPSCs();
      }
    });

    $('.reload-on-change').change(function (e) {
      getUNSPSCs();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getUNSPSCs();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getUNSPSCs();
    });
  });
})();
