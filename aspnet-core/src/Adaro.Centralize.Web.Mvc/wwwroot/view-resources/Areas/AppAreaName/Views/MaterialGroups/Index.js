(function () {
  $(function () {
    var _$materialGroupsTable = $('#MaterialGroupsTable');
    var _materialGroupsService = abp.services.app.materialGroups;

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
        getMaterialGroups();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getMaterialGroups();
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
        getMaterialGroups();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getMaterialGroups();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.MaterialGroups.Create'),
      edit: abp.auth.hasPermission('Pages.MaterialGroups.Edit'),
      delete: abp.auth.hasPermission('Pages.MaterialGroups.Delete'),
    };

    var _createOrEditModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MaterialGroups/CreateOrEditModal',
      scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/MaterialGroups/_CreateOrEditModal.js',
      modalClass: 'CreateOrEditMaterialGroupModal',
    });

    var _viewMaterialGroupModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/MaterialGroups/ViewmaterialGroupModal',
      modalClass: 'ViewMaterialGroupModal',
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

    var dataTable = _$materialGroupsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _materialGroupsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#MaterialGroupsTableFilter').val(),
            materialGroupCodeFilter: $('#MaterialGroupCodeFilterId').val(),
            materialGroupNameFilter: $('#MaterialGroupNameFilterId').val(),
            materialGroupDescriptionFilter: $('#MaterialGroupDescriptionFilterId').val(),
            languageFilter: $('#LanguageFilterId').val(),
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
                  _viewMaterialGroupModal.open({ id: data.record.materialGroup.id });
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  _createOrEditModal.open({ id: data.record.materialGroup.id });
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteMaterialGroup(data.record.materialGroup);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'materialGroup.materialGroupCode',
          name: 'materialGroupCode',
        },
        {
          targets: 3,
          data: 'materialGroup.materialGroupName',
          name: 'materialGroupName',
        },
        {
          targets: 4,
          data: 'materialGroup.materialGroupDescription',
          name: 'materialGroupDescription',
        },
        {
          targets: 5,
          data: 'materialGroup.language',
          name: 'language',
        },
      ],
    });

    function getMaterialGroups() {
      dataTable.ajax.reload();
    }

    function deleteMaterialGroup(materialGroup) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _materialGroupsService
            .delete({
              id: materialGroup.id,
            })
            .done(function () {
              getMaterialGroups(true);
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

    $('#CreateNewMaterialGroupButton').click(function () {
      _createOrEditModal.open();
    });

    $('#ExportToExcelButton').click(function () {
      _materialGroupsService
        .getMaterialGroupsToExcel({
          filter: $('#MaterialGroupsTableFilter').val(),
          materialGroupCodeFilter: $('#MaterialGroupCodeFilterId').val(),
          materialGroupNameFilter: $('#MaterialGroupNameFilterId').val(),
          materialGroupDescriptionFilter: $('#MaterialGroupDescriptionFilterId').val(),
          languageFilter: $('#LanguageFilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditMaterialGroupModalSaved', function () {
      getMaterialGroups();
    });

    $('#GetMaterialGroupsButton').click(function (e) {
      e.preventDefault();
      getMaterialGroups();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getMaterialGroups();
      }
    });

    $('.reload-on-change').change(function (e) {
      getMaterialGroups();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getMaterialGroups();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getMaterialGroups();
    });
  });
})();
