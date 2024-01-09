(function () {
  $(function () {
    var _$travelRequestsTable = $('#TravelRequestsTable');
    var _travelRequestsService = abp.services.app.travelRequests;

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
        getTravelRequests();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.startDate = null;
        getTravelRequests();
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
        getTravelRequests();
      })
      .on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
        $selectedDate.endDate = null;
        getTravelRequests();
      });

    var _permissions = {
      create: abp.auth.hasPermission('Pages.TravelRequests.Create'),
      edit: abp.auth.hasPermission('Pages.TravelRequests.Edit'),
      delete: abp.auth.hasPermission('Pages.TravelRequests.Delete'),
    };

    var _viewTravelRequestModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/TravelRequests/ViewtravelRequestModal',
      modalClass: 'ViewTravelRequestModal',
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

    var dataTable = _$travelRequestsTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _travelRequestsService.getAll,
        inputFilter: function () {
          return {
            filter: $('#TravelRequestsTableFilter').val(),
            requestNoFilter: $('#RequestNoFilterId').val(),
            travelStatusFilter: $('#TravelStatusFilterId').val(),
            travelTypeFilter: $('#TravelTypeFilterId').val(),
            minRequestDateFilter: getDateFilter($('#MinRequestDateFilterId')),
            maxRequestDateFilter: getMaxDateFilter($('#MaxRequestDateFilterId')),
            minRequestPlanDateFilter: getDateFilter($('#MinRequestPlanDateFilterId')),
            maxRequestPlanDateFilter: getMaxDateFilter($('#MaxRequestPlanDateFilterId')),
            campFilter: $('#CampFilterId').val(),
            transportBusFilter: $('#TransportBusFilterId').val(),
            minCreatedDateFilter: getDateFilter($('#MinCreatedDateFilterId')),
            maxCreatedDateFilter: getMaxDateFilter($('#MaxCreatedDateFilterId')),
            userNameFilter: $('#UserNameFilterId').val(),
            airportDisplayPropertyFilter: $('#AirportDisplayPropertyFilterId').val(),
            airportDisplayProperty2Filter: $('#AirportDisplayProperty2FilterId').val(),
            userName2Filter: $('#UserName2FilterId').val(),
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
                  window.location = '/AppAreaName/TravelRequests/ViewTravelRequest/' + data.record.travelRequest.id;
                },
              },
              {
                text: app.localize('Edit'),
                visible: function () {
                  return _permissions.edit;
                },
                action: function (data) {
                  window.location = '/AppAreaName/TravelRequests/CreateOrEdit/' + data.record.travelRequest.id;
                },
              },
              {
                text: app.localize('Delete'),
                visible: function () {
                  return _permissions.delete;
                },
                action: function (data) {
                  deleteTravelRequest(data.record.travelRequest);
                },
              },
            ],
          },
        },
        {
          targets: 2,
          data: 'travelRequest.requestNo',
          name: 'requestNo',
        },
        {
          targets: 3,
          data: 'travelRequest.travelStatus',
          name: 'travelStatus',
          render: function (travelStatus) {
            return app.localize('Enum_TravelStatus_' + travelStatus);
          },
        },
        {
          targets: 4,
          data: 'travelRequest.travelType',
          name: 'travelType',
          render: function (travelType) {
            return app.localize('Enum_TravelType_' + travelType);
          },
        },
        {
          targets: 5,
          data: 'travelRequest.requestDate',
          name: 'requestDate',
          render: function (requestDate) {
            if (requestDate) {
              return moment(requestDate).format('L');
            }
            return '';
          },
        },
        {
          targets: 6,
          data: 'travelRequest.camp',
          name: 'camp',
        },
        {
          targets: 7,
          data: 'travelRequest.transportBus',
          name: 'transportBus',
        },
        {
          targets: 8,
          data: 'userName',
          name: 'userTravelFk.name',
        },
        {
          targets: 9,
          data: 'airportDisplayProperty',
          name: 'airportFromFk.displayProperty',
        },
        {
          targets: 10,
          data: 'airportDisplayProperty2',
          name: 'airportToFk.displayProperty',
        },
        {
          targets: 11,
          data: 'userName2',
          name: 'createdByFk.name',
        },
      ],
    });

    function getTravelRequests() {
      dataTable.ajax.reload();
    }

    function deleteTravelRequest(travelRequest) {
      abp.message.confirm('', app.localize('AreYouSure'), function (isConfirmed) {
        if (isConfirmed) {
          _travelRequestsService
            .delete({
              id: travelRequest.id,
            })
            .done(function () {
              getTravelRequests(true);
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
      _travelRequestsService
        .getTravelRequestsToExcel({
          filter: $('#TravelRequestsTableFilter').val(),
          requestNoFilter: $('#RequestNoFilterId').val(),
          travelStatusFilter: $('#TravelStatusFilterId').val(),
          travelTypeFilter: $('#TravelTypeFilterId').val(),
          minRequestDateFilter: getDateFilter($('#MinRequestDateFilterId')),
          maxRequestDateFilter: getMaxDateFilter($('#MaxRequestDateFilterId')),
          minRequestPlanDateFilter: getDateFilter($('#MinRequestPlanDateFilterId')),
          maxRequestPlanDateFilter: getMaxDateFilter($('#MaxRequestPlanDateFilterId')),
          campFilter: $('#CampFilterId').val(),
          transportBusFilter: $('#TransportBusFilterId').val(),
          minCreatedDateFilter: getDateFilter($('#MinCreatedDateFilterId')),
          maxCreatedDateFilter: getMaxDateFilter($('#MaxCreatedDateFilterId')),
          userNameFilter: $('#UserNameFilterId').val(),
          airportDisplayPropertyFilter: $('#AirportDisplayPropertyFilterId').val(),
          airportDisplayProperty2Filter: $('#AirportDisplayProperty2FilterId').val(),
          userName2Filter: $('#UserName2FilterId').val(),
        })
        .done(function (result) {
          app.downloadTempFile(result);
        });
    });

    abp.event.on('app.createOrEditTravelRequestModalSaved', function () {
      getTravelRequests();
    });

    $('#GetTravelRequestsButton').click(function (e) {
      e.preventDefault();
      getTravelRequests();
    });

    $(document).keypress(function (e) {
      if (e.which === 13) {
        getTravelRequests();
      }
    });

    $('.reload-on-change').change(function (e) {
      getTravelRequests();
    });

    $('.reload-on-keyup').keyup(function (e) {
      getTravelRequests();
    });

    $('#btn-reset-filters').click(function (e) {
      $('.reload-on-change,.reload-on-keyup,#MyEntsTableFilter').val('');
      getTravelRequests();
    });
  });
})();
