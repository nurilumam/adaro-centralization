(function ($) {
  app.modals.GeneralLedgerMappingLookupTableModal = function () {
    var _modalManager;

    var _materialsService = abp.services.app.materials;
    var _$generalLedgerMappingTable = $('#GeneralLedgerMappingTable');

    this.init = function (modalManager) {
      _modalManager = modalManager;
    };

    var dataTable = _$generalLedgerMappingTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _materialsService.getAllGeneralLedgerMappingForLookupTable,
        inputFilter: function () {
          return {
            filter: $('#GeneralLedgerMappingTableFilter').val(),
          };
        },
      },
      columnDefs: [
        {
          targets: 0,
          data: null,
          orderable: false,
          autoWidth: false,
          defaultContent:
            "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" +
            app.localize('Select') +
            "' /></div>",
        },
        {
          autoWidth: false,
          orderable: false,
          targets: 1,
          data: 'displayName',
        },
      ],
    });

    $('#GeneralLedgerMappingTable tbody').on('click', '[id*=selectbtn]', function () {
      var data = dataTable.row($(this).parents('tr')).data();
      _modalManager.setResult(data);
      _modalManager.close();
    });

    function getGeneralLedgerMapping() {
      dataTable.ajax.reload();
    }

    $('#GetGeneralLedgerMappingButton').click(function (e) {
      e.preventDefault();
      getGeneralLedgerMapping();
    });

    $('#SelectButton').click(function (e) {
      e.preventDefault();
    });

    $('#GeneralLedgerMappingTableFilter').keypress(function (e) {
      if (e.which === 13 && e.target.tagName.toLocaleLowerCase() != 'textarea') {
        getGeneralLedgerMapping();
      }
    });
  };
})(jQuery);
