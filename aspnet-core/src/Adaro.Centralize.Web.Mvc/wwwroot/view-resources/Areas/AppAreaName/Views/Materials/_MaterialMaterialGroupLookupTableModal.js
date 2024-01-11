(function ($) {
  app.modals.MaterialGroupLookupTableModal = function () {
    var _modalManager;

    var _materialsService = abp.services.app.materials;
    var _$materialGroupTable = $('#MaterialGroupTable');

    this.init = function (modalManager) {
      _modalManager = modalManager;
    };

    var dataTable = _$materialGroupTable.DataTable({
      paging: true,
      serverSide: true,
      processing: true,
      listAction: {
        ajaxFunction: _materialsService.getAllMaterialGroupForLookupTable,
        inputFilter: function () {
          return {
            filter: $('#MaterialGroupTableFilter').val(),
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

    $('#MaterialGroupTable tbody').on('click', '[id*=selectbtn]', function () {
      var data = dataTable.row($(this).parents('tr')).data();
      _modalManager.setResult(data);
      _modalManager.close();
    });

    function getMaterialGroup() {
      dataTable.ajax.reload();
    }

    $('#GetMaterialGroupButton').click(function (e) {
      e.preventDefault();
      getMaterialGroup();
    });

    $('#SelectButton').click(function (e) {
      e.preventDefault();
    });

    $('#MaterialGroupTableFilter').keypress(function (e) {
      if (e.which === 13 && e.target.tagName.toLocaleLowerCase() != 'textarea') {
        getMaterialGroup();
      }
    });
  };
})(jQuery);
