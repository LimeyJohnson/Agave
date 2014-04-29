/// <reference path="../App.js" />

//Variable initialization
var BindingName = "RouteBinding";
var ToAddress = "";
var FromAddress = "";
var map = null;
var PostField = "";
var MilesTraveled = 0;

// The initialize function will be run on App For Office initialization on start, page refresh and insertion

Office.initialize = function (reason) {
    // This async method will check to see if a binding already exists. 
    Office.context.document.bindings.getByIdAsync(BindingName, function (callback) {
        //check the callback.status field to see if the call succeeded. If the callback succeeded we know a binding already exists
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            //We have the binding so call functions to register handlers and update the map
            RegisterHandlers(callback.value);
            RecalculateDistance();
        }
        else {
            //We don't have a binding yet so call functions to create a binding
            BindToData();
        }
    });
    //Register button events
    $("#btnBind").click(BindToData);
    $("#setdistance").click(SetDistance);
};

//Show the binding prompt with sample text and bind to the data the user selects
function BindToData() {
    //TableData to hold the sample data
    var sampleDataTable = new Office.TableData();
    //Sample headers
    sampleDataTable.headers = [["From Address", "To Address", "Distance Field"]];
    //Sample data rows
    sampleDataTable.rows = [["White House", "Seattle", "2716.2"], ["Houston","Dallas","293.3"], ["400 Broad St., Seattle, WA", "1 Microsoft Way, Redmond WA", "13.3"]];

    //Show the binding promp with the sample data and create a binding with id:BindingName
    Office.context.document.bindings.addFromPromptAsync(Office.BindingType.Table, {
        id: BindingName,
        sampleData: sampleDataTable
    }, function (bindingCallback) {
        //Check if the callback succeeded
        if (bindingCallback.status == Office.AsyncResultStatus.Succeeded) {
            //Register handlers for events
            RegisterHandlers(bindingCallback.value)
        }
    });
}

//Register handlers for events
function RegisterHandlers(binding) {
    //Map Binding Selection Changed event to the HandleRecordChange function
    binding.addHandlerAsync(Office.EventType.BindingSelectionChanged, RecalculateDistance);
    //Map Binding Data Changed event to the HandleRecordChange function
    binding.addHandlerAsync(Office.EventType.BindingDataChanged, RecalculateDistance);

    //Get the name of the field to set data into. This is the third field in the binding
    Office.select("bindings#" + BindingName).getDataAsync({
        coercionType: Office.CoercionType.Table,
        //Get the data for the current row
        rows: "thisRow"
    }, function (callback) {
        //Check if the call succeeded
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            //Store the name(header) of the field
            PostField = callback.value.headers[0][2];
        }
    });
}
//Recalulate distance with current row's data. To be called on app initial load, selectionchanged or datachanged
function RecalculateDistance() {
    //Get the To and From address from the current row
    Office.select("bindings#" + BindingName).getDataAsync({
        coercionType: Office.CoercionType.Table,
        //Specify to get the data from the currently selected data row
        rows: "thisRow"
    }, function (callback) {
        // Check to see if the function was successful
        if (callback.status == Office.AsyncResultStatus.Succeeded) {
            //Store the from address
            FromAddress = callback.value.rows[0][0];
            //Store the to address
            ToAddress = callback.value.rows[0][1];
            //Update the UI to reflect the new data
            $("#fromaddress").text(FromAddress);
            $("#toaddress").text(ToAddress);
            //C
            CallBingMapsService();
        }
    });
}

function CallBingMapsService() {
    //Create new Microsoft Maps object
    map = new Microsoft.Maps.Map(document.getElementById("mapDiv"), { credentials: "AguE3HISSlzPg-QcuUpQIeg6p3l8B18n_T5aMVqUkYXY9DhlE5Lgj1Z_YXvWsD3P", mapTypeId: Microsoft.Maps.MapTypeId.r });
    map.getCredentials(MakeRouteRequest);
}


function MakeRouteRequest(credentials) {
    //Send request to bing. Specify the to and from Address and the callback function
    var routeRequest = "https://dev.virtualearth.net/REST/v1/Routes?wp.0=" + FromAddress + "&wp.1=" + ToAddress + "&routePathOutput=Points&output=json&jsonp=RouteCallback&key=" + credentials;

    CallRestService(routeRequest);

}
//To called by Bing Maps Response
function RouteCallback(result) {
    //Check to see if all the data we need exists
    if (result &&
          result.resourceSets &&
          result.resourceSets.length > 0 &&
          result.resourceSets[0].resources &&
          result.resourceSets[0].resources.length > 0) {

        //Store the distance between addresses
        MilesTraveled = Math.round(result.resourceSets[0].resources[0].travelDistance * 6.2137119) / 10;
        //Update the Apps for Office UI
        $("#distance").text(MilesTraveled);
    }
}
//Respond to set data button click and set the distance field back to the host
function SetDistance(eventArgs) {
    if (MilesTraveled !== 0) {
        //Set data in the host
        Office.select("bindings#" + BindingName).setDataAsync([[MilesTraveled]], {
            //On the current row
            rows: "thisRow",
            //In the PostField
            columns: [PostField]
        }, function (callback) {
            //Set Data Callback
        });
    }
}

//Function to call rest service by injecting script tag
function CallRestService(request) {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", request);
    document.body.appendChild(script);
}
