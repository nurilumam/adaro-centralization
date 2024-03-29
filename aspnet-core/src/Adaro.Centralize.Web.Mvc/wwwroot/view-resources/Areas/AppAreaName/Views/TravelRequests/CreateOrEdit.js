﻿(function () {
  $(function () {
    var _travelRequestsService = abp.services.app.travelRequests;

    var _$travelRequestInformationForm = $('form[name=TravelRequestInformationsForm]');
    _$travelRequestInformationForm.validate();

    var _TravelRequestuserLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/TravelRequests/UserLookupTableModal',
      scriptUrl:
        abp.appPath + 'view-resources/Areas/AppAreaName/Views/TravelRequests/_TravelRequestUserLookupTableModal.js',
      modalClass: 'UserLookupTableModal',
    });
    var _TravelRequestairportLookupTableModal = new app.ModalManager({
      viewUrl: abp.appPath + 'AppAreaName/TravelRequests/AirportLookupTableModal',
      scriptUrl:
        abp.appPath + 'view-resources/Areas/AppAreaName/Views/TravelRequests/_TravelRequestAirportLookupTableModal.js',
      modalClass: 'AirportLookupTableModal',
    });

    $('.date-picker').daterangepicker({
      singleDatePicker: true,
      locale: abp.localization.currentLanguage.name,
      format: 'L',
    });

    $('#OpenUserLookupTableButton').click(function () {
      var travelRequest = _$travelRequestInformationForm.serializeFormToObject();

      _TravelRequestuserLookupTableModal.open(
        { id: travelRequest.userTravel, displayName: travelRequest.userName },
        function (data) {
          _$travelRequestInformationForm.find('input[name=userName]').val(data.displayName);
          _$travelRequestInformationForm.find('input[name=userTravel]').val(data.id);
        }
      );
    });

    $('#ClearUserNameButton').click(function () {
      _$travelRequestInformationForm.find('input[name=userName]').val('');
      _$travelRequestInformationForm.find('input[name=userTravel]').val('');
    });

    $('#OpenAirportLookupTableButton').click(function () {
      var travelRequest = _$travelRequestInformationForm.serializeFormToObject();

      _TravelRequestairportLookupTableModal.open(
        { id: travelRequest.airportFrom, displayName: travelRequest.airportDisplayProperty },
        function (data) {
          _$travelRequestInformationForm.find('input[name=airportDisplayProperty]').val(data.displayName);
          _$travelRequestInformationForm.find('input[name=airportFrom]').val(data.id);
        }
      );
    });

    $('#ClearAirportDisplayPropertyButton').click(function () {
      _$travelRequestInformationForm.find('input[name=airportDisplayProperty]').val('');
      _$travelRequestInformationForm.find('input[name=airportFrom]').val('');
    });

    $('#OpenAirport2LookupTableButton').click(function () {
      var travelRequest = _$travelRequestInformationForm.serializeFormToObject();

      _TravelRequestairportLookupTableModal.open(
        { id: travelRequest.airportTo, displayName: travelRequest.airportDisplayProperty2 },
        function (data) {
          _$travelRequestInformationForm.find('input[name=airportDisplayProperty2]').val(data.displayName);
          _$travelRequestInformationForm.find('input[name=airportTo]').val(data.id);
        }
      );
    });

    $('#ClearAirportDisplayProperty2Button').click(function () {
      _$travelRequestInformationForm.find('input[name=airportDisplayProperty2]').val('');
      _$travelRequestInformationForm.find('input[name=airportTo]').val('');
    });

    $('#OpenUser2LookupTableButton').click(function () {
      var travelRequest = _$travelRequestInformationForm.serializeFormToObject();

      _TravelRequestuserLookupTableModal.open(
        { id: travelRequest.createdBy, displayName: travelRequest.userName2 },
        function (data) {
          _$travelRequestInformationForm.find('input[name=userName2]').val(data.displayName);
          _$travelRequestInformationForm.find('input[name=createdBy]').val(data.id);
        }
      );
    });

    $('#ClearUserName2Button').click(function () {
      _$travelRequestInformationForm.find('input[name=userName2]').val('');
      _$travelRequestInformationForm.find('input[name=createdBy]').val('');
    });

    function save(successCallback) {
      if (!_$travelRequestInformationForm.valid()) {
        return;
      }
      if ($('#TravelRequest_UserTravel').prop('required') && $('#TravelRequest_UserTravel').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
        return;
      }
      if ($('#TravelRequest_AirportFrom').prop('required') && $('#TravelRequest_AirportFrom').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('Airport')));
        return;
      }
      if ($('#TravelRequest_AirportTo').prop('required') && $('#TravelRequest_AirportTo').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('Airport')));
        return;
      }
      if ($('#TravelRequest_CreatedBy').prop('required') && $('#TravelRequest_CreatedBy').val() == '') {
        abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
        return;
      }

      var travelRequest = _$travelRequestInformationForm.serializeFormToObject();

      abp.ui.setBusy();
      _travelRequestsService
        .createOrEdit(travelRequest)
        .done(function () {
          abp.notify.info(app.localize('SavedSuccessfully'));
          abp.event.trigger('app.createOrEditTravelRequestModalSaved');

          if (typeof successCallback === 'function') {
            successCallback();
          }
        })
        .always(function () {
          abp.ui.clearBusy();
        });
    }

    function clearForm() {
      _$travelRequestInformationForm[0].reset();
    }

    $('#saveBtn').click(function () {
      save(function () {
        window.location = '/AppAreaName/TravelRequests';
      });
    });

    $('#saveAndNewBtn').click(function () {
      save(function () {
        if (!$('input[name=id]').val()) {
          //if it is create page
          clearForm();
        }
      });
    });
  });
})();
