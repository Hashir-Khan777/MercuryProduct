var map;
var center;
var markers = [];
var bounds = new tt.LngLatBounds();
function initMap(centerLat, centerLng) {
  markers = [];
  center = { lat: centerLat, lng: centerLng };
  window.tt.setProductInfo("Mercury Product", "1.0.0");
  if (centerLat && centerLng) {
    map = window.tt.map({
      key: "FAywZGZYK8dXtjREG8KFziDuedaBFSjb",
      container: "map",
      stylesVisibility: {
        trafficIncidents: true,
        trafficFlow: true,
      },
      center: { lat: centerLat, lng: centerLng },
      zoom: 10,
    });
  } else {
    map = window.tt.map({
      key: "FAywZGZYK8dXtjREG8KFziDuedaBFSjb",
      container: "map",
      stylesVisibility: {
        trafficIncidents: true,
        trafficFlow: true,
      },
      zoom: 10,
    });
  }
  var nav = new window.tt.NavigationControl();
  map.addControl(nav);
}

function addMarker(
  lon,
  lat,
  id,
  name,
  color,
  cus_name,
  cus_address,
  cus_number
) {
  markers.push([lon, lat]);
  var element = document.createElement("div");
  var popup = new window.tt.Popup().setHTML(
    `
        <div>
            <div style="display: flex; align-items: center; gap: 3px">
                <p>Name:</p>
                <p>${cus_name}</p>
            </div>
            <div style="display: flex; align-items: center; gap: 3px">
                <p>Address:</p>
                <p>${cus_address}</p>
            </div>
            <div style="display: flex; align-items: center; gap: 3px">
                <p>Phone Number:</p>
                <p>${cus_number}</p>
            </div>
            <button style="background: none; color: blue; text-decoration: underline; border: none;" onclick="markerClicked(${id}, '${name}')">Show Vehicles</button>
        </div>
        `
  );
  var html = `
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512" fill=${color}>
          <path
            d="M215.7 499.2C267 435 384 279.4 384 192C384 86 298 0 192 0S0 86 0 192c0 87.4 117 243 168.3 307.2c12.3 15.3 35.1 15.3 47.4 0zM192 128a64 64 0 1 1 0 128 64 64 0 1 1 0-128z"
          />
        </svg>
    `;
  element.style = "width: 30px; height: 30px;";
  element.innerHTML = html;
  element.id = "marker";
  new window.tt.Marker({ element: element })
    .setLngLat([lon, lat])
    .setPopup(popup)
    .addTo(map);
}

// PP-75 & PP-81: make state level zoom in map
// Bug: Map does not shows all markers in the view
// Fix: Add fit bounds function that set the zoom level according to the number of markers
function setBounds() {
  var bounds = new tt.LngLatBounds();
  if (markers.length > 0) {
    for (var i = 0; i <= markers.length; i++) {
      bounds.extend(markers[i]);
    }
    map.fitBounds(bounds, { padding: 20 });
  }
}

async function markerClicked(cusId, name) {
  await DotNet.invokeMethodAsync(name, "OnMarkerClick", cusId);
}

window.generatePDF = function (item, id) {
    window.html2canvas = html2canvas;
    const { jsPDF } = window.jspdf;

    let doc = new jsPDF('l', 'pt', [920, 712]);

    doc.html(item, {
        callback: function (doc) {
            doc.save(`Sale-${id}.pdf`);
        },
        x: 10,
        y: 10
    });
}

window.generateExpensePDF = function (item) {
    window.html2canvas = html2canvas;
    const { jsPDF } = window.jspdf;

    let doc = new jsPDF('l', 'pt', [1200, 1200]);

    doc.html(item, {
        callback: function (doc) {
            doc.save(`Cash-Flow.pdf`);
        },
        x: 10,
        y: 10
    });
}
