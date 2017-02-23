// "defaultContent": "-"

//var routesData = {
//    data: undefined,
//    lastUpdate: undefined
//};
var infowindow;
var prev_infowindow = false;
var globalMarker;
var currentMarkers = [];
var routeset;
var globalLocations;

//function getBaseUrl() {
//    var re = new RegExp(/^.*\//);
//    return re.exec(window.location.href);
//}

$('document').ready(function () {
    //$('li#user').click(function () {
    //    $(this).addClass('selectedEmp');
    //});
    loadAllRoutes();
    
});

function loadAllRoutes() {
    $.ajax({
        type: "POST",
        url: getBaseUrl() +"/OutletDestinationValidation/getallroutes",
        error: function(xhr, status, error) {
          //  var err = eval("(" + xhr.responseText + ")");
            console.log(error);
        },
        success: function (data) {
            var d = new Date();
            var rtd = new Object();
            rtd.data = data;
            rtd.lastUpdate = d.getTime();
            RenderRouteTree(rtd);
        }
    });
}

function RenderRouteTree(routesData) {
    //$('#tbcontainer').height($('#treeview').height());
    var options = {
       // color: "#428bca",
        color: "black",
        expandIcon: 'glyphicon glyphicon-chevron-right',
        collapseIcon: 'glyphicon glyphicon-chevron-down',
        //nodeIcon: 'glyphicon glyphicon-bookmark',
        onNodeSelected: function (event, node) {
            console.log(node.id);
            console.log(node.text);
            GetOutletsByRoute(node.id);
           // updateOutletsTableData2(node.id);
        },
        data: routesData.data
        };

    $('#treeview').treeview(options);
    $('#treeview').treeview('collapseAll', { silent: false });
    //var allNodes = $('#treeview').treeview('getNodes');
    //$(allNodes).each(function(index, element) {
    //                    //$(this.$el[0]).attr('data-attribute-name', this.data-attribute-name);
    //    $('#treeview').treeview('collapseNode', [element.id, { silent: true }]);
                
    //    });
}



function GetOutletsByRoute(routeid) {
    //console.log(routeid);
    $("#tbcontainer").removeClass('hidden');
    $("#maprow").addClass('hidden');
    $.ajax({
        type: "POST",
        url: getBaseUrl() + "/OutletDestinationValidation/GetRouteSet",
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ routeId: routeid, showOnlyUnknown: $("#chbonlyunknown").prop("checked") }),
        error: function (xhr, status, error) {
            console.log(error);
        },
        success: function (data) {
            //console.log(data);
           // updateOutletsTableData("{\"data\":"+data+"}");
            //updateOutletsTableData( data );
            routeset = data;
            renderJsGrid(data);
        }
    });
}


function renderJsGrid(outlets) {
//    console.log(outlets);
    $("#jsGrid").jsGrid({
        height: "100%",
        width: "100%",

       // filtering: true,
       // editing: true,
        sorting: true,
        paging: true,
        autoload: true,

        pageSize:25,
        //pageButtonCount: 5,
        data: outlets,
       
        fields: [
            { name: "OutletName", type: "text", width: 150, validate: "required", title: "Торговая точка" },
            { name: "Outletid", type: "number", width: 50, visible: false },
            { name: "Adrress", type: "text", width: 150, validate: "required",  title: "Адрес" },
            { name: "RouteId", type: "text", width: 150, visible: false, validate: "required" },
            { name: "RouteName", type: "text", width: 150, validate: "required" , title: "Маршрут"},
            { name: "Map", title: "Карта", align: "center",
            	itemTemplate: function(value, item) {
            	    return $("<button>").append('<img   src="http://maps.google.com/mapfiles/ms/icons/red-dot.png"  width="50%" height="40%" />')
                		.on("click", function() {
                		    //alert(item.Outletid);
                		    //alert(item.OutletName);
                		    getKnownLocation(item);
                      return false;
                    });
              }
            }
        ]
       
    });
}

//var table = undefined;

function updateOutletsTableData2(routeId) {
    $('#tbcontainer').html('<table id="outletstable" class="display" cellspacing="0" width="100%"></table>');
   $('#outletstable').dataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            type: "POST",
            url: "/OutletDestinationValidation/GetRouteSet",
            data: "routeId=" + routeId
        },
        "columns": [
        {
            "data": "outletname"
        }]
    });
   // table.draw();
}

function updateOutletsTableData(mData) {
    
    //    var table = $('#outletstable').DataTable();
    // if (table == undefined)
    $('#tbcontainer').html('<table id="outletstable" class="display" cellspacing="0" width="100%"></table>');
    var table_config = {
        // "bDestroy": true,
        //"bServerSide": true,
        //"sAjaxSource": mData,
        //aoColumns: [
        //{
        //    "mData": "outletname", sTitle: 'Торговая точка'
        //}]
        //columns: [
        //     { "data": "outletname" }
        //]
        "columnDefs": [
            //{ "data": 'Outletid', sTitle: 'OutletId', visible: false, "targets": [0] },
            { "data": "outletname", sTitle: 'Торговая точка', visible: true, "targets": [0], "width": "20" }
            //{ "data": 'Adrress', sTitle: 'Адрес', visible: true, "targets": 2, "width": "150%" },
            //{ "data": 'RouteId', sTitle: 'RouteId', visible: false, "targets": 3 },
            //{ "data": 'RouteName', sTitle: 'Маршрут', visible: true, "targets": 4 }
        ]
    };
  //$('#outletstable').dataTable(
  //  {
  //      "bDestroy": true,
  //      "columnDefs": [
  //          { "data": 'Outletid', "name": 'OutletId', visible: false, "targets": 0 },
  //          { "data": 'OutletName', "name": 'Торговая точка', visible: true, "targets": 1, "width": "80%" },
  //          { "data": 'Adrress', "name": 'Адрес', visible: true, "targets": 2, "width": "150%" },
  //          { "data"1: 'RouteId', "name": 'RouteId', visible: false, "targets": 3 },
  //          { "data": 'RouteName', "name": 'Маршрут', visible: true, "targets": 4 }
  //      ]
  //  });
    //else
    console.log(mData);
    var table = $('#outletstable').DataTable(table_config);
    //table.fnClearTable();
    //table.fnAddData(mData);
    //table.fnDraw();
    table.clear();
    table.rows.add(mData);
    table.draw();
}


