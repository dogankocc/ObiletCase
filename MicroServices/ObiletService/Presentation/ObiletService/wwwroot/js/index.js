
var datepicker;

function updateStorage(key, value) {
    _storage = localStorage.getItem("obilet_index_storage");
    if (!_storage) {
        _storage = { [key]: value }
    }
    else {
        _storage = JSON.parse(_storage);
        _storage[key] = value;
    }
    window.localStorage.setItem('obilet_index_storage', JSON.stringify(_storage))
}

function handleSelectFromLocation(event, locationId) {

    var selectedToLocationId = document.getElementById("selected_to_location").getAttribute("data-selected-to-location-id");

    if (selectedToLocationId == locationId) {
        showToast("Kalkış ve varış yeri aynı olamaz", "danger");
        return;
    }

    var selectedFromLocationElement = document.getElementById("selected_from_location");

    selectedFromLocationElement.innerText = event.target.innerText;
    selectedFromLocationElement.setAttribute("data-selected-from-location-id", locationId);

    updateStorage('departureLocation', event.target.innerText)
    updateStorage('departureLocationId', locationId)
}

function handleSelectToLocation(event, locationId) {

    var selectedFromLocationId = document.getElementById("selected_from_location").getAttribute("data-selected-from-location-id");

    if (selectedFromLocationId == locationId) {
        showToast("Kalkış ve varış yeri aynı olamaz", "danger");
        return;
    }
    var selectedToLocationElement = document.getElementById("selected_to_location");

    selectedToLocationElement.innerText = event.target.innerText;
    selectedToLocationElement.setAttribute("data-selected-to-location-id", locationId);

    updateStorage('destinationLocation', event.target.innerText)
    updateStorage('destinationLocationId', locationId)
}

function handleShiftLocations(event) {
    var selectedFromLocationElement = document.getElementById("selected_from_location");
    var selectedFromLocationId = selectedFromLocationElement.getAttribute("data-selected-from-location-id");
    var fromLocationName = selectedFromLocationElement.innerText;

    var selectedToLocationElement = document.getElementById("selected_to_location");
    var selectedToLocationId = selectedToLocationElement.getAttribute("data-selected-to-location-id");
    var toLocationName = selectedToLocationElement.innerText;

    selectedToLocationElement.innerText = fromLocationName;
    selectedToLocationElement.setAttribute("data-selected-to-location-id", selectedFromLocationId);
    updateStorage('departureLocation', toLocationName)
    updateStorage('departureLocationId', selectedToLocationId)


    selectedFromLocationElement.innerText = toLocationName;
    selectedFromLocationElement.setAttribute("data-selected-from-location-id", selectedToLocationId);
    updateStorage('destinationLocation', fromLocationName)
    updateStorage('destinationLocationId', selectedFromLocationId)
}

function handleClickDate(event) {
    var dateInput = document.getElementById("date-input");
    dateInput.click()
}


function handleClickNowDate(event) {
    const today = new Date();
    datepicker.setDate(today);
    let day = String(today.getDate()).padStart(2, "0");
    let month = String(today.getMonth() + 1).padStart(2, "0");
    let year = today.getFullYear();

    let formattedDate = `${day}-${month}-${year}`;

    document.getElementById("selected_date").innerText = formattedDate;

    selectedDate = `${year}-${month}-${day}`

    updateStorage('selectedDate', selectedDate)
}

function handleClickTomorrowDate(event) {
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    datepicker.setDate(tomorrow);

    let day = String(tomorrow.getDate()).padStart(2, "0");
    let month = String(tomorrow.getMonth() + 1).padStart(2, "0");
    let year = tomorrow.getFullYear();

    let formattedDate = `${day}-${month}-${year}`;

    document.getElementById("selected_date").innerText = formattedDate;

    selectedDate = `${year}-${month}-${day}`

    updateStorage('selectedDate', selectedDate)
}

function searchDepartureBusLocation(event) {
    event.stopPropagation()
    let text = document.getElementById("departure-bus-location").value;

    fetch('https://localhost:5000/home/SearchBusLocations', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            data: text
        })
    })
    .then(response => response.json())
    .then(response => {
        let dropdownMenu = document.getElementById("departure-bus-location-list")

        // Tüm mevcut öğeleri sil, ancak ilk öğeyi koru
        while (dropdownMenu.children.length > 1) {
            dropdownMenu.removeChild(dropdownMenu.lastChild);
        }

        // Yeni öğeleri ekle
        response.result.data.forEach(location => {
            let li = document.createElement("li");
            li.id = `from_location_${location.id}`;
            li.onclick = function (event) { handleSelectFromLocation(event, location.id); };

            let a = document.createElement("a");
            a.className = "dropdown-item";
            a.href = "#";
            a.textContent = location.name;

            li.appendChild(a);
            dropdownMenu.appendChild(li);
        });

        updateStorage('fromLocation', text)

    }).catch(error => console.error("Error:", error));
}

