$(function () {
    var basicAuthUI =
        '<div class ="input"> 用户名:<input placeholder ="username" id ="ipt_userCode" name ="username" type ="text" size ="10"> </ div>' +
        '<div class ="input"> 密码:<input placeholder ="password" id ="ipt_password"  name ="password" type ="password" size ="15"> </ div>' +
        '<div class ="input"> 仓库号:<input placeholder ="warehouse" id ="ipt_warehouse"  name ="warehouse" type ="text" size ="10"> </ div>' +
        '<div class ="input"><input type="button" id ="input_btn" onclick="addAuthorization();" value="提交" style="width:80px;cursor:pointer"></ div>';
    $('#input_apiKey').hide();
    $('#api_selector').html(basicAuthUI);
});

function addAuthorization() {
    var usercode = document.getElementById("ipt_userCode").value;
    var password = document.getElementById("ipt_password").value;
    var warehouse = document.getElementById("ipt_warehouse").value;
    if (usercode.length == 0 || password.length == 0) {
        alert("请输入用户名和密码");
        return;
    }

    var guid = uuid();
    paramData = {
        'User_Code': usercode,
        'User_Password': password,
        'Random': guid
    };
    var urlConfig = "/api/v1/System/UserManage/User/Login";
    urlConfig = warehouse.length == 0 ? urlConfig : "/" + warehouse + urlConfig;
    $.ajax({
        url:  urlConfig,
        type: 'POST',
        dataType: 'json',
        data: paramData,
        contenttype: 'application/json',
        success: function (response) {
            if (response.Subrc == 0) {
                var bearerToken = response.Data.Random;
                swaggerUi.api.clientAuthorizations.add('key', new SwaggerClient.ApiKeyAuthorization('random', bearerToken, 'header'));
            }
            else {
                alert("账户登录失败：" + response.Message);
            }
        }
    });
}
//生成用户随机码
function uuid() {
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);
    s[8] = s[13] = s[18] = s[23] = "-";
    var uuid = s.join("");
    return uuid;
}