function getKnownLocation(outletobj) {
    $.ajax({
        type: "POST",
        url: getBaseUrl() + "/OutletDestinationValidation/GetKnownLocation",
        data: "outletid=" + outletobj.Outletid,
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            console.log(error);
        },
        success: function (data) {
            console.log(data);
            // updateOutletsTableData("{\"data\":"+data+"}");
            //updateOutletsTableData( data );
            if (data.length === 0)
            //alert("Нет идентификацию для торговой точки: " + outletobj.OutletName);
            {
                $("#dlgmessage").text("Нет идентфикаций для "+outletobj.OutletName + " - " + outletobj.Adrress);
                $('#myModal').modal('toggle');
                $('#myModal').modal('show');
               // $('#myModal').modal('hide');
            }
            else
                renderMap(data, outletobj);
        }
    });
}


function renderMap(locations, outletobj) {
    globalLocations = locations;
    $("#maprow").removeClass('hidden');
    $('html, body').animate({
        scrollTop: parseInt($("#canvas").offset().top)
    }, 2000);
    $("#mapheader").text(outletobj.OutletName + " - " + outletobj.Adrress);
    google.maps.visualRefresh = true;
    
    var currentPlace = new google.maps.LatLng(locations[0].Latitude, locations[0].Longtitude);
    var mapOptions = {
        zoom: 16,
        center: currentPlace,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP//ROADMAP
    };
    currentMarkers = [];
    var map = new google.maps.Map(document.getElementById("canvas"), mapOptions);
    $.each(locations, function (i, item) {
        
        var id = i + 1;
        var marker = new google.maps.Marker({

            'position': new google.maps.LatLng(item.Latitude, item.Longtitude),
            'map': map,
            //'title': item.Outlet + ":  " + item.CheckInTimeStr,
            'label': id.toString(),
            customData: outletobj,
            location: item
        });
        marker.setIcon({
            path: google.maps.SymbolPath.BACKWARD_CLOSED_ARROW,
            scale: 4,
            fillColor: 'green',
            fillOpacity: 0.3,
            strokeWeight: 1
        });
        currentMarkers.push(marker);
        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png');



        infowindow = new google.maps.InfoWindow({
            content: '<div class="outletInfo"><h5><span id="mapheader" class="label label-success ">' + //(id).toString() +
                  outletobj.OutletName +
                    '</span></h5>' +
                    '<button id="approvebtn" type="button" class="btn btn-primary btn-md" onclick="approveBtnClick()">Подтвердить</button>' +
                    '</div>'
            }
        );


        google.maps.event.addListener(marker, 'click', function () {
            if (prev_infowindow) {
                prev_infowindow.close();
            }
            prev_infowindow = infowindow;
            globalMarker = marker;
            infowindow.open(map, marker);
        });

        if (outletobj.Latitude != null) {
            var marker = new google.maps.Marker({
                'position': new google.maps.LatLng(outletobj.Latitude, outletobj.Longtitude),
                'map': map,
                //'title': item.Outlet + ":  " + item.CheckInTimeStr,
                'label': id.toString(),
                customData: outletobj,
                location: item
            });
            marker.setIcon({
                path: google.maps.SymbolPath.BACKWARD_CLOSED_ARROW,
                scale: 4,
                fillColor: 'green',
                fillOpacity: 0.3,
                strokeWeight: 1
            });
            currentMarkers.push(marker);
            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
        }
    });
}

function approveBtnClick() {
    console.log(globalMarker.location.Latitude + "   " + globalMarker.location.Longtitude);
    var knownLocation = {
        OutletId : globalMarker.customData.Outletid,
        Latitude: globalMarker.location.Latitude,
        Longtitude: globalMarker.location.Longtitude
    };
    console.log(knownLocation);
    $.ajax({
        type: "POST",
        url: getBaseUrl() + "/OutletDestinationValidation/SaveApprovedLocation",
        //contentType: "application/json",
        // traditional: true,
        dataType: "json",
        data: knownLocation,
        error: function (xhr, status, error) {
            //var err = eval("(" + xhr.responseText + ")");
            console.log(error);
            if (xhr.status === 200) {
                infowindow.close();
                
                var lat = globalMarker.location.Latitude;
                var long = globalMarker.location.Longtitude;
                //for (var i = 0; i < routeset.length; i++) {
                //    routeset[i].Latitude = null;
                //    routeset[i].Longtitude = null;
                //}
                globalMarker.customData.Latitude = lat;
                globalMarker.customData.Longtitude = long;
                renderMap(globalLocations, globalMarker.customData);
                //changeMarkerIcon(globalMarker);
            }
                
        },
        success: function (data) {
            infowindow.close();
        }
    });


}

function changeMarkerIcon(marker) {
   
    for (var i = 0; i < currentMarkers.length; i++) {
        currentMarkers[i].setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png');
    }
    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');
}