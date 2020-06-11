function search() {
    _userDataTable.reload();
}

var _userDataTable;

_orderPage = true;

$(document).ready(function () {
    $("#searchDiv .search-criteria").keypress(function (e) {
        var key = e.which || e.keyCode;
        if (key === 13) { // 13 is enter
            search();
        }
    });
    $('#Type').change(search);
    $("#searchBtn").click(search);
    var colunms = [];
    colunms.push({ Text: _number, Name: "Id" });
    colunms.push({ Name: "mutliSelected", IsHidden: true });
    colunms.push({ Text: _userName, Name: "Name" });
    colunms.push({ Text: _name, Name: "Name" });
    colunms.push({ Text: _mobile, Name: "Name" });
    colunms.push({ Text: _status, Name: "IdNumber" });
    colunms.push({ Text: _type, Name: "IdNumber" });
    colunms.push({ Text: _createDate, Name: "IssueDate" });
    colunms.push({ Text: '', Name: "IssueDate" });

    var model =
    {
        Attributes: colunms,
    };

    _userDataTable = addDataTable('grid', model);
    _userDataTable.getSettingsCallBack = function () {
        if ($.isEmptyObject(_userDataTable.SearchSettings)) {
            _userDataTable.SearchSettings.Name = $('#Name').val();
            _userDataTable.SearchSettings.Type = $('#Type').val();
            _userDataTable.SearchSettings.Mobile = $('#Mobile').val();
        } return _userDataTable.SearchSettings;
    };
    _userDataTable.actionName = _baseUrl + "MobileCustomer/List";
    _userDataTable.paging = true;
    _userDataTable.tableDrawCallBack = function () {
        $('#Name').focus();
    }
    _userDataTable.bindGrid();
})

function flagSelectChange() {
    if ($('#Flag').val() != 11)
        $('#messageDiv').show();
    else
        $('#messageDiv').hide();
}

function updateUserFlag(userId) {
    showDialog2('updateUserDiv', _okText, _cancelText, function ()
    {
        ajaxCall(getRequestModel(true, _baseUrl + 'Management/UpdateFlag', {
            UserId : userId
            ,Flag : $('#updateFlag').val()
            ,Customer :true
            , Message: $('#updateMessage').val()
        }, function (data) {
            if (data.ErrorCode == 0) {
                showSide();
                search();
                $('#updateFlag').val('10');
                $('#updateMessage').val('');
            }
            else
                showSide('Error',data.Message);
        }));
    }, null, null, null, _updateStatus);
}