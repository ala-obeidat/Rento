function search() {
    _userDataTable.reload();
}
var _userDataTable;
_orderPage = true;
$(document).ready(function () {
    $("#searchId").keypress(function (e) {
        var key = e.which || e.keyCode;
        if (key === 13) { // 13 is enter
            search();
        }
    });
    $("#searchBtn").click(search);
    var colunms = [];
    colunms.push({ Text: _number, Name: "Id" });
    colunms.push({ Name: "mutliSelected", IsHidden: true });
    colunms.push({ Text: _carName, Name: "Name" });
    colunms.push({ Text: _status, Name: "IdNumber" });
    colunms.push({ Text: _from, Name: "IdNumber" });
    colunms.push({ Text: _to, Name: "IdTypeName" });
    colunms.push({ Text: _createDate, Name: "IssueDate" });
    var model =
    {
        Attributes: colunms,
    };

    _userDataTable = addDataTable('grid', model);
    _userDataTable.getSettingsCallBack = function () {
        if ($.isEmptyObject(_userDataTable.SearchSettings)) {
            var id = $('#searchId').val();
            _userDataTable.SearchSettings.Key = id ? id : '0';
        }
        return _userDataTable.SearchSettings;
    };
    _userDataTable.actionName = _baseUrl + "Order/List";
    _userDataTable.paging = true;
    _userDataTable.tableDrawCallBack = function () {
        $('#searchId').focus();
    }
    _userDataTable.clickItemCallBack = function (data) {
        if (_userType === 'Active')
            redirectWithBaseUrl('Order/Details?id=' + data[0]);
        else
            redirectWithBaseUrl('Order/Action?id=' + data[0]);

    }
    _userDataTable.bindGrid();
    setInterval(function () {
        if (!$("#searchId").val() || $("#searchId").val() === '0')
            search();
    }, 15000);
});
