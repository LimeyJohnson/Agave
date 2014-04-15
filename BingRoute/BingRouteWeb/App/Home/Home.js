/// <reference path="../App.js" />


var BindingName = "RouteBinding";
var ToAddress = "";
var FromAddress = "";
var map = null;
var PostField = "";
var MilesTraveled = 0;
// The initialize function must be run each time a new page is loaded
Office.initialize = function (reason) {
    Office.context.document.bindings.getByIdAsync(BindingName, function (callback) {
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            //We have the binding
            RegisterCallbacks(callback.value);
            UpdateMap();
        }
        else {
            //We don't have a binding yet. Ask for a binding
            BindToData();
        }
    });
    $(GetMap);
    $("#btnBind").click(BindToData)
};
function BindToData() {
    var sampleDataTable = new Office.TableData();
    sampleDataTable.header = ['To Address', 'Miles Field'];
    sampleDataTable.rows = [["Pensylvania", "2000"], ["Try Again", "please"]];

    Office.context.document.bindings.addFromPromptAsync(Office.BindingType.Table, {
        id: BindingName,
        sampleData: sampleDataTable
    }, function (bindingCallback) {
        if (bindingCallback.status == Office.AsyncResultStatus.Succeeded) {
            RegisterCallbacks(bindingCallback.value)
        }
    });
}

function RegisterCallbacks(binding) {
    binding.addHandlerAsync(Office.EventType.BindingSelectionChanged, HandleRecordChange);
    binding.addHandlerAsync(Office.EventType.BindingDataChanged, HandleDataChanged);

    Office.select("bindings#" + BindingName).getDataAsync({
        coercionType: Office.CoercionType.Table,
        rows: "thisRow"
    }, function (callback) {
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            PostField = callback.value.headers[0][2];
        }
    });
}
function HandleRecordChange() {
    UpdateMap();
}
function UpdateMap() {
    Office.select("bindings#" + BindingName).getDataAsync({
        coercionType: Office.CoercionType.Table,
        rows: "thisRow"
    }, function (callback) {
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            FromAddress = callback.value.rows[0][0];
            ToAddress = callback.value.rows[0][1];
            MilesTraveled = callback.value.rows[0][2];
            ClickRoute();
        }
    });
}
function HandleDataChanged() {
    UpdateMap();
}



function GetMap() {
    // Remove old map;
    map = null;
    $('#mapDiv').empty();


}

function ClickRoute() {
    map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), { credentials: "AguE3HISSlzPg-QcuUpQIeg6p3l8B18n_T5aMVqUkYXY9DhlE5Lgj1Z_YXvWsD3P", mapTypeId: Microsoft.Maps.MapTypeId.r });
    map.getCredentials(MakeRouteRequest);
}


function MakeRouteRequest(credentials) {
    var routeRequest = "https://dev.virtualearth.net/REST/v1/Routes?wp.0="+FromAddress+"&wp.1=" + ToAddress + "&routePathOutput=Points&output=json&jsonp=RouteCallback&key=" + credentials;

    CallRestService(routeRequest);

}

function RouteCallback(result) {
    if (result &&
          result.resourceSets &&
          result.resourceSets.length > 0 &&
          result.resourceSets[0].resources &&
          result.resourceSets[0].resources.length > 0) {

        // Set the map view
        var bbox = result.resourceSets[0].resources[0].bbox;
        var viewBoundaries = Microsoft.Maps.LocationRect.fromLocations(new Microsoft.Maps.Location(bbox[0], bbox[1]), new Microsoft.Maps.Location(bbox[2], bbox[3]));
        map.setView({ bounds: viewBoundaries });

        //Update the map
        var newMilesTraveled = Math.round(result.resourceSets[0].resources[0].travelDistance * 10.62137119) / 10;
        UpdateMilesTraveled(newMilesTraveled);
        
        // Draw the route
        var routeline = result.resourceSets[0].resources[0].routePath.line;
        var routepoints = new Array();

        for (var i = 0; i < routeline.coordinates.length; i++) {

            routepoints[i] = new Microsoft.Maps.Location(routeline.coordinates[i][0], routeline.coordinates[i][1]);
        }


        // Draw the route on the map
        var routeshape = new Microsoft.Maps.Polyline(routepoints, { strokeColor: new Microsoft.Maps.Color(200, 0, 0, 200) });
        map.entities.push(routeshape);

    }
}
function UpdateMilesTraveled(newMilesTraveled)
{
    if (newMilesTraveled !== MilesTraveled) {
        Office.select("bindings#" + BindingName).setDataAsync([[newMilesTraveled]], {
            rows: "thisRow",
            columns: [PostField]
        }, function (callback) {
        });
    }
}

function CallRestService(request) {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", request);
    document.body.appendChild(script);
}