function searchDestinationBusLocation(event) {
    event.stopPropagation()
    let text = document.getElementById("destination-bus-location").value;

    fetch('https://localhost:5000/home/SearchBusLocations', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            data: text
        })
    })
    .then(response => response.json())
    .then(response => {
        let dropdownMenu = document.getElementById("destination-bus-location-list")

        // Tüm mevcut öğeleri sil, ancak ilk öğeyi koru
        while (dropdownMenu.children.length > 1) {
            dropdownMenu.removeChild(dropdownMenu.lastChild);
        }

        // Yeni öğeleri ekle
        response.result.data.forEach(location => {
            let li = document.createElement("li");
            li.id = `to_location_${location.id}`;
            li.onclick = function (event) { handleSelectToLocation(event, location.id); };

            let a = document.createElement("a");
            a.className = "dropdown-item";
            a.href = "#";
            a.textContent = location.name;

            li.appendChild(a);
            dropdownMenu.appendChild(li);
        });

        updateStorage('toLocation', text)

    }).catch(error => console.error("Error:", error));
}

function getjourney(event) {
    var fromLocation = document.getElementById("selected_from_location");

    let originId = fromLocation.getAttribute("data-selected-from-location-id");

    var toLocation = document.getElementById("selected_to_location");
    let destinationId = toLocation.getAttribute("data-selected-to-location-id");

    const params = new URLSearchParams({
        originId,
        destinationId,
        selectedDate
    });
    window.location.href = `/home/journey?${params.toString()}`;
}

function setStorageValues() {
    _storage = localStorage.getItem("obilet_index_storage");

    let fromLocationDropdown = document.getElementById("from-location-dropdown-btn");

    let toLocationDropdown = document.getElementById("to-location-dropdown-btn");

    function toLocationDropdownStorageEvent() {
        document.getElementById('destination-bus-location-btn').click()
    }
    function fromLocationDropdownStorageEvent() {
        document.getElementById('departure-bus-location-btn').click()
    }
    if (_storage) {
        _storage = decodeHtmlEntities(_storage);
        _storage = JSON.parse(_storage);

        if (_storage.departureLocationId && _storage.departureLocation) {
            var selectedFromLocationElement = document.getElementById("selected_from_location");

            selectedFromLocationElement.innerText = _storage.departureLocation;
            selectedFromLocationElement.setAttribute("data-selected-from-location-id", _storage.departureLocationId);
        }
        if (_storage.destinationLocationId && _storage.destinationLocation) {
            var selectedToLocationElement = document.getElementById("selected_to_location");

            selectedToLocationElement.innerText = _storage.destinationLocation;
            selectedToLocationElement.setAttribute("data-selected-to-location-id", _storage.destinationLocationId);
        }

        if (_storage.selectedDate) {
            selectedDate = _storage.selectedDate;

            setDateButtons()
        }

        if (_storage.fromLocation) {
            document.getElementById("departure-bus-location").value = _storage.fromLocation;

            fromLocationDropdown.addEventListener("show.bs.dropdown", fromLocationDropdownStorageEvent);
        }

        if (_storage.toLocation) {
            document.getElementById("destination-bus-location").value = _storage.toLocation;

            toLocationDropdown.addEventListener("show.bs.dropdown", toLocationDropdownStorageEvent);
        }
        return;
    }
    fromLocationDropdown.removeEventListener("show.bs.dropdown", fromLocationDropdownStorageEvent)
    toLocationDropdown.removeEventListener("show.bs.dropdown", toLocationDropdownStorageEvent)
}

function setDateButtons() {

    const date = new Date();
    let day = String(date.getDate()).padStart(2, "0");
    let month = String(date.getMonth() + 1).padStart(2, "0");
    let year = date.getFullYear();

    if (selectedDate == `${year}-${month}-${day}`) {
        document.getElementById('tomorrow-button').setAttribute("checked", "")

        document.getElementById('today-button').setAttribute("checked", "")
        return;
    }

    date.setDate(date.getDate() + 1);

    day = String(date.getDate()).padStart(2, "0");
    month = String(date.getMonth() + 1).padStart(2, "0");
    year = date.getFullYear();

    if (selectedDate == `${year}-${month}-${day}`) {
        document.getElementById('today-button').removeAttribute("checked", "")
        document.getElementById('tomorrow-button').setAttribute("checked", "")
        return;
    }

    document.getElementById('today-button').removeAttribute("checked", "")
    document.getElementById('tomorrow-button').removeAttribute("checked", "")
}

document.addEventListener("DOMContentLoaded", function () {

    document.getElementById("today-button").addEventListener("click", handleClickNowDate);

    document.getElementById("tomorrow-button").addEventListener("click", handleClickTomorrowDate);

    setStorageValues();

    var parts = selectedDate.split('-');

    var year = parts[0];
    var month = parts[1];
    var day = parts[2];

    var defaultSelectedDate = new Date(year, month - 1, day);

    let formattedDate = `${day}-${month}-${year}`;

    document.getElementById("selected_date").innerText = formattedDate;

    datepicker = flatpickr("#date-input", {
        defaultDate: defaultSelectedDate,
        dateFormat: "d-m-Y",
        locale: "tr",
        minDate: "today",
        onChange: function (selectedDates, dateStr, instance) {
            document.getElementById("selected_date").innerText = dateStr;
            selectedDate = instance.formatDate(selectedDates[0], "Y-m-d");
            updateStorage('selectedDate', selectedDate)
        }
    });
});