(function () {
    "use strict";

    angular
        .module("common.services", [])
        .constant("appSettings",
        {
            serverPath: "https://localhost:44301/"
            // serverPath: "http://www.gada.local"
            //serverPath: "http://gada-dev-api.azurewebsites.net"
        });
}());