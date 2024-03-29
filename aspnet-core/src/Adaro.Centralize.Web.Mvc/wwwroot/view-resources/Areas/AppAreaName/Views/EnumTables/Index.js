﻿(function () {
  $(function () {
    var _$enumTablesTable = $('#EnumTablesTable');
    var _enumTablesService = abp.services.app.enumTables;

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
        getEnumTables();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getEnumTables();
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
        getEnumTables();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getEnumTables();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.EnumTables.Create'),
      edit: abp.auth.hasPermission('Pages.EnumTables.Edit'),
      delete: abp.auth.hasPermission('Pages.EnumTables.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/EnumTables/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EnumTables/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditEnumTableModal',
    });

    var _viewEnumTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/EnumTables/ViewenumTableModal',
      modalClass: 'ViewEnumTableModal',
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

    var dataTable = _$enumTablesTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _enumTablesService.getAll,
        inputFilter: function () {
          return {
            filter: $('#EnumTablesTableFilter').val(),
            enumCodeFilter: $('#EnumCodeFilterId').val(),
            enumValueFilter: $('#EnumValueFilterId').val(),
            enumLabelFilter: $('#EnumLabelFilterId').val(),
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
                  _viewEnumTableModal.open({ id: data.record.enumTable.id });
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.enumTable.id });
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteEnumTable(data.record.enumTable);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'enumTable.enumCode',
          name: 'enumCode',
        },
        {
          targets: 3,
          data: 'enumTable.enumValue',
          name: 'enumValue',
        },
        {
          targets: 4,
          data: 'enumTable.enumLabel',
          name: 'enumLabel',
        },
      ],
    });

    function getEnumTables() {
      dataTable.ajax.reload();
    }

    function deleteEnumTable(enumTable) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _enumTablesService
            .delete({
              id: enumTable.id,
            })
            .done(function () {
              getEnumTables(true);
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

    $('#CreateNewEnumTableButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _enumTablesService
        .getEnumTablesToExcel({
          filter: $('#EnumTablesTableFilter').val(),
          enumCodeFilter: $('#EnumCodeFilterId').val(),
          enumValueFilter: $('#EnumValueFilterId').val(),
          enumLabelFilter: $('#EnumLabelFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditEnumTableModalSaved', function () {
      getEnumTables();
    });

    $('#GetEnumTablesButton').click(function (e) {
      e.preventDefault();
      getEnumTables();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getEnumTables();
      }
    });

    $('.reload-on-change').change(function (e) {
      getEnumTables();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getEnumTables();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getEnumTables();
    });
  });
})();
