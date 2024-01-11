(function ($) {
  app.modals.UNSPSCLookupTableModal = function () {
    var _modalManager;

    var _materialsService = abp.services.app.materials;
    var _$unspscTable = $('#UNSPSCTable');

    this.init = function (modalManager) {
      _modalManager = modalManager;
    };

    var dataTable = _$unspscTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _materialsService.getAllUNSPSCForLookupTable,
        inputFilter: function () {
          return {
            filter: $('#UNSPSCTableFilter').val(),
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

    $('#UNSPSCTable tbody').on('click', '[id*=selectbtn]', function () {
      var data = dataTable.row($(this).parents('tr')).data();
      _modalManager.setResult(data);
      _modalManager.close();
    });

    function getUNSPSC() {
      dataTable.ajax.reload();
    }

    $('#GetUNSPSCButton').click(function (e) {
      e.preventDefault();
      getUNSPSC();
    });

    $('#SelectButton').click(function (e) {
      e.preventDefault();
    });

    $('#UNSPSCTableFilter').keypress(function (e) {
      if (e.which === 13 && e.target.tagName.toLocaleLowerCase() != 'textarea') {
        getUNSPSC();
      }
    });
  };
})(jQuery);
