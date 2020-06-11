function search() {
    _userDataTable.reload();
}
var _userDataTable;
_orderPage = true;
function typeChange() {
    var options = JSON.parse(document.getElementById('subTypeOptions').value.replace(/'/gi, '"'));
    var typeItem = document.getElementById('CarSubType');
    var typeValue = document.getElementById('CarType').value;
    var targetOption = '<option value="0" >' + _carSubType+'</option>';
    if (typeValue!='0' && options) {
        for (var i = 0; i < options.length; i++) {
            if (options[i].ExternalData == typeValue)
                targetOption += '<option value="' + options[i].Id + '">' + (_isRtl ? options[i].Name : options[i].NameEn) + '</option>';
        }
    }
    typeItem.innerHTML = targetOption;
    targetOption = null;
}

$(document).ready(function () {
    
    $("#searchBtn").click(search);
    var colunms = [];
    colunms.push({ Text: _number, Name: "Id" });
    colunms.push({ Name: "mutliSelected", IsHidden: true });
    colunms.push({ Text: _carName, Name: "Name" });
    colunms.push({ Text: _status, Name: "IdNumber" });
    colunms.push({ Text: _office, Name: "IdNumber2" });
    colunms.push({ Text: _createDate, Name: "IssueDate" });
    colunms.push({ Text: '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;', Name: "IssueDate2" });
    var model =
    {
        Attributes: colunms,
    };

    _userDataTable = addDataTable('grid', model);
    _userDataTable.deleteItemCallBack = function (id) {
        sendGetRequest('Car', 'Delete', id);
    }
    _userDataTable.getSettingsCallBack = function () {
        if ($.isEmptyObject(_userDataTable.SearchSettings)) {
            _userDataTable.SearchSettings.CityId = $('#City').val();
            _userDataTable.SearchSettings.Model = $('#Year').val();
            _userDataTable.SearchSettings.Type = $('#CarType').val();
            _userDataTable.SearchSettings.SubType = $('#CarSubType').val();
            _userDataTable.SearchSettings.ProviderId = $('#ProviderId').val();
        } return _userDataTable.SearchSettings;
    };
    _userDataTable.actionName = _baseUrl + "Car/Search";
    _userDataTable.paging = true;
    
    _userDataTable.bindGrid();
});